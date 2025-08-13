using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.Novel
{
    /// <summary>
    /// Represents a data model for a visual novel, holding its properties, events,
    /// global variables, and managing the player's path through the novel.
    /// This class is marked <see cref="Serializable"/> to allow for serialization
    /// (e.g., saving and loading novel data).
    /// </summary>
    [Serializable]
    public class VisualNovel
    {
        public long id;
        public string folderName;
        public string title;
        public string designation;
        public string description;
        public string feedback;
        public string context;
        public List<VisualNovelEvent> novelEvents;
        public Dictionary<string, string> GlobalVariables;
        public string playedPath;
        public List<string> characters;
        public bool isKiteNovel;
        public Color novelColor;
        public Color novelFrameColor;

        /// <summary>
        /// Adds a new global variable to the <see cref="GlobalVariables"/> dictionary.
        /// If the dictionary is null, it will be initialized.
        /// </summary>
        /// <param name="name">The name (key) of the global variable.</param>
        /// <param name="value">The string value of the global variable.</param>
        public void AddGlobalVariable(string name, string value)
        {
            GlobalVariables ??= new Dictionary<string, string>();

            GlobalVariables.Add(name, value);
        }

        /// <summary>
        /// Appends a new path value to the <see cref="playedPath"/> string.
        /// If <see cref="playedPath"/> is empty, the value is set directly; otherwise, it's appended with a colon separator.
        /// </summary>
        /// <param name="pathValue">The integer value representing a step or choice in the novel's path.</param>
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

        /// <summary>
        /// Retrieves the current played path string.
        /// </summary>
        /// <returns>A string representing the sequence of choices made by the player.</returns>
        public string GetPlayedPath()
        {
            return playedPath;
        }

        /// <summary>
        /// Sets or updates the value of an existing global variable.
        /// If the variable does not exist, it will be added.
        /// </summary>
        /// <param name="varName">The name of the global variable to set.</param>
        /// <param name="value">The new string value for the variable.</param>
        public void SetGlobalVariable(string varName, string value)
        {
            GlobalVariables[varName] = value;
        }

        /// <summary>
        /// Checks if a global variable with the specified name exists.
        /// </summary>
        /// <param name="name">The name of the variable to check.</param>
        /// <returns>True if the variable exists and <see cref="GlobalVariables"/> is not null, otherwise false.</returns>
        public bool IsVariableExistent(string name)
        {
            return GlobalVariables != null && GlobalVariables.ContainsKey(name);
        }

        /// <summary>
        /// Removes a global variable with the specified name from the <see cref="GlobalVariables"/> dictionary.
        /// Does nothing if the dictionary is null.
        /// </summary>
        /// <param name="name">The name of the variable to remove.</param>
        public void RemoveGlobalVariable(string name)
        {
            if (GlobalVariables == null)
            {
                return;
            }

            GlobalVariables.Remove(name);
        }

        /// <summary>
        /// Retrieves the value of a global variable by its name.
        /// </summary>
        /// <param name="name">The name of the global variable.</param>
        /// <returns>The string value of the variable if found; otherwise, an empty string.</returns>
        public string GetGlobalVariable(string name)
        {
            if (GlobalVariables == null || !GlobalVariables.TryGetValue(name, out var variable))
            {
                return string.Empty;
            }

            return variable;
        }

        /// <summary>
        /// Retrieves the dictionary of all global variables.
        /// If the dictionary is null, it will be initialized and returned.
        /// </summary>
        /// <returns>The <see cref="GlobalVariables"/> dictionary.</returns>
        public Dictionary<string, string> GetGlobalVariables()
        {
            return GlobalVariables ??= new Dictionary<string, string>();
        }

        /// <summary>
        /// Clears all global variables by creating a new empty dictionary.
        /// </summary>
        public void ClearGlobalVariables()
        {
            GlobalVariables = new Dictionary<string, string>();
        }

        /// <summary>
        /// Creates a deep copy of the current <see cref="VisualNovel"/> instance.
        /// This includes copying primitive types directly, and creating new lists/dictionaries
        /// for reference types like <see cref="novelEvents"/> and <see cref="GlobalVariables"/>.
        /// Each <see cref="VisualNovelEvent"/> within the list is also deep-copied.
        /// </summary>
        /// <returns>A new <see cref="VisualNovel"/> instance that is a deep copy of the original.</returns>
        public VisualNovel DeepCopy()
        {
            VisualNovel newNovel = new VisualNovel
            {
                id = id,
                folderName = folderName,
                title = title,
                designation = designation,
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