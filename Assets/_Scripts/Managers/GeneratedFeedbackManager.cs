using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Assets._Scripts._Mappings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets._Scripts.Managers
{
    public class GeneratedFeedbackManager : MonoBehaviour
    {
        private Dictionary<int, List<FeedbackEntry>> feedbackList = new Dictionary<int, List<FeedbackEntry>>();

#if UNITY_WEBGL
        private static readonly string RootFolder = Application.streamingAssetsPath;
#else
        private static readonly string RootFolder = "Assets/_novels_twee";
#endif
        private FeedbackMatcher matcher = new FeedbackMatcher();
        
        private static GeneratedFeedbackManager _instance;
        
        public static GeneratedFeedbackManager Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = FindObjectOfType<GeneratedFeedbackManager>();
                    if (!_instance)
                    {
                        GameObject obj = new GameObject("GeneratedFeedbackManager");
                        _instance = obj.AddComponent<GeneratedFeedbackManager>();
                        DontDestroyOnLoad(obj);
                    }
                }
                return _instance;
            }
        }

        
        public void LoadFeedbacks()
        {
            // 1) Mapping asynchron laden
            StartCoroutine(MappingManager.Instance.LoadNovelMapping(folderToId =>
            {
                // 2) Feedbacks mit Callback laden
                StartCoroutine(LoadAllFeedbackWithMappingAsync(folderToId, result =>
                {
                    feedbackList = result;
                    DebugPrintAvailableIds();
                    // DebugPrintAllFeedbackEntries();
                }));
            }));
        }



        public void Reset()
        {
            matcher.Reset();
            WebLogger.Log("Resetted matcher");
        }

        /// <summary>
        /// Setzt die Feedback-Liste für die gegebene Novel-ID, wenn sie im Dictionary existiert.
        /// Wirft keinen Fehler, wenn der Key nicht gefunden wird.
        /// </summary>
        public void SetIdForNovel(int id)
        {
            if (feedbackList.TryGetValue(id, out var entries) && entries != null && entries.Count > 0)
            {
                WebLogger.Log("SetIdForNovel: " + id + " with " + entries.Count + " entries.");
                matcher.SetFeedbackList(entries);
            }
            else
            {
                // Optional: Warnung ausgeben, dass keine Einträge gefunden wurden
                WebLogger.LogWarning($"Kein Feedback für Novel-ID {id} gefunden.");
                // matcher bleibt im vorherigen Zustand
            }
        }

        public void SetEvent(string eventName)
        {
            WebLogger.Log("SetEvent: " + eventName);
            matcher.RegisterChoice(eventName);
        }
        
        /// <summary>
        /// Prüft, ob für die gegebene Novel‐ID bereits ein Feedback‐Eintrag mit Event-Listen vorliegt.
        /// </summary>
        /// <param name="id">Die Novel-ID, die Du prüfen möchtest.</param>
        /// <returns>True, wenn eine nicht-leere Liste von Feedback-Einträgen existiert, sonst False.</returns>
        public bool HasEventsForId(int id)
        {
            WebLogger.Log("HasEventsForId: " + id);
            return feedbackList.TryGetValue(id, out var entries)
                   && entries != null
                   && entries.Count > 0;
        }

        public string GetFeedback()
        {
            // Einmalig am Ende auswerten:
            var best = matcher.SelectBestEntry();

            // Logging für Nachvollziehbarkeit
            if (best.Entry != null)
            {
                if (best.IsFullMatch)
                {
                    //Debug.Log($"[Matcher] Vollständiger Sub-Pfad gefunden (Länge={best.Entry.Path.Count}):");
                    //Debug.Log("  " + string.Join(" -> ", best.Entry.Path));
                    // Vollständiger Match → Feedback ausgeben
                    return best.Entry.Feedback;
                }
                else
                {
                    //Debug.Log($"[Matcher] KEIN vollständiger Sub-Pfad. Bester Teiltreffer: {best.MatchedCount} Treffer von {best.Entry.Path.Count}");
                    //Debug.Log("  Bester (teilweiser) Sub-Pfad: " + string.Join(" -> ", best.Entry.Path));

                    var full = string.Join(" -> ", best.FullMainPath);
                    var sub  = string.Join(", ", best.Entry.Path);
                    var msg =
                        "Vollständiger Pfad:\n\n" + full + "\n\n" +
                        "Bester (teilweiser) Sub-Pfad:\n\n  - " + sub +
                        $"  ({best.MatchedCount} Treffer)";
                    WebLogger.Log(msg);
                    return msg;
                }
            }

            // Fallback (keine Einträge)
            var (fullEvents, subPaths) = matcher.GetStatus();
            string subPathsString = "";
            if (subPaths.Count > 0)
            {
                foreach (var sp in subPaths)
                {
                    subPathsString += ("  - " + string.Join(", ", sp));
                }
            }
            string returnString = "Vollständiger Pfad:\n\n" + string.Join(" -> ", fullEvents) + "\n\n" + "Mögliche Sub-Pfade:\n\n" + subPathsString;
            WebLogger.Log(returnString);
            return returnString;
        } 
        
        // TODO: Entferne eine LoadFeedbackMethode. Aktuell Duplikat
        
        /// <summary>
    /// Liest in allen Unterordnern von <paramref name="RootFolder"/>
    /// - aus visual_novel_meta_data.txt die idNumberOfNovel
    /// - aus generated_feedback.txt alle (Path, Feedback)-Paare
    /// und mappt sie auf die jeweilige ID.
    /// </summary>
    public static Dictionary<int, List<FeedbackEntry>> LoadAllFeedback()
{
    var result = new Dictionary<int, List<FeedbackEntry>>();

#if UNITY_WEBGL
    // ===== WebGL-Variante: IDs aus NovelMapping.txt holen =====Application.streamingAssetsPath
    // Mapping-Datei
    var mappingPath = Path.Combine(RootFolder, "NovelMapping.txt");
    if (!File.Exists(mappingPath))
    {
        WebLogger.LogError($"NovelMapping.txt nicht gefunden unter {mappingPath}");
        return result;
    }

    // Lese alle Zeilen "OrdnerName:ID"
    var folderToId = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
    foreach (var line in File.ReadAllLines(mappingPath))
    {
        if (string.IsNullOrWhiteSpace(line) || !line.Contains(':')) continue;
        var parts = line.Split(new[] { ':' }, 2);
        var folderName = parts[0].Trim();
        if (int.TryParse(parts[1].Trim(), out var id))
            folderToId[folderName] = id;
        else
            WebLogger.LogWarning($"Ungültige Zeile in NovelMapping.txt: „{line}“");
    }

    // Feedback-Ordner unter StreamingAssets/feedbacks
    var feedbackRoot = Path.Combine(RootFolder, "feedbacks");
    if (!Directory.Exists(feedbackRoot))
    {
        WebLogger.LogError($"Feedback-Ordner nicht gefunden unter {feedbackRoot}");
        return result;
    }

    // pro Unterordner feedback parsen
    foreach (var folder in Directory.EnumerateDirectories(feedbackRoot))
    {
        var name = Path.GetFileName(folder);
        if (!folderToId.TryGetValue(name, out var novelId))
        {
            WebLogger.LogWarning($"Kein Mapping für Ordner „{name}“, übersprungen.");
            continue;
        }

        var feedbackFile = Path.Combine(folder, "generated_feedback.txt");
        if (!File.Exists(feedbackFile))
        {
            WebLogger.LogWarning($"Keine generated_feedback.txt in „{name}“, übersprungen.");
            continue;
        }

        var entries = ParseFeedbackFile(feedbackFile);
        WebLogger.Log($"Folder „{name}“ → novelId={novelId}, feedback-Entries={entries.Count}");
        result[novelId] = entries;
    }

#else
    // ===== Standardeinleseweg: IDs aus visual_novel_meta_data.txt =====
    foreach (var folder in Directory.EnumerateDirectories(RootFolder))
    {
        var metaFile = Path.Combine(folder, "visual_novel_meta_data.txt");
        if (!File.Exists(metaFile)) continue;

        int novelId = ExtractNovelId(metaFile);

        var feedbackFile = Path.Combine(folder, "generated_feedback.txt");
        if (!File.Exists(feedbackFile)) continue;

        var entries = ParseFeedbackFile(feedbackFile);
        WebLogger.Log($"Folder „{folder}“ → novelId={novelId}, feedback-Entries={entries.Count}");
        result[novelId] = entries;
    }
#endif

    WebLogger.Log("Loaded feedbacks! " + result.Count);
    return result;
}

    /// <summary>
/// Lädt asynchron alle Feedbacks, basierend auf dem übergebenen Mapping.
/// </summary>
/// <param name="folderToId">Mapping von Ordnernamen zu Novel-IDs</param>
/// <param name="onComplete">Callback, das das Dictionary<int, List<FeedbackEntry>> erhält</param>
private IEnumerator LoadAllFeedbackWithMappingAsync(
    Dictionary<string,int> folderToId,
    Action<Dictionary<int,List<FeedbackEntry>>> onComplete)
{
    var result = new Dictionary<int, List<FeedbackEntry>>();

#if UNITY_WEBGL
    // Basis-URL zu Deinen Feedback-Dateien im StreamingAssets
    string baseUrl = Application.streamingAssetsPath.TrimEnd('/') + "/feedbacks";

    foreach (var kv in folderToId)
    {
        string folderName = kv.Key;
        int    novelId    = kv.Value;
        string feedbackUrl = $"{baseUrl}/{folderName}/generated_feedback.txt";

        using var www = UnityWebRequest.Get(feedbackUrl);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            WebLogger.LogWarning($"[WebGL] Konnte {feedbackUrl} nicht laden: {www.error}");
            continue;
        }

        var content = www.downloadHandler.text;
        var entries = ParseFeedbackFileFromString(content);
        WebLogger.Log($"[WebGL] Folder „{folderName}“ → novelId={novelId}, entries={entries.Count}");
        result[novelId] = entries;
    }
#else
    // Editor/Standalone: Mapping-Keys durchgehen
    string baseFolder = RootFolder;
    foreach (var kv in folderToId)
    {
        string folderName = kv.Key;
        int    novelId    = kv.Value;
        string folderPath = Path.Combine(baseFolder, folderName);

        if (!Directory.Exists(folderPath))
        {
            WebLogger.LogWarning($"[Standalone] Ordner „{folderName}“ unter {baseFolder} nicht gefunden, übersprungen.");
            continue;
        }

        string feedbackFile = Path.Combine(folderPath, "generated_feedback.txt");
        if (!File.Exists(feedbackFile))
        {
            WebLogger.LogWarning($"[Standalone] Keine generated_feedback.txt in „{folderName}“, übersprungen.");
            continue;
        }

        var entries = ParseFeedbackFile(feedbackFile);
        WebLogger.Log($"Folder „{folderName}“ → novelId={novelId}, feedback-Entries={entries.Count}");
        result[novelId] = entries;
    }
#endif

    // Callback aufrufen
    onComplete?.Invoke(result);
}

       
        
        
        private static int ExtractNovelId(string metaFilePath)
        {
            if (!File.Exists(metaFilePath))
                throw new FileNotFoundException($"Meta-Datei nicht gefunden: {metaFilePath}");

            var json = File.ReadAllText(metaFilePath, Encoding.UTF8);
            var root = JObject.Parse(json);

            // Versuche, die idNumberOfNovel als Integer auszulesen
            var token = root["idNumberOfNovel"];
            if (token != null && int.TryParse(token.ToString(), out var id))
            {
                return id;
            }

            throw new InvalidDataException($"\"idNumberOfNovel\" nicht gefunden oder ungültig in {metaFilePath}");
        }

        
        // TODO: Duplikat. Eines entfernen oder zusammenführen.
        /// <summary>
        /// Liest das Feedback-File ein und liefert pro “###<nummer>[…]…#$%”-Block
        /// eine FeedbackEntry mit der Liste (Path) und dem Feedback-Text.
        /// </summary>
        public static List<FeedbackEntry> ParseFeedbackFile(string feedbackFilePath)
        {
            string content = File.ReadAllText(feedbackFilePath);
            var entries = new List<FeedbackEntry>();

            // Regex fängt jeden Block ab: ### gefolgt von Zahl, dann [...] und den Text bis 
            // zum nächsten ###<Zahl> oder Dateiende.
            var pattern = @"###\s*\d+\s*\[(.*?)\]\s*(.*?)(?=###\s*\d+|$)";
            var rx = new Regex(pattern, RegexOptions.Singleline);

            foreach (Match m in rx.Matches(content))
            {
                // Gruppe 1 = alles zwischen den eckigen Klammern
                var listContent = m.Groups[1].Value;
                var pathList = listContent
                    .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(p => p.Trim())
                    .ToList();

                // Gruppe 2 = Feedback-Text (inklusive "#$%" am Ende), wir trimmen Marker weg
                var feedbackBlock = m.Groups[2].Value;
                var endMarkerIndex = feedbackBlock.IndexOf("#$%", StringComparison.Ordinal);
                var feedback = (endMarkerIndex >= 0
                        ? feedbackBlock.Substring(0, endMarkerIndex)
                        : feedbackBlock)
                    .Trim();

                entries.Add(new FeedbackEntry
                {
                    Path     = pathList,
                    Feedback = feedback
                });
            }

            return entries;
        }
        
        /// <summary>
        /// Extrahiert FeedbackEntries aus rohem Dateiinhalt (statt „ParseFeedbackFile(string path)“).
        /// </summary>
        private static List<FeedbackEntry> ParseFeedbackFileFromString(string content)
        {
            var entries = new List<FeedbackEntry>();
            var blocks = content
                .Split(new[] { "###" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var block in blocks)
            {
                int startList = block.IndexOf('[');
                int endList   = block.IndexOf(']', startList + 1);
                if (startList < 0 || endList < 0) continue;

                var pathList = block
                    .Substring(startList + 1, endList - startList - 1)
                    .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(p => p.Trim())
                    .ToList();

                int startFb = endList + 1;
                int endFb   = block.IndexOf("#$%", startFb, StringComparison.Ordinal);
                if (endFb < 0) endFb = block.Length;

                var fb = block.Substring(startFb, endFb - startFb).Trim();
                entries.Add(new FeedbackEntry { Path = pathList, Feedback = fb });
            }

            return entries;
        }
    
    /// <summary>
    /// Gibt in der Unity-Konsole aus, für welche Novel-IDs bereits Feedback-Listen geladen sind.
    /// </summary>
    public void DebugPrintAvailableIds()
    {
        if (feedbackList == null || feedbackList.Count == 0)
        {
            WebLogger.Log("Keine Feedback-Listen geladen.");
            return;
        }

        var ids = feedbackList.Keys
            .OrderBy(id => id)
            .Select(id => id.ToString())
            .ToArray();

        WebLogger.Log($"Verfügbare Novel-IDs mit Feedback: {string.Join(", ", ids)}");
        //Debug.Log($"Verfügbare Novel-IDs mit Feedback: {string.Join(", ", ids)}");
    }
    
    /// <summary>
    /// Gibt in der Unity-Konsole für jede geladene Novel-ID
    /// alle Pfade und dazugehörigen Feedback-Texte aus.
    /// </summary>
    public void DebugPrintAllFeedbackEntries()
    {
        if (feedbackList == null || feedbackList.Count == 0)
        {
            WebLogger.Log("Keine Feedback-Daten geladen.");
            return;
        }

        foreach (var kv in feedbackList.OrderBy(k => k.Key))
        {
            WebLogger.Log($"--- Novel-ID {kv.Key} ({kv.Value.Count} Einträge) ---");
            foreach (var entry in kv.Value)
            {
                WebLogger.Log($"Pfad: {string.Join(" -> ", entry.Path)}");
                WebLogger.Log($"Feedback: {entry.Feedback}");
            }
        }
    }
    }
    
    
    public class FeedbackEntry
    {
        public List<string> Path { get; set; } = new();
        public string Feedback { get; set; } = string.Empty;
    }
    
    public class FeedbackMatcher
{
    private readonly List<FeedbackEntry> _allEntries = new();
    private readonly List<string> _chosenEvents = new();

    public void SetFeedbackList(IEnumerable<FeedbackEntry> entries)
    {
        _allEntries.Clear();
        _allEntries.AddRange(entries);
        Reset();
    }

    public void Reset()
    {
        _chosenEvents.Clear();
    }

    public void RegisterChoice(string chosenEvent)
    {
        _chosenEvents.Add(chosenEvent);
    }

    // ---- Auswahl-Logik nur am Ende des Playthroughs ----

    public sealed class BestSelection
    {
        public FeedbackEntry Entry { get; set; }     // ausgewählter Eintrag (voll/teilweise)
        public bool IsFullMatch { get; set; }        // true = kompletter Subpfad enthalten
        public int MatchedCount { get; set; }        // Anzahl gematchter Elemente (bei Teilmatch)
        public List<string> FullMainPath { get; set; } // der komplette gewählte Pfad (normalisiert)
    }

    /// <summary>
    /// Wählt am Ende den passenden Sub-Pfad:
    /// 1) Gibt es volle Matches? → nimm den LÄNGSTEN.
    /// 2) Sonst nimm den Teil-Match mit der höchsten Trefferzahl (LCS).
    /// </summary>
    public BestSelection SelectBestEntry()
    {
        var main = Normalize(_chosenEvents);

        FeedbackEntry bestFull = null;
        int bestFullLen = -1;

        FeedbackEntry bestPartial = null;
        int bestPartialCount = -1;
        int bestPartialPathLen = -1;

        foreach (var e in _allEntries)
        {
            var sub = Normalize(e.Path);

            if (IsSubsequence(main, sub))
            {
                // Vollständiger Match → längsten wählen
                if (sub.Count > bestFullLen)
                {
                    bestFullLen = sub.Count;
                    bestFull = e;
                }
                continue;
            }

            // Teiltreffer per LCS
            int lcs = LcsLength(main, sub);
            if (lcs > bestPartialCount ||
               (lcs == bestPartialCount && sub.Count > bestPartialPathLen))
            {
                bestPartialCount = lcs;
                bestPartialPathLen = sub.Count;
                bestPartial = e;
            }
        }

        if (bestFull != null)
        {
            return new BestSelection
            {
                Entry = bestFull,
                IsFullMatch = true,
                MatchedCount = bestFull.Path.Count,
                FullMainPath = main
            };
        }

        if (bestPartial != null)
        {
            return new BestSelection
            {
                Entry = bestPartial,
                IsFullMatch = false,
                MatchedCount = bestPartialCount,
                FullMainPath = main
            };
        }

        // Nichts gefunden (keine Einträge)
        return new BestSelection
        {
            Entry = null,
            IsFullMatch = false,
            MatchedCount = 0,
            FullMainPath = main
        };
    }

    /// <summary>
    /// Liefert den Feedback-Text desjenigen Eintrags, der per SelectBestEntry()
    /// (vollständig) passt. Bei Teilmatch gibt diese Methode null zurück,
    /// damit der Aufrufer (Manager) den besten Teil-Pfad ausgeben kann.
    /// </summary>
    public string? GetFeedback()
    {
        var best = SelectBestEntry();
        if (best.Entry != null && best.IsFullMatch)
            return best.Entry.Feedback;
        return null;
    }

    /// <summary>
    /// Gibt den kompletten Event-Verlauf zurück und alle Sub-Pfade,
    /// die irgendwo (in richtiger Reihenfolge) darin enthalten sind.
    /// </summary>
    public (List<string> FullEvents, List<List<string>> SubPaths) GetStatus()
    {
        var full = new List<string>(_chosenEvents);

        var subs = _allEntries
            .Select(fe => fe.Path)
            .Where(path => ContainsSubsequenceAnywhere(full, path))
            .ToList();

        return (full, subs);
    }

    // ---------- Hilfsfunktionen ----------

    private static List<string> Normalize(IReadOnlyList<string> list)
        => list.Where(s => !string.IsNullOrWhiteSpace(s))
               .Select(s => s.Trim())
               .ToList();

    /// <summary>
    /// Prüft, ob 'sub' als Subsequenz (mit Gaps) in 'main' vorkommt.
    /// </summary>
    private static bool IsSubsequence(IReadOnlyList<string> main, IReadOnlyList<string> sub)
    {
        if (sub.Count == 0) return true;
        int j = 0;
        for (int i = 0; i < main.Count; i++)
        {
            if (string.Equals(main[i], sub[j], StringComparison.Ordinal))
            {
                j++;
                if (j == sub.Count) return true;
            }
        }
        return false;
    }

    /// <summary>
    /// LCS-Länge (Longest Common Subsequence) zwischen zwei Sequenzen.
    /// Misst die maximale Anzahl von Elementen aus 'sub', die in 'main'
    /// in derselben Reihenfolge (mit Gaps) vorkommen.
    /// </summary>
    private static int LcsLength(IReadOnlyList<string> a, IReadOnlyList<string> b)
    {
        int n = a.Count, m = b.Count;
        if (n == 0 || m == 0) return 0;

        // Speicheroptimierte 2-Zeilen-DP
        var prev = new int[m + 1];
        var curr = new int[m + 1];

        for (int i = 1; i <= n; i++)
        {
            Array.Clear(curr, 0, curr.Length);
            for (int j = 1; j <= m; j++)
            {
                if (string.Equals(a[i - 1], b[j - 1], StringComparison.Ordinal))
                    curr[j] = prev[j - 1] + 1;
                else
                    curr[j] = Math.Max(prev[j], curr[j - 1]);
            }
            // swap
            var tmp = prev; prev = curr; curr = tmp;
        }
        return prev[m];
    }

    /// <summary>
    /// Für Status-Ansicht: einfacher Subsequence-Check (mit Gaps).
    /// </summary>
    private static bool ContainsSubsequenceAnywhere(
        IReadOnlyList<string> main,
        IReadOnlyList<string> sub)
    {
        var mainClean = Normalize(main);
        var subClean  = Normalize(sub);
        return IsSubsequence(mainClean, subClean);
    }
}


    public static class WebLogger
    {
        public static void Log(string message)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        // ruft im HTML-Template die JS-Funktion logMessage(message) auf
        Application.ExternalCall("logMessage", message);
#else
            //Debug.Log(message);
#endif
        }

        public static void LogWarning(string message)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        Application.ExternalCall("logMessage", $"⚠️ {message}");
#else
            //Debug.LogWarning(message);
#endif
        }

        public static void LogError(string message)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        Application.ExternalCall("logMessage", $"❌ {message}");
#else
            //Debug.LogError(message);
#endif
        }
    }


}
