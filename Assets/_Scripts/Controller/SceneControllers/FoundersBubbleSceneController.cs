using System.Collections.Generic;
using Assets._Scripts.Managers;
using Assets._Scripts.Novel;
using Assets._Scripts.Player;
using Assets._Scripts.SceneManagement;
using Assets._Scripts.UIElements.FoundersBubble;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Controller.SceneControllers
{
    public class FoundersBubbleSceneController : SceneController
    {
        [Header("Novel Description Textbox")]
        
        [SerializeField] private NovelDescriptionTextbox novelDescriptionTextbox;
        [SerializeField] private NovelDescriptionTextbox novelDescriptionTextboxIntro;
        [SerializeField] private bool isPopupOpen;
        [SerializeField] private VisualNovelNames currentlyOpenedVisualNovelPopup;

        [Header("Infinity Scroll")] [SerializeField]
        private InfinityScroll infinityScroll;

        [Header("Founder's Well Button")] [SerializeField]
        private Button foundersWellButton;

        [Header("Novels Contained in Version")] 
        [SerializeField] private bool isIntroNovelNovelInVersionContained;
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
        [SerializeField] private bool isInvestorNovelInVersionContained;
        [SerializeField] private bool isVertriebNovelInVersionContained;

        [Header("General Buttons")] [SerializeField]
        private Button novelListButton;

        [SerializeField] private Button settingsButton;

        [Header("Burger Menu")] [SerializeField]
        private GameObject burgerMenu;

        [SerializeField] private bool isBurgerMenuOpen;
        [SerializeField] private Button burgerMenuBackground;

        [Header("Burger Menu Buttons")] 
        [SerializeField] private Button introNovelButtonFromBurgerMenu;
        [SerializeField] private Button bankkreditNovelButtonFromBurgerMenu;
        [SerializeField] private Button elternNovelButtonFromBurgerMenu;
        [SerializeField] private Button notarinNovelButtonFromBurgerMenu;
        [SerializeField] private Button presseNovelButtonFromBurgerMenu;
        [SerializeField] private Button bueroNovelButtonFromBurgerMenu;
        [SerializeField] private Button honorarNovelButtonFromBurgerMenu;
        [SerializeField] private Button investorNovelButtonFromBurgerMenu;
        [SerializeField] private Button vertriebNovelButtonFromBurgerMenu;

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
            BackStackManager.Instance().Push(SceneNames.FoundersBubbleScene);

            currentlyOpenedVisualNovelPopup = VisualNovelNames.None;

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
            isInvestorNovelInVersionContained = true;
            isVertriebNovelInVersionContained = true;

            novelListButton.onClick.AddListener(OnNovelListButton);
            settingsButton.onClick.AddListener(OnSettingsButton);

            introNovelButtonFromBurgerMenu.onClick.AddListener(OnIntroButtonFromBurgerMenu);
            bankkreditNovelButtonFromBurgerMenu.onClick.AddListener(OnBankkreditButtonFromBurgerMenu);
            elternNovelButtonFromBurgerMenu.onClick.AddListener(OnElternButtonFromBurgerMenu);
            notarinNovelButtonFromBurgerMenu.onClick.AddListener(OnNotarinButtonFromBurgerMenu);
            presseNovelButtonFromBurgerMenu.onClick.AddListener(OnPresseButtonFromBurgerMenu);
            bueroNovelButtonFromBurgerMenu.onClick.AddListener(OnBueroButtonFromBurgerMenu);
            investorNovelButtonFromBurgerMenu.onClick.AddListener(OnInvestorNovelButtonFromBurgerMenu);
            honorarNovelButtonFromBurgerMenu.onClick.AddListener(OnHonorarNovelButtonFromBurgerMenu);
            vertriebNovelButtonFromBurgerMenu.onClick.AddListener(OnVertriebNovelButtonFromBurgerMenu);
            burgerMenuBackground.onClick.AddListener(OnBackgroundButton);

            // if (inputField != null)
            // {
            //     // Füge den Listener für Änderungen am Text des InputFields hinzu
            //     inputField.onValueChanged.AddListener(OnInputValueChanged);
            // }
            // else
            // {
            //     Debug.LogError("InputField ist nicht zugewiesen.");
            // }

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
            string inputLower = input.ToLower();
            
            // Erstelle eine temporäre Liste, um die Container neu anzuordnen
            List<GameObject> visibleContainers = new List<GameObject>();
            List<GameObject> hiddenContainers = new List<GameObject>();

            foreach (var container in _originalOrder)
            {
                if (container == null) continue;
                
                // Hole den Button innerhalb des Containers
                Button button = container.GetComponentInChildren<Button>();
                if (button == null) continue;
                
                // Hole den TextMeshPro-Text, der im Button enthalten ist
                TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
                if (buttonText == null) continue;
                
                // Vergleiche den Button-Text mit dem Input
                if (buttonText.text.ToLower().Contains(inputLower))
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

            // Setze die Reihenfolge der Container in der Liste neu
            buttonContainers.Clear();
            buttonContainers.AddRange(visibleContainers);
            buttonContainers.AddRange(hiddenContainers);

            // Aktualisiere die Reihenfolge der Container in der UI
            for (int i = 0; i < buttonContainers.Count; i++)
            {
                buttonContainers[i].transform.SetSiblingIndex(i /*+ 1*/); // +1, um das InputField oben zu halten
            }
            // inputField.transform.SetSiblingIndex(1);
        }

        public void OnBackgroundButton()
        {
            CloseBurgerMenuIfOpen();

            if (isPopupOpen)
            {
                MakeTextboxInvisible();
            }
        }

        public void OnBankkreditNovelButton()
        {
            DisplayTextBoxForVisualNovel(VisualNovelNames.BankKreditNovel, isBankkreditNovelInVersionContained);
            infinityScroll.MoveToVisualNovel(VisualNovelNames.BankKreditNovel);
        }

        public void OnInvestorNovelButton()
        {
            DisplayTextBoxForVisualNovel(VisualNovelNames.InvestorNovel,
                isInvestorNovelInVersionContained);
            infinityScroll.MoveToVisualNovel(VisualNovelNames.InvestorNovel);
        }

        // public void OnBankKontoNovelButton()
        // {
        //     DisplayTextBoxForVisualNovel(VisualNovelNames.BANK_KONTO_NOVEL, isBankkontoNovelInVersionContained);
        //     infinityScroll.MoveToVisualNovel(VisualNovelNames.BANK_KONTO_NOVEL);
        // }

        // public void OnFoerderantragNovelButton()
        // {
        //     DisplayTextBoxForVisualNovel(VisualNovelNames.FOERDERANTRAG_NOVEL, isFoerderantragNovelInVersionContained);
        //     infinityScroll.MoveToVisualNovel(VisualNovelNames.FOERDERANTRAG_NOVEL);
        // }

        public void OnElternNovelButton()
        {
            DisplayTextBoxForVisualNovel(VisualNovelNames.ElternNovel, isElternNovelInVersionContained);
            infinityScroll.MoveToVisualNovel(VisualNovelNames.ElternNovel);
        }

        public void OnNotariatNovelButton()
        {
            DisplayTextBoxForVisualNovel(VisualNovelNames.NotariatNovel, isNotarinNovelInVersionContained);
            infinityScroll.MoveToVisualNovel(VisualNovelNames.NotariatNovel);
        }

        public void OnPresseNovelButton()
        {
            DisplayTextBoxForVisualNovel(VisualNovelNames.PresseNovel, isPresseNovelInVersionContained);
            infinityScroll.MoveToVisualNovel(VisualNovelNames.PresseNovel);
        }

        public void OnBueroNovelButton()
        {
            DisplayTextBoxForVisualNovel(VisualNovelNames.VermieterNovel, isBueroNovelInVersionContained);
            infinityScroll.MoveToVisualNovel(VisualNovelNames.VermieterNovel);
        }

        // public void OnGruenderzuschussNovelButton()
        // {
        //     DisplayTextBoxForVisualNovel(VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL,
        //         isGruendungszuschussNovelInVersionContained);
        //     infinityScroll.MoveToVisualNovel(VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL);
        // }

        public void OnHonorarNovelButton()
        {
            DisplayTextBoxForVisualNovel(VisualNovelNames.HonorarNovel, isHonorarNovelInVersionContained);
            infinityScroll.MoveToVisualNovel(VisualNovelNames.HonorarNovel);
        }

        // public void OnLebenspartnerNovelButton()
        // {
        //     DisplayTextBoxForVisualNovel(VisualNovelNames.LEBENSPARTNER_NOVEL, isLebenspartnerNovelInVersionContained);
        //     infinityScroll.MoveToVisualNovel(VisualNovelNames.LEBENSPARTNER_NOVEL);
        // }

        public void OnIntroNovelButton()
        {
            DisplayTextBoxForVisualNovel(VisualNovelNames.EinstiegsNovel, isIntroNovelNovelInVersionContained);
            infinityScroll.MoveToVisualNovel(VisualNovelNames.EinstiegsNovel);
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
            SceneLoader.LoadSettingsScene();
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
                novelDescriptionTextbox.gameObject.SetActive(true);
                novelDescriptionTextbox.SetHead(FoundersBubbleMetaInformation.IsHighInGui(visualNovel));
                novelDescriptionTextbox.SetVisualNovelName(visualNovel);
                novelDescriptionTextbox.SetText("Leider ist diese Novel nicht in der Testversion enthalten. Bitte spiele eine andere Novel.");
                novelDescriptionTextbox.SetColorOfImage(FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(visualNovel));
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
                        novelDescriptionTextbox.gameObject.SetActive(false);
                        novelDescriptionTextboxIntro.gameObject.SetActive(true);
                        novelDescriptionTextboxIntro.SetHead(FoundersBubbleMetaInformation.IsHighInGui(visualNovel));
                        novelDescriptionTextboxIntro.SetVisualNovel(novel);
                        novelDescriptionTextboxIntro.SetVisualNovelName(visualNovel);
                        novelDescriptionTextboxIntro.SetText(novel.description);
                        novelDescriptionTextboxIntro.SetColorOfImage(FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(visualNovel));
                        novelDescriptionTextboxIntro.SetButtonsActive(true);
                        novelDescriptionTextboxIntro.UpdateSize();
                    }
                    else
                    {
                        novelDescriptionTextboxIntro.gameObject.SetActive(false);
                        novelDescriptionTextbox.gameObject.SetActive(true);
                        novelDescriptionTextbox.SetHead(FoundersBubbleMetaInformation.IsHighInGui(visualNovel));
                        novelDescriptionTextbox.SetVisualNovel(novel);
                        novelDescriptionTextbox.SetVisualNovelName(visualNovel);
                        novelDescriptionTextbox.SetText(novel.description);
                        novelDescriptionTextbox.SetColorOfImage(FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(visualNovel));
                        novelDescriptionTextbox.SetButtonsActive(true);
                        novelDescriptionTextbox.InitializeBookMarkButton(FavoritesManager.Instance().IsFavorite(novel));
                        novelDescriptionTextbox.UpdateSize();
                    }

                    isPopupOpen = true;
                    currentlyOpenedVisualNovelPopup = visualNovel;
                    NovelColorManager.Instance().SetColor(FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(visualNovel));
                }
            }

            FontSizeManager.Instance().UpdateAllTextComponents();
        }

        public void MakeTextboxInvisible()
        {
            isPopupOpen = false;
            currentlyOpenedVisualNovelPopup = VisualNovelNames.None;
            novelDescriptionTextbox.gameObject.SetActive(false);
            novelDescriptionTextboxIntro.gameObject.SetActive(false);
        }

        public override void OnStop()
        {
            base.OnStop();
            SceneMemoryManager.Instance().SetMemoryOfFoundersBubbleScene(infinityScroll.GetCurrentScrollPosition());
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
            DisplayNovelFromMenu(VisualNovelNames.EinstiegsNovel);
        }

        private void OnBankkreditButtonFromBurgerMenu()
        {
            DisplayNovelFromMenu(VisualNovelNames.BankKreditNovel);
        }

        private void OnElternButtonFromBurgerMenu()
        {
            DisplayNovelFromMenu(VisualNovelNames.ElternNovel);
        }

        private void OnNotarinButtonFromBurgerMenu()
        {
            DisplayNovelFromMenu(VisualNovelNames.NotariatNovel);
        }

        private void OnPresseButtonFromBurgerMenu()
        {
            DisplayNovelFromMenu(VisualNovelNames.PresseNovel);
        }

        private void OnBueroButtonFromBurgerMenu()
        {
            DisplayNovelFromMenu(VisualNovelNames.VermieterNovel);
        }

        private void OnHonorarNovelButtonFromBurgerMenu()
        {
            DisplayNovelFromMenu(VisualNovelNames.HonorarNovel);
        }

        private void OnInvestorNovelButtonFromBurgerMenu()
        {
            DisplayNovelFromMenu(VisualNovelNames.InvestorNovel);
        }
        
        private void OnVertriebNovelButtonFromBurgerMenu()
        {
            DisplayNovelFromMenu(VisualNovelNames.VertriebNovel);
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
            NovelColorManager.Instance().SetColor(FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(VisualNovelNamesHelper.ValueOf((int)visualNovelToDisplay.id)));
            PlayManager.Instance().SetForegroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetForegroundColorOfNovel(visualNovelName));
            PlayManager.Instance().SetBackgroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(visualNovelName));
            PlayManager.Instance().SetDisplayNameOfNovelToPlay(FoundersBubbleMetaInformation.GetDisplayNameOfNovelToPlay(visualNovelName));
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