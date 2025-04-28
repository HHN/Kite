using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Assets._Scripts.Controller.SceneControllers;
using Assets._Scripts.Novel;
using Assets._Scripts.Player;
using Newtonsoft.Json;
using UnityEngine;

// Manages saving and loading game data for a visual novel-style game
namespace Assets._Scripts.SaveNovelData
{
    public class SaveLoadManager : MonoBehaviour
    {
        private static readonly string SaveFilePath = Application.persistentDataPath + "/novelSaveData.json";
        private static int _count;
        private static Dictionary<string, GameObject> _characterPrefabs;

        /// <summary>
        /// Saves the current data of a novel.
        /// </summary>
        /// <param name="playNovelSceneController">The controller for the current play scene.</param>
        /// <param name="conversationContentGuiController">The controller for the conversation content.</param>
        public static void SaveNovelData(PlayNovelSceneController playNovelSceneController,
            ConversationContentGuiController conversationContentGuiController)
        {
            // Load all saved data as a dictionary
            Dictionary<string, NovelSaveData> allSaveData = LoadAllSaveData();

            // Current novel data
            string currentNovelId = playNovelSceneController.NovelToPlay.id.ToString();

            // Speichern an der Stelle von OnChoice
            // Alles danach verwerfen, sodass man wieder beim Auswahlverfahren beginnt
            VisualNovelEvent currentEvent = playNovelSceneController.GetCurrentEvent();

            // Define a new event ID format
            string formattedId = currentEvent.id;

            // Use Regex to extract "OptionsLabel" followed by numbers
            Match match = Regex.Match(currentEvent.id, @"^OptionsLabel(\d+)");
            bool noMatch = false;

            // Falls ein passendes Muster gefunden wurde, formatierte ID setzen
            if (match.Success)
            {
                formattedId = "OptionsLabel" + match.Groups[1].Value;
            }
            else if (!match.Success) // Falls kein Treffer, durchlaufe die Liste rückwärts
            {
                for (int i = conversationContentGuiController.Content.Count - 1; i >= 0; i--)
                {
                    match = Regex.Match(conversationContentGuiController.Content[i].id, @"^OptionsLabel(\d+)");
                    if (match.Success)
                    {
                        formattedId = "OptionsLabel" + match.Groups[1].Value;
                        noMatch = true;
                        break;
                    }
                }
            }

            // List<string> messageBoxesNames = new List<string>();
            // foreach (var messageBox in conversationContentGuiController.GuiContent)
            // {
            //     if (!messageBox.name.Contains("OptionsToChooseFrom(Clone)"))
            //     {
            //         if (messageBox.name.Contains("Blue Message Prefab With Trigger(Clone)"))
            //         {
            //             _count++;
            //         }
            //
            //         messageBoxesNames.Add(messageBox.name);
            //     }
            // }

            List<string> messageBoxesNames = new List<string>();
            int index = 0;

            if (noMatch) // Falls vorher kein Match gefunden wurde, auch hier rückwärts durchsuchen
            {
                for (int i = conversationContentGuiController.GuiContent.Count - 1; i >= 0; i--)
                {
                    var messageBox = conversationContentGuiController.GuiContent[i];

                    // Wenn "OptionsToChooseFrom(Clone)" gefunden wird, brich ab
                    if (messageBox.name.Contains("OptionsToChooseFrom(Clone)"))
                    {
                        index = i;
                        break;
                    }
                }

                // Hier wird der GuiContent nur bis zum gefundenen Index durchlaufen
                for (int i = 0; i < index; i++)
                {
                    var messageBox = conversationContentGuiController.GuiContent[i];

                    if (!messageBox.name.Contains("OptionsToChooseFrom(Clone)"))
                    {
                        if (messageBox.name.Contains("Blue Message Prefab With Trigger(Clone)"))
                        {
                            _count++;
                        }

                        messageBoxesNames.Add(messageBox.name);
                    }
                }

                int indexToRemoveUpTo = -1;
                for (int i = conversationContentGuiController.VisualNovelEvents.Count - 1; i >= 0; i--)
                {
                    if (conversationContentGuiController.VisualNovelEvents[i].id.Contains(formattedId))
                    {
                        indexToRemoveUpTo = i;
                        break; // Element gefunden, Schleife verlassen
                    }
                }

                // Wenn ein Element gefunden wurde, lösche alles von hinten bis zu diesem Index
                if (indexToRemoveUpTo != -1)
                {
                    // Entfernt alle Elemente von indexToRemoveUpTo bis zum Ende der Liste
                    conversationContentGuiController.VisualNovelEvents.RemoveRange(indexToRemoveUpTo, conversationContentGuiController.VisualNovelEvents.Count - indexToRemoveUpTo);
                }
            }
            else // Falls vorher ein Match gefunden wurde, normal durchlaufen
            {
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
            }

            Dictionary<long, CharacterData> characterPrefabData = new Dictionary<long, CharacterData>();

            if (playNovelSceneController.NovelImageController != null)
            {
                var characterController = playNovelSceneController.NovelImageController;
                if (characterController != null)
                {
                    foreach (var characterData in GameManager.Instance.GetCharacterDataDictionary())
                    {
                        // Add the data to the dictionary
                        characterPrefabData.Add(characterData.Key, characterData.Value);
                    }
                }
            }

            if (playNovelSceneController.PlayThroughHistory[^1].Equals(": "))
            {
                playNovelSceneController.PlayThroughHistory[^1] = "Spielerin: ";
            }

            NovelSaveData saveData = new NovelSaveData
            {
                //novelId = currentNovelId,
                currentEventId = formattedId,
                playThroughHistory = playNovelSceneController.PlayThroughHistory,
                optionsId = playNovelSceneController.OptionsId.ToArray(),
                eventHistory = playNovelSceneController.EventHistory,
                content = conversationContentGuiController.Content,
                visualNovelEvents = conversationContentGuiController.VisualNovelEvents,
                messageType = messageBoxesNames,
                optionCount = _count,
                CharacterExpressions = playNovelSceneController.CharacterExpressions,
                CharacterPrefabData = characterPrefabData
            };

            DeleteNovelSaveData(currentNovelId);
            // Save or update the novel in the dictionary
            allSaveData[currentNovelId] = saveData;

            foreach (var novelSaveData in allSaveData)
            {
                Debug.Log($"novelId: {novelSaveData.Key}");
                Debug.Log($"{novelSaveData.Value.currentEventId}");
            }

            // Serialize the dictionary and save it in pretty format
            string json = JsonConvert.SerializeObject(allSaveData, Formatting.Indented);

            File.WriteAllText(SaveFilePath, json, Encoding.UTF8);

            GameManager.Instance.UpdateNovelSaveStatus(currentNovelId, true);
        }

        /// <summary>
        /// Loads the data for a specific novel.
        /// </summary>
        /// <param name="currentNovelId">The unique ID of the novel to load.</param>
        /// <returns>Returns the saved data of the novel, or <c>null</c> if no data is found.</returns>
        public static NovelSaveData Load(string currentNovelId)
        {
            // Load all saved data
            Dictionary<string, NovelSaveData> allSaveData = LoadAllSaveData();

            // Look for a save file with the matching novel ID
            return allSaveData.GetValueOrDefault(currentNovelId);
        }

        /// <summary>
        /// Loads all saved data from the JSON file.
        /// </summary>
        /// <returns>A dictionary containing all saved novel data, where the key is the novel ID.</returns>
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
        /// Deletes the saved data for a specific novel.
        /// </summary>
        /// <param name="novelId">The unique ID of the novel to delete.</param>
        public static void DeleteNovelSaveData(string novelId)
        {
            // Load all saved data
            Dictionary<string, NovelSaveData> allSaveData = LoadAllSaveData();

            // Try to remove the item
            bool removed = allSaveData.Remove(novelId);

            if (removed)
            {
                // Overwrite the file only if the deletion was successful
                string json = JsonConvert.SerializeObject(allSaveData, Formatting.Indented);
                File.WriteAllText(SaveFilePath, json, Encoding.UTF8);
            }
            else
            {
                Debug.LogWarning($"Kein Spielstand für Novel ID {novelId} gefunden.");
            }
        }

        /// <summary>
        /// Deletes all saved novel data.
        /// </summary>
        public static void ClearAllSaveData()
        {
            // Create an empty dictionary
            Dictionary<string, NovelSaveData> emptySaveData = new Dictionary<string, NovelSaveData>();

            // Serialize the empty dictionary and overwrite the file
            string json = JsonConvert.SerializeObject(emptySaveData, Formatting.Indented);
            File.WriteAllText(SaveFilePath, json, Encoding.UTF8);
        }
    }
}