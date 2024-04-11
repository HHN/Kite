using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class NovelLoader : MonoBehaviour 
{
    private const string NovelListPath = "novels_twee/list_of_novels.txt";
    private const string MetaDataFileName = "visual_novel_meta_data.txt";
    private const string EventListFileName = "visual_novel_event_list.txt";

    void Start()
    {
        StartCoroutine(LoadAllNovelsWithTweeApproach());
    }

    private IEnumerator LoadAllNovelsWithTweeApproach()
    {
        if (KiteNovelManager.Instance().AreNovelsLoaded())
        {
            yield break;
        }
        string fullPath = Path.Combine(Application.streamingAssetsPath, NovelListPath);

        yield return StartCoroutine(LoadNovelPaths(fullPath, listOfAllNovelPaths =>
        {
            if (listOfAllNovelPaths == null || listOfAllNovelPaths.Count == 0)
            {
                Debug.LogWarning("Loading Novels failed: No Novels found!");
                KiteNovelManager.Instance().SetAllKiteNovels(new List<VisualNovel>());
                return;
            }

            StartCoroutine(ProcessNovels(listOfAllNovelPaths));
        }));
    }

    private IEnumerator ProcessNovels(List<string> listOfAllNovelPaths)
    {
        List<KiteNovelFolder> allFolders = new List<KiteNovelFolder>();

        foreach (string pathOfNovel in listOfAllNovelPaths)
        {
            string fullPathOfNovelMetaData = Path.Combine(Application.streamingAssetsPath, pathOfNovel, MetaDataFileName);
            string fullPathOfNovelEventList = Path.Combine(Application.streamingAssetsPath, pathOfNovel, EventListFileName);

            KiteNovelMetaData kiteNovelMetaData = null;
            string jsonStringOfEventList = null;

            yield return StartCoroutine(LoadAndDeserialize<KiteNovelMetaData>(fullPathOfNovelMetaData, result =>
            {
                kiteNovelMetaData = result;
            }));

            if (kiteNovelMetaData == null)
            {
                Debug.LogWarning("Kite Novel Meta Data could not be loaded: " + pathOfNovel);
                continue;
            }
            yield return StartCoroutine(LoadFileContent(fullPathOfNovelEventList, result =>
            {
                jsonStringOfEventList = result;
            }));

            if (string.IsNullOrEmpty(jsonStringOfEventList))
            {
                Debug.LogWarning("Kite Novel Event List could not be loaded: " + pathOfNovel);
                continue;
            }

            jsonStringOfEventList = ReplaceWordsInString(jsonStringOfEventList, kiteNovelMetaData.WordsToReplace);

            KiteNovelEventList kiteNovelEventList = KiteNovelConverter.ConvertTextDocumentIntoEventList(jsonStringOfEventList, kiteNovelMetaData);

            allFolders.Add(new KiteNovelFolder(kiteNovelMetaData, kiteNovelEventList));
        }
        List<VisualNovel> visualNovels = KiteNovelConverter.ConvertFilesToNovels(allFolders);
        KiteNovelManager.Instance().SetAllKiteNovels(visualNovels);
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

    private string ReplaceWordsInString(string input, List<WordPair> wordsToReplace)
    {
        foreach (WordPair wordPair in wordsToReplace)
        {
            if (wordPair != null && !string.IsNullOrEmpty(wordPair.WordToReplace) && !string.IsNullOrEmpty(wordPair.ReplaceByValue))
            {
                input = input.Replace(wordPair.WordToReplace, wordPair.ReplaceByValue);
            }
        }
        return input;
    }
}
