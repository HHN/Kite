using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets._Scripts.Managers;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets._Scripts.Novel.VisualNovelLoader
{
    /// <summary>
    /// NovelLoader is a MonoBehaviour that is responsible for loading novels (Visual Novels)
    /// from a JSON file and passing them to the KiteNovelManager.
    /// </summary>
    public class NovelLoader : MonoBehaviour
    {
        private const string NovelsPath = "novels.json";

        /// <summary>
        /// Initiates the process to load all novels from the JSON file
        /// and schedules the destruction of the NovelLoader object after a set duration.
        /// </summary>
        private void Start()
        {
            StartCoroutine(LoadAllNovelsFromJson());

            Destroy(gameObject, 5f);
        }

        /// <summary>
        /// Loads all novels from the JSON file specified in the configuration
        /// and assigns them to the KiteNovelManager for further management.
        /// </summary>
        /// <returns>Returns an IEnumerator to support coroutine execution.</returns>
        private IEnumerator LoadAllNovelsFromJson()
        {
            if (KiteNovelManager.Instance().AreNovelsLoaded())
            {
                yield break;
            }

            string fullPath = Path.Combine(Application.streamingAssetsPath, NovelsPath);

            yield return StartCoroutine(LoadNovels(fullPath, listOfAllNovel =>
            {
                if (listOfAllNovel != null && listOfAllNovel.Count != 0)
                {
                    KiteNovelManager.Instance().SetAllKiteNovels(listOfAllNovel);
                }
                else
                {
                    Debug.LogWarning("Loading Novels failed: No Novels found! Path: " + fullPath);
                }
            }));
        }

        /// <summary>
        /// Loads novels from the given file path and invokes the callback function
        /// with a list of VisualNovel objects upon completion.
        /// </summary>
        /// <param name="path">The path to the file containing the novel JSON data.</param>
        /// <param name="callback">A callback function to handle the list of VisualNovel objects after loading.</param>
        /// <returns>An IEnumerator allowing the operation to be executed asynchronously.</returns>
        private IEnumerator LoadNovels(string path, System.Action<List<VisualNovel>> callback)
        {
            yield return StartCoroutine(LoadFileContent(path, jsonString =>
            {
                if (string.IsNullOrEmpty(jsonString))
                {
                    callback(null);
                }
                else
                {
                    NovelListWrapper kiteNovelList = JsonConvert.DeserializeObject<NovelListWrapper>(jsonString);
                    callback(kiteNovelList?.VisualNovels);
                }
            }));
        }

        /// <summary>
        /// Reads the content of a file from the specified path and invokes the provided callback
        /// with a string representing the file's content.
        /// </summary>
        /// <param name="path">The path to the file from which content is to be read. It can support platform-specific file systems.</param>
        /// <param name="callback">The callback function that handles the file content as a string once loading is complete.</param>
        /// <returns>An IEnumerator to facilitate asynchronous loading of the file content.</returns>
        private IEnumerator LoadFileContent(string path, System.Action<string> callback)
        {
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

                    if (www.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
                    {
                        Debug.LogError($"Error loading file at {path}: {www.error}");
                        callback(null);
                    }
                    else
                    {
                        callback(www.downloadHandler.text);
                    }
                }
            }
        }
    }
}