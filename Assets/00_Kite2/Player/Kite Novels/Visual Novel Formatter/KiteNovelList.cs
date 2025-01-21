using System.Collections.Generic;
using UnityEngine;

namespace _00_Kite2.Player.Kite_Novels.Visual_Novel_Formatter
{
    public class KiteNovelList
    {
        public KiteNovelList(List<string> visualNovels)
        {
            this.VisualNovels = visualNovels;
        }

        public KiteNovelList()
        {
            VisualNovels = new List<string>();
        }

        public List<string> VisualNovels { get; set; }
    }
}