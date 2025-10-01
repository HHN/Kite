using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets._Scripts.Managers;
using Assets._Scripts.Novel;
using Assets._Scripts.Player;
using Assets._Scripts.SceneManagement;
using Assets._Scripts.UIElements.FoundersBubble;
using Assets._Scripts.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets._Scripts.Controller.SceneControllers
{
    /// <summary>
    /// Manages the behavior and interactions within the FoundersBubble scene of the application.
    /// This includes handling user input on visual novel buttons, managing introductory novels,
    /// and responding to interactions with background elements while considering
    /// the scene's state and UI components.
    /// </summary>
    public class FounderBubbleSceneControllerNew : MonoBehaviour
    {
        [Header("UI Elements")] 
        [SerializeField] private NovelDescriptionTextbox novelDescriptionTextbox;
        [SerializeField] private ScrollRect novelButtonsScrollRect;
        [SerializeField] private Transform novelButtonsContainer;
        [SerializeField] private GameObject introNovelButton;
        [SerializeField] private HorizontalLayoutGroup horizontalLayoutGroup;
        [SerializeField] private GameObject novelButtonPrefab;

        [Header("Burger Menu")] 
        [SerializeField] private GameObject burgerMenu;
        [SerializeField] private GameObject burgerMenuHeadlinePrefab;
        [SerializeField] private GameObject burgerMenuButtonPrefab;
        [SerializeField] private GameObject burgerMenuSeparatorImage;
        [SerializeField] private List<GameObject> burgerMenuButtons;
        [SerializeField] private bool isBurgerMenuOpen;

        [Header("Navigation Buttons")] 
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
        private Transform _lastScaledNovelButton;
        private int _buttonCount;
        private Coroutine _initialScrollRoutine;
        private Coroutine _snapCoroutine;
        private bool _userInterrupted;

        #region Unity Lifecycle

        /// <summary>
        /// Sets up and initializes the FoundersBubble scene when it starts.
        /// This includes preparing managers, organizing UI components,
        /// and adding necessary event listeners to ensure the scene functions properly.
        /// </summary>
        private void Start()
        {
            InitializeManagersAndState();
            SetupMainNovelTiles();
            
            Transform content = GetBurgerMenuContentTransform();
            if (!content) return;
            SetupBurgerMenuUI(content);

            if (GameManager.Instance.ShowKiteNovels)
            {
                SetupIntroNovelButton();
            }
            else
            {
                introNovelButton.SetActive(false);
            }
            
            AddEventListeners();
            PerformPostUISetupActions();
            
            StartCoroutine(SetInitialScrollDelayed());

            if (GameManager.Instance.IsIntroNovelLoadedFromMainMenu)
            {
                _initialScrollRoutine = StartCoroutine(PerformInitialScrolling());
                
                GameManager.Instance.IsIntroNovelLoadedFromMainMenu = false;
            }
        }

        /// <summary>
        /// Updates the state of the FoundersBubble scene on each frame.
        /// Stops the initial scrolling routine if user input is detected to interrupt it.
        /// </summary>
        private void Update()
        {
            if (!_userInterrupted && UserInterruptedByInput())
            {
                InterruptScrolling();
            }
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
            BackStackManager.Instance.Push(SceneNames.FoundersBubbleScene);
            DestroyPlayNovelSceneController();
            FooterActivationManager.Instance().SetFooterActivated(true);
            currentlyOpenedVisualNovelPopup = VisualNovelNames.None;

            List<VisualNovel> allKiteNovelsList = KiteNovelManager.Instance().GetAllKiteNovels();
            
            _allKiteNovelsById = allKiteNovelsList.ToDictionary(novel => novel.id);
            _isNovelContainedInVersion = new List<NovelEntry>();
            
            bool showKite = GameManager.Instance.ShowKiteNovels;
            
            foreach (VisualNovel novel in allKiteNovelsList)
            {
                if (novel.isKiteNovel != showKite) continue;
            
                NovelEntry novelEntry = new NovelEntry
                {
                    novelId = novel.id,
                    isContained = true
                };
                _isNovelContainedInVersion.Add(novelEntry);
            }
        }
        
        /// <summary>
        /// Configures and populates the visual novel tiles in the scroll view in the scene.
        /// Iterates through the collection of available visual novels, instantiates UI elements
        /// for each novel that is classified as a "kite novel," and positions them in the UI container.
        /// Dynamically applies visual properties such as labels, colors, and button interactions.
        /// Updates layout properties and adjusts paddings to ensure proper alignment and spacing of the tiles.
        /// </summary>
        private void SetupMainNovelTiles()
        {
            // Initialize index for button placement order
            int insertionIndex = 0;

            // Loop through each visual novel in the collection
            foreach (NovelEntry entry in _isNovelContainedInVersion)
            {
                if (!_allKiteNovelsById.TryGetValue(entry.novelId, out var visualNovel)) continue;

                // Get the name of the visual novel
                string novelName = VisualNovelNamesHelper.GetName(visualNovel.id);

                // Skip intro novels (not needed in scroll view)
                if (novelName.Contains("Einstieg")) continue;

                // Create a button instance from prefab
                GameObject novelButtonGameObject = Instantiate(novelButtonPrefab, novelButtonsContainer);
                RectTransform novelButtonRect = novelButtonGameObject.GetComponent<RectTransform>();

                // Set button order 
                novelButtonGameObject.transform.SetSiblingIndex(insertionIndex);
                insertionIndex++;

                // Configure button properties
                novelButtonGameObject.name = novelName;

                Image[] images = novelButtonGameObject.GetComponentsInChildren<Image>(true);

                foreach (Image img in images)
                {
                    if (img.gameObject.name == "ButtonFrame")
                    {
                        img.color = visualNovel.novelFrameColor;
                    }
                    else if (img.gameObject.name == "NovelName")
                    {
                        img.color = visualNovel.novelColor;
                    }
                }

                novelButtonGameObject.GetComponentInChildren<Button>().name = novelName;
                novelButtonGameObject.GetComponentInChildren<TextMeshProUGUI>().text = !visualNovel.isKiteNovel ? visualNovel.title : visualNovel.designation;

                // Setup button click handling
                Button button = novelButtonGameObject.GetComponentInChildren<Button>();
                VisualNovelNames novelNamesCopy = VisualNovelNamesHelper.ValueByString(novelButtonGameObject.name);
                RectTransform buttonRect = novelButtonGameObject.GetComponent<RectTransform>();

                // Configure bookmark and played status updaters
                novelButtonGameObject.GetComponentInChildren<BookmarkUpdater>().VisualNovel = VisualNovelNamesHelper.ValueOf((int)visualNovel.id);
                novelButtonGameObject.GetComponentInChildren<AlreadyPlayedUpdater>().VisualNovel = VisualNovelNamesHelper.ValueOf((int)visualNovel.id);

                // Add click listener
                button.onClick.AddListener(() => { OnNovelButton(buttonRect, novelNamesCopy); });

                // Store reference to the last button for padding calculations
                if (novelButtonGameObject)
                {
                    _lastNovelButtonRectTransform = novelButtonRect;
                }
            }

            ApplyDynamicPadding();
            SetupZigZagLayout();
            _buttonCount = novelButtonsContainer.childCount;
            Canvas.ForceUpdateCanvases();
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
        /// Arranges UI elements in a zigzag pattern within the specified container.
        /// Each child element in the container is repositioned dynamically
        /// using a Positioner component to create a staggered or alternating layout.
        /// </summary>
        private void SetupZigZagLayout()
        {
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
        }

        private void SetupIntroNovelButton()
        {
            if (introNovelButton)
            {
                foreach (NovelEntry entry in _isNovelContainedInVersion)
                {
                    if (!_allKiteNovelsById.TryGetValue(entry.novelId, out var visualNovel)) continue;
                    
                    if (visualNovel.designation.Equals("Mehr zu KITE II erfahren"))
                    {
                        Image[] images = introNovelButton.GetComponentsInChildren<Image>(true);

                        foreach (Image img in images)
                        {
                            if (img.gameObject.name == "ButtonFrame")
                            {
                                img.color = visualNovel.novelFrameColor;
                            }
                            else if (img.gameObject.name == "NovelName")
                            {
                                img.color = visualNovel.novelColor;
                            }
                        }

                        introNovelButton.GetComponentInChildren<Button>().onClick.AddListener(OnIntroNovelButton);
                        introNovelButton.GetComponentInChildren<Button>().name = "Mehr zu\nKITE II";
                        introNovelButton.GetComponentInChildren<TextMeshProUGUI>().text = "Mehr zu\nKITE II";
                    }
                }
            }
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

        /// <summary>
        /// Performs initial scrolling to ensure the scroll view is positioned correctly
        /// when the scene is loaded. This method scrolls to the first button in the
        /// novel buttons container, ensuring that the user sees the first visual novel
        /// immediately upon entering the scene.
        /// </summary>
        private IEnumerator PerformInitialScrolling()
        {
            float tempScrollDuration = scrollDuration;
            scrollDuration = 1.5f;

            if (_buttonCount <= 1)
            {
                yield break;
            }

            if (_buttonCount % 2 != 0)
            {
                int middleIndex = _buttonCount / 2;
                RectTransform middleButton = novelButtonsContainer.GetChild(middleIndex).GetComponent<RectTransform>();

                RectTransform leftButton = novelButtonsContainer.GetChild(middleIndex - 1).GetComponent<RectTransform>();
                RectTransform rightButton = novelButtonsContainer.GetChild(middleIndex + 1).GetComponent<RectTransform>();

                yield return new WaitForSeconds(1f);
                SnapToButton(rightButton);

                yield return new WaitForSeconds(1f);
                SnapToButton(leftButton);

                yield return new WaitForSeconds(1f);
                SnapToButton(middleButton);
            }
            else
            {
                int middleIndex1 = _buttonCount / 2 - 1;
                int middleIndex2 = _buttonCount / 2;

                RectTransform middleButton1 = novelButtonsContainer.GetChild(middleIndex1).GetComponent<RectTransform>();
                RectTransform middleButton2 = novelButtonsContainer.GetChild(middleIndex2).GetComponent<RectTransform>();

                yield return new WaitForSeconds(1f);
                SnapToButton(middleButton2);

                yield return new WaitForSeconds(1f);
                SnapToButton(middleButton1);

                yield return new WaitForSeconds(1f);
                StartCoroutine(SetInitialScrollDelayed());
            }

            scrollDuration = tempScrollDuration;
        }

        /// <summary>
        /// A coroutine that sets the initial horizontal normalized position of the ScrollRect
        /// to the center (0.5f) after a short delay. This ensures the layout is fully built
        /// before setting the scroll position, preventing visual glitches.
        /// </summary>
        /// <returns>An IEnumerator for coroutine execution.</returns>
        private IEnumerator SetInitialScrollDelayed()
        {
            yield return null;
            novelButtonsScrollRect.horizontalNormalizedPosition = 0.5f;
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
            List<GameObject> novelButtonsList = new List<GameObject>();
            
            foreach (NovelEntry entry in _isNovelContainedInVersion)
            {
                if (!_allKiteNovelsById.TryGetValue(entry.novelId, out var visualNovel)) continue;
                if (visualNovel.id == 13) continue; 
                
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
            // Initialize index for placing UI elements in order
            int currentSiblingIndex = 1;

            // Find search and background elements to keep at the top of the menu
            List<Transform> searchAndBackground = new List<Transform>();
            foreach (Transform child in content)
            {
                if (child.gameObject.name.Contains("Search") || child.gameObject.name.Contains("Background"))
                {
                    searchAndBackground.Add(child);
                }
            }

            // Sort and position search/background elements (background first, then search)
            foreach (Transform child in searchAndBackground.OrderBy(child => child.gameObject.name.Contains("Background") ? 0 : 1))
            {
                child.SetSiblingIndex(currentSiblingIndex);
                currentSiblingIndex++;
            }

            // Add novel buttons and separators in sorted order
            foreach (GameObject novelButton in sortedNovelButtons)
            {
                // Position novel button
                novelButton.transform.SetSiblingIndex(currentSiblingIndex);
                currentSiblingIndex++;
                burgerMenuButtons.Add(novelButton);

                // Create and position a separator line after each button
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
        private void OnNovelButton(RectTransform buttonRect, VisualNovelNames novelNames)
        {
            if (_lastScaledNovelButton != null)
            {
                _lastScaledNovelButton.localScale = Vector3.one;
            }

            NovelEntry entry = _isNovelContainedInVersion.FirstOrDefault(novel => novel.novelId == VisualNovelNamesHelper.ToInt(novelNames));
            if (entry == null) return;

            Image[] images = buttonRect.GetComponentsInChildren<Image>(true);

            foreach (Image img in images)
            {
                if (img.gameObject.name == VisualNovelNamesHelper.GetName(VisualNovelNamesHelper.ToInt(novelNames)))
                {
                    img.gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
                    _lastScaledNovelButton = img.gameObject.transform;
                }
            }

            if (SnapToButton(buttonRect)) return;

            DisplayTextBoxForVisualNovel(novelNames, entry.isContained);

            if (novelNames == VisualNovelNames.EinstiegsNovel)
            {
                novelDescriptionTextbox.DeactivatedBookmarkButton();
            }
        }

        /// <summary>
        /// Handles the event triggered when the introductory novel button is clicked.
        /// Retrieves and validates the corresponding novel using its identifier,
        /// updates the current visual novel's theme color, clears the navigation stack,
        /// and loads the play instruction scene.
        /// </summary>
        private void OnIntroNovelButton()
        {
            _novelId = VisualNovelNamesHelper.ToInt(VisualNovelNames.EinstiegsNovel);
            if (!_allKiteNovelsById.TryGetValue(_novelId, out VisualNovel currentNovel))
            {
                LogManager.Error($"Novel mit ID {_novelId} nicht im Dictionary gefunden.");
                return;
            }
            
            NovelColorManager.Instance().SetColor(currentNovel.novelColor);
            PlayManager.Instance().SetVisualNovelToPlay(currentNovel);
            
            SceneLoader.LoadPlayInstructionScene();
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

        private void InterruptScrolling()
        {
            _userInterrupted = true;

            if (_initialScrollRoutine != null)
            {
                StopCoroutine(_initialScrollRoutine);
                _initialScrollRoutine = null;
            }

            if (_snapCoroutine != null)
            {
                StopCoroutine(_snapCoroutine);
                _snapCoroutine = null;
            }
        }

        private bool UserInterruptedByInput()
        {
            // Tastatur-Eingaben
            if (Input.anyKey || Input.anyKeyDown)
                return true;

            // Maus-Klick oder gehaltene Taste
            if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
                return true;

            // Touch-Eingabe (Tap oder Ziehen)
            if (Input.touchCount > 0)
                return true;

            return false;
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

            CloseNovelList();
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
        private void DisplayTextBoxForVisualNovel(VisualNovelNames visualNovel, bool isNovelContainedInVersion)
        {
            _novelId = VisualNovelNamesHelper.ToInt(visualNovel);
            if (!_allKiteNovelsById.TryGetValue(_novelId, out VisualNovel currentNovel))
            {
                LogManager.Error($"Novel mit ID {_novelId} nicht im Dictionary gefunden.");
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
                novelDescriptionTextbox.ActivateHeadIcon();
                novelDescriptionTextbox.SetVisualNovelName(visualNovel);
                novelDescriptionTextbox.SetText("Leider ist diese Novel nicht in der Testversion enthalten. Bitte spiele eine andere Novel.");
                novelDescriptionTextbox.SetColorOfImage(currentNovel.novelColor);
                novelDescriptionTextbox.SetButtonsActive(false);
                isPopupOpen = true;
                currentlyOpenedVisualNovelPopup = visualNovel;
                return;
            }

            novelDescriptionTextbox.gameObject.SetActive(true);
            novelDescriptionTextbox.ActivateHeadIcon();
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
            if (_lastScaledNovelButton != null)
            {
                _lastScaledNovelButton.localScale = Vector3.one;
                _lastScaledNovelButton = null;
            }

            isPopupOpen = false;
            currentlyOpenedVisualNovelPopup = VisualNovelNames.None;
            novelDescriptionTextbox.gameObject.SetActive(false);
        }

        private void LoadAndPlayNovel(VisualNovelNames novelName, bool isIntroNovel = false)
        {
            if (!_allKiteNovelsById.TryGetValue(VisualNovelNamesHelper.ToInt(novelName), out VisualNovel visualNovelToDisplay))
            {
                LogManager.Warning($"Novel with name {novelName} (ID: {VisualNovelNamesHelper.ToInt(novelName)}) not found in dictionary.");
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
                SceneLoader.LoadPlayNovelScene();
            }
        }

        #endregion

        #region Helper Methods

        private Transform GetBurgerMenuContentTransform()
        {
            Transform[] children = burgerMenu.GetComponentsInChildren<Transform>();
            Transform content = children.FirstOrDefault(k => k.gameObject.name == "Content");

            if (!content) LogManager.Error("Content Transform not found in burgerMenu. Cannot proceed with UI setup.");

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
            if (_snapCoroutine != null)
            {
                StopCoroutine(_snapCoroutine);
            }
            
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

            _snapCoroutine = StartCoroutine(SmoothScrollToPosition(normalizedPosition, scrollDuration));
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