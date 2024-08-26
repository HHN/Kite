using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    [SerializeField] private bool finishedInitialization;

    [SerializeField] private Button novelListButton;
    [SerializeField] private Button settingsButton;

    [SerializeField] private GameObject burgerMenu;
    [SerializeField] private bool isBurgerMenuOpen;

    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private List<GameObject> buttonContainers;
    private List<GameObject> originalOrder;

    [SerializeField] private Button bankkreditButtonFromBurgerMenu;
    [SerializeField] private Button elternButtonFromBurgerMenu;
    [SerializeField] private Button notarinButtonFromBurgerMenu;
    [SerializeField] private Button presseButtonFromBurgerMenu;
    [SerializeField] private Button bueroButtonFromBurgerMenu;
    [SerializeField] private Button introButtonFromBurgerMenu;

    [SerializeField] private GameObject selectNovelSoundPrefab;

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

        novelListButton.onClick.AddListener(delegate { OnNovelListButton(); });
        //searchButton.onClick.AddListener(delegate { OnSearchButton(); });
        settingsButton.onClick.AddListener(delegate { OnSettingsButton(); });

        bankkreditButtonFromBurgerMenu.onClick.AddListener(delegate { OnBankkreditButtonFromBurgerMenu(); });
        elternButtonFromBurgerMenu.onClick.AddListener(delegate { OnElternButtonFromBurgerMenu(); });
        notarinButtonFromBurgerMenu.onClick.AddListener(delegate { OnNotarinButtonFromBurgerMenu(); });
        presseButtonFromBurgerMenu.onClick.AddListener(delegate { OnPresseButtonFromBurgerMenu(); });
        bueroButtonFromBurgerMenu.onClick.AddListener(delegate { OnBueroButtonFromBurgerMenu(); });
        introButtonFromBurgerMenu.onClick.AddListener(delegate { OnIntroButtonFromBurgerMenu(); });

        if (inputField != null)
        {
            // Füge den Listener für Änderungen am Text des InputFields hinzu
            inputField.onValueChanged.AddListener(OnInputValueChanged);
        }
        else
        {
            Debug.LogError("InputField ist nicht zugewiesen.");
        }

        if (buttonContainers != null && buttonContainers.Count > 0)
        {
            // Speichere die ursprüngliche Reihenfolge der Container
            originalOrder = new List<GameObject>(buttonContainers);
        }
        else
        {
            Debug.LogError("Die Button-Container-Liste ist nicht zugewiesen oder leer.");
        }
    }

    private void OnInputValueChanged(string input)
    {
        // Erstelle eine temporäre Liste, um die Container neu anzuordnen
        List<GameObject> visibleContainers = new List<GameObject>();
        List<GameObject> hiddenContainers = new List<GameObject>();

        foreach (var container in originalOrder)
        {
            if (container != null)
            {
                // Hole den Button innerhalb des Containers
                Button button = container.GetComponentInChildren<Button>();

                if (button != null)
                {
                    // Hole den TextMeshPro-Text, der im Button enthalten ist
                    TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();

                    if (buttonText != null)
                    {
                        // Vergleiche den Button-Text mit dem Input
                        if (buttonText.text.ToLower().Contains(input.ToLower()))
                        {
                            // Container sichtbar machen und zur Liste der sichtbaren Container hinzufügen
                            container.SetActive(true);
                            visibleContainers.Add(container);
                        }
                        else
                        {
                            // Container deaktivieren und zur Liste der versteckten Container hinzufügen
                            container.SetActive(false);
                            hiddenContainers.Add(container);
                        }
                    }
                }
            }
        }

        // Setze die Reihenfolge der Container in der Liste neu
        buttonContainers.Clear();
        buttonContainers.AddRange(visibleContainers);
        buttonContainers.AddRange(hiddenContainers);

        // Aktualisiere die Reihenfolge der Container in der UI
        for (int i = 0; i < buttonContainers.Count; i++)
        {
            buttonContainers[i].transform.SetSiblingIndex(i + 1); // +1, um das InputField oben zu halten
        }
    }

    public void OnBackgroundButton()
    {
        if (isBurgerMenuOpen)
        {
            this.burgerMenu.gameObject.SetActive(false);
            isBurgerMenuOpen = false;
            return;
        }
        if (isPopupOpen)
        {
            MakeTextboxInvisible();
        }
    }

    public void OnFoundersWellButton()
    {
        if (isBurgerMenuOpen)
        {
            this.burgerMenu.gameObject.SetActive(false);
            isBurgerMenuOpen = false;
            return;
        }
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

    private void OnNovelListButton()
    {
        if (isBurgerMenuOpen)
        {
            this.burgerMenu.gameObject.SetActive(false);
            isBurgerMenuOpen = false;
            return;
        }
        isBurgerMenuOpen = true;
        this.burgerMenu.gameObject.SetActive(true);
    }   

    private void OnSettingsButton()
    {
        SceneLoader.LoadEinstellungenScene();
    }


    public void DisplayTextBoxForVisualNovel(VisualNovelNames visualNovel, bool isNovelContainedInVersion)
    {
        if (isBurgerMenuOpen)
        {
            this.burgerMenu.gameObject.SetActive(false);
            isBurgerMenuOpen = false;
        }
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

    public void OnBankkreditButtonFromBurgerMenu()
    {
        VisualNovel visualNovelToDisplay = null;
        VisualNovelNames visualNovelName = VisualNovelNames.BANK_KREDIT_NOVEL;

        KiteNovelManager.Instance().GetAllKiteNovels().ForEach(kiteNovel =>
        {
            if (VisualNovelNamesHelper.ValueOf((int) kiteNovel.id) == visualNovelName)
            {
                visualNovelToDisplay = kiteNovel;
            }
        });

        if (visualNovelToDisplay == null)
        {
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
        return;
    }

    public void OnElternButtonFromBurgerMenu()
    {
        VisualNovel visualNovelToDisplay = null;
        VisualNovelNames visualNovelName = VisualNovelNames.ELTERN_NOVEL;

        KiteNovelManager.Instance().GetAllKiteNovels().ForEach(kiteNovel =>
        {
            if (VisualNovelNamesHelper.ValueOf((int)kiteNovel.id) == visualNovelName)
            {
                visualNovelToDisplay = kiteNovel;
            }
        });

        if (visualNovelToDisplay == null)
        {
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
        return;
    }

    public void OnNotarinButtonFromBurgerMenu()
    {
        VisualNovel visualNovelToDisplay = null;
        VisualNovelNames visualNovelName = VisualNovelNames.NOTARIAT_NOVEL;

        KiteNovelManager.Instance().GetAllKiteNovels().ForEach(kiteNovel =>
        {
            if (VisualNovelNamesHelper.ValueOf((int)kiteNovel.id) == visualNovelName)
            {
                visualNovelToDisplay = kiteNovel;
            }
        });

        if (visualNovelToDisplay == null)
        {
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
        return;
    }

    public void OnPresseButtonFromBurgerMenu()
    {
        VisualNovel visualNovelToDisplay = null;
        VisualNovelNames visualNovelName = VisualNovelNames.PRESSE_NOVEL;

        KiteNovelManager.Instance().GetAllKiteNovels().ForEach(kiteNovel =>
        {
            if (VisualNovelNamesHelper.ValueOf((int)kiteNovel.id) == visualNovelName)
            {
                visualNovelToDisplay = kiteNovel;
            }
        });

        if (visualNovelToDisplay == null)
        {
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
        return;
    }

    public void OnBueroButtonFromBurgerMenu()
    {
        VisualNovel visualNovelToDisplay = null;
        VisualNovelNames visualNovelName = VisualNovelNames.BUERO_NOVEL;

        KiteNovelManager.Instance().GetAllKiteNovels().ForEach(kiteNovel =>
        {
            if (VisualNovelNamesHelper.ValueOf((int)kiteNovel.id) == visualNovelName)
            {
                visualNovelToDisplay = kiteNovel;
            }
        });

        if (visualNovelToDisplay == null)
        {
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
        return;
    }

    public void OnIntroButtonFromBurgerMenu()
    {
        VisualNovel visualNovelToDisplay = null;
        VisualNovelNames visualNovelName = VisualNovelNames.INTRO_NOVEL;

        KiteNovelManager.Instance().GetAllKiteNovels().ForEach(kiteNovel =>
        {
            if (VisualNovelNamesHelper.ValueOf((int)kiteNovel.id) == visualNovelName)
            {
                visualNovelToDisplay = kiteNovel;
            }
        });

        if (visualNovelToDisplay == null)
        {
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
        return;
    }
}
