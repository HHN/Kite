using System.Collections.Generic;
using System.Text;

namespace Assets._Scripts.Managers
{
    /// <summary>
    /// PlayerDataManager is responsible for managing player-specific data
    /// and preferences. This class supports saving, retrieving, and
    /// clearing data related to user preferences, gameplay history, and
    /// other persistent information. It acts as a wrapper around Unity's
    /// PlayerPrefs, providing additional logic for managing application-specific
    /// data needs.
    /// </summary>
    public class PlayerDataManager
    {
        private static PlayerDataManager _instance;

        private Dictionary<string, string> _playerPrefs = new Dictionary<string, string>();

        private List<string> _keys = new List<string>();

        private List<string> _novelHistory = new List<string>();

        /// <summary>
        /// Provides a global access point to the single instance of the PlayerDataManager class.
        /// Ensures that only one instance of PlayerDataManager exists in the application (Singleton pattern).
        /// </summary>
        /// <returns>The single instance of the PlayerDataManager class.</returns>
        public static PlayerDataManager Instance()
        {
            if (_instance == null)
            {
                _instance = new PlayerDataManager();
            }

            return _instance;
        }

        /// <summary>
        /// Determines whether a specific type exists in the player's data.
        /// Checks both the in-memory player preferences and the Unity PlayerPrefs storage.
        /// </summary>
        /// <param name="key">The type to check for existence in the player data.</param>
        /// <returns>True if the type exists in either the in-memory dictionary or Unity PlayerPrefs; otherwise, false.</returns>
        public bool HasKey(string key)
        {
            if (_playerPrefs.ContainsKey(key))
            {
                return true;
            }
            else if (UnityEngine.PlayerPrefs.HasKey(key))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Updates the player's novel history with a new list of novels.
        /// This method sets the internal novel history for tracking
        /// previously played or interacted visual novels.
        /// </summary>
        /// <param name="novelHistory">A list of novel identifiers representing the player's novel history.</param>
        public void SetNovelHistory(List<string> novelHistory)
        {
            _novelHistory = novelHistory;
        }

        /// <summary>
        /// Saves a string value associated with a specific type to persistent storage.
        /// Uses Unity's PlayerPrefs system to store the data and ensures the type is added
        /// to an internal type management list for better organization of stored data.
        /// </summary>
        /// <param name="key">The unique identifier for the data being saved.</param>
        /// <param name="content">The string content to be saved under the given type.</param>
        public void SavePlayerData(string key, string content)
        {
            UnityEngine.PlayerPrefs.SetString(key, content);
            UnityEngine.PlayerPrefs.Save();
            AddKeyToKeyList(key);
        }

        /// <summary>
        /// Adds a specified type to the internal list of keys if it is not already present.
        /// Ensures that new keys are tracked and subsequently saved for persistence.
        /// </summary>
        /// <param name="key">The type to add to the internal type list.</param>
        private void AddKeyToKeyList(string key)
        {
            if (!_keys.Contains(key))
            {
                _keys.Add(key);
                SaveKeys();
            }
        }

        /// <summary>
        /// Persists the list of keys associated with stored player data to Unity's PlayerPrefs.
        /// Ensures that all keys managed by the PlayerDataManager are saved as a single, delimited string.
        /// </summary>
        private void SaveKeys()
        {
            UnityEngine.PlayerPrefs.SetString("keys", string.Join(",", _keys));
            UnityEngine.PlayerPrefs.Save();
        }

        /// <summary>
        /// Loads all player preferences stored in Unity's PlayerPrefs and caches them locally in the PlayerDataManager instance.
        /// This method ensures that previously persisted keys and their associated data are retrieved and kept up-to-date
        /// in the local dictionary for quick access during runtime.
        /// </summary>
        public void LoadAllPlayerPrefs()
        {
            if (_keys.Count == 0)
            {
                string keysString = UnityEngine.PlayerPrefs.GetString("keys", "");
                _keys = new List<string>(keysString.Split(','));
            }

            foreach (string playerPref in _keys)
            {
                if (!_playerPrefs.ContainsKey(playerPref))
                {
                    _playerPrefs.Add(playerPref, ReadPlayerData(playerPref));
                }
                else
                {
                    _playerPrefs[playerPref] = ReadPlayerData(playerPref);
                }
            }
        }

        /// <summary>
        /// Retrieves the data associated with the specified type.
        /// If the type exists in the internal dictionary, the value is returned.
        /// If the type is not found in the dictionary but exists in Unity's PlayerPrefs, the value from PlayerPrefs is returned.
        /// If the type does not exist in both, an empty string is returned.
        /// </summary>
        /// <param name="key">The type whose associated data is to be retrieved.</param>
        /// <returns>The data as a string associated with the specified type, or an empty string if the type does not exist.</returns>
        public string GetPlayerData(string key)
        {
            if (_playerPrefs.TryGetValue(key, out string value))
            {
                return value;
            }
            else
            {
                // Überprüft, ob ein Wert für den angegebenen Schlüssel existiert
                if (UnityEngine.PlayerPrefs.HasKey(key))
                {
                    // Gibt den Wert zurück, der dem Schlüssel zugeordnet ist
                    return UnityEngine.PlayerPrefs.GetString(key);
                }
                else
                {
                    // Gibt einen leeren String zurück, wenn der Schlüssel nicht existiert
                    return "";
                }
            }
        }

        /// <summary>
        /// Reads the player-specific data for the given type from PlayerPrefs.
        /// Retrieves the value associated with the specified type if it exists;
        /// otherwise, returns an empty string.
        /// </summary>
        /// <param name="key">The type for which the associated value is to be retrieved from PlayerPrefs.</param>
        /// <returns>The value associated with the specified type if it exists; otherwise, an empty string.</returns>
        private string ReadPlayerData(string key)
        {
            // Überprüft, ob ein Wert für den angegebenen Schlüssel existiert
            if (UnityEngine.PlayerPrefs.HasKey(key))
            {
                // Gibt den Wert zurück, der dem Schlüssel zugeordnet ist
                return UnityEngine.PlayerPrefs.GetString(key);
            }
            else
            {
                // Gibt einen leeren String zurück, wenn der Schlüssel nicht existiert
                return "";
            }
        }

        /// <summary>
        /// Saves an evaluation for a specific novel into persistent storage, maintaining a record of novel history
        /// and evaluations using structured formatting. The data is stored in Unity's PlayerPrefs.
        /// </summary>
        /// <param name="novelName">The name of the novel for which the evaluation is being saved.</param>
        /// <param name="evaluation">The evaluation or feedback provided for the novel.</param>
        public void SaveEvaluation(string novelName, string evaluation)
        {
            // Key for PlayerPrefs
            const string key = "evaluations";

            // Load existing evaluations 
            string existingEvaluations = UnityEngine.PlayerPrefs.GetString(key, "");
            string novelHistoryString = CombineSentences(_novelHistory);

            // Define separators
            const string mainSeparator = "|"; // Separates different evaluations
            const string subSeparator = "~"; // Separates novel name from flow and evaluation
            const string flowSeparator = "^"; // Separates flow from evaluation

            // Prepare a new evaluation by combining novel name, flow and evaluation with separators
            string newEvaluation = novelName + subSeparator + novelHistoryString + flowSeparator + evaluation;

            // Append new evaluation to existing ones
            if (!string.IsNullOrEmpty(existingEvaluations))
            {
                existingEvaluations += mainSeparator;
            }

            existingEvaluations += newEvaluation;

            UnityEngine.PlayerPrefs.SetString(key, existingEvaluations);
            UnityEngine.PlayerPrefs.Save();
        }

        /// <summary>
        /// Combines a list of strings into a single string, separated by a specified separator.
        /// </summary>
        /// <param name="sentences">A list of strings to be combined.</param>
        /// <returns>A single string containing all the input strings, separated by delimiters.</returns>
        private string CombineSentences(List<string> sentences)
        {
            // Choose a separator guaranteed not to appear in the sentences
            const string separator = "##";

            // Use StringBuilder to ensure efficiency when concatenating strings
            StringBuilder combined = new StringBuilder();

            // Iterate through all sentences
            foreach (string sentence in sentences)
            {
                // Append each sentence with the separator
                combined.Append(sentence);
                combined.Append(separator);
            }

            // Remove the last separator
            if (combined.Length > 0)
            {
                combined.Remove(combined.Length - separator.Length, separator.Length);
            }

            // Return the combined string
            return combined.ToString();
        }

        /// <summary>
        /// Clears all player-related data managed by the PlayerDataManager, including
        /// player preferences, keys, and novel history. This method resets the internal
        /// data structures to their default empty states.
        /// </summary>
        public void ClearEverything()
        {
            _playerPrefs = new Dictionary<string, string>();
            _keys = new List<string>();
            _novelHistory = new List<string>();
        }
    }
}