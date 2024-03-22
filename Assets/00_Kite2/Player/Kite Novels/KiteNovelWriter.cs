using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class KiteNovelWriter
{
    public IEnumerator SaveNovelsAsFiles()
    {
        List<VisualNovel> allNovels = KiteNovelManager.Instance().GetAllKiteNovels();

        if (allNovels == null ||allNovels.Count == 0 )
        {
            yield break;
        }
        string topLevelPath = Path.Combine(Application.dataPath, "output");

        if (Directory.Exists(topLevelPath))
        {
            Directory.Delete(topLevelPath, true);
        }
        Directory.CreateDirectory(topLevelPath);

        List<KiteNovelFolder> allFolders = KiteNovelConverter.ConvertNovelsToFiles(allNovels);
        KiteNovelList novelList = new KiteNovelList();
        List<string> listOfNovels = new List<string>();

        foreach (KiteNovelFolder folder in allFolders)
        {
            string subfolderPath = Path.Combine(topLevelPath, folder.novelMetaData.folderName);
            Directory.CreateDirectory(subfolderPath);
            string metaDataString = JsonUtility.ToJson(folder.novelMetaData);
            string eventListString = JsonUtility.ToJson(folder.novelEventList);
            File.WriteAllText(Path.Combine(subfolderPath, "visual_novel_meta_data.json"), metaDataString);
            File.WriteAllText(Path.Combine(subfolderPath, "visual_novel_event_list.json"), eventListString);

            listOfNovels.Add(Path.Combine("novels", folder.novelMetaData.folderName));
        }

        novelList.visualNovels = listOfNovels;
        string novelListJson = JsonUtility.ToJson(novelList);
        File.WriteAllText(Path.Combine(Application.dataPath, "output", "list_of_novels.json"), novelListJson);
    }
}
