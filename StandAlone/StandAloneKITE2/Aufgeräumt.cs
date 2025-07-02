using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class TweePathCalculator2
{
    // Configuration and state
    private const string MetadataFilePath = @"..\..\..\Notarin\visual_novel_meta_data.txt";
    private const string EventListFilePath = @"..\..\..\Notarin\visual_novel_event_list.txt";
    private const string OutputFilePath = @"..\..\..\Notarin\pathOutput.txt";
    private const string ResponseFilePath = @"..\..\..\Notarin\response.txt";
    private const string ApiKey = "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";

    private static readonly HttpClient Http = new HttpClient { BaseAddress = new Uri("https://api.openai.com/") };
    private static readonly Dictionary<string, Node2> Graph = new Dictionary<string, Node2>();
    private static readonly Dictionary<string, string> CharacterToSpeaker = new Dictionary<string, string>();

    private static string _contextForPrompt;
    private static string _completeTweeContent;

    // public static async Task Main(string[] args)
    // {
    //     // Load and parse metadata
    //     var metaJson = ReadFile(MetadataFilePath);
    //     ParseMetaData(metaJson);
    //
    //     // Load and parse event list
    //     _completeTweeContent = ReadFile(EventListFilePath);
    //     ParseTweeContent(_completeTweeContent);
    //
    //     // Calculate and output all unique bias paths
    //     var allPaths = GetAllPaths("Anfang");
    //     var uniquePaths = ExtractUniqueBiasPaths(allPaths);
    //
    //     Console.WriteLine($"Unique bias paths found: {uniquePaths.Count}");
    //
    //     // Prepare prompts
    //     Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
    //     var prompts = LoadPrompts(OutputFilePath).ToList();
    //
    //     int index = 1;
    //     foreach (var prompt in prompts)
    //     {
    //         Console.WriteLine($"Sending prompt #{index}");
    //         //await SendAnalysisRequestAsync(prompt, index);
    //         index++;
    //     }
    // }

    // Reads entire file content as UTF-8 string
    private static string ReadFile(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"File not found: {path}");
        return File.ReadAllText(path, Encoding.UTF8);
    }

    // Parses JSON metadata for character mapping and context
    private static void ParseMetaData(string json)
    {
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        // 1) talkingPartnerXX → CharacterXX
        foreach (var prop in root.EnumerateObject())
        {
            if (prop.Name.StartsWith("talkingPartner", StringComparison.Ordinal))
            {
                // z.B. Name = "talkingPartner02"
                if (int.TryParse(prop.Name["talkingPartner".Length..], out int idx))
                {
                    var speaker = prop.Value.GetString() ?? "";
                    if (!string.IsNullOrWhiteSpace(speaker))
                        CharacterToSpeaker[$"Character{idx:D2}"] = speaker;
                }
            }
        }

        // 2) feste Mappings
        CharacterToSpeaker["InfoNachrichtWirdAngezeigt"]    = "Info";
        CharacterToSpeaker["SpielerinCharakterSpricht"]    = "Spielerin";

        // 3) contextForPrompt
        if (root.TryGetProperty("contextForPrompt", out var ctx))
            _contextForPrompt = ctx.GetString() ?? "";
    }


    // Parses Twee event Node2s into a graph structure
    private static void ParseTweeContent(string content)
    {
        const string Node2Pattern = @"::\s*([^\n\{\[\|]+).*?\n((?:.|\n)*?)(?=(::|$))";
        const string Link2Pattern = @"\[\[(?:(.*?)(?:\s*(?:\||->)\s*(.*?))|([^|\]]+))\]\]";
        var Node2Matches = Regex.Matches(content, Node2Pattern, RegexOptions.Singleline);

        foreach (Match match in Node2Matches)
        {
            var Node2Name = match.Groups[1].Value.Trim();
            var body = match.Groups[2].Value;

            // Replace character placeholders
            foreach (var kv in CharacterToSpeaker)
                body = body.Replace(kv.Key, kv.Value);

            // Build Node2 and Link2s
            var Node2 = new Node2(Node2Name, body);
            ExtractLink2s(body, Node2, Link2Pattern);
            Graph[Node2Name] = Node2;
        }

        // Ensure an "Ende" Node2 exists
        if (!Graph.ContainsKey("Ende"))
            Graph["Ende"] = new Node2("Ende", string.Empty);
    }

    // Extracts outgoing Link2s from a Node2 body
    private static void ExtractLink2s(string body, Node2 Node2, string Link2Pattern)
    {
        var regex = new Regex(Link2Pattern, RegexOptions.Singleline);
        var matches = regex.Matches(body);
        foreach (Match lm in matches)
        {
            var Link2 = new Link2
                {
                    TargetNode2 = !string.IsNullOrEmpty(lm.Groups[2].Value)
                        ? lm.Groups[2].Value.Trim()
                        : lm.Groups[3].Value.Trim(),
                    DialogueText = lm.Groups[1].Value.Trim()
                };
            Node2.Link2s.Add(Link2);
        }
    }

    // Finds all paths from start Node2 to "Ende"
    private static List<List<Link2Step>> GetAllPaths(string start)
    {
        var result = new List<List<Link2Step>>();
        void DFS(string current, List<Link2Step> path)
        {
            if (!Graph.ContainsKey(current)
                || current == "Ende"
                || Graph[current].Link2s.Count == 0)
            {
                result.Add(new List<Link2Step>(path));
                return;
            }
            foreach (var Link2 in Graph[current].Link2s)
            {
                path.Add(new Link2Step(current, Link2));
                DFS(Link2.TargetNode2, path);
                path.RemoveAt(path.Count - 1);
            }
        }
        DFS(start, new List<Link2Step>());
        return result;
    }

    // Filters and deduplicates paths that contain bias Node2s
    private static List<List<Link2Step>> ExtractUniqueBiasPaths(List<List<Link2Step>> paths)
    {
        var unique = new List<List<Link2Step>>();
        foreach (var path in paths)
        {
            var biasSteps = path.Where(ls => ls.Link2PresenceInNode2(Graph[ls.Node2Name].Body)).ToList();
            if (!unique.Any(existing => ArePathsEqual(existing, biasSteps)))
                unique.Add(biasSteps);
        }

        // Persist to output file
        File.WriteAllText(OutputFilePath, string.Empty);
        int counter = 1;
        foreach (var p in unique)
        {
            foreach (var step in p)
                AppendLineToFile(step.Node2Body.Trim(), OutputFilePath);
            AppendLineToFile("################", OutputFilePath);
            AppendLineToFile($"PATH {counter} FINISHED", OutputFilePath);
            counter++;
        }
        return unique;
    }

    // Sends a completion request and appends the response
    private static async Task SendAnalysisRequestAsync(string prompt, int index)
    {
        var fullPrompt = BuildFullPrompt(prompt);
        var payload = JsonSerializer.Serialize(new
        {
            model = "gpt-4o",
            messages = new[] { new { role = "user", content = fullPrompt } }
        });

        using var content = new StringContent(payload, Encoding.UTF8, "application/json");
        var response = await Http.PostAsync("/v1/chat/completions", content);
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var answer = JsonDocument.Parse(jsonResponse)
                                  .RootElement.GetProperty("choices")[0]
                                  .GetProperty("message").GetProperty("content").GetString();

        var entry = $"###{index}\n\n[{prompt}]\n\n{answer}\n#$%\n";
        await File.AppendAllTextAsync(ResponseFilePath, entry, Encoding.UTF8);
    }

    // Constructs the full system prompt with context and biases list
    private static string BuildFullPrompt(string snippet)
    {
        return new StringBuilder()
            .AppendLine("Du bist eine Geschlechterforscherin.\\n Deine Aufgabe ist es, den folgenden Dialogausschnitt auf Diskriminierung hin zu untersuchen. \\n\\n")
            .AppendLine(_contextForPrompt)
            .AppendLine("Die Spielerin hat den gesamten Dialog durchgespielt. Ich gebe Dir allerdings nur den Dialogausschnitt, in dem Biases vorkommen, weil dies die relevanten Passagen sind. Du findest dort jeweils die Aussage des Gegenübers, den Namen des Bias, der hier zum Tragen kommt und die Reaktion der Spielerin. Nutze die Information über den Bias als Basis für Deine Interpretation.\nAußerdem gebe ich dir die twee-Datei mit allen möglichen Gesprächspassagen. Dies ist aber nur für Dich zur Kontextualisierung, nicht zur Analyse.\nSchreibe einen Analysetext über den Dialog. Berücksichtige dabei nur die Dialogausschnitte, in denen Biases vorkommen. Stelle die Biases und Verzerrungen dar, auf die du dich beziehst (zur Erläuterung findest du unten eine Liste mit Geschlechterbiases im Gründungsprozess). Drücke Dich allgemeinverständlich aus. Verwende bei der Benennung der Biases ausschließlich den deutschen Namen. Setze keinen einleitenden Text davor, sondern starte direkt mit der Ansprache an die Spielerin.\nAnalysiere auch das Verhalten der Spielerin und ihre Reaktionen auf diese Biases zu den jeweiligen Biases mit konkreten Beispielen aus dem Dialog.\nStelle die Vorteile des Verhaltens der Gründerin dar und deute vorsichtig an, welche Nachteile ihre Reaktion haben könnte.\nFühre das Nicht-Ansprechen geschlechterstereotyper Annahmen nicht bei den Nachteilen auf.\nSei vorsichtig mit dem Hinweis, Biases und Stereotype direkt anzusprechen, weil dies zwar generell sinnvoll sein kann, die Spielerin aber in erster Linie darauf achten muss, dass sie das Gespräch so führt, dass sie im Gespräch erfolgreich ist.\nNutze geschlechtergerechte Sprache (z.B. Gründer*innen, weibliche Gründerinnen).\nRichte den Text in der Du-Form an die Spielerin. Sei wohlwollend und ermunternd. Sprich die Gründerin nicht mit ihrem Namen an. Formuliere den Text aus einer unbestimmten Ich-Perspektive. \n\n"
                 + "Hier die Liste mit Geschlechterbiases im Gründungsprozess:\n\n" +
                 "Finanzielle und Geschäftliche Herausforderungen\n\n" +
                 "Finanzierungszugang \n>>Bias|AccessToFinancing<<\nBias: Schwierigkeiten von Frauen, Kapital für ihre Unternehmen zu beschaffen.\n\n" +
                 "Gender Pay Gap\n>>Bias|GenderPayGap<<\nBias: Lohnungleichheit zwischen Männern und Frauen.\n\n" +
                 "Unterbewertung weiblich geführter Unternehmen\n>>Bias|UndervaluationFemaleManagedCompany<<\nBias: Geringere Bewertung von Unternehmen, die von Frauen geführt werden.\n\n" +
                 "Risk Aversion Bias\n>>Bias|RiskAversionBias<<\nBias: Wahrnehmung von Frauen als risikoaverser.\n\n" +
                 "Bestätigungsverzerrung\n>>Bias|ConfirmationBias<<\nBias: Tendenz, Informationen zu interpretieren, die bestehende Vorurteile bestätigen.\n\n" +
                 "Gesellschaftliche Erwartungen & soziale Normen\n\n" +
                 "Tokenism\n>>Bias|Tokenism<<\nBias: Wahrnehmung von Frauen in unternehmerischen Kreisen als Alibifiguren.\n\n" +
                 "Bias in der Wahrnehmung von Führungsfähigkeiten\n>>Bias|BiasInThePerceptionOfLeadershipSkills<<\nBias: Infragestellung der Führungsfähigkeiten von Frauen.\n\n" +
                 "Benevolenter Sexismus\n>>Bias|BenevolentSexism<<\nBias: Schützend oder wohlwollend gemeinte, aber dennoch herabwürdigende Haltungen gegenüber Frauen, die sie als weniger kompetent und in Bedarf von männlicher Hilfe darstellen.\n\n" +
                 "Alter- und Generationen-Biases\n>>Bias|AgeAndGenerationsBiases<<\nBias: Diskriminierung aufgrund von Altersstereotypen.\n\n" +
                 "Stereotype gegenüber Frauen in nicht-traditionellen Branchen\n>>Bias|StereotypesAboutWomenInNon<<\nBias: Widerstände gegen Frauen in männlich dominierten Feldern.\n\n" +
                 "Prove-it-Again-Bias\n>>Bias|ProveItAgainBias<<\nBias: Anforderung an Frauen, insbesondere in technischen Bereichen, ihre Kompetenzen wiederholt zu beweisen.\n\n" +
                 "Wahrnehmung & Führungsrollen\n\n" +
                 "Heteronormativität\n>>Bias|Heteronormativity<<\nBias: Annahmen und Erwartungen, die auf der Vorstellung beruhen, dass Heterosexualität die einzige oder bevorzugte sexuelle Orientierung ist.\n\n" +
                 "Maternal Bias\n>>Bias|BiasesAgainstWomenWithChildren<<\nBias: Annahmen über geringere Engagementbereitschaft von Müttern oder potenziellen Müttern.\n\n" +
                 "Erwartungshaltung bezüglich Familienplanung\n>>Bias|ExpectationsRegardingFamilyPlanning<<\nBias: Annahmen über zukünftige Familienplanung bei Frauen im gebärfähigen Alter.\n\n" +
                 "Work-Life-Balance-Erwartungen\n>>Bias|WorkLifeBalanceExpectations<<\nBias: Druck auf Frauen, ein Gleichgewicht zwischen Beruf und Familie zu finden.\n\n" +
                 "Geschlechterspezifische Stereotype\n>>Bias|GenderSpecificStereotypes<<\nBias: Annahmen über geringere Kompetenz von Frauen in bestimmten Bereichen.\n\n" +
                 "Psychologische Barrieren & kommunikative Hindernisse\n\n" +
                 "Doppelte Bindung (Tightrope Bias)\n>>Bias|TightropeBias<<\nBias: Konflikt zwischen der Wahrnehmung als zu weich oder zu aggressiv.\n\n" +
                 "Mikroaggressionen\n>>Bias|Microaggression<<\nBias: Subtile Formen der Diskriminierung gegenüber Frauen.\n\n" +
                 "Leistungsattributions-Bias\n>>Bias|PerformanceAttributionBias<<\nBias: Externe Zuschreibung von Erfolgen von Frauen.\n\n" +
                 "Unbewusste Biases in der Kommunikation\n>>Bias|UnconsciousBiasInCommunication<<\nBias: Herabsetzende Art und Weise, wie über Frauenunternehmen gesprochen wird.\n\n")
            .AppendLine("Hier der Dialogausschnitt, in denen Biases vorgekommen sind. Analysiere nur diesen Ausschnitt. \\n\\n")
            .AppendLine(snippet)
            .AppendLine("Hier zur Kontextualisierung die twee-Datei mit dem gesamten Dialog. Aber du sollst dich wie oben dargestellt nur auf den oben dargestellten Dialogabschnitt beziehen. \n\n")
            .Append(_completeTweeContent)
            .ToString();
    }
    
    // Splits the output text into prompt segments, removing path markers
    private static IEnumerable<string> LoadPrompts(string path)
    {
        var parts = ReadFile(path).Split("################", StringSplitOptions.RemoveEmptyEntries);
        foreach (var part in parts)
        {
            var cleaned = part.Split(new[] {'\r','\n'}, StringSplitOptions.RemoveEmptyEntries)
                              .Where(l => !l.StartsWith("PATH", StringComparison.OrdinalIgnoreCase) &&
                                          !l.Contains("FINISHED", StringComparison.OrdinalIgnoreCase));
            var prompt = string.Join(Environment.NewLine, cleaned).Trim();
            if (!string.IsNullOrWhiteSpace(prompt))
                yield return prompt;
        }
    }

    // Appends a line to a file, creating it if necessary
    private static void AppendLineToFile(string line, string path)
    {
        if (!File.Exists(path))
            File.WriteAllText(path, line + Environment.NewLine);
        else
            File.AppendAllText(path, line + Environment.NewLine);
    }

    // Compares two bias paths for equality
    private static bool ArePathsEqual(List<Link2Step> a, List<Link2Step> b)
    {
        if (a.Count != b.Count) return false;
        return !a.Where((t, i) => !t.Equals(b[i])).Any();
    }

    // Represents a traversal step
    private record Link2Step(string Node2Name, Link2 Link2)
    {
        public string Node2Body => Link2 != null ? Link2.DialogueText : string.Empty;
        public bool Link2PresenceInNode2(string body) => body.Contains(">>Bias|");
    }
    
    
    
    
    
    
    
    
    
    
    
    

    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}

// Node2 and Link2 classes
public class Node2
{
    public string Name { get; init; }
    public string Body { get; init; }
    public List<Link2> Link2s { get; } = new List<Link2>();

    public Node2(string name, string body)
    {
        Name = name;
        Body = body;
    }
}

public class Link2
{
    public string TargetNode2 { get; init; }
    public string DialogueText { get; init; }
}