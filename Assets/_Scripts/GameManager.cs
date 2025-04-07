using System;
using System.Collections.Generic;
using System.Linq;
using Assets._Scripts.Controller.CharacterController;
using Assets._Scripts.Player;
using Assets._Scripts.SaveNovelData;
using UnityEngine;

namespace Assets._Scripts
{
    [Serializable]
    public class NovelSaveStatus
    {
        public string novelId; // Unique identifier for the novel
        public bool isSaved; // Whether the novel has been saved
    }

    [Serializable]
    public class CharacterData
    {
        // Indices for character customization (set 1 and set 2)
        public int skinIndex;
        public int glassIndex;
        public HandSpriteIndex handIndex;
        public int clotheIndex;
        public int hairIndex;

        public int skinIndex2;
        public int glassIndex2;
        public HandSpriteIndex handIndex2;
        public int clotheIndex2;
        public int hairIndex2;
    }

    [Serializable]
    public class CharacterDataEntry
    {
        public long id;
        public CharacterData data;
    }

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private bool skipIntroNovel; // Whether to skip the introduction novel
        [SerializeField] private bool isIntroNovelSaved; // Tracks if the intro novel is saved

        [SerializeField]
        private bool introNovelLoadedFromMainMenu = true; // Whether the intro novel was loaded from the main menu

        // List to display in the Inspector (only for debugging, not used directly)
        [SerializeField] private List<NovelSaveStatus> novelSaveStatusList = new();

        // Static dictionary to store character data globally
        [SerializeField] private List<CharacterDataEntry> characterDataList = new();

        public bool calledFromReload = true; // Flag to check if the scene is being reloaded

        // Dictionary to dynamically manage the save status of each novel
        private readonly Dictionary<string, bool> _novelSaveStatus = new();
        public static GameManager Instance { get; private set; }

        private Dictionary<long, CharacterData> _characterDataDictionary = new();
        
        public List<NovelSaveStatus> NovelSaveStatusList => novelSaveStatusList;

        // Property to get or set the skipIntroNovel flag
        public bool SkipIntroNovel
        {
            get => skipIntroNovel;
            set => skipIntroNovel = value;
        }

        // Property to get or set the introNovelLoadedFromMainMenu flag
        public bool IsIntroNovelLoadedFromMainMenu
        {
            get => introNovelLoadedFromMainMenu;
            set => introNovelLoadedFromMainMenu = value;
        }

        private void Awake()
        {
            // Singleton pattern to ensure a single instance of GameManager exists
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject); // Destroy duplicate GameManager instances
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensure this object persists across scene changes

            // Liste in Dictionary umwandeln
            _characterDataDictionary = characterDataList.ToDictionary(entry => entry.id, entry => entry.data);

            // Check and set the save status for all novels at startup
            CheckAndSetAllNovelsStatus();
        }

        /// <summary>
        /// Checks and sets the save status for all novels.
        /// </summary>
        public void CheckAndSetAllNovelsStatus()
        {
            _novelSaveStatus.Clear(); // Clear the current dictionary
            novelSaveStatusList.Clear(); // Clear the Inspector list

            // Iterate through all VisualNovelNames (enums) and check their save status
            foreach (VisualNovelNames novelName in Enum.GetValues(typeof(VisualNovelNames)))
            {
                string novelId = VisualNovelNamesHelper.ToInt(novelName).ToString();

                // Check if a save exists for the novel
                bool isSaved = SaveLoadManager.Load(novelId) != null;
                _novelSaveStatus[novelId] = isSaved;

                // Add the save status to the Inspector list for debugging
                novelSaveStatusList.Add(new NovelSaveStatus { novelId = novelId, isSaved = isSaved });
            }
        }

        /// <summary>
        /// Checks whether saved progress exists for a specific novel.
        /// </summary>
        /// <param name="novelId">The unique ID of the novel.</param>
        /// <returns>Returns <c>true</c> if a save exists; otherwise, <c>false</c>.</returns>
        public bool HasSavedProgress(string novelId)
        {
            // Here it checks if the save status for the novel exists in the dictionary and is `true`
            return _novelSaveStatus.TryGetValue(novelId, out var isSaved) && isSaved;
        }

        /// <summary>
        /// Updates the save status of a novel, e.g., after a save operation.
        /// </summary>
        /// <param name="novelId">The unique ID of the novel.</param>
        /// <param name="isSaved">The new save status of the novel.</param>
        public void UpdateNovelSaveStatus(string novelId, bool isSaved)
        {
            // Update the dictionary if the novel ID exists
            if (_novelSaveStatus.ContainsKey(novelId))
            {
                _novelSaveStatus[novelId] = isSaved;
            }
        }

        public void AddCharacterData(long id, CharacterData data)
        {
            _characterDataDictionary[id] = data;

            // Falls ID schon existiert, aktualisieren
            var existingEntry = characterDataList.FirstOrDefault(entry => entry.id == id);
            if (existingEntry != null)
            {
                existingEntry.data = data;
            }
            else
            {
                characterDataList.Add(new CharacterDataEntry { id = id, data = data });
            }
        }

        public Dictionary<long, CharacterData> GetCharacterDataDictionary()
        {
            return _characterDataDictionary;
        }
    }
}