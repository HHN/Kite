using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Assets._Scripts.Controller.SceneControllers;
using Assets._Scripts.Managers;
using Assets._Scripts.Novel;
using Assets._Scripts.Player;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets._Scripts.SaveNovelData
{
    /// <summary>
    /// Manages the saving and loading of visual novel game progress to and from persistent storage.
    /// This static class handles serialization and deserialization of <see cref="NovelSaveData"/>
    /// objects, allowing players to resume their progress.
    /// </summary>
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
            // Load all existing saved data as a dictionary to ensure we don't overwrite other novel saves.
            Dictionary<string, NovelSaveData> allSaveData = LoadAllSaveData();

            // Get the unique ID of the novel currently being played.
            string currentNovelId = playNovelSceneController.NovelToPlay.id.ToString();

            // The goal is to save at the point of a choice, discarding subsequent events.
            VisualNovelEvent currentEvent = playNovelSceneController.GetCurrentEvent();

            // Define a new event ID format, initially using the current event's ID.
            string formattedId = currentEvent.id;
            bool noMatch = false; // Flag to indicate if no "OptionsLabel" match was found initially.

            // Use Regex to find "OptionsLabel" followed by numbers.
            // This suggests saving typically occurs right after a choice event ID.
            Match match = Regex.Match(currentEvent.id, @"^OptionsLabel(\d+)");

            // If a matching pattern is found in the current event's ID, set the formatted ID.
            if (match.Success)
            {
                formattedId = "OptionsLabel" + match.Groups[1].Value;
            }
            else // If no direct match in the current event, search backwards in the content history.
            {
                for (int i = conversationContentGuiController.Content.Count - 1; i >= 0; i--)
                {
                    match = Regex.Match(conversationContentGuiController.Content[i].id, @"^OptionsLabel(\d+)");
                    if (match.Success)
                    {
                        formattedId = "OptionsLabel" + match.Groups[1].Value;
                        noMatch = true; // Indicate that a match was found by searching backwards.
                        break; // Found the last options label, exit loop.
                    }
                }
            }

            List<string> messageBoxesNames = new List<string>();
            int index = 0; // Index to mark where to truncate GUI content.

            if (noMatch) // If "OptionsLabel" was found by searching backwards through `Content` list
            {
                // Now search backwards through `GuiContent` to find the corresponding "OptionsToChooseFrom" GUI element.
                for (int i = conversationContentGuiController.GuiContent.Count - 1; i >= 0; i--)
                {
                    var messageBox = conversationContentGuiController.GuiContent[i];

                    // If "OptionsToChooseFrom(Clone)" is found, this marks the cut-off point for GUI content.
                    if (messageBox.name.Contains("OptionsToChooseFrom(Clone)"))
                    {
                        index = i; // Store this index.
                        break; // Element found, exit loop.
                    }
                }

                // Iterate through GuiContent up to the determined 'index' to capture message box names.
                for (int i = 0; i < index; i++)
                {
                    var messageBox = conversationContentGuiController.GuiContent[i];

                    // Exclude the options selection UI itself from the saved message types.
                    if (!messageBox.name.Contains("OptionsToChooseFrom(Clone)"))
                    {
                        if (messageBox.name.Contains("Blue Message Prefab With Trigger(Clone)"))
                        {
                            _count++; // Increment count for player messages with triggers.
                        }
                        messageBoxesNames.Add(messageBox.name);
                    }
                }

                int indexToRemoveUpTo = -1; // Index to mark where to truncate VisualNovelEvents.
                // Search backwards through `VisualNovelEvents` to find the exact "OptionsLabel" event.
                for (int i = conversationContentGuiController.VisualNovelEvents.Count - 1; i >= 0; i--)
                {
                    if (conversationContentGuiController.VisualNovelEvents[i].id.Contains(formattedId))
                    {
                        indexToRemoveUpTo = i; // Store this index.
                        break; // Element found, exit loop.
                    }
                }

                // If an element was found, remove all subsequent elements from VisualNovelEvents.
                if (indexToRemoveUpTo != -1)
                {
                    // Removes all elements from indexToRemoveUpTo to the end of the list.
                    conversationContentGuiController.VisualNovelEvents.RemoveRange(indexToRemoveUpTo, conversationContentGuiController.VisualNovelEvents.Count - indexToRemoveUpTo);
                }
            }
            else // If "OptionsLabel" was found directly in the `currentEvent.id` (normal flow for saving at a choice).
            {
                // Iterate through all GuiContent (excluding options UI) to collect message box names.
                foreach (var messageBox in conversationContentGuiController.GuiContent)
                {
                    if (!messageBox.name.Contains("OptionsToChooseFrom(Clone)"))
                    {
                        if (messageBox.name.Contains("Blue Message Prefab With Trigger(Clone)"))
                        {
                            _count++; // Increment count for player messages with triggers.
                        }
                        messageBoxesNames.Add(messageBox.name);
                    }
                }
            }

            // Prepare character prefab data for saving.
            Dictionary<long, CharacterData> characterPrefabData = new Dictionary<long, CharacterData>();
            if (playNovelSceneController.NovelImageController != null)
            {
                // Assuming NovelImageController has access to the global CharacterData dictionary.
                var characterController = playNovelSceneController.NovelImageController; // This line seems redundant as characterController is not used directly.
                if (GameManager.Instance.GetCharacterDataDictionary() != null)
                {
                    foreach (var characterData in GameManager.Instance.GetCharacterDataDictionary())
                    {
                        characterPrefabData.Add(characterData.Key, characterData.Value);
                    }
                }
            }

            // Special handling for the last entry in PlayThroughHistory if it's just a placeholder.
            if (playNovelSceneController.PlayThroughHistory.Count > 0 && playNovelSceneController.PlayThroughHistory[^1].Equals(": "))
            {
                playNovelSceneController.PlayThroughHistory[^1] = "Spielerin: "; // Correct the placeholder.
            }

            // Create a new NovelSaveData object with all the collected information.
            NovelSaveData saveData = new NovelSaveData
            {
                currentEventId = formattedId,
                playThroughHistory = playNovelSceneController.PlayThroughHistory,
                optionsId = playNovelSceneController.OptionsId.ToArray(),
                eventHistory = playNovelSceneController.EventHistory,
                content = conversationContentGuiController.Content, // Conversation content.
                visualNovelEvents = conversationContentGuiController.VisualNovelEvents, // Visual novel event history.
                messageType = messageBoxesNames, // Names of message box prefabs used.
                optionCount = _count, // Number of "Blue Message Prefab With Trigger" messages.
                CharacterExpressions = playNovelSceneController.CharacterExpressions, // Character expressions.
                CharacterPrefabData = characterPrefabData // Data about character prefabs (positions, etc.).
            };

            // Delete any existing save data for this novel ID before saving the new one.
            DeleteNovelSaveData(currentNovelId);
            // Add or update the novel's save data in the dictionary.
            allSaveData[currentNovelId] = saveData;

            // Serialize the entire dictionary of save data to a JSON string with pretty formatting.
            string json = JsonConvert.SerializeObject(allSaveData, Formatting.Indented);

            // Write the JSON string to the save file.
            File.WriteAllText(SaveFilePath, json, Encoding.UTF8);

            // Update the GameManager to reflect that this novel now has saved data.
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
        /// <returns>A dictionary containing all saved novel data, where the type is the novel ID.</returns>
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