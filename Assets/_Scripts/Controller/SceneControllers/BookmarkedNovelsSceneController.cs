using System.Collections.Generic;
using System.Linq;
using Assets._Scripts.Managers;
using Assets._Scripts.Novel;
using Assets._Scripts.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Controller.SceneControllers
{
    /// <summary>
    /// Manages the Bookmarked Novels Scene by initializing and handling the visual novels marked as favorite.
    /// Responsible for managing the user interaction and displaying relevant bookmarked novels.
    /// </summary>
    public class BookmarkedNovelsSceneController : SceneController
    {
        [SerializeField] private RectTransform visualNovelHolder;
        [SerializeField] private GameObject novelButtonPrefab;

        [SerializeField] private GameObject selectNovelSoundPrefab;

        [Header("Layout")] [SerializeField] private int columns = 3;
        [SerializeField, Range(0.6f, 1.5f)] private float horizontalSpacingMultiplier = 1f;
        [SerializeField, Range(0.6f, 1.5f)] private float verticalSpacingMultiplier = 1f;

        [SerializeField] private float horizontalOffset = 190f;
        [SerializeField] private float verticalOffset = 15.5f;
        
        private float _prefabWidth;
        private float _prefabHeight;

        /// <summary>
        /// Initializes the bookmarked novels scene by setting up the back stack, mapping novel buttons to visual novel names,
        /// adding listener events to buttons, hiding all buttons by default, and populating the UI based on favorite visual novels.
        /// </summary>
        public void Start()
        {
            // Add current scene to back stack for navigation
            BackStackManager.Instance().Push(SceneNames.BookmarkedNovelsScene);
            
            ClearNovelHolder();
            InitializePrefabSize();

            Dictionary<long, VisualNovel> allNovels = KiteNovelManager.Instance().GetAllKiteNovels().ToDictionary(n => n.id);
            List<long> favoriteIds = FavoritesManager.Instance().GetFavoritesIds();
            
            if (favoriteIds.Count == 0) return;
            
            CreateNovelButtons(favoriteIds, allNovels);
            AdjustScrollHeight(favoriteIds.Count);
        }

        /// <summary>
        /// Clears all child objects under the visual novel holder to reset the UI.
        /// This method ensures that the visual novel holder is emptied before populating it with new data.
        /// </summary>
        private void ClearNovelHolder()
        {
            for (int i = visualNovelHolder.childCount - 1; i >= 0; i--)
                Destroy(visualNovelHolder.GetChild(i).gameObject);
        }

        /// <summary>
        /// Calculates and sets the width and height of the novel button prefab based on its RectTransform or LayoutElement values.
        /// Logs an error if the prefab size cannot be determined.
        /// </summary>
        private void InitializePrefabSize()
        {
            if (novelButtonPrefab == null)
            {
                Debug.LogError("NovelButtonPrefab is missing!");
                return;
            }

            RectTransform prefabRect = novelButtonPrefab.GetComponent<RectTransform>();
            if (prefabRect != null)
            {
                _prefabWidth = prefabRect.sizeDelta.x > 0 ? prefabRect.sizeDelta.x : prefabRect.rect.width;
                _prefabHeight = prefabRect.sizeDelta.y > 0 ? prefabRect.sizeDelta.y : prefabRect.rect.height;
            }

            if (_prefabWidth <= 0 || _prefabHeight <= 0)
            {
                var le = novelButtonPrefab.GetComponent<LayoutElement>();
                if (le != null)
                {
                    _prefabWidth = le.preferredWidth > 0 ? le.preferredWidth : _prefabWidth;
                    _prefabHeight = le.preferredHeight > 0 ? le.preferredHeight : _prefabHeight;
                }
            }

            if (_prefabWidth <= 0 || _prefabHeight <= 0)
                Debug.LogError("Could not determine prefab size.");
        }

        /// <summary>
        /// Creates and initializes novel buttons for the bookmarked novels scene based on the provided list of favorite IDs
        /// and the complete collection of visual novels. Each button is configured to represent a specific novel.
        /// </summary>
        /// <param name="favoriteIds">A list of IDs representing the user's bookmarked novels.</param>
        /// <param name="allNovelsById">A dictionary mapping novel IDs to their corresponding visual novel objects.</param>
        private void CreateNovelButtons(List<long> favoriteIds, Dictionary<long, VisualNovel> allNovelsById)
        {
            for (int i = 0; i < favoriteIds.Count; i++)
            {
                if (!allNovelsById.TryGetValue(favoriteIds[i], out var novel))
                {
                    Debug.LogWarning($"Favorite ID {favoriteIds[i]} not found in novels.");
                    continue;
                }

                GameObject go = Instantiate(novelButtonPrefab, visualNovelHolder, false);
                RectTransform rt = go.GetComponent<RectTransform>();
                rt.localScale = Vector3.one;

                rt.anchoredPosition = GetHexPosition(i);
                go.name = novel.isKiteNovel ? novel.designation : novel.title;

                SetupButtonUI(go, novel);
            }
        }

        /// <summary>
        /// Configures the UI components of a button representing a visual novel, including its display text, background color,
        /// and click event for selecting the associated visual novel.
        /// </summary>
        /// <param name="go">The GameObject representing the button to be configured.</param>
        /// <param name="novel">The VisualNovel object containing data to configure the button.</param>
        private void SetupButtonUI(GameObject go, VisualNovel novel)
        {
            var image = go.GetComponent<Image>();
            if (image != null)
                image.color = novel.novelColor;

            var text = go.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null)
                text.text = novel.isKiteNovel ? novel.designation : novel.title;

            var button = go.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => OnNovelSelected(novel));
            }
        }

        /// <summary>
        /// Calculates the position of a visual novel button in a hexagonal grid layout based on its index.
        /// This method determines the row and column for the button, calculates its X and Y coordinates,
        /// and returns a 3D position vector for placement in the scene.
        /// </summary>
        /// <param name="index">The zero-based index of the button in the list of visual novels.</param>
        /// <returns>A Vector3 representing the position of the button in the hexagonal grid layout.</returns>
        private Vector3 GetHexPosition(int index)
        {
            float xSpacing = _prefabWidth * Mathf.Sqrt(3) * 0.5f + horizontalOffset;
            float ySpacing = _prefabHeight * 0.5f + verticalOffset;

            int patternIndex = index;
            int row = 0;
            int col = 0;

            while (true)
            {
                int rowCount = (row % 2 == 0) ? 2 : 1;
                if (patternIndex < rowCount)
                {
                    col = patternIndex;
                    break;
                }

                patternIndex -= rowCount;
                row++;
            }

            float x = 0;
            int rowCountThis = (row % 2 == 0) ? 2 : 1;

            if (rowCountThis == 2)
            {
                x = col == 0 ? -xSpacing : xSpacing;
            }
            else
            {
                x = 0;
            }

            float y = -row * ySpacing;

            return new Vector3(x, y, 0f);
        }

        /// <summary>
        /// Adjusts the height of the scroll view to accommodate the layout of visual novel buttons,
        /// ensuring they are arranged correctly based on the number of favorite novels and their grid spacing.
        /// </summary>
        /// <param name="count">The number of favorite visual novels to display, which determines the required height of the scroll.</param>
        private void AdjustScrollHeight(int count)
        {
            float size = _prefabWidth / 2f;
            GetSpacing(size, out float _, out float ySpacing);

            int totalRows = 0;
            int remaining = count;
            while (remaining > 0)
            {
                totalRows++;
                remaining -= (totalRows % 2 == 1) ? 2 : 1;
            }

            float height = (totalRows - 1) * ySpacing + _prefabHeight;

            var le = visualNovelHolder.GetComponent<LayoutElement>();
            if (le != null) le.preferredHeight = height;
        }

        /// <summary>
        /// Calculates the horizontal and vertical spacing for elements based on the size of each element
        /// and the spacing multipliers defined for horizontal and vertical spacing.
        /// </summary>
        /// <param name="size">The size of the element used to calculate spacing.</param>
        /// <param name="xSpacing">The calculated horizontal spacing for the elements.</param>
        /// <param name="ySpacing">The calculated vertical spacing for the elements.</param>
        private void GetSpacing(float size, out float xSpacing, out float ySpacing)
        {
            xSpacing = size * Mathf.Sqrt(3) * horizontalSpacingMultiplier;
            ySpacing = size * 1.5f * verticalSpacingMultiplier;
        }

        /// <summary>
        /// Handles the process of loading and preparing the selected visual novel for play.
        /// </summary>
        /// <param name="visualNovel">The name of the visual novel to be loaded and played.</param>
        private void OnNovelSelected(VisualNovel visualNovel)
        {
            if (visualNovel == null)
            {
                DisplayErrorMessage("Die gewünschte Novel konnte nicht geladen werden.");
                return;
            }

            NovelColorManager.Instance().SetColor(visualNovel.novelColor);

            var playManager = PlayManager.Instance();
            playManager.SetVisualNovelToPlay(visualNovel);
            playManager.SetColorOfVisualNovelToPlay(visualNovel.novelColor);
            playManager.SetDisplayNameOfNovelToPlay(visualNovel.title);
            playManager.SetDesignationOfNovelToPlay(visualNovel.designation);

            if (selectNovelSoundPrefab != null)
            {
                var sound = Instantiate(selectNovelSoundPrefab);
                DontDestroyOnLoad(sound);
            }

            SceneLoader.LoadPlayNovelScene();
        }
    }
}