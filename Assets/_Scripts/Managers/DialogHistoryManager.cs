using System.Collections.Generic;
using Assets._Scripts.Player;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    /// <summary>
    /// Represents a collection of dialog history entries, enabling the grouping and management
    /// of individual dialog interactions stored as a list.
    /// </summary>
    [System.Serializable]
    public class DialogHistoryEntryList
    {
        public List<DialogHistoryEntry> entries;
    }
    
    /// <summary>
    /// Manages the history of dialog interactions within the application, providing functionality
    /// to add, retrieve, or clear dialog history entries while ensuring proper data persistence.
    /// </summary>
    public class DialogHistoryManager
    {
        private static DialogHistoryManager _instance;
        [SerializeField] private DialogHistoryEntryList _entries;
        private const string Key = "DialogueHistoryEntries";


        /// <summary>
        /// Manages the history of dialog interactions. This includes adding entries,
        /// retrieving the list of dialog entries, saving, loading, and clearing dialog history.
        /// Provides data persistence through PlayerDataManager to ensure continuity across sessions.
        /// </summary>
        private DialogHistoryManager()
        {
            _entries = LoadEntries();
        }

        /// <summary>
        /// Provides access to the single instance of the DialogHistoryManager.
        /// Ensures that only one instance of the manager exists throughout the application lifecycle,
        /// enabling centralized management and retrieval of dialog history data.
        /// </summary>
        /// <returns>The singleton instance of DialogHistoryManager.</returns>
        public static DialogHistoryManager Instance()
        {
            if (_instance == null)
            {
                _instance = new DialogHistoryManager();
            }

            return _instance;
        }

        /// <summary>
        /// Loads the dialog history entries data from persistent storage. If no existing data is found,
        /// a new empty dialog history entry list is created and returned. Ensures entries are properly
        /// deserialized into a usable format.
        /// </summary>
        /// <returns>A populated instance of DialogHistoryEntryList containing deserialized dialog entries
        /// or a new instance with an empty list if no data is found in storage.</returns>
        private DialogHistoryEntryList LoadEntries()
        {
            if (PlayerDataManager.Instance().HasKey(Key))
            {
                string json = PlayerDataManager.Instance().GetPlayerData(Key);
                return JsonUtility.FromJson<DialogHistoryEntryList>(json);
            }
            else
            {
                return new DialogHistoryEntryList() { entries = new List<DialogHistoryEntry>() };
            }
        }

        /// <summary>
        /// Adds a new dialog history entry to the internal list. Adjusts the dialog text if needed and saves the updated history.
        /// </summary>
        /// <param name="entry">The dialog history entry to be added to the list.</param>
        public void AddEntry(DialogHistoryEntry entry)
        {
            string dialog = entry.GetDialog().Replace("Lea", "Du"); // ToDo: Beobachten, möglicherweise nicht mehr nötig
            entry.SetDialog(dialog);
            _entries.entries.Add(entry);
            Save();
        }

        /// <summary>
        /// Retrieves the list of dialog history entries stored in the manager.
        /// </summary>
        /// <returns>A list of dialog history entries.</returns>
        public List<DialogHistoryEntry> GetEntries()
        {
            return _entries.entries;
        }

        /// <summary>
        /// Saves the current state of dialog history entries to persistent storage.
        /// Serializes the internal dialog history entries list into a JSON format
        /// and uses the PlayerDataManager to store the serialized data under
        /// a predefined key.
        /// Ensures that dialog history can be persisted across application sessions.
        /// </summary>
        private void Save()
        {
            string json = JsonUtility.ToJson(_entries);
            PlayerDataManager.Instance().SavePlayerData(Key, json);
        }

        /// <summary>
        /// Clears all entries from the dialog history, resetting it to a new empty list.
        /// This method is useful for removing all recorded dialog interactions and starting fresh.
        /// </summary>
        public void ClearList()
        {
            _entries = new DialogHistoryEntryList() { entries = new List<DialogHistoryEntry>() };
        }
    }
}