using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets._Scripts.Managers;
using Assets._Scripts.Novel.VisualNovelLoader;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using Assets._Scripts.Player.KiteNovels.VisualNovelFormatter;

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

        /// <summary>
        /// Startet den Prozess, alle Novels im Twee-Format zu laden und in JSON zu konvertieren.
        /// Diese Methode startet die Coroutine LoadAllNovelsWithTweeApproach.
        /// </summary>
        public void ConvertNovelsFromTweeToJSON()
        {
            StartCoroutine(LoadAllNovelsWithTweeApproach());
        }

        /// <summary>
        /// Startet den Prozess, alle Novellen zu laden und nur selektiv alte Novellen zu �berschreiben.
        /// Diese Methode startet die Coroutine LoadAllNovelsWithTweeApproachAndSelectiveOverrideOldNovels.
        /// </summary>
        public void ConvertNovelsFromTweeToJSONAndSelectiveOverrideOldNovels()
        {
            StartCoroutine(LoadAllNovelsWithTweeApproachAndSelectiveOverrideOldNovels());
        }

        /// <summary>
        /// Gibt zur�ck, ob der gesamte Konvertierungsvorgang abgeschlossen ist.
        /// </summary>
        /// <returns>true, wenn der Prozess abgeschlossen ist, ansonsten false.</returns>
        public bool IsFinished()
        {
            return _isFinished;
        }

        /// <summary>
        /// Coroutine, die alle Novellen im Twee-Format l�dt.
        /// Zun�chst wird gepr�ft, ob bereits Novellen geladen wurden.
        /// Anschlie�end wird der vollst�ndige Pfad zur Datei, die alle Novellenpfade enth�lt, ermittelt.
        /// Danach werden die Pfade geladen und, falls vorhanden, die einzelnen Novellen verarbeitet.
        /// </summary>
        private IEnumerator LoadAllNovelsWithTweeApproach()
        {
            // Wenn bereits Novellen geladen sind, wird die Coroutine beendet.
            if (KiteNovelManager.Instance().AreNovelsLoaded())
            {
                yield break;
            }

            // Ermittelt den vollst�ndigen Pfad zur Datei, die die Liste der Novellenpfade enth�lt.
            string fullPath = Path.Combine(Application.dataPath, NovelListPath);

            // Starte die Coroutine zum Laden der Novellenpfade und verarbeite diese anschlie�end.
            yield return StartCoroutine(LoadNovelPaths(fullPath, listOfAllNovelPaths =>
            {
                // Falls keine Novellenpfade gefunden wurden, wird eine Warnung ausgegeben und der Manager
                // mit einer leeren Liste initialisiert.
                if (listOfAllNovelPaths == null || listOfAllNovelPaths.Count == 0)
                {
                    Debug.LogWarning("Loading Novels failed: No Novels found! Path: " + fullPath);
                    KiteNovelManager.Instance().SetAllKiteNovels(new List<VisualNovel>());
                    return;
                }

                // Falls Novellenpfade vorhanden sind, starte die Verarbeitung der einzelnen Novellen.
                StartCoroutine(ProcessNovels(listOfAllNovelPaths));
            }));
        }

        /// <summary>
        /// �hnlich wie LoadAllNovelsWithTweeApproach, jedoch mit zus�tzlicher Logik zum selektiven �berschreiben alter Novellen.
        /// </summary>
        private IEnumerator LoadAllNovelsWithTweeApproachAndSelectiveOverrideOldNovels()
        {
            // Pr�ft, ob bereits Novellen geladen wurden.
            if (KiteNovelManager.Instance().AreNovelsLoaded())
            {
                yield break;
            }

            // Bestimmt den vollst�ndigen Pfad zur Liste der Novellenpfade.
            string fullPath = Path.Combine(Application.dataPath, NovelListPath);

            // Lade die Liste der Novellenpfade.
            yield return StartCoroutine(LoadNovelPaths(fullPath, listOfAllNovelPaths =>
            {
                if (listOfAllNovelPaths == null || listOfAllNovelPaths.Count == 0)
                {
                    Debug.LogWarning("Loading Novels failed: No Novels found! Path: " + fullPath);
                    KiteNovelManager.Instance().SetAllKiteNovels(new List<VisualNovel>());
                    return;
                }

                // Starte die Verarbeitung der Novellen mit selektivem �berschreiben.
                StartCoroutine(ProcessNovelsAndSelectiveOverrideOldNovels(listOfAllNovelPaths));
            }));
        }

        /// <summary>
        /// Verarbeitet alle Novellen, indem f�r jeden Novellenpfad
        /// die Metadaten und die Event-Liste geladen, deserialisiert und in einen KiteNovelFolder
        /// zusammengefasst werden.
        /// Anschlie�end werden alle Ordner in VisualNovel-Objekte konvertiert und als JSON gespeichert.
        /// </summary>
        private IEnumerator ProcessNovels(List<string> listOfAllNovelPaths)
        {
            // Liste zum Speichern aller verarbeiteten Novellenordner.
            List<KiteNovelFolder> allFolders = new List<KiteNovelFolder>();

            // Durchlaufe alle Novellenpfade.
            foreach (string pathOfNovel in listOfAllNovelPaths)
            {
                // Erzeuge den vollst�ndigen Pfad zur Metadaten-Datei der Novelle.
                string fullPathOfNovelMetaData = Path.Combine(Application.dataPath, pathOfNovel, MetaDataFileName);
                // Erzeuge den vollst�ndigen Pfad zur Event-Liste der Novelle.
                string fullPathOfNovelEventList = Path.Combine(Application.dataPath, pathOfNovel, EventListFileName);

                KiteNovelMetaData kiteNovelMetaData = null;
                string jsonStringOfEventList = null;

                // Lade und deserialisiere die Metadaten der Novelle.
                yield return StartCoroutine(LoadAndDeserialize<KiteNovelMetaData>(fullPathOfNovelMetaData, result => { kiteNovelMetaData = result; }));

                // Falls die Metadaten nicht geladen werden konnten, gebe eine Warnung aus und �berspringe diese Novelle.
                if (kiteNovelMetaData == null)
                {
                    Debug.LogWarning("Kite Novel Meta Data could not be loaded: " + pathOfNovel);
                    continue;
                }

                // Lade den Inhalt der Event-Liste.
                yield return StartCoroutine(LoadFileContent(fullPathOfNovelEventList, result => { jsonStringOfEventList = result; }));

                // Falls die Event-Liste leer ist, gebe eine Warnung aus und �berspringe diese Novelle.
                if (string.IsNullOrEmpty(jsonStringOfEventList))
                {
                    Debug.LogWarning("Kite Novel Event List could not be loaded: " + pathOfNovel);
                    continue;
                }

                // Ersetze bestimmte W�rter in der Event-Liste, basierend auf den in den Metadaten angegebenen Ersetzungen.

                // Why? No one knows...
                jsonStringOfEventList = ReplaceWordsInString(jsonStringOfEventList, kiteNovelMetaData.WordsToReplace);

                // Konvertiere den Text der Event-Liste in eine strukturierte Event-Liste.
                List<VisualNovelEvent> kiteNovelEventList = KiteNovelConverter.ConvertTextDocumentIntoEventList(jsonStringOfEventList, kiteNovelMetaData);

                // F�ge den aktuellen Novellenordner (bestehend aus Metadaten und Event-Liste) zur Gesamtliste hinzu.
                allFolders.Add(new KiteNovelFolder(kiteNovelMetaData, kiteNovelEventList));
            }

            // Konvertiere alle verarbeiteten Novellenordner in VisualNovel-Objekte.
            List<VisualNovel> visualNovels = KiteNovelConverter.ConvertFilesToNovels(allFolders);

            // Speichere die konvertierten Visual Novels als JSON-Datei.
            SaveToJson(new NovelListWrapper(visualNovels));
        }

        /// <summary>
        /// Verarbeitet alle Novellen �hnlich wie ProcessNovels, f�hrt aber zus�tzlich einen Vergleich
        /// mit bereits geladenen Novellen durch, um nur ge�nderte Novellen zu �berschreiben.
        /// </summary>
        private IEnumerator ProcessNovelsAndSelectiveOverrideOldNovels(List<string> listOfAllNovelPaths)
        {
            // Liste zur Speicherung aller verarbeiteten Novellenordner.
            List<KiteNovelFolder> allFolders = new List<KiteNovelFolder>();

            // Durchlaufe alle Novellenpfade.
            foreach (string pathOfNovel in listOfAllNovelPaths)
            {
                // Erstelle die vollst�ndigen Pfade zu den Metadaten- und Event-Listen-Dateien.
                string fullPathOfNovelMetaData = Path.Combine(Application.dataPath, pathOfNovel, MetaDataFileName);
                string fullPathOfNovelEventList = Path.Combine(Application.dataPath, pathOfNovel, EventListFileName);

                KiteNovelMetaData kiteNovelMetaData = null;
                string jsonStringOfEventList = null;

                // Lade und deserialisiere die Metadaten.
                yield return StartCoroutine(LoadAndDeserialize<KiteNovelMetaData>(fullPathOfNovelMetaData,
                    result => { kiteNovelMetaData = result; }));

                // Falls die Metadaten nicht geladen werden konnten, �berspringe diese Novelle.
                if (kiteNovelMetaData == null)
                {
                    Debug.LogWarning("Kite Novel Meta Data could not be loaded: " + pathOfNovel);
                    continue;
                }

                // Lade den Inhalt der Event-Liste.
                yield return StartCoroutine(LoadFileContent(fullPathOfNovelEventList, result => { jsonStringOfEventList = result; }));

                // Falls die Event-Liste leer ist, �berspringe diese Novelle.
                if (string.IsNullOrEmpty(jsonStringOfEventList))
                {
                    Debug.LogWarning("Kite Novel Event List could not be loaded: " + pathOfNovel);
                    continue;
                }

                // Ersetze W�rter in der Event-Liste anhand der in den Metadaten angegebenen Wortpaare.
                jsonStringOfEventList = ReplaceWordsInString(jsonStringOfEventList, kiteNovelMetaData.WordsToReplace);
                
                // Zwischenspeichern der in der Novel verwendeten Keywords.
                // Mit Mimiken / Audio Files / Biases

                // Konvertiere den Text der Event-Liste in eine strukturierte Event-Liste.
                List<VisualNovelEvent> kiteNovelEventList = KiteNovelConverter.ConvertTextDocumentIntoEventList(jsonStringOfEventList, kiteNovelMetaData);

                // F�ge den verarbeiteten Ordner zur Gesamtliste hinzu.
                allFolders.Add(new KiteNovelFolder(kiteNovelMetaData, kiteNovelEventList));
            }

            // Konvertiere alle Ordner in VisualNovel-Objekte.
            List<VisualNovel> visualNovels = KiteNovelConverter.ConvertFilesToNovels(allFolders);

            // Warte, bis der Manager mindestens eine Visual Novel geladen hat.
            List<VisualNovel> novels = KiteNovelManager.Instance().GetAllKiteNovels();
            while (novels.Count == 0)
            {
                yield return new WaitForSeconds(1);
            }

            // Erhalte die aktuell geladenen (alten) Novellen.
            List<VisualNovel> oldNovels = novels;

            // Erzeuge eine neue Liste, in die entweder das alte Novel oder ein neues, aktualisiertes Novel �bernommen wird.
            List<VisualNovel> modifiedListOfNovels = new List<VisualNovel>();

            // Vergleiche jedes alte Novel mit den neu geladenen Novellen.
            foreach (VisualNovel visualNovel in oldNovels)
            {
                VisualNovel novel = visualNovel;

                // Falls ein neues Novel mit derselben ID gefunden wird, wird das alte Novel durch das neue ersetzt.
                foreach (VisualNovel newNovel in visualNovels)
                {
                    if (newNovel.id == novel.id)
                    {
                        novel = newNovel;
                        Debug.Log("Override Novel : " + newNovel.title);
                        break;
                    }
                }

                // F�ge das (ggf. aktualisierte) Novel der neuen Liste hinzu.
                modifiedListOfNovels.Add(novel);
            }

            // Speichere die modifizierte Liste als JSON.
            SaveToJson(new NovelListWrapper(modifiedListOfNovels));
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
            string path = Path.Combine(Application.dataPath, "novels.json");
            // Schreibe den JSON-String in die Datei.
            File.WriteAllText(path, json);
            // Logge den erfolgreichen Abschluss mit dem Speicherpfad.
            Debug.Log("Visual Novels have been successfully converted to JSON format and saved under the following path: " + path);
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
        // Public field to specify the file path in the Inspector.
        // Example: "TestKeywords.txt" if the file is located in the Application.dataPath folder.
        public string filePath = "Assets/_novels_twee/Eltern/visual_novel_event_list.txt";

        /// <summary>
        /// Called when the script starts.
        /// Initiates the process of reading and processing the keyword file.
        /// </summary>
        public void Start()
        {
            // Start the coroutine that loads and processes the keywords from the file.
            StartCoroutine(ProcessKeywordFile());
        }

        /// <summary>
        /// Coroutine that reads the keyword file, processes each line to extract the keyword data,
        /// and outputs the extracted properties to the Unity console.
        /// </summary>
        private IEnumerator ProcessKeywordFile()
        {
            // Build the full file path by combining Application.dataPath with the filePath provided.
            // If your file is located elsewhere, adjust accordingly.
            string fullPath = Path.Combine(Application.dataPath, filePath);

            string fileContent = "";

            // Check if the file exists at the specified path.
            if (File.Exists(fullPath))
            {
                // Read the entire file content synchronously.
                fileContent = File.ReadAllText(fullPath);
            }
            else
            {
                Debug.LogError("Keyword file not found at: " + fullPath);
                yield break;
            }

            NovelKeywordParser.ParseKeywordsFromFile(fileContent);

            //// Split the file content into lines (removing empty entries).
            //string[] lines = fileContent.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);


            //// Process each line individually.
            //foreach (string line in lines)
            //{
            //    // Trim whitespace from the line.
            //    string trimmedLine = line.Trim();

            //    // Skip if the line is empty.
            //    if (string.IsNullOrEmpty(trimmedLine))
            //        continue;

            //    // Use the NovelKeywordParser to convert the line into a NovelKeywordModel.
            //    NovelKeywordModel model = NovelKeywordParser.ParseKeyword(trimmedLine);

            //    // Build an output string containing the parsed keyword properties.
            //    if (model != null)
            //    {
            //        string output = "Parsed Keyword: ";
            //        if (model.End.HasValue && model.End.Value)
            //            output += "[End] ";
            //        if (model.CharacterIndex.HasValue)
            //            output += $"CharacterIndex: {model.CharacterIndex.Value} ";
            //        if (!string.IsNullOrEmpty(model.Action))
            //            output += $"Action: {model.Action} ";
            //        if (!string.IsNullOrEmpty(model.FaceExpression))
            //            output += $"FaceExpression: {model.FaceExpression} ";
            //        if (!string.IsNullOrEmpty(model.Sound))
            //            output += $"Sound: {model.Sound} ";
            //        if (!string.IsNullOrEmpty(model.Bias))
            //            output += $"Bias: {model.Bias} ";

            //        // Output the result to the Unity Console.
            //        Debug.Log(output);
            //    }
            //    else
            //    {
            //        //Debug.Log("No keyword model parsed for line: " + trimmedLine);
            //    }

            //    // Yield return null to wait for the next frame (optional, for large files).
                yield return null;
            
        }
    }
}
