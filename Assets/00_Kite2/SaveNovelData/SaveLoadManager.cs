using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

// Manages saving and loading game data for a visual novel-style game
public class SaveLoadManager : MonoBehaviour
{
    private static string saveFilePath = Application.persistentDataPath + "/novelSaveData.json";

    public static void SaveNovelData(PlayNovelSceneController playNovelSceneController, ConversationContentGuiController conversationContentGuiController)
    {
        // Lade alle gespeicherten Daten als Dictionary
        Dictionary<string, NovelSaveData> allSaveData = LoadAllSaveData();

        // Aktuelle Novel-Daten
        string currentNovelId = playNovelSceneController.NovelToPlay.id.ToString();
        string nextEventToPlayId = playNovelSceneController.NextEventToPlay.id;

        VisualNovelEvent currentEvent = playNovelSceneController.GetCurrentEvent();

        // Definiere ein neues Event-ID-Format
        string formattedId = currentEvent.id;

        if (currentEvent.id.StartsWith("OptionsLabel"))
        {
            // Verwende Regex, um "OptionsLabel" gefolgt von Zahlen zu extrahieren
            Match match = Regex.Match(currentEvent.id, @"^OptionsLabel(\d+)");
            if (match.Success)
            {
                // Extrahiere die Zahl nach "OptionsLabel"
                string numericPart = match.Groups[1].Value;
                formattedId = "OptionsLabel" + numericPart;
            }
        }

        NovelSaveData saveData = new NovelSaveData
        {
            //novelId = currentNovelId,
            currentEvent = formattedId,
            nextEventToPlayId = nextEventToPlayId,
            playThroughHistory = playNovelSceneController.PlayThroughHistory,
            visualNovelEvents = conversationContentGuiController.VisualNovelEvents,
        };

        // Speichere oder aktualisiere die Novel im Dictionary
        allSaveData[currentNovelId] = saveData;

        // Serialisiere das Dictionary und speichere es im Pretty-Format
        string json = JsonConvert.SerializeObject(allSaveData, Formatting.Indented);

        File.WriteAllText(saveFilePath, json, Encoding.UTF8);

        GameManager.Instance.UpdateNovelSaveStatus(currentNovelId, true);
    }

    // Lädt die Daten einer bestimmten Novel
    public static NovelSaveData Load(string currentNovelId)
    {
        // Lade alle gespeicherten Daten
        Dictionary<string, NovelSaveData> allSaveData = LoadAllSaveData();

        // Suche nach einem Speicherstand mit passender novelId
        if (allSaveData.TryGetValue(currentNovelId, out NovelSaveData saveData))
        {
            return saveData;
        }
        else
        {
            return null;
        }
    }

    // Lädt alle gespeicherten Daten aus der JSON-Datei
    private static Dictionary<string, NovelSaveData> LoadAllSaveData()
    {
        if (File.Exists(saveFilePath))
        {
            try
            {
                string json = File.ReadAllText(saveFilePath);
                Dictionary<string, NovelSaveData> allSaveData = JsonConvert.DeserializeObject<Dictionary<string, NovelSaveData>>(json);
                return allSaveData ?? new Dictionary<string, NovelSaveData>();
            }
            catch (Exception ex)
            {
                Debug.LogError("Fehler beim Laden der Daten: " + ex.Message);
                return new Dictionary<string, NovelSaveData>();
            }
        }
        else
        {
            return new Dictionary<string, NovelSaveData>();
        }
    }

    public static void DeleteNovelSaveData(string novelId)
    {
        // Lade alle gespeicherten Daten
        Dictionary<string, NovelSaveData> allSaveData = LoadAllSaveData();

        // Versuche, das Element zu löschen
        bool removed = allSaveData.Remove(novelId);

        if (removed)
        {
            // Überschreibe die Datei nur, wenn die Löschung erfolgreich war
            string json = JsonConvert.SerializeObject(allSaveData, Formatting.Indented);
            File.WriteAllText(saveFilePath, json, Encoding.UTF8);
        }
        else
        {
            Debug.LogWarning($"Kein Spielstand für Novel ID {novelId} gefunden.");
        }
    }

    public static void ClearAllSaveData()
    {
        // Leeres Dictionary erstellen
        Dictionary<string, NovelSaveData> emptySaveData = new Dictionary<string, NovelSaveData>();

        // Serialisiere das leere Dictionary und überschreibe die Datei
        string json = JsonConvert.SerializeObject(emptySaveData, Formatting.Indented);
        File.WriteAllText(saveFilePath, json, Encoding.UTF8);
    }

    // Wrapper-Klasse zum Speichern der Liste als JSON
    [Serializable]
    private class SaveDataListWrapper
    {
        public Dictionary<string, NovelSaveData> allSaveData = new Dictionary<string, NovelSaveData>();
    }
}