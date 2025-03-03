using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets._Scripts.Managers;
using Assets._Scripts.Player;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets._Scripts.OfflineAiFeedback
{
    public class OfflineFeedbackLoader : MonoBehaviour
    {
        private static Dictionary<VisualNovelNames, string> FEEDBACK_PATHS = new Dictionary<VisualNovelNames, string>()
        {
            { VisualNovelNames.BANK_KREDIT_NOVEL, "bank_kredit_feedback.json" },
            { VisualNovelNames.BEKANNTEN_TREFFEN_NOVEL, "bekannte_treffen_feedback.json" },
            { VisualNovelNames.BANK_KONTO_NOVEL, "bank_konto_feedback.json" },
            { VisualNovelNames.FOERDERANTRAG_NOVEL, "foerderantrag_feedback.json" },
            { VisualNovelNames.ELTERN_NOVEL, "eltern_feedback.json" },
            { VisualNovelNames.NOTARIAT_NOVEL, "notariat_feedback.json" },
            { VisualNovelNames.PRESSE_NOVEL, "presse_feedback.json" },
            { VisualNovelNames.BUERO_NOVEL, "buero_feedback.json" },
            { VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL, "gruender_zuschuss_feedback.json" },
            { VisualNovelNames.HONORAR_NOVEL, "honorar_feedback.json" },
            { VisualNovelNames.LEBENSPARTNER_NOVEL, "lebenspartner_feedback.json" },
            { VisualNovelNames.INTRO_NOVEL, "intro_feedback.json" }
        };


        public void LoadOfflineFeedbackForNovel(VisualNovelNames visualNovel)
        {
            StartCoroutine(LoadOfflineFeedbackForNovelFromJson(visualNovel));
        }

        public void SaveOfflineFeedbackForNovelInEditMode(VisualNovelNames visualNovel,
            FeedbackNodeList feedbackNodeList)
        {
            StartCoroutine(SaveOfflineFeedbackForNovelToJsonInEditMode(visualNovel, feedbackNodeList));
        }

        private IEnumerator SaveOfflineFeedbackForNovelToJsonInEditMode(VisualNovelNames visualNovel,
            FeedbackNodeList feedbackNodeList)
        {
            string json = JsonUtility.ToJson(feedbackNodeList, true);
            string path = Path.Combine(Application.dataPath, FEEDBACK_PATHS[visualNovel]);
            File.WriteAllText(path, json);
            Debug.Log("FeedbackNodes have been successfully saved under the following path: " + path);
            yield break;
        }

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