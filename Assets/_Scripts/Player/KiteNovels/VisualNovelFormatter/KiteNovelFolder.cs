namespace Assets._Scripts.Player.KiteNovels.VisualNovelFormatter
{
    public class KiteNovelFolder
    {
        public KiteNovelFolder(KiteNovelMetaData novelMetaData, KiteNovelEventList novelEventList)
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

        public KiteNovelEventList NovelEventList { get; set; }
    }
}