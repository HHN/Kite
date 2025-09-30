using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Assets._Scripts.Managers;
using Assets._Scripts.Novel;
using Assets._Scripts.Player;
using Assets._Scripts.SceneManagement;
using Assets._Scripts.UIElements.DropDown;
using Assets._Scripts.UIElements.FoundersBubble;
using Assets._Scripts.Utilities;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

namespace Assets._Scripts.Controller.SceneControllers
{
    /// <summary>
    /// Manages the novel history scene, handling functionalities
    /// specific to viewing and interacting with the history of novel content in the game.
    /// </summary>
    /// <remarks>
    /// Inherits functionality and properties from the SceneController class, allowing it to
    /// use common scene management capabilities such as message display and scene control.
    /// </remarks>
    public class NovelHistorySceneController : SceneController
    {
        [SerializeField] private GameObject containerPrefab;
        [SerializeField] private GameObject spacingPrefab;
        [SerializeField] private Transform containerParent;

        [SerializeField] private GameObject entryPrefab;

        [SerializeField] private GameObject container;
        [SerializeField] private GameObject noDataObjectsHint;

        [SerializeField] private bool displayNoDataObjectsHint;

        [SerializeField] private List<NovelHistoryEntryGuiElement> novelHistoryEntries;

        [SerializeField] private GameObject copyNotificationContainer;

        private readonly Dictionary<long, List<DialogHistoryEntry>> _novelHistoryEntriesDictionary = new();
        private readonly Dictionary<long, GameObject> _novelContainers = new();
        private Dictionary<long, DropDownMenu> _novelDropDownMenus = new();
        private Dictionary<DateTime, long> _dateAndTimeToNovelIdDictionary = new();

        private readonly CultureInfo _culture = new("de-DE");

        /// <summary>
        /// Initializes the novel history scene and its components when the scene is loaded.
        /// </summary>
        /// <remarks>
        /// This method is responsible for managing the lifecycle of the scene by performing the following tasks:
        /// - Adding the current scene to the BackStackManager for navigation management.
        /// - Initializing the copy notification system, if applicable.
        /// - Initializing the list of novel history entries.
        /// - Configuring the scene by invoking specific initialization methods.
        /// - Rebuilding the layout asynchronously to ensure proper UI setup.
        /// </remarks>
        /// <seealso cref="BackStackManager"/>
        /// <seealso cref="NovelHistoryEntryGuiElement"/>
        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.NovelHistoryScene);

            if (copyNotificationContainer != null) InitCopyNotification();

            novelHistoryEntries = new List<NovelHistoryEntryGuiElement>();
            InitializeScene();

            StartCoroutine(RebuildLayout());
        }

        /// <summary>
        /// Configures and initializes the copy notification system for the novel history scene.
        /// </summary>
        private void InitCopyNotification()
        {
            copyNotificationContainer.SetActive(false);
            GameObjectManager.Instance().SetCopyNotification(copyNotificationContainer);
        }

        /// <summary>
        /// Sets up the novel history scene by processing and displaying dialog history entries
        /// and managing relevant UI components.
        /// </summary>
        private void InitializeScene()
        {
            List<DialogHistoryEntry> entries = DialogHistoryManager.Instance().GetEntries();

            if (entries == null || entries.Count == 0)
            {
                noDataObjectsHint.SetActive(true);
                return;
            }

            noDataObjectsHint.SetActive(false);
            SortEntriesAndCreateDictionary(entries);
            AddNovelContainer(entries);
        }

        /// <summary>
        /// Adds novel containers and organizes dialog history entries based on their corresponding novels.
        /// </summary>
        /// <param name="entries">A list of dialog history entries to be processed and categorized into novel containers.</param>
        private void AddNovelContainer(List<DialogHistoryEntry> entries)
        {
            List<VisualNovel> allKiteNovels = KiteNovelManager.Instance().GetAllKiteNovels();
            Dictionary<long, VisualNovel> allKiteNovelsById = allKiteNovels.ToDictionary(novel => novel.id);

            foreach (DialogHistoryEntry entry in entries)
            {
                long novelId = entry.GetNovelId();

                if (allKiteNovelsById.TryGetValue(novelId, out VisualNovel foundNovel))
                {
                    string designation = foundNovel.designation;

                    if (!_novelHistoryEntriesDictionary.ContainsKey(novelId))
                    {
                        CreateNovelContainer(novelId);
                    }

                    _novelHistoryEntriesDictionary[novelId].Add(entry);
                    AddEntryToContainer(entry, _novelContainers[novelId], designation);
                }
                else
                {
                    LogManager.Warning($"Novel with ID {novelId} from history entry not found in available Kite Novels. Skipping this entry.");
                }
            }
        }

        /// <summary>
        /// Creates a novel container for a specified novel ID and initializes its associated UI elements and data structures.
        /// </summary>
        /// <param name="novelId">The unique identifier of the novel for which the container is created.</param>
        private void CreateNovelContainer(long novelId)
        {
            GameObject novelContainer = Instantiate(containerPrefab, containerParent);
            novelContainer.name = VisualNovelNamesHelper.GetName(novelId);

            _novelContainers[novelId] = novelContainer;
            _novelHistoryEntriesDictionary[novelId] = new List<DialogHistoryEntry>();

            Instantiate(spacingPrefab, containerParent);
        }

        /// <summary>
        /// Adds a dialog history entry to the specified novel container and sets up its visual representation.
        /// </summary>
        /// <param name="entry">The dialog history entry to be added.</param>
        /// <param name="novelContainer">The GameObject container associated with the novel.</param>
        /// <param name="designation">The designation or label text to be assigned to the entry's representation.</param>
        private void AddEntryToContainer(DialogHistoryEntry entry, GameObject novelContainer, string designation)
        {
            List<VisualNovel> allKiteNovels = KiteNovelManager.Instance().GetAllKiteNovels();
            VisualNovel novel = allKiteNovels.FirstOrDefault(n => n.id == entry.GetNovelId());

            GameObject reviewContainer = novelContainer.transform.Find("Review Container").gameObject;
            GameObject reviewButton = novelContainer.transform.Find("Review Button").gameObject;

            var visualNovel = VisualNovelNamesHelper.ValueOf((int)entry.GetNovelId());

            if (novel != null) reviewButton.GetComponent<Image>().color = novel.novelColor;
            reviewButton.GetComponentInChildren<TextMeshProUGUI>().text = designation;
            reviewButton.GetComponentInChildren<AlreadyPlayedUpdater>().VisualNovel = visualNovel;

            RectTransform containerTransform = container.GetComponent<RectTransform>();
            reviewButton.GetComponent<DropDownMenu>().AddLayoutToUpdateOnChange(containerTransform);

            var dataObjectGuiElement = Instantiate(entryPrefab, reviewContainer.transform).GetComponent<NovelHistoryEntryGuiElement>();
            dataObjectGuiElement.InitializeEntry(entry);
            if (novel != null) dataObjectGuiElement.SetVisualNovelColor(novel.novelColor);

            dataObjectGuiElement.AddLayoutToUpdateOnChange(reviewContainer.GetComponent<RectTransform>());
            dataObjectGuiElement.AddLayoutToUpdateOnChange(novelContainer.GetComponent<RectTransform>());
            dataObjectGuiElement.AddLayoutToUpdateOnChange(containerTransform);

            FontSizeManager.Instance().UpdateAllTextComponents();
        }

        /// <summary>
        /// Rebuilds the layout for all dropdown menus associated with the novel history scene.
        /// </summary>
        /// <returns>
        /// An enumerator representing the asynchronous operation for layout reconstruction.
        /// </returns>
        private IEnumerator RebuildLayout()
        {
            foreach (KeyValuePair<long, DropDownMenu> novelDropDownMenu in _novelDropDownMenus)
            {
                novelDropDownMenu.Value.RebuildLayout();
            }

            yield break;
        }

        /// <summary>
        /// Sorts the list of dialog history entries and creates a dictionary mapping their corresponding date and time to novel IDs.
        /// </summary>
        /// <param name="entries">The list of dialog history entries to sort and process. Each entry contains information about the novel's date, time, and identifier.</param>
        private void SortEntriesAndCreateDictionary(List<DialogHistoryEntry> entries)
        {
            // Create a list to store the sorting
            List<KeyValuePair<DateTime, long>> sortedEntries = new List<KeyValuePair<DateTime, long>>();
            List<KeyValuePair<DialogHistoryEntry, DateTime>> sortedEntriesWithOriginal = new List<KeyValuePair<DialogHistoryEntry, DateTime>>();

            foreach (var entry in entries)
            {
                // Split HeadButtonText.text by " | " into parts
                string[] parts = entry.GetDateAndTime().Split('|');

                if (parts.Length < 3) continue; // Skip entry if the format is invalid

                string datePart = parts[1].Trim(); // "dd.MM.yyyy"
                string timePart = parts[2].Trim(); // "HH:mm"

                // Combine date and time and convert to DateTime
                if (DateTime.TryParseExact($"{datePart} {timePart}", "dd.MM.yyyy HH:mm", _culture, DateTimeStyles.None, out DateTime parsedDate))
                {
                    // Add date and novelId to the list (for dictionary)
                    sortedEntries.Add(new KeyValuePair<DateTime, long>(parsedDate, entry.GetNovelId()));

                    // Also add the original element and date to the list (for a sorted result)
                    sortedEntriesWithOriginal.Add(new KeyValuePair<DialogHistoryEntry, DateTime>(entry, parsedDate));
                }
                else
                {
                    // If parsing fails, add an element with DateTime.MinValue
                    sortedEntriesWithOriginal.Add(new KeyValuePair<DialogHistoryEntry, DateTime>(entry, DateTime.MinValue));
                }
            }

            // Sort entries by date and time (newest first)
            sortedEntriesWithOriginal = sortedEntriesWithOriginal
                .OrderByDescending(entry => entry.Value) // Sort by DateTime in descending order
                .ThenBy(entry => entry.Key.GetNovelId()) // Then sort by novelId
                .ToList();

            // Create a dictionary for mapping DateTime to NovelId
            _dateAndTimeToNovelIdDictionary.Clear(); // Clear the dictionary if it already exists

            foreach (var entry in sortedEntries)
            {
                // Add value to the dictionary
                _dateAndTimeToNovelIdDictionary[entry.Key] = entry.Value;
            }

            // Return sorted DialogHistoryEntry elements in the correct order
            entries.Clear();
            entries.AddRange(sortedEntriesWithOriginal.Select(entry => entry.Key));
        }
    }
}