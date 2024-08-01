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
    public string playedPath;

    public void AddGlobalVariable(string name, string value)
    {
        if (globalVariables == null) 
        {  
            globalVariables = new Dictionary<string, string>(); 
        }
        globalVariables.Add(name, value);
    }

    public void AddToPath(int pathValue)
    {
        if (string.IsNullOrEmpty(playedPath))
        {
            playedPath = pathValue.ToString();

        }
        else
        {
            playedPath = playedPath + ":" + pathValue;
        }
    }
    
    public string GetPlayedPath()
    {
        return playedPath;
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
        newNovel.playedPath = playedPath;

        if (novelEvents != null)
        {
            newNovel.novelEvents = new List<VisualNovelEvent>();
            foreach (VisualNovelEvent eventItem in novelEvents)
            {
                newNovel.novelEvents.Add(eventItem.DeepCopy());
            }
        }

        if (globalVariables != null)
        {
            newNovel.globalVariables = new Dictionary<string, string>(globalVariables);
        }

        return newNovel;
    }
}
