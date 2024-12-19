using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using _00_Kite2.Common.Novel;
using _00_Kite2.Player;
using Newtonsoft.Json;
using UnityEngine;

// Manages saving and loading game data for a visual novel-style game
namespace _00_Kite2.SaveNovelData
{
    public class SaveLoadManager : MonoBehaviour
    {
        private static readonly string SaveFilePath = Application.persistentDataPath + "/novelSaveData.json";
        private static int _count;

        /// <summary>
        /// Speichert die aktuellen Daten einer Novel.
        /// </summary>
        /// <param name="playNovelSceneController">Der Controller der aktuellen Spielszenen.</param>
        /// <param name="conversationContentGuiController">Der Controller f�r den Konversationsinhalt.</param>
        public static void SaveNovelData(PlayNovelSceneController playNovelSceneController,
            ConversationContentGuiController conversationContentGuiController)
        {
            // Lade alle gespeicherten Daten als Dictionary
            Dictionary<string, NovelSaveData> allSaveData = LoadAllSaveData();

            // Aktuelle Novel-Daten
            string currentNovelId = playNovelSceneController.NovelToPlay.id.ToString();

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

            List<string> messageBoxesNames = new List<string>();
            foreach (var messageBox in conversationContentGuiController.GuiContent)
            {
                if (!messageBox.name.Contains("OptionsToChooseFrom(Clone)"))
                {
                    if (messageBox.name.Contains("Blue Message Prefab With Trigger(Clone)"))
                    {
                        _count++;
                    }

                    messageBoxesNames.Add(messageBox.name);
                }
            }

            NovelSaveData saveData = new NovelSaveData
            {
                //novelId = currentNovelId,
                currentEvent = formattedId,
                playThroughHistory = playNovelSceneController.PlayThroughHistory,
                optionsId = playNovelSceneController.OptionsId.ToArray(),
                eventHistory = playNovelSceneController.EventHistory,
                content = conversationContentGuiController
                    .Content, // Brauch ich hier auch die eventHistory von playNovelSceneController
                visualNovelEvents =
                    conversationContentGuiController
                        .VisualNovelEvents, // Brauch ich hier auch die eventHistory von playNovelSceneController
                messageType = messageBoxesNames,
                optionCount = _count,
            };

            // Speichere oder aktualisiere die Novel im Dictionary
            allSaveData[currentNovelId] = saveData;

            // Serialisiere das Dictionary und speichere es im Pretty-Format
            string json = JsonConvert.SerializeObject(allSaveData, Formatting.Indented);

            File.WriteAllText(SaveFilePath, json, Encoding.UTF8);

            GameManager.Instance.UpdateNovelSaveStatus(currentNovelId, true);
        }

        /// <summary>
        /// L�dt die Daten einer bestimmten Novel.
        /// </summary>
        /// <param name="currentNovelId">Die eindeutige ID der zu ladenden Novel.</param>
        /// <returns>Gibt die gespeicherten Daten der Novel zur�ck, oder <c>null</c>, wenn keine Daten gefunden wurden.</returns>
        public static NovelSaveData Load(string currentNovelId)
        {
            // Lade alle gespeicherten Daten
            Dictionary<string, NovelSaveData> allSaveData = LoadAllSaveData();

            // Suche nach einem Speicherstand mit passender novelId
            return allSaveData.GetValueOrDefault(currentNovelId);
        }

        /// <summary>
        /// L�dt alle gespeicherten Daten aus der JSON-Datei.
        /// </summary>
        /// <returns>Ein Dictionary mit allen gespeicherten Noveldaten, wobei der Schlüssel die Novel-ID ist.</returns>
        private static Dictionary<string, NovelSaveData> LoadAllSaveData()
        {
            if (File.Exists(SaveFilePath))
            {
                try
                {
                    string json = File.ReadAllText(SaveFilePath);
                    Dictionary<string, NovelSaveData> allSaveData =
                        JsonConvert.DeserializeObject<Dictionary<string, NovelSaveData>>(json);
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

        /// <summary>
        /// Löscht die gespeicherten Daten einer bestimmten Novel.
        /// </summary>
        /// <param name="novelId">Die eindeutige ID der zu löschenden Novel.</param>
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
                File.WriteAllText(SaveFilePath, json, Encoding.UTF8);
            }
            else
            {
                Debug.LogWarning($"Kein Spielstand f�r Novel ID {novelId} gefunden.");
            }
        }

        /// <summary>
        /// Löscht alle gespeicherten Noveldaten.
        /// </summary>
        public static void ClearAllSaveData()
        {
            // Leeres Dictionary erstellen
            Dictionary<string, NovelSaveData> emptySaveData = new Dictionary<string, NovelSaveData>();

            // Serialisiere das leere Dictionary und Überschreibe die Datei
            string json = JsonConvert.SerializeObject(emptySaveData, Formatting.Indented);
            File.WriteAllText(SaveFilePath, json, Encoding.UTF8);
        }
    }
}