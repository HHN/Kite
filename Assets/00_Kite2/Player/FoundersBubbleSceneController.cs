using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoundersBubbleSceneController : SceneController
{
    [SerializeField] private GameObject novelDescriptionTextboxGameObject;
    [SerializeField] private NovelDescriptionTextbox novelDescriptionTextbox;
    [SerializeField] private InfinityScroll infinityScroll;
    [SerializeField] private Button foundersWellButton;
    [SerializeField] private bool isPopupOpen;
    [SerializeField] private VisualNovelNames currentlyOpenedVisualNovelPopup;

    [SerializeField] private bool isBankkreditNovelInVersionContained;
    [SerializeField] private bool isBekannteTreffenNovelInVersionContained;
    [SerializeField] private bool isBankkontoNovelInVersionContained;
    [SerializeField] private bool isFoerderantragNovelInVersionContained;
    [SerializeField] private bool isElternNovelInVersionContained;
    [SerializeField] private bool isNotarinNovelInVersionContained;
    [SerializeField] private bool isPresseNovelInVersionContained;
    [SerializeField] private bool isBueroNovelInVersionContained;
    [SerializeField] private bool isGruendungszuschussNovelInVersionContained;
    [SerializeField] private bool isHonorarNovelInVersionContained;
    [SerializeField] private bool isLebnenspartnerNovelInVersionContained;
    [SerializeField] private bool isIntroNovelNovelInVersionContained;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.FOUNDERS_BUBBLE_SCENE);

        foundersWellButton.onClick.AddListener(delegate { OnFoundersWellButton(); });

        currentlyOpenedVisualNovelPopup = VisualNovelNames.NONE;

        isBankkreditNovelInVersionContained = true;
        isBekannteTreffenNovelInVersionContained = true;
        isBankkontoNovelInVersionContained = true;
        isFoerderantragNovelInVersionContained = true;
        isElternNovelInVersionContained = true;
        isNotarinNovelInVersionContained = true;
        isPresseNovelInVersionContained = true;
        isBueroNovelInVersionContained = true;
        isGruendungszuschussNovelInVersionContained = true;
        isHonorarNovelInVersionContained = true;
        isLebnenspartnerNovelInVersionContained = true;
        isIntroNovelNovelInVersionContained = true;
    }

    public void OnBackgroundButton()
    {
        if (isPopupOpen)
        {
            MakeTextboxInvisible();
        }
    }

    public void OnFoundersWellButton()
    {
        if (isPopupOpen)
        {
            MakeTextboxInvisible();
            return;
        }
        //SceneLoader.LoadFoundersWellScene();
        SceneLoader.LoadFoundersWell2Scene();
    }

    public void OnBankkreditNovelButton()
    {
        DisplayTextBoxForVisualNovel(VisualNovelNames.BANK_KREDIT_NOVEL, isBankkreditNovelInVersionContained);
        infinityScroll.MoveToVisualNovel(VisualNovelNames.BANK_KREDIT_NOVEL);
    }

    public void OnBekannteTreffenNovelButton()
    {
        DisplayTextBoxForVisualNovel(VisualNovelNames.BEKANNTE_TREFFEN_NOVEL, isBekannteTreffenNovelInVersionContained);
        infinityScroll.MoveToVisualNovel(VisualNovelNames.BEKANNTE_TREFFEN_NOVEL);
    }

    public void OnBankKontoNovelButton()
    {
        DisplayTextBoxForVisualNovel(VisualNovelNames.BANK_KONTO_NOVEL, isBankkontoNovelInVersionContained);
        infinityScroll.MoveToVisualNovel(VisualNovelNames.BANK_KONTO_NOVEL);
    }

    public void OnFoerderantragNovelButton()
    {
        DisplayTextBoxForVisualNovel(VisualNovelNames.FOERDERANTRAG_NOVEL, isFoerderantragNovelInVersionContained);
        infinityScroll.MoveToVisualNovel(VisualNovelNames.FOERDERANTRAG_NOVEL);
    }

    public void OnElternNovelButton()
    {
        DisplayTextBoxForVisualNovel(VisualNovelNames.ELTERN_NOVEL, isElternNovelInVersionContained);
        infinityScroll.MoveToVisualNovel(VisualNovelNames.ELTERN_NOVEL);
    }

    public void OnNotariatNovelButton()
    {
        DisplayTextBoxForVisualNovel(VisualNovelNames.NOTARIAT_NOVEL, isNotarinNovelInVersionContained);
        infinityScroll.MoveToVisualNovel(VisualNovelNames.NOTARIAT_NOVEL);
    }

    public void OnPresseNovelButton()
    {
        DisplayTextBoxForVisualNovel(VisualNovelNames.PRESSE_NOVEL, isPresseNovelInVersionContained);
        infinityScroll.MoveToVisualNovel(VisualNovelNames.PRESSE_NOVEL);
    }

    public void OnBueroNovelButton()
    {
        DisplayTextBoxForVisualNovel(VisualNovelNames.BUERO_NOVEL, isBueroNovelInVersionContained);
        infinityScroll.MoveToVisualNovel(VisualNovelNames.BUERO_NOVEL);
    }

    public void OnGruenderzuschussNovelButton()
    {
        DisplayTextBoxForVisualNovel(VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL, isGruendungszuschussNovelInVersionContained);
        infinityScroll.MoveToVisualNovel(VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL);
    }

    public void OnHonorarNovelButton()
    {
        DisplayTextBoxForVisualNovel(VisualNovelNames.HONORAR_NOVEL, isHonorarNovelInVersionContained);
        infinityScroll.MoveToVisualNovel(VisualNovelNames.HONORAR_NOVEL);
    }

    public void OnLebenspartnerNovelButton()
    {
        DisplayTextBoxForVisualNovel(VisualNovelNames.LEBENSPARTNER_NOVEL, isLebnenspartnerNovelInVersionContained);
        infinityScroll.MoveToVisualNovel(VisualNovelNames.LEBENSPARTNER_NOVEL);
    }

    public void OnIntroNovelButton()
    {
        DisplayTextBoxForVisualNovel(VisualNovelNames.INTRO_NOVEL, isIntroNovelNovelInVersionContained);
        infinityScroll.MoveToVisualNovel(VisualNovelNames.INTRO_NOVEL);
    }

    public void DisplayTextBoxForVisualNovel(VisualNovelNames visualNovel, bool isNovelContainedInVersion)
    {
        if (isPopupOpen && visualNovel == currentlyOpenedVisualNovelPopup)
        {
            MakeTextboxInvisible();
            return;
        }
        if (!isNovelContainedInVersion)
        {
            novelDescriptionTextboxGameObject.SetActive(true);
            novelDescriptionTextbox.SetHead(FoundersBubbleMetaInformation.IsHighInGui(visualNovel));
            novelDescriptionTextbox.SetVisualNovelName(visualNovel);
            novelDescriptionTextbox.SetText("Leider ist diese Novel nicht in der Testversion enthalten. Bitte spiele eine andere Novel.");
            novelDescriptionTextbox.SetColorOfImage(FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(visualNovel));
            novelDescriptionTextbox.SetButtonsActive(false);
            isPopupOpen = true;
            currentlyOpenedVisualNovelPopup = visualNovel;
            return;
        }
        int novelId = VisualNovelNamesHelper.ToInt(visualNovel);

        List<VisualNovel> allNovels = KiteNovelManager.Instance().GetAllKiteNovels();

        foreach (VisualNovel novel in allNovels)
        {
            if (novel.id == novelId)
            {
                novelDescriptionTextboxGameObject.SetActive(true);
                novelDescriptionTextbox.SetHead(FoundersBubbleMetaInformation.IsHighInGui(visualNovel));
                novelDescriptionTextbox.SetVisualNovel(novel);
                novelDescriptionTextbox.SetVisualNovelName(visualNovel);
                novelDescriptionTextbox.SetText(novel.description);
                novelDescriptionTextbox.SetColorOfImage(FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(visualNovel));
                novelDescriptionTextbox.SetButtonsActive(true);
                novelDescriptionTextbox.InitializeBookMarkButton(FavoritesManager.Instance().IsFavorite(novel));


                isPopupOpen = true;
                currentlyOpenedVisualNovelPopup = visualNovel;
                NovelColorManager.Instance().SetColor(FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(visualNovel));
            }
        }
    }

    public void MakeTextboxInvisible()
    {
        isPopupOpen = false;
        currentlyOpenedVisualNovelPopup = VisualNovelNames.NONE;
        novelDescriptionTextboxGameObject.SetActive(false);
    }

    public override void OnStop()
    {
        base.OnStop();
        FoundersBubbleSceneMemory memory = new FoundersBubbleSceneMemory();
        memory.scrollPosition = infinityScroll.GetCurrentScrollPosition();
        SceneMemoryManager.Instance().SetMemoryOfFoundersBubbleScene(memory);
    }
}
