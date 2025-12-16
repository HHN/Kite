using System;
using System.Threading.Tasks;
using StandAloneKITE2;

public static class Program
{
    // Change HERE: which “program” should run?
    //   App.Twee      -> starts TweePathCalculator
    //   App.Audit     -> starts FeedbackPathAudit
    //   App.Coverage  -> starts DialogListCoverageAudit
    private const App Active = App.Coverage;

    public static async Task Main(string[] args)
    {
        switch (Active)
        {
            case App.Twee:
                Console.WriteLine("[Bootstrap] Starte TweePathCalculator …");
                await TweePathCalculator.RunAsync();
                break;

            case App.Audit:
                Console.WriteLine("[Bootstrap] Starte FeedbackPathAudit …");
                FeedbackPathAudit.FeedbackPathAudit.Run();
                break;

            case App.Coverage:
                Console.WriteLine("[Bootstrap] Starte DialogListCoverageAudit …");
                DialogListCoverageAudit.Run();
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private enum App { Twee, Audit, Coverage }
}

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
        private const string MetadataFilePath = @"..\..\..\Notarin\visual_novel_meta_data.txt";
        private const string EventListFilePath = @"..\..\..\Notarin\visual_novel_event_list.txt";
        private const string OutputFilePath = @"..\..\..\Notarin\pathOutput.txt";
        private const string ResponseFilePath = @"..\..\..\Notarin\response.txt";
        private static readonly string apiKey = Environment.GetEnvironmentVariable("API_KEY");
        
        private static readonly HttpClient http = new HttpClient
        {
            BaseAddress = new Uri("https://api.openai.com/")
        };
        private const string ModelName        = "gpt-4o";
        private const string ChatEndpoint     = "v1/chat/completions";
        
        private static readonly Dictionary<string,Node> _graph = new Dictionary<string,Node>();
        private static readonly Dictionary<string, string> _characterToSpeakerMap = new Dictionary<string, string>();
        
        private static string _contextForPrompt;
        private static string _tweeContent;
        private static readonly string Seperator = "################";
        
        

        static async Task Main(string[] args)
        {
            ParseMetaTweeFile(ReadTweeFile(MetadataFilePath));
            
            _tweeContent = ReadTweeFile(EventListFilePath);
            ParseTweeFile(_tweeContent);
            
            List<Dictionary<Node, Link>> allPaths = GetAllPaths("Anfang");
            
            PrintPathsAndSpeakers(allPaths);
            Console.WriteLine("----------------------------------------");
            Console.WriteLine(ReturnUniquePaths(allPaths));
            
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            
            List<string> prompts = LoadPromptsFromFile(OutputFilePath);
            int index = 1;
            foreach (var prompt in prompts)
            {
                Console.WriteLine(prompt);
                Console.WriteLine($"[{string.Join("; ", ExtractInOrder(prompt))}]");
                Console.WriteLine(prompt);
                await SendPrompt(prompt, index++);
                Console.WriteLine("Prompt gesendet und Antwort angehängt.");
                if (index==11)
                {
                    return;
                }
            }

        }

        /// <summary>
        /// Reads pathOutput.txt line by line, splits at lines beginning with “################” ,
        /// removes intermediate blocks that only contain “PATH...FINISHED” or are empty,
        /// and returns the clean prompt blocks as List&lt;string&gt;.
        /// </summary>
        private static List<string> LoadPromptsFromFile(string filePath)
        {
            var prompts = new List<string>();
            var buffer = new List<string>();

            foreach (var rawLine in File.ReadLines(filePath, Encoding.UTF8))
            {
                // Separation point reached?
                if (rawLine.StartsWith(Seperator, StringComparison.Ordinal))
                {
                    if (buffer.Count > 0)
                    {
                        // Build and save finished prompt
                        prompts.Add(BuildPrompt(buffer));
                        buffer.Clear();
                    }
                    continue;
                }

                // Skip PATH or FINISHED lines and blank lines
                if (rawLine.StartsWith("PATH", StringComparison.OrdinalIgnoreCase) ||
                    rawLine.Contains("FINISHED", StringComparison.OrdinalIgnoreCase) ||
                    string.IsNullOrWhiteSpace(rawLine))
                {
                    continue;
                }

                // Add normal line to current block
                buffer.Add(rawLine);
            }

            // Add last block (if available)
            if (buffer.Count > 0)
                prompts.Add(BuildPrompt(buffer));

            return prompts;
        }

        /// <summary>
        /// Helper method: Combines the collected lines into a prompt and trims them.
        /// </summary>
        private static string BuildPrompt(IReadOnlyList<string> lines)
        {
            return string.Join(Environment.NewLine, lines).Trim();
        }
        
        public static async Task SendPrompt(string prompt, int index)
        {
            string originalPrompt = prompt;
            prompt = GetCompletePrompt(prompt);
            var requestBody = new
            {
                // model = "gpt-4o-mini",
                model = ModelName,
                messages = new[]
                {
                    new { role = "user", content = prompt }
                }
            };
            
            string jsonRequest = JsonSerializer.Serialize(requestBody);
            using var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            // 3) Submit request
            HttpResponseMessage response = await http.PostAsync(ChatEndpoint, content);
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(jsonResponse);

            // 4) Extract response
            string answer = doc
                .RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString() ?? "";
            
            string entry = $"###{index} \n\n[{string.Join("; ", ExtractInOrder(originalPrompt))}\n\n\n\n{answer}\n\n\n\n#$%\n\n\n\n";
            await File.AppendAllTextAsync(ResponseFilePath, entry, Encoding.UTF8);
        }

        private static string GetCompletePrompt(string prompt)
        {
            return
                "Du bist eine Geschlechterforscherin.\nDeine Aufgabe ist es, den folgenden Dialogausschnitt auf Diskriminierung hin zu untersuchen. \n\n"
                + _contextForPrompt + "\n\n"
                +
                "Die Spielerin hat den gesamten Dialog durchgespielt. Ich gebe Dir allerdings nur den Dialogausschnitt, in dem Biases vorkommen, weil dies die relevanten Passagen sind. Du findest dort jeweils die Aussage des Gegenübers, den Namen des Bias, der hier zum Tragen kommt und die Reaktion der Spielerin. Nutze die Information über den Bias als Basis für Deine Interpretation.\nAußerdem gebe ich dir die twee-Datei mit allen möglichen Gesprächspassagen. Dies ist aber nur für Dich zur Kontextualisierung, nicht zur Analyse.\nSchreibe einen Analysetext über den Dialog. Berücksichtige dabei nur die Dialogausschnitte, in denen Biases vorkommen. Stelle die Biases und Verzerrungen dar, auf die du dich beziehst (zur Erläuterung findest du unten eine Liste mit Geschlechterbiases im Gründungsprozess). Drücke Dich allgemeinverständlich aus. Verwende bei der Benennung der Biases ausschließlich den deutschen Namen. Setze keinen einleitenden Text davor, sondern starte direkt mit der Ansprache an die Spielerin.\nAnalysiere auch das Verhalten der Spielerin und ihre Reaktionen auf diese Biases zu den jeweiligen Biases mit konkreten Beispielen aus dem Dialog.\nStelle die Vorteile des Verhaltens der Gründerin dar und deute vorsichtig an, welche Nachteile ihre Reaktion haben könnte.\nFühre das Nicht-Ansprechen geschlechterstereotyper Annahmen nicht bei den Nachteilen auf.\nSei vorsichtig mit dem Hinweis, Biases und Stereotype direkt anzusprechen, weil dies zwar generell sinnvoll sein kann, die Spielerin aber in erster Linie darauf achten muss, dass sie das Gespräch so führt, dass sie im Gespräch erfolgreich ist.\nNutze geschlechtergerechte Sprache (z.B. Gründer*innen, weibliche Gründerinnen).\n Richte den Text unbedingt in der Du-Form an die Spielerin. Sprich niemals von 'Spielerin', 'Gründerin' und einer anderen Bezeichnung als 'DU'. Dies muss unbedingt beachtet werden, da die Antwort ansonst nicht weiter verwendet werden kann. Sei wohlwollend und ermunternd. Sprich die Gründerin nicht mit ihrem Namen an. Formuliere den Text aus einer unbestimmten Ich-Perspektive. \n\n"
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
                "Unbewusste Biases in der Kommunikation\n>>Bias|UnconsciousBiasInCommunication<<\nBias: Herabsetzende Art und Weise, wie über Frauenunternehmen gesprochen wird.\n\n"
                + "Hier der Dialogausschnitt, in denen Biases vorgekommen sind. Analysiere nur diesen Ausschnitt. Spreche in deiner Antwort immer von 'dem Dialog' und NIE! von Dialogausschnitt oder Dialogszene. Dies ist extrem wichtig, da die Antwort sonst nicht verwendet werden kann. \n\n"
                + prompt + "\n\n"
                + "Hier zur Kontextualisierung die twee-Datei mit dem gesamten Dialog. Aber du sollst dich wie oben dargestellt nur auf den oben dargestellten Dialogabschnitt beziehen. \n\n"
                + _tweeContent;

        }
        
        /// <summary>
        /// Extracts in the following order:
        /// 1) Blocks between a tag marker >>Name|Tag<< and an end marker >>--<<
        /// 2) Lines beginning with “Player:” (without this prefix)
        /// </summary>
        public static List<string> ExtractInOrder(string input)
        {
            var results = new List<string>();
            var lines = input.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            // Regex to recognize “Player: ...” (with any leading spaces)
            var spielerinPattern = new Regex(@"^\s*Spielerin:\s*(.+)$", RegexOptions.Compiled);

            bool capturing = false;
            var buffer = new StringBuilder();

            foreach (var line in lines)
            {
                // Is it a marker?
                if (line.StartsWith(">>") && line.EndsWith("<<"))
                {
                    // Content between >> and <<
                    var content = line.Substring(2, line.Length - 4).Trim();

                    if (content == "--")
                    {
                        // End marker: Complete block
                        if (capturing)
                        {
                            var block = buffer.ToString().Trim();
                            if (block.Length > 0)
                                results.Add(block);
                            buffer.Clear();
                            capturing = false;
                        }
                    }
                    else
                    {
                        // Tag marker: 
                        //   If we are currently in a block, close it first
                        if (capturing)
                        {
                            var block = buffer.ToString().Trim();
                            if (block.Length > 0)
                                results.Add(block);
                            buffer.Clear();
                        }
                        // and always start a new block
                        capturing = true;
                    }
                }
                else
                {
                    // No marker line:
                    // 1) Immediately extract player lines
                    var m = spielerinPattern.Match(line);
                    if (m.Success)
                    {
                        results.Add(m.Groups[1].Value.Trim());
                    }
                    // 2) Otherwise, if we are in a block, in the buffer
                    else if (capturing)
                    {
                        buffer.AppendLine(line);
                    }
                }
            }

            // If there is still an open block at the end, send it
            if (capturing)
            {
                var block = buffer.ToString().Trim();
                if (block.Length > 0)
                    results.Add(block);
            }

            return results;
        }
        
        /// <summary>
        /// Reads a Twee file from the specified file and returns its contents as a string.
        /// </summary>
        /// <param name="filePath">Path to the Twee file</param>
        /// <returns>Contents of the Twee file as a string</returns>
        public static string ReadTweeFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Die Datei wurde nicht gefunden: {filePath}");
            }

            try
            {
                // Read file contents and return as string
                return File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Fehler beim Lesen der Datei: {ex.Message}");
            }
        }

        public static void ParseMetaTweeFile(string tweeContent)
        {
            string metaPattern = @"""(talkingPartner\d+)""\s*:\s*""([^""]*)""";

            MatchCollection matches = Regex.Matches(tweeContent, metaPattern);
            
            Console.WriteLine("Matches: " + matches.Count);

            foreach (Match match in matches)
            {
                string key = match.Groups[1].Value.Trim(); // e.g., talkingPartner01
                string value = match.Groups[2].Value.Trim(); // e.g., Notarin

                string numberMatch = Regex.Match(key, @"\d+").Value; // Extract the number
                if (!string.IsNullOrEmpty(value))
                {
                    // Dynamically assign all possible `Character01...`
                    string dynamicPattern = $@"Character{int.Parse(numberMatch)}";

                    if (!_characterToSpeakerMap.ContainsKey(dynamicPattern))
                    {
                        _characterToSpeakerMap[dynamicPattern] = value;
                    }
                }
            }

            // Static assignments
            _characterToSpeakerMap[@"InfoNachrichtWirdAngezeigt"] = "Info";
            _characterToSpeakerMap[@"SpielerinCharakterSpricht"] = "Spielerin";

            // Output of the entire CharacterToSpeakerMap
            Console.WriteLine("CharacterToSpeakerMap-Inhalt:");
            foreach (var entry in _characterToSpeakerMap)
            {
                Console.WriteLine($"  {entry.Key} -> {entry.Value}");
            }
            
            try
            {
                using var doc = JsonDocument.Parse(tweeContent);
                if (doc.RootElement.TryGetProperty("contextForPrompt", out var ctxProp))
                {
                    _contextForPrompt = ctxProp.GetString()!;
                    Console.WriteLine("contextForPrompt: " + _contextForPrompt);
                }
                else
                {
                    Console.WriteLine("Key 'contextForPrompt' nicht gefunden.");
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine("Fehler beim Parsen von JSON: " + ex.Message);
            }
        }

        public static void ParseTweeFile(string tweeContent)
        {
            string nodePattern = @"::\s*([^\n\{\[\|]+).*?\n((?:.|\n)*?)(?=(::|$))";
            string linkPattern = @"\[\[(?:(.*?)(?:\s*(?:\||->)\s*(.*?))|([^|\]]+))\]\]";

            MatchCollection matches = Regex.Matches(tweeContent, nodePattern);

            foreach (Match match in matches)
            {
                string nodeName = match.Groups[1].Value.Trim();
                string nodeBody = match.Groups[2].Value;
                string dialogue = "";

                List<Link> links = new List<Link>();
                Dictionary<string,int> linkCount = new Dictionary<string,int>();
                List<(string Speaker, string Text)> conversations = new List<(string Speaker, string Text)>();

                // Replace speaker
                foreach (KeyValuePair<string,string> speaker in _characterToSpeakerMap)
                {
                    if(speaker.Value != "")
                    {
                        nodeBody = nodeBody.Replace(speaker.Key, speaker.Value);
                    }
                }
               
                var speakerMatch = Regex.Match(nodeBody, @">>(.*?)\|.*?<<\s*(.*?)\s*>>--<<", RegexOptions.Singleline);
                if (speakerMatch.Success)
                {
                    string speaker = speakerMatch.Groups[1].Value.Trim();
                    string message = speakerMatch.Groups[2].Value.Trim();
                    dialogue = $"{speaker}: {message}\n";
                }

                // Extract bias
                var biasMatch = Regex.Match(nodeBody, @">>Bias\|(.*?)<<");
                if (biasMatch.Success)
                {
                    string bias = biasMatch.Groups[1].Value.Trim();
                    dialogue = dialogue + $"Bias: {bias}";
                }
                
                // Extract links
                MatchCollection linkMatches = Regex.Matches(nodeBody, linkPattern);
                foreach (Match linkMatch in linkMatches)
                {
                    Link targetLink = new Link();
                    if (!string.IsNullOrEmpty(linkMatch.Groups[2].Value))
                    {
                        targetLink.targetNode = linkMatch.Groups[2].Value.Trim(); // Text after | or ->
                    }
                    else if (!string.IsNullOrEmpty(linkMatch.Groups[3].Value))
                    {
                        targetLink.targetNode = linkMatch.Groups[3].Value.Trim(); // Standalone text as a link
                    }

                    if (!string.IsNullOrEmpty(targetLink.targetNode))
                    {
                        targetLink.dialogueText = linkMatch.Groups[1].Value.Trim();
                        links.Add(targetLink);
                        if(linkCount.ContainsKey(targetLink.targetNode))
                        {
                            linkCount[targetLink.targetNode]++;
                        }
                        else
                        {
                            linkCount[targetLink.targetNode]=1;
                        }
                    }
                }

                nodeBody = Regex.Replace(nodeBody, @"\[\[.*?->.*?\]\]\s*", "", RegexOptions.Singleline);
                // Save node in graph
                if (!_graph.ContainsKey(nodeName))
                {
                    Node newNode = new Node(nodeName, nodeBody, links);
                    _graph[nodeName] = newNode;
                    _graph[nodeName].linkCount = linkCount;
                    newNode.dialogue = dialogue;
                }
                else
                {
                    _graph[nodeName].links.AddRange(links);
                }
            }
            
            // Ensure that “End” exists in the graph
            if (!_graph.ContainsKey("Ende"))
            {
                _graph["Ende"] = new Node("Ende", "",new List<Link>());
            }
        }

        //Returns all paths with responses to the nodes
        public static List<Dictionary<Node, Link>> GetAllPaths(string startNode)
        {
            Console.WriteLine("GetAllPaths gestartet");

            List<Dictionary<Node, Link>> allPaths = new List<Dictionary<Node, Link>>();
            Dictionary<Node, Link> currentPath = new Dictionary<Node, Link>();

            void DFS(Node node)
            {
                if (!_graph.ContainsKey(node.name))
                {
                    Console.WriteLine($"Knoten {node} existiert nicht im Graphen!");
                    return;
                }

                // Add the current node to the path
                List<Link> links = _graph[node.name].links;

                // When end nodes are reached
                if (links.Count == 0 || node.name == "Ende")
                {
                    allPaths.Add(new Dictionary<Node, Link>(currentPath));
                }
                else
                {
                    foreach (Link neighbor in links)
                    {
                        currentPath[node] = neighbor;
                        DFS(_graph[neighbor.targetNode]);
                    }
                }

                currentPath.Remove(node);
            }

            DFS(_graph[startNode]);
            return allPaths;
        }

        // Reduces paths to bias nodes only and returns unique paths
        public static List<Dictionary<Node,Link>> ReturnUniquePaths(List<Dictionary<Node, Link>> paths)
        {
            System.IO.File.WriteAllText(OutputFilePath, string.Empty);
            int pathCount = 1;
            List<Dictionary<Node, Link>> newPaths = new List<Dictionary<Node, Link>>();
            // Iterates over list of dictionaries
            foreach(Dictionary<Node, Link> path in paths)
            {
                bool duplicate = false;
                Dictionary<Node, Link> newPath = new Dictionary<Node, Link>();
                // Iterates over key-value pairs within the dictionaries.
                // If a bias exists, the pair is added to the path list.
                foreach(KeyValuePair<Node,Link> decision in path)
                {
                    string nodeBody = decision.Key.body;
                    if(nodeBody.Contains(">>Bias|"))
                    {
                        newPath.Add(decision.Key,decision.Value);
                    }
                }
                Console.WriteLine();
                // Iterates over the new paths
                foreach(Dictionary<Node, Link> listElement in newPaths)
                {
                    if(PathEqual(listElement, newPath)) //Checks if the path was documented before
                    {
                        duplicate = true;
                    }
                }
                // If no duplicate -> Unique
                // Write to list and file
                // Count number of paths up
                if(!duplicate) 
                {
                    newPaths.Add(newPath);
                    WritePath(newPath);
                    WriteInFile("################", OutputFilePath);
                    WriteInFile("PATH " + pathCount + " FINISHED" + "\r\n", OutputFilePath);
                    pathCount++;
                }
            }
            Console.WriteLine("Unique Paths created");
            Console.WriteLine($"Gefundene Pfade: {newPaths.Count}");
            
            // Iterates over all new paths
            foreach (Dictionary<Node,Link> path in newPaths)
            {
                Console.WriteLine("Path:");
                foreach(KeyValuePair<Node,Link> decision in path)
                {
                    Console.WriteLine("Node: " + decision.Key.name + "; Link: " + decision.Value.targetNode);
                }
            }
            return newPaths;
        }

        // Returns true if two paths are equal
        public static bool PathEqual(Dictionary<Node, Link> firstPath, Dictionary<Node, Link> secondPath)
        {
            if(firstPath.Count != secondPath.Count)
            {
                return false;
            }
            foreach(KeyValuePair<Node,Link> decision in firstPath)
            {
                if(secondPath.TryGetValue(decision.Key,out Link value))
                {
                    if(!value.Equals(decision.Value))
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        // Writes a path into a specified file
        public static void WritePath(Dictionary<Node,Link> path)
        {
            foreach(KeyValuePair<Node,Link> node in path)
            {
                WriteInFile(node.Key.body.Trim() + "\r\n", OutputFilePath);
                if (node.Value.dialogueText != string.Empty)
                {
                    WriteInFile("Spielerin: " + node.Value.dialogueText + "\r\n", OutputFilePath);
                }
            }
        }

        // Simple method for file output
        public static void WriteInFile(string output, string path)
        {
            if (!File.Exists(path))
            {
                var sr = File.CreateText(path);
                sr.WriteLine(output);
                sr.Close();
            }
            else
            {
                var sr = new StreamWriter(path, append:true);
                sr.WriteLine(output);
                sr.Close();
            }
        }

        public static void PrintPathsAndSpeakers(List<Dictionary<Node,Link>> paths)
        {
            Console.WriteLine($"Gefundene Pfade: {paths.Count}");
            Console.WriteLine();

            // Loop through each path dictionary instance
            foreach (var pathDict in paths)
            {
                Console.WriteLine("Einzelner Pfad:");

                // 1) Print each node -> link pair
                foreach (var kv in pathDict)
                {
                    Console.WriteLine($"  Node: {kv.Key.name}; Link: {kv.Value.targetNode}");
                }

                // 2) Only arrange the node names in an “A -> B -> C” sequence
                var nodeNames = pathDict.Keys.Select(n => n.name);
                Console.WriteLine("  Zusammenfassung: " + 
                                  string.Join(" -> ", nodeNames));

                // 3) Optional: If you want to show the dialogue content
                foreach (var kv in pathDict)
                {
                    if (!string.IsNullOrWhiteSpace(kv.Key.dialogue))
                        Console.WriteLine($"  {kv.Key.name} sagt: {kv.Key.dialogue.Trim()}");
                }

                Console.WriteLine(); // Blank line for separation
            }
        }
    }
    
    public class Node
    {
        public string name;
        public string body;
        public string dialogue;
        public List<Link> links = new List<Link>();
        public Dictionary<string,int> linkCount = new Dictionary<string,int>();

        public Node(string _name, string _nodeBody, List<Link> _links)
        {
            name = _name;
            body = _nodeBody;
            links = _links;
        }
        public override bool Equals(object obj) // Needed for path comparison
        {
            if (obj is not Node other) return false;
            return name == other.name;
        }

        public override int GetHashCode() // Needed for path comparison
        {
            return HashCode.Combine(name); // Use .NET's built-in hash combiner
        }
    }
    public class Link
    {
        public string targetNode;
        public string dialogueText;
        public override bool Equals(object obj) // Needed for path comparison
        {
            if (obj is not Link other) return false;
            return targetNode == other.targetNode && dialogueText == other.dialogueText;
        }

        public override int GetHashCode() // Needed for path comparison
        {
            return HashCode.Combine(targetNode, dialogueText); // Use .NET's built-in hash combiner
        }
    }
