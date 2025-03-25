using Assets._Scripts.NovelData;

namespace Assets._Scripts.Player.KiteNovels.VisualNovelFormatter
{
    public class KiteNovelFolder
    {
        public KiteNovelFolder(NovelMetaData novelMetaData, KiteNovelEventList novelEventList)
        {
            this.NovelMetaData = novelMetaData;
            this.NovelEventList = novelEventList;
        }

        public KiteNovelFolder()
        {
            this.NovelMetaData = null;
            this.NovelEventList = null;
        }

        public NovelMetaData NovelMetaData { get; set; }

        public KiteNovelEventList NovelEventList { get; set; }
    }
}