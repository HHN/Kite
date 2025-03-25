using System.Collections.Generic;

namespace Assets._Scripts.Player.Kite_Novels.Visual_Novel_Formatter
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