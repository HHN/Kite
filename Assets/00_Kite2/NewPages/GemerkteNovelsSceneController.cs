using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.UI;

public class GemerkteNovelsSceneController : SceneController
{
    [SerializeField] private RectTransform visualNovelHolder;
    [SerializeField] private Button bankkreditNovel;
    [SerializeField] private Button bekannteTreffenNovel;
    [SerializeField] private Button bankkontoNovel;
    [SerializeField] private Button foerderantragNovel;
    [SerializeField] private Button elternNovel;
    [SerializeField] private Button notarinNovel;
    [SerializeField] private Button presseNovel;
    [SerializeField] private Button bueroNovel;
    [SerializeField] private Button gruenderzuschussNovel;
    [SerializeField] private Button honorarNovel;
    [SerializeField] private Button lebenspartnerNovel;
    [SerializeField] private Button introNovel;

    [SerializeField] private GameObject selectNovelSoundPrefab;

    public void Start()
    {
        bankkreditNovel.onClick.AddListener(delegate { OnBankkreditNovelButton(); });
        bekannteTreffenNovel.onClick.AddListener(delegate { OnBekannteTreffenNovelButton(); });
        bankkontoNovel.onClick.AddListener(delegate { OnBankKontoNovelButton(); });
        foerderantragNovel.onClick.AddListener(delegate { OnFoerderantragNovelButton(); });
        elternNovel.onClick.AddListener(delegate { OnElternNovelButton(); });
        notarinNovel.onClick.AddListener(delegate { OnNotariatNovelButton(); });
        presseNovel.onClick.AddListener(delegate { OnPresseNovelButton(); });
        bueroNovel.onClick.AddListener(delegate { OnBueroNovelButton(); });
        gruenderzuschussNovel.onClick.AddListener(delegate { OnGruenderzuschussNovelButton(); });
        honorarNovel.onClick.AddListener(delegate { OnHonorarNovelButton(); });
        lebenspartnerNovel.onClick.AddListener(delegate { OnLebenspartnerNovelButton(); });
        introNovel.onClick.AddListener(delegate { OnIntroNovelButton(); });

        bankkreditNovel.gameObject.SetActive(false);
        bekannteTreffenNovel.gameObject.SetActive(false);
        bankkontoNovel.gameObject.SetActive(false);
        foerderantragNovel.gameObject.SetActive(false);
        elternNovel.gameObject.SetActive(false);
        notarinNovel.gameObject.SetActive(false);
        presseNovel.gameObject.SetActive(false);
        bueroNovel.gameObject.SetActive(false);
        gruenderzuschussNovel.gameObject.SetActive(false);
        honorarNovel.gameObject.SetActive(false);
        lebenspartnerNovel.gameObject.SetActive(false);
        introNovel.gameObject.SetActive(false);

        List<long> favoriteIds = FavoritesManager.Instance().GetFavoritesIds();
        int index = 0;

        foreach (long id in favoriteIds)
        {
            GameObject novel = GetNovelById(id);

            if (novel == null)
            {
                continue;
            }

            Vector3 localPosition = GetLocalPositionByIndex(index);
            novel.transform.localPosition = localPosition;
            novel.gameObject.SetActive(true);

            index++;
        }

        if (index == 12)
        {
            visualNovelHolder.GetComponent<LayoutElement>().preferredHeight = 1300;
        } 
        else if (index >= 10)
        {
            visualNovelHolder.GetComponent<LayoutElement>().preferredHeight = 1200;
        }
        else if (index == 9)
        {
            visualNovelHolder.GetComponent<LayoutElement>().preferredHeight = 1050;
        }
        else if (index >= 7)
        {
            visualNovelHolder.GetComponent<LayoutElement>().preferredHeight = 900;
        }
        else if (index == 6)
        {
            visualNovelHolder.GetComponent<LayoutElement>().preferredHeight = 750;
        }
        else if (index >= 4)
        {
            visualNovelHolder.GetComponent<LayoutElement>().preferredHeight = 600;
        }
        else if (index == 3)
        {
            visualNovelHolder.GetComponent<LayoutElement>().preferredHeight = 450;
        }
        else
        {
            visualNovelHolder.GetComponent<LayoutElement>().preferredHeight = 300;
        }
    }

    public Vector3 GetLocalPositionByIndex(int index)
    {
        switch (index)
        {
            case 0:
                {
                    return new Vector3(-250f, 0f, 0f);
                }
            case 1:
                {
                    return new Vector3(250f, 0f, 0f);
                }
            case 2:
                {
                    return new Vector3(0f, -144.5f, 0f);
                }
            case 3:
                {
                    return new Vector3(-250f, -290f, 0f);
                }
            case 4:
                {
                    return new Vector3(250f, -290f, 0f);
                }
            case 5:
                {
                    return new Vector3(0f, -433.5f, 0f);
                }
            case 6:
                {
                    return new Vector3(-250f, -578f, 0f);
                }
            case 7:
                {
                    return new Vector3(250f, -578f, 0f);
                }
            case 8:
                {
                    return new Vector3(0f, -722.5f, 0f);
                }
            case 9:
                {
                    return new Vector3(-250f, -868f, 0f);
                }
            case 10:
                {
                    return new Vector3(250f, -868f, 0f);
                }
            case 11:
                {
                    return new Vector3(0f, -1011.5f, 0f);
                }
            default:
                {
                    return Vector3.zero;
                }
        }
    }

    public GameObject GetNovelById(long id)
    {
        VisualNovelNames visualNovelName = VisualNovelNamesHelper.ValueOf((int)id);

        switch (visualNovelName)
        {
            case VisualNovelNames.ELTERN_NOVEL:
                {
                    return elternNovel.gameObject;
                }
            case VisualNovelNames.PRESSE_NOVEL:
                {
                    return presseNovel.gameObject;
                }
            case VisualNovelNames.NOTARIAT_NOVEL:
                {
                    return notarinNovel.gameObject;
                }
            case VisualNovelNames.BANK_KONTO_NOVEL:
                {
                    return bankkontoNovel.gameObject;
                }
            case VisualNovelNames.BUERO_NOVEL:
                {
                    return bueroNovel.gameObject;
                }
            case VisualNovelNames.FOERDERANTRAG_NOVEL:
                {
                    return foerderantragNovel.gameObject;
                }
            case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                {
                    return gruenderzuschussNovel.gameObject;
                }
            case VisualNovelNames.BEKANNTE_TREFFEN_NOVEL:
                {
                    return bekannteTreffenNovel.gameObject;
                }
            case VisualNovelNames.BANK_KREDIT_NOVEL:
                {
                    return bankkreditNovel.gameObject;
                }
            case VisualNovelNames.HONORAR_NOVEL:
                {
                    return honorarNovel.gameObject;
                }
            case VisualNovelNames.INTRO_NOVEL:
                {
                    return introNovel.gameObject;
                }
            case VisualNovelNames.LEBENSPARTNER_NOVEL:
                {
                    return lebenspartnerNovel.gameObject;
                }
            default:
                {
                    return null;
                }
        }
    }


    public void OnBankkreditNovelButton()
    {
        VisualNovelNames visualNovelName = VisualNovelNames.BANK_KREDIT_NOVEL;
        VisualNovel visualNovelToDisplay = null;

        List<VisualNovel> allNovels = KiteNovelManager.Instance().GetAllKiteNovels();

        foreach (VisualNovel novel in allNovels)
        {
            if (novel.id == VisualNovelNamesHelper.ToInt(visualNovelName))
            {
                visualNovelToDisplay = novel;
            }
        }
        if (visualNovelToDisplay == null)
        {
            DisplayErrorMessage("Die gewünschte Novel konnte nicht geladen werden.");
            return;
        }

        PlayManager.Instance().SetVisualNovelToPlay(visualNovelToDisplay);
        PlayManager.Instance().SetForegroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetForegrundColorOfNovel(visualNovelName));
        PlayManager.Instance().SetBackgroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(visualNovelName));
        PlayManager.Instance().SetDiplayNameOfNovelToPlay(FoundersBubbleMetaInformation.GetDisplayNameOfNovelToPlay(visualNovelName));
        GameObject buttonSound = Instantiate(selectNovelSoundPrefab);
        DontDestroyOnLoad(buttonSound);

        if (ShowPlayInstructionManager.Instance().ShowInstruction())
        {
            SceneLoader.LoadPlayInstructionScene();

        }
        else
        {
            SceneLoader.LoadPlayNovelScene();
        }
    }

    public void OnBekannteTreffenNovelButton()
    {
        VisualNovelNames visualNovelName = VisualNovelNames.BEKANNTE_TREFFEN_NOVEL;
        VisualNovel visualNovelToDisplay = null;

        List<VisualNovel> allNovels = KiteNovelManager.Instance().GetAllKiteNovels();

        foreach (VisualNovel novel in allNovels)
        {
            if (novel.id == VisualNovelNamesHelper.ToInt(visualNovelName))
            {
                visualNovelToDisplay = novel;
            }
        }
        if (visualNovelToDisplay == null)
        {
            DisplayErrorMessage("Die gewünschte Novel konnte nicht geladen werden.");
            return;
        }

        PlayManager.Instance().SetVisualNovelToPlay(visualNovelToDisplay);
        PlayManager.Instance().SetForegroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetForegrundColorOfNovel(visualNovelName));
        PlayManager.Instance().SetBackgroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(visualNovelName));
        PlayManager.Instance().SetDiplayNameOfNovelToPlay(FoundersBubbleMetaInformation.GetDisplayNameOfNovelToPlay(visualNovelName));
        GameObject buttonSound = Instantiate(selectNovelSoundPrefab);
        DontDestroyOnLoad(buttonSound);

        if (ShowPlayInstructionManager.Instance().ShowInstruction())
        {
            SceneLoader.LoadPlayInstructionScene();

        }
        else
        {
            SceneLoader.LoadPlayNovelScene();
        }
    }

    public void OnBankKontoNovelButton()
    {
        VisualNovelNames visualNovelName = VisualNovelNames.BANK_KONTO_NOVEL;
        VisualNovel visualNovelToDisplay = null;

        List<VisualNovel> allNovels = KiteNovelManager.Instance().GetAllKiteNovels();

        foreach (VisualNovel novel in allNovels)
        {
            if (novel.id == VisualNovelNamesHelper.ToInt(visualNovelName))
            {
                visualNovelToDisplay = novel;
            }
        }
        if (visualNovelToDisplay == null)
        {
            DisplayErrorMessage("Die gewünschte Novel konnte nicht geladen werden.");
            return;
        }

        PlayManager.Instance().SetVisualNovelToPlay(visualNovelToDisplay);
        PlayManager.Instance().SetForegroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetForegrundColorOfNovel(visualNovelName));
        PlayManager.Instance().SetBackgroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(visualNovelName));
        PlayManager.Instance().SetDiplayNameOfNovelToPlay(FoundersBubbleMetaInformation.GetDisplayNameOfNovelToPlay(visualNovelName));
        GameObject buttonSound = Instantiate(selectNovelSoundPrefab);
        DontDestroyOnLoad(buttonSound);

        if (ShowPlayInstructionManager.Instance().ShowInstruction())
        {
            SceneLoader.LoadPlayInstructionScene();

        }
        else
        {
            SceneLoader.LoadPlayNovelScene();
        }
    }

    public void OnFoerderantragNovelButton()
    {
        VisualNovelNames visualNovelName = VisualNovelNames.FOERDERANTRAG_NOVEL;
        VisualNovel visualNovelToDisplay = null;

        List<VisualNovel> allNovels = KiteNovelManager.Instance().GetAllKiteNovels();

        foreach (VisualNovel novel in allNovels)
        {
            if (novel.id == VisualNovelNamesHelper.ToInt(visualNovelName))
            {
                visualNovelToDisplay = novel;
            }
        }
        if (visualNovelToDisplay == null)
        {
            DisplayErrorMessage("Die gewünschte Novel konnte nicht geladen werden.");
            return;
        }

        PlayManager.Instance().SetVisualNovelToPlay(visualNovelToDisplay);
        PlayManager.Instance().SetForegroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetForegrundColorOfNovel(visualNovelName));
        PlayManager.Instance().SetBackgroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(visualNovelName));
        PlayManager.Instance().SetDiplayNameOfNovelToPlay(FoundersBubbleMetaInformation.GetDisplayNameOfNovelToPlay(visualNovelName));
        GameObject buttonSound = Instantiate(selectNovelSoundPrefab);
        DontDestroyOnLoad(buttonSound);

        if (ShowPlayInstructionManager.Instance().ShowInstruction())
        {
            SceneLoader.LoadPlayInstructionScene();

        }
        else
        {
            SceneLoader.LoadPlayNovelScene();
        }
    }

    public void OnElternNovelButton()
    {
        VisualNovelNames visualNovelName = VisualNovelNames.ELTERN_NOVEL;
        VisualNovel visualNovelToDisplay = null;

        List<VisualNovel> allNovels = KiteNovelManager.Instance().GetAllKiteNovels();

        foreach (VisualNovel novel in allNovels)
        {
            if (novel.id == VisualNovelNamesHelper.ToInt(visualNovelName))
            {
                visualNovelToDisplay = novel;
            }
        }
        if (visualNovelToDisplay == null)
        {
            DisplayErrorMessage("Die gewünschte Novel konnte nicht geladen werden.");
            return;
        }

        PlayManager.Instance().SetVisualNovelToPlay(visualNovelToDisplay);
        PlayManager.Instance().SetForegroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetForegrundColorOfNovel(visualNovelName));
        PlayManager.Instance().SetBackgroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(visualNovelName));
        PlayManager.Instance().SetDiplayNameOfNovelToPlay(FoundersBubbleMetaInformation.GetDisplayNameOfNovelToPlay(visualNovelName));
        GameObject buttonSound = Instantiate(selectNovelSoundPrefab);
        DontDestroyOnLoad(buttonSound);

        if (ShowPlayInstructionManager.Instance().ShowInstruction())
        {
            SceneLoader.LoadPlayInstructionScene();

        }
        else
        {
            SceneLoader.LoadPlayNovelScene();
        }
    }

    public void OnNotariatNovelButton()
    {
        VisualNovelNames visualNovelName = VisualNovelNames.NOTARIAT_NOVEL;
        VisualNovel visualNovelToDisplay = null;

        List<VisualNovel> allNovels = KiteNovelManager.Instance().GetAllKiteNovels();

        foreach (VisualNovel novel in allNovels)
        {
            if (novel.id == VisualNovelNamesHelper.ToInt(visualNovelName))
            {
                visualNovelToDisplay = novel;
            }
        }
        if (visualNovelToDisplay == null)
        {
            DisplayErrorMessage("Die gewünschte Novel konnte nicht geladen werden.");
            return;
        }

        PlayManager.Instance().SetVisualNovelToPlay(visualNovelToDisplay);
        PlayManager.Instance().SetForegroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetForegrundColorOfNovel(visualNovelName));
        PlayManager.Instance().SetBackgroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(visualNovelName));
        PlayManager.Instance().SetDiplayNameOfNovelToPlay(FoundersBubbleMetaInformation.GetDisplayNameOfNovelToPlay(visualNovelName));
        GameObject buttonSound = Instantiate(selectNovelSoundPrefab);
        DontDestroyOnLoad(buttonSound);

        if (ShowPlayInstructionManager.Instance().ShowInstruction())
        {
            SceneLoader.LoadPlayInstructionScene();

        }
        else
        {
            SceneLoader.LoadPlayNovelScene();
        }
    }

    public void OnPresseNovelButton()
    {
        VisualNovelNames visualNovelName = VisualNovelNames.PRESSE_NOVEL;
        VisualNovel visualNovelToDisplay = null;

        List<VisualNovel> allNovels = KiteNovelManager.Instance().GetAllKiteNovels();

        foreach (VisualNovel novel in allNovels)
        {
            if (novel.id == VisualNovelNamesHelper.ToInt(visualNovelName))
            {
                visualNovelToDisplay = novel;
            }
        }
        if (visualNovelToDisplay == null)
        {
            DisplayErrorMessage("Die gewünschte Novel konnte nicht geladen werden.");
            return;
        }

        PlayManager.Instance().SetVisualNovelToPlay(visualNovelToDisplay);
        PlayManager.Instance().SetForegroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetForegrundColorOfNovel(visualNovelName));
        PlayManager.Instance().SetBackgroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(visualNovelName));
        PlayManager.Instance().SetDiplayNameOfNovelToPlay(FoundersBubbleMetaInformation.GetDisplayNameOfNovelToPlay(visualNovelName));
        GameObject buttonSound = Instantiate(selectNovelSoundPrefab);
        DontDestroyOnLoad(buttonSound);

        if (ShowPlayInstructionManager.Instance().ShowInstruction())
        {
            SceneLoader.LoadPlayInstructionScene();

        }
        else
        {
            SceneLoader.LoadPlayNovelScene();
        }
    }

    public void OnBueroNovelButton()
    {
        VisualNovelNames visualNovelName = VisualNovelNames.BUERO_NOVEL;
        VisualNovel visualNovelToDisplay = null;

        List<VisualNovel> allNovels = KiteNovelManager.Instance().GetAllKiteNovels();

        foreach (VisualNovel novel in allNovels)
        {
            if (novel.id == VisualNovelNamesHelper.ToInt(visualNovelName))
            {
                visualNovelToDisplay = novel;
            }
        }
        if (visualNovelToDisplay == null)
        {
            DisplayErrorMessage("Die gewünschte Novel konnte nicht geladen werden.");
            return;
        }

        PlayManager.Instance().SetVisualNovelToPlay(visualNovelToDisplay);
        PlayManager.Instance().SetForegroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetForegrundColorOfNovel(visualNovelName));
        PlayManager.Instance().SetBackgroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(visualNovelName));
        PlayManager.Instance().SetDiplayNameOfNovelToPlay(FoundersBubbleMetaInformation.GetDisplayNameOfNovelToPlay(visualNovelName));
        GameObject buttonSound = Instantiate(selectNovelSoundPrefab);
        DontDestroyOnLoad(buttonSound);

        if (ShowPlayInstructionManager.Instance().ShowInstruction())
        {
            SceneLoader.LoadPlayInstructionScene();

        }
        else
        {
            SceneLoader.LoadPlayNovelScene();
        }
    }

    public void OnGruenderzuschussNovelButton()
    {
        VisualNovelNames visualNovelName = VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL;
        VisualNovel visualNovelToDisplay = null;

        List<VisualNovel> allNovels = KiteNovelManager.Instance().GetAllKiteNovels();

        foreach (VisualNovel novel in allNovels)
        {
            if (novel.id == VisualNovelNamesHelper.ToInt(visualNovelName))
            {
                visualNovelToDisplay = novel;
            }
        }
        if (visualNovelToDisplay == null)
        {
            DisplayErrorMessage("Die gewünschte Novel konnte nicht geladen werden.");
            return;
        }

        PlayManager.Instance().SetVisualNovelToPlay(visualNovelToDisplay);
        PlayManager.Instance().SetForegroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetForegrundColorOfNovel(visualNovelName));
        PlayManager.Instance().SetBackgroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(visualNovelName));
        PlayManager.Instance().SetDiplayNameOfNovelToPlay(FoundersBubbleMetaInformation.GetDisplayNameOfNovelToPlay(visualNovelName));
        GameObject buttonSound = Instantiate(selectNovelSoundPrefab);
        DontDestroyOnLoad(buttonSound);

        if (ShowPlayInstructionManager.Instance().ShowInstruction())
        {
            SceneLoader.LoadPlayInstructionScene();

        }
        else
        {
            SceneLoader.LoadPlayNovelScene();
        }
    }

    public void OnHonorarNovelButton()
    {
        VisualNovelNames visualNovelName = VisualNovelNames.HONORAR_NOVEL;
        VisualNovel visualNovelToDisplay = null;

        List<VisualNovel> allNovels = KiteNovelManager.Instance().GetAllKiteNovels();

        foreach (VisualNovel novel in allNovels)
        {
            if (novel.id == VisualNovelNamesHelper.ToInt(visualNovelName))
            {
                visualNovelToDisplay = novel;
            }
        }
        if (visualNovelToDisplay == null)
        {
            DisplayErrorMessage("Die gewünschte Novel konnte nicht geladen werden.");
            return;
        }

        PlayManager.Instance().SetVisualNovelToPlay(visualNovelToDisplay);
        PlayManager.Instance().SetForegroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetForegrundColorOfNovel(visualNovelName));
        PlayManager.Instance().SetBackgroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(visualNovelName));
        PlayManager.Instance().SetDiplayNameOfNovelToPlay(FoundersBubbleMetaInformation.GetDisplayNameOfNovelToPlay(visualNovelName));
        GameObject buttonSound = Instantiate(selectNovelSoundPrefab);
        DontDestroyOnLoad(buttonSound);

        if (ShowPlayInstructionManager.Instance().ShowInstruction())
        {
            SceneLoader.LoadPlayInstructionScene();

        }
        else
        {
            SceneLoader.LoadPlayNovelScene();
        }
    }

    public void OnLebenspartnerNovelButton()
    {
        VisualNovelNames visualNovelName = VisualNovelNames.LEBENSPARTNER_NOVEL;
        VisualNovel visualNovelToDisplay = null;

        List<VisualNovel> allNovels = KiteNovelManager.Instance().GetAllKiteNovels();

        foreach (VisualNovel novel in allNovels)
        {
            if (novel.id == VisualNovelNamesHelper.ToInt(visualNovelName))
            {
                visualNovelToDisplay = novel;
            }
        }
        if (visualNovelToDisplay == null)
        {
            DisplayErrorMessage("Die gewünschte Novel konnte nicht geladen werden.");
            return;
        }

        PlayManager.Instance().SetVisualNovelToPlay(visualNovelToDisplay);
        PlayManager.Instance().SetForegroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetForegrundColorOfNovel(visualNovelName));
        PlayManager.Instance().SetBackgroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(visualNovelName));
        PlayManager.Instance().SetDiplayNameOfNovelToPlay(FoundersBubbleMetaInformation.GetDisplayNameOfNovelToPlay(visualNovelName));
        GameObject buttonSound = Instantiate(selectNovelSoundPrefab);
        DontDestroyOnLoad(buttonSound);

        if (ShowPlayInstructionManager.Instance().ShowInstruction())
        {
            SceneLoader.LoadPlayInstructionScene();

        }
        else
        {
            SceneLoader.LoadPlayNovelScene();
        }
    }

    public void OnIntroNovelButton()
    {
        VisualNovelNames visualNovelName = VisualNovelNames.INTRO_NOVEL;
        VisualNovel visualNovelToDisplay = null;

        List<VisualNovel> allNovels = KiteNovelManager.Instance().GetAllKiteNovels();

        foreach (VisualNovel novel in allNovels)
        {
            if (novel.id == VisualNovelNamesHelper.ToInt(visualNovelName))
            {
                visualNovelToDisplay = novel;
            }
        }
        if (visualNovelToDisplay == null)
        {
            DisplayErrorMessage("Die gewünschte Novel konnte nicht geladen werden.");
            return;
        }

        PlayManager.Instance().SetVisualNovelToPlay(visualNovelToDisplay);
        PlayManager.Instance().SetForegroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetForegrundColorOfNovel(visualNovelName));
        PlayManager.Instance().SetBackgroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(visualNovelName));
        PlayManager.Instance().SetDiplayNameOfNovelToPlay(FoundersBubbleMetaInformation.GetDisplayNameOfNovelToPlay(visualNovelName));
        GameObject buttonSound = Instantiate(selectNovelSoundPrefab);
        DontDestroyOnLoad(buttonSound);

        if (ShowPlayInstructionManager.Instance().ShowInstruction())
        {
            SceneLoader.LoadPlayInstructionScene();

        }
        else
        {
            SceneLoader.LoadPlayNovelScene();
        }
    }
}
