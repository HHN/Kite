using System.Collections.Generic;
using _00_Kite2.Player;
using UnityEngine;

public class DialogHistoryManager
{
    private static DialogHistoryManager instance;
    private DialogHistoryEntryList entries;
    private const string KEY = "DialogueHistoryEntries";


    private DialogHistoryManager()
    {
        entries = LoadEntries();
    }

    public static DialogHistoryManager Instance()
    {
        if (instance == null)
        {
            instance = new DialogHistoryManager();
        }
        return instance;
    }

    public DialogHistoryEntryList LoadEntries()
    {
        if (PlayerDataManager.Instance().HasKey(KEY))
        {
            string json = PlayerDataManager.Instance().GetPlayerData(KEY);
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
        entries.entries.Add(entry); 
        Save();
    }

    public List<DialogHistoryEntry> GetEntries()
    {
        return entries.entries;
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(entries);
        PlayerDataManager.Instance().SavePlayerData(KEY, json);
    }

    public void ClearList()
    {
        entries = new DialogHistoryEntryList();
    }
}

[System.Serializable]
public class DialogHistoryEntryList
{
    public List<DialogHistoryEntry> entries;
}