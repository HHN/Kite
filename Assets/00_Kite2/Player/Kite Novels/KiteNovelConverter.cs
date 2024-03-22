using System.Collections.Generic;

public class KiteNovelConverter
{
    public static List<KiteNovelFolder> ConvertNovelsToFiles(List<VisualNovel> novels)
    {
        List<KiteNovelFolder> folders = new List<KiteNovelFolder>();

        foreach (VisualNovel novel in novels)
        {
            KiteNovelFolder folder = new KiteNovelFolder();
            KiteNovelMetaData kiteNovelMetaData = new KiteNovelMetaData();
            KiteNovelEventList kiteNovelEventList = new KiteNovelEventList();

            kiteNovelMetaData.idNumberOfNovel = novel.id;
            kiteNovelMetaData.titleOfNovel = novel.title;
            kiteNovelMetaData.descriptionOfNovel = novel.description;
            kiteNovelMetaData.idNumberOfRepresentationImage = novel.image;
            kiteNovelMetaData.nameOfMainCharacter = novel.nameOfMainCharacter;
            kiteNovelMetaData.contextForPrompt = novel.context;
            kiteNovelMetaData.folderName = novel.folderName;
            kiteNovelMetaData.isKite2Novel = novel.isKite2Novel;

            kiteNovelEventList.novelEvents = novel.novelEvents;

            folder.novelMetaData = kiteNovelMetaData;
            folder.novelEventList = kiteNovelEventList;
            folders.Add(folder);
        }

        return folders;
    }

    public static List<VisualNovel> ConvertFilesToNovels(List<KiteNovelFolder> folders)
    {
        List<VisualNovel> novels = new List<VisualNovel>();

        foreach (KiteNovelFolder folder in folders)
        {
            VisualNovel novel = new VisualNovel();
            KiteNovelMetaData kiteNovelMetaData = folder.novelMetaData;
            KiteNovelEventList kiteNovelEventList = folder.novelEventList;

            novel.id = kiteNovelMetaData.idNumberOfNovel;
            novel.title = kiteNovelMetaData.titleOfNovel;
            novel.description = kiteNovelMetaData.descriptionOfNovel;
            novel.image = kiteNovelMetaData.idNumberOfRepresentationImage;
            novel.nameOfMainCharacter = kiteNovelMetaData.nameOfMainCharacter;
            novel.context = kiteNovelMetaData.contextForPrompt;
            novel.folderName = kiteNovelMetaData.folderName;
            novel.isKite2Novel = kiteNovelMetaData.isKite2Novel;

            novel.novelEvents = kiteNovelEventList.novelEvents;

            novels.Add(novel);
        }

        return novels;
    }
}
