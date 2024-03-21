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

        string fullPath = Path.Combine(Application.streamingAssetsPath, "allnovels.txt");
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
