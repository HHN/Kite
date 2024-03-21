using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class NovelReader
{
    public IEnumerator LoadAllNovels()
    {
        List<VisualNovel> alreadyLoadedNovels = KiteNovelManager.Instance().GetAllKiteNovels();

        if (alreadyLoadedNovels != null && alreadyLoadedNovels.Count > 0)
        {
            yield break; // Novels already loaded --> Stop here.
        }

        bool isRunningOnIOS = false;
        string fullPath = Path.Combine(Application.streamingAssetsPath, "allnovels.txt");

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
            }
            else
            {
                NovelWrapper novelWrapper = JsonUtility.FromJson<NovelWrapper>(jsonString);
                KiteNovelManager.Instance().SetAllKiteNovels(novelWrapper.GetAllNovels());
            }
        } 
        else
        {
            UnityWebRequest www = UnityWebRequest.Get(fullPath);
            yield return www.SendWebRequest();

            if ((www.result == UnityWebRequest.Result.ConnectionError) || (www.result == UnityWebRequest.Result.ProtocolError))
            {
                KiteNovelManager.Instance().SetAllKiteNovels(new List<VisualNovel>());
            }
            else
            {
                string jsonString = www.downloadHandler.text;

                if (string.IsNullOrEmpty(jsonString))
                {
                    KiteNovelManager.Instance().SetAllKiteNovels(new List<VisualNovel>());
                }
                NovelWrapper novelWrapper = JsonUtility.FromJson<NovelWrapper>(jsonString);
                KiteNovelManager.Instance().SetAllKiteNovels(novelWrapper.GetAllNovels());
            }
        }
    }
}
