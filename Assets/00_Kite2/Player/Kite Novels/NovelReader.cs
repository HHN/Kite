using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class NovelReader
{
 
    public IEnumerator LoadAllNovelsWithTweeApproach()
    {
        List<VisualNovel> alreadyLoadedNovels = KiteNovelManager.Instance().GetAllKiteNovels();

        if (alreadyLoadedNovels != null && alreadyLoadedNovels.Count > 0)
        {
            yield break; // Novels already loaded --> Stop here.
        }

        bool isRunningOnIOS = false;
        string fullPath = Path.Combine(Application.streamingAssetsPath, "novels_twee", "list_of_novels.txt");
        List<string> listOfAllNovelPaths = new List<string>();

#if UNITY_IOS
            isRunningOnIOS = true;
#endif

        if (isRunningOnIOS)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);
            string jsonString = System.Text.Encoding.UTF8.GetString(fileBytes);

            if (string.IsNullOrEmpty(jsonString))
            {
                KiteNovelManager.Instance().SetAllKiteNovels(new List<VisualNovel>());
                yield break;
            }
            else
            {
                KiteNovelList kiteNovelList = JsonUtility.FromJson<KiteNovelList>(jsonString);
                listOfAllNovelPaths = kiteNovelList.visualNovels;
            }
        }
        else
        {
            UnityWebRequest www = UnityWebRequest.Get(fullPath);
            yield return www.SendWebRequest();

            if ((www.result == UnityWebRequest.Result.ConnectionError) || (www.result == UnityWebRequest.Result.ProtocolError))
            {
                KiteNovelManager.Instance().SetAllKiteNovels(new List<VisualNovel>());
                yield break;
            }
            else
            {
                string jsonString = www.downloadHandler.text;

                if (string.IsNullOrEmpty(jsonString))
                {
                    KiteNovelManager.Instance().SetAllKiteNovels(new List<VisualNovel>());
                }
                KiteNovelList kiteNovelList = JsonUtility.FromJson<KiteNovelList>(jsonString);
                listOfAllNovelPaths = kiteNovelList.visualNovels;
            }
        }

        List<KiteNovelFolder> allFolders = new List<KiteNovelFolder>();

        foreach (string pathOfNovel in listOfAllNovelPaths)
        {
            string fullPathOfNovelMetaData = Path.Combine(Application.streamingAssetsPath, pathOfNovel, "visual_novel_meta_data.txt");
            string fullPathOfNovelEventList = Path.Combine(Application.streamingAssetsPath, pathOfNovel, "visual_novel_event_list.txt");

            if (isRunningOnIOS)
            {
                byte[] fileBytesOfMetaData = System.IO.File.ReadAllBytes(fullPathOfNovelMetaData);
                byte[] fileBytesOfEventList = System.IO.File.ReadAllBytes(fullPathOfNovelEventList);

                string jsonStringOfMetaData = System.Text.Encoding.UTF8.GetString(fileBytesOfMetaData);
                string jsonStringOfEventList = System.Text.Encoding.UTF8.GetString(fileBytesOfEventList);

                if (string.IsNullOrEmpty(jsonStringOfMetaData) || string.IsNullOrEmpty(jsonStringOfEventList))
                {
                    continue;
                }

                KiteNovelMetaData kiteNovelMetaData = JsonUtility.FromJson<KiteNovelMetaData>(jsonStringOfMetaData);

                if (kiteNovelMetaData == null)
                {
                    continue;
                }

                if (kiteNovelMetaData.wordsToReplace != null)
                {
                    foreach (WordPair wordPair in kiteNovelMetaData.wordsToReplace)
                    {
                        if (wordPair != null && wordPair.wordToReplace != null && wordPair.replaceByValue != null)
                        {
                            jsonStringOfEventList = jsonStringOfEventList.Replace(wordPair.wordToReplace, wordPair.replaceByValue);
                        }
                    }
                }
                KiteNovelEventList kiteNovelEventList = KiteNovelConverter.ConvertTextDocumentIntoEventList(jsonStringOfEventList, 
                    kiteNovelMetaData.isWithStartValues, kiteNovelMetaData.startLocation, 
                    kiteNovelMetaData.talkingPartner01, kiteNovelMetaData.startTalkingPartnerEmotion,
                    kiteNovelMetaData.talkingPartner01, kiteNovelMetaData.talkingPartner02, kiteNovelMetaData.talkingPartner03);
                KiteNovelFolder folder = new KiteNovelFolder();
                folder.novelMetaData = kiteNovelMetaData;
                folder.novelEventList = kiteNovelEventList;
                allFolders.Add(folder);
            }
            else
            {
                KiteNovelMetaData kiteNovelMetaData;
                KiteNovelEventList kiteNovelEventList;

                UnityWebRequest www = UnityWebRequest.Get(fullPathOfNovelMetaData);
                yield return www.SendWebRequest();

                if ((www.result == UnityWebRequest.Result.ConnectionError) || (www.result == UnityWebRequest.Result.ProtocolError))
                {
                    continue;
                }
                else
                {
                    string jsonString = www.downloadHandler.text;

                    if (string.IsNullOrEmpty(jsonString))
                    {
                        continue;
                    }
                    kiteNovelMetaData = JsonUtility.FromJson<KiteNovelMetaData>(jsonString);
                }

                www = UnityWebRequest.Get(fullPathOfNovelEventList);
                yield return www.SendWebRequest();

                if ((www.result == UnityWebRequest.Result.ConnectionError) || (www.result == UnityWebRequest.Result.ProtocolError))
                {
                    continue;
                }
                else
                {
                    string jsonString = www.downloadHandler.text;

                    if (string.IsNullOrEmpty(jsonString))
                    {
                        continue;
                    }
                    if (kiteNovelMetaData.wordsToReplace != null)
                    {
                        foreach (WordPair wordPair in kiteNovelMetaData.wordsToReplace)
                        {
                            if (wordPair != null && wordPair.wordToReplace != null && wordPair.replaceByValue != null)
                            {
                                jsonString = jsonString.Replace(wordPair.wordToReplace, wordPair.replaceByValue);
                            }
                        }
                    }
                    kiteNovelEventList = KiteNovelConverter.ConvertTextDocumentIntoEventList(jsonString, 
                        kiteNovelMetaData.isWithStartValues, kiteNovelMetaData.startLocation, 
                        kiteNovelMetaData.talkingPartner01, kiteNovelMetaData.startTalkingPartnerEmotion,
                        kiteNovelMetaData.talkingPartner01, kiteNovelMetaData.talkingPartner02, kiteNovelMetaData.talkingPartner03);
                }
                KiteNovelFolder folder = new KiteNovelFolder();
                folder.novelMetaData = kiteNovelMetaData;
                folder.novelEventList = kiteNovelEventList;
                allFolders.Add(folder);
            }
        }

        List<VisualNovel> visualNovels = KiteNovelConverter.ConvertFilesToNovels(allFolders);
        KiteNovelManager.Instance().SetAllKiteNovels(visualNovels);
    }
}