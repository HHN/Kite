using System;
using UnityEngine;

[Serializable]
public class DialogHistoryEntry
{
    [SerializeField] private long novelId;
    [SerializeField] private string dialog;
    [SerializeField] private string completion;

    public long GetNovelId() 
    { 
        return novelId; 
    }

    public void SetNovelId(long novelId)
    {
        this.novelId = novelId;
    }

    public string GetDialog() 
    { 
        return dialog; 
    }

    public void SetDialog(string dialog)
    {
        this.dialog = dialog;
    }

    public string GetCompletion() 
    { 
        return completion; 
    }

    public void SetCompletion(string completion)
    {
        this .completion = completion;
    }
}
