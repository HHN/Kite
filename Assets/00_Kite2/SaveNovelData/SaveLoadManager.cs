using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

// Manages saving and loading game data for a visual novel-style game
public class SaveLoadManager : MonoBehaviour
{
    private static string saveFilePath = Application.persistentDataPath + "/novelSaveData.json";

    public static void SaveNovelData(PlayNovelSceneController playNovelSceneController, ConversationContentGuiController conversationContentGuiController)
    {
        NovelSaveData saveData = new NovelSaveData
        {
            novelId = playNovelSceneController.NovelToPlay.id.ToString(),
            currentEventId = playNovelSceneController.NextEventToPlay.id,
            playThroughHistory = playNovelSceneController.PlayThroughHistory,
        };

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(saveFilePath, json);

        Debug.Log("Spielstatus gespeichert: " + saveFilePath);
    }

    public static NovelSaveData Load(string currentNovelId)
    {
        if (File.Exists(saveFilePath))
        {
            try
            {
                string json = File.ReadAllText(saveFilePath);
                NovelSaveData data = NovelSaveData.FromJson(json);

                Debug.Log("data.novelId: " + data.novelId);
                Debug.Log("currentNovelId: " + currentNovelId);

                // Prüfe, ob die gespeicherte novelId mit der aktuellen übereinstimmt
                if (data.novelId == currentNovelId)
                {
                    Debug.Log("Daten erfolgreich geladen.");
                    return data;
                }
                else
                {
                    Debug.LogWarning("Keine passenden Speicherdateien für die aktuelle Novel.");
                    return null; // Kein passender Speicherstand
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Fehler beim Laden der Daten: " + ex.Message);
            }
        }
        else
        {
            Debug.LogWarning("Speicherdatei nicht gefunden.");
        }

        return null;
    }

    // Methode zum Überprüfen, ob Speicherdaten vorhanden sind
    public static bool SaveExists()
    {
        return File.Exists(saveFilePath);
    }
}