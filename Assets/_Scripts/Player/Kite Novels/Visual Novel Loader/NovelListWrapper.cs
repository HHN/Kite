using System;
using System.Collections.Generic;
using Assets._Scripts.Common.Novel;
using UnityEngine;

namespace Assets._Scripts.Player.Kite_Novels.Visual_Novel_Loader
{
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
            get => visualNovels;
            set => visualNovels = value;
        }
    }
}