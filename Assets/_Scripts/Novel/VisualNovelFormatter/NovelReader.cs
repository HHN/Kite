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

namespace Assets._Scripts.Novel.VisualNovelFormatter
{
    // The NovelReader class is a MonoBehaviour that manages loading, processing, and converting visual novels from the Twee format to JSON.
    public class NovelReader : MonoBehaviour
    {
        // Constants defining the file paths
        private const string NovelListPath = "_novels_twee/list_of_novels.txt"; // Path to file containing list of all novel directories
        private const string MetaDataFileName = "visual_novel_meta_data.txt"; // File containing metadata of a novel
        private const string EventListFileName = "visual_novel_event_list.txt"; // File containing the list of events for a novel

        private bool _isFinished; // Flag to indicate whether the import process has finished
        
        private static NovelReader _instance;

        public static NovelReader Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject("NovelReader");
                    _instance = go.AddComponent<NovelReader>();
                    DontDestroyOnLoad(go);
                }

                return _instance;
            }
        }

        public void ImportNovel()
        {
            StartCoroutine(ImportNovelWithTweeApproach());
        }

        public bool IsFinished()
        {
            return _isFinished;
        }

        private IEnumerator ImportNovelWithTweeApproach()
        {
            string dataPath = Application.dataPath;
            string fullPath = Path.Combine(dataPath, NovelListPath);

            yield return StartCoroutine(LoadNovelPaths(fullPath, listOfAllNovelPaths =>
            {
                if (listOfAllNovelPaths == null || listOfAllNovelPaths.Count == 0)
                {
                    Log($"Loading Novels failed: No Novels found! Path: {fullPath}", LogType.Warning);

                    KiteNovelManager.Instance().SetAllKiteNovels(new List<VisualNovel>());
                    return;
                }

                StartCoroutine(ProcessAndMergeNovels(listOfAllNovelPaths));
            }));
        }

        /// <summary>
        /// Processes and merges visual novels by converting from Twee to VisualNovel format.
        /// Updates existing ones and adds new ones based on ID comparison.
        /// Saves the final list as JSON.
        /// </summary>
        private IEnumerator ProcessAndMergeNovels(List<string> listOfAllNovelPaths)
        {
            Debug.Log("Processing and merging visual novels...");
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
            while (existingNovels.Count == 0)
            {
                yield return new WaitForSeconds(1);
            }

            // If no novels exist, save the entire new list.
            if (existingNovels.Count == 0)
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

                    modifiedListOfNovels.Add(updatedNovel);
                }

                // Add any entirely new novels that weren't already included.
                foreach (VisualNovel newNovel in visualNovels)
                {
                    if (modifiedListOfNovels.All(n => n.id != newNovel.id))
                    {
                        modifiedListOfNovels.Add(newNovel);
                    }
                }

                // Save the merged result to JSON.
                SaveToJson(new NovelListWrapper(modifiedListOfNovels));
            }
        }

        /// <summary>
        /// Loads a single visual novel's metadata and event list, processes replacements,
        /// and adds it to the list of novel folders.
        /// </summary>
        /// <param name="pathOfNovel">Relative path to the novel's folder inside the project.</param>
        /// <param name="allFolders">The list to which the processed novel will be added.</param>
        /// <returns>IEnumerator for coroutine execution.</returns>
        private IEnumerator ProcessSingleNovel(string pathOfNovel, List<KiteNovelFolder> allFolders)
        {
            Debug.Log("Processing single novel...");
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
                Log($"Kite Novel Meta Data could not be loaded: {pathOfNovel}", LogType.Warning);

                yield break;
            }

            // Load the event list content.
            yield return StartCoroutine(LoadFileContent(fullPathOfNovelEventList, result => { jsonStringOfEventList = result; }));

            // Skip if event list is empty or missing.
            if (string.IsNullOrEmpty(jsonStringOfEventList))
            {
                Log($"Kite Novel Event List could not be loaded: {pathOfNovel}", LogType.Warning);

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
        /// Loads the list of visual novel paths and invokes the callback with the result.
        /// </summary>
        private IEnumerator LoadNovelPaths(string path, System.Action<List<string>> callback)
        {
            yield return StartCoroutine(LoadFileContent(path, jsonString =>
            {
                if (string.IsNullOrEmpty(jsonString))
                {
                    callback(null);
                }
                else
                {
                    KiteNovelList kiteNovelList = JsonConvert.DeserializeObject<KiteNovelList>(jsonString);
                    callback(kiteNovelList?.VisualNovels);
                }
            }));
        }

        /// <summary>
        /// Loads and deserializes a file's content into a specific type T.
        /// </summary>
        private IEnumerator LoadAndDeserialize<T>(string path, System.Action<T> callback)
        {
            Debug.Log("Loading and deserializing file...");
            yield return LoadFileContent(path, jsonString =>
            {
                callback(string.IsNullOrEmpty(jsonString) ? default : JsonConvert.DeserializeObject<T>(jsonString));
            });
        }

        /// <summary>
        /// Loads a file's content. Uses File.ReadAllText on iOS, UnityWebRequest otherwise.
        /// </summary>
        private IEnumerator LoadFileContent(string path, System.Action<string> callback)
        {
            Debug.Log("Loading file content...");
            // May be refactored in the future, but for now, we need to ensure the file is in the correct location.
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                string jsonString = File.ReadAllText(path);
                callback(jsonString);
            }
            else
            {
                using (UnityWebRequest www = UnityWebRequest.Get(path))
                {
                    yield return www.SendWebRequest();

                    if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
                    {
                        Log($"Error loading file at {path}: {www.error}", LogType.Error);
                        callback(null);
                    }
                    else
                    {
                        callback(www.downloadHandler.text);
                    }
                }
            }
        }

        /// <summary>
        /// Replaces all occurrences of words defined in WordPairs with replacement values.
        /// </summary>
        private string ReplaceWordsInString(string input, List<WordPair> wordsToReplace)
        {
            Debug.Log("Replacing words in string...");
            if (wordsToReplace == null || wordsToReplace.Count == 0) return input;

            string result = input;
            
            foreach (var word in wordsToReplace)
            {
                if (!string.IsNullOrWhiteSpace(word?.WordToReplace) && !string.IsNullOrWhiteSpace(word.ReplaceByValue))
                {
                    result = result.Replace(word.WordToReplace, word.ReplaceByValue);
                }
            }
            return result;
        }

        /// <summary>
        /// Converts the novel list to JSON and writes it to a file.
        /// </summary>
        private void SaveToJson(NovelListWrapper novelListWrapper)
        {
            Debug.Log("Saving novel list to JSON...");
            string json = JsonUtility.ToJson(novelListWrapper, true);
            string path = Path.Combine(Application.dataPath, "StreamingAssets/novels.json");
            File.WriteAllText(path, json);
            Log($"Visual Novels have been successfully converted to JSON format and saved under the following path: {path}");
            _isFinished = true;
        }
        
        private void Log(string message, LogType type = LogType.Log)
        {
            switch (type)
            {
                case LogType.Warning:
                    Debug.LogWarning(message);
                    break;
                case LogType.Error:
                    Debug.LogError(message);
                    break;
                default:
                    Debug.Log(message);
                    break;
            }
        }

    }
}