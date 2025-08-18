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
        // HIER anpassen
        private const string RESPONSE_FILE   = @"..\..\..\Vermieter\response.txt";
        private const string PATHOUTPUT_FILE = @"..\..\..\Vermieter\pathOutput.txt";

        // Flags: Welche Files prüfen? (können beide true sein)
        private const bool ANALYZE_RESPONSE   = true;
        private const bool ANALYZE_PATHOUTPUT = true;

        // Duplikate direkt entfernen? (nur erste Vorkommnis behalten)
        private const bool REMOVE_DUPLICATES = false;

        // Optional: Duplikate detailliert auflisten?
        private const bool LIST_DUPLICATES = true;

        // Groß-/Kleinschreibung bei Signaturen beachten?
        private static readonly StringComparer SigComparer = StringComparer.Ordinal;
        // <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        private const string RESPONSE_END_TOKEN   = "#$%";
        private const string PATHOUTPUT_SEPARATOR = "################";

        // Einheitliche Repräsentation eines Eintrags
        private sealed class Entry
        {
            public int Index { get; }
            public string Block { get; } // reiner Textinhalt (ohne Marker)
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

            // --- Duplikate zählen (mit Indizes für Detail-Listing) ---
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
                list.Add(e.Index); // bei pathOutput: PATH-Index, bei response: ###-Index
            }

            int total      = entries.Count;
            int unique     = sigToIndices.Count + nullSigCount; // Einträge ohne Signatur separat
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

            // --- Duplikate entfernen (nur erste Vorkommnis behalten) ---
            var seen      = new HashSet<string>(SigComparer);
            var remaining = new List<Entry>(entries.Count);

            foreach (var e in entries)
            {
                var sig = BuildSignature(e.Block, isResponse);

                // Einträge ohne Signatur niemals deduplizieren -> immer behalten
                if (string.IsNullOrWhiteSpace(sig) || seen.Add(sig))
                    remaining.Add(e);
            }

            string rebuilt = isResponse
                ? RebuildResponseFile(remaining)
                : RebuildPathOutputFile(remaining);

            // Backup mit Zeitstempel (nie überschreiben)
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
        // ============= Parsing für response.txt ========================
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
        // ============= Parsing für pathOutput.txt ======================
        // ==============================================================
        private static List<Entry> ExtractEntriesFromPathOutput(string content)
        {
            /*
             * Ziel: jeden Blockinhalt mit dem nachfolgenden "PATH X FINISHED" paaren.
             * Format:
             *   <BLOCK>
             *   ################
             *   PATH X FINISHED
             *   [Leerzeilen]
             *   <BLOCK>
             *   ################
             *   PATH Y FINISHED
             *   ...
             *
             * Vorgehen: Zeilenweise lesen, bis Separator. Danach "PATH (\d+) FINISHED" erwarten.
             */
            var entries = new List<Entry>();
            var lines = new List<string>();
            using var reader = new StringReader(content);

            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (!line.Trim().Equals(PATHOUTPUT_SEPARATOR, StringComparison.Ordinal))
                {
                    // Zeilen für den aktuellen Block sammeln
                    lines.Add(line);
                    continue;
                }

                // Separator erreicht: Jetzt die PATH-Zeile(n) lesen
                var blockText = string.Join(Environment.NewLine, lines).Trim();
                lines.Clear();

                // Erwartete PATH-Zeile
                string? pathLine;
                do
                {
                    pathLine = reader.ReadLine();
                    if (pathLine == null) break; // Datei-Ende
                    pathLine = pathLine.Trim();
                } while (pathLine.Length == 0); // leere Zeilen überspringen

                if (pathLine == null)
                {
                    // Kein PATH-Marker mehr -> ignorieren (Fragment am Ende)
                    continue;
                }

                var m = Regex.Match(pathLine, @"^PATH\s+(\d+)\s+FINISHED$", RegexOptions.IgnoreCase);
                if (!m.Success)
                {
                    // Unerwartete Zeile – defensiv: diesen „Block“ ignorieren
                    continue;
                }

                int pathIndex = int.Parse(m.Groups[1].Value);

                // Den Blocktext von evtl. alten PATH-Markern säubern (falls welche im Block stehen sollten)
                var cleaned = blockText
                    .Split(new[] { "\r\n", "\n" }, StringSplitOptions.None)
                    .Where(l => !l.TrimStart().StartsWith("PATH ", StringComparison.OrdinalIgnoreCase)
                             && !string.IsNullOrWhiteSpace(l))
                    .ToList();

                if (cleaned.Count == 0)
                {
                    // reiner Marker-Block -> ignorieren
                    continue;
                }

                var normalizedBlock = string.Join(Environment.NewLine, cleaned).Trim();
                entries.Add(new Entry(pathIndex, normalizedBlock));

                // Optionale Leerzeilen nach PATH überspringen (werden durch die while-Schleife automatisch abgedeckt)
            }

            // Falls nach letztem PATH noch Zeilen übrig blieben (Fragment ohne Marker) -> ignorieren

            // Sortieren nach PATH-Index, um ein konsistentes Listing zu haben
            entries.Sort((a, b) => a.Index.CompareTo(b.Index));
            return entries;
        }

        // ==============================================================
        // ============= Signaturen =====================================
        // ==============================================================
        private static string BuildSignature(string block, bool isResponse)
        {
            if (string.IsNullOrWhiteSpace(block))
                return null;

            if (isResponse)
            {
                // erste [ ... ]-Zeile suchen
                var listLine = block.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None)
                                    .Select(l => l.Trim())
                                    .FirstOrDefault(l => l.StartsWith("[") && l.EndsWith("]"));
                if (listLine == null) return null;

                var inner = listLine[1..^1]; // ohne [ ]
                var items = inner.Split(';')
                                 .Select(NormalizeItem)
                                 .Where(s => s.Length > 0);
                return string.Join(" ; ", items);
            }
            else
            {
                // pathOutput: alle inhaltlichen Zeilen (Marker wurden bereits entfernt)
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
            s = Regex.Replace(s, @"\s+", " "); // Whitespace normalisieren
            s = s.Replace("→", "↦");           // Kollisionsschutz
            return s;
        }

        // ==============================================================
        // ============= Rebuild ========================================
        // ==============================================================
        private static string RebuildResponseFile(List<Entry> blocks)
        {
            var sb = new StringBuilder();
            // nach Index sortieren (falls nötig)
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
            // nach Index sortieren (falls nötig)
            var ordered = blocks.OrderBy(b => b.Index).ToList();

            for (int i = 0; i < ordered.Count; i++)
            {
                // Inhalt schreiben
                sb.AppendLine(ordered[i].Block.Trim());
                // Trenner + neu nummerierter Marker (sauber 1..N)
                sb.AppendLine(PATHOUTPUT_SEPARATOR);
                sb.AppendLine($"PATH {i + 1} FINISHED");
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
