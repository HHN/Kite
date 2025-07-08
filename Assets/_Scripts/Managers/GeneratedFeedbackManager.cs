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

        // ReSharper disable Unity.PerformanceAnalysis
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
                    DebugPrintAllFeedbackEntries();
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
            string feedback = matcher.GetFeedback();
            if (feedback != null)
            {
                return feedback;
            }

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
    string baseUrl = Path.Combine(Application.streamingAssetsPath, "feedbacks");

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
        WebLogger.Log($"Folder „{folderName}“ → novelId={novelId}, entries={entries.Count}");
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

    /// <summary>
    /// Gibt true zurück, wenn genau ein einziger FeedbackEntry
    /// die gewählten Events als Subsequenz in seinem Path enthält.
    /// </summary>
    public bool IsComplete
        => _allEntries.Count(fe => IsSubsequence(_chosenEvents, fe.Path)) == 1;

    /// <summary>
    /// Gibt den Feedback-Text desjenigen Eintrags zurück, dessen Pfad (SubPfad)
    /// als Subsequenz vollständig im gewählten Hauptpfad enthalten ist.
    /// Wenn mehrere Einträge passen, wird der mit den meisten Elementen (längster SubPfad) ausgewählt.
    /// </summary>
    /// <returns>
    /// Den Feedback-Text des besten Treffers oder null, falls keine SubPfad-Übereinstimmung gefunden wurde.
    /// </returns>
    public string? GetFeedback()
    {
        // Finde alle Einträge, deren Path als Subsequenz in _chosenEvents vorkommt
        var matches = _allEntries
            .Where(fe => IsSubsequence(_chosenEvents, fe.Path))
            .ToList();

        // Keine Übereinstimmungen → kein Feedback
        if (!matches.Any())
            return null;

        // Bestimme die maximale Länge unter den gefundenen SubPfaden
        int maxLength = matches.Max(fe => fe.Path.Count);

        // Wähle den ersten Eintrag mit dieser maximalen Länge
        var bestMatch = matches.First(fe => fe.Path.Count == maxLength);

        // Gib sein Feedback zurück
        return bestMatch.Feedback;
    }

    /// <summary>
    /// Liefert den aktuell gewählten Hauptpfad und alle
    /// Candidate-Pfade, die innerhalb dieses Hauptpfades
    /// als Subsequence vorkommen – inklusive Ein-Element-Pfade,
    /// sobald der erste Event davon gewählt ist.
    /// </summary>
    public (List<string> FullEvents, List<List<string>> SubPaths) GetStatus()
    {
        var full = new List<string>(_chosenEvents);

        // *korrekt* prüft: ist jeder Eintrag von 'path' irgendwo in 'full' in richtiger Reihenfolge?
        var subPaths = _allEntries
            .Select(fe => fe.Path)
            .Where(path => IsSubsequence(full, path))
            .ToList();

        return (full, subPaths);
    }

    private static bool IsSubsequence(
        IReadOnlyList<string> main,
        IReadOnlyList<string> sub)
    {
        int j = 0;
        for (int i = 0; i < main.Count && j < sub.Count; i++)
        {
            if (main[i] == sub[j])
                j++;
        }
        return j == sub.Count;
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
            WebLogger.Log(message);
#endif
        }

        public static void LogWarning(string message)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        Application.ExternalCall("logMessage", $"⚠️ {message}");
#else
            WebLogger.LogWarning(message);
#endif
        }

        public static void LogError(string message)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        Application.ExternalCall("logMessage", $"❌ {message}");
#else
            WebLogger.LogError(message);
#endif
        }
    }


}
