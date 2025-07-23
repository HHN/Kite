using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets._Scripts._Mappings
{
    /// <summary>
    /// Handles the mapping of various types of terms and entities (biases, face expressions, characters)
    /// used in the application to their corresponding mapped values or identifiers.
    /// Provides centralized mapping logic for consistent translation and retrieval of related data.
    /// </summary>
    public class MappingManager : MonoBehaviour
    {
        private static MappingManager _instance;

        // Mapping-Files
        private static readonly string MappingFileBias;
        private static readonly string MappingFileFaceExpression;
        private static readonly string MappingFileCharacter;

        // Dictionaries to store the mappings for bias, face expression, and characters
        private static Dictionary<string, string> _biasMapping = new Dictionary<string, string>();
        private static Dictionary<string, int> _faceExpressionMapping = new Dictionary<string, int>();
        private static Dictionary<string, int> _characterMapping = new Dictionary<string, int>();

        /// <summary>
        /// Provides functionality to manage and load mappings for biases, face expressions, and character data.
        /// This class uses platform-specific approaches to load mapping data from files and provides methods to map individual entries.
        /// </summary>
        static MappingManager()
        {
            // For WebGL, we use the StreamingAssets folder, but WebGL files need to be accessed asynchronously
            MappingFileBias = Path.Combine(Application.streamingAssetsPath, "BiasMapping.txt");
            MappingFileFaceExpression = Path.Combine(Application.streamingAssetsPath, "FaceExpressionMapping.txt");
            MappingFileCharacter = Path.Combine(Application.streamingAssetsPath, "CharacterMapping.txt");

            LoadBiasMappingAsync();
            LoadFaceExpressionMappingAsync();
            LoadCharacterMappingAsync();
        }

        /// <summary>
        /// Singleton pattern to ensure only one instance of MappingManager exists
        /// </summary>
        public static MappingManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<MappingManager>();
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject("MappingManager");
                        _instance = obj.AddComponent<MappingManager>();
                        DontDestroyOnLoad(obj);
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// Asynchronously loads the bias mapping data from the appropriate source based on the target platform.
        /// For WebGL, the file is accessed via a UnityWebRequest, while for other platforms the file is read directly from disk.
        /// On a successful load, the file content is processed to populate the bias mapping dictionary.
        /// Logs warnings or errors when the file is not found or cannot be loaded.
        /// </summary>
        private static void LoadBiasMappingAsync()
        {
#if UNITY_WEBGL
            // For WebGL, use UnityWebRequest to load the file asynchronously
            UnityWebRequest www = UnityWebRequest.Get(MappingFileBias);
            www.SendWebRequest().completed += _ =>
            {
                if (www.result == UnityWebRequest.Result.Success)
                {
                    // Split the downloaded content into lines
                    string[] lines = www.downloadHandler.text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    ProcessMappingFile(lines, ref _biasMapping);
                }
                else
                {
                    Application.ExternalCall("logMessage", "Error loading bias mapping: " + www.error);
                }
            };
#else
            // For other platforms, read directly from the file system
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                ProcessMappingFile(lines, ref _biasMapping);
            }
            else
            {
                Debug.LogWarning($"Bias mapping file not found at: {filePath}");
            }
#endif
        }

        /// <summary>
        /// Asynchronously loads the face expressions mapping data from the appropriate source based on the target platform.
        /// For WebGL, the file is accessed via a UnityWebRequest, while for other platforms the file is read directly from disk.
        /// On a successful load, the file content is processed to populate the bias mapping dictionary.
        /// Logs warnings or errors when the file is not found or cannot be loaded.
        /// </summary>
        private static void LoadFaceExpressionMappingAsync()
        {
#if UNITY_WEBGL
            // For WebGL, use UnityWebRequest to load the file asynchronously
            UnityWebRequest www = UnityWebRequest.Get(MappingFileFaceExpression);
            www.SendWebRequest().completed += _ =>
            {
                if (www.result == UnityWebRequest.Result.Success)
                {
                    // Split the downloaded content into lines
                    string[] lines = www.downloadHandler.text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    ProcessFaceExpressionFile(lines, ref _faceExpressionMapping);
                }
                else
                {
                    Application.ExternalCall("logMessage", "Error loading face expression mapping: " + www.error);
                }
            };
#else
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                ProcessFaceExpressionFile(lines, ref _faceExpressionMapping);
            }
            else
            {
                Debug.LogWarning($"Face expression mapping file not found at: {filePath}");
            }
#endif
        }

        /// <summary>
        /// Asynchronously loads the character mapping data from the appropriate source based on the target platform.
        /// For WebGL, the file is accessed via a UnityWebRequest, while for other platforms the file is read directly from disk.
        /// On a successful load, the file content is processed to populate the bias mapping dictionary.
        /// Logs warnings or errors when the file is not found or cannot be loaded.
        /// </summary>
        private static void LoadCharacterMappingAsync()
        {
#if UNITY_WEBGL
            // For WebGL, use UnityWebRequest to load the file asynchronously
            UnityWebRequest www = UnityWebRequest.Get(MappingFileCharacter);
            www.SendWebRequest().completed += _ =>
            {
                if (www.result == UnityWebRequest.Result.Success)
                {
                    // Split the downloaded content into lines
                    string[] lines = www.downloadHandler.text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    ProcessCharacterFile(lines, ref _characterMapping);
                }
                else
                {
                    Application.ExternalCall("logMessage", "Error loading character mapping: " + www.error);
                }
            };
#else
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                ProcessCharacterFile(lines, ref _characterMapping);
            }
            else
            {
                Debug.LogWarning($"Character mapping file not found at: {filePath}");
            }
#endif
        }

        /// <summary>
        /// Processes the mapping file by parsing an array of lines and populating the provided dictionary with key-value pairs.
        /// Each line in the input should have a colon (':') separating the key and value.
        /// Invalid lines are ignored, and warnings are logged for improperly formatted lines.
        /// </summary>
        /// <param name="lines">An array of strings where each element represents a line from a mapping file.</param>
        /// <param name="mapping">A reference to the dictionary that will be populated with key-value pairs extracted from the mapping file.</param>
        private static void ProcessMappingFile(string[] lines, ref Dictionary<string, string> mapping)
        {
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                // Split the line into a key and value at the first colon.
                int colonIndex = line.IndexOf(':');
                if (colonIndex > 0 && colonIndex < line.Length - 1)
                {
                    string key = line.Substring(0, colonIndex).Trim();
                    string value = line.Substring(colonIndex + 1).Trim();

                    // Ensure the key is not empty and add the pair to the dictionary.
                    // TryAdd is a concise way to add only if the key doesn't already exist.
                    if (!string.IsNullOrEmpty(key))
                    {
                        mapping.TryAdd(key, value);
                    }
                }
                else
                {
                    Debug.LogWarning($"Invalid mapping line (expected format 'key:value'): {line}");
                }
            }
        }
        
        /// <summary>
        /// Processes the face expression mapping file lines and populates the given dictionary with mappings.
        /// Each line is expected to have a key-value pair in the format "key:value".
        /// Invalid or malformed lines are ignored, and warnings are logged for invalid entries.
        /// </summary>
        /// <param name="lines">An array of strings representing lines from the face expression mapping file.</param>
        /// <param name="mapping">A reference to the dictionary where the mappings will be stored. Keys are strings, and values are integers.</param>
        private static void ProcessFaceExpressionFile(string[] lines, ref Dictionary<string, int> mapping)
        {
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                int colonIndex = line.IndexOf(':');
                if (colonIndex > 0 && colonIndex < line.Length - 1)
                {
                    string key = line.Substring(0, colonIndex).Trim();
                    string valueStr = line.Substring(colonIndex + 1).Trim();

                    if (int.TryParse(valueStr, out int id))
                    {
                        if (!string.IsNullOrEmpty(key))
                        {
                            mapping.TryAdd(key, id);
                        }
                    }
                    else
                    {
                        Debug.LogWarning($"Invalid face expression mapping value (not an integer): {line}");
                    }
                }
                else
                {
                    Debug.LogWarning($"Invalid mapping line: {line}");
                }
            }
        }

        /// <summary>
        /// Processes character file lines to populate a character mapping dictionary.
        /// Parses each line of the file to extract key-value pairs representing character names and their corresponding integer IDs.
        /// </summary>
        /// <param name="lines">An array of strings representing lines from the character mapping file.</param>
        /// <param name="mapping">A reference to the dictionary where the parsed mappings will be stored.</param>
        private static void ProcessCharacterFile(string[] lines, ref Dictionary<string, int> mapping)
        {
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                int colonIndex = line.IndexOf(':');
                if (colonIndex > 0 && colonIndex < line.Length - 1)
                {
                    string key = line.Substring(0, colonIndex).Trim();
                    string valueStr = line.Substring(colonIndex + 1).Trim();

                    if (int.TryParse(valueStr, out int id))
                    {
                        if (!string.IsNullOrEmpty(key))
                        {
                            mapping.TryAdd(key, id);
                        }
                    }
                    else
                    {
                        Debug.LogWarning($"Invalid character mapping value (not an integer): {line}");
                    }
                }
                else
                {
                    Debug.LogWarning($"Invalid mapping line: {line}");
                }
            }
        }

        /// <summary>
        /// Maps a given English bias term to its corresponding German translation using the loaded dictionary.
        /// If the mapping is not found, returns the original English bias as a fallback.
        /// </summary>
        /// <param name="englishBias">The bias term in English to be translated.</param>
        /// <returns>The corresponding German translation if found; otherwise, the original English bias.</returns>
        public static string MapBias(string englishBias)
        {
            if (_biasMapping.TryGetValue(englishBias, out string germanBias))
            {
                return germanBias;
            }

            Debug.LogWarning($"Bias mapping not found for: {englishBias}");
            return englishBias; // Fallback to the original if no mapping is found
        }

        /// <summary>
        /// Maps a given face expression string to its corresponding integer value based on the loaded mapping dictionary.
        /// Returns a fallback value if the mapping is not found or if the input is null/empty.
        /// </summary>
        /// <param name="faceExpression">The face expression string to be mapped.</param>
        /// <returns>The integer value corresponding to the face expression, or -1 if the mapping does not exist.</returns>
        public static int MapFaceExpressions(string faceExpression)
        {
            if (string.IsNullOrEmpty(faceExpression))
            {
                return -1;
            }

            if (_faceExpressionMapping.TryGetValue(faceExpression, out int id))
            {
                return id;
            }

            Debug.LogWarning($"Face expression mapping not found for: {faceExpression}");
            return -1; // Fallback value if no mapping is found
        }

        /// <summary>
        /// Maps a specified character name to its corresponding integer identifier.
        /// If the character is not found in the mapping, a fallback value of -1 is returned.
        /// Logs a warning if the character's mapping is not found.
        /// </summary>
        /// <param name="character">The name of the character to map.</param>
        /// <returns>The integer identifier of the mapped character if it exists; otherwise, -1.</returns>
        public static int MapCharacter(string character)
        {
            if (string.IsNullOrEmpty(character))
            {
                return -1;
            }

            if (_characterMapping.TryGetValue(character, out int id))
            {
                return id;
            }

            Debug.LogWarning("Character mapping not found for: " + character);
            return -1; // Fallback value if no mapping is found
        }

        /// <summary>
        /// Maps a character's numeric identifier to its corresponding string representation.
        /// </summary>
        /// <param name="character">The numeric identifier of the character to map.</param>
        /// <returns>The string representation of the character if found; otherwise, an empty string.</returns>
        public static string MapCharacterToString(int character)
        {
            return character == -1 ? "" : _characterMapping.FirstOrDefault(x => x.Value == character).Key;
        }
    }
}
