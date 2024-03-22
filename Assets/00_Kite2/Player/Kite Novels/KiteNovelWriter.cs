using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

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
            string metaDataString = JsonConvert.SerializeObject(folder.novelMetaData, Formatting.Indented);
            string eventListString = JsonConvert.SerializeObject(folder.novelEventList, Formatting.Indented);
            File.WriteAllText(Path.Combine(subfolderPath, "visual_novel_meta_data.txt"), metaDataString);
            File.WriteAllText(Path.Combine(subfolderPath, "visual_novel_event_list.txt"), eventListString);

            listOfNovels.Add(Path.Combine("novels", folder.novelMetaData.folderName));
        }

        novelList.visualNovels = listOfNovels;
        string novelListJson = JsonConvert.SerializeObject(novelList, Formatting.Indented);
        File.WriteAllText(Path.Combine(Application.dataPath, "output", "list_of_novels.txt"), novelListJson);
    }
}
