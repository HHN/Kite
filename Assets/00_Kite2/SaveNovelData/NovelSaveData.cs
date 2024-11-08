using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NovelSaveData
{
    public string novelId; // Einzigartige ID der Novel
    public string currentEventId; // Aktuelles Event
    public List<string> playThroughHistory; // Verlauf der gewählten Aktionen oder Dialoge

    // GUI-relevante Daten (IDs statt GameObjects)
    //public List<string> guiContent;

    // Methode zum Umwandeln in JSON-Format (Hilfsmethode für den SaveManager)
    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    // Methode zum Erstellen aus JSON (Hilfsmethode für den SaveManager)
    public static NovelSaveData FromJson(string json)
    {
        return JsonUtility.FromJson<NovelSaveData>(json);
    }
}