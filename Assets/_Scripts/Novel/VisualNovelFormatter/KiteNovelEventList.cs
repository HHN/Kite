using System.Collections.Generic;

namespace Assets._Scripts.Novel.VisualNovelFormatter
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