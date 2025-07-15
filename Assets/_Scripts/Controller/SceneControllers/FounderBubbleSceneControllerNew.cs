using System.Collections;
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
    
    public class FounderBubbleSceneControllerNew : MonoBehaviour
    {
        [Header("UI Elements")] 
        [SerializeField] private NovelDescriptionTextbox novelDescriptionTextbox;
        [SerializeField] private ScrollRect novelButtonsScrollRect;
        [SerializeField] private Transform novelButtonsContainer;
        [SerializeField] private HorizontalLayoutGroup horizontalLayoutGroup;
        [SerializeField] private GameObject novelButtonPrefab;

        [Header("Burger Menu")] 
        [SerializeField] private GameObject burgerMenu;
        [SerializeField] private GameObject burgerMenuHeadlinePrefab;
        [SerializeField] private GameObject burgerMenuButtonPrefab;
        [SerializeField] private GameObject burgerMenuSeparatorImage;
        [SerializeField] private List<GameObject> burgerMenuButtons;
        [SerializeField] private bool isBurgerMenuOpen;

        [Header("navigation Buttons")] 
        [SerializeField] private Button legalInformationButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button novelListButtonToOpen;
        [SerializeField] private Button novelListButtonToClose;
        [SerializeField] private Button burgerMenuBackground;

        [Header("Layout Settings")] 
        [SerializeField] private float scrollDuration = 0.3f;
        [SerializeField] private int yOffsetForZigzag = 100;

        [Header("State Management")] 
        [SerializeField] private bool isPopupOpen;
        [SerializeField] private VisualNovelNames currentlyOpenedVisualNovelPopup;

        [Header("Audio")] 
        [SerializeField] private GameObject selectNovelSoundPrefab;

        private Dictionary<long, VisualNovel> _allKiteNovelsById;
        private RectTransform _lastNovelButtonRectTransform;
        private List<NovelEntry> _isNovelContainedInVersion;
        private int _novelId;

        #region Unity Lifecycle

        private void Start()
        {
            InitializeManagersAndState();
            Transform content = GetBurgerMenuContentTransform();
            if (!content) return;

            SetupBurgerMenuUI(content);
            SetupMainNovelTiles();
            AddEventListeners();
            PerformPostUISetupActions();
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes various managers and updates the state of the scene.
        /// Registers the current scene to the back stack, activates the footer UI,
        /// resets the intro novel loading flag, and clears the visual novel popup state.
        /// Populates and organizes the visual novel data by retrieving and processing
        /// all visual novels, specifically including only those marked as kite novels.
        /// </summary>
        private void InitializeManagersAndState()
        {
            BackStackManager.Instance().Push(SceneNames.FoundersBubbleScene);
            DestroyPlayNovelSceneController();
            FooterActivationManager.Instance().SetFooterActivated(true);
            GameManager.Instance.IsIntroNovelLoadedFromMainMenu = false;
            currentlyOpenedVisualNovelPopup = VisualNovelNames.None;

            List<VisualNovel> allKiteNovelsList = KiteNovelManager.Instance().GetAllKiteNovels();
            _allKiteNovelsById = allKiteNovelsList.ToDictionary(novel => novel.id);
            _isNovelContainedInVersion = new List<NovelEntry>();

            foreach (VisualNovel novel in allKiteNovelsList)
            {
                if (!novel.isKiteNovel) continue;
                
                NovelEntry novelEntry = new NovelEntry
                {
                    novelId = novel.id,
                    isContained = true
                };
                _isNovelContainedInVersion.Add(novelEntry);
            }
        }

        /// <summary>
        /// Sets up the UI for the burger menu by creating and organizing its content.
        /// Instantiates the headline element, generates buttons for available visual novels,
        /// and sorts these buttons alphabetically before adding them to the menu in the correct order.
        /// </summary>
        /// <param name="content">The transform representing the container within which the burger menu elements will be organized.</param>
        private void SetupBurgerMenuUI(Transform content)
        {
            InstantiateHeadline(content);
            List<GameObject> novelButtonsToSort = CreateAndCollectNovelButtons(content);

            novelButtonsToSort = novelButtonsToSort.OrderBy(btn =>
            {
                TextMeshProUGUI textMesh = btn.GetComponentInChildren<TextMeshProUGUI>();
                return textMesh ? textMesh.text : btn.name;
            }).ToList();

            ReorderBurgerMenuItems(content, novelButtonsToSort);
        }

        /// <summary>
        /// Configures and populates the main visual novel tiles in the scene.
        /// Iterates through the collection of available visual novels, instantiates UI elements
        /// for each novel that is classified as a "kite novel," and positions them in the UI container.
        /// Dynamically applies visual properties such as labels, colors, and button interactions.
        /// Updates layout properties and adjusts paddings to ensure proper alignment and spacing of the tiles.
        /// </summary>
        private void SetupMainNovelTiles()
        {
            int insertionIndex = 0;

            foreach (VisualNovel visualNovel in _allKiteNovelsById.Values)
            {
                if (!visualNovel.isKiteNovel) continue;
                
                string novelName = VisualNovelNamesHelper.GetName(visualNovel.id);

                if (novelName.Contains("Einstieg")) continue;

                GameObject novelButtonGameObject = Instantiate(novelButtonPrefab, novelButtonsContainer);
                RectTransform novelButtonRect = novelButtonGameObject.GetComponent<RectTransform>();

                novelButtonGameObject.transform.SetSiblingIndex(insertionIndex);
                insertionIndex++;

                novelButtonGameObject.name = novelName;
                novelButtonGameObject.GetComponentInChildren<Button>().name = novelName;
                novelButtonGameObject.GetComponentInChildren<TextMeshProUGUI>().text = !visualNovel.isKiteNovel ? visualNovel.title : visualNovel.designation;
                novelButtonGameObject.GetComponentInChildren<Image>().color = visualNovel.novelColor;

                Button button = novelButtonGameObject.GetComponentInChildren<Button>();
                VisualNovelNames novelNamesCopy = VisualNovelNamesHelper.ValueByString(novelButtonGameObject.name);
                RectTransform buttonRect = novelButtonGameObject.GetComponent<RectTransform>();
                
                novelButtonGameObject.GetComponentInChildren<BookmarkUpdater>().VisualNovel = VisualNovelNamesHelper.ValueOf((int)visualNovel.id);
                novelButtonGameObject.GetComponentInChildren<AlreadyPlayedUpdater>().VisualNovel = VisualNovelNamesHelper.ValueOf((int)visualNovel.id);

                button.onClick.AddListener(() => { OnNovelButton(buttonRect, novelNamesCopy); });

                if (novelButtonGameObject)
                {
                    _lastNovelButtonRectTransform = novelButtonRect;
                }
            }

            ApplyDynamicPadding();

            for (int i = 0; i < novelButtonsContainer.childCount; i++)
            {
                Transform childTransform = novelButtonsContainer.GetChild(i);
                RectTransform childRectTransform = childTransform as RectTransform;

                Positioner positioner = childRectTransform?.GetComponent<Positioner>();
                if (positioner)
                {
                    positioner.SetupButton(i, yOffsetForZigzag, novelButtonsContainer.childCount);
                }
            }

            Canvas.ForceUpdateCanvases();
        }

        /// <summary>
        /// Adds event listeners to various UI elements in the scene.
        /// Configures button actions for opening and closing the novel list,
        /// navigating to legal information, accessing settings, and handling
        /// interactions with the burger menu background.
        /// </summary>
        private void AddEventListeners()
        {
            novelListButtonToOpen.onClick.AddListener(OpenNovelList);
            novelListButtonToClose.onClick.AddListener(CloseNovelList);
            legalInformationButton.onClick.AddListener(OnLegalInformationButton);
            settingsButton.onClick.AddListener(OnSettingsButton);
            burgerMenuBackground.onClick.AddListener(OnBackgroundButton);
        }

        /// <summary>
        /// Executes post-UI setup actions to ensure proper audio management and feedback.
        /// Initiates a coroutine for text-to-speech to maintain interactivity and stops
        /// any ongoing sounds via the global volume manager, providing a clean audio state.
        /// </summary>
        private void PerformPostUISetupActions()
        {
            StartCoroutine(TextToSpeechManager.Instance.Speak(" "));
            GlobalVolumeManager.Instance.StopSound();
        }

        #endregion

        #region UI Setup and Layout

        /// <summary>
        /// Instantiates and adds the headline element for the burger menu to the specified content container.
        /// The headline is positioned at the top of the container by setting its sibling index to zero.
        /// </summary>
        /// <param name="content">The transform representing the container where the headline element will be added and organized.</param>
        private void InstantiateHeadline(Transform content)
        {
            GameObject burgerMenuHeadline = Instantiate(burgerMenuHeadlinePrefab, content);
            burgerMenuHeadline.transform.SetSiblingIndex(0);
        }

        /// <summary>
        /// Generates and collects a list of burger menu buttons for all visual novels.
        /// Each button corresponds to a unique visual novel and is created dynamically based
        /// on the novel's data.
        /// </summary>
        /// <param name="content">The parent transform where the buttons will be instantiated.</param>
        /// <returns>A list of game objects representing the created visual novel buttons.</returns>
        private List<GameObject> CreateAndCollectNovelButtons(Transform content)
        {
            _isNovelContainedInVersion = new List<NovelEntry>();
            List<GameObject> novelButtonsList = new List<GameObject>();

            foreach (VisualNovel visualNovel in _allKiteNovelsById.Values)
            {
                NovelEntry novelEntry = new NovelEntry
                {
                    novelId = visualNovel.id,
                    isContained = true
                };
                _isNovelContainedInVersion.Add(novelEntry);

                GameObject novelButton = CreateBurgerMenuButton(visualNovel, content);
                if (novelButton)
                {
                    novelButtonsList.Add(novelButton);
                }
            }

            return novelButtonsList;
        }

        /// <summary>
        /// Reorders the burger menu items within the provided content. Ensures that certain items
        /// like search and background elements are correctly positioned first, before appending
        /// the sorted novel buttons. Inserts separator images between novel buttons for better
        /// visual structure and adds them to the burger menu buttons list.
        /// </summary>
        /// <param name="content">The parent transform containing the burger menu items.</param>
        /// <param name="sortedNovelButtons">The list of visually sorted novel buttons to be added to the menu.</param>
        private void ReorderBurgerMenuItems(Transform content, List<GameObject> sortedNovelButtons)
        {
            int currentSiblingIndex = 1;

            List<Transform> searchAndBackground = new List<Transform>();
            foreach (Transform child in content)
            {
                if (child.gameObject.name.Contains("Search") || child.gameObject.name.Contains("Background"))
                {
                    searchAndBackground.Add(child);
                }
            }

            foreach (Transform child in searchAndBackground.OrderBy(child => child.gameObject.name.Contains("Background") ? 0 : 1))
            {
                child.SetSiblingIndex(currentSiblingIndex);
                currentSiblingIndex++;
            }

            foreach (GameObject novelButton in sortedNovelButtons)
            {
                novelButton.transform.SetSiblingIndex(currentSiblingIndex);
                currentSiblingIndex++;
                burgerMenuButtons.Add(novelButton);

                GameObject separatorLine = Instantiate(burgerMenuSeparatorImage, content);
                separatorLine.name = $"{novelButton.name}Separator";
                separatorLine.GetComponentInChildren<Image>().name = $"{novelButton.name}Separator";
                separatorLine.transform.SetSiblingIndex(currentSiblingIndex);
                currentSiblingIndex++;
                burgerMenuButtons.Add(separatorLine);
            }
        }

        /// <summary>
        /// Adjusts dynamic padding for a scrollable container to ensure proper alignment of its contents.
        /// Calculates the required padding values for both left and right sides based on the
        /// viewport's width and the last button's dimensions. Updates the horizontal layout
        /// group's padding, triggers a layout rebuild to reflect these changes, and ensures
        /// the UI is rendered correctly by forcing a canvas update.
        /// </summary>
        private void ApplyDynamicPadding()
        {
            float viewportHalfWidth = novelButtonsScrollRect.viewport.rect.width / 2f;
            float lastButtonHalfWidth = _lastNovelButtonRectTransform.rect.width / 2f;

            float requiredPadding = viewportHalfWidth - lastButtonHalfWidth;

            if (requiredPadding < 0) requiredPadding = 0;

            horizontalLayoutGroup.padding.right = Mathf.RoundToInt(requiredPadding);
            horizontalLayoutGroup.padding.left = Mathf.RoundToInt(requiredPadding);

            LayoutRebuilder.ForceRebuildLayoutImmediate(novelButtonsContainer.GetComponent<RectTransform>());
            Canvas.ForceUpdateCanvases();
        }

        /// <summary>
        /// Creates a burger menu button for a given visual novel and adds it to a specified content container.
        /// The button is configured with the appropriate name, text, design elements, and a click listener
        /// corresponding to the provided visual novel.
        /// </summary>
        /// <param name="visualNovel">The visual novel object containing data for button setup, such as ID, title, and visual attributes.</param>
        /// <param name="content">The transform of the parent object to which the burger menu button will be added.</param>
        /// <returns>The created burger menu button as a GameObject, or null if the visual novel ID is invalid.</returns>
        private GameObject CreateBurgerMenuButton(VisualNovel visualNovel, Transform content)
        {
            VisualNovelNames visualNovelName = VisualNovelNamesHelper.ValueOf((int)visualNovel.id);
            if (visualNovelName == VisualNovelNames.None) return null;

            string novelName = VisualNovelNamesHelper.GetName(visualNovel.id);

            GameObject burgerMenuButton = Instantiate(burgerMenuButtonPrefab, content);
            burgerMenuButton.name = novelName;
            burgerMenuButton.GetComponentInChildren<Button>().name = novelName;
            burgerMenuButton.GetComponentInChildren<TextMeshProUGUI>().text = !visualNovel.isKiteNovel ? visualNovel.title : visualNovel.designation;
            burgerMenuButton.GetComponentInChildren<Button>().onClick.AddListener(OnButtonFromBurgerMenu);
            burgerMenuButton.GetComponentInChildren<Image>().color = visualNovel.novelColor;

            return burgerMenuButton;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the click event for buttons created in the burger menu.
        /// Retrieves the visual novel ID associated with the clicked button, checks its presence
        /// in the list of available novel entries, and initiates the loading and playback of the
        /// corresponding visual novel if it exists.
        /// </summary>
        private void OnButtonFromBurgerMenu()
        {
            GameObject buttonObject = EventSystem.current.currentSelectedGameObject;
            VisualNovelNames novelNames = VisualNovelNamesHelper.ValueByString(buttonObject.name.Replace("Button", ""));

            NovelEntry entry = _isNovelContainedInVersion.FirstOrDefault(novel => novel.novelId == VisualNovelNamesHelper.ToInt(novelNames));

            if (entry == null) return;

            LoadAndPlayNovel(novelNames);
        }

        /// <summary>
        /// Handles the logic for when a visual novel button is clicked.
        /// Identifies the associated visual novel entry, triggers snapping to the button,
        /// displays the relevant novel's description, and deactivates the bookmark button
        /// if the clicked novel corresponds to the intro novel.
        /// </summary>
        /// <param name="buttonRect">The RectTransform of the clicked visual novel button.</param>
        /// <param name="novelNames">The enumeration value representing the visual novel associated with the button.</param>
        public void OnNovelButton(RectTransform buttonRect, VisualNovelNames novelNames)
        {
            NovelEntry entry = _isNovelContainedInVersion.FirstOrDefault(novel => novel.novelId == VisualNovelNamesHelper.ToInt(novelNames));
            if (entry == null) return;

            if (SnapToButton(buttonRect)) return;

            DisplayTextBoxForVisualNovel(novelNames, entry.isContained);

            if (novelNames == VisualNovelNames.EinstiegsNovel)
            {
                novelDescriptionTextbox.DeactivatedBookmarkButton();
            }
        }

        /// <summary>
        /// Loads and plays the introduction novel based on the currently selected button in the UI.
        /// Retrieves the novel name from the button's name, determines the corresponding visual novel,
        /// and initializes it as an introductory novel to be played.
        /// </summary>
        public void OnIntroNovelButton()
        {
            GameObject buttonObject = EventSystem.current.currentSelectedGameObject;
            VisualNovelNames novelNames = VisualNovelNamesHelper.ValueByString(buttonObject.name);

            LoadAndPlayNovel(novelNames, true);
        }

        /// <summary>
        /// Handles the functionality when the background button is clicked.
        /// Closes the burger menu if it is currently open and hides the text box
        /// if a popup is active in the scene.
        /// </summary>
        public void OnBackgroundButton()
        {
            CloseBurgerMenuIfOpen();

            if (isPopupOpen)
            {
                MakeTextboxInvisible();
            }
        }

        /// <summary>
        /// Triggers the navigation to the legal information scene.
        /// Invokes the SceneLoader to load the scene associated with legal information.
        /// </summary>
        private void OnLegalInformationButton()
        {
            SceneLoader.LoadLegalInformationScene();
        }

        /// <summary>
        /// Triggers the navigation to the settings scene.
        /// Invokes the SceneLoader to load the scene associated with settings.
        /// </summary>
        private void OnSettingsButton()
        {
            SceneLoader.LoadSettingsScene();
        }

        #endregion

        #region Menu Management

        /// <summary>
        /// Opens the novel list by enabling the burger menu and setting its state to active.
        /// Updates all text components using the FontSizeManager and toggles the visibility
        /// of the open and close buttons for the novel list UI.
        /// </summary>
        private void OpenNovelList()
        {
            isBurgerMenuOpen = true;
            burgerMenu.gameObject.SetActive(true);
            FontSizeManager.Instance().UpdateAllTextComponents();
            ToggleOpenCloseButton();
        }

        /// <summary>
        /// Closes the visual novel list UI by deactivating the burger menu
        /// and marking the menu as closed. Also manages the state of
        /// the novel list open and close toggle buttons accordingly.
        /// </summary>
        private void CloseNovelList()
        {
            burgerMenu.gameObject.SetActive(false);
            isBurgerMenuOpen = false;
            ToggleOpenCloseButton();
        }

        /// <summary>
        /// Toggles the active state of the open and close buttons for the novel list.
        /// If the button to open the novel list is currently active, it is hidden, and the button
        /// to close the novel list is displayed. Conversely, if the button to close the novel list
        /// is active, it is hidden, and the button to open the novel list is displayed.
        /// </summary>
        private void ToggleOpenCloseButton()
        {
            if (novelListButtonToOpen.IsActive())
            {
                novelListButtonToOpen.gameObject.SetActive(false);
                novelListButtonToClose.gameObject.SetActive(true);
            }
            else
            {
                novelListButtonToOpen.gameObject.SetActive(true);
                novelListButtonToClose.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Closes the burger menu if it is currently open.
        /// Updates the menu's visibility state and sets the associated flag to false.
        /// </summary>
        private void CloseBurgerMenuIfOpen()
        {
            if (!isBurgerMenuOpen) return;
            
            burgerMenu.SetActive(false);
            isBurgerMenuOpen = false;
        }

        #endregion

        #region Visual Novel Functionality

        /// <summary>
        /// Displays a text box for a specific visual novel, updating its content and visibility
        /// based on the novel's availability and state. Adjusts UI elements such as the description
        /// textbox, buttons, colors, and visibility depending on whether the novel is part of the
        /// current version and interactions with the burger menu or other UI components.
        /// </summary>
        /// <param name="visualNovel">The visual novel to be displayed in the text box.</param>
        /// <param name="isNovelContainedInVersion">Indicates whether the visual novel is available in the current version.</param>
        /// <param name="TEST_VERSION_ERROR_MESSAGE"></param>
        private void DisplayTextBoxForVisualNovel(VisualNovelNames visualNovel, bool isNovelContainedInVersion)
        {
            _novelId = VisualNovelNamesHelper.ToInt(visualNovel);
            if (!_allKiteNovelsById.TryGetValue(_novelId, out VisualNovel currentNovel))
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

        private void MakeTextboxInvisible()
        {
            isPopupOpen = false;
            currentlyOpenedVisualNovelPopup = VisualNovelNames.None;
            novelDescriptionTextbox.gameObject.SetActive(false);
        }
        
        private void LoadAndPlayNovel(VisualNovelNames novelName, bool isIntroNovel = false)
        {
            if (!_allKiteNovelsById.TryGetValue(VisualNovelNamesHelper.ToInt(novelName), out VisualNovel visualNovelToDisplay))
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

        #endregion

        #region Helper Methods

        private Transform GetBurgerMenuContentTransform()
        {
            Transform[] children = burgerMenu.GetComponentsInChildren<Transform>();
            Transform content = children.FirstOrDefault(k => k.gameObject.name == "Content");

            if (!content) Debug.LogError("Content Transform not found in burgerMenu. Cannot proceed with UI setup.");

            return content;
        }

        private void DestroyPlayNovelSceneController()
        {
            GameObject persistentController = GameObject.Find("PlayNovelSceneController");
            if (persistentController)
            {
                Destroy(persistentController);
            }
        }
        
        private bool SnapToButton(RectTransform buttonRect)
        {
            float viewportWidth = novelButtonsScrollRect.viewport.rect.width;

            float buttonCenterXInContent = buttonRect.anchoredPosition.x;

            float targetContentX = buttonCenterXInContent - (viewportWidth / 2);

            float maxScrollableWidth = novelButtonsContainer.GetComponent<RectTransform>().rect.width - viewportWidth;

            if (maxScrollableWidth <= 0)
            {
                novelButtonsScrollRect.horizontalNormalizedPosition = 0.5f;
                return true;
            }

            float normalizedPosition = targetContentX / maxScrollableWidth;

            normalizedPosition = Mathf.Clamp01(normalizedPosition);

            StartCoroutine(SmoothScrollToPosition(normalizedPosition, scrollDuration));
            return false;
        }
        
        private IEnumerator SmoothScrollToPosition(float targetNormalizedPosition, float duration)
        {
            float startNormalizedPosition = novelButtonsScrollRect.horizontalNormalizedPosition;
            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                novelButtonsScrollRect.horizontalNormalizedPosition = Mathf.Lerp(startNormalizedPosition, targetNormalizedPosition, (elapsedTime / duration));

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            novelButtonsScrollRect.horizontalNormalizedPosition = targetNormalizedPosition;
        }
        
        #endregion
   }
}