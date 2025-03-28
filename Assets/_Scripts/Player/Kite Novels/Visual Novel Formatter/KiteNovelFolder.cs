namespace Assets._Scripts.Player.Kite_Novels.Visual_Novel_Formatter
{
    public class KiteNovelFolder
    {
        public KiteNovelFolder(KiteNovelMetaData novelMetaData, KiteNovelEventList novelEventList)
        {
            NovelMetaData = novelMetaData;
            NovelEventList = novelEventList;
        }

        public KiteNovelMetaData NovelMetaData { get; }

        public KiteNovelEventList NovelEventList { get; }
    }
}