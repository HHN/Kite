using System.Collections;
using Assets._Scripts.Managers;
using Assets._Scripts.Novel;
using Assets._Scripts.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Player
{
    public class NovelDescriptionTextbox : MonoBehaviour
    {
        private const string BookmarkedText = "GEMERKT";
        private const string UnbookmarkedText = "MERKEN";

        [Header("UI Elemente")] [SerializeField]
        private Image image;

        [SerializeField] private GameObject smallHead;
        [SerializeField] private GameObject bigHead;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Button playButton;
        [SerializeField] private Button bookMarkButton;
        [SerializeField] private TextMeshProUGUI playText;
        [SerializeField] private TextMeshProUGUI bookmarkText;
        [SerializeField] private Image bookmarkImage;
        [SerializeField] private Sprite bookmarkSprite;
        [SerializeField] private Sprite unBookmarkSprite;
        [SerializeField] private GameObject selectNovelSoundPrefab;

        [Header("Visual Novel Daten")] [SerializeField]
        private VisualNovel visualNovelToDisplay;

        [SerializeField] private VisualNovelNames visualNovelName;

        [Header("Erscheinungsbild")] [SerializeField]
        private Color colorOfText;

        private void Start()
        {
            playButton.onClick.AddListener(OnPlayButton);
            if (bookMarkButton != null) bookMarkButton.onClick.AddListener(OnBookmarkButton);
        }

        /// <summary>
        /// Initialisiert den Lesezeichen-Button basierend darauf, ob die Visual Novel favorisiert ist.
        /// </summary>
        /// <param name="isFavorite">Gibt an, ob die Visual Novel favorisiert ist.</param>
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
        /// Setzt die Farbe des Bildes und der Kopf-Icons.
        /// </summary>
        /// <param name="color">Die zu setzende Farbe.</param>
        public void SetColorOfImage(Color color)
        {
            colorOfText = color;
            image.color = color;
            smallHead.GetComponent<Image>().color = color;
            bigHead.GetComponent<Image>().color = color;
        }

        /// <summary>
        /// Setzt den angezeigten Text.
        /// </summary>
        /// <param name="text">Der anzuzeigende Text.</param>
        public void SetText(string text)
        {
            this.text.text = text;
        }

        /// <summary>
        /// Setzt den Namen der Visual Novel.
        /// </summary>
        /// <param name="visualNovelName">Der Name der Visual Novel.</param>
        public void SetVisualNovelName(VisualNovelNames visualNovelName)
        {
            this.visualNovelName = visualNovelName;
        }

        /// <summary>
        /// Legt die anzuzeigende Visual Novel fest.
        /// </summary>
        /// <param name="visualNovel">Die Visual Novel.</param>
        public void SetVisualNovel(VisualNovel visualNovel)
        {
            visualNovelToDisplay = visualNovel;

            if (bookMarkButton != null) InitializeBookMarkButton(FavoritesManager.Instance().IsFavorite(visualNovel));
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Play-Button gedr�ckt wird.
        /// </summary>
        private void OnPlayButton()
        {
            if (visualNovelToDisplay.id == 13)
            {
                GameManager.Instance.IsIntroNovelLoadedFromMainMenu = false;
            }

            PlayManager.Instance().SetVisualNovelToPlay(visualNovelToDisplay);
            PlayManager.Instance()
                .SetForegroundColorOfVisualNovelToPlay(
                    FoundersBubbleMetaInformation.GetForegroundColorOfNovel(visualNovelName));
            PlayManager.Instance()
                .SetBackgroundColorOfVisualNovelToPlay(
                    FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(visualNovelName));
            PlayManager.Instance()
                .SetDisplayNameOfNovelToPlay(
                    FoundersBubbleMetaInformation.GetDisplayNameOfNovelToPlay(visualNovelName));
            GameObject buttonSound = Instantiate(selectNovelSoundPrefab);
            DontDestroyOnLoad(buttonSound);

            if (ShowPlayInstructionManager.Instance().ShowInstruction() &&
                visualNovelToDisplay.title != "Einstiegsdialog")
            {
                SceneLoader.LoadPlayInstructionScene();
            }
            else
            {
                SceneLoader.LoadPlayNovelScene();
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Lesezeichen-Button gedr�ckt wird.
        /// </summary>
        private void OnBookmarkButton()
        {
            StartCoroutine(MarkAsFavorite(visualNovelToDisplay));
        }

        /// <summary>
        /// Markiert oder entmarkiert die Visual Novel als Favorit.
        /// </summary>
        /// <param name="visualNovel">Die Visual Novel.</param>
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
        /// Setzt die Aktivit�t der Buttons.
        /// </summary>
        /// <param name="active">Aktiviert oder deaktiviert die Buttons.</param>
        public void SetButtonsActive(bool active)
        {
            playButton.gameObject.SetActive(active);
            
            if (visualNovelToDisplay.id == 13)
            {
                bookMarkButton.gameObject.SetActive(false);
            }
            else
            {
                if (bookMarkButton != null) bookMarkButton.gameObject.SetActive(active);
            }
        }

        /// <summary>
        /// Setzt den angezeigten Kopf basierend auf der H�he.
        /// </summary>
        /// <param name="isHigh">True f�r gro�en Kopf, False f�r kleinen Kopf.</param>
        public void SetHead(bool isHigh)
        {
            bigHead.SetActive(isHigh);
            smallHead.SetActive(!isHigh);
        }

        /// <summary>
        /// Aktualisiert die Gr��e des Layouts.
        /// </summary>
        public void UpdateSize()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        }
    }
}