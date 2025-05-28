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

        [Header("Burger Menu")] 
        [SerializeField] private Sprite closedBurgerMenuSprite;
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
            
            // Instantiate the headline and set its sibling index to 0
            GameObject burgerMenuHeadline = Instantiate(burgerMenuHeadlinePrefab, content?.transform);
            if (content != null)
            {
                burgerMenuHeadline.transform.SetSiblingIndex(0); 
            }

            _isNovelContainedInVersion = new List<NovelEntry>();
            foreach (VisualNovel visualNovel in allKiteNovelsList)
            {
                NovelEntry novelEntry = new NovelEntry
                {
                    novelId = visualNovel.id,
                    isContained = true
                };

                _isNovelContainedInVersion.Add(novelEntry);
                
                CreateBurgerMenuButton(visualNovel, content);
            }

 // Now, collect all children EXCEPT the headline, then sort and reorder
            List<Transform> sortableChildren = new List<Transform>();
            if (content != null)
            {
                foreach (Transform child in content)
                {
                    // Exclude the headline from the sorting list
                    if (!child.gameObject.name.Contains("Headline"))
                    {
                        sortableChildren.Add(child);
                    }
                }
            }

            // Separate search/background, then sort other children
            List<Transform> searchAndBackground = new List<Transform>();
            List<Transform> otherButtons = new List<Transform>();

            foreach(Transform child in sortableChildren)
            {
                if (child.gameObject.name.Contains("Search") || child.gameObject.name.Contains("Background"))
                {
                    searchAndBackground.Add(child);
                }
                else
                {
                    otherButtons.Add(child);
                }
            }

            // Sort other buttons alphabetically
            otherButtons = otherButtons.OrderBy(child => child.name).ToList();

            // Reconstruct the order: Headline (already at 0), then Background, then Search, then sorted Novel buttons/separators
            int currentSiblingIndex = 1; // Start from 1 because headline is at 0

            // Position Background and Search
            foreach(Transform child in searchAndBackground.OrderBy(child => child.gameObject.name.Contains("Background") ? 0 : 1))
            {
                child.SetSiblingIndex(currentSiblingIndex);
                currentSiblingIndex++;
            }

            // Position sorted novel buttons and separators
            foreach (Transform child in otherButtons)
            {
                child.SetSiblingIndex(currentSiblingIndex);
                currentSiblingIndex++;
            }

            novelListButton.onClick.AddListener(OnNovelListButton);
            legalInformationButton.onClick.AddListener(OnLegalInformationButton);
            settingsButton.onClick.AddListener(OnSettingsButton);

            burgerMenuBackground.onClick.AddListener(OnBackgroundButton);

            if (novelButtons != null && novelButtons.Count > 0)
            {
                // Speichere die ursprüngliche Reihenfolge der Container
                // new List<GameObject>(novelButtons);
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
            if (novelId == VisualNovelNames.None) return;

            string novelName = VisualNovelNamesHelper.GetName(visualNovel.id);

            GameObject burgerMenuButton = Instantiate(burgerMenuButtonPrefab, content?.transform);

            burgerMenuButton.name = novelName;
            burgerMenuButton.GetComponentInChildren<Button>().name = novelName;
            burgerMenuButton.GetComponentInChildren<TextMeshProUGUI>().text = !visualNovel.isKiteNovel ? visualNovel.title : novelName;
            burgerMenuButton.GetComponentInChildren<Button>().onClick.AddListener(OnButtonFromBurgerMenu);

            burgerMenuButton.GetComponentInChildren<Image>().color = visualNovel.novelColor;
            burgerMenuButtons.Add(burgerMenuButton);
            
            GameObject separatorLine = Instantiate(burgerMenuSeparatorImage, content?.transform);
            separatorLine.name = $"{novelName}Separator";
            separatorLine.GetComponentInChildren<Image>().name = $"{novelName}Separator";
            burgerMenuButtons.Add(separatorLine);
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