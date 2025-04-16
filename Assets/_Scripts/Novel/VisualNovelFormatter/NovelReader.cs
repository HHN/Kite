using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets._Scripts.Managers;
using Assets._Scripts.Novel.VisualNovelLoader;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using Assets._Scripts.Player.KiteNovels.VisualNovelFormatter;
using Assets._Scripts.Player.KiteNovels.VisualNovelLoader;

namespace Assets._Scripts.Novel.VisualNovelFormatter
{
    // Die NovelReader-Klasse ist ein MonoBehaviour, das den Prozess des Ladens, Verarbeitens
    // und Konvertierens von Novellen aus dem Twee-Format in das JSON-Format steuert.
    public class NovelReader : MonoBehaviour
    {
        // Konstanten, die die Pfade zu wichtigen Dateien definieren.
        // NovelListPath: Pfad zur Datei, die eine Liste aller Novellenpfade enth�lt.
        private const string NovelListPath = "_novels_twee/list_of_novels.txt";
        // MetaDataFileName: Dateiname der Datei, die Metadaten (z. B. Titel, Einstellungen) der Novelle enth�lt.
        private const string MetaDataFileName = "visual_novel_meta_data.txt";
        // EventListFileName: Dateiname der Datei, die die Event-Liste (Story-Ereignisse) der Novelle enth�lt.
        private const string EventListFileName = "visual_novel_event_list.txt";
        // Flag, das angibt, ob der gesamte Konvertierungsvorgang abgeschlossen ist.
        private bool _isFinished;

        public void ImportNovel()
        {
            StartCoroutine(ImportNovelWithTweeApproach());
        }

        /// <summary>
        /// Gibt zur�ck, ob der gesamte Konvertierungsvorgang abgeschlossen ist.
        /// </summary>
        /// <returns>true, wenn der Prozess abgeschlossen ist, ansonsten false.</returns>
        public bool IsFinished()
        {
            return _isFinished;
        }

        private IEnumerator ImportNovelWithTweeApproach()
        {
            // Bestimmt den vollst�ndigen Pfad zur Liste der Novellenpfade.
            string fullPath = Path.Combine(Application.dataPath, NovelListPath);
            
            // Lade die Liste der Novel-Pfade.
            yield return StartCoroutine(LoadNovelPaths(fullPath, listOfAllNovelPaths =>
            {
                if (listOfAllNovelPaths == null || listOfAllNovelPaths.Count == 0)
                {
                    Debug.LogWarning("Loading Novels failed: No Novels found! Path: " + fullPath);
                    KiteNovelManager.Instance().SetAllKiteNovels(new List<VisualNovel>());
                    return;
                }

                // Starte die Verarbeitung der Novellen mit selektivem �berschreiben.
                StartCoroutine(ProcessAndMergeNovels(listOfAllNovelPaths));
            }));
        }

        /// <summary>
        /// Processes and merges visual novels by loading, converting, and updating the existing list.
        /// New novels are added, and existing ones with matching IDs are updated.
        /// The result is saved as a JSON file.
        /// </summary>
        /// <param name="listOfAllNovelPaths">List of relative paths to all novel folders.</param>
        /// <returns>IEnumerator for coroutine execution.</returns>
        private IEnumerator ProcessAndMergeNovels(List<string> listOfAllNovelPaths)
        {
            // List to hold all processed novel folders.
            List<KiteNovelFolder> allFolders = new List<KiteNovelFolder>();

            // Process each novel path individually.
            foreach (string pathOfNovel in listOfAllNovelPaths)
            {
                yield return ProcessSingleNovel(pathOfNovel, allFolders);
            }

            // Convert the processed folders into VisualNovel objects.
            List<VisualNovel> visualNovels = KiteNovelConverter.ConvertFilesToNovels(allFolders);

            // Retrieve already loaded novels from the manager.
            List<VisualNovel> existingNovels = KiteNovelManager.Instance().GetAllKiteNovels();

            // If no novels exist, save the entire new list.
            if (existingNovels == null || existingNovels.Count == 0)
            {
                SaveToJson(new NovelListWrapper(visualNovels));
            }
            else
            {
                // Prepare a list for the final result, including updated and new novels.
                List<VisualNovel> modifiedListOfNovels = new List<VisualNovel>();

                // Update existing novels with new ones if IDs match.
                foreach (VisualNovel oldNovel in existingNovels)
                {
                    VisualNovel updatedNovel = visualNovels.FirstOrDefault(n => n.id == oldNovel.id) ?? oldNovel;

                    if (!ReferenceEquals(updatedNovel, oldNovel))
                    {
                        Debug.Log("Overriding Novel: " + updatedNovel.title);
                    }

                    // F�ge das (ggf. aktualisierte) Novel der neuen Liste hinzu.
                    modifiedListOfNovels.Add(updatedNovel);
                }
            
                // Add any entirely new novels that weren't already included.
                foreach (VisualNovel newNovel in visualNovels)
                {
                    if (!modifiedListOfNovels.Any(n => n.id == newNovel.id))
                    {
                        modifiedListOfNovels.Add(newNovel);
                        Debug.Log("Added new Novel : " + newNovel.title);
                    }
                }

                // Save the merged result to JSON.
                SaveToJson(new NovelListWrapper(modifiedListOfNovels));
            }
        }

        /// <summary>
        /// Processes a single visual novel by loading its metadata and event list,
        /// transforming the content, and adding it to the provided folder list.
        /// </summary>
        /// <param name="pathOfNovel">Relative path to the novel's folder inside the project.</param>
        /// <param name="allFolders">The list to which the processed novel will be added.</param>
        /// <returns>IEnumerator for coroutine execution.</returns>
        private IEnumerator ProcessSingleNovel(string pathOfNovel, List<KiteNovelFolder> allFolders)
        {
            // Build the full paths for metadata and event list files.
            string fullPathOfNovelMetaData = Path.Combine(Application.dataPath, pathOfNovel, MetaDataFileName);
            string fullPathOfNovelEventList = Path.Combine(Application.dataPath, pathOfNovel, EventListFileName);

            KiteNovelMetaData kiteNovelMetaData = null;
            string jsonStringOfEventList = null;

            // Load and deserialize the novel's metadata.
            yield return StartCoroutine(LoadAndDeserialize<KiteNovelMetaData>(fullPathOfNovelMetaData, result => { kiteNovelMetaData = result; }));

            // Skip if metadata couldn't be loaded.
            if (kiteNovelMetaData == null)
            {
                Debug.LogWarning("Kite Novel Meta Data could not be loaded: " + pathOfNovel);
                yield break;
            }

            // Load the event list content.
            yield return StartCoroutine(LoadFileContent(fullPathOfNovelEventList, result => { jsonStringOfEventList = result; }));

            // Skip if event list is empty or missing.
            if (string.IsNullOrEmpty(jsonStringOfEventList))
            {
                Debug.LogWarning("Kite Novel Event List could not be loaded: " + pathOfNovel);
                yield break;
            }

            // Replace placeholder words in the event list using metadata replacement rules.
            jsonStringOfEventList = ReplaceWordsInString(jsonStringOfEventList, kiteNovelMetaData.WordsToReplace);

            // Convert the raw text into a structured list of visual novel events.
            List<VisualNovelEvent> kiteNovelEventList = KiteNovelConverter.ConvertTextDocumentIntoEventList(jsonStringOfEventList, kiteNovelMetaData);

            // Add the processed folder to the result list.
            allFolders.Add(new KiteNovelFolder(kiteNovelMetaData, kiteNovelEventList));
        }

        /// <summary>
        /// L�dt den Inhalt der Datei, die alle Novellenpfade enth�lt, und �bergibt die
        /// deserialisierte Liste an den Callback.
        /// </summary>
        private IEnumerator LoadNovelPaths(string path, System.Action<List<string>> callback)
        {
            // Starte die Coroutine zum Laden des Dateiinhalts.
            yield return StartCoroutine(LoadFileContent(path, jsonString =>
            {
                // Falls der geladene Inhalt leer ist, rufe den Callback mit null auf.
                if (string.IsNullOrEmpty(jsonString))
                {
                    callback(null);
                }
                else
                {
                    // Deserialisiere den JSON-String in ein KiteNovelList-Objekt.
                    KiteNovelList kiteNovelList = JsonConvert.DeserializeObject<KiteNovelList>(jsonString);
                    // �bergibt die Liste der Novellenpfade an den Callback.
                    callback(kiteNovelList?.VisualNovels);
                }
            }));
        }

        /// <summary>
        /// L�dt den Inhalt einer Datei und deserialisiert diesen in ein Objekt vom Typ T.
        /// Das Ergebnis wird �ber den Callback zur�ckgegeben.
        /// </summary>
        private IEnumerator LoadAndDeserialize<T>(string path, System.Action<T> callback)
        {
            // Starte die Coroutine zum Laden des Dateiinhalts.
            yield return StartCoroutine(LoadFileContent(path, jsonString =>
            {
                // Falls der Inhalt leer ist, wird default(T) zur�ckgegeben.
                if (string.IsNullOrEmpty(jsonString))
                {
                    callback(default);
                }
                else
                {
                    // Deserialisiere den JSON-String in ein Objekt vom Typ T (mittels Newtonsoft.Json).
                    T result = JsonConvert.DeserializeObject<T>(jsonString);
                    callback(result);
                }
            }));
        }

        /// <summary>
        /// L�dt den Inhalt einer Datei asynchron.
        /// - Auf iOS (iPhonePlayer) wird der Inhalt synchron mit File. ReadAllText geladen.
        /// - Auf anderen Plattformen wird UnityWebRequest verwendet, um den Inhalt asynchron zu laden.
        /// </summary>
        private IEnumerator LoadFileContent(string path, System.Action<string> callback)
        {
            // Falls die Plattform iOS ist, wird die Datei direkt gelesen.
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                string jsonString = File.ReadAllText(path);
                callback(jsonString);
            }
            else
            {
                // F�r andere Plattformen wird UnityWebRequest verwendet.
                using (UnityWebRequest www = UnityWebRequest.Get(path))
                {
                    yield return www.SendWebRequest();

                    // �berpr�fe, ob ein Verbindungs- oder Protokollfehler aufgetreten ist.
                    if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
                    {
                        Debug.LogError($"Error loading file at {path}: {www.error}");
                        callback(null);
                    }
                    else
                    {
                        // Wenn kein Fehler aufgetreten ist, wird der geladene Text zur�ckgegeben.
                        callback(www.downloadHandler.text);
                    }
                }
            }
        }

        /// <summary>
        /// Ersetzt in einem Eingabestring alle Vorkommen von bestimmten W�rtern durch definierte Ersatzwerte.
        /// Die Wortpaare werden in der Liste wordsToReplace �bergeben.
        /// </summary>
        private string ReplaceWordsInString(string input, List<WordPair> wordsToReplace)
        {
            // Iteriere �ber alle Wortpaare.
            foreach (WordPair wordPair in wordsToReplace)
            {
                // Pr�fe, ob das Wortpaar g�ltig ist (nicht null und beide Werte sind nicht leer).
                if (wordPair != null && !string.IsNullOrEmpty(wordPair.WordToReplace) &&
                    !string.IsNullOrEmpty(wordPair.ReplaceByValue))
                {
                    // Ersetze das zu ersetzende Wort durch den definierten Ersatzwert.
                    input = input.Replace(wordPair.WordToReplace, wordPair.ReplaceByValue);
                }
            }
            // Gib den modifizierten String zur�ck.
            return input;
        }

        /// <summary>
        /// Konvertiert das �bergebene NovelListWrapper-Objekt in einen JSON-String und speichert diesen in einer Datei.
        /// Der Pfad wird �ber Application.dataPath bestimmt.
        /// Anschlie�end wird ein Log ausgegeben und _isFinished auf true gesetzt.
        /// </summary>
        private void SaveToJson(NovelListWrapper novelListWrapper)
        {
            // Konvertiere das Objekt in einen formatierten JSON-String.
            string json = JsonUtility.ToJson(novelListWrapper, true);
            // Bestimme den Speicherpfad (im gleichen Verzeichnis wie die Applikationsdaten).
            string path = Path.Combine(Application.dataPath, "StreamingAssets/novels.json");
            // Schreibe den JSON-String in die Datei.
            File.WriteAllText(path, json);
            // Logge den erfolgreichen Abschluss mit dem Speicherpfad.
            Debug.Log($"Visual Novels have been successfully converted to JSON format and saved under the following path: {path}");
            // Setze das Flag, dass der Konvertierungsvorgang abgeschlossen ist.
            _isFinished = true;
        }
    }

    //TODO: Auslagern in eigenes File

    /// <summary>
    /// This script tests the keyword reading and extraction.
    /// You can specify the file path (relative to Application.dataPath or an absolute path) in the Inspector.
    /// The script reads the file, processes each line using the NovelKeywordParser, and outputs
    /// the extracted keyword properties (such as End, CharacterIndex, Action, FaceExpression, Sound, Bias)
    /// to the console.
    /// </summary>
    public class KeywordTester : MonoBehaviour
    {
        // Public field to specify the folder path in the Inspector.
        // Example: "Assets/_novels_twee/Eltern" if all files are located in this folder or its subfolders.
        public string folderPath = "Assets/_novels_twee/";

        /// <summary>
        /// Called when the script starts.
        /// Initiates the process of reading all files named "visual_novel_event_list.txt" from the given folder.
        /// </summary>
        public void Start()
        {
            // Start the coroutine that loads and processes keywords from all matching files in the folder.
            StartCoroutine(ProcessKeywordFolder());
        }

        /// <summary>
        /// Coroutine that searches for all files named "visual_novel_event_list.txt"
        /// in the specified folder (including subdirectories), processes each file to extract the keyword data,
        /// and outputs the total count of valid keywords found to the Unity console.
        /// </summary>
        private IEnumerator ProcessKeywordFolder()
        {
            // Build the full folder path by combining Application.dataPath with the folderPath provided.
            string fullFolderPath = Path.Combine(Application.dataPath, folderPath);

            // Check if the folder exists.
            if (!Directory.Exists(fullFolderPath))
            {
                Debug.LogError("Keyword folder not found at: " + fullFolderPath);
                yield break;
            }

            // Get all files with the name "visual_novel_event_list.txt" in the folder and its subdirectories.
            string[] filePaths = Directory.GetFiles(fullFolderPath, "visual_novel_event_list.txt", SearchOption.AllDirectories);
            Debug.Log("Number of files found: " + filePaths.Length);

            // Liste zum Sammeln aller validen Keyword-Modelle aus allen Dateien.
            List<NovelKeywordModel> allModels = new List<NovelKeywordModel>();

            // Gehe alle gefundenen Dateien durch.
            foreach (string file in filePaths)
            {
                Debug.Log("Processing file: " + file);

                // Lese den Inhalt der Datei synchron.
                List<string> fileContent = new List<string>();
                fileContent.AddRange(File.ReadAllLines(file));

                // Rufe den Parser auf, der den gesamten Dateiinhalt verarbeitet.
                List<NovelKeywordModel> models = NovelKeywordParser.ParseKeywordsFromFile(fileContent);

                if (models != null)
                {
                    allModels.AddRange(models);
                }

                // Optional: Warte einen Frame, um große Dateien nicht blockierend zu verarbeiten.
                yield return null;
            }

            // Gib die Gesamtzahl der gefundenen validen Keywords in der Konsole aus.
            Debug.Log("Total valid keywords found across all files: " + allModels.Count);

            yield return null;
        }
    }
}
