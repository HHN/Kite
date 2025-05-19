using System.Collections;
using Assets._Scripts.Managers;
using Assets._Scripts.Player;
using Assets._Scripts.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Novel
{
    public class NovelDescriptionTextbox : MonoBehaviour
    {
        private const string BookmarkedText = "GEMERKT";
        private const string UnbookmarkedText = "MERKEN";

        [Header("UI Elemente")] [SerializeField]
        private Image image;

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
        }

        /// <summary>
        /// Setzt den angezeigten Text.
        /// </summary>
        /// <param name="novelDescription">Der anzuzeigende Text.</param>
        public void SetText(string novelDescription)
        {
            text.text = novelDescription;
        }

        /// <summary>
        /// Setzt den Namen der Visual Novel.
        /// </summary>
        /// <param name="novelName">Der Name der Visual Novel.</param>
        public void SetVisualNovelName(VisualNovelNames novelName)
        {
            visualNovelName = novelName;
            Debug.Log(novelName);
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
        /// Wird aufgerufen, wenn der Play-Button gedrückt wird.
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
        /// Wird aufgerufen, wenn der Lesezeichen-Button gedrückt wird.
        /// </summary>
        private void OnBookmarkButton()
        {
            StartCoroutine(MarkAsFavorite(visualNovelToDisplay));
        }

        /// <summary>
        /// Toggled die Markierung der Visual Novel als Favorit.
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
        /// Setzt die Aktivität der Buttons.
        /// </summary>
        /// <param name="active">Aktiviert oder deaktiviert die Buttons.</param>
        public void SetButtonsActive(bool active)
        {
            playButton.gameObject.SetActive(active);
            if (bookMarkButton != null) bookMarkButton.gameObject.SetActive(active);
        }

        /// <summary>
        /// Setzt den angezeigten Kopf basierend auf der H�he.
        /// </summary>
        /// <param name="isHigh">True f�r gro�en Kopf, False f�r kleinen Kopf.</param>
        public void SetHead(bool isHigh)
        {
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