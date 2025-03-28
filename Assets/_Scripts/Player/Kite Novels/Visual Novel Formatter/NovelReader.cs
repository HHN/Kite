using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets._Scripts.Managers;
using Assets._Scripts.Novel;
using Assets._Scripts.Player.Kite_Novels.Visual_Novel_Loader;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets._Scripts.Player.Kite_Novels.Visual_Novel_Formatter
{
    public class NovelReader : MonoBehaviour
    {
        private const string NovelListPath = "_novels_twee/list_of_novels.txt";
        private const string MetaDataFileName = "visual_novel_meta_data.txt";
        private const string EventListFileName = "visual_novel_event_list.txt";
        private bool _isFinished;

        public void ConvertNovelsFromTweeToJSON()
        {
            StartCoroutine(LoadAllNovelsWithTweeApproach());
        }

        public void ConvertNovelsFromTweeToJSONAndSelectiveOverrideOldNovels()
        {
            StartCoroutine(LoadAllNovelsWithTweeApproachAndSelectiveOverrideOldNovels());
        }

        public bool IsFinished()
        {
            return _isFinished;
        }

        private IEnumerator LoadAllNovelsWithTweeApproach()
        {
            if (KiteNovelManager.Instance().AreNovelsLoaded())
            {
                yield break;
            }

            string fullPath = Path.Combine(Application.dataPath, NovelListPath);

            yield return StartCoroutine(LoadNovelPaths(fullPath, listOfAllNovelPaths =>
            {
                if (listOfAllNovelPaths == null || listOfAllNovelPaths.Count == 0)
                {
                    Debug.LogWarning("Loading Novel failed: No Novel found! Path: " + fullPath);
                    KiteNovelManager.Instance().SetAllKiteNovels(new List<VisualNovel>());
                    return;
                }

                StartCoroutine(ProcessNovels(listOfAllNovelPaths));
            }));
        }

        private IEnumerator LoadAllNovelsWithTweeApproachAndSelectiveOverrideOldNovels()
        {
            if (KiteNovelManager.Instance().AreNovelsLoaded())
            {
                yield break;
            }

            string fullPath = Path.Combine(Application.dataPath, NovelListPath);

            yield return StartCoroutine(LoadNovelPaths(fullPath, listOfAllNovelPaths =>
            {
                if (listOfAllNovelPaths == null || listOfAllNovelPaths.Count == 0)
                {
                    Debug.LogWarning("Loading Novel failed: No Novel found! Path: " + fullPath);
                    KiteNovelManager.Instance().SetAllKiteNovels(new List<VisualNovel>());
                    return;
                }

                StartCoroutine(ProcessNovelsAndSelectiveOverrideOldNovels(listOfAllNovelPaths));
            }));
        }

        private IEnumerator ProcessNovels(List<string> listOfAllNovelPaths)
        {
            List<KiteNovelFolder> allFolders = new List<KiteNovelFolder>();

            foreach (string pathOfNovel in listOfAllNovelPaths)
            {
                string fullPathOfNovelMetaData = Path.Combine(Application.dataPath, pathOfNovel, MetaDataFileName);
                string fullPathOfNovelEventList = Path.Combine(Application.dataPath, pathOfNovel, EventListFileName);

                KiteNovelMetaData kiteNovelMetaData = null;
                string jsonStringOfEventList = null;

                yield return StartCoroutine(LoadAndDeserialize<KiteNovelMetaData>(fullPathOfNovelMetaData,
                    result => { kiteNovelMetaData = result; }));

                if (kiteNovelMetaData == null)
                {
                    Debug.LogWarning("Kite Novel Meta Data could not be loaded: " + pathOfNovel);
                    continue;
                }

                yield return StartCoroutine(LoadFileContent(fullPathOfNovelEventList,
                    result => { jsonStringOfEventList = result; }));

                if (string.IsNullOrEmpty(jsonStringOfEventList))
                {
                    Debug.LogWarning("Kite Novel Event List could not be loaded: " + pathOfNovel);
                    continue;
                }

                jsonStringOfEventList = ReplaceWordsInString(jsonStringOfEventList, kiteNovelMetaData.WordsToReplace);

                KiteNovelEventList kiteNovelEventList =
                    KiteNovelConverter.ConvertTextDocumentIntoEventList(jsonStringOfEventList, kiteNovelMetaData);

                allFolders.Add(new KiteNovelFolder(kiteNovelMetaData, kiteNovelEventList));
            }

            List<VisualNovel> visualNovels = KiteNovelConverter.ConvertFilesToNovels(allFolders);

            SaveToJson(new NovelListWrapper(visualNovels));
        }

        private IEnumerator ProcessNovelsAndSelectiveOverrideOldNovels(List<string> listOfAllNovelPaths)
        {
            List<KiteNovelFolder> allFolders = new List<KiteNovelFolder>();

            foreach (string pathOfNovel in listOfAllNovelPaths)
            {
                string fullPathOfNovelMetaData = Path.Combine(Application.dataPath, pathOfNovel, MetaDataFileName);
                string fullPathOfNovelEventList = Path.Combine(Application.dataPath, pathOfNovel, EventListFileName);

                KiteNovelMetaData kiteNovelMetaData = null;
                string jsonStringOfEventList = null;

                yield return StartCoroutine(LoadAndDeserialize<KiteNovelMetaData>(fullPathOfNovelMetaData,
                    result => { kiteNovelMetaData = result; }));

                if (kiteNovelMetaData == null)
                {
                    Debug.LogWarning("Kite Novel Meta Data could not be loaded: " + pathOfNovel);
                    continue;
                }

                yield return StartCoroutine(LoadFileContent(fullPathOfNovelEventList,
                    result => { jsonStringOfEventList = result; }));

                if (string.IsNullOrEmpty(jsonStringOfEventList))
                {
                    Debug.LogWarning("Kite Novel Event List could not be loaded: " + pathOfNovel);
                    continue;
                }

                jsonStringOfEventList = ReplaceWordsInString(jsonStringOfEventList, kiteNovelMetaData.WordsToReplace);

                KiteNovelEventList kiteNovelEventList =
                    KiteNovelConverter.ConvertTextDocumentIntoEventList(jsonStringOfEventList, kiteNovelMetaData);

                allFolders.Add(new KiteNovelFolder(kiteNovelMetaData, kiteNovelEventList));
            }

            List<VisualNovel> visualNovels = KiteNovelConverter.ConvertFilesToNovels(allFolders);

            while (KiteNovelManager.Instance().GetAllKiteNovels().Count == 0)
            {
                yield return new WaitForSeconds(1);
            }

            List<VisualNovel> oldNovels = KiteNovelManager.Instance().GetAllKiteNovels();

            List<VisualNovel> modifiedListOfNovels = new List<VisualNovel>();

            foreach (VisualNovel visualNovel in oldNovels)
            {
                VisualNovel novel = visualNovel;

                foreach (VisualNovel newNovel in visualNovels)
                {
                    if (newNovel.id == novel.id)
                    {
                        novel = newNovel;
                        Debug.Log("Override Novel : " + newNovel.title);
                        break;
                    }
                }

                modifiedListOfNovels.Add(novel);
            }

            SaveToJson(new NovelListWrapper(modifiedListOfNovels));
        }

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

        private IEnumerator LoadAndDeserialize<T>(string path, System.Action<T> callback)
        {
            yield return StartCoroutine(LoadFileContent(path, jsonString =>
            {
                if (string.IsNullOrEmpty(jsonString))
                {
                    callback(default);
                }
                else
                {
                    T result = JsonConvert.DeserializeObject<T>(jsonString);
                    callback(result);
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

        private string ReplaceWordsInString(string input, List<WordPair> wordsToReplace)
        {
            foreach (WordPair wordPair in wordsToReplace)
            {
                if (wordPair != null && !string.IsNullOrEmpty(wordPair.WordToReplace) &&
                    !string.IsNullOrEmpty(wordPair.ReplaceByValue))
                {
                    input = input.Replace(wordPair.WordToReplace, wordPair.ReplaceByValue);
                }
            }

            return input;
        }

        private void SaveToJson(NovelListWrapper novelListWrapper)
        {
            string json = JsonUtility.ToJson(novelListWrapper, true);
            string path = Path.Combine(Application.dataPath, "novels.json");
            File.WriteAllText(path, json);
            Debug.Log(
                "Visual Novel have been successfully converted to JSON format and saved under the following path: " +
                path);
            _isFinished = true;
        }
    }
}