using Assets._Scripts.Managers;
using Assets._Scripts.SceneManagement;
using Assets._Scripts.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets._Scripts
{
    /// <summary>
    /// Manages the behavior and visual state of the application's footer menu.
    /// It handles navigation between different main scenes and updates the
    /// appearance of the active menu button. It also responds to global footer
    /// activation/deactivation events.
    /// </summary>
    public class FooterMenuController : MonoBehaviour
    {
        [SerializeField] private Button homeButton;
        [SerializeField] private Button archivButton;
        [SerializeField] private Button bookmarkButton;
        [SerializeField] private Button linksButton;
        [SerializeField] private Button knowledgeButton;

        [SerializeField] private Sprite homeButtonActiveImage;
        [SerializeField] private Sprite homeButtonInactiveImage;

        [SerializeField] private Sprite archivButtonActiveImage;
        [SerializeField] private Sprite archivButtonInactiveImage;

        [SerializeField] private Sprite bookmarkButtonActiveImage;
        [SerializeField] private Sprite bookmarkButtonInactiveImage;

        [SerializeField] private Sprite linksButtonActiveImage;
        [SerializeField] private Sprite linksButtonInactiveImage;
        
        [SerializeField] private Sprite knowledgeButtonActiveImage;
        [SerializeField] private Sprite knowledgeButtonInactiveImage;

        [SerializeField] private TextMeshProUGUI startTextElement;
        [SerializeField] private TextMeshProUGUI archivTextElement;
        [SerializeField] private TextMeshProUGUI gemerktTextElement;
        [SerializeField] private TextMeshProUGUI linkTextElement;
        [SerializeField] private TextMeshProUGUI knowledgeTextElement;

        [SerializeField] private string currentScene;

        /// <summary>
        /// Called when the script instance is being loaded.
        /// Subscribes to the <see cref="FooterActivationManager.OnActivationChanged"/> event
        /// to react to global changes in footer visibility.
        /// </summary>
        private void Awake()
        {
            FooterActivationManager.Instance().OnActivationChanged += HandleFooterActivationChanged;
        }

        /// <summary>
        /// Called when the GameObject is being destroyed.
        /// Unsubscribes from the <see cref="FooterActivationManager.OnActivationChanged"/> event
        /// to prevent memory leaks and ensure proper cleanup.
        /// </summary>
        private void OnDestroy()
        {
            FooterActivationManager.Instance().OnActivationChanged -= HandleFooterActivationChanged;
        }

        /// <summary>
        /// Called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// Checks the initial activation status of the footer and configures buttons accordingly.
        /// </summary>
        private void Start()
        {
            bool isActive = FooterActivationManager.Instance().IsActivated();
            SetButtonsActive(isActive);

            if (isActive)
                SetupButtonsAndVisuals();
        }

        /// <summary>
        /// Handles changes in the footer's activation status.
        /// This method is called when the <see cref="FooterActivationManager.OnActivationChanged"/> event is triggered.
        /// It dynamically enables/disables buttons and sets up/removes event listeners.
        /// </summary>
        /// <param name="newValue">The new activation state of the footer (true for active, false for inactive).</param>
        private void HandleFooterActivationChanged(bool newValue)
        {
            SetButtonsActive(newValue);

            if (newValue)
            {
                SetupButtonsAndVisuals();
            }
            else
            {
                RemoveButtonListeners();
            }
        }

        /// <summary>
        /// Sets the active state of all footer buttons.
        /// </summary>
        /// <param name="active">If true, buttons are active; if false, they are inactive.</param>
        private void SetButtonsActive(bool active)
        {
            homeButton.gameObject.SetActive(active);
            archivButton.gameObject.SetActive(active);
            bookmarkButton.gameObject.SetActive(active);
            linksButton.gameObject.SetActive(active);
            knowledgeButton.gameObject.SetActive(active);
        }

        /// <summary>
        /// Sets up the click listeners for all footer buttons and updates their initial visual states.
        /// </summary>
        private void SetupButtonsAndVisuals()
        {
            homeButton.onClick.AddListener(OnHomeButton);
            archivButton.onClick.AddListener(OnArchiveButton);
            bookmarkButton.onClick.AddListener(OnBookmarkButton);
            linksButton.onClick.AddListener(OnLinksButton);
            knowledgeButton.onClick.AddListener(OnKnowledgeButton);

            SetButtonImagesInaktiv();
            SetTextColorsInaktiv();
            SetImageForActiveButton();
        }

        /// <summary>
        /// Removes all click listeners from the footer buttons.
        /// This is crucial when the footer is deactivated to avoid calling methods on disabled GameObjects.
        /// </summary>
        private void RemoveButtonListeners()
        {
            homeButton.onClick.RemoveListener(OnHomeButton);
            archivButton.onClick.RemoveListener(OnArchiveButton);
            bookmarkButton.onClick.RemoveListener(OnBookmarkButton);
            linksButton.onClick.RemoveListener(OnLinksButton);
            knowledgeButton.onClick.RemoveListener(OnKnowledgeButton);
        }

        /// <summary>
        /// Determines the currently active scene and updates the corresponding footer button's
        /// image to its active sprite and its text to an active color (white).
        /// </summary>
        private void SetImageForActiveButton()
        {
            string imageName = SceneManager.GetActiveScene().name;
            currentScene = imageName;
            switch (imageName)
            {
                case SceneNames.FoundersBubbleScene:
                    SetHomeButtonImage();
                    SetTextToActive(startTextElement);
                    break;
                case SceneNames.NovelHistoryScene:
                    SetArchivButtonImage();
                    SetTextToActive(archivTextElement);
                    break;
                case SceneNames.BookmarkedNovelsScene:
                    SetBookmarkButtonImage();
                    SetTextToActive(gemerktTextElement);
                    break;
                case SceneNames.ResourcesScene:
                    SetLinksButtonImage();
                    SetTextToActive(linkTextElement);
                    break;
                case SceneNames.KnowledgeScene:
                    SetKnowledgeButtonImage();
                    SetTextToActive(knowledgeTextElement);
                    break;
            }
        }

        /// <summary>
        /// Handles the click event for the Home button.
        /// Loads the <see cref="SceneNames.FoundersBubbleScene"/> if it's not the current scene.
        /// </summary>
        private void OnHomeButton()
        {
            if (currentScene.Equals(SceneNames.FoundersBubbleScene)) return;
            SceneLoader.LoadFoundersBubbleScene();
        }

        /// <summary>
        /// Handles the click event for the Archive button.
        /// Loads the <see cref="SceneNames.NovelHistoryScene"/> if it's not the current scene.
        /// </summary>
        private void OnArchiveButton()
        {
            if (currentScene.Equals(SceneNames.NovelHistoryScene)) return;
            SceneLoader.LoadNovelHistoryScene();
        }

        /// <summary>
        /// Handles the click event for the Bookmark button.
        /// Loads the <see cref="SceneNames.BookmarkedNovelsScene"/> if it's not the current scene.
        /// </summary>
        private void OnBookmarkButton()
        {
            if (currentScene.Equals(SceneNames.BookmarkedNovelsScene)) return;
            SceneLoader.LoadBookmarkedNovelsScene();
        }

        /// <summary>
        /// Handles the click event for the Links button.
        /// Loads the <see cref="SceneNames.ResourcesScene"/> if it's not the current scene.
        /// </summary>
        private void OnLinksButton()
        {
            if (currentScene.Equals(SceneNames.ResourcesScene)) return;
            SceneLoader.LoadResourcesScene();
        }
        
        /// <summary>
        /// Handles the click event for the Knowledge button.
        /// Loads the <see cref="SceneNames.KnowledgeScene"/>. There is no explicit check to prevent re-loading
        /// if it's already the current scene, implying it might be designed to refresh or navigate within.
        /// </summary>
        private void OnKnowledgeButton()
        {
            SceneLoader.LoadKnowledgeScene();
        }

        /// <summary>
        /// Sets all footer button images to their inactive sprite.
        /// </summary>
        private void SetButtonImagesInaktiv()
        {
            homeButton.image.sprite      = homeButtonInactiveImage;
            archivButton.image.sprite    = archivButtonInactiveImage;
            bookmarkButton.image.sprite  = bookmarkButtonInactiveImage;
            linksButton.image.sprite     = linksButtonInactiveImage;
            knowledgeButton.image.sprite = knowledgeButtonInactiveImage;
        }

        /// <summary>
        /// Sets all footer button text colors to an inactive grey color (#B6BBC0).
        /// Includes a warning if the color conversion fails.
        /// </summary>
        private void SetTextColorsInaktiv()
        {
            string hexColor = "#B6BBC0";
            if (ColorUtility.TryParseHtmlString(hexColor, out Color newColor))
            {
                startTextElement.color     = newColor;
                archivTextElement.color    = newColor;
                gemerktTextElement.color   = newColor;
                linkTextElement.color      = newColor;
                knowledgeTextElement.color = newColor;
            }
            else
            {
                LogManager.Warning("Ungültiger Hex-Farbcode: " + hexColor);
            }
        }

        /// <summary>
        /// Sets the text color of a specific TextMeshProUGUI element to active (white).
        /// Logs a warning if the provided text element is null.
        /// </summary>
        /// <param name="textElement">The TextMeshProUGUI element to set to active color.</param>
        private void SetTextToActive(TextMeshProUGUI textElement)
        {
            if (textElement != null)
                textElement.color = Color.white;
            else
                LogManager.Warning("TextElement ist nicht gesetzt!");
        }

        /// <summary>
        /// Sets the Home button's image to its active sprite.
        /// </summary>
        private void SetHomeButtonImage()
        {
            homeButton.image.sprite = homeButtonActiveImage;
        }

        /// <summary>
        /// Sets the Archive button's image to its active sprite.
        /// </summary>
        private void SetArchivButtonImage()
        {
            archivButton.image.sprite = archivButtonActiveImage;
        }

        /// <summary>
        /// Sets the Bookmark button's image to its active sprite.
        /// </summary>
        private void SetBookmarkButtonImage()
        {
            bookmarkButton.image.sprite = bookmarkButtonActiveImage;
        }

        /// <summary>
        /// Sets the Links button's image to its active sprite.
        /// </summary>
        private void SetLinksButtonImage()
        {
            linksButton.image.sprite = linksButtonActiveImage;
        }
        
        /// <summary>
        /// Sets the Knowledge button's image to its active sprite.
        /// </summary>
        private void SetKnowledgeButtonImage()
        {
            knowledgeButton.image.sprite = knowledgeButtonActiveImage;
        }
    }
}
