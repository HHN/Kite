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
        private const int IntroNovelId = 13;


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

        [Header("Visual Novel Data")] 
        [SerializeField] private VisualNovel visualNovelToDisplay;
        [SerializeField] private VisualNovelNames visualNovelName;

        [Header("Appearance")] 
        [SerializeField] private Color colorOfText;

        /// <summary>
        /// Initializes the NovelDescriptionTextbox by setting up a button click listener.
        /// </summary>
        private void Start()
        {
            playButton.onClick.AddListener(OnPlayButton);
            if (HasBookmarkButton()) bookMarkButton.onClick.AddListener(OnBookmarkButton);
        }

        /// <summary>
        /// Initializes the bookmark button based on whether the Visual Novel is marked as a favorite.
        /// </summary>
        /// <param name="isFavorite">Indicates whether the Visual Novel is marked as a favorite.</param>
        public void InitializeBookMarkButton(bool isFavorite)
        {
            playText.color = colorOfText;

            if (isFavorite)
            {
                SetBookmarkedAppearance();
            }
            else
            {
                SetUnbookmarkedAppearance();
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

            if (HasBookmarkButton()) InitializeBookMarkButton(FavoritesManager.Instance().IsFavorite(visualNovel));
        }

        /// <summary>
        /// Called when the Play button is pressed.
        /// </summary>
        private void OnPlayButton()
        {
            HandleIntroNovelSpecialCase();
            ConfigurePlayManager();
            PlaySelectNovelSound();
            SceneLoader.LoadPlayNovelScene();
        }

        /// <summary>
        /// Called when the Bookmark button is pressed.
        /// </summary>
        private void OnBookmarkButton()
        {
            ToggleFavorite(visualNovelToDisplay);
        }

        /// <summary>
        /// Toggles marking the Visual Novel as favorite.
        /// </summary>
        /// <param name="visualNovel">The Visual Novel.</param>
        private void ToggleFavorite(VisualNovel visualNovel)
        {
            bool isFavorite = FavoritesManager.Instance().IsFavorite(visualNovel);
            
            if (isFavorite)
            {
                FavoritesManager.Instance().UnmarkAsFavorite(visualNovel);
            }
            else
            {
                FavoritesManager.Instance().MarkAsFavorite(visualNovel);
            }
            
            InitializeBookMarkButton(!isFavorite);
        }

        /// <summary>
        /// Sets the activity state of the buttons.
        /// </summary>
        /// <param name="active">Enables or disables the buttons.</param>
        public void SetButtonsActive(bool active)
        {
            playButton.gameObject.SetActive(active);
            if (HasBookmarkButton()) bookMarkButton.gameObject.SetActive(active);
        }

        /// <summary>
        /// Deactivates the bookmark button if it is initialized and active.
        /// </summary>
        public void DeactivatedBookmarkButton()
        {
            if (HasBookmarkButton()) bookMarkButton.gameObject.SetActive(false);
        }

        /// <summary>
        /// Sets the displayed head.
        /// </summary>
        public void ActivateHeadIcon()
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

        /// <summary>
        /// Determines whether the bookmark button exists and is initialized.
        /// </summary>
        /// <returns>True if a bookmark button is present, otherwise false.</returns>
        private bool HasBookmarkButton()
        {
            return bookMarkButton != null;
        }

        /// <summary>
        /// Updates the appearance of the bookmark to reflect a "bookmarked" state
        /// by modifying the text, sprite, and color.
        /// </summary>
        private void SetBookmarkedAppearance()
        {
            bookmarkText.text = BookmarkedText;
            bookmarkImage.sprite = bookmarkSprite;
            bookmarkText.color = Color.white;
        }

        /// <summary>
        /// Configures the appearance of the bookmark elements to indicate an unbookmarked state.
        /// Updates the bookmark text, image, and text color accordingly.
        /// </summary>
        private void SetUnbookmarkedAppearance()
        {
            bookmarkText.text = UnbookmarkedText;
            bookmarkImage.sprite = unBookmarkSprite;
            bookmarkText.color = colorOfText;
        }

        /// <summary>
        /// Handles a special case for the intro novel by resetting the corresponding flag in the GameManager.
        /// </summary>
        private void HandleIntroNovelSpecialCase()
        {
            if (visualNovelToDisplay.id == IntroNovelId)
            {
                GameManager.Instance.IsIntroNovelLoadedFromMainMenu = false;
            }
        }

        /// <summary>
        /// Configures the PlayManager with the information of the currently selected visual novel.
        /// This includes setting the visual novel to play, its color, display name, and designation.
        /// </summary>
        private void ConfigurePlayManager()
        {
            var playManager = PlayManager.Instance();
            playManager.SetVisualNovelToPlay(visualNovelToDisplay);
            playManager.SetColorOfVisualNovelToPlay(visualNovelToDisplay.novelColor);
            playManager.SetDisplayNameOfNovelToPlay(FoundersBubbleMetaInformation.GetDisplayNameOfNovelToPlay(visualNovelName));
        }

        /// <summary>
        /// Plays a sound effect indicating selection of a novel in the novel description textbox.
        /// </summary>
        private void PlaySelectNovelSound()
        {
            GameObject buttonSound = Instantiate(selectNovelSoundPrefab);
            DontDestroyOnLoad(buttonSound);
        }
    }
}