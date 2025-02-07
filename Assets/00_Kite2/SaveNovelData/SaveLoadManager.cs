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

            VisualNovelEvent currentEvent = playNovelSceneController.GetCurrentEvent();

            // Define a new event ID format
            string formattedId = currentEvent.id;

            if (currentEvent.id.StartsWith("OptionsLabel"))
            {
                // Use Regex to extract "OptionsLabel" followed by numbers
                Match match = Regex.Match(currentEvent.id, @"^OptionsLabel(\d+)");
                if (match.Success)
                {
                    // Extract the number after "OptionsLabel"
                    string numericPart = match.Groups[1].Value;
                    formattedId = "OptionsLabel" + numericPart;
                    
                    // if (playNovelSceneController.PlayThroughHistory[^1].Equals(": "))
                    // {
                    //     playNovelSceneController.PlayThroughHistory[^1] = "Spielerin: ";
                    // }
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
            
            // VisualNovelEvent currentNovelEvent = new VisualNovelEvent();
            // if (currentEvent.id.StartsWith("OptionsLabel"))
            // {
            //     int index = playNovelSceneController.EventHistory.Count - 1;
            //
            //     while (index >= 0 && playNovelSceneController.EventHistory[index].id.StartsWith("OptionsLabel"))
            //     {
            //         index--;
            //     }
            //
            //     if (index >= 0) // Falls eine gültige ID gefunden wurde
            //     {
            //         string id = playNovelSceneController.EventHistory[index].id;
            //         
            //         foreach (var novelEvent in playNovelSceneController.NovelToPlay.novelEvents)
            //         {
            //             if (novelEvent.id == id)
            //             {
            //                 currentEvent = novelEvent;
            //             }
            //         }
            //     }
            // }
            //
            // Debug.Log(currentNovelEvent.character + " " + currentEvent.expressionType);
            
            // foreach (var characterData in characterPrefabData)
            // {
            //     // Holen des Values (CharacterData)
            //     CharacterData character = characterData.Value;
            //
            //     // Ausgabe der Eigenschaften
            //     Debug.Log("Character Skin Index 1: " + character.skinIndex);
            //     Debug.Log("Character Glass Index 1: " + character.glassIndex);
            //     Debug.Log("Character Hand Index 1: " + character.handIndex);
            //     Debug.Log("Character Clothe Index 1: " + character.clotheIndex);
            //     Debug.Log("Character Hair Index 1: " + character.hairIndex);
            //
            //     Debug.Log("Character Skin Index 2: " + character.skinIndex2);
            //     Debug.Log("Character Glass Index 2: " + character.glassIndex2);
            //     Debug.Log("Character Hand Index 2: " + character.handIndex2);
            //     Debug.Log("Character Clothe Index 2: " + character.clotheIndex2);
            //     Debug.Log("Character Hair Index 2: " + character.hairIndex2);
            // }
            
            // foreach (var expression in playNovelSceneController.CharacterExpressions)
            // {
            //     Debug.Log("Save: " + expression.Key + " : " + expression.Value);
            // }

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

            // Debug.Log("PlayThroughHistory");
            // foreach (var text in playNovelSceneController.PlayThroughHistory)
            // {
            //     Debug.Log(text);
            // }

            // Save or update the novel in the dictionary
            allSaveData[currentNovelId] = saveData;

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