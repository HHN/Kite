using System.Collections.Generic;
using System.Linq;
using Assets._Scripts.Managers;
using Assets._Scripts.Novel;
using Assets._Scripts.Player;
using Assets._Scripts.SceneManagement;
using Assets._Scripts.UIElements.FoundersBubble;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets._Scripts.Controller.SceneControllers
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

        [Header("Infinity Scroll")] [SerializeField]
        private InfinityScroll infinityScroll;

        [SerializeField] private Button novelListButton;
        [SerializeField] private Image novelListButtonImage;
        [SerializeField] private Button legalInformationButton;
        [SerializeField] private Button settingsButton;

        [Header("Burger Menu")] [SerializeField]
        private Sprite closedBurgerMenuSprite;

        [SerializeField] private Sprite openedBurgerMenuSprite;
        [SerializeField] private GameObject burgerMenu;

        [SerializeField] private bool isBurgerMenuOpen;
        [SerializeField] private Button burgerMenuBackground;
        [SerializeField] private GameObject burgerMenuButtonPrefab;
        [SerializeField] private GameObject burgerMenuSeparatorImage;
        [SerializeField] private GameObject burgerMenuHeadlinePrefab;
        [SerializeField] private List<GameObject> burgerMenuButtons;

        [Header("Search Input and Button Containers")] [SerializeField]
        private List<GameObject> novelButtons;

        [SerializeField] private GameObject selectNovelSoundPrefab;

        [SerializeField] private bool finishedInitialization;

        private Dictionary<long, VisualNovel> _allKiteNovelsById;

        private int _novelId;

        private List<NovelEntry> _isNovelContainedInVersion;

        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.FoundersBubbleScene);
            GameManager.Instance.IsIntroNovelLoadedFromMainMenu = false;
            currentlyOpenedVisualNovelPopup = VisualNovelNames.None;

            List<VisualNovel> allKiteNovelsList = KiteNovelManager.Instance().GetAllKiteNovels();
            _allKiteNovelsById = allKiteNovelsList.ToDictionary(novel => novel.id);

            var children = burgerMenu.GetComponentsInChildren<Transform>();
            var content = children.FirstOrDefault(k => k.gameObject.name == "Content");

            // Sicherstellen, dass 'content' existiert
            if (content == null)
            {
                Debug.LogError("Content Transform not found in burgerMenu. Cannot proceed with UI setup.");
                return;
            }

            // 1. Headline instantiieren und am Anfang platzieren
            GameObject burgerMenuHeadline = Instantiate(burgerMenuHeadlinePrefab, content.transform);
            burgerMenuHeadline.transform.SetSiblingIndex(0);

            _isNovelContainedInVersion = new List<NovelEntry>();

            // 2. Alle Buttons erstellen und in einer temporären Liste speichern
            List<GameObject> novelButtonsToSort = new List<GameObject>();
            foreach (VisualNovel visualNovel in allKiteNovelsList)
            {
                NovelEntry novelEntry = new NovelEntry
                {
                    novelId = visualNovel.id,
                    isContained = true
                };
                _isNovelContainedInVersion.Add(novelEntry);

                GameObject novelButton = CreateBurgerMenuButton(visualNovel, content); // CreateBurgerMenuButton nur den Button zurückgeben lassen
                if (novelButton != null)
                {
                    novelButtonsToSort.Add(novelButton);
                }
            }

            // 3. Temporäre Liste der Buttons alphabetisch sortieren
            // (Achte darauf, dass der Name des GameObject die Sortiergrundlage ist)
            novelButtonsToSort = novelButtonsToSort.OrderBy(btn => btn.GetComponentInChildren<TextMeshProUGUI>().text).ToList();

            // 4. Buttons und Separatoren in der richtigen Reihenfolge dem Content hinzufügen
            // Bestimme den Startindex für die sortierten Buttons und Separatoren
            // Headline ist bei Index 0. Background und Search kommen danach.
            int currentSiblingIndex = 1;

            // Zuerst Background und Search platzieren, falls vorhanden
            List<Transform> searchAndBackground = new List<Transform>();
            foreach (Transform child in content)
            {
                // Hole alle Kinder, die noch nicht sortiert wurden (also nicht die Headline und nicht die Buttons)
                if (child.gameObject.name.Contains("Search") || child.gameObject.name.Contains("Background"))
                {
                    searchAndBackground.Add(child);
                }
            }

            // Position Background und Search (falls vorhanden und in der gewünschten Reihenfolge)
            // Angenommen, Background kommt vor Search, falls beide existieren
            foreach (Transform child in searchAndBackground.OrderBy(child => child.gameObject.name.Contains("Background") ? 0 : 1))
            {
                child.SetSiblingIndex(currentSiblingIndex);
                currentSiblingIndex++;
            }

            // Jetzt die sortierten Novel-Buttons und ihre Separatoren hinzufügen
            foreach (GameObject novelButton in novelButtonsToSort)
            {
                // Setze den Sibling Index des Buttons
                novelButton.transform.SetSiblingIndex(currentSiblingIndex);
                currentSiblingIndex++;
                burgerMenuButtons.Add(novelButton); // Füge den Button zur burgerMenuButtons Liste hinzu

                // Erstelle den Separator direkt nach dem Button
                GameObject separatorLine = Instantiate(burgerMenuSeparatorImage, content.transform);
                separatorLine.name = $"{novelButton.name}Separator"; // Name des Separators vom Button ableiten
                separatorLine.GetComponentInChildren<Image>().name = $"{novelButton.name}Separator";
                separatorLine.transform.SetSiblingIndex(currentSiblingIndex);
                currentSiblingIndex++;
                burgerMenuButtons.Add(separatorLine); // Füge den Separator zur burgerMenuButtons Liste hinzu
            }


            // Listener hinzufügen
            novelListButton.onClick.AddListener(OnNovelListButton);
            legalInformationButton.onClick.AddListener(OnLegalInformationButton);
            settingsButton.onClick.AddListener(OnSettingsButton);
            burgerMenuBackground.onClick.AddListener(OnBackgroundButton);

            // Die Debug.LogError und die if-else-Anweisung bezüglich novelButtons sind hier nicht mehr relevant,
            // da wir die Liste dynamisch füllen.
            // if (novelButtons != null && novelButtons.Count > 0)
            // {
            //     // Speichere die ursprüngliche Reihenfolge der Container
            //     // new List<GameObject>(novelButtons);
            // }
            // else
            // {
            //     Debug.LogError("Die Button-Container-Liste ist nicht zugewiesen oder leer.");
            // }

            StartCoroutine(TextToSpeechManager.Instance.Speak(" "));
            GlobalVolumeManager.Instance.StopSound();
        }

// Die CreateBurgerMenuButton Methode muss jetzt nur den Button zurückgeben
        private GameObject CreateBurgerMenuButton(VisualNovel visualNovel, Transform content)
        {
            var novelId = VisualNovelNamesHelper.ValueOf((int)(visualNovel.id));
            if (novelId == VisualNovelNames.None) return null;

            string novelName = VisualNovelNamesHelper.GetName(visualNovel.id);

            GameObject burgerMenuButton = Instantiate(burgerMenuButtonPrefab, content); // Instanziiere direkt unter dem Content
            burgerMenuButton.name = novelName;
            burgerMenuButton.GetComponentInChildren<Button>().name = novelName;
            burgerMenuButton.GetComponentInChildren<TextMeshProUGUI>().text = !visualNovel.isKiteNovel ? visualNovel.title : visualNovel.designation;
            burgerMenuButton.GetComponentInChildren<Button>().onClick.AddListener(OnButtonFromBurgerMenu);
            burgerMenuButton.GetComponentInChildren<Image>().color = visualNovel.novelColor;
    
            // Füge den Button hier NICHT zur burgerMenuButtons Liste hinzu,
            // da er erst nach der Sortierung hinzugefügt werden soll.
            return burgerMenuButton;
        }

        public void OnBackgroundButton()
        {
            CloseBurgerMenuIfOpen();

            if (isPopupOpen)
            {
                MakeTextboxInvisible();
            }
        }

        public void OnNovelButton()
        {
            GameObject buttonObject = EventSystem.current.currentSelectedGameObject;

            VisualNovelNames novelNames = VisualNovelNamesHelper.ValueByString(buttonObject.name);

            NovelEntry entry = _isNovelContainedInVersion.FirstOrDefault(novel => novel.novelId == VisualNovelNamesHelper.ToInt(novelNames));

            if (entry == null) return;

            DisplayTextBoxForVisualNovel(novelNames, entry.isContained);
            infinityScroll.MoveToVisualNovel(novelNames);
            if (novelNames.ToString() == "EinstiegsNovel")
            {
                novelDescriptionTextbox.DeactivatedBookmarkButton();
            }
        }

        private void OnNovelListButton()
        {
            if (isBurgerMenuOpen)
            {
                novelListButtonImage.sprite = closedBurgerMenuSprite;

                burgerMenu.gameObject.SetActive(false);
                isBurgerMenuOpen = false;
                return;
            }

            novelListButtonImage.sprite = openedBurgerMenuSprite;

            isBurgerMenuOpen = true;
            burgerMenu.gameObject.SetActive(true);
            FontSizeManager.Instance().UpdateAllTextComponents();
        }

        public void OnIntroNovelButton()
        {
            GameObject buttonObject = EventSystem.current.currentSelectedGameObject;
            VisualNovelNames novelNames = VisualNovelNamesHelper.ValueByString(buttonObject.name);

            LoadAndPlayNovel(novelNames, true);
        }

        private void OnLegalInformationButton()
        {
            SceneLoader.LoadLegalInformationScene();
        }

        private void OnSettingsButton()
        {
            SceneLoader.LoadSettingsScene();
        }

        private void DisplayTextBoxForVisualNovel(VisualNovelNames visualNovel, bool isNovelContainedInVersion)
        {
            _novelId = VisualNovelNamesHelper.ToInt(visualNovel);
            VisualNovel currentNovel;
            if (!_allKiteNovelsById.TryGetValue(_novelId, out currentNovel))
            {
                Debug.LogError($"Novel mit ID {_novelId} nicht im Dictionary gefunden.");
                return;
            }

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
                novelDescriptionTextbox.SetHead();
                novelDescriptionTextbox.SetVisualNovelName(visualNovel);
                novelDescriptionTextbox.SetText("Leider ist diese Novel nicht in der Testversion enthalten. Bitte spiele eine andere Novel.");
                novelDescriptionTextbox.SetColorOfImage(currentNovel.novelColor);
                novelDescriptionTextbox.SetButtonsActive(false);
                isPopupOpen = true;
                currentlyOpenedVisualNovelPopup = visualNovel;
                return;
            }

            novelDescriptionTextbox.gameObject.SetActive(true);
            novelDescriptionTextbox.SetHead();
            novelDescriptionTextbox.SetVisualNovel(currentNovel);
            novelDescriptionTextbox.SetVisualNovelName(visualNovel);
            novelDescriptionTextbox.SetText(currentNovel.description);
            novelDescriptionTextbox.SetColorOfImage(currentNovel.novelColor);
            novelDescriptionTextbox.SetButtonsActive(true);
            novelDescriptionTextbox.InitializeBookMarkButton(FavoritesManager.Instance().IsFavorite(currentNovel));
            novelDescriptionTextbox.UpdateSize();

            isPopupOpen = true;
            currentlyOpenedVisualNovelPopup = visualNovel;
            NovelColorManager.Instance().SetColor(currentNovel.novelColor);

            FontSizeManager.Instance().UpdateAllTextComponents();
        }

        public void MakeTextboxInvisible()
        {
            isPopupOpen = false;
            currentlyOpenedVisualNovelPopup = VisualNovelNames.None;
            novelDescriptionTextbox.gameObject.SetActive(false);
        }

        public override void OnStop()
        {
            base.OnStop();
            SceneMemoryManager.Instance().SetMemoryOfFoundersBubbleScene(infinityScroll.GetCurrentScrollPosition());
        }

        private void OnButtonFromBurgerMenu()
        {
            Debug.Log($"OnButtonFromBurgerMenu called for button: {EventSystem.current.currentSelectedGameObject.name}");
            GameObject buttonObject = EventSystem.current.currentSelectedGameObject;
            Debug.Log(buttonObject.name.Replace("Button", ""));
            VisualNovelNames novelNames = VisualNovelNamesHelper.ValueByString(buttonObject.name.Replace("Button", ""));

            var entry = _isNovelContainedInVersion.FirstOrDefault(novel => novel.novelId == VisualNovelNamesHelper.ToInt(novelNames));

            if (entry == null) return;

            LoadAndPlayNovel(novelNames);
        }

        private void CloseBurgerMenuIfOpen()
        {
            if (isBurgerMenuOpen)
            {
                burgerMenu.SetActive(false); // Verstecke das Burger-Men�
                isBurgerMenuOpen = false;
            }
        }

        private void LoadAndPlayNovel(VisualNovelNames novelName, bool isIntroNovel = false)
        {
            VisualNovel visualNovelToDisplay;
            if (!_allKiteNovelsById.TryGetValue(VisualNovelNamesHelper.ToInt(novelName), out visualNovelToDisplay))
            {
                Debug.LogWarning($"Novel with name {novelName} (ID: {VisualNovelNamesHelper.ToInt(novelName)}) not found in dictionary.");
                return;
            }

            PlayManager.Instance().SetVisualNovelToPlay(visualNovelToDisplay);
            NovelColorManager.Instance().SetColor(visualNovelToDisplay.novelColor);
            PlayManager.Instance().SetColorOfVisualNovelToPlay(visualNovelToDisplay.novelColor);
            PlayManager.Instance().SetDisplayNameOfNovelToPlay(FoundersBubbleMetaInformation.GetDisplayNameOfNovelToPlay(novelName));
            PlayManager.Instance().SetDesignationOfNovelToPlay(visualNovelToDisplay.designation);

            GameObject buttonSound = Instantiate(selectNovelSoundPrefab);
            DontDestroyOnLoad(buttonSound);

            if (isIntroNovel)
            {
                GameManager.Instance.IsIntroNovelLoadedFromMainMenu = false;
                SceneLoader.LoadPlayNovelScene();
            }
            else
            {
                if (ShowPlayInstructionManager.Instance().ShowInstruction() && visualNovelToDisplay.title != "Einstiegsdialog")
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
}