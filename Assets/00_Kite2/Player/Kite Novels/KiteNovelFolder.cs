public class KiteNovelFolder
{
    private KiteNovelMetaData novelMetaData;
    private KiteNovelEventList novelEventList;

    public KiteNovelFolder(KiteNovelMetaData novelMetaData, KiteNovelEventList novelEventList)
    {
        this.novelMetaData = novelMetaData;
        this.novelEventList = novelEventList;
    }

    public KiteNovelFolder()
    {
        this.NovelMetaData = null;
        this.NovelEventList = null;
    }

    public KiteNovelMetaData NovelMetaData
    {
        get { return novelMetaData; }
        set { novelMetaData = value; }
    }

    public KiteNovelEventList NovelEventList
    {
        get { return novelEventList; }
        set { novelEventList = value; }
    }
}
