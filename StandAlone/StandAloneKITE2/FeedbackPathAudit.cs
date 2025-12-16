using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FeedbackPathAudit
{
    internal static class FeedbackPathAudit
    {
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // Customize HERE
        private const string RESPONSE_FILE   = @"..\..\..\Vermieter\response.txt";
        private const string PATHOUTPUT_FILE = @"..\..\..\Vermieter\pathOutput.txt";

        // Flags: Which files to check? (both can be true)
        private const bool ANALYZE_RESPONSE   = true;
        private const bool ANALYZE_PATHOUTPUT = true;

        // Remove duplicates immediately? (keep only the first occurrence)
        private const bool REMOVE_DUPLICATES = false;

        // Optional: List duplicates in detail?
        private const bool LIST_DUPLICATES = true;

        // Pay attention to upper/lower case in signatures?
        private static readonly StringComparer SigComparer = StringComparer.Ordinal;
        // <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        private const string RESPONSE_END_TOKEN   = "#$%";
        private const string PATHOUTPUT_SEPARATOR = "################";

        // Uniform representation of an entry
        private sealed class Entry
        {
            public int Index { get; }
            public string Block { get; } // Plain text content (without markers)
            public Entry(int index, string block) { Index = index; Block = block; }
        }

        public static void Run()
        {
            if (ANALYZE_RESPONSE)
            {
                Console.WriteLine("===== Analyse: response.txt =====");
                AnalyzeFile(RESPONSE_FILE, isResponse: true);
            }

            if (ANALYZE_PATHOUTPUT)
            {
                Console.WriteLine("===== Analyse: pathOutput.txt =====");
                AnalyzeFile(PATHOUTPUT_FILE, isResponse: false);
            }
        }

        private static void AnalyzeFile(string filePath, bool isResponse)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Datei nicht gefunden:\n{filePath}");
                return;
            }

            string content = File.ReadAllText(filePath, Encoding.UTF8);
            var entries = isResponse
                ? ExtractEntriesFromResponse(content)
                : ExtractEntriesFromPathOutput(content);

            // --- Count duplicates (with indexes for detailed listing) ---
            var sigToIndices = new Dictionary<string, List<int>>(SigComparer);
            int nullSigCount = 0;

            foreach (var e in entries)
            {
                var sig = BuildSignature(e.Block, isResponse);
                if (string.IsNullOrWhiteSpace(sig))
                {
                    nullSigCount++;
                    continue;
                }
                if (!sigToIndices.TryGetValue(sig, out var list))
                    sigToIndices[sig] = list = new List<int>();
                list.Add(e.Index); // For pathOutput: PATH index, for response: ### index
            }

            int total      = entries.Count;
            int unique     = sigToIndices.Count + nullSigCount; // Entries without signature separately
            int duplicates = total - unique;

            Console.WriteLine($"Datei:                        {Path.GetFullPath(filePath)}");
            Console.WriteLine($"Gesamtanzahl Einträge:        {total:N0}");
            Console.WriteLine($"Unterschiedliche Signaturen:  {unique:N0} (davon ohne Signatur: {nullSigCount:N0})");
            Console.WriteLine($"Anzahl Duplikate:             {duplicates:N0}");

            if (LIST_DUPLICATES)
            {
                Console.WriteLine();
                Console.WriteLine("Duplizierte Signaturen (Signatur → Indizes):");
                var dups = sigToIndices
                    .Where(kv => kv.Value.Count > 1)
                    .OrderByDescending(kv => kv.Value.Count)
                    .ToList();

                if (dups.Count == 0)
                {
                    Console.WriteLine("  Keine Duplikate gefunden.");
                }
                else
                {
                    foreach (var kv in dups)
                    {
                        Console.WriteLine($"  {kv.Key}");
                        Console.WriteLine($"    Einträge: #{string.Join(", #", kv.Value.OrderBy(x => x))}");
                    }
                }
            }

            if (!REMOVE_DUPLICATES)
                return;

            // --- Remove duplicates (keep only the first occurrence) ---
            var seen      = new HashSet<string>(SigComparer);
            var remaining = new List<Entry>(entries.Count);

            foreach (var e in entries)
            {
                var sig = BuildSignature(e.Block, isResponse);

                // Never deduplicate entries without signatures -> always keep them
                if (string.IsNullOrWhiteSpace(sig) || seen.Add(sig))
                    remaining.Add(e);
            }

            string rebuilt = isResponse
                ? RebuildResponseFile(remaining)
                : RebuildPathOutputFile(remaining);

            // Backup with timestamp (never overwrite)
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HHmm-ss");
            string backupPath = Path.Combine(
                Path.GetDirectoryName(filePath)!,
                Path.GetFileNameWithoutExtension(filePath) + "_" + timestamp + ".bak"
            );
            File.Copy(filePath, backupPath, overwrite: false);

            File.WriteAllText(filePath, rebuilt, Encoding.UTF8);

            Console.WriteLine();
            Console.WriteLine("----- Bereinigung durchgeführt -----");
            Console.WriteLine($"Backup gespeichert:            {backupPath}");
            Console.WriteLine($"Verbliebene Einträge:          {remaining.Count:N0}");
            Console.WriteLine();
        }

        // ==============================================================
        // ============= Parsing for response.txt ========================
        // ==============================================================
        private static List<Entry> ExtractEntriesFromResponse(string content)
        {
            var result = new List<Entry>();
            var startRegex = new Regex(@"(?m)^###\s*(\d+)\s*$");
            var starts = startRegex.Matches(content).Cast<Match>().ToList();

            for (int i = 0; i < starts.Count; i++)
            {
                int idx = int.Parse(starts[i].Groups[1].Value);
                int blockStart   = starts[i].Index + starts[i].Length;
                int nextStartPos = (i + 1 < starts.Count) ? starts[i + 1].Index : content.Length;

                int endPos   = content.IndexOf(RESPONSE_END_TOKEN, blockStart, nextStartPos - blockStart, StringComparison.Ordinal);
                int blockEnd = (endPos >= 0) ? endPos : nextStartPos;

                string block = content.Substring(blockStart, blockEnd - blockStart).Trim();
                result.Add(new Entry(idx, block));
            }
            return result;
        }

        // ==============================================================
        // ============= Parsing for pathOutput.txt ======================
        // ==============================================================
        private static List<Entry> ExtractEntriesFromPathOutput(string content)
        {
            /*
             * Goal: Pair each block content with the following “PATH X FINISHED”.
             * Format:
             *   <BLOCK>
             *   ################
             *   PATH X FINISHED
             *   [Blank lines]
             *   <BLOCK>
             *   ################
             *   PATH Y FINISHED
             *   ...
             *
             * Procedure: Read line by line until separator. Then expect “PATH (\d+) FINISHED”.
             */
            var entries = new List<Entry>();
            var lines = new List<string>();
            using var reader = new StringReader(content);

            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (!line.Trim().Equals(PATHOUTPUT_SEPARATOR, StringComparison.Ordinal))
                {
                    // Collect lines for the current block
                    lines.Add(line);
                    continue;
                }

                // Separator reached: Now read the PATH line(s)
                var blockText = string.Join(Environment.NewLine, lines).Trim();
                lines.Clear();

                // Expected PATH line
                string? pathLine;
                do
                {
                    pathLine = reader.ReadLine();
                    if (pathLine == null) break; // End of file
                    pathLine = pathLine.Trim();
                } while (pathLine.Length == 0); // Skip empty lines

                if (pathLine == null)
                {
                    // No more PATH marker -> ignore (fragment at the end)
                    continue;
                }

                var m = Regex.Match(pathLine, @"^PATH\s+(\d+)\s+FINISHED$", RegexOptions.IgnoreCase);
                if (!m.Success)
                {
                    // Unexpected line – defensive: ignore this “block”
                    continue;
                }

                int pathIndex = int.Parse(m.Groups[1].Value);

                // Clean the block text of any old PATH markers (if there are any in the block)
                var cleaned = blockText
                    .Split(new[] { "\r\n", "\n" }, StringSplitOptions.None)
                    .Where(l => !l.TrimStart().StartsWith("PATH ", StringComparison.OrdinalIgnoreCase)
                             && !string.IsNullOrWhiteSpace(l))
                    .ToList();

                if (cleaned.Count == 0)
                {
                    // pure marker block -> ignore
                    continue;
                }

                var normalizedBlock = string.Join(Environment.NewLine, cleaned).Trim();
                entries.Add(new Entry(pathIndex, normalizedBlock));

                // Optionally skip blank lines after PATH (automatically covered by the while loop)
            }

            // If there are still lines left after the last PATH (fragment without marker) -> ignore

            // Sort by PATH index to have a consistent listing
            entries.Sort((a, b) => a.Index.CompareTo(b.Index));
            return entries;
        }

        // ==============================================================
        // ============= Signatures =====================================
        // ==============================================================
        private static string BuildSignature(string block, bool isResponse)
        {
            if (string.IsNullOrWhiteSpace(block))
                return null;

            if (isResponse)
            {
                // Find the first [ ... ] line
                var listLine = block.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None)
                                    .Select(l => l.Trim())
                                    .FirstOrDefault(l => l.StartsWith("[") && l.EndsWith("]"));
                if (listLine == null) return null;

                var inner = listLine[1..^1]; // Without [ ]
                var items = inner.Split(';')
                                 .Select(NormalizeItem)
                                 .Where(s => s.Length > 0);
                return string.Join(" ; ", items);
            }
            else
            {
                // pathOutput: all content lines (markers have already been removed)
                var lines = block.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None)
                                 .Where(l => !string.IsNullOrWhiteSpace(l))
                                 .Select(NormalizeItem);
                return string.Join(" | ", lines);
            }
        }

        private static string NormalizeItem(string s)
        {
            if (s == null) return "";
            s = s.Trim();
            s = Regex.Replace(s, @"\s+", " "); // Normalize whitespace
            s = s.Replace("→", "↦");           // Collision protection
            return s;
        }

        // ==============================================================
        // ============= Rebuild ========================================
        // ==============================================================
        private static string RebuildResponseFile(List<Entry> blocks)
        {
            var sb = new StringBuilder();
            // Sort by index (if necessary)
            var ordered = blocks.OrderBy(b => b.Index).ToList();

            for (int i = 0; i < ordered.Count; i++)
            {
                sb.AppendLine($"###{i + 1}");
                sb.AppendLine();
                sb.Append(ordered[i].Block.TrimEnd());
                sb.AppendLine();
                sb.AppendLine();
                sb.AppendLine(RESPONSE_END_TOKEN);
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private static string RebuildPathOutputFile(List<Entry> blocks)
        {
            var sb = new StringBuilder();
            // Sort by index (if necessary)
            var ordered = blocks.OrderBy(b => b.Index).ToList();

            for (int i = 0; i < ordered.Count; i++)
            {
                // Write content
                sb.AppendLine(ordered[i].Block.Trim());
                // Separator + newly numbered marker (clean 1..N)
                sb.AppendLine(PATHOUTPUT_SEPARATOR);
                sb.AppendLine($"PATH {i + 1} FINISHED");
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
