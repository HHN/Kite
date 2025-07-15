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

    static async Task Main(string[] args)
    {
        // 1) Meta- und Twee-Dateien einlesen und parsen
    ParseMetaTweeFile(ReadTweeFile(MetadataFilePath));
    _tweeContent = ReadTweeFile(EventListFilePath);
    ParseTweeFile(_tweeContent);

    // 2) Streaming aller Pfade + Unique-Filter auf Bias-Knoten
    Console.WriteLine("[GetAllPaths] Streaming aller Pfade startet...");
    var seen = new HashSet<string>();
    long processed = 0, uniqueCount = 0;

    // Ausgabedatei für die Pfade zurücksetzen
    File.WriteAllText(OutputFilePath, string.Empty);

    await Task.Run(() =>
    {
        foreach (var path in StreamAllPaths("Anfang", count =>
        {
            if (count % 1_000_000 == 0)
                Console.WriteLine($"[GetAllPaths] {count:N0} Pfade bisher…");
        }))
        {
            processed++;

            // nur die Bias-Entscheidungen extrahieren
            var filtered = path
                .Where(kv => kv.Key.body.Contains(">>Bias|"))
                .ToDictionary(kv => kv.Key, kv => kv.Value);

            // Signatur für Duplikat-Check
            var sig = string.Join("→",
                filtered.Select(kv =>
                    $"{kv.Key.name}|{kv.Value.targetNode}|{kv.Value.dialogueText.Replace("→","↦")}"
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

    // 3) Prompts einlesen und Antworten senden
    http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
    // var prompts = LoadPromptsFromFile(OutputFilePath);
    // int idx = 1;
    // foreach (var prompt in prompts)
    // {
    //     Console.WriteLine($"Verarbeite Prompt #{idx}…");
    //     await SendPrompt(prompt, idx);
    //     Console.WriteLine($"Prompt #{idx} gesendet und Antwort gespeichert.");
    //     idx++;
    // }
    }

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
        var prompts = new List<string>();
        var buffer = new List<string>();

        foreach (var rawLine in File.ReadLines(filePath, Encoding.UTF8))
        {
            if (rawLine.StartsWith(Seperator, StringComparison.Ordinal))
            {
                if (buffer.Count > 0)
                {
                    prompts.Add(BuildPrompt(buffer));
                    buffer.Clear();
                }
                continue;
            }
            if (rawLine.StartsWith("PATH", StringComparison.OrdinalIgnoreCase) ||
                rawLine.Contains("FINISHED", StringComparison.OrdinalIgnoreCase) ||
                string.IsNullOrWhiteSpace(rawLine))
            {
                continue;
            }
            buffer.Add(rawLine);
        }
        if (buffer.Count > 0)
            prompts.Add(BuildPrompt(buffer));
        return prompts;
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
            $"###{index}\n\n[{string.Join("; ", ExtractInOrder(original))}\n\n\n\n{answer}\n\n\n\n#$%\n\n\n\n";
        await File.AppendAllTextAsync(ResponseFilePath, entry, Encoding.UTF8);
    }

    private static string GetCompletePrompt(string prompt)
    {
        return
            "Du bist eine Geschlechterforscherin.\n…\n"
            + prompt + "\n\n"
            + _tweeContent;
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
            File.WriteAllText(path, output + Environment.NewLine);
        else
            File.AppendAllText(path, output + Environment.NewLine);
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
