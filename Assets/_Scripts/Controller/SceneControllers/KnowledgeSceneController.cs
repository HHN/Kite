using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets._Scripts._Mappings;
using Assets._Scripts.Biases;
using Assets._Scripts.Managers;
using Assets._Scripts.SceneManagement;
using Assets._Scripts.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Controller.SceneControllers
{
    /// <summary>
    /// A controller responsible for managing the Knowledge Scene in the application.
    /// Handles initialization, navigation logic, and interactions within the scene,
    /// ensuring that data, UI components, and event listeners are properly configured.
    /// Handles back navigation and manages category-specific logic and searches effectively.
    /// </summary>
    public class KnowledgeSceneController : MonoBehaviour
    {
        [SerializeField] private RectTransform contentRectTransform;

        [SerializeField] private GameObject infoText;
        [SerializeField] private GameObject biasInformation;
        [SerializeField] private GameObject biasDetailsObject;

        [SerializeField] private Sprite arrowLeftSprite;
        [SerializeField] private Sprite arrowDownSprite;
        
        [SerializeField] private GameObject categoryButtonPrefab;
        [SerializeField] private GameObject biasButtonPrefab;
        
        [Header("SearchBar")] 
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button searchBarButton;
        [SerializeField] private Image searchBarImage;
        [SerializeField] private GameObject searchList;
        [SerializeField] private RectTransform searchBarRect;
        
        private Dictionary<string, Bias> _biases;
        private readonly Dictionary<string, GameObject> _categoryContainers = new();
        private readonly Dictionary<string, bool> _categoryStates = new();
        private readonly Dictionary<string, bool> _categoryHasLoadedButtons = new();
        
        private readonly List<GameObject> _searchResultButtons = new();
        private TMP_Text _biasDetailsText;
        private Dictionary<Button, Action> _buttonActions;
        
        private IEnumerable<string> _categories;

        /// <summary>
        /// Initializes the Knowledge Scene by setting up the necessary data, UI components,
        /// and event listeners. Pushes the current scene to the back stack for navigation purposes.
        /// Loads bias data, configures category buttons and their respective actions, and prepares
        /// the search functionality. Ensures that the font sizes are updated across text components
        /// and triggers a layout rebuild.
        /// </summary>
        public void Start()
        {
            BackStackManager.Instance().Push(SceneNames.KnowledgeScene);

            _biases = MappingManager.biases;
            InitializeCategoryButtons();

            InitializeButtonActions();
            AddButtonListeners();

            if (biasDetailsObject) _biasDetailsText = biasDetailsObject.GetComponentInChildren<TextMeshProUGUI>();
            if (_biasDetailsText == null) LogManager.Warning("Kein TextMeshProUGUI unter biasDetailsObject gefunden.");
            
            if (inputField) inputField.onValueChanged.AddListener(Search);
            else LogManager.Error("InputField ist nicht zugewiesen.");

            FontSizeManager.Instance().UpdateAllTextComponents();
            StartCoroutine(RebuildLayout());
        }

        /// <summary>
        /// Creates and initializes category buttons dynamically based on the distinct categories
        /// derived from the biases loaded in the scene. Each button represents a unique category.
        /// Configures button properties, such as text and name, and sets up click event listeners
        /// for toggling the respective category state.
        /// </summary>
        private void InitializeCategoryButtons()
        {
            _categories = _biases.Values.Select(b => b.category).Distinct();
            foreach (string category in _categories)
            {
                GameObject categoryGameObject = Instantiate(categoryButtonPrefab, biasInformation.transform);
                categoryGameObject.name = category;
                categoryGameObject.GetComponentInChildren<TextMeshProUGUI>().text = category;

                _categoryContainers[category] = categoryGameObject;
                _categoryStates[category] = false;
                _categoryHasLoadedButtons[category] = false;

                categoryGameObject.GetComponent<Button>().onClick.AddListener(() => ToggleCategory(category));
            }
        }

        /// <summary>
        /// Configures and initializes the actions associated with various UI buttons,
        /// mapping each button to its respective functionality within the Knowledge Scene.
        /// </summary>
        private void InitializeButtonActions()
        {
            _buttonActions = new Dictionary<Button, Action>
            {
                { searchBarButton, CloseSearchBar }
            };
        }

        /// <summary>
        /// Adds event listeners to buttons within the Knowledge Scene, linking each button to its predefined action.
        /// </summary>
        private void AddButtonListeners()
        {
            foreach (var (button, action) in _buttonActions)
            {
                button.onClick.AddListener(() => action.Invoke());
            }
        }

        /// <summary>
        /// Toggles the visibility of a category's content in the Knowledge Scene. Expands or collapses
        /// the category's associated container and updates UI elements such as arrow icons and layout.
        /// </summary>
        /// <param name="category">The name of the category to toggle.</param>
        private void ToggleCategory(string category)
        {
            bool isOpen = _categoryStates[category];
            GameObject container = _categoryContainers[category];

            if (isOpen)
            {
                foreach (Transform child in container.transform)
                {
                    if (child.name != "Headline") child.gameObject.SetActive(false);
                }

                SetArrow(container, arrowLeftSprite);
                _categoryStates[category] = false;
            }
            else
            {
                if (!_categoryHasLoadedButtons[category])
                {
                    foreach (var bias in _biases.Values.Where(b => b.category == category))
                    {
                        GameObject biasGameObject = Instantiate(biasButtonPrefab, container.transform);
                        biasGameObject.name = bias.headline;
                        biasGameObject.GetComponentInChildren<TextMeshProUGUI>().text = FormatBiasButtonText(bias);

                        biasGameObject.GetComponent<Button>().onClick.AddListener(() => OnBiasClicked(bias));
                    }
                }
                else
                {
                    foreach (Transform child in container.transform)
                    {
                        if (child.name != "Headline") child.gameObject.SetActive(true);
                    }
                }

                SetArrow(container, arrowDownSprite);
                _categoryStates[category] = true;
            }

            FontSizeManager.Instance().UpdateAllTextComponents();
            LayoutRebuilder.ForceRebuildLayoutImmediate(contentRectTransform);

        }

        /// <summary>
        /// Updates the arrow sprite for a specified category GameObject, allowing for visual feedback on category state changes.
        /// </summary>
        /// <param name="categoryGameObject">The GameObject representing the category whose arrow sprite will be updated.</param>
        /// <param name="sprite">The new Sprite to be set for the category's arrow.</param>
        private void SetArrow(GameObject categoryGameObject, Sprite sprite)
        {
            var arrow = categoryGameObject.GetComponentsInChildren<Image>().FirstOrDefault(img => img.gameObject.name == "Arrow");
            if (arrow != null) arrow.sprite = sprite;
        }

        /// <summary>
        /// Handles the event triggered when a bias button is clicked, parsing the bias type
        /// and displaying detailed information about the selected bias.
        /// </summary>
        /// <param name="bias">The bias object associated with the clicked button, containing its type, headline, and other details.</param>
        private void OnBiasClicked(Bias bias)
        {
            ShowBiasDetails(bias.type);
            
        }

        /// <summary>
        /// Updates the UI to display the details of a specified bias type.
        /// Ensures that the appropriate UI elements are visible and populated
        /// with the corresponding description text while hiding unrelated elements.
        /// </summary>
        /// <param name="type">The type of the bias to display details for.</param>
        private void ShowBiasDetails(string type)
        {
            if (searchList.activeInHierarchy) searchList.SetActive(false);

            infoText.SetActive(false);
            biasInformation.SetActive(false);

            biasDetailsObject.SetActive(true);
            _biasDetailsText.text = BiasDescriptionTexts.GetBiasText(type);

            FontSizeManager.Instance().UpdateAllTextComponents();
            StartCoroutine(RebuildLayout());
        }

        /// <summary>
        /// Handles the navigation logic within the Knowledge Scene, including toggling UI elements,
        /// managing category visibility, and loading appropriate scenes or views based on the current context.
        /// </summary>
        public void NavigateScene()
        {
            infoText.SetActive(true);

            if (biasInformation.activeInHierarchy)
            {
                SceneLoader.LoadFoundersBubbleScene();
            }
            else if (biasDetailsObject.activeInHierarchy)
            {
                biasDetailsObject.SetActive(false);
                biasInformation.SetActive(true);
                
                foreach (var category in _categoryContainers.Keys) _categoryContainers[category].SetActive(true);

                CloseSearchBar();
                StartCoroutine(RebuildLayout());
            }
        }

        /// <summary>
        /// Searches the list of biases and updates the search interface with results
        /// that match the provided input. The search is case-insensitive and matches
        /// both the headline and preview properties of biases.
        /// </summary>
        /// <param name="input">The search query entered by the user.</param>
        private void Search(string input)
        {
            searchBarButton.gameObject.SetActive(true);
            searchBarImage.gameObject.SetActive(false);

            biasInformation.SetActive(false);
            searchList.SetActive(true);
            
            foreach (var btn in _searchResultButtons) btn.SetActive(false);
            _searchResultButtons.Clear();

            if (string.IsNullOrWhiteSpace(input)) return;

            string inputLower = input.ToLower();
            var matches = _biases.Values
                .Where(b => b.headline.ToLower().Contains(inputLower) || b.preview.ToLower().Contains(inputLower));

            foreach (var bias in matches)
            {
                GameObject buttonGameObject = Instantiate(biasButtonPrefab, searchList.transform);
                buttonGameObject.name = bias.headline;
                buttonGameObject.GetComponentInChildren<TextMeshProUGUI>().text = FormatBiasButtonText(bias);
                buttonGameObject.GetComponent<Button>().onClick.AddListener(() => OnBiasClicked(bias));

                _searchResultButtons.Add(buttonGameObject);
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(searchList.GetComponent<RectTransform>());
        }

        /// <summary>
        /// Formats the text content for a bias button, including its headline and preview description.
        /// </summary>
        /// <param name="bias">The bias object containing metadata such as headline and preview.</param>
        /// <returns>A formatted string containing the headline in bold and the preview on a new line.</returns>
        private string FormatBiasButtonText(Bias bias) => $"<b>{bias.headline}</b>\n{bias.preview}";

        /// <summary>
        /// Resets the search bar by clearing the input field, hiding the search button,
        /// showing the search bar image, and deactivating the search list if active.
        /// Ensures the bias information is displayed when needed.
        /// </summary>
        private void CloseSearchBar()
        {
            inputField.text = "";
            searchBarButton.gameObject.SetActive(false);
            searchBarImage.gameObject.SetActive(true);

            if (searchList.activeInHierarchy) searchList.SetActive(false);
            if (!biasInformation.activeInHierarchy) biasInformation.SetActive(true);
        }

        /// <summary>
        /// Rebuilds the layout of the content UI element to ensure proper alignment and spacing updates.
        /// </summary>
        /// <returns>An enumerator that allows this method to be used as a coroutine.</returns>
        private IEnumerator RebuildLayout()
        {
            yield return null;
            LayoutRebuilder.ForceRebuildLayoutImmediate(contentRectTransform);
        }
    }
}