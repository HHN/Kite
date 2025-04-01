using System.Collections.Generic;
using Assets._Scripts.Novel;

namespace Assets._Scripts.Player.KiteNovels.VisualNovelFormatter
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