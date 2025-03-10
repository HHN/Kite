using System.Collections.Generic;
using Assets._Scripts.Player;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    public class DialogHistoryManager
    {
        private static DialogHistoryManager _instance;
        [SerializeField] private DialogHistoryEntryList _entries;
        private const string Key = "DialogueHistoryEntries";


        private DialogHistoryManager()
        {
            _entries = LoadEntries();
        }

        public static DialogHistoryManager Instance()
        {
            if (_instance == null)
            {
                _instance = new DialogHistoryManager();
            }

            return _instance;
        }

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

        public void AddEntry(DialogHistoryEntry entry)
        {
            string dialog = entry.GetDialog().Replace("Lea", "Du");
            entry.SetDialog(dialog);
            _entries.entries.Add(entry);
            Save();
        }

        public List<DialogHistoryEntry> GetEntries()
        {
            return _entries.entries;
        }

        private void Save()
        {
            string json = JsonUtility.ToJson(_entries);
            PlayerDataManager.Instance().SavePlayerData(Key, json);
        }

        public void ClearList()
        {
            _entries = new DialogHistoryEntryList() { entries = new List<DialogHistoryEntry>() };
        }
    }

    [System.Serializable]
    public class DialogHistoryEntryList
    {
        public List<DialogHistoryEntry> entries;
    }
}