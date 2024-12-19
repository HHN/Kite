using System;
using System.Collections.Generic;

namespace _00_Kite2.Common.Novel
{
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
        public Dictionary<string, string> GlobalVariables;
        public string playedPath;

        public void AddGlobalVariable(string name, string value)
        {
            if (GlobalVariables == null)
            {
                GlobalVariables = new Dictionary<string, string>();
            }

            GlobalVariables.Add(name, value);
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
            GlobalVariables[varName] = value;
        }

        public bool IsVariableExistent(string name)
        {
            if (GlobalVariables == null)
            {
                return false;
            }

            return GlobalVariables.ContainsKey(name);
        }

        public void RemoveGlobalVariable(string name)
        {
            if (GlobalVariables == null)
            {
                return;
            }

            if (GlobalVariables.ContainsKey(name))
            {
                GlobalVariables.Remove(name);
            }
        }

        public string GetGlobalVariable(string name)
        {
            if (GlobalVariables == null || !GlobalVariables.ContainsKey(name))
            {
                return string.Empty;
            }

            return GlobalVariables[name];
        }

        public Dictionary<string, string> GetGlobalVariables()
        {
            if (GlobalVariables == null)
            {
                GlobalVariables = new Dictionary<string, string>();
            }

            return GlobalVariables;
        }

        public void ClearGlobalVariables()
        {
            GlobalVariables = new Dictionary<string, string>();
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

            if (GlobalVariables != null)
            {
                newNovel.GlobalVariables = new Dictionary<string, string>(GlobalVariables);
            }

            return newNovel;
        }
    }
}