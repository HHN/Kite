using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratedNovelManager : MonoBehaviour
{
    private static GeneratedNovelManager instance;
    private List<VisualNovel> novels;

    private GeneratedNovelManager() 
    { 
        novels = new List<VisualNovel>();
    }

    public static GeneratedNovelManager Instance() 
    {
        if (instance == null)
        {
            instance = new GeneratedNovelManager();
        }

        return instance; 
    }

    public List<VisualNovel> GetUserNovels()
    {
        return novels;
    }

    internal void AddUserNovel(VisualNovel novel)
    {
        novels.Add(novel);
    }
}
