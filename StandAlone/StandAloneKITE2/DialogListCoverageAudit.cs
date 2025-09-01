using System.Text;
using System.Text.RegularExpressions;

namespace StandAloneKITE2
{
    /// <summary>
    /// Prüft: Für alle Dialoge in pathOutput.txt wird die Listenform "[...]" erzeugt
    /// und mit den bereits in response.txt vorhandenen Listen verglichen.
    /// Fehlende werden am Ende ausgegeben.
    /// </summary>
    public static class DialogListCoverageAudit
    {
        // Pfade wie von dir angegeben:
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

            // 1) Alle Pfad-Blöcke aus pathOutput.txt laden
            var pathBlocks = LoadPromptBlocks(PathOutputFile);
            Console.WriteLine($"[Audit] Geladene Pfad-Blöcke: {pathBlocks.Count}");

            // 2) Für jeden Block die Listen-Darstellung bauen (wie in response.txt)
            var allPathSigs = new Dictionary<string, string>(StringComparer.Ordinal); // sig -> "[...]" (roh)
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

            // 3) Alle vorhandenen Listen-Signaturen aus response.txt lesen
            var knownSigs = LoadExistingListSignaturesFromResponse(ResponseFile);
            Console.WriteLine($"[Audit] Bekannte Listen aus response.txt (Signaturen): {knownSigs.Count}");

            // 4) Fehlende ermitteln
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

        // ---------- Parser/Utils (kompakt, stabil) ----------

        /// <summary>
        /// Liest pathOutput.txt in Blöcken, getrennt durch Zeilen, die nur aus '#' bestehen.
        /// PATH/FINISHED-Zeilen werden entfernt.
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
        /// Extrahiert in Reihenfolge: Blöcke zwischen >>Tag|...<< und >>--<<
        /// sowie Zeilen "Spielerin: ...".
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

        /// <summary>Aus pathItems eine stabile Signatur bauen (Trim + Whitespace normalisieren).</summary>
        private static string BuildListSignature(IEnumerable<string> items)
        {
            if (items == null) return null;
            var norm = items.Select(NormalizeItem)
                            .Where(s => s.Length > 0)
                            .ToList();
            if (norm.Count == 0) return null;
            return string.Join(" ; ", norm); // Semikolon + Space wie im Generator
        }

        private static string NormalizeItem(string s)
        {
            if (s == null) return "";
            s = s.Trim();
            s = Regex.Replace(s, @"\s+", " "); // Whitespace zusammenziehen
            return s;
        }

        /// <summary>
        /// Parsed response.txt und liefert alle vorhandenen Listen-Signaturen (Zeile mit [ ... ]).
        /// </summary>
        private static HashSet<string> LoadExistingListSignaturesFromResponse(string responsePath)
        {
            var set = new HashSet<string>(StringComparer.Ordinal);

            var content = File.ReadAllText(responsePath, Encoding.UTF8);
            // Blöcke starten mit "### <zahl>" und enden vor dem nächsten "###" oder #$%
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

                // erste Zeile, die mit '[' beginnt und mit ']' endet
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
