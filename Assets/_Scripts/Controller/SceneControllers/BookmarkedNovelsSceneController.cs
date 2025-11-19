using System.Collections.Generic;
using System.Linq;
using Assets._Scripts.Managers;
using Assets._Scripts.Novel;
using Assets._Scripts.SceneManagement;
using Assets._Scripts.Utilities;
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

        [Header("Layout")] 
        [SerializeField] private int columns = 3;
        [SerializeField, Range(0.6f, 1.5f)] private float horizontalSpacingMultiplier = 1f;
        [SerializeField, Range(0.6f, 1.5f)] private float verticalSpacingMultiplier = 1f;

        [SerializeField] private float horizontalOffset = 190f;
        [SerializeField] private float verticalOffset = 15.5f;
        
        private const int ColumnsInEvenRow = 2;
        private const int ColumnsInOddRow = 1;
        private const float Sqrt3Half = 0.866025404f; // Sqrt(3) / 2
        
        private float _prefabWidth;
        private float _prefabHeight;

        /// <summary>
        /// Initializes the bookmarked novels scene by setting up the back stack, mapping novel buttons to visual novel names,
        /// adding listener events to buttons, hiding all buttons by default, and populating the UI based on favorite visual novels.
        /// </summary>
        public void Start()
        {
            // Add current scene to back stack for navigation
            BackStackManager.Instance.Push(SceneNames.BookmarkedNovelsScene);
            
            ClearNovelHolder();
            CalculatePrefabDimensions();

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
        /// Calculates the dimensions of the novel button prefab for proper layout and rendering.
        /// Attempts to determine size using its RectTransform and validates the result.
        /// If the size cannot be determined through RectTransform, fallback logic attempts to use additional layout data.
        /// Logs an error if the dimensions cannot be determined.
        /// </summary>
        private void CalculatePrefabDimensions()
        {
            if (novelButtonPrefab == null)
            {
                LogManager.Error("NovelButtonPrefab is missing!");
                return;
            }

            TryGetSizeFromRectTransform();
            
            if (!ValidatePrefabSize())
            {
                TryGetSizeFromLayoutElement();
            }

            if (!ValidatePrefabSize())
            {
                LogManager.Error("Could not determine prefab size.");
            }
        }

        /// <summary>
        /// Attempts to retrieve the size dimensions (width and height) of the `novelButtonPrefab`
        /// from its associated `RectTransform` component. Extracted values are assigned to the
        /// `_prefabWidth` and `_prefabHeight` fields. If the `sizeDelta` dimensions are nonzero,
        /// those values are used. Otherwise, the dimensions of the `Rect` are used.
        /// </summary>
        private void TryGetSizeFromRectTransform()
        {
            RectTransform prefabRect = novelButtonPrefab.GetComponent<RectTransform>();
            if (prefabRect != null)
            {
                _prefabWidth = prefabRect.sizeDelta.x > 0 ? prefabRect.sizeDelta.x : prefabRect.rect.width;
                _prefabHeight = prefabRect.sizeDelta.y > 0 ? prefabRect.sizeDelta.y : prefabRect.rect.height;
            }
        }

        /// <summary>
        /// Attempts to update the width and height of the prefab by retrieving preferred dimensions
        /// from the LayoutElement component attached to the prefab. If the LayoutElement exists and
        /// preferred dimensions are specified, these values are assigned to the prefab's dimensions.
        /// </summary>
        private void TryGetSizeFromLayoutElement()
        {
            var layoutElement = novelButtonPrefab.GetComponent<LayoutElement>();
            if (layoutElement != null)
            {
                _prefabWidth = layoutElement.preferredWidth > 0 ? layoutElement.preferredWidth : _prefabWidth;
                _prefabHeight = layoutElement.preferredHeight > 0 ? layoutElement.preferredHeight : _prefabHeight;
            }
        }

        /// <summary>
        /// Validates the dimensions of the prefab by checking if the width and height are both greater than zero.
        /// </summary>
        /// <returns>
        /// Returns true if both width and height of the prefab are greater than zero, otherwise returns false.
        /// </returns>
        private bool ValidatePrefabSize()
        {
            return _prefabWidth > 0 && _prefabHeight > 0;
        }

        /// <summary>
        /// Generates and initializes buttons for the favorite visual novels based on the provided lists of favorite IDs
        /// and all available novels. Each button is tied to a novel and indexed for a proper UI arrangement.
        /// </summary>
        /// <param name="favoriteIds">A list of IDs representing the user's favorite visual novels.</param>
        /// <param name="allNovelsById">A dictionary containing all available visual novels keyed by their unique IDs.</param>
        private void CreateNovelButtons(List<long> favoriteIds, Dictionary<long, VisualNovel> allNovelsById)
        {
            for (int i = 0; i < favoriteIds.Count; i++)
            {
                if (!allNovelsById.TryGetValue(favoriteIds[i], out var novel))
                {
                    LogManager.Warning($"Favorite ID {favoriteIds[i]} not found in novels.");
                    continue;
                }

                CreateSingleNovelButton(novel, i);
            }
        }

        /// <summary>
        /// Creates a single visual novel button based on the provided visual novel data and its index.
        /// This method initializes the button's position, scale, name, and visual representation.
        /// </summary>
        /// <param name="novel">The visual novel information used to populate the button's details.</param>
        /// <param name="index">The positional index of the button, used to calculate its placement.</param>
        private void CreateSingleNovelButton(VisualNovel novel, int index)
        {
            GameObject buttonObject = Instantiate(novelButtonPrefab, visualNovelHolder, false);
            RectTransform rectTransform = buttonObject.GetComponent<RectTransform>();
            
            rectTransform.localScale = Vector3.one;
            rectTransform.anchoredPosition = GetHexPosition(index);
            
            buttonObject.name = novel.title;
            SetupButtonUI(buttonObject, novel);
        }

        /// <summary>
        /// Configures the UI and functionality of a button associated with a specific visual novel.
        /// Sets the button's image, text, and click event handler based on the corresponding visual novel properties.
        /// </summary>
        /// <param name="go">The game object representing the button in the UI.</param>
        /// <param name="novel">The visual novel associated with the button, providing necessary data for configuration.</param>
        private void SetupButtonUI(GameObject go, VisualNovel novel)
        {
            ConfigureButtonImage(go, novel);
            ConfigureButtonText(go, novel);
            ConfigureButtonClickHandler(go, novel);
        }

        /// <summary>
        /// Configures the image of a button based on the properties of the provided visual novel.
        /// Updates the button's color using the novel's predefined color attribute.
        /// </summary>
        /// <param name="go">The button game object whose image component is to be configured.</param>
        /// <param name="novel">The visual novel object containing the color properties to apply to the button.</param>
        private void ConfigureButtonImage(GameObject go, VisualNovel novel)
        {
            var image = go.GetComponent<Image>();
            if (image != null)
                image.color = novel.novelColor;
        }

        /// <summary>
        /// Updates the text component of the button to display information about the given visual novel,
        /// such as its title or designation, based on whether the novel is categorized as a Kite novel.
        /// </summary>
        /// <param name="go">The game object representing the button that will be updated.</param>
        /// <param name="novel">The visual novel whose details are to be displayed on the button.</param>
        private void ConfigureButtonText(GameObject go, VisualNovel novel)
        {
            var text = go.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null)
                text.text = novel.title;
        }

        /// <summary>
        /// Configures the click handler for a button associated with a given visual novel,
        /// ensuring the appropriate action is executed when the button is clicked.
        /// </summary>
        /// <param name="go">The GameObject representing the button to configure.</param>
        /// <param name="novel">The VisualNovel associated with the button that will be passed to the click handler.</param>
        private void ConfigureButtonClickHandler(GameObject go, VisualNovel novel)
        {
            var button = go.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => OnNovelSelected(novel));
            }
        }

        /// <summary>
        /// Calculates the hexagonal grid position based on the given index, taking into account
        /// the horizontal and vertical spacing, as well as the hexagonal row and column alignment.
        /// </summary>
        /// <param name="index">The index of the hexagonal grid element for which the position is being calculated.</param>
        /// <returns>The calculated position as a <see cref="Vector3"/>, representing the local position in the grid.</returns>
        private Vector3 GetHexPosition(int index)
        {
            float horizontalSpacing = _prefabWidth * Sqrt3Half + horizontalOffset;
            float verticalSpacing = _prefabHeight * 0.5f + verticalOffset;
            
            var (row, column) = CalculateHexRowAndColumn(index);
            
            float x = CalculateHexXPosition(row, column, horizontalSpacing);
            float y = -row * verticalSpacing;
            
            return new Vector3(x, y, 0f);
        }

        /// <summary>
        /// Calculates the hexagonal grid row and column indices based on the provided flat index.
        /// This method determines the position of a visual element in a hexagonal grid by iterating through rows until the
        /// specified index is located within a particular row.
        /// </summary>
        /// <param name="index">The flat index representing the position in a sequential collection of grid elements.</param>
        /// <returns>A tuple containing the calculated row and column indices in the hexagonal grid.</returns>
        private (int row, int column) CalculateHexRowAndColumn(int index)
        {
            int remainingIndex = index;
            int row = 0;
            int column;
            
            while (true)
            {
                int columnsInCurrentRow = GetColumnsInRow(row);
                
                if (remainingIndex < columnsInCurrentRow)
                {
                    column = remainingIndex;
                    break;
                }
                
                remainingIndex -= columnsInCurrentRow;
                row++;
            }
            
            return (row, column);
        }

        /// <summary>
        /// Determines the number of columns in a given row based on whether the row is odd or even.
        /// </summary>
        /// <param name="row">The row index for which the number of columns is to be determined.</param>
        /// <returns>The number of columns in the specified row. Returns COLUMNS_IN_EVEN_ROW for even rows and COLUMNS_IN_ODD_ROW for odd rows.</returns>
        private int GetColumnsInRow(int row)
        {
            bool isEvenRow = row % 2 == 0;
            return isEvenRow ? ColumnsInEvenRow : ColumnsInOddRow;
        }

        /// <summary>
        /// Calculates the horizontal X position for a hexagonal grid layout based on the given row, column, and horizontal spacing.
        /// </summary>
        /// <param name="row">The row of the hexagonal grid where the element is located.</param>
        /// <param name="column">The column within the current row of the hexagonal grid.</param>
        /// <param name="horizontalSpacing">The spacing between columns in the hexagonal grid.</param>
        /// <returns>Returns the calculated horizontal X position of the hexagonal cell.</returns>
        private float CalculateHexXPosition(int row, int column, float horizontalSpacing)
        {
            int columnsInCurrentRow = GetColumnsInRow(row);

            if (columnsInCurrentRow == ColumnsInEvenRow)
            {
                return column == 0 ? -horizontalSpacing : horizontalSpacing;
            }
            
            return 0f;
        }

        /// <summary>
        /// Adjusts the height of the scroll view to accommodate the layout of visual novel buttons,
        /// ensuring they are arranged correctly based on the number of favorite novels and their grid spacing.
        /// </summary>
        /// <param name="count">The number of favorite visual novels to display, which determines the required height of the scroll.</param>
        private void AdjustScrollHeight(int count)
        {
            float size = _prefabWidth / 2f;
            GetSpacing(size, out float ySpacing);
            
            int totalRows = CalculateTotalRows(count);
            float requiredHeight = totalRows * ySpacing;
            
            visualNovelHolder.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, requiredHeight);
        }

        /// <summary>
        /// Calculates the total number of rows required to display a given number of items,
        /// based on the number of columns that can fit in each row.
        /// </summary>
        /// <param name="count">The total number of items to be displayed.</param>
        /// <returns>The total number of rows required to display all items.</returns>
        private int CalculateTotalRows(int count)
        {
            int totalRows = 0;
            int remainingItems = count;
            
            while (remainingItems > 0)
            {
                totalRows++;
                int columnsInCurrentRow = GetColumnsInRow(totalRows - 1);
                remainingItems -= columnsInCurrentRow;
            }
            
            return totalRows;
        }

        /// <summary>
        /// Calculates the horizontal and vertical spacing for elements based on the size of each element
        /// and the spacing multipliers defined for horizontal and vertical spacing.
        /// </summary>
        /// <param name="size">The size of the element used to calculate spacing.</param>
        /// <param name="ySpacing">The calculated vertical spacing for the elements.</param>
        private void GetSpacing(float size, out float ySpacing)
        {
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

            if (selectNovelSoundPrefab != null)
            {
                var sound = Instantiate(selectNovelSoundPrefab);
                DontDestroyOnLoad(sound);
            }

            SceneLoader.LoadPlayNovelScene();
        }
    }
}