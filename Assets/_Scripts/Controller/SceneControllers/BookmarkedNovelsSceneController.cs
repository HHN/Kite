using System.Collections.Generic;
using System.Linq;
using Assets._Scripts.Managers;
using Assets._Scripts.Novel;
using Assets._Scripts.Player;
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
        [SerializeField] private Button bankkreditNovel;
        [SerializeField] private Button investorNovel;
        [SerializeField] private Button elternNovel;
        [SerializeField] private Button notarinNovel;
        [SerializeField] private Button presseNovel;
        [SerializeField] private Button bueroNovel;
        [SerializeField] private Button honorarNovel;

        [SerializeField] private GameObject selectNovelSoundPrefab;

        private Dictionary<VisualNovelNames, Button> _novelButtons;

        /// <summary>
        /// Initializes the bookmarked novels scene by setting up the back stack, mapping novel buttons to visual novel names,
        /// adding listener events to buttons, hiding all buttons by default, and populating the UI based on favorite visual novels.
        /// </summary>
        public void Start()
        {
            // Add current scene to back stack for navigation
            BackStackManager.Instance().Push(SceneNames.BookmarkedNovelsScene);
            
            // Initialize dictionary mapping novel names to UI buttons
            _novelButtons = new Dictionary<VisualNovelNames, Button>
            {
                { VisualNovelNames.BankKreditNovel, bankkreditNovel },
                { VisualNovelNames.InvestorNovel, investorNovel },
                { VisualNovelNames.ElternNovel, elternNovel },
                { VisualNovelNames.NotariatNovel, notarinNovel },
                { VisualNovelNames.PresseNovel, presseNovel },
                { VisualNovelNames.VermieterNovel, bueroNovel },
                { VisualNovelNames.HonorarNovel, honorarNovel },
            };

            // Add click listeners and hide all novel buttons initially
            foreach (var novelButton in _novelButtons)
            {
                novelButton.Value.onClick.AddListener(() => OnNovelButton(novelButton.Key));
                novelButton.Value.gameObject.SetActive(false);
            }
            
            // Get all available novels and create a lookup by ID
            List<VisualNovel> allKiteNovels = KiteNovelManager.Instance().GetAllKiteNovels();
            Dictionary<long, VisualNovel> allKiteNovelsById = allKiteNovels.ToDictionary(novel => novel.id);

            // Get a list of favorite novel IDs
            List<long> favoriteIds = FavoritesManager.Instance().GetFavoritesIds();
            int index = 0;

            // Process each favorite novel
            foreach (long id in favoriteIds)
            {
                if (allKiteNovelsById.TryGetValue(id, out VisualNovel foundNovel))
                {
                    // Get the button instance for this novel
                    GameObject novelButtonInstance = GetNovelById(id);

                    if (novelButtonInstance == null)
                    {
                        Debug.LogWarning($"Button instance for Novel ID {id} not found in scene. Skipping.");
                        continue;
                    }

                    // Position and show the button
                    Vector3 localPosition = GetLocalPositionByIndex(index);
                    novelButtonInstance.transform.localPosition = localPosition;
                    novelButtonInstance.gameObject.SetActive(true);

                    // Set the button text
                    TextMeshProUGUI novelText = novelButtonInstance.gameObject.GetComponentInChildren<TextMeshProUGUI>();
                    if (novelText != null)
                    {
                        novelText.text = foundNovel.isKiteNovel ? foundNovel.designation : foundNovel.title;
                    }
                    else
                    {
                        Debug.LogWarning($"TextMeshProUGUI component not found on novel button for ID: {id}");
                    }

                    index++;
                }
                else
                {
                    Debug.LogWarning($"Favorite Novel with ID {id} not found in allKiteNovels. Skipping.");
                }
            }
            
            // Adjust the height of the visual novel holder based on the number of novels
            SetVisualNovelHolderHeight(index);
        }

        /// <summary>
        /// Sets the height of the visual novel holder based on the number of visual novels displayed.
        /// </summary>
        /// <param name="index">The count of visual novels to determine the height of the holder.</param>
        private void SetVisualNovelHolderHeight(int index)
        {
            float height = index switch
            {
                12 => 1300,
                >= 10 => 1200,
                9 => 1050,
                >= 7 => 900,
                6 => 750,
                >= 4 => 600,
                3 => 450,
                _ => 300
            };

            visualNovelHolder.GetComponent<LayoutElement>().preferredHeight = height;
        }

        /// <summary>
        /// Returns the local position of a UI element based on its index.
        /// </summary>
        /// <param name="index">The index for which the local position is to be calculated.</param>
        /// <returns>A <see cref="Vector3"/> representing the local position corresponding to the specified index.</returns>
        private static Vector3 GetLocalPositionByIndex(int index)
        {
            return index switch
            {
                0 => new Vector3(-250f, 0f, 0f),
                1 => new Vector3(250f, 0f, 0f),
                2 => new Vector3(0f, -144.5f, 0f),
                3 => new Vector3(-250f, -290f, 0f),
                4 => new Vector3(250f, -290f, 0f),
                5 => new Vector3(0f, -433.5f, 0f),
                6 => new Vector3(-250f, -578f, 0f),
                7 => new Vector3(250f, -578f, 0f),
                8 => new Vector3(0f, -722.5f, 0f),
                9 => new Vector3(-250f, -868f, 0f),
                10 => new Vector3(250f, -868f, 0f),
                11 => new Vector3(0f, -1011.5f, 0f),
                _ => Vector3.zero
            };
        }

        /// <summary>
        /// Retrieves the GameObject representing the novel button associated with the specified novel ID.
        /// </summary>
        /// <param name="id">The unique identifier of the visual novel to locate the corresponding button.</param>
        /// <returns>The GameObject of the novel button if found; otherwise, null.</returns>
        private GameObject GetNovelById(long id)
        {
            VisualNovelNames visualNovelName = VisualNovelNamesHelper.ValueOf((int)id);
            return _novelButtons.TryGetValue(visualNovelName, out var button) ? button.gameObject : null;
        }

        /// <summary>
        /// Handles the process of loading and preparing the selected visual novel for play.
        /// </summary>
        /// <param name="visualNovelName">The name of the visual novel to be loaded and played.</param>
        private void OnNovelButton(VisualNovelNames visualNovelName)
        {
            VisualNovel visualNovelToDisplay = null;
            List<VisualNovel> allNovels = KiteNovelManager.Instance().GetAllKiteNovels();

            foreach (VisualNovel novel in allNovels)
            {
                if (novel.id == VisualNovelNamesHelper.ToInt(visualNovelName))
                {
                    visualNovelToDisplay = novel;
                    break;
                }
            }

            if (visualNovelToDisplay == null)
            {
                DisplayErrorMessage("Die gewünschte Novel konnte nicht geladen werden.");
                return;
            }

            Color novelColor = visualNovelToDisplay.novelColor;
            NovelColorManager.Instance().SetColor(novelColor);

            PlayManager.Instance().SetVisualNovelToPlay(visualNovelToDisplay);
            PlayManager.Instance().SetColorOfVisualNovelToPlay(novelColor);
            PlayManager.Instance().SetDisplayNameOfNovelToPlay(FoundersBubbleMetaInformation.GetDisplayNameOfNovelToPlay(visualNovelName));
            PlayManager.Instance().SetDesignationOfNovelToPlay(visualNovelToDisplay.designation);
            GameObject buttonSound = Instantiate(selectNovelSoundPrefab);
            DontDestroyOnLoad(buttonSound);

            if (ShowPlayInstructionManager.Instance().ShowInstruction())
            {
                SceneLoader.LoadPlayInstructionScene();
            }
            else
            {
                SceneLoader.LoadPlayNovelScene();
            }
        }
    }
}