using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoundersBubbleSceneController : SceneController
{
    public GameObject selectNovelSoundPrefab;

    public Button foundersWellButton;

    public GameObject bankkreditNovelTextBubble;
    public GameObject bekannteTreffenNovelTextBubble;
    public GameObject bankkontoNovelTextBubble;
    public GameObject foerderantragNovelTextBubble;
    public GameObject elternNovelTextBubble;
    public GameObject notariatNovelTextBubble;
    public GameObject presseNovelTextBubble;
    public GameObject bueroNovelTextBubble;
    public GameObject gruenderzuschussNovelTextBubble;
    public GameObject honorarNovelTextBubble;
    public GameObject lebenspartnerNovelTextBubble;
    public GameObject introNovelTextBubble;

    public GameObject currentOpenBubble;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.FOUNDERS_BUBBLE_SCENE);

        foundersWellButton.onClick.AddListener(delegate { OnFoundersWellButton(); });

        bankkreditNovelTextBubble.SetActive(false);
        bekannteTreffenNovelTextBubble.SetActive(false);
        bankkontoNovelTextBubble.SetActive(false);
        foerderantragNovelTextBubble.SetActive(false);
        elternNovelTextBubble.SetActive(false); 
        notariatNovelTextBubble.SetActive(false);
        presseNovelTextBubble.SetActive(false);
        bueroNovelTextBubble.SetActive(false);
        gruenderzuschussNovelTextBubble.SetActive(false); 
        honorarNovelTextBubble.SetActive(false);
        lebenspartnerNovelTextBubble.SetActive(false);
        introNovelTextBubble.SetActive(false);
        currentOpenBubble = null;
    }

    public void OnFoundersWellButton()
    {
        SceneLoader.LoadFoundersWellScene();
    }

    public void OnBankkreditNovelButton()
    {
        if (currentOpenBubble == bankkreditNovelTextBubble)
        {
            bankkreditNovelTextBubble.SetActive(false);
            currentOpenBubble = null;
            return;
        }
        bankkreditNovelTextBubble.SetActive(true);
        bekannteTreffenNovelTextBubble.SetActive(false);
        bankkontoNovelTextBubble.SetActive(false);
        foerderantragNovelTextBubble.SetActive(false);
        elternNovelTextBubble.SetActive(false);
        notariatNovelTextBubble.SetActive(false);
        presseNovelTextBubble.SetActive(false);
        bueroNovelTextBubble.SetActive(false);
        gruenderzuschussNovelTextBubble.SetActive(false);
        honorarNovelTextBubble.SetActive(false);
        lebenspartnerNovelTextBubble.SetActive(false);
        introNovelTextBubble.SetActive(false);
        currentOpenBubble = bankkreditNovelTextBubble;
    }

    public void OnPlayBankkreditNovelButton()
    {
        LoadNovel(10);
    }

    public void OnBekannteTreffenNovelButton()
    {
        if (currentOpenBubble == bekannteTreffenNovelTextBubble)
        {
            bekannteTreffenNovelTextBubble.SetActive(false);
            currentOpenBubble = null;
            return;
        }
        bankkreditNovelTextBubble.SetActive(false);
        bekannteTreffenNovelTextBubble.SetActive(true);
        bankkontoNovelTextBubble.SetActive(false);
        foerderantragNovelTextBubble.SetActive(false);
        elternNovelTextBubble.SetActive(false);
        notariatNovelTextBubble.SetActive(false);
        presseNovelTextBubble.SetActive(false);
        bueroNovelTextBubble.SetActive(false);
        gruenderzuschussNovelTextBubble.SetActive(false);
        honorarNovelTextBubble.SetActive(false);
        lebenspartnerNovelTextBubble.SetActive(false);
        introNovelTextBubble.SetActive(false);
        currentOpenBubble = bekannteTreffenNovelTextBubble;
    }

    public void OnPlayBekannteTreffenNovelButton()
    {
        LoadNovel(9);
    }

    public void OnBankKontoNovelButton()
    {
        if (currentOpenBubble == bankkontoNovelTextBubble)
        {
            bankkontoNovelTextBubble.SetActive(false);
            currentOpenBubble = null;
            return;
        }
        bankkreditNovelTextBubble.SetActive(false);
        bekannteTreffenNovelTextBubble.SetActive(false);
        bankkontoNovelTextBubble.SetActive(true);
        foerderantragNovelTextBubble.SetActive(false);
        elternNovelTextBubble.SetActive(false);
        notariatNovelTextBubble.SetActive(false);
        presseNovelTextBubble.SetActive(false);
        bueroNovelTextBubble.SetActive(false);
        gruenderzuschussNovelTextBubble.SetActive(false);
        honorarNovelTextBubble.SetActive(false);
        lebenspartnerNovelTextBubble.SetActive(false);
        introNovelTextBubble.SetActive(false);
        currentOpenBubble = bankkontoNovelTextBubble;
    }

    public void OnPlayBankKontoNovelButton()
    {
        LoadNovel(5);
    }

    public void OnFoerderantragNovelButton()
    {
        if (currentOpenBubble == foerderantragNovelTextBubble)
        {
            foerderantragNovelTextBubble.SetActive(false);
            currentOpenBubble = null;
            return;
        }
        bankkreditNovelTextBubble.SetActive(false);
        bekannteTreffenNovelTextBubble.SetActive(false);
        bankkontoNovelTextBubble.SetActive(false);
        foerderantragNovelTextBubble.SetActive(true);
        elternNovelTextBubble.SetActive(false);
        notariatNovelTextBubble.SetActive(false);
        presseNovelTextBubble.SetActive(false);
        bueroNovelTextBubble.SetActive(false);
        gruenderzuschussNovelTextBubble.SetActive(false);
        honorarNovelTextBubble.SetActive(false);
        lebenspartnerNovelTextBubble.SetActive(false);
        introNovelTextBubble.SetActive(false);
        currentOpenBubble = foerderantragNovelTextBubble;
    }

    public void OnPlayFoerderantragNovelButton()
    {
        LoadNovel(7);
    }

    public void OnElternNovelButton()
    {
        if (currentOpenBubble == elternNovelTextBubble)
        {
            elternNovelTextBubble.SetActive(false);
            currentOpenBubble = null;
            return;
        }
        bankkreditNovelTextBubble.SetActive(false);
        bekannteTreffenNovelTextBubble.SetActive(false);
        bankkontoNovelTextBubble.SetActive(false);
        foerderantragNovelTextBubble.SetActive(false);
        elternNovelTextBubble.SetActive(true);
        notariatNovelTextBubble.SetActive(false);
        presseNovelTextBubble.SetActive(false);
        bueroNovelTextBubble.SetActive(false);
        gruenderzuschussNovelTextBubble.SetActive(false);
        honorarNovelTextBubble.SetActive(false);
        lebenspartnerNovelTextBubble.SetActive(false);
        introNovelTextBubble.SetActive(false);
        currentOpenBubble = elternNovelTextBubble;
    }

    public void OnPlayElternNovelButton()
    {
        LoadNovel(2);
    }

    public void OnNotariatNovelButton()
    {
        if (currentOpenBubble == notariatNovelTextBubble)
        {
            notariatNovelTextBubble.SetActive(false);
            currentOpenBubble = null;
            return;
        }
        bankkreditNovelTextBubble.SetActive(false);
        bekannteTreffenNovelTextBubble.SetActive(false);
        bankkontoNovelTextBubble.SetActive(false);
        foerderantragNovelTextBubble.SetActive(false);
        elternNovelTextBubble.SetActive(false);
        notariatNovelTextBubble.SetActive(true);
        presseNovelTextBubble.SetActive(false);
        bueroNovelTextBubble.SetActive(false);
        gruenderzuschussNovelTextBubble.SetActive(false);
        honorarNovelTextBubble.SetActive(false);
        lebenspartnerNovelTextBubble.SetActive(false);
        introNovelTextBubble.SetActive(false);
        currentOpenBubble = notariatNovelTextBubble;
    }

    public void OnPlayNotariatNovelButton()
    {
        LoadNovel(4);
    }

    public void OnPresseNovelButton()
    {
        if (currentOpenBubble == presseNovelTextBubble)
        {
            presseNovelTextBubble.SetActive(false);
            currentOpenBubble = null;
            return;
        }
        bankkreditNovelTextBubble.SetActive(false);
        bekannteTreffenNovelTextBubble.SetActive(false);
        bankkontoNovelTextBubble.SetActive(false);
        foerderantragNovelTextBubble.SetActive(false);
        elternNovelTextBubble.SetActive(false);
        notariatNovelTextBubble.SetActive(false);
        presseNovelTextBubble.SetActive(true);
        bueroNovelTextBubble.SetActive(false);
        gruenderzuschussNovelTextBubble.SetActive(false);
        honorarNovelTextBubble.SetActive(false);
        lebenspartnerNovelTextBubble.SetActive(false);
        introNovelTextBubble.SetActive(false);
        currentOpenBubble = presseNovelTextBubble;
    }

    public void OnPlayPresseNovelButton()
    {
        LoadNovel(3);
    }

    public void OnBueroNovelButton()
    {
        if (currentOpenBubble == bueroNovelTextBubble)
        {
            bueroNovelTextBubble.SetActive(false);
            currentOpenBubble = null;
            return;
        }
        bankkreditNovelTextBubble.SetActive(false);
        bekannteTreffenNovelTextBubble.SetActive(false);
        bankkontoNovelTextBubble.SetActive(false);
        foerderantragNovelTextBubble.SetActive(false);
        elternNovelTextBubble.SetActive(false);
        notariatNovelTextBubble.SetActive(false);
        presseNovelTextBubble.SetActive(false);
        bueroNovelTextBubble.SetActive(true);
        gruenderzuschussNovelTextBubble.SetActive(false);
        honorarNovelTextBubble.SetActive(false);
        lebenspartnerNovelTextBubble.SetActive(false);
        introNovelTextBubble.SetActive(false);
        currentOpenBubble = bueroNovelTextBubble;
    }

    public void OnPlayBueroNovelButton()
    {
        LoadNovel(6);
    }

    public void OnGruenderzuschussNovelButton()
    {
        if (currentOpenBubble == gruenderzuschussNovelTextBubble)
        {
            gruenderzuschussNovelTextBubble.SetActive(false);
            currentOpenBubble = null;
            return;
        }
        bankkreditNovelTextBubble.SetActive(false);
        bekannteTreffenNovelTextBubble.SetActive(false);
        bankkontoNovelTextBubble.SetActive(false);
        foerderantragNovelTextBubble.SetActive(false);
        elternNovelTextBubble.SetActive(false);
        notariatNovelTextBubble.SetActive(false);
        presseNovelTextBubble.SetActive(false);
        bueroNovelTextBubble.SetActive(false);
        gruenderzuschussNovelTextBubble.SetActive(true);
        honorarNovelTextBubble.SetActive(false);
        lebenspartnerNovelTextBubble.SetActive(false);
        introNovelTextBubble.SetActive(false);
        currentOpenBubble = gruenderzuschussNovelTextBubble;
    }

    public void OnPlayGruenderzuschussNovelButton()
    {
        LoadNovel(8);
    }

    public void OnHonorarNovelButton()
    {
        if (currentOpenBubble == honorarNovelTextBubble)
        {
            honorarNovelTextBubble.SetActive(false);
            currentOpenBubble = null;
            return;
        }
        bankkreditNovelTextBubble.SetActive(false);
        bekannteTreffenNovelTextBubble.SetActive(false);
        bankkontoNovelTextBubble.SetActive(false);
        foerderantragNovelTextBubble.SetActive(false);
        elternNovelTextBubble.SetActive(false);
        notariatNovelTextBubble.SetActive(false);
        presseNovelTextBubble.SetActive(false);
        bueroNovelTextBubble.SetActive(false);
        gruenderzuschussNovelTextBubble.SetActive(false);
        honorarNovelTextBubble.SetActive(true);
        lebenspartnerNovelTextBubble.SetActive(false);
        introNovelTextBubble.SetActive(false);
        currentOpenBubble = honorarNovelTextBubble;
    }

    public void OnPlayHonorarNovelButton()
    {
        LoadNovel(11);
    }

    public void OnLebenspartnerNovelButton()
    {
        if (currentOpenBubble == lebenspartnerNovelTextBubble)
        {
            lebenspartnerNovelTextBubble.SetActive(false);
            currentOpenBubble = null;
            return;
        }
        bankkreditNovelTextBubble.SetActive(false);
        bekannteTreffenNovelTextBubble.SetActive(false);
        bankkontoNovelTextBubble.SetActive(false);
        foerderantragNovelTextBubble.SetActive(false);
        elternNovelTextBubble.SetActive(false);
        notariatNovelTextBubble.SetActive(false);
        presseNovelTextBubble.SetActive(false);
        bueroNovelTextBubble.SetActive(false);
        gruenderzuschussNovelTextBubble.SetActive(false);
        honorarNovelTextBubble.SetActive(false);
        lebenspartnerNovelTextBubble.SetActive(true);
        introNovelTextBubble.SetActive(false);
        currentOpenBubble = lebenspartnerNovelTextBubble;
    }

    public void OnPlayLebenspartnerNovelButton()
    {
        LoadNovel(-10);
    }

    public void OnIntroNovelButton()
    {
        if (currentOpenBubble == introNovelTextBubble)
        {
            introNovelTextBubble.SetActive(false);
            currentOpenBubble = null;
            return;
        }
        bankkreditNovelTextBubble.SetActive(false);
        bekannteTreffenNovelTextBubble.SetActive(false);
        bankkontoNovelTextBubble.SetActive(false);
        foerderantragNovelTextBubble.SetActive(false);
        elternNovelTextBubble.SetActive(false);
        notariatNovelTextBubble.SetActive(false);
        presseNovelTextBubble.SetActive(false);
        bueroNovelTextBubble.SetActive(false);
        gruenderzuschussNovelTextBubble.SetActive(false);
        honorarNovelTextBubble.SetActive(false);
        lebenspartnerNovelTextBubble.SetActive(false);
        introNovelTextBubble.SetActive(true);
        currentOpenBubble = introNovelTextBubble;
    }

    public void OnPlayIntroNovelButton()
    {
        LoadNovel(13);
    }

    public void LoadNovel(int novelId)
    {
        List<VisualNovel> allNovels = KiteNovelManager.Instance().GetAllKiteNovels();

        foreach (VisualNovel novel in allNovels)
        {
            if (novel.id == novelId)
            {
                PlayManager.Instance().SetVisualNovelToPlay(novel);
                GameObject buttonSound = Instantiate(selectNovelSoundPrefab);
                DontDestroyOnLoad(buttonSound);
                SceneLoader.LoadDetailsViewScene();
                return;
            }
        }
        this.DisplayErrorMessage(ErrorMessages.NOVEL_NOT_FOUND);
    }
}
