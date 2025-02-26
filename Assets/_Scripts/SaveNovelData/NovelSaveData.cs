using System;
using System.Collections.Generic;
using Assets._Scripts.Common.Novel;
using UnityEngine;

namespace Assets._Scripts.SaveNovelData
{
    [Serializable]
    public class NovelSaveData
    {
        public string currentEventId;
        public List<string> playThroughHistory; // Verlauf der gewählten Aktionen oder Dialoge
        public string[] optionsId;
        public List<VisualNovelEvent> eventHistory;
        public List<VisualNovelEvent> visualNovelEvents;
        public List<VisualNovelEvent> content;
        public List<string> messageType;
        public int optionCount;
        public Dictionary<int, int> CharacterExpressions = new();
        public Dictionary<long, CharacterData> CharacterPrefabData;

        /// <summary>
        /// Konvertiert diese Instanz in einen JSON-String.
        /// Hilfsmethode für den SaveManager.
        /// </summary>
        /// <returns>Ein JSON-formatierter String, der diese Instanz repräsentiert.</returns>
        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }

        /// <summary>
        /// Erstellt eine <see cref="NovelSaveData"/>-Instanz aus einem JSON-String.
        /// Hilfsmethode für den SaveManager.
        /// </summary>
        /// <param name="json">Der zu deserialisierende JSON-String.</param>
        /// <returns>Eine neue Instanz von <see cref="NovelSaveData"/>, gefüllt mit den Daten aus dem JSON-String.</returns>
        public static NovelSaveData FromJson(string json)
        {
            return JsonUtility.FromJson<NovelSaveData>(json);
        }
    }
}