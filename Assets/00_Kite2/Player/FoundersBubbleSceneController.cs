using System.Collections.Generic;
using _00_Kite2.Common;
using _00_Kite2.Common.Managers;
using _00_Kite2.Common.Novel;
using _00_Kite2.Common.SceneManagement;
using _00_Kite2.Common.SceneMemory;
using _00_Kite2.Common.UI.Founders_Bubble.ScrollingBehavior;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.Player
{
    public class FoundersBubbleSceneController : SceneController
    {
        [Header("Novel Description Textbox")] [SerializeField]
        private GameObject novelDescriptionTextboxGameObject;

        [SerializeField] private NovelDescriptionTextbox novelDescriptionTextbox;
        [SerializeField] private GameObject novelDescriptionTextboxIntroGameObject;
        [SerializeField] private NovelDescriptionTextbox novelDescriptionTextboxIntro;
        [SerializeField] private bool isPopupOpen;
        [SerializeField] private VisualNovelNames currentlyOpenedVisualNovelPopup;

        [Header("Infinity Scroll")] [SerializeField]
        private InfinityScroll infinityScroll;

        [Header("Founder's Well Button")] [SerializeField]
        private Button foundersWellButton;

        [Header("Novels Contained in Version")] [SerializeField]
        private bool isIntroNovelNovelInVersionContained;

        [SerializeField] private bool isBankkreditNovelInVersionContained;
        [SerializeField] private bool isBankkontoNovelInVersionContained;
        [SerializeField] private bool isFoerderantragNovelInVersionContained;
        [SerializeField] private bool isElternNovelInVersionContained;
        [SerializeField] private bool isNotarinNovelInVersionContained;
        [SerializeField] private bool isPresseNovelInVersionContained;
        [SerializeField] private bool isBueroNovelInVersionContained;
        [SerializeField] private bool isGruendungszuschussNovelInVersionContained;
        [SerializeField] private bool isHonorarNovelInVersionContained;
        [SerializeField] private bool isLebenspartnerNovelInVersionContained;
        [SerializeField] private bool isBekanntenTreffenNovelInVersionContained;

        [Header("General Buttons")] [SerializeField]
        private Button novelListButton;

        [SerializeField] private Button settingsButton;

        [Header("Burger Menu")] [SerializeField]
        private GameObject burgerMenu;

        [SerializeField] private bool isBurgerMenuOpen;
        [SerializeField] private Button burgerMenuBackground;

        [Header("Burger Menu Buttons")] [SerializeField]
        private Button introNovelButtonFromBurgerMenu;

        [SerializeField] private Button bankkreditNovelButtonFromBurgerMenu;
        [SerializeField] private Button elternNovelButtonFromBurgerMenu;
        [SerializeField] private Button notarinNovelButtonFromBurgerMenu;
        [SerializeField] private Button presseNovelButtonFromBurgerMenu;
        [SerializeField] private Button bueroNovelButtonFromBurgerMenu;
        [SerializeField] private Button honorarNovelButtonFromBurgerMenu;
        [SerializeField] private Button bekanntenNovelButtonFromBurgerMenu;

        [Header("Search Input and Button Containers")] [SerializeField]
        private TMP_InputField inputField;

        [SerializeField] private List<GameObject> buttonContainers;

        [Header("Sound Prefab")] [SerializeField]
        private GameObject selectNovelSoundPrefab;

        [Header("Other Variables")] [SerializeField]
        private bool finishedInitialization;

        private int _novelId;
        private List<GameObject> _originalOrder;

        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.FOUNDERS_BUBBLE_SCENE);

            foundersWellButton.onClick.AddListener(OnFoundersWellButton);

            currentlyOpenedVisualNovelPopup = VisualNovelNames.NONE;

            isIntroNovelNovelInVersionContained = true;
            isBankkreditNovelInVersionContained = true;
            isBankkontoNovelInVersionContained = true;
            isFoerderantragNovelInVersionContained = true;
            isElternNovelInVersionContained = true;
            isNotarinNovelInVersionContained = true;
            isPresseNovelInVersionContained = true;
            isBueroNovelInVersionContained = true;
            isGruendungszuschussNovelInVersionContained = true;
            isHonorarNovelInVersionContained = true;
            isLebenspartnerNovelInVersionContained = true;
            isBekanntenTreffenNovelInVersionContained = true;

            novelListButton.onClick.AddListener(OnNovelListButton);
            settingsButton.onClick.AddListener(OnSettingsButton);

            introNovelButtonFromBurgerMenu.onClick.AddListener(OnIntroButtonFromBurgerMenu);
            bankkreditNovelButtonFromBurgerMenu.onClick.AddListener(OnBankkreditButtonFromBurgerMenu);
            elternNovelButtonFromBurgerMenu.onClick.AddListener(OnElternButtonFromBurgerMenu);
            notarinNovelButtonFromBurgerMenu.onClick.AddListener(OnNotarinButtonFromBurgerMenu);
            presseNovelButtonFromBurgerMenu.onClick.AddListener(OnPresseButtonFromBurgerMenu);
            bueroNovelButtonFromBurgerMenu.onClick.AddListener(OnBueroButtonFromBurgerMenu);
            bekanntenNovelButtonFromBurgerMenu.onClick.AddListener(OnBekannteNovelButtonFromBurgerMenu);
            honorarNovelButtonFromBurgerMenu.onClick.AddListener(OnHonorarNovelButtonFromBurgerMenu);
            burgerMenuBackground.onClick.AddListener(OnBackgroundButton);

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
                _originalOrder = new List<GameObject>(buttonContainers);
            }
            else
            {
                Debug.LogError("Die Button-Container-Liste ist nicht zugewiesen oder leer.");
            }
            StartCoroutine(TextToSpeechManager.Instance.Speak(" "));
            GlobalVolumeManager.Instance.StopSound();
        }

        private void OnInputValueChanged(string input)
        {
            // Erstelle eine temporäre Liste, um die Container neu anzuordnen
            List<GameObject> visibleContainers = new List<GameObject>();
            List<GameObject> hiddenContainers = new List<GameObject>();

            foreach (var container in _originalOrder)
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
            CloseBurgerMenuIfOpen();

            if (isPopupOpen)
            {
                MakeTextboxInvisible();
            }
        }

        private void OnFoundersWellButton()
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
            DisplayTextBoxForVisualNovel(VisualNovelNames.BEKANNTEN_TREFFEN_NOVEL,
                isBekanntenTreffenNovelInVersionContained);
            infinityScroll.MoveToVisualNovel(VisualNovelNames.BEKANNTEN_TREFFEN_NOVEL);
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
            DisplayTextBoxForVisualNovel(VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL,
                isGruendungszuschussNovelInVersionContained);
            infinityScroll.MoveToVisualNovel(VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL);
        }

        public void OnHonorarNovelButton()
        {
            DisplayTextBoxForVisualNovel(VisualNovelNames.HONORAR_NOVEL, isHonorarNovelInVersionContained);
            infinityScroll.MoveToVisualNovel(VisualNovelNames.HONORAR_NOVEL);
        }

        public void OnLebenspartnerNovelButton()
        {
            DisplayTextBoxForVisualNovel(VisualNovelNames.LEBENSPARTNER_NOVEL, isLebenspartnerNovelInVersionContained);
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
            FontSizeManager.Instance().UpdateAllTextComponents();
        }

        private void OnSettingsButton()
        {
            SceneLoader.LoadEinstellungenScene();
        }

        private void DisplayTextBoxForVisualNovel(VisualNovelNames visualNovel, bool isNovelContainedInVersion)
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
                novelDescriptionTextbox.SetText(
                    "Leider ist diese Novel nicht in der Testversion enthalten. Bitte spiele eine andere Novel.");
                novelDescriptionTextbox.SetColorOfImage(
                    FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(visualNovel));
                novelDescriptionTextbox.SetButtonsActive(false);
                isPopupOpen = true;
                currentlyOpenedVisualNovelPopup = visualNovel;
                return;
            }

            _novelId = VisualNovelNamesHelper.ToInt(visualNovel);

            List<VisualNovel> allNovels = KiteNovelManager.Instance().GetAllKiteNovels();

            foreach (VisualNovel novel in allNovels)
            {
                if (novel.id == _novelId)
                {
                    //Debug.Log("title: " + novel.title);
                    //Debug.Log("description: " + novel.description);

                    if (novel.id == 13)
                    {
                        novelDescriptionTextboxGameObject.SetActive(false);
                        novelDescriptionTextboxIntroGameObject.SetActive(true);
                        novelDescriptionTextboxIntro.SetHead(FoundersBubbleMetaInformation.IsHighInGui(visualNovel));
                        novelDescriptionTextboxIntro.SetVisualNovel(novel);
                        novelDescriptionTextboxIntro.SetVisualNovelName(visualNovel);
                        novelDescriptionTextboxIntro.SetText(novel.description);
                        novelDescriptionTextboxIntro.SetColorOfImage(
                            FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(visualNovel));
                        novelDescriptionTextboxIntro.SetButtonsActive(true);
                        novelDescriptionTextboxIntro.UpdateSize();
                    }
                    else
                    {
                        novelDescriptionTextboxIntroGameObject.SetActive(false);
                        novelDescriptionTextboxGameObject.SetActive(true);
                        novelDescriptionTextbox.SetHead(FoundersBubbleMetaInformation.IsHighInGui(visualNovel));
                        novelDescriptionTextbox.SetVisualNovel(novel);
                        novelDescriptionTextbox.SetVisualNovelName(visualNovel);
                        novelDescriptionTextbox.SetText(novel.description);
                        novelDescriptionTextbox.SetColorOfImage(
                            FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(visualNovel));
                        novelDescriptionTextbox.SetButtonsActive(true);
                        novelDescriptionTextbox.InitializeBookMarkButton(FavoritesManager.Instance().IsFavorite(novel));
                        novelDescriptionTextbox.UpdateSize();
                    }

                    isPopupOpen = true;
                    currentlyOpenedVisualNovelPopup = visualNovel;
                    NovelColorManager.Instance()
                        .SetColor(FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(visualNovel));
                }
            }

            FontSizeManager.Instance().UpdateAllTextComponents();
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
            FoundersBubbleSceneMemory memory = new FoundersBubbleSceneMemory
            {
                ScrollPosition = infinityScroll.GetCurrentScrollPosition()
            };
            SceneMemoryManager.Instance().SetMemoryOfFoundersBubbleScene(memory);
        }

        private void ToggleBurgerMenu()
        {
            isBurgerMenuOpen = !isBurgerMenuOpen;
            burgerMenu.SetActive(isBurgerMenuOpen); // Zeige oder verstecke das Burger-Men�
            FontSizeManager.Instance().UpdateAllTextComponents();
        }

        private void CloseBurgerMenuIfOpen()
        {
            if (isBurgerMenuOpen)
            {
                burgerMenu.SetActive(false); // Verstecke das Burger-Men�
                isBurgerMenuOpen = false;
            }
        }

        private void OnIntroButtonFromBurgerMenu()
        {
            GameManager.Instance.IsIntroNovelLoadedFromMainMenu = false;
            DisplayNovelFromMenu(VisualNovelNames.INTRO_NOVEL);
        }

        private void OnBankkreditButtonFromBurgerMenu()
        {
            DisplayNovelFromMenu(VisualNovelNames.BANK_KREDIT_NOVEL);
        }

        private void OnElternButtonFromBurgerMenu()
        {
            DisplayNovelFromMenu(VisualNovelNames.ELTERN_NOVEL);
        }

        private void OnNotarinButtonFromBurgerMenu()
        {
            DisplayNovelFromMenu(VisualNovelNames.NOTARIAT_NOVEL);
        }

        private void OnPresseButtonFromBurgerMenu()
        {
            DisplayNovelFromMenu(VisualNovelNames.PRESSE_NOVEL);
        }

        private void OnBueroButtonFromBurgerMenu()
        {
            DisplayNovelFromMenu(VisualNovelNames.BUERO_NOVEL);
        }

        private void OnHonorarNovelButtonFromBurgerMenu()
        {
            DisplayNovelFromMenu(VisualNovelNames.HONORAR_NOVEL);
        }

        private void OnBekannteNovelButtonFromBurgerMenu()
        {
            DisplayNovelFromMenu(VisualNovelNames.BEKANNTEN_TREFFEN_NOVEL);
        }

        private void DisplayNovelFromMenu(VisualNovelNames visualNovelName)
        {
            VisualNovel visualNovelToDisplay = null;

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
            NovelColorManager.Instance()
                .SetColor(FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(
                    VisualNovelNamesHelper.ValueOf((int)visualNovelToDisplay.id)));
            PlayManager.Instance()
                .SetForegroundColorOfVisualNovelToPlay(
                    FoundersBubbleMetaInformation.GetForegroundColorOfNovel(visualNovelName));
            PlayManager.Instance()
                .SetBackgroundColorOfVisualNovelToPlay(
                    FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(visualNovelName));
            PlayManager.Instance()
                .SetDisplayNameOfNovelToPlay(
                    FoundersBubbleMetaInformation.GetDisplayNameOfNovelToPlay(visualNovelName));
            GameObject buttonSound = Instantiate(selectNovelSoundPrefab);
            DontDestroyOnLoad(buttonSound);

            if (ShowPlayInstructionManager.Instance().ShowInstruction() &&
                visualNovelToDisplay.title != "Einstiegsdialog")
            {
                SceneLoader.LoadPlayInstructionScene();
            }
            else
            {
                SceneLoader.LoadPlayNovelScene();
            }
        }
    }
}