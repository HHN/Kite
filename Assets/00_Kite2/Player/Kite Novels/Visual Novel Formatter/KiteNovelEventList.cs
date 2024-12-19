using System.Collections.Generic;
using _00_Kite2.Common.Novel;

namespace _00_Kite2.Player.Kite_Novels.Visual_Novel_Formatter
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