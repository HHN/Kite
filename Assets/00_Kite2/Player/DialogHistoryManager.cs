using System.Collections.Generic;
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
        if (PlayerPrefs.HasKey(KEY))
        {
            string json = PlayerPrefs.GetString(KEY);
            return JsonUtility.FromJson<DialogHistoryEntryList>(json);
        }
        else
        {
            return new DialogHistoryEntryList() { entries = new List<DialogHistoryEntry>() };
        }
    }

    public void AddEntry(DialogHistoryEntry entry) 
    { 
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
        PlayerPrefs.SetString(KEY, json);
        PlayerPrefs.Save();
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