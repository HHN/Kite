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
        [SerializeField] private Button profilButton;

        [SerializeField] private Sprite homeButtonActiveImage;
        [SerializeField] private Sprite homeButtonInactiveImage;

        [SerializeField] private Sprite archivButtonActiveImage;
        [SerializeField] private Sprite archivButtonInactiveImage;

        [SerializeField] private Sprite bookmarkButtonActiveImage;
        [SerializeField] private Sprite bookmarkButtonInactiveImage;

        [SerializeField] private Sprite linksButtonActiveImage;
        [SerializeField] private Sprite linksButtonInactiveImage;

        [SerializeField] private Sprite profilButtonActiveImage;
        [SerializeField] private Sprite profilButtonInactiveImage;

        [SerializeField] private TextMeshProUGUI startTextElement;
        [SerializeField] private TextMeshProUGUI archivTextElement;
        [SerializeField] private TextMeshProUGUI gemerktTextElement;
        [SerializeField] private TextMeshProUGUI linkTextElement;
        [SerializeField] private TextMeshProUGUI profilTextElement;

        [SerializeField] private string currentScene;

        private void Start()
        {
            homeButton.onClick.AddListener(OnHomeButton);
            archivButton.onClick.AddListener(OnArchivButton);
            bookmarkButton.onClick.AddListener(OnBookmarkButton);
            linksButton.onClick.AddListener(OnLinksButton);
            profilButton.onClick.AddListener(OnProfilButton);
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
                case SceneNames.FOUNDERS_BUBBLE_SCENE:
                    SetHomeButtonImage();
                    SetTextToActive(startTextElement);
                    break;
                case SceneNames.NOVEL_HISTORY_SCENE:
                    SetArchivButtonImage();
                    SetTextToActive(archivTextElement);
                    break;
                case SceneNames.GEMERKTE_NOVELS_SCENE:
                    SetBookmarkButtonImage();
                    SetTextToActive(gemerktTextElement);
                    break;
                case SceneNames.RESSOURCEN_SCENE:
                    SetLinksButtonImage();
                    SetTextToActive(linkTextElement);
                    break;
                case SceneNames.FOUNDERS_WELL_2_SCENE:
                    SetProfilButtonImage();
                    SetTextToActive(profilTextElement);
                    break;
            }
        }

        private void OnHomeButton()
        {
            if (currentScene.Equals(SceneNames.FOUNDERS_BUBBLE_SCENE))
            {
                return;
            }

            SceneLoader.LoadFoundersBubbleScene();
        }

        private void OnArchivButton()
        {
            if (currentScene.Equals(SceneNames.NOVEL_HISTORY_SCENE))
            {
                return;
            }

            SceneLoader.LoadNovelHistoryScene();
        }

        private void OnBookmarkButton()
        {
            if (currentScene.Equals(SceneNames.GEMERKTE_NOVELS_SCENE))
            {
                return;
            }

            SceneLoader.LoadGemerkteNovelsScene();
        }

        private void OnLinksButton()
        {
            if (currentScene.Equals(SceneNames.RESSOURCEN_SCENE))
            {
                return;
            }

            SceneLoader.LoadRessourcenScene();
        }

        private void OnProfilButton()
        {
            if (currentScene.Equals(SceneNames.FOUNDERS_WELL_2_SCENE))
            {
                return;
            }

            SceneLoader.LoadFoundersWell2Scene();
        }

        private void SetButtonImagesInaktiv()
        {
            homeButton.image.sprite = homeButtonInactiveImage;
            archivButton.image.sprite = archivButtonInactiveImage;
            bookmarkButton.image.sprite = bookmarkButtonInactiveImage;
            linksButton.image.sprite = linksButtonInactiveImage;
            profilButton.image.sprite = profilButtonInactiveImage;
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
                    profilTextElement.color = newColor;
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

        private void SetProfilButtonImage()
        {
            profilButton.image.sprite = profilButtonActiveImage;
        }
    }
}