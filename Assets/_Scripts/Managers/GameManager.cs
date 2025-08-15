using System;
using System.Collections.Generic;
using System.Linq;
using Assets._Scripts.Controller.CharacterController;
using Assets._Scripts.Novel;
using Assets._Scripts.SaveNovelData;
using Assets._Scripts.UIElements.Messages;
using Assets._Scripts.Utilities;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    /// <summary>
    /// Represents the save status of a novel. This class holds information about
    /// whether a specific novel identified by its unique ID has been saved or not.
    /// </summary>
    [Serializable]
    public class NovelSaveStatus
    {
        public string novelId; // Unique identifier for the novel
        public bool isSaved; // Whether the novel has been saved
    }

    /// <summary>
    /// Represents data associated with a character, including customization indices
    /// for various attributes like skin, glasses, clothes, hair, and hand sprites.
    /// Allows for two sets of attributes, providing flexibility for character customization.
    /// </summary>
    [Serializable]
    public class CharacterData
    {
        // Indices for character customization (set 1 and set 2)
        public int skinIndex;
        public int glassIndex;
        public HandSpriteIndex handIndex;
        public int clotheIndex;
        public int hairIndex;
        public int characterId;

        public int skinIndex2;
        public int glassIndex2;
        public HandSpriteIndex handIndex2;
        public int clotheIndex2;
        public int hairIndex2;
        public int characterId2;
    }

    /// <summary>
    /// Represents an entry that associates a unique identifier with character data.
    /// This is used to map specific character data configurations to an ID,
    /// facilitating lookups and updates.
    /// </summary>
    [Serializable]
    public class CharacterDataEntry
    {
        public long id;
        public CharacterData data;
    }

    /// <summary>
    /// Manages the game state and provides a centralized system for handling various
    /// game-related functionalities, including save statuses, novel progress, and UI elements.
    /// Implements a singleton pattern for easy global access.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private bool showAllNovels;
        [SerializeField] private bool skipIntroNovel;
        [SerializeField] private bool isIntroNovelSaved;
        [SerializeField] private bool introNovelLoadedFromMainMenu = true;

        [SerializeField] private List<NovelSaveStatus> novelSaveStatusList = new();

        [SerializeField] private List<CharacterDataEntry> characterDataList = new();

        [SerializeField] private GameObject messageBox;

        public bool calledFromReload = true;
        public bool resetApp;
        public GameObject canvas;

        private readonly Dictionary<string, bool> _novelSaveStatus = new();
        public static GameManager Instance { get; private set; }

        private Dictionary<long, CharacterData> _characterDataDictionary = new();
        
        public List<NovelSaveStatus> NovelSaveStatusList => novelSaveStatusList;
        
        private MessageBox _messageObject;
        
        // Property to get or set the showAllNovels flag
        public bool ShowAllNovels
        {
            get => showAllNovels;
            set => showAllNovels = value;
        }

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

        /// <summary>
        /// Initializes the GameManager instance and ensures it follows the singleton pattern,
        /// preventing duplicate instances. Configures the GameManager object to persist between
        /// scene loads, converts the character data list into a dictionary for efficient access,
        /// and verifies the save status for all novels upon startup.
        /// </summary>
        private void Awake()
        {
            if (Instance && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            _characterDataDictionary = characterDataList.ToDictionary(entry => entry.id, entry => entry.data);

            CheckAndSetAllNovelsStatus();
        }

        /// <summary>
        /// Updates the status of all visual novels by iterating through all available novel identifiers
        /// and checking if a save file exists for each. Clears and rebuilds the internal dictionary and
        /// serialized list used for debugging and status tracking, ensuring they reflect the latest save data.
        /// Additionally, handles character data synchronization if a save file is found.
        /// </summary>
        public void CheckAndSetAllNovelsStatus()
        {
            _novelSaveStatus.Clear();
            novelSaveStatusList.Clear();
            _characterDataDictionary.Clear();
            characterDataList.Clear();

            foreach (VisualNovelNames novelName in Enum.GetValues(typeof(VisualNovelNames)))
            {
                string novelId = VisualNovelNamesHelper.ToInt(novelName).ToString();

                var saveData = SaveLoadManager.Load(novelId);
                bool isSaved = saveData != null;
                _novelSaveStatus[novelId] = isSaved;

                novelSaveStatusList.Add(new NovelSaveStatus { novelId = novelId, isSaved = isSaved });
                
                if (isSaved && saveData.CharacterPrefabData != null)
                {
                    foreach (var kvp in saveData.CharacterPrefabData)
                    {
                        _characterDataDictionary[kvp.Key] = kvp.Value;

                        characterDataList.Add(new CharacterDataEntry
                        {
                            id = kvp.Key,
                            data = kvp.Value
                        });
                    }
                }
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
            if (_novelSaveStatus.ContainsKey(novelId))
            {
                _novelSaveStatus[novelId] = isSaved;
            }
        }

        /// <summary>
        /// Adds or updates character data in the GameManager. The method ensures the
        /// character data is stored in both the internal dictionary and the list of character data entries.
        /// If an entry with the specified ID already exists, it updates the existing entry. Otherwise,
        /// a new entry is created and added to the list.
        /// </summary>
        /// <param name="id">The unique identifier for the character data to be added or updated.</param>
        /// <param name="data">The character data to be stored or updated in the GameManager.</param>
        public void AddCharacterData(long id, CharacterData data)
        {
            _characterDataDictionary[id] = data;

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

        /// <summary>
        /// Retrieves the dictionary containing character data associated with unique identifiers.
        /// Provides access to manage, retrieve, and manipulate character information
        /// stored within the game manager.
        /// </summary>
        /// <returns>
        /// A dictionary with unique identifiers as keys and corresponding
        /// <see cref="CharacterData"/> objects as values.
        /// </returns>
        public Dictionary<long, CharacterData> GetCharacterDataDictionary()
        {
            return _characterDataDictionary;
        }

        /// <summary>
        /// Displays a message in a message box UI element. If a previous message box exists, it is closed
        /// before displaying the new message. This method ensures the message box is instantiated,
        /// configured, and activated on the assigned canvas.
        /// </summary>
        /// <param name="message">The message body text to be displayed.</param>
        public void DisplayMessage(string message)
        {
            if (!resetApp)
            {
                return;
            }
            
            if (!_messageObject.IsNullOrDestroyed())
            {
                _messageObject.CloseMessageBox();
            }

            if (canvas.IsNullOrDestroyed())
            {
                return;
            }

            _messageObject = null;
            _messageObject = Instantiate(messageBox, canvas.transform).GetComponent<MessageBox>();
            _messageObject.SetHeadline("INFORMATION");
            _messageObject.SetBody(message);
            _messageObject.SetIsErrorMessage(false);
            _messageObject.Activate();

            resetApp = false;
        }
    }
}