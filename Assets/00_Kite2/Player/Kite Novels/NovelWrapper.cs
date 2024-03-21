using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NovelWrapper
{
    [SerializeField] private List<VisualNovel> allNovels;

    public void SetAllNovels(List<VisualNovel> allNovels)
    {
        this.allNovels = allNovels;
    }

    public List<VisualNovel> GetAllNovels()
    {
        return this.allNovels;
    }
}
