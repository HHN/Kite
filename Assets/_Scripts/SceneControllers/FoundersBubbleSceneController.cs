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
using UnityEngine.Serialization;
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

        [SerializeField] private bool isPopupOpen;
        [SerializeField] private VisualNovelNames currentlyOpenedVisualNovelPopup;

        [SerializeField] private InfinityScroll infinityScroll;

        [SerializeField] private Button foundersWellButton;
        [SerializeField] private Button novelListButton;
        [SerializeField] private Button settingsButton;

        [Header("Burger Menu")] [SerializeField]
        private GameObject burgerMenu;

        [SerializeField] private bool isBurgerMenuOpen;
        [SerializeField] private Button burgerMenuBackground;
        [SerializeField] private GameObject burgerMenuButtonPrefab;
        [SerializeField] private List<GameObject> burgerMenuButtons;

        [Header("Search Input and Button Containers")] [SerializeField]
        private TMP_InputField inputField;

        [SerializeField] private List<GameObject> novelButtons;

        [SerializeField] private GameObject selectNovelSoundPrefab;

        [SerializeField] private bool finishedInitialization;

        private int _novelId;
        private List<GameObject> _originalOrder;

        private List<NovelEntry> _isNovelContainedInVersion;

        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.FoundersBubbleScene);

            foundersWellButton.onClick.AddListener(OnFoundersWellButton);

            currentlyOpenedVisualNovelPopup = VisualNovelNames.NONE;

            List<VisualNovel> allKiteNovels = KiteNovelManager.Instance().GetAllKiteNovels();

            var children = burgerMenu.GetComponentsInChildren<Transform>();
            var content = children.FirstOrDefault(k => k.gameObject.name == "Content");

            _isNovelContainedInVersion = new List<NovelEntry>();
            foreach (VisualNovel visualNovel in allKiteNovels)
            {
                NovelEntry novelEntry = new NovelEntry
                {
                    novelId = visualNovel.id,
                    isContained = true
                };

                _isNovelContainedInVersion.Add(novelEntry);

                CreateBurgerMenuButton(visualNovel, content);
            }


            List<Transform> buttonChildren = new List<Transform>();
            if (content != null)
            {
                foreach (Transform child in content)
                {
                    if (!child.gameObject.name.Contains("Search") || !child.gameObject.name.Contains("Background"))
                    {
                        buttonChildren.Add(child);
                    }
                    else
                    {
                        if (child.gameObject.name.Contains("Background"))
                        {
                            buttonChildren.Insert(0, child);
                        }
                        else
                        {
                            buttonChildren.Insert(1, child);
                        }
                    }
                }
            }

            // Alphabetisch sortieren
            buttonChildren = buttonChildren.OrderBy(child => child.name).ToList();

            // Reihenfolge in der Hierarchie anpassen
            for (int i = 0; i < buttonChildren.Count; i++)
            {
                buttonChildren[i].SetSiblingIndex(i);
            }

            novelListButton.onClick.AddListener(OnNovelListButton);
            settingsButton.onClick.AddListener(OnSettingsButton);

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

            if (novelButtons != null && novelButtons.Count > 0)
            {
                // Speichere die ursprüngliche Reihenfolge der Container
                _originalOrder = new List<GameObject>(novelButtons);
            }
            else
            {
                Debug.LogError("Die Button-Container-Liste ist nicht zugewiesen oder leer.");
            }

            StartCoroutine(TextToSpeechManager.Instance.Speak(" "));
            GlobalVolumeManager.Instance.StopSound();
        }

        private void CreateBurgerMenuButton(VisualNovel visualNovel, Transform content)
        {
            var novelId = VisualNovelNamesHelper.ValueOf((int)(visualNovel.id));
            if (novelId == VisualNovelNames.NONE) return;

            string novelName = VisualNovelNamesHelper.GetName(visualNovel.id);

            GameObject burgerMenuButton = Instantiate(burgerMenuButtonPrefab, content?.transform);

            burgerMenuButton.name = novelName;
            burgerMenuButton.GetComponentInChildren<Button>().name = novelName;
            burgerMenuButton.GetComponentInChildren<TextMeshProUGUI>().text = novelName;
            burgerMenuButton.GetComponentInChildren<Button>().onClick.AddListener(OnButtonFromBurgerMenu);

            burgerMenuButton.GetComponentInChildren<Image>().color =
                FoundersBubbleMetaInformation.GetForegroundColorOfNovel(novelId);
            burgerMenuButtons.Add(burgerMenuButton);
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
            novelButtons.Clear();
            novelButtons.AddRange(visibleContainers);
            novelButtons.AddRange(hiddenContainers);

            // Aktualisiere die Reihenfolge der Container in der UI
            for (int i = 0; i < novelButtons.Count; i++)
            {
                novelButtons[i].transform.SetSiblingIndex(i + 1); // +1, um das InputField oben zu halten
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
            VisualNovelNames novelNames = VisualNovelNamesHelper.ValueByString(buttonObject.name);

            var entry = _isNovelContainedInVersion.FirstOrDefault(novel =>
                novel.novelId == VisualNovelNamesHelper.ToInt(novelNames));

            if (entry == null) return;

            DisplayTextBoxForVisualNovel(novelNames, entry.isContained);
            infinityScroll.MoveToVisualNovel(novelNames);
        }

        private void OnNovelListButton()
        {
            if (isBurgerMenuOpen)
            {
                burgerMenu.gameObject.SetActive(false);
                isBurgerMenuOpen = false;
                return;
            }

            isBurgerMenuOpen = true;
            burgerMenu.gameObject.SetActive(true);
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
                burgerMenu.gameObject.SetActive(false);
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

        public void OnButtonFromBurgerMenu()
        {
            GameObject buttonObject = EventSystem.current.currentSelectedGameObject;
            Debug.Log(buttonObject.name);
            VisualNovelNames novelNames = VisualNovelNamesHelper.ValueByString(buttonObject.name.Replace("Button", ""));

            var entry = _isNovelContainedInVersion.FirstOrDefault(novel =>
                novel.novelId == VisualNovelNamesHelper.ToInt(novelNames));

            if (entry == null) return;

            if (novelNames == VisualNovelNames.EINSTIEGS_NOVEL)
            {
                GameManager.Instance.IsIntroNovelLoadedFromMainMenu = false;
            }

            DisplayNovelFromMenu(novelNames);
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