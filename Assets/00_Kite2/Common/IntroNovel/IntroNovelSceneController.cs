using System.Collections;
using System.Collections.Generic;
using _00_Kite2.Player;
using UnityEngine;

public class IntroNovelSceneController : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(StartIntroNovel());
    }

    private IEnumerator StartIntroNovel()
    {
        yield return new WaitForSeconds(2.5f);

        Debug.Log("What's here?");

        List<VisualNovel> allNovels = KiteNovelManager.Instance().GetAllKiteNovels();

        Debug.Log(allNovels.Count);

        foreach (var novel in allNovels)
        {
            if (novel.title == "Einstiegsdialog")
            {
                Debug.Log("Check");

                VisualNovelNames novelNames = VisualNovelNamesHelper.ValueOf((int)novel.id);

                PlayManager.Instance().SetVisualNovelToPlay(novel);
                PlayManager.Instance().SetForegroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetForegroundColorOfNovel(novelNames));
                PlayManager.Instance().SetBackgroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(novelNames));
                PlayManager.Instance().SetDiplayNameOfNovelToPlay(FoundersBubbleMetaInformation.GetDisplayNameOfNovelToPlay(novelNames));

                if (ShowPlayInstructionManager.Instance().ShowInstruction())
                {
                    Debug.Log("Check 2");
                    // Load the PlayNovelScene directly
                    SceneLoader.LoadPlayNovelScene();
                    //return; // Exit the loop after loading
                }
            }
        }
    }
}
