using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets._Scripts.Managers;
using Assets._Scripts.Novel.VisualNovelLoader;
using Assets._Scripts.Utilities;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets._Scripts.Novel.VisualNovelFormatter
{
    /// <summary>
    /// Manages the loading, processing, and conversion of visual novels from the Twee format to JSON.
    /// Implements a singleton pattern for global access and ensures the object persists across scenes.
    /// </summary>
    public class NovelReader : MonoBehaviour
    {
        // Constants defining the file paths
        private const string NovelListPath = "_novels_twee/list_of_novels.txt"; // Path to file containing a list of all novel directories
        private const string MetaDataFileName = "visual_novel_meta_data.txt";   // File containing metadata of a novel
        private const string EventListFileName = "visual_novel_event_list.txt"; // File containing the list of events for a novel

        private bool _isFinished; // Flag to indicate whether the import process has finished

        private static NovelReader _instance;

        /// <summary>
        /// Provides a singleton instance of the <see cref="NovelReader"/> class for global access.
        /// Ensures only one instance of the class exists during the application's lifetime.
        /// </summary>
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

        /// <summary>
        /// Initiates the import process for all visual novels using the Twee format.
        /// The import operates asynchronously and converts the novel data into JSON format.
        /// The resulting novels.json file is always fully rewritten.
        /// </summary>
        public void ImportNovel()
        {
            // Reset the finished flag so the TestRunner can reliably wait on this run.
            _isFinished = false;
            StartCoroutine(ImportNovelWithTweeApproach());
        }

        /// <summary>
        /// Indicates whether the import process for the visual novels has been completed.
        /// Returns true if the process has finished, otherwise false.
        /// </summary>
        public bool IsFinished()
        {
            return _isFinished;
        }

        /// <summary>
        /// Executes the import process for visual novels using the Twee format.
        /// It reads the novel paths, processes each novel and finally
        /// rewrites the novels.json file in StreamingAssets.
        /// </summary>
        private IEnumerator ImportNovelWithTweeApproach()
        {
            string dataPath = Application.dataPath;
            string fullPath = Path.Combine(dataPath, NovelListPath);

            List<string> listOfAllNovelPaths = null;

            // Load the list of novel paths from list_of_novels.txt
            yield return StartCoroutine(LoadNovelPaths(fullPath, paths =>
            {
                listOfAllNovelPaths = paths;
            }));

            // If no novels are found, clear everything and still write an empty novels.json
            if (listOfAllNovelPaths == null || listOfAllNovelPaths.Count == 0)
            {
                LogManager.Warning($"Loading novels failed: no novels found! Path: {fullPath}");

                // Clear manager and write an empty JSON file so that the file exists.
                KiteNovelManager.Instance().SetAllKiteNovels(new List<VisualNovel>());
                SaveToJson(new NovelListWrapper()); // writes an empty list into novels.json
                yield break;
            }

            // Process all novels and always overwrite the JSON file with fresh data
            yield return StartCoroutine(ProcessAndRewriteNovels(listOfAllNovelPaths));
        }

        /// <summary>
        /// Processes all novels (metadata + event list), converts them into VisualNovel objects
        /// and rewrites the novels.json file with the new list (no merging with existing data).
        /// </summary>
        private IEnumerator ProcessAndRewriteNovels(List<string> listOfAllNovelPaths)
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

            // Update the in-memory manager (optional but useful for runtime/tests).
            KiteNovelManager.Instance().SetAllKiteNovels(visualNovels);

            // Always overwrite novels.json with the newly generated list.
            SaveToJson(new NovelListWrapper(visualNovels));
        }

        /// <summary>
        /// Loads a single visual novel's metadata and event list, processes replacements,
        /// and adds it to the list of novel folders.
        /// </summary>
        /// <param name="pathOfNovel">Relative path to the novel's folder inside the project.</param>
        /// <param name="allFolders">The list to which the processed novel will be added.</param>
        private IEnumerator ProcessSingleNovel(string pathOfNovel, List<KiteNovelFolder> allFolders)
        {
            // Build the full paths for metadata and event list files.
            string fullPathOfNovelMetaData = Path.Combine(Application.dataPath, pathOfNovel, MetaDataFileName);
            string fullPathOfNovelEventList = Path.Combine(Application.dataPath, pathOfNovel, EventListFileName);

            KiteNovelMetaData kiteNovelMetaData = null;
            string jsonStringOfEventList = null;

            // Load and deserialize the novel's metadata.
            yield return StartCoroutine(LoadAndDeserialize<KiteNovelMetaData>(fullPathOfNovelMetaData, result => { kiteNovelMetaData = result; }));

            // Skip if metadata could not be loaded.
            if (kiteNovelMetaData == null)
            {
                LogManager.Warning($"Kite Novel Meta Data could not be loaded: {pathOfNovel}");
                yield break;
            }

            // Load the event list content.
            yield return StartCoroutine(LoadFileContent(fullPathOfNovelEventList, result => { jsonStringOfEventList = result; }));

            // Skip if event list is empty or missing.
            if (string.IsNullOrEmpty(jsonStringOfEventList))
            {
                LogManager.Warning($"Kite Novel Event List could not be loaded: {pathOfNovel}");
                yield break;
            }

            // Convert the raw text into a structured list of visual novel events.
            List<VisualNovelEvent> kiteNovelEventList = KiteNovelConverter.ConvertTextDocumentIntoEventList(jsonStringOfEventList, kiteNovelMetaData);

            // Add the processed folder to the result list.
            allFolders.Add(new KiteNovelFolder(kiteNovelMetaData, kiteNovelEventList));
        }

        /// <summary>
        /// Loads the file paths of novels from the specified path and invokes a callback with the list of novel paths.
        /// Handles deserialization of data and manages asynchronous operations within Unity's coroutine system.
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
        /// Loads the content of a file at the specified path and deserializes it into an object of type T.
        /// </summary>
        private IEnumerator LoadAndDeserialize<T>(string path, System.Action<T> callback)
        {
            yield return LoadFileContent(path, jsonString =>
            {
                callback(string.IsNullOrEmpty(jsonString) ? default : JsonConvert.DeserializeObject<T>(jsonString));
            });
        }

        /// <summary>
        /// Reads the content of a file from the specified path and provides it to the provided callback.
        /// The method handles differences between platforms and loads the content accordingly.
        /// </summary>
        private IEnumerator LoadFileContent(string path, System.Action<string> callback)
        {
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
                        LogManager.Error($"Error loading file at {path}: {www.error}");
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
        /// Converts a list of visual novels encapsulated in a NovelListWrapper object into JSON format
        /// and saves it to a predefined location in the StreamingAssets directory.
        /// Marks the process as finished upon successful execution.
        /// If the StreamingAssets folder or the JSON file does not exist, they will be created.
        /// </summary>
        private void SaveToJson(NovelListWrapper novelListWrapper)
        {
            if (novelListWrapper == null)
            {
                novelListWrapper = new NovelListWrapper();
            }

            string json = JsonUtility.ToJson(novelListWrapper, true);

            // Ensure StreamingAssets directory exists
            string directory = Path.Combine(Application.dataPath, "StreamingAssets");
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string path = Path.Combine(directory, "novels.json");
            File.WriteAllText(path, json);

            _isFinished = true;
        }
    }
}
