using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NovelSaveData
{
    public string currentEvent;
    public List<string> playThroughHistory; // Verlauf der gewählten Aktionen oder Dialoge
    public List<VisualNovelEvent> visualNovelEvents;

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