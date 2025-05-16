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
        [SerializeField] private Button settingsButton;

        [Header("Burger Menu")] [SerializeField]
        private GameObject burgerMenu;

        [SerializeField] private bool isBurgerMenuOpen;
        [SerializeField] private Button burgerMenuBackground;
        [SerializeField] private GameObject burgerMenuButtonPrefab;
        [SerializeField] private List<GameObject> burgerMenuButtons;

        [Header("Search Input and Button Containers")] 
        [SerializeField] private List<GameObject> novelButtons;

        [SerializeField] private GameObject selectNovelSoundPrefab;

        [SerializeField] private bool finishedInitialization;

        private int _novelId;

        private List<NovelEntry> _isNovelContainedInVersion;

        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.FoundersBubbleScene);

            currentlyOpenedVisualNovelPopup = VisualNovelNames.None;

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
                        buttonChildren.Insert(child.gameObject.name.Contains("Background") ? 0 : 1, child);
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

            if (novelButtons != null && novelButtons.Count > 0)
            {
                // Speichere die ursprüngliche Reihenfolge der Container
                new List<GameObject>(novelButtons);
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
            
            Debug.Log($"buttonObject.name: {buttonObject.name}");
            VisualNovelNames novelNames = VisualNovelNamesHelper.ValueByString(buttonObject.name);
            Debug.Log($"novelNames: {novelNames}");

            var entry = _isNovelContainedInVersion.FirstOrDefault(novel => novel.novelId == VisualNovelNamesHelper.ToInt(novelNames));

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
            SceneLoader.LoadSettingsScene();
        }

        private void DisplayTextBoxForVisualNovel(VisualNovelNames visualNovel, bool isNovelContainedInVersion)
        {
            List<VisualNovel> allNovels = KiteNovelManager.Instance().GetAllKiteNovels();
            allNovels = allNovels.OrderBy(novel => novel.id).ToList();
            
            _novelId = VisualNovelNamesHelper.ToInt(visualNovel);
            
            VisualNovel currentNovel = allNovels.Find(novel => novel.id == _novelId);
            
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
                novelDescriptionTextbox.SetText("Leider ist diese Novel nicht in der Testversion enthalten. Bitte spiele eine andere Novel.");
                novelDescriptionTextbox.SetColorOfImage(currentNovel.novelColor);
                novelDescriptionTextbox.SetButtonsActive(false);
                isPopupOpen = true;
                currentlyOpenedVisualNovelPopup = visualNovel;
                return;
            }

            foreach (VisualNovel novel in allNovels)
            {
                if (novel.id != _novelId) continue;
                
                novelDescriptionTextbox.gameObject.SetActive(true);
                novelDescriptionTextbox.SetHead(FoundersBubbleMetaInformation.IsHighInGui(visualNovel));
                novelDescriptionTextbox.SetVisualNovel(novel);
                novelDescriptionTextbox.SetVisualNovelName(visualNovel);
                novelDescriptionTextbox.SetText(novel.description);
                novelDescriptionTextbox.SetColorOfImage(novel.novelColor);
                novelDescriptionTextbox.SetButtonsActive(true);
                novelDescriptionTextbox.InitializeBookMarkButton(FavoritesManager.Instance().IsFavorite(novel));
                novelDescriptionTextbox.UpdateSize();

                isPopupOpen = true;
                currentlyOpenedVisualNovelPopup = visualNovel;
                NovelColorManager.Instance().SetColor(novel.novelColor);
            }

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
            GameObject buttonObject = EventSystem.current.currentSelectedGameObject;
            VisualNovelNames novelNames = VisualNovelNamesHelper.ValueByString(buttonObject.name.Replace("Button", ""));

            var entry = _isNovelContainedInVersion.FirstOrDefault(novel => novel.novelId == VisualNovelNamesHelper.ToInt(novelNames));

            if (entry == null) return;

            if (novelNames == VisualNovelNames.EinstiegsNovel)
            {
                GameManager.Instance.IsIntroNovelLoadedFromMainMenu = false;
            }

            DisplayNovelFromMenu(novelNames);
        }

        private void CloseBurgerMenuIfOpen()
        {
            if (isBurgerMenuOpen)
            {
                burgerMenu.SetActive(false); // Verstecke das Burger-Men�
                isBurgerMenuOpen = false;
            }
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

            Color color = visualNovelToDisplay.novelColor;

            PlayManager.Instance().SetVisualNovelToPlay(visualNovelToDisplay);
            NovelColorManager.Instance().SetColor(color);
            PlayManager.Instance().SetColorOfVisualNovelToPlay(color);
            PlayManager.Instance().SetDisplayNameOfNovelToPlay(FoundersBubbleMetaInformation.GetDisplayNameOfNovelToPlay(visualNovelName));
            GameObject buttonSound = Instantiate(selectNovelSoundPrefab);
            DontDestroyOnLoad(buttonSound);

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