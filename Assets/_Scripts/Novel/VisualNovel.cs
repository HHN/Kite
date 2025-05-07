using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.Novel
{
    [Serializable]
    public class VisualNovel
    {
        public long id;
        public string folderName;
        public string title;
        public string description;
        public string feedback;
        public string context;
        public List<VisualNovelEvent> novelEvents;
        public Dictionary<string, string> GlobalVariables;
        public string playedPath;
        public List<string> characters;
        public bool isKiteNovel;
        public Color novelColor;

        public void AddGlobalVariable(string name, string value)
        {
            GlobalVariables ??= new Dictionary<string, string>();

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
            return GlobalVariables != null && GlobalVariables.ContainsKey(name);
        }

        public void RemoveGlobalVariable(string name)
        {
            if (GlobalVariables == null)
            {
                return;
            }

            GlobalVariables.Remove(name);
        }

        public string GetGlobalVariable(string name)
        {
            if (GlobalVariables == null || !GlobalVariables.TryGetValue(name, out var variable))
            {
                return string.Empty;
            }

            return variable;
        }

        public Dictionary<string, string> GetGlobalVariables()
        {
            return GlobalVariables ??= new Dictionary<string, string>();
        }

        public void ClearGlobalVariables()
        {
            GlobalVariables = new Dictionary<string, string>();
        }

        public VisualNovel DeepCopy()
        {
            VisualNovel newNovel = new VisualNovel
            {
                id = id,
                folderName = folderName,
                title = title,
                description = description,
                feedback = feedback,
                context = context,
                playedPath = playedPath,
                isKiteNovel = isKiteNovel
            };

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