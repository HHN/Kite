using Assets._Scripts.Player.KiteNovels.VisualNovelFormatter;

namespace Assets._Scripts.Novel.VisualNovelFormatter
{
    public class KiteNovelFolder
    {
        public KiteNovelFolder(KiteNovelMetaData novelMetaData, List<VisualNovelEvent> novelEventList)
        {
            this.NovelMetaData = novelMetaData;
            this.NovelEventList = novelEventList;
        }

        public KiteNovelFolder()
        {
            this.NovelMetaData = null;
            this.NovelEventList = null;
        }

        public KiteNovelMetaData NovelMetaData { get; set; }

        public List<VisualNovelEvent> NovelEventList { get; set; }
    }
}