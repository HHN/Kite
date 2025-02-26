using System.Collections.Generic;
using Assets._Scripts.Common.Novel;

namespace Assets._Scripts.Player.Kite_Novels.Visual_Novel_Formatter
{
    public class KiteNovelEventList
    {
        public KiteNovelEventList()
        {
            NovelEvents = new List<VisualNovelEvent>();
        }

        public KiteNovelEventList(List<VisualNovelEvent> novelEvents)
        {
            this.NovelEvents = novelEvents;
        }

        public List<VisualNovelEvent> NovelEvents { get; set; }
    }
}