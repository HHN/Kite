using Assets._Scripts.SceneControllers;
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

        private void Start()
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
                case SceneNames.GemerkteNovelsScene:
                    SetBookmarkButtonImage();
                    SetTextToActive(gemerktTextElement);
                    break;
                case SceneNames.RessourcenScene:
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
            if (currentScene.Equals(SceneNames.FoundersBubbleScene))
            {
                return;
            }

            SceneLoader.LoadFoundersBubbleScene();
        }

        private void OnArchivButton()
        {
            if (currentScene.Equals(SceneNames.NovelHistoryScene))
            {
                return;
            }

            SceneLoader.LoadNovelHistoryScene();
        }

        private void OnBookmarkButton()
        {
            if (currentScene.Equals(SceneNames.GemerkteNovelsScene))
            {
                return;
            }

            SceneLoader.LoadGemerkteNovelsScene();
        }

        private void OnLinksButton()
        {
            if (currentScene.Equals(SceneNames.RessourcenScene))
            {
                return;
            }

            SceneLoader.LoadRessourcenScene();
        }
        
        private void OnKnowledgeButton()
        {
            SceneLoader.LoadKnowledgeScene();
        }

        private void SetButtonImagesInaktiv()
        {
            homeButton.image.sprite = homeButtonInactiveImage;
            archivButton.image.sprite = archivButtonInactiveImage;
            bookmarkButton.image.sprite = bookmarkButtonInactiveImage;
            linksButton.image.sprite = linksButtonInactiveImage;
            knowledgeButton.image.sprite = knowledgeButtonInactiveImage;
        }

        private void SetTextColorsInaktiv()
        {
            string hexColor = "#B6BBC0";
            if (startTextElement != null)
            {
                if (ColorUtility.TryParseHtmlString(hexColor, out Color newColor))
                {
                    startTextElement.color = newColor;
                    archivTextElement.color = newColor;
                    gemerktTextElement.color = newColor;
                    linkTextElement.color = newColor;
                    knowledgeTextElement.color = newColor;
                }
                else
                {
                    Debug.LogWarning("Ungültiger Hex-Farbcode: " + hexColor);
                }
            }
            else
            {
                Debug.LogWarning("TextElement ist nicht gesetzt!");
            }
        }

        private void SetTextToActive(TextMeshProUGUI textElement)
        {
            if (textElement != null)
            {
                textElement.color = Color.white;
            }
            else
            {
                Debug.LogWarning("TextElement ist nicht gesetzt!");
            }
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
