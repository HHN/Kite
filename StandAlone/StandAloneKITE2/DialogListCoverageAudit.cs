using System.Text;
using System.Text.RegularExpressions;

namespace StandAloneKITE2
{
    /// <summary>
    /// Checks: For all dialogs in pathOutput.txt, the list form “[...]” is generated
    /// and compared with the lists already present in response.txt.
    /// Missing ones are output at the end.
    /// </summary>
    public static class DialogListCoverageAudit
    {
        // Paths as specified by you:
        private const string PathOutputFile = @"..\..\..\Kreditantrag\pathOutput.txt";
        private const string ResponseFile   = @"..\..\..\Kreditantrag\response.txt";

        public static void Run()
        {
            if (!File.Exists(PathOutputFile))
            {
                Console.WriteLine($"[Audit] Pfad-Datei nicht gefunden: {Path.GetFullPath(PathOutputFile)}");
                return;
            }
            if (!File.Exists(ResponseFile))
            {
                Console.WriteLine($"[Audit] Response-Datei nicht gefunden: {Path.GetFullPath(ResponseFile)}");
                return;
            }

            // 1) Load all path blocks from pathOutput.txt
            var pathBlocks = LoadPromptBlocks(PathOutputFile);
            Console.WriteLine($"[Audit] Geladene Pfad-Blöcke: {pathBlocks.Count}");

            // 2) Build the list representation for each block (as in response.txt)
            var allPathSigs = new Dictionary<string, string>(StringComparer.Ordinal); // sig -> “[...]” (raw)
            int built = 0;
            foreach (var block in pathBlocks)
            {
                var items = ExtractInOrder(block);
                var sig   = BuildListSignature(items);
                if (!string.IsNullOrWhiteSpace(sig) && !allPathSigs.ContainsKey(sig))
                {
                    var bracket = "[" + string.Join("; ", items) + "]";
                    allPathSigs[sig] = bracket;
                    built++;
                }
            }
            Console.WriteLine($"[Audit] Erzeugte eindeutige Listen (Signaturen): {built}");

            // 3) Read all existing list signatures from response.txt
            var knownSigs = LoadExistingListSignaturesFromResponse(ResponseFile);
            Console.WriteLine($"[Audit] Bekannte Listen aus response.txt (Signaturen): {knownSigs.Count}");

            // 4) Determine what is missing
            var missing = allPathSigs.Keys
                                     .Where(sig => !knownSigs.Contains(sig))
                                     .Select(sig => allPathSigs[sig])
                                     .ToList();

            Console.WriteLine();
            Console.WriteLine("========== FEHLENDE LISTEN ==========");
            if (missing.Count == 0)
            {
                Console.WriteLine("(keine)");
            }
            else
            {
                foreach (var m in missing)
                    Console.WriteLine(m);
            }
            Console.WriteLine("=====================================");
            Console.WriteLine($"[Audit] Zusammenfassung: total={allPathSigs.Count}, vorhanden={allPathSigs.Count - missing.Count}, fehlend={missing.Count}");
        }

        // --------- - Parser/Utils (compact, stable) ----------

        /// <summary>
        /// Reads pathOutput.txt in blocks separated by lines consisting only of ‘#’.
        /// PATH/FINISHED lines are removed.
        /// </summary>
        private static List<string> LoadPromptBlocks(string filePath)
        {
            var result = new List<string>();
            var sb = new StringBuilder();
            var sepRegex = new Regex(@"^\s*#+\s*$"); // "################"

            void Flush()
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

            foreach (var raw in File.ReadLines(filePath, Encoding.UTF8))
            {
                if (sepRegex.IsMatch(raw))
                    Flush();
                else
                    sb.AppendLine(raw);
            }
            Flush();

            return result;
        }

        /// <summary>
        /// Extracts in order: blocks between >>Tag|...<< and >>--<<
        /// as well as lines “Spielerin: ...”.
        /// </summary>
        private static List<string> ExtractInOrder(string input)
        {
            var results = new List<string>();
            var lines = input.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            var spielerinPattern = new Regex(@"^\s*Spielerin:\s*(.+)$", RegexOptions.Compiled);

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
                            if (block.Length > 0) results.Add(NormalizeItem(block));
                            buf.Clear();
                            capturing = false;
                        }
                    }
                    else
                    {
                        if (capturing)
                        {
                            var block = buf.ToString().Trim();
                            if (block.Length > 0) results.Add(NormalizeItem(block));
                            buf.Clear();
                        }
                        capturing = true;
                    }
                }
                else
                {
                    var m = spielerinPattern.Match(line);
                    if (m.Success)
                    {
                        results.Add(NormalizeItem(m.Groups[1].Value.Trim()));
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
                if (block.Length > 0) results.Add(NormalizeItem(block));
            }

            return results;
        }

        /// <summary>
        /// Build a stable signature from pathItems (trim + normalize whitespace).
        /// </summary>
        private static string BuildListSignature(IEnumerable<string> items)
        {
            if (items == null) return null;
            var norm = items.Select(NormalizeItem)
                            .Where(s => s.Length > 0)
                            .ToList();
            if (norm.Count == 0) return null;
            return string.Join(" ; ", norm); // Semicolon + space, as in the generator
        }

        private static string NormalizeItem(string s)
        {
            if (s == null) return "";
            s = s.Trim();
            s = Regex.Replace(s, @"\s+", " "); // Collapse whitespace
            return s;
        }

        /// <summary>
        /// Parses response.txt and returns all existing list signatures (lines containing [ ... ]).
        /// </summary>
        private static HashSet<string> LoadExistingListSignaturesFromResponse(string responsePath)
        {
            var set = new HashSet<string>(StringComparer.Ordinal);

            var content = File.ReadAllText(responsePath, Encoding.UTF8);
            // Blocks start with “### <number>” and end before the next “###” or #$%.
            var startRegex = new Regex(@"(?m)^###\s*\d+\s*$");
            const string endToken = "#$%";

            var starts = startRegex.Matches(content).Cast<Match>().ToList();
            for (int i = 0; i < starts.Count; i++)
            {
                int blockStart = starts[i].Index + starts[i].Length;
                int nextStartPos = (i + 1 < starts.Count) ? starts[i + 1].Index : content.Length;

                int endPos = content.IndexOf(endToken, blockStart, nextStartPos - blockStart, StringComparison.Ordinal);
                int blockEnd = (endPos >= 0) ? endPos : nextStartPos;

                string block = content.Substring(blockStart, blockEnd - blockStart);

                // First line starting with ‘[’ and ending with ']'
                string listLine = ExtractFirstBracketLine(block);
                if (listLine == null) continue;

                var inner = listLine.Trim();
                if (inner.StartsWith("[")) inner = inner.Substring(1);
                if (inner.EndsWith("]")) inner = inner.Substring(0, inner.Length - 1);

                var items = inner.Split(';')
                                 .Select(NormalizeItem)
                                 .Where(x => x.Length > 0);

                var sig = string.Join(" ; ", items);
                if (!string.IsNullOrWhiteSpace(sig))
                    set.Add(sig);
            }

            return set;
        }

        private static string ExtractFirstBracketLine(string block)
        {
            foreach (var line in block.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None))
            {
                var t = line.Trim();
                if (t.StartsWith("[") && t.EndsWith("]"))
                    return t;
            }
            return null;
        }
    }
}
