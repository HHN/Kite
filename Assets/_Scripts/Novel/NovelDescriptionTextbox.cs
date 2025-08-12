using System.Collections;
using Assets._Scripts.Managers;
using Assets._Scripts.Player;
using Assets._Scripts.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Novel
{
    /// <summary>
    /// Represents a description textbox for a visual novel, providing functionalities
    /// to display and manage visual novel details including text, color, and bookmark status.
    /// </summary>
    public class NovelDescriptionTextbox : MonoBehaviour
    {
        private const string BookmarkedText = "GEMERKT";
        private const string UnbookmarkedText = "MERKEN";

        [Header("UI Elements")] 
        [SerializeField] private Image image;

        [SerializeField] private GameObject smallHead;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Button playButton;
        [SerializeField] private Button bookMarkButton;
        [SerializeField] private TextMeshProUGUI playText;
        [SerializeField] private TextMeshProUGUI bookmarkText;
        [SerializeField] private Image bookmarkImage;
        [SerializeField] private Sprite bookmarkSprite;
        [SerializeField] private Sprite unBookmarkSprite;
        [SerializeField] private GameObject selectNovelSoundPrefab;

        [Header("Visual Novel Data")] [SerializeField]
        private VisualNovel visualNovelToDisplay;

        [SerializeField] private VisualNovelNames visualNovelName;

        [Header("Appearance")] [SerializeField]
        private Color colorOfText;

        /// <summary>
        /// Initializes the NovelDescriptionTextbox by setting up button click listeners.
        /// </summary>
        private void Start()
        {
            playButton.onClick.AddListener(OnPlayButton);
            if (bookMarkButton != null) bookMarkButton.onClick.AddListener(OnBookmarkButton);
        }

        /// <summary>
        /// Initializes the bookmark button based on whether the Visual Novel is marked as favorite.
        /// </summary>
        /// <param name="isFavorite">Indicates whether the Visual Novel is marked as favorite.</param>
        public void InitializeBookMarkButton(bool isFavorite)
        {
            playText.color = colorOfText;

            if (isFavorite)
            {
                bookmarkText.text = BookmarkedText;
                bookmarkImage.sprite = bookmarkSprite;
                bookmarkText.color = Color.white;
            }
            else
            {
                bookmarkText.text = UnbookmarkedText;
                bookmarkImage.sprite = unBookmarkSprite;
                bookmarkText.color = colorOfText;
            }
        }

        /// <summary>
        /// Sets the color of the image and head icons.
        /// </summary>
        /// <param name="color">The color to set.</param>
        public void SetColorOfImage(Color color)
        {
            colorOfText = color;
            image.color = color;
            smallHead.GetComponent<Image>().color = color;
        }

        /// <summary>
        /// Sets the displayed text.
        /// </summary>
        /// <param name="novelDescription">The text to display.</param>
        public void SetText(string novelDescription)
        {
            text.text = novelDescription;
        }

        /// <summary>
        /// Sets the name of the Visual Novel.
        /// </summary>
        /// <param name="novelName">The name of the Visual Novel.</param>
        public void SetVisualNovelName(VisualNovelNames novelName)
        {
            visualNovelName = novelName;
        }

        /// <summary>
        /// Sets the Visual Novel to display.
        /// </summary>
        /// <param name="visualNovel">The Visual Novel.</param>
        public void SetVisualNovel(VisualNovel visualNovel)
        {
            visualNovelToDisplay = visualNovel;

            if (bookMarkButton != null) InitializeBookMarkButton(FavoritesManager.Instance().IsFavorite(visualNovel));
        }

        /// <summary>
        /// Called when the Play button is pressed.
        /// </summary>
        private void OnPlayButton()
        {
            if (visualNovelToDisplay.id == 13)
            {
                GameManager.Instance.IsIntroNovelLoadedFromMainMenu = false;
            }

            PlayManager.Instance().SetVisualNovelToPlay(visualNovelToDisplay);
            PlayManager.Instance().SetColorOfVisualNovelToPlay(visualNovelToDisplay.novelColor);
            PlayManager.Instance().SetDisplayNameOfNovelToPlay(FoundersBubbleMetaInformation.GetDisplayNameOfNovelToPlay(visualNovelName));
            PlayManager.Instance().SetDesignationOfNovelToPlay(visualNovelToDisplay.designation);
            GameObject buttonSound = Instantiate(selectNovelSoundPrefab);
            DontDestroyOnLoad(buttonSound);

            if (ShowPlayInstructionManager.Instance().ShowInstruction() && visualNovelToDisplay.title != "EinstiegsNovel")
            {
                SceneLoader.LoadPlayInstructionScene();
            }
            else
            {
                SceneLoader.LoadPlayNovelScene();
            }
        }

        /// <summary>
        /// Called when the Bookmark button is pressed.
        /// </summary>
        private void OnBookmarkButton()
        {
            StartCoroutine(MarkAsFavorite(visualNovelToDisplay));
        }

        /// <summary>
        /// Toggles marking the Visual Novel as favorite.
        /// </summary>
        /// <param name="visualNovel">The Visual Novel.</param>
        private IEnumerator MarkAsFavorite(VisualNovel visualNovel)
        {
            if (FavoritesManager.Instance().IsFavorite(visualNovel))
            {
                FavoritesManager.Instance().UnmarkAsFavorite(visualNovel);
                InitializeBookMarkButton(false);
                yield break;
            }

            FavoritesManager.Instance().MarkAsFavorite(visualNovel);
            InitializeBookMarkButton(true);
            yield return null;
        }

        /// <summary>
        /// Sets the activity state of the buttons.
        /// </summary>
        /// <param name="active">Enables or disables the buttons.</param>
        public void SetButtonsActive(bool active)
        {
            playButton.gameObject.SetActive(active);
            if (bookMarkButton != null) bookMarkButton.gameObject.SetActive(active);
        }

        public void DeactivatedBookmarkButton()
        {
            bookMarkButton.gameObject.SetActive(false);
        }

        /// <summary>
        /// Sets the displayed head.
        /// </summary>
        public void SetHead()
        {
            smallHead.SetActive(true);
        }

        /// <summary>
        /// Updates the layout size.
        /// </summary>
        public void UpdateSize()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        }
    }
}