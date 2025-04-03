using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets._Scripts.Managers;
using Assets._Scripts.Novel;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets._Scripts.Player.KiteNovels.Visual_Novel_Loader
{
    public class NovelLoader : MonoBehaviour
    {
        private const string NOVELS_PATH = "novels.json";

        private void Start()
        {
            StartCoroutine(LoadAllNovelsFromJson());
        }

        private IEnumerator LoadAllNovelsFromJson()
        {
            if (KiteNovelManager.Instance().AreNovelsLoaded())
            {
                yield break;
            }
            string fullPath = Path.Combine(Application.streamingAssetsPath, NOVELS_PATH);

            yield return StartCoroutine(LoadNovels(fullPath, listOfAllNovel =>
            {
                if (listOfAllNovel == null || listOfAllNovel.Count == 0)
                {
                    Debug.LogWarning("Loading Novels failed: No Novels found! Path: " + fullPath);
                }
                KiteNovelManager.Instance().SetAllKiteNovels(listOfAllNovel);
            }));
        }

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

                    if ((www.result == UnityWebRequest.Result.ConnectionError) || (www.result == UnityWebRequest.Result.ProtocolError))
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
