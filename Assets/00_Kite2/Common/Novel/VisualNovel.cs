using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class VisualNovel
{
    public long id;
    public string folderName;
    public string title;
    public string description;
    public long image;
    public string feedback;
    public string context;
    public bool isKite2Novel;
    public List<VisualNovelEvent> novelEvents;
    public Dictionary<string, string> globalVariables;
    public List<VisualNovelEvent> listOfPlayedEvents;

    public void AddGlobalVariable(string name, string value)
    {
        if (globalVariables == null) 
        {  
            globalVariables = new Dictionary<string, string>(); 
        }
        globalVariables.Add(name, value);
    }
    
    public void AddPlayedEvent(VisualNovelEvent visualNovelEvent)
    {
        if (listOfPlayedEvents == null)
        {
            listOfPlayedEvents = new List<VisualNovelEvent>();
        }
        listOfPlayedEvents.Add(visualNovelEvent);
    }

    public string GetBiasCombination()
    {
        string biasCombination = "";

        foreach (VisualNovelEvent playedEvent in listOfPlayedEvents)
        {
            if (playedEvent.eventType == VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.MARK_BIAS_EVENT))
            {
                biasCombination = biasCombination + "<|Seperator|>" + playedEvent.id;
            }
        }
        return biasCombination;
    }

    public List<VisualNovelEvent> GetPlayedEvents()
    {
        if (listOfPlayedEvents == null)
        {
            listOfPlayedEvents = new List<VisualNovelEvent>();
        }
        return listOfPlayedEvents;
    }

    public void SetGlobalVariable(string varName, string value)
    {
        globalVariables[varName] = value;
    }

    public bool IsVariableExistend(string name)
    {
        if (globalVariables == null)
        {
            Debug.Log("globalVariables == null");
            return false;
        }
        Debug.Log(globalVariables.ContainsKey(name));
        return globalVariables.ContainsKey(name);
    }

    public void RemoveGlobalVariable(string name)
    {
        if (globalVariables == null)
        {
            return;
        }
        if (globalVariables.ContainsKey(name))
        {
            Debug.Log("Removed var");
            globalVariables.Remove(name);
        }
    }

    public string GetGlobalVariable(string name)
    {
        if (globalVariables == null || !globalVariables.ContainsKey(name))
        {
            return string.Empty;
        }
        return globalVariables[name];
    }

    public Dictionary<string, string> GetGlobalVariables()
    {
        if (globalVariables == null)
        {
            globalVariables = new Dictionary<string, string>();
        }
        return globalVariables;
    }

    public void ClearGlobalVariables()
    {
        globalVariables = new Dictionary<string, string>();
    }

    public void ClearPlayedEvents()
    {
        listOfPlayedEvents = new List<VisualNovelEvent>();
    }

    public VisualNovel DeepCopy()
    {
        VisualNovel newNovel = new VisualNovel();

        newNovel.id = id;
        newNovel.folderName = folderName;
        newNovel.title = title;
        newNovel.description = description;
        newNovel.image = image;
        newNovel.feedback = feedback;
        newNovel.context = context;
        newNovel.isKite2Novel = isKite2Novel;

        if (novelEvents != null)
        {
            newNovel.novelEvents = new List<VisualNovelEvent>();
            foreach (VisualNovelEvent eventItem in novelEvents)
            {
                newNovel.novelEvents.Add(eventItem.DeepCopy()); // Stelle sicher, dass VisualNovelEvent eine DeepCopy-Methode hat
            }
        }

        if (globalVariables != null)
        {
            newNovel.globalVariables = new Dictionary<string, string>(globalVariables);
        }

        return newNovel;
    }
}
