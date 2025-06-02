// FooterActivationManager.cs bleibt unverändert

// FooterMenuController.cs
using Assets._Scripts.Managers;
using Assets._Scripts.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets._Scripts
{
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

        private void Awake()
        {
            // Event abonnieren, auch wenn später das GameObject deaktiviert wäre
            FooterActivationManager.Instance().OnActivationChanged += HandleFooterActivationChanged;
        }

        private void OnDestroy()
        {
            FooterActivationManager.Instance().OnActivationChanged -= HandleFooterActivationChanged;
        }

        private void Start()
        {
            bool isActive = FooterActivationManager.Instance().IsActivated();
            // Buttons initial ein- oder ausblenden
            SetButtonsActive(isActive);

            if (isActive)
                SetupButtonsAndVisuals();
        }

        private void HandleFooterActivationChanged(bool newValue)
        {
            // Statt das gesamte GameObject zu deaktivieren, nur die Buttons
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

        private void SetButtonsActive(bool active)
        {
            homeButton.gameObject.SetActive(active);
            archivButton.gameObject.SetActive(active);
            bookmarkButton.gameObject.SetActive(active);
            linksButton.gameObject.SetActive(active);
            knowledgeButton.gameObject.SetActive(active);
        }

        private void SetupButtonsAndVisuals()
        {
            homeButton.onClick.AddListener(OnHomeButton);
            archivButton.onClick.AddListener(OnArchivButton);
            bookmarkButton.onClick.AddListener(OnBookmarkButton);
            linksButton.onClick.AddListener(OnLinksButton);
            knowledgeButton.onClick.AddListener(OnKnowledgeButton);

            SetButtonImagesInaktiv();
            SetTextColorsInaktiv();
            SetImageForActiveButton();
        }

        private void RemoveButtonListeners()
        {
            homeButton.onClick.RemoveListener(OnHomeButton);
            archivButton.onClick.RemoveListener(OnArchivButton);
            bookmarkButton.onClick.RemoveListener(OnBookmarkButton);
            linksButton.onClick.RemoveListener(OnLinksButton);
            knowledgeButton.onClick.RemoveListener(OnKnowledgeButton);
        }

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

        private void OnHomeButton()
        {
            if (currentScene.Equals(SceneNames.FoundersBubbleScene)) return;
            SceneLoader.LoadFoundersBubbleScene();
        }

        private void OnArchivButton()
        {
            if (currentScene.Equals(SceneNames.NovelHistoryScene)) return;
            SceneLoader.LoadNovelHistoryScene();
        }

        private void OnBookmarkButton()
        {
            if (currentScene.Equals(SceneNames.BookmarkedNovelsScene)) return;
            SceneLoader.LoadBookmarkedNovelsScene();
        }

        private void OnLinksButton()
        {
            if (currentScene.Equals(SceneNames.ResourcesScene)) return;
            SceneLoader.LoadResourcesScene();
        }
        
        private void OnKnowledgeButton()
        {
            SceneLoader.LoadKnowledgeScene();
        }

        private void SetButtonImagesInaktiv()
        {
            homeButton.image.sprite      = homeButtonInactiveImage;
            archivButton.image.sprite    = archivButtonInactiveImage;
            bookmarkButton.image.sprite  = bookmarkButtonInactiveImage;
            linksButton.image.sprite     = linksButtonInactiveImage;
            knowledgeButton.image.sprite = knowledgeButtonInactiveImage;
        }

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
                Debug.LogWarning("Ungültiger Hex-Farbcode: " + hexColor);
            }
        }

        private void SetTextToActive(TextMeshProUGUI textElement)
        {
            if (textElement != null)
                textElement.color = Color.white;
            else
                Debug.LogWarning("TextElement ist nicht gesetzt!");
        }

        private void SetHomeButtonImage()
        {
            homeButton.image.sprite = homeButtonActiveImage;
        }

        private void SetArchivButtonImage()
        {
            archivButton.image.sprite = archivButtonActiveImage;
        }

        private void SetBookmarkButtonImage()
        {
            bookmarkButton.image.sprite = bookmarkButtonActiveImage;
        }

        private void SetLinksButtonImage()
        {
            linksButton.image.sprite = linksButtonActiveImage;
        }
        
        private void SetKnowledgeButtonImage()
        {
            knowledgeButton.image.sprite = knowledgeButtonActiveImage;
        }
    }
}
