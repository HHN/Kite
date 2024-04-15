using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NovelListWrapper
{
    [SerializeField] private List<VisualNovel> visualNovels;

    public NovelListWrapper(List<VisualNovel> visualNovels)
    {
        this.visualNovels = visualNovels;
    }

    public NovelListWrapper()
    {
        visualNovels = new List<VisualNovel>();
    }

    public List<VisualNovel> VisualNovels
    {
        get { return visualNovels; }
        set { visualNovels = value; }
    }
}
