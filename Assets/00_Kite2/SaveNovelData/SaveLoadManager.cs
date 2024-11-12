using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

// Manages saving and loading game data for a visual novel-style game
public class SaveLoadManager : MonoBehaviour
{
    private static string saveFilePath = Application.persistentDataPath + "/novelSaveData.json";

    public static void SaveNovelData(PlayNovelSceneController playNovelSceneController, ConversationContentGuiController conversationContentGuiController)
    {
        List<NovelSaveData> allSaveData = LoadAllSaveData();

        // Aktuelle Novel-Daten
        string currentNovelId = playNovelSceneController.NovelToPlay.id.ToString();
        NovelSaveData saveData = new NovelSaveData
        {
            novelId = currentNovelId,
            currentEventId = playNovelSceneController.NextEventToPlay.id,
            playThroughHistory = playNovelSceneController.PlayThroughHistory,
            visualNovelEvents = conversationContentGuiController.VisualNovelEvents,
        };

        // Überprüfe, ob die Novel bereits in der Liste existiert
        int existingIndex = allSaveData.FindIndex(data => data.novelId == currentNovelId);
        if (existingIndex >= 0)
        {
            // Überschreibe den bisherigen Speicherstand für diese Novel
            allSaveData[existingIndex] = saveData;
        }
        else
        {
            // Neue Novel speichern
            allSaveData.Add(saveData);
        }

        // Serialisiere die gesamte Liste und speichere sie im Pretty-Format
        var wrapper = new SaveDataListWrapper { allSaveData = allSaveData };
        string json = JsonConvert.SerializeObject(wrapper, Formatting.Indented);

        File.WriteAllText(saveFilePath, json, Encoding.UTF8);

        //Debug.Log("Spielstand gespeichert für Novel ID: " + currentNovelId);

        SaveLoadManager.SaveNovelData(playNovelSceneController, conversationContentGuiController);
        GameManager.Instance.UpdateNovelSaveStatus(currentNovelId, true);

    }

    // Lädt die Daten einer bestimmten Novel
    public static NovelSaveData Load(string currentNovelId)
    {
        //Debug.Log("Versuche, Novel mit ID zu laden: " + currentNovelId);

        // Lade alle gespeicherten Daten
        List<NovelSaveData> allSaveData = LoadAllSaveData();

        // Suche nach einem Speicherstand mit passender novelId
        NovelSaveData saveData = allSaveData.FirstOrDefault(data => data.novelId == currentNovelId);
        if (saveData != null)
        {
            //Debug.Log("Daten erfolgreich geladen für Novel ID: " + currentNovelId);
            return saveData;
        }
        else
        {
            //Debug.LogWarning("Kein Speicherstand für Novel ID: " + currentNovelId);
            return null;
        }
    }

    // Lädt alle gespeicherten Daten aus der JSON-Datei
    private static List<NovelSaveData> LoadAllSaveData()
    {
        if (File.Exists(saveFilePath))
        {
            try
            {
                string json = File.ReadAllText(saveFilePath);
                //Debug.Log("Geladener JSON-Inhalt: " + json);
                SaveDataListWrapper wrapper = JsonUtility.FromJson<SaveDataListWrapper>(json);
                //Debug.Log("Anzahl der geladenen Einträge: " + wrapper.allSaveData.Count);
                return wrapper.allSaveData;
            }
            catch (Exception ex)
            {
                Debug.LogError("Fehler beim Laden der Daten: " + ex.Message);
                return new List<NovelSaveData>();
            }
        }
        else
        {
            //Debug.LogWarning("Speicherdatei nicht gefunden.");
            return new List<NovelSaveData>();
        }
    }

    public static void DeleteNovelSaveData(string novelId)
    {
        // Lade alle gespeicherten Daten
        List<NovelSaveData> allSaveData = LoadAllSaveData();

        // Suche und entferne den Speicherstand für die gegebene novelId
        int indexToRemove = allSaveData.FindIndex(data => data.novelId == novelId);
        if (indexToRemove >= 0)
        {
            allSaveData.RemoveAt(indexToRemove);

            // Speichere die aktualisierte Liste zurück in die Datei
            string json = JsonUtility.ToJson(new SaveDataListWrapper { allSaveData = allSaveData });
            File.WriteAllText(saveFilePath, json);

            Debug.Log($"Spielstand für Novel ID {novelId} gelöscht.");
        }
        else
        {
            Debug.LogWarning($"Kein Spielstand für Novel ID {novelId} gefunden.");
        }
    }


    // Wrapper-Klasse zum Speichern der Liste als JSON
    [Serializable]
    private class SaveDataListWrapper
    {
        public List<NovelSaveData> allSaveData = new List<NovelSaveData>();
    }
}