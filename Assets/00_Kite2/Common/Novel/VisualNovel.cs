using System;
using System.Collections.Generic;

[Serializable]
public class VisualNovel
{
    public long id;
    public string folderName;
    public string title;
    public string description;
    public long image;
    public string nameOfMainCharacter;
    public string feedback;
    public string context;
    public bool isKite2Novel;
    public List<VisualNovelEvent> novelEvents;
    public Dictionary<string, string> globalVariables;

    public void AddGlobalVariable(string name, string value)
    {
        if (globalVariables == null) 
        {  
            globalVariables = new Dictionary<string, string>(); 
        }
        globalVariables[name] = value;
    }

    public bool IsVariableExistend(string name)
    {
        if (globalVariables == null)
        {
            return false;
        }
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
}
