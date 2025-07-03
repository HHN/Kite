using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    public class GeneratedFeedbackManager : MonoBehaviour
    {
        private Dictionary<int, List<FeedbackEntry>> feedbackList = new Dictionary<int, List<FeedbackEntry>>();

        private static readonly string RootFolder = "Assets/_novels_twee";
        
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
            feedbackList = LoadAllFeedback();
        }

        public void Reset()
        {
            matcher.Reset();
            Debug.Log("Resetted matcher");
        }

        /// <summary>
        /// Setzt die Feedback-Liste für die gegebene Novel-ID, wenn sie im Dictionary existiert.
        /// Wirft keinen Fehler, wenn der Key nicht gefunden wird.
        /// </summary>
        public void SetIdForNovel(int id)
        {
            if (feedbackList.TryGetValue(id, out var entries) && entries != null && entries.Count > 0)
            {
                Debug.Log("SetIdForNovel: " + id + " with " + entries.Count + " entries.");
                matcher.SetFeedbackList(entries);
            }
            else
            {
                // Optional: Warnung ausgeben, dass keine Einträge gefunden wurden
                Debug.LogWarning($"Kein Feedback für Novel-ID {id} gefunden.");
                // matcher bleibt im vorherigen Zustand
            }
        }

        public void SetEvent(string eventName)
        {
            Debug.Log("SetEvent: " + eventName);
            matcher.RegisterChoice(eventName);
        }
        
        /// <summary>
        /// Prüft, ob für die gegebene Novel‐ID bereits ein Feedback‐Eintrag mit Event-Listen vorliegt.
        /// </summary>
        /// <param name="id">Die Novel-ID, die Du prüfen möchtest.</param>
        /// <returns>True, wenn eine nicht-leere Liste von Feedback-Einträgen existiert, sonst False.</returns>
        public bool HasEventsForId(int id)
        {
            Debug.Log("HasEventsForId: " + id);
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
            Debug.Log(returnString);
            return returnString;
        }
        
        /// <summary>
    /// Liest in allen Unterordnern von <paramref name="RootFolder"/>
    /// - aus visual_novel_meta_data.txt die idNumberOfNovel
    /// - aus generated_feedback.txt alle (Path, Feedback)-Paare
    /// und mappt sie auf die jeweilige ID.
    /// </summary>
    public static Dictionary<int, List<FeedbackEntry>> LoadAllFeedback()
    {
        var result = new Dictionary<int, List<FeedbackEntry>>();

        foreach (var folder in Directory.EnumerateDirectories(RootFolder))
        {
            // 1) Meta‐Datei parsen
            var metaFile = Path.Combine(folder, "visual_novel_meta_data.txt");
            if (!File.Exists(metaFile)) 
                continue;

            int novelId = ExtractNovelId(metaFile);

            // 2) Feedback‐Datei parsen
            var feedbackFile = Path.Combine(folder, "generated_feedback.txt");
            if (!File.Exists(feedbackFile)) 
                continue;

            var entries = ParseFeedbackFile(feedbackFile);
            Debug.Log($"Folder „{folder}“ → novelId={novelId}, feedback-Entries={entries.Count}");
            result[novelId] = entries;
        }
        Debug.Log("Loaded feedbacks! " +  result.Count);
        return result;
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
    /// Gibt in der Unity-Konsole aus, für welche Novel-IDs bereits Feedback-Listen geladen sind.
    /// </summary>
    public void DebugPrintAvailableIds()
    {
        if (feedbackList == null || feedbackList.Count == 0)
        {
            Debug.Log("Keine Feedback-Listen geladen.");
            return;
        }

        var ids = feedbackList.Keys
            .OrderBy(id => id)
            .Select(id => id.ToString())
            .ToArray();

        Debug.Log($"Verfügbare Novel-IDs mit Feedback: {string.Join(", ", ids)}");
    }
    
    /// <summary>
    /// Gibt in der Unity-Konsole für jede geladene Novel-ID
    /// alle Pfade und dazugehörigen Feedback-Texte aus.
    /// </summary>
    public void DebugPrintAllFeedbackEntries()
    {
        if (feedbackList == null || feedbackList.Count == 0)
        {
            Debug.Log("Keine Feedback-Daten geladen.");
            return;
        }

        foreach (var kv in feedbackList.OrderBy(k => k.Key))
        {
            Debug.Log($"--- Novel-ID {kv.Key} ({kv.Value.Count} Einträge) ---");
            foreach (var entry in kv.Value)
            {
                Debug.Log($"Pfad: {string.Join(" -> ", entry.Path)}");
                Debug.Log($"Feedback: {entry.Feedback}");
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

}
