using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets._Scripts.Managers;
using Assets._Scripts.Novel;
using Assets._Scripts.Player;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets._Scripts.OfflineAiFeedback
{
    /// <summary>
    /// Manages the loading and saving of pre-generated offline AI feedback for various visual novels.
    /// This class handles file I/O operations for JSON feedback data, distinguishing between
    /// runtime loading (from StreamingAssets) and editor-mode operations (from DataPath).
    /// </summary>
    public class OfflineFeedbackLoader : MonoBehaviour
    {
        private static Dictionary<VisualNovelNames, string> FEEDBACK_PATHS = new Dictionary<VisualNovelNames, string>()
        {
            { VisualNovelNames.BankKreditNovel, "bank_kredit_feedback.json" },
            { VisualNovelNames.InvestorNovel, "investor_feedback.json" },
            { VisualNovelNames.ElternNovel, "eltern_feedback.json" },
            { VisualNovelNames.NotariatNovel, "notariat_feedback.json" },
            { VisualNovelNames.PresseNovel, "presse_feedback.json" },
            { VisualNovelNames.VermieterNovel, "buero_feedback.json" },
            { VisualNovelNames.HonorarNovel, "honorar_feedback.json" },
            { VisualNovelNames.EinstiegsNovel, "intro_feedback.json" }
        };

        /// <summary>
        /// Initiates the asynchronous loading of offline feedback for a specific visual novel from JSON.
        /// This method is intended for runtime use, loading from StreamingAssets.
        /// </summary>
        /// <param name="visualNovel">The <see cref="VisualNovelNames"/> enum value indicating which novel's feedback to load.</param>
        public void LoadOfflineFeedbackForNovel(VisualNovelNames visualNovel)
        {
            StartCoroutine(LoadOfflineFeedbackForNovelFromJson(visualNovel));
        }

        /// <summary>
        /// Initiates the asynchronous saving of offline feedback for a specific visual novel to JSON.
        /// This method is designed for use within the Unity editor to save generated feedback data.
        /// </summary>
        /// <param name="visualNovel">The <see cref="VisualNovelNames"/> enum value indicating which novel's feedback to save.</param>
        /// <param name="feedbackNodeList">The <see cref="FeedbackNodeList"/> containing the feedback data to save.</param>
        public void SaveOfflineFeedbackForNovelInEditMode(VisualNovelNames visualNovel,
            FeedbackNodeList feedbackNodeList)
        {
            StartCoroutine(SaveOfflineFeedbackForNovelToJsonInEditMode(visualNovel, feedbackNodeList));
        }

        /// <summary>
        /// Coroutine to asynchronously save offline feedback data for a visual novel to a JSON file.
        /// This method writes to <see cref="Application.dataPath"/> and is intended for editor use.
        /// </summary>
        /// <param name="visualNovel">The <see cref="VisualNovelNames"/> enum value specifying the novel.</param>
        /// <param name="feedbackNodeList">The list of feedback nodes to be serialized and saved.</param>
        /// <returns>An <see cref="IEnumerator"/> for the coroutine execution.</returns>
        private IEnumerator SaveOfflineFeedbackForNovelToJsonInEditMode(VisualNovelNames visualNovel,
            FeedbackNodeList feedbackNodeList)
        {
            string json = JsonUtility.ToJson(feedbackNodeList, true);
            string path = Path.Combine(Application.dataPath, FEEDBACK_PATHS[visualNovel]);
            File.WriteAllText(path, json);
            Debug.Log("FeedbackNodes have been successfully saved under the following path: " + path);
            yield break;
        }

        /// <summary>
        /// Coroutine to asynchronously load offline feedback for a visual novel from a JSON file.
        /// This method loads from <see cref="Application.streamingAssetsPath"/> and is intended for runtime use.
        /// If feedback is already loaded for the given novel, the operation is skipped.
        /// </summary>
        /// <param name="visualNovel">The <see cref="VisualNovelNames"/> enum value specifying the novel.</param>
        /// <returns>An <see cref="IEnumerator"/> for the coroutine execution.</returns>
        private IEnumerator LoadOfflineFeedbackForNovelFromJson(VisualNovelNames visualNovel)
        {
            if (PreGeneratedOfflineFeedbackManager.Instance().IsFeedbackLoaded(visualNovel))
            {
                yield break;
            }

            string fullPath = Path.Combine(Application.streamingAssetsPath, FEEDBACK_PATHS[visualNovel]);

            yield return StartCoroutine(LoadFeedback(fullPath, listOfFeedbackNodes =>
            {
                if (listOfFeedbackNodes == null || listOfFeedbackNodes.Count == 0)
                {
                    Debug.LogWarning("Loading Offline Feedback failed: No Offline Feedback found! Path: " + fullPath);
                }

                Dictionary<string, FeedbackNodeContainer> feedback = new Dictionary<string, FeedbackNodeContainer>();

                foreach (FeedbackNodeContainer node in listOfFeedbackNodes)
                {
                    feedback.Add(node.path, node);
                }

                PreGeneratedOfflineFeedbackManager.Instance().SetPreGeneratedOfflineFeedback(visualNovel, feedback);
            }));
        }

        /// <summary>
        /// Coroutine to asynchronously load offline feedback for a visual novel from a JSON file in edit mode.
        /// This method loads from <see cref="Application.dataPath"/> and is intended for use within the Unity editor.
        /// If feedback is already loaded for the given novel, the operation is skipped.
        /// </summary>
        /// <param name="visualNovel">The <see cref="VisualNovelNames"/> enum value specifying the novel.</param>
        /// <returns>An <see cref="IEnumerator"/> for the coroutine execution.</returns>
        public IEnumerator LoadOfflineFeedbackForNovelFromJsonInEditMode(VisualNovelNames visualNovel)
        {
            if (PreGeneratedOfflineFeedbackManager.Instance().IsFeedbackLoaded(visualNovel))
            {
                yield break;
            }

            string fullPath = Path.Combine(Application.dataPath, FEEDBACK_PATHS[visualNovel]);

            yield return StartCoroutine(LoadFeedbackInEditMode(fullPath, listOfFeedbackNodes =>
            {
                if (listOfFeedbackNodes == null || listOfFeedbackNodes.Count == 0)
                {
                    return;
                }

                Dictionary<string, FeedbackNodeContainer> feedback = new Dictionary<string, FeedbackNodeContainer>();
                foreach (FeedbackNodeContainer node in listOfFeedbackNodes)
                {
                    feedback.Add(node.path, node);
                }

                PreGeneratedOfflineFeedbackManager.Instance().SetPreGeneratedOfflineFeedback(visualNovel, feedback);
            }));
        }

        /// <summary>
        /// Coroutine to load feedback data from a JSON string using Newtonsoft.Json.
        /// This method is called by runtime loading operations (using UnityWebRequest).
        /// </summary>
        /// <param name="path">The full path to the JSON file.</param>
        /// <param name="callback">A callback action that receives the list of <see cref="FeedbackNodeContainer"/> objects, or null if loading fails.</param>
        /// <returns>An <see cref="IEnumerator"/> for the coroutine execution.</returns>
        private IEnumerator LoadFeedback(string path, System.Action<List<FeedbackNodeContainer>> callback)
        {
            yield return StartCoroutine(LoadFileContent(path, jsonString =>
            {
                if (string.IsNullOrEmpty(jsonString))
                {
                    callback(null);
                }
                else
                {
                    FeedbackNodeList feedbackNodeList = JsonConvert.DeserializeObject<FeedbackNodeList>(jsonString);
                    callback(feedbackNodeList?.feedbackNodes);
                }
            }));
        }

        /// <summary>
        /// Coroutine to load feedback data from a JSON string using Newtonsoft.Json, specifically for edit mode.
        /// This method is called by editor-specific loading operations (using UnityWebRequest in some platforms).
        /// </summary>
        /// <param name="path">The full path to the JSON file.</param>
        /// <param name="callback">A callback action that receives the list of <see cref="FeedbackNodeContainer"/> objects, or null if loading fails.</param>
        /// <returns>An <see cref="IEnumerator"/> for the coroutine execution.</returns>
        private IEnumerator LoadFeedbackInEditMode(string path, System.Action<List<FeedbackNodeContainer>> callback)
        {
            yield return StartCoroutine(LoadFileContentInEditMode(path, jsonString =>
            {
                if (string.IsNullOrEmpty(jsonString))
                {
                    callback(null);
                }
                else
                {
                    FeedbackNodeList feedbackNodeList = JsonConvert.DeserializeObject<FeedbackNodeList>(jsonString);
                    callback(feedbackNodeList?.feedbackNodes);
                }
            }));
        }

        /// <summary>
        /// Coroutine to load file content from a given path.
        /// Handles platform-specific loading (File.ReadAllText for iOS, UnityWebRequest for others).
        /// This method is used for runtime loading from StreamingAssets.
        /// </summary>
        /// <param name="path">The full path to the file to load.</param>
        /// <param name="callback">A callback action that receives the file content as a string, or null on error.</param>
        /// <returns>An <see cref="IEnumerator"/> for the coroutine execution.</returns>
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

                    if ((www.result == UnityWebRequest.Result.ConnectionError) ||
                        (www.result == UnityWebRequest.Result.ProtocolError))
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

        /// <summary>
        /// Coroutine to load file content from a given path specifically for edit mode.
        /// Handles platform-specific loading (File.ReadAllText for iOS, UnityWebRequest for others).
        /// This method is primarily used for loading data from Application.dataPath in the editor.
        /// </summary>
        /// <param name="path">The full path to the file to load.</param>
        /// <param name="callback">A callback action that receives the file content as a string, or null on error.</param>
        /// <returns>An <see cref="IEnumerator"/> for the coroutine execution.</returns>
        private IEnumerator LoadFileContentInEditMode(string path, System.Action<string> callback)
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

                    if ((www.result == UnityWebRequest.Result.ConnectionError) ||
                        (www.result == UnityWebRequest.Result.ProtocolError))
                    {
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