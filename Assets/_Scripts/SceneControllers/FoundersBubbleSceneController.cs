using System.Collections.Generic;
using System.Linq;
using Assets._Scripts.Managers;
using Assets._Scripts.Novel;
using Assets._Scripts.Player;
using Assets._Scripts.SceneManagement;
using Assets._Scripts.SceneMemory;
using Assets._Scripts.UI_Elements.Founders_Bubble;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets._Scripts.SceneControllers
{
    [System.Serializable]
    public class NovelEntry
    {
        public long novelId;
        public bool isContained;
    }

    public class FoundersBubbleSceneController : SceneController
    {
        [Header("Novel Description Textbox")] [SerializeField]
        private NovelDescriptionTextbox novelDescriptionTextbox;

        [SerializeField] private NovelDescriptionTextbox novelDescriptionTextboxIntro;
        [SerializeField] private bool isPopupOpen;
        [SerializeField] private VisualNovelNames currentlyOpenedVisualNovelPopup;

        [Header("Infinity Scroll")] [SerializeField]
        private InfinityScroll infinityScroll;

        [Header("Founder's Well Button")] [SerializeField]
        private Button foundersWellButton;

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
        [SerializeField] private Button investorNovelButtonFromBurgerMenu;

        [Header("Search Input and Button Containers")] [SerializeField]
        private TMP_InputField inputField;

        [SerializeField] private List<GameObject> buttonContainers;

        [Header("Sound Prefab")] [SerializeField]
        private GameObject selectNovelSoundPrefab;

        [Header("Other Variables")] [SerializeField]
        private bool finishedInitialization;

        private int _novelId;
        private List<GameObject> _originalOrder;

        [SerializeField] private List<NovelEntry> _isNovelContainedInVersion;

        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.FoundersBubbleScene);

            foundersWellButton.onClick.AddListener(OnFoundersWellButton);

            currentlyOpenedVisualNovelPopup = VisualNovelNames.NONE;

            List<VisualNovel> allKiteNovels = KiteNovelManager.Instance().GetAllKiteNovels();

            _isNovelContainedInVersion = new List<NovelEntry>();
            foreach (var visualNovel in allKiteNovels)
            {
                NovelEntry novelEntry = new NovelEntry
                {
                    novelId = visualNovel.id,
                    isContained = true
                };

                _isNovelContainedInVersion.Add(novelEntry);
            }

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
                buttonContainers[i].transform.SetSiblingIndex(i + 1); // +1, um das InputField oben zu halten
            }

            inputField.transform.SetSiblingIndex(1);
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

            SceneLoader.LoadFoundersWell2Scene();
        }
        
        public void OnNovelButton()
        {
            GameObject buttonObject = EventSystem.current.currentSelectedGameObject;
            long novelNames = VisualNovelNamesHelper.GetId(buttonObject.name);
            
            //Neue Methode in VisualNovelNamesHelper, die aus einem string den dazugehörigen Enum-Wert zurückgibt
            // var entry = _isNovelContainedInVersion.FirstOrDefault(novel =>
            //     novel.novelId == (long)VisualNovelNamesHelper.ToInt(novelNames));
            //
            // if (entry == null) return;
            //
            // DisplayTextBoxForVisualNovel(novelNames, entry.isContained);
            // infinityScroll.MoveToVisualNovel(novelNames);
        }

        // public void OnBankkreditNovelButton()
        // {
        //     VisualNovelNames novelNames = VisualNovelNames.BANK_KREDIT_NOVEL;
        //     var entry = _isNovelContainedInVersion.FirstOrDefault(novel =>
        //         novel.novelId == (long)VisualNovelNamesHelper.ToInt(novelNames));
        //
        //     if (entry == null) return;
        //
        //     DisplayTextBoxForVisualNovel(novelNames, entry.isContained);
        //     infinityScroll.MoveToVisualNovel(novelNames);
        // }
        //
        // public void OnInvestorNovelButton()
        // {
        //     VisualNovelNames novelNames = VisualNovelNames.INVESTOR_NOVEL;
        //     var entry = _isNovelContainedInVersion.FirstOrDefault(novel =>
        //         novel.novelId == (long)VisualNovelNamesHelper.ToInt(novelNames));
        //
        //     if (entry == null) return;
        //
        //     DisplayTextBoxForVisualNovel(novelNames, entry.isContained);
        //     infinityScroll.MoveToVisualNovel(novelNames);
        // }
        //
        // public void OnBankKontoNovelButton()
        // {
        //     VisualNovelNames novelNames = VisualNovelNames.BANK_KONTO_NOVEL;
        //     var entry = _isNovelContainedInVersion.FirstOrDefault(novel =>
        //         novel.novelId == (long)VisualNovelNamesHelper.ToInt(novelNames));
        //
        //     if (entry == null) return;
        //
        //     DisplayTextBoxForVisualNovel(novelNames, entry.isContained);
        //     infinityScroll.MoveToVisualNovel(novelNames);
        // }
        //
        // public void OnFoerderantragNovelButton()
        // {
        //     VisualNovelNames novelNames = VisualNovelNames.FOERDERANTRAG_NOVEL;
        //     var entry = _isNovelContainedInVersion.FirstOrDefault(novel =>
        //         novel.novelId == (long)VisualNovelNamesHelper.ToInt(novelNames));
        //
        //     if (entry == null) return;
        //
        //     DisplayTextBoxForVisualNovel(novelNames, entry.isContained);
        //     infinityScroll.MoveToVisualNovel(novelNames);
        // }
        //
        // public void OnElternNovelButton()
        // {
        //     VisualNovelNames novelNames = VisualNovelNames.ELTERN_NOVEL;
        //     var entry = _isNovelContainedInVersion.FirstOrDefault(novel =>
        //         novel.novelId == (long)VisualNovelNamesHelper.ToInt(novelNames));
        //
        //     if (entry == null) return;
        //
        //     DisplayTextBoxForVisualNovel(novelNames, entry.isContained);
        //     infinityScroll.MoveToVisualNovel(novelNames);
        // }
        //
        // public void OnNotariatNovelButton()
        // {
        //     VisualNovelNames novelNames = VisualNovelNames.NOTARIAT_NOVEL;
        //     var entry = _isNovelContainedInVersion.FirstOrDefault(novel =>
        //         novel.novelId == (long)VisualNovelNamesHelper.ToInt(novelNames));
        //
        //     if (entry == null) return;
        //
        //     DisplayTextBoxForVisualNovel(novelNames, entry.isContained);
        //     infinityScroll.MoveToVisualNovel(novelNames);
        // }
        //
        // public void OnPresseNovelButton()
        // {
        //     VisualNovelNames novelNames = VisualNovelNames.PRESSE_NOVEL;
        //     var entry = _isNovelContainedInVersion.FirstOrDefault(novel =>
        //         novel.novelId == (long)VisualNovelNamesHelper.ToInt(novelNames));
        //
        //     if (entry == null) return;
        //
        //     DisplayTextBoxForVisualNovel(novelNames, entry.isContained);
        //     infinityScroll.MoveToVisualNovel(novelNames);
        // }
        //
        // public void OnBueroNovelButton()
        // {
        //     VisualNovelNames novelNames = VisualNovelNames.BUERO_NOVEL;
        //     var entry = _isNovelContainedInVersion.FirstOrDefault(novel =>
        //         novel.novelId == (long)VisualNovelNamesHelper.ToInt(novelNames));
        //
        //     if (entry == null) return;
        //
        //     DisplayTextBoxForVisualNovel(novelNames, entry.isContained);
        //     infinityScroll.MoveToVisualNovel(novelNames);
        // }
        //
        // public void OnGruenderzuschussNovelButton()
        // {
        //     VisualNovelNames novelNames = VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL;
        //     var entry = _isNovelContainedInVersion.FirstOrDefault(novel =>
        //         novel.novelId == (long)VisualNovelNamesHelper.ToInt(novelNames));
        //
        //     if (entry == null) return;
        //
        //     DisplayTextBoxForVisualNovel(novelNames, entry.isContained);
        //     infinityScroll.MoveToVisualNovel(novelNames);
        // }
        //
        // public void OnHonorarNovelButton()
        // {
        //     VisualNovelNames novelNames = VisualNovelNames.HONORAR_NOVEL;
        //     var entry = _isNovelContainedInVersion.FirstOrDefault(novel =>
        //         novel.novelId == (long)VisualNovelNamesHelper.ToInt(novelNames));
        //
        //     if (entry == null) return;
        //
        //     DisplayTextBoxForVisualNovel(novelNames, entry.isContained);
        //     infinityScroll.MoveToVisualNovel(novelNames);
        // }
        //
        // public void OnLebenspartnerNovelButton()
        // {
        //     VisualNovelNames novelNames = VisualNovelNames.LEBENSPARTNER_NOVEL;
        //     var entry = _isNovelContainedInVersion.FirstOrDefault(novel =>
        //         novel.novelId == (long)VisualNovelNamesHelper.ToInt(novelNames));
        //
        //     if (entry == null) return;
        //
        //     DisplayTextBoxForVisualNovel(novelNames, entry.isContained);
        //     infinityScroll.MoveToVisualNovel(novelNames);
        // }
        //
        // public void OnIntroNovelButton()
        // {
        //     VisualNovelNames novelNames = VisualNovelNames.INTRO_NOVEL;
        //     var entry = _isNovelContainedInVersion.FirstOrDefault(novel =>
        //         novel.novelId == (long)VisualNovelNamesHelper.ToInt(novelNames));
        //
        //     if (entry == null) return;
        //
        //     DisplayTextBoxForVisualNovel(novelNames, entry.isContained);
        //     infinityScroll.MoveToVisualNovel(novelNames);
        // }

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
                novelDescriptionTextbox.gameObject.SetActive(true);
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
                        novelDescriptionTextbox.gameObject.SetActive(false);
                        novelDescriptionTextboxIntro.gameObject.SetActive(true);
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
                        novelDescriptionTextboxIntro.gameObject.SetActive(false);
                        novelDescriptionTextbox.gameObject.SetActive(true);
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
            novelDescriptionTextbox.gameObject.SetActive(false);
            novelDescriptionTextboxIntro.gameObject.SetActive(false);
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

        private void OnInvestorNovelButtonFromBurgerMenu()
        {
            DisplayNovelFromMenu(VisualNovelNames.INVESTOR_NOVEL);
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