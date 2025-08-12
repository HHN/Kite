using System;
using System.Collections.Generic;
using Assets._Scripts.Managers;
using Assets._Scripts.Novel;
using UnityEngine;

namespace Assets._Scripts.SaveNovelData
{
    /// <summary>
    /// Represents the complete save data for a visual novel, encapsulating all necessary states
    /// to resume gameplay from a specific point. This includes narrative progression,
    /// displayed content, character states, and choices made.
    /// Marked as <see cref="Serializable"/> to allow Unity's JsonUtility (or other serializers) to convert it to JSON.
    /// </summary>
    [Serializable]
    public class NovelSaveData
    {
        public string currentEventId;
        public List<string> playThroughHistory;
        public string[] optionsId;
        public List<VisualNovelEvent> eventHistory;
        public List<VisualNovelEvent> visualNovelEvents;
        public List<VisualNovelEvent> content;
        public List<string> messageType;
        public int optionCount;
        public Dictionary<int, int> CharacterExpressions = new();
        public Dictionary<long, CharacterData> CharacterPrefabData;

        /// <summary>
        /// Converts this instance of <see cref="NovelSaveData"/> into a JSON string.
        /// </summary>
        /// <returns>A JSON-formatted string representing this instance.</returns>
        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }

        /// <summary>
        /// Creates a new <see cref="NovelSaveData"/> instance from a given JSON string.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>A new instance of <see cref="NovelSaveData"/> populated with data from the JSON string.</returns>
        public static NovelSaveData FromJson(string json)
        {
            return JsonUtility.FromJson<NovelSaveData>(json);
        }
    }
}