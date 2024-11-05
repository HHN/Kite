using System.Collections.Generic;
using UnityEngine;

public class KiteNovelManager
{
    private static KiteNovelManager instance;
    private List<VisualNovel> kiteNovels;

    private KiteNovelManager() 
    {
        this.kiteNovels = new List<VisualNovel>();
    }

    public static KiteNovelManager Instance()
    {
        if (instance == null)
        {
            instance = new KiteNovelManager();
        }
        return instance;
    }

    public List<VisualNovel> GetAllKiteNovels()
    {
        return kiteNovels;
    }

    public void SetAllKiteNovels(List<VisualNovel> kiteNovels)
    {
        Debug.Log("Setting all novels...");
        this.kiteNovels = kiteNovels;
        Debug.Log("Novels set: " + this.kiteNovels.Count);
    }

    public bool AreNovelsLoaded()
    {
        return (kiteNovels != null && kiteNovels.Count > 0);
    }
}
