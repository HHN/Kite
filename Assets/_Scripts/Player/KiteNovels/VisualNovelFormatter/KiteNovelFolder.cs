using Assets._Scripts.NovelData;

namespace Assets._Scripts.Player.KiteNovels.VisualNovelFormatter
{
    public class KiteNovelFolder
    {
        public KiteNovelFolder(NovelMetaData novelMetaData, KiteNovelEventList novelEventList)
        {
            NovelMetaData = novelMetaData;
            NovelEventList = novelEventList;
        }

        public NovelMetaData NovelMetaData { get; set; }

        public KiteNovelEventList NovelEventList { get; }
    }
}