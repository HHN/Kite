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
        private const string MetaDataFileName = "visual_novel_meta_data.txt"; // File containing metadata of a novel
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
        /// Initiates the import process for a visual novel using the Twee format.
        /// The import operates asynchronously and converts the novel data into JSON format.
        /// </summary>
        public void ImportNovel()
        {
            StartCoroutine(ImportNovelWithTweeApproach());
        }

        /// <summary>
        /// Indicates whether the import process for the visual novel has been completed.
        /// Returns true if the process has finished, otherwise false.
        /// </summary>
        /// <returns>True if the import process is complete, false otherwise.</returns>
        public bool IsFinished()
        {
            return _isFinished;
        }

        /// <summary>
        /// Executes the import process for visual novels using the Twee format.
        /// The method reads novel data, processes it asynchronously, and merges the data into JSON format.
        /// </summary>
        /// <returns>Coroutine indicating the progress and completion status of the import process.</returns>
        private IEnumerator ImportNovelWithTweeApproach()
        {
            string dataPath = Application.dataPath;
            string fullPath = Path.Combine(dataPath, NovelListPath);

            yield return StartCoroutine(LoadNovelPaths(fullPath, listOfAllNovelPaths =>
            {
                if (listOfAllNovelPaths == null || listOfAllNovelPaths.Count == 0)
                {
                    LogManager.Warning($"Loading Novels failed: No Novels found! Path: {fullPath}");

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
        /// <param name="path">The path to the file that contains the list of novel paths.</param>
        /// <param name="callback">The callback function to receive the list of novel paths as a List of strings. The callback is invoked with null if an error occurs or no paths are found.</param>
        /// <returns>An IEnumerator to facilitate asynchronous loading of the novel paths in Unity's coroutine system.</returns>
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
        /// The deserialization process uses JSON formatting to reconstruct the object from the file data.
        /// </summary>
        /// <param name="path">The file path from which the content will be loaded.</param>
        /// <param name="callback">A callback that receives the deserialized object of type T. If deserialization fails, the callback receives the default value for type T.</param>
        /// <typeparam name="T">The type of object into which the file content will be deserialized.</typeparam>
        /// <returns>An IEnumerator used to control the asynchronous loading and deserialization process.</returns>
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
        /// <param name="path">The path to the file to be loaded.</param>
        /// <param name="callback">The callback function to receive the loaded file content as a string.</param>
        /// <returns>An IEnumerator to allow asynchronous file loading in Unity's coroutine system.</returns>
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
        /// </summary>
        /// <param name="novelListWrapper">A wrapper object containing the list of visual novels to be serialized and saved.</param>
        private void SaveToJson(NovelListWrapper novelListWrapper)
        {
            string json = JsonUtility.ToJson(novelListWrapper, true);
            string path = Path.Combine(Application.dataPath, "StreamingAssets/novels.json");
            File.WriteAllText(path, json);
            LogManager.Info($"Visual Novels have been successfully converted to JSON format and saved under the following path: {path}");
            _isFinished = true;
        }
    }
}