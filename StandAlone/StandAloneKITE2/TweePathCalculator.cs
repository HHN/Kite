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

public class TweePathCalculator
{
    private const string MetadataFilePath    = @"..\..\..\Honorar\visual_novel_meta_data.txt";
    private const string EventListFilePath   = @"..\..\..\Honorar\visual_novel_event_list.txt";
    private const string OutputFilePath      = @"..\..\..\Honorar\pathOutput.txt";
    private const string ResponseFilePath    = @"..\..\..\Honorar\response.txt";
    private static readonly string apiKey    = Environment.GetEnvironmentVariable("API_KEY");

    private static readonly HttpClient http  = new HttpClient
    {
        BaseAddress = new Uri("https://api.openai.com/")
    };
    private const string ModelName    = "gpt-4o";
    private const string ChatEndpoint = "v1/chat/completions";

    private static readonly Dictionary<string, Node> _graph                 = new Dictionary<string, Node>();
    private static readonly Dictionary<string, string> _characterToSpeakerMap = new Dictionary<string, string>();

    private static string _contextForPrompt;
    private static string _tweeContent;
    private static readonly string Seperator = "################";

    // ==========================================
    // ================ MAIN ====================
    // ==========================================
    public static async Task RunAsync()
    {
        // 1) Meta- und Twee-Dateien einlesen und parsen
        ParseMetaTweeFile(ReadTweeFile(MetadataFilePath));
        _tweeContent = ReadTweeFile(EventListFilePath);
        ParseTweeFile(_tweeContent);

        // 2) Pfade erzeugen ODER vorhandene pathOutput.txt verwenden
        if (File.Exists(OutputFilePath))
        {
            Console.WriteLine($"[GetAllPaths] Übersprungen: '{OutputFilePath}' existiert bereits. Verwende gespeicherte Pfade.");
        }
        else
        {
            Console.WriteLine("[GetAllPaths] Streaming aller Pfade startet…");
            var seen = new HashSet<string>(StringComparer.Ordinal);
            long processed = 0, uniqueCount = 0;

            // Ausgabedatei für die Pfade initial leeren
            File.WriteAllText(OutputFilePath, string.Empty, Encoding.UTF8);

            await Task.Run(() =>
            {
                foreach (var path in StreamAllPaths("Anfang", count =>
                {
                    if (count % 1_000_000 == 0)
                        Console.WriteLine($"[GetAllPaths] {count:N0} Pfade bisher…");
                }))
                {
                    processed++;

                    // nur die Bias-Entscheidungen extrahieren (wie bisher)
                    var filtered = path
                        .Where(kv => kv.Key.body.Contains(">>Bias|", StringComparison.Ordinal))
                        .ToDictionary(kv => kv.Key, kv => kv.Value);

                    // Signatur (Achtung: Dictionary-Enumeration ist nicht stabil; belassen wir hier wie bei dir)
                    var sig = string.Join("→",
                        filtered.Select(kv =>
                            $"{kv.Key.name}|{kv.Value.targetNode}|{(kv.Value.dialogueText ?? string.Empty).Replace("→","↦")}"
                        ));

                    if (seen.Add(sig))
                    {
                        uniqueCount++;
                        // in die Datei schreiben
                        WritePath(filtered);
                        WriteInFile("\n################", OutputFilePath);
                        WriteInFile($"PATH {uniqueCount} FINISHED\n", OutputFilePath);

                        if (uniqueCount == 1 || uniqueCount % 100 == 0)
                            Console.WriteLine($"[Unique] {uniqueCount:N0} einzigartige Pfade (processed={processed:N0})");
                    }
                }

                Console.WriteLine($"[GetAllPaths] Fertig! Verarbeitet: {processed:N0}, einzigartig: {uniqueCount:N0}.");
            });
        }

        // 3) Vorhandene response.txt einlesen und dortige Listen-Signaturen sammeln (zum Überspringen bereits verarbeiteter Pfade)
        var existingListSigs = LoadExistingListSignaturesFromResponse(ResponseFilePath);
        int nextIndex = GetNextIndexFromResponse(ResponseFilePath);

        Console.WriteLine($"[Resume] Bereits vorhandene Feedback-Einträge: {nextIndex - 1}. Starte bei Index #{nextIndex}.");
        Console.WriteLine($"[Resume] Bereits bekannte Pfad-Listen (Signaturen): {existingListSigs.Count:N0}");

        // 4) Prompts aus pathOutput laden
        var allPrompts = LoadPromptsFromFile(OutputFilePath);
        Console.WriteLine($"[Paths] Geladene Pfade aus pathOutput.txt: {allPrompts.Count:N0}");

        // 5) HTTP vorbereiten
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        // 6) Nur neue Feedbacks generieren (die Liste darf NICHT in existingListSigs vorkommen)
        Console.WriteLine($"[Resume] Bereits bekannte Pfad-Signaturen: {existingListSigs.Count:N0}");

        int generated = 0;
        foreach (var prompt in allPrompts)
        {
            var listItems = ExtractInOrder(prompt);
            var listSig = BuildListSignature(listItems);

            if (string.IsNullOrWhiteSpace(listSig))
            {
                // Falls aus irgendeinem Grund keine Liste gebaut werden kann, generieren wir vorsichtshalber (oder überspringen?)
                // Hier: wir generieren.
            }
            else if (existingListSigs.Contains(listSig))
            {
                // Bereits vorhanden -> überspringen
                continue;
            }

            Console.WriteLine($"Verarbeite Prompt #{nextIndex}…");
            await SendPrompt(prompt, nextIndex);
            Console.WriteLine($"Prompt #{nextIndex} gesendet und Antwort gespeichert.");

            existingListSigs.Add(listSig); // damit bei gleicher Liste später im selben Lauf nicht doppelt gesendet wird
            nextIndex++;
            generated++;
        }

        Console.WriteLine($"[Done] Neu generierte Feedbacks: {generated}.");
        
        // 7) Cleanup: alle ">--<" aus response.txt entfernen
        CleanupResponseFileArtifacts();
    }
    
    /// <summary>
    /// Entfernt alle Vorkommen des Artefakt-Tokens ">--<" aus response.txt.
    /// </summary>
    private static void CleanupResponseFileArtifacts()
    {
        try
        {
            if (!File.Exists(ResponseFilePath)) return;

            string content = File.ReadAllText(ResponseFilePath, Encoding.UTF8);
            int occurrences = Regex.Matches(content, Regex.Escape(">--<")).Count;

            if (occurrences > 0)
            {
                string cleaned = content.Replace(">--<", string.Empty);
                File.WriteAllText(ResponseFilePath, cleaned, Encoding.UTF8);
                Console.WriteLine($"[Cleanup] Entfernt {occurrences} Vorkommen von \">--<\" aus response.txt.");
            }
            else
            {
                Console.WriteLine("[Cleanup] Keine \">--<\"-Artefakte in response.txt gefunden.");
            }
            
            occurrences = Regex.Matches(content, Regex.Escape("***")).Count;
            if (occurrences > 0)
            {
                string cleaned = content.Replace("**", string.Empty);
                File.WriteAllText(ResponseFilePath, cleaned, Encoding.UTF8);
                Console.WriteLine($"[Cleanup] Entfernt {occurrences} Vorkommen von \"***\" aus response.txt.");
            }
            else
            {
                Console.WriteLine("[Cleanup] Keine \"***\"-Artefakte in response.txt gefunden.");
            }
            
            occurrences = Regex.Matches(content, Regex.Escape("**")).Count;
            if (occurrences > 0)
            {
                string cleaned = content.Replace("**", string.Empty);
                File.WriteAllText(ResponseFilePath, cleaned, Encoding.UTF8);
                Console.WriteLine($"[Cleanup] Entfernt {occurrences} Vorkommen von \"**\" aus response.txt.");
            }
            else
            {
                Console.WriteLine("[Cleanup] Keine \"**\"-Artefakte in response.txt gefunden.");
            }
            
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Cleanup] Fehler beim Bereinigen von response.txt: {ex.Message}");
        }
    }

    // ==========================================
    // ======= Hilfsfunktionen für Resume =======
    // ==========================================

    /// <summary>
    /// Liest response.txt (falls vorhanden) und extrahiert alle vorhandenen Listen-Signaturen (Zeile mit [ ... ]).
    /// </summary>
    private static HashSet<string> LoadExistingListSignaturesFromResponse(string responsePath)
    {
        var set = new HashSet<string>(StringComparer.Ordinal);

        if (!File.Exists(responsePath))
            return set;

        // Wir parsen Blöcke ###<n> ... #$% und nehmen die erste Zeile, die wie [ ... ] aussieht.
        var content = File.ReadAllText(responsePath, Encoding.UTF8);
        var startRegex = new Regex(@"(?m)^###\s*(\d+)\s*$");
        const string endToken = "#$%";

        var starts = startRegex.Matches(content).Cast<Match>().ToList();
        for (int i = 0; i < starts.Count; i++)
        {
            int blockStart = starts[i].Index + starts[i].Length;
            int nextStartPos = (i + 1 < starts.Count) ? starts[i + 1].Index : content.Length;

            int endPos = content.IndexOf(endToken, blockStart, nextStartPos - blockStart, StringComparison.Ordinal);
            int blockEnd = (endPos >= 0) ? endPos : nextStartPos;

            string block = content.Substring(blockStart, blockEnd - blockStart);

            // erste [ ... ]-Zeile im Block
            string listRaw = ExtractFirstBracketLine(block);
            if (listRaw == null) continue;

            // zur Signatur normalisieren
            var inner = listRaw.Trim();
            if (inner.StartsWith("[")) inner = inner[1..];
            if (inner.EndsWith("]")) inner = inner[..^1];

            var items = inner.Split(';').Select(NormalizeItem).Where(s => s.Length > 0);
            var sig = string.Join(" ; ", items); // Semikolon + Spaces stabilisieren
            if (!string.IsNullOrWhiteSpace(sig))
                set.Add(sig);
        }
        Console.WriteLine($"[Response] Bereits vorhandene Feedback-Signaturen: {set.Count:N0}");

        return set;
    }

    /// <summary>
    /// Bestimmt den nächsten Index (###<n>) für neue Einträge in response.txt.
    /// </summary>
    private static int GetNextIndexFromResponse(string responsePath)
    {
        if (!File.Exists(responsePath))
            return 1;

        int max = 0;
        foreach (var line in File.ReadLines(responsePath, Encoding.UTF8))
        {
            var m = Regex.Match(line, @"^\s*###\s*(\d+)\s*$");
            if (m.Success && int.TryParse(m.Groups[1].Value, out int n))
                if (n > max) max = n;
        }
        return max + 1;
    }

    private static string ExtractFirstBracketLine(string block)
    {
        // Suche nach einer Zeile, die mit '[' beginnt und mit ']' endet (ggf. mit Spaces)
        foreach (var line in block.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None))
        {
            var trimmed = line.Trim();
            if (trimmed.StartsWith("[") && trimmed.EndsWith("]"))
                return trimmed;
        }
        return null;
    }

    private static string BuildListSignature(IEnumerable<string> items)
    {
        if (items == null) return null;
        var norm = items.Select(NormalizeItem).Where(s => s.Length > 0).ToList();
        if (norm.Count == 0) return null;
        // gleiche Join-Form wie beim Einlesen aus response.txt
        return string.Join(" ; ", norm);
    }

    private static string NormalizeItem(string s)
    {
        if (s == null) return "";
        s = s.Trim();
        s = Regex.Replace(s, @"\s+", " "); // Whitespace zusammenziehen
        s = s.Replace("→", "↦");           // Kollisionsschutz, falls vorkommend
        return s;
    }

    // ==========================================
    // ======= Dein bestehender Code (teils) =====
    // ==========================================

    /// <summary>
    /// Streaming-DFS: liefert jeden vollständigen Pfad als Dictionary zurück.
    /// Ruft progressCallback(count) bei jedem Pfad auf.
    /// </summary>
    public static IEnumerable<Dictionary<Node, Link>> StreamAllPaths(
        string startNode,
        Action<long>? progressCallback = null)
    {
        var currentPath = new Dictionary<Node, Link>();
        long counter = 0;

        IEnumerable<Dictionary<Node, Link>> DFS(Node node)
        {
            if (!_graph.ContainsKey(node.name))
                yield break;

            var links = _graph[node.name].links;
            if (links.Count == 0 || node.name == "Ende")
            {
                counter++;
                progressCallback?.Invoke(counter);
                yield return new Dictionary<Node, Link>(currentPath);
            }
            else
            {
                foreach (var neighbor in links)
                {
                    currentPath[node] = neighbor;
                    foreach (var p in DFS(_graph[neighbor.targetNode]))
                        yield return p;
                }
                currentPath.Remove(node);
            }
        }

        if (_graph.ContainsKey(startNode))
        {
            foreach (var p in DFS(_graph[startNode]))
                yield return p;
        }
    }

    private static List<string> LoadPromptsFromFile(string filePath)
    {
        var result = new List<string>();
        if (!File.Exists(filePath)) return result;

        var lines = File.ReadAllLines(filePath, Encoding.UTF8);
        var sb = new StringBuilder();

        // Separator: eine Zeile, die nur aus '#' besteht
        var sepRegex = new Regex(@"^\s*#+\s*$");

        void flush()
        {
            var cleaned = sb.ToString()
                .Split(new[] { "\r\n", "\n" }, StringSplitOptions.None)
                .Where(l =>
                    !string.IsNullOrWhiteSpace(l) &&
                    !l.StartsWith("PATH", StringComparison.OrdinalIgnoreCase) &&
                    !l.Contains("FINISHED", StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (cleaned.Count > 0)
                result.Add(string.Join(Environment.NewLine, cleaned).Trim());

            sb.Clear();
        }

        foreach (var raw in lines)
        {
            if (sepRegex.IsMatch(raw))
            {
                flush();
            }
            else
            {
                sb.AppendLine(raw);
            }
        }

        flush(); // letzten Block mitnehmen
        return result;
    }


    private static string BuildPrompt(IReadOnlyList<string> lines)
        => string.Join(Environment.NewLine, lines).Trim();

    public static async Task SendPrompt(string prompt, int index)
    {
        string original = prompt;
        prompt = GetCompletePrompt(prompt);

        var requestBody = new
        {
            model = ModelName,
            messages = new[] { new { role = "user", content = prompt } }
        };
        string jsonRequest = JsonSerializer.Serialize(requestBody);
        using var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        var response = await http.PostAsync(ChatEndpoint, content);
        response.EnsureSuccessStatusCode();

        string jsonResponse = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(jsonResponse);
        string answer = doc.RootElement
                           .GetProperty("choices")[0]
                           .GetProperty("message")
                           .GetProperty("content")
                           .GetString() ?? "";

        string entry =
            $"###{index}\n\n[{string.Join("; ", ExtractInOrder(original))}]\n\n\n\n{answer}\n\n\n\n#$%\n\n\n\n";
        await File.AppendAllTextAsync(ResponseFilePath, entry, Encoding.UTF8);
    }

    private static string GetCompletePrompt(string prompt)
    {
        return
            "Du bist eine Geschlechterforscherin.\n" +
            "Deine Aufgabe ist es, den folgenden Dialogausschnitt auf Diskriminierung hin zu untersuchen.\n\n" +
            _contextForPrompt + "\n\n" +
            "Die Spielerin hat den gesamten Dialog durchgespielt. Ich gebe Dir allerdings nur den Dialogausschnitt, in dem Biases vorkommen, weil dies die relevanten Passagen sind. Du findest dort jeweils die Aussage des Gegenübers, den Namen des Bias, der hier zum Tragen kommt und die Reaktion der Spielerin. Nutze die Information über den Bias als Basis für Deine Interpretation.\nAußerdem gebe ich dir die twee-Datei mit allen möglichen Gesprächspassagen. Dies ist aber nur für Dich zur Kontextualisierung, nicht zur Analyse.\nSchreibe einen Analysetext über den Dialogausschnitt, in dem Biases vorkommen. Stelle die Biases und Verzerrungen dar, auf die du dich beziehst (zur Erläuterung findest du unten eine Liste mit Geschlechterbiases im Gründungsprozess). Drücke Dich allgemeinverständlich aus. Verwende bei der Benennung der Biases ausschließlich den deutschen Namen. Setze keinen einleitenden Text davor, sondern starte direkt mit der Ansprache an die Spielerin.\nAnalysiere auch das Verhalten der Spielerin und ihre Reaktionen auf diese Biases zu den jeweiligen Biases mit konkreten Beispielen aus dem Dialog.\nStelle die Vorteile des Verhaltens der Gründerin dar und deute vorsichtig an, welche Nachteile ihre Reaktion haben könnte.\nFühre das Nicht-Ansprechen geschlechterstereotyper Annahmen nicht bei den Nachteilen auf.\nSei vorsichtig mit dem Hinweis, Biases und Stereotype direkt anzusprechen, weil dies zwar generell sinnvoll sein kann, die Spielerin aber in erster Linie darauf achten muss, dass sie das Gespräch so führt, dass sie im Gespräch erfolgreich ist.\nNutze geschlechtergerechte Sprache (z.B. Gründer*innen, weibliche Gründerinnen).\nRichte den Text in der du-Form an die Spielerin.\nSprich niemals von 'Spielerin', 'Gründerin' oder einer anderen Bezeichnung als 'du'. Dies muss unbedingt beachtet werden, da die Antwort ansonst nicht weiter verwendet werden kann.\nSei wohlwollend und ermunternd. Sprich die Gründerin nicht mit ihrem Namen an. Formuliere den Text aus einer unbestimmten Ich-Perspektive." +
            "Hier die Liste mit Geschlechterbiases im Gründungsprozess:\n\n" +
            "Finanzielle und Geschäftliche Herausforderungen\n\n" +
            "Finanzierungszugang \n>>Bias|AccessToFinancing<<\nBias: Schwierigkeiten von Frauen, Kapital für ihre Unternehmen zu beschaffen.\n\n" +
            "Gender Pay Gap\n>>Bias|GenderPayGap<<\nBias: Lohnungleichheit zwischen Männern und Frauen.\n\n" +
            "Unterbewertung weiblich geführter Unternehmen\n>>Bias|UndervaluationFemaleManagedCompany<<\nBias: Geringere Bewertung von Unternehmen, die von Frauen geführt werden.\n\n" +
            "Risikovermeidungs-Bias\n>>Bias|RiskAversionBias<<\nBias: Wahrnehmung von Frauen als risikoaverser.\n\n" +
            "Bestätigungsverzerrung\n>>Bias|ConfirmationBias<<\nBias: Tendenz, Informationen zu interpretieren, die bestehende Vorurteile bestätigen.\n\n" +
            "Gesellschaftliche Erwartungen & soziale Normen\n\n" +
            "Tokenism\n>>Bias|Tokenism<<\nBias: Wahrnehmung von Frauen in unternehmerischen Kreisen als Alibifiguren.\n\n" +
            "Bias in der Wahrnehmung von Führungsfähigkeiten\n>>Bias|BiasInThePerceptionOfLeadershipSkills<<\nBias: Infragestellung der Führungsfähigkeiten von Frauen.\n\n" +
            "Benevolenter Sexismus\n>>Bias|BenevolentSexism<<\nBias: Schützend oder wohlwollend gemeinte, aber dennoch herabwürdigende Haltungen gegenüber Frauen, " +
            "die sie als weniger kompetent und in Bedarf von männlicher Hilfe darstellen.\n\n" +
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
            "Unbewusste Biases in der Kommunikation\n>>Bias|UnconsciousBiasInCommunication<<\nBias: Herabsetzende Art und Weise, wie über Frauenunternehmen gesprochen wird.\n\n\n" +
            "Hier der Dialogausschnitt, in denen Biases vorgekommen sind. Analysiere nur diesen Ausschnitt.\nSpreche in deiner Antwort immer von 'dem Dialog' und NIE! von Dialogausschnitt oder Dialogszene. Dies ist extrem wichtig, da die Antwort sonst nicht verwendet werden kann." +
            prompt +
            "Hier zur Kontextualisierung die twee-Datei mit dem gesamten Dialog. Aber du sollst dich wie oben dargestellt nur auf den oben dargestellten Dialogabschnitt beziehen." +
            _tweeContent;
    }

    public static List<string> ExtractInOrder(string input)
    {
        var results = new List<string>();
        var lines = input.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        var pattern = new Regex(@"^\s*Spielerin:\s*(.+)$", RegexOptions.Compiled);
        bool capturing = false;
        var buf = new StringBuilder();

        foreach (var line in lines)
        {
            if (line.StartsWith(">>") && line.EndsWith("<<"))
            {
                var content = line.Substring(2, line.Length - 4).Trim();
                if (content == "--")
                {
                    if (capturing)
                    {
                        var block = buf.ToString().Trim();
                        if (block.Length > 0) results.Add(block);
                        buf.Clear();
                        capturing = false;
                    }
                }
                else
                {
                    if (capturing)
                    {
                        var block = buf.ToString().Trim();
                        if (block.Length > 0) results.Add(block);
                        buf.Clear();
                    }
                    capturing = true;
                }
            }
            else
            {
                var m = pattern.Match(line);
                if (m.Success)
                {
                    results.Add(m.Groups[1].Value.Trim());
                }
                else if (capturing)
                {
                    buf.AppendLine(line);
                }
            }
        }

        if (capturing)
        {
            var block = buf.ToString().Trim();
            if (block.Length > 0) results.Add(block);
        }

        return results;
    }

    public static string ReadTweeFile(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Datei nicht gefunden: {filePath}");
        return File.ReadAllText(filePath);
    }

    public static void ParseMetaTweeFile(string tweeContent)
    {
        var metaPattern = @"""(talkingPartner\d+)""\s*:\s*""([^""]*)""";
        var matches = Regex.Matches(tweeContent, metaPattern);
        Console.WriteLine("Matches: " + matches.Count);
        foreach (Match match in matches)
        {
            string key = match.Groups[1].Value.Trim();
            string val = match.Groups[2].Value.Trim();
            string num = Regex.Match(key, @"\d+").Value;
            string dyn = $@"Character{int.Parse(num)}";
            if (!_characterToSpeakerMap.ContainsKey(dyn))
                _characterToSpeakerMap[dyn] = val;
        }
        _characterToSpeakerMap[@"InfoNachrichtWirdAngezeigt"]   = "Info";
        _characterToSpeakerMap[@"SpielerinCharakterSpricht"] = "Spielerin";

        Console.WriteLine("CharacterToSpeakerMap:");
        foreach (var e in _characterToSpeakerMap)
            Console.WriteLine($"  {e.Key} -> {e.Value}");

        try
        {
            using var doc = JsonDocument.Parse(tweeContent);
            if (doc.RootElement.TryGetProperty("contextForPrompt", out var ctx))
            {
                _contextForPrompt = ctx.GetString()!;
                Console.WriteLine("contextForPrompt: " + _contextForPrompt);
            }
            else Console.WriteLine("Key 'contextForPrompt' nicht gefunden.");
        }
        catch (JsonException ex)
        {
            Console.WriteLine("JSON-Parse-Fehler: " + ex.Message);
        }
    }

    public static void ParseTweeFile(string tweeContent)
    {
        var nodePattern = @"::\s*([^\n\{\[\|]+).*?\n((?:.|\n)*?)(?=(::|$))";
        var linkPattern = @"\[\[(?:(.*?)(?:\s*(?:\||->)\s*(.*?))|([^|\]]+))\]\]";
        var matches = Regex.Matches(tweeContent, nodePattern);

        foreach (Match match in matches)
        {
            string nodeName = match.Groups[1].Value.Trim();
            string body     = match.Groups[2].Value;
            string dialogue = "";

            foreach (var sp in _characterToSpeakerMap)
                body = body.Replace(sp.Key, sp.Value);

            var spMatch = Regex.Match(body, @">>(.*?)\|.*?<<\s*(.*?)\s*>>--<<", RegexOptions.Singleline);
            if (spMatch.Success)
                dialogue = $"{spMatch.Groups[1].Value.Trim()}: {spMatch.Groups[2].Value.Trim()}\n";

            var biasMatch = Regex.Match(body, @">>Bias\|(.*?)<<");
            if (biasMatch.Success)
                dialogue += $"Bias: {biasMatch.Groups[1].Value.Trim()}";

            var links = new List<Link>();
            var linkCount = new Dictionary<string,int>();
            foreach (Match lm in Regex.Matches(body, linkPattern))
            {
                var link = new Link
                {
                    targetNode   = !string.IsNullOrEmpty(lm.Groups[2].Value)
                                    ? lm.Groups[2].Value.Trim()
                                    : lm.Groups[3].Value.Trim(),
                    dialogueText = lm.Groups[1].Value.Trim()
                };
                links.Add(link);
                linkCount[link.targetNode] = linkCount.ContainsKey(link.targetNode)
                    ? linkCount[link.targetNode] + 1
                    : 1;
            }

            body = Regex.Replace(body, @"\[\[.*?->.*?\]\]\s*", "", RegexOptions.Singleline);

            if (!_graph.ContainsKey(nodeName))
            {
                var n = new Node(nodeName, body, links) { dialogue = dialogue };
                n.linkCount = linkCount;
                _graph[nodeName] = n;
            }
            else
            {
                _graph[nodeName].links.AddRange(links);
            }
        }

        if (!_graph.ContainsKey("Ende"))
            _graph["Ende"] = new Node("Ende", "", new List<Link>());
    }

    public static bool PathEqual(Dictionary<Node, Link> a, Dictionary<Node, Link> b)
    {
        if (a.Count != b.Count) return false;
        foreach (var kv in a)
        {
            if (!b.TryGetValue(kv.Key, out var val) || !val.Equals(kv.Value))
                return false;
        }
        return true;
    }

    public static void WritePath(Dictionary<Node, Link> path)
    {
        foreach (var kv in path)
        {
            WriteInFile(kv.Key.body.Trim(), OutputFilePath);
            if (!string.IsNullOrEmpty(kv.Value.dialogueText))
                WriteInFile("Spielerin: " + kv.Value.dialogueText, OutputFilePath);
        }
    }

    public static void WriteInFile(string output, string path)
    {
        if (!File.Exists(path))
            File.WriteAllText(path, output + Environment.NewLine, Encoding.UTF8);
        else
            File.AppendAllText(path, output + Environment.NewLine, Encoding.UTF8);
    }
}

public class Node
{
    public string name;
    public string body;
    public string dialogue;
    public List<Link> links;
    public Dictionary<string,int> linkCount;

    public Node(string _name, string _body, List<Link> _links)
    {
        name = _name;
        body = _body;
        links = _links;
        linkCount = new Dictionary<string,int>();
    }

    public override bool Equals(object obj)
        => obj is Node o && name == o.name;

    public override int GetHashCode()
        => HashCode.Combine(name);
}

public class Link
{
    public string targetNode;
    public string dialogueText;

    public override bool Equals(object obj)
        => obj is Link o
           && targetNode   == o.targetNode
           && dialogueText == o.dialogueText;

    public override int GetHashCode()
        => HashCode.Combine(targetNode, dialogueText);
}
