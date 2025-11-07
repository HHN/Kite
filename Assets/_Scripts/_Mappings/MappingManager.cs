using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets._Scripts.Biases;
using Assets._Scripts.Utilities;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

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
        
        public static readonly Dictionary<string, Bias> BIASES = new(StringComparer.OrdinalIgnoreCase);

        // Mapping-Files
        private static readonly string MappingFileFaceExpression;
        private static readonly string MappingFileCharacter;
        private static readonly string MappingFileSound;
        
        // Dictionaries
        private static Dictionary<string, int> _faceExpressionMapping = new(StringComparer.OrdinalIgnoreCase);
        private static Dictionary<string, int> _characterMapping = new(StringComparer.OrdinalIgnoreCase);
        private static Dictionary<string, int> _soundMapping = new(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Provides functionality to manage and load mappings for biases, face expressions, and character data.
        /// </summary>
        static MappingManager()
        {
            MappingFileFaceExpression = Path.Combine(Application.streamingAssetsPath, "FaceExpressionMapping.txt");
            MappingFileCharacter = Path.Combine(Application.streamingAssetsPath, "CharacterMapping.txt");
            MappingFileSound = Path.Combine(Application.streamingAssetsPath, "SoundMapping.txt");

            LoadBiasesFromJson();
            LoadFaceExpressionMappingAsync();
            LoadCharacterMappingAsync();
            LoadSoundMappingAsync();
        }

        /// <summary>Singleton</summary>
        public static MappingManager Instance
        {
            get
            {
                if (_instance == null)
                {
#if UNITY_2023_1_OR_NEWER
                    _instance = FindAnyObjectByType<MappingManager>();
#else
                    _instance = FindObjectOfType<MappingManager>();
#endif
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
        /// Asynchronously loads the face expressions mapping data from the appropriate source based on the target platform.
        /// For WebGL, the file is accessed via a UnityWebRequest, while for other platforms the file is read directly from disk.
        /// On a successful load, the file content is processed to populate the bias mapping dictionary.
        /// Logs warnings or errors when the file is not found or cannot be loaded.
        /// </summary>
        private static void LoadFaceExpressionMappingAsync()
        {
#if UNITY_WEBGL
            UnityWebRequest www = UnityWebRequest.Get(MappingFileFaceExpression);
            www.SendWebRequest().completed += _ =>
            {
                if (www.result == UnityWebRequest.Result.Success)
                {
                    string[] lines = www.downloadHandler.text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    ProcessFaceExpressionFile(lines, ref _faceExpressionMapping);
                }
                else
                {
                    Application.ExternalCall("logMessage", "Error loading face expression mapping: " + www.error);
                }
            };
#else
            var filePath = MappingFileFaceExpression;
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                ProcessFaceExpressionFile(lines, ref _faceExpressionMapping);
            }
            else
            {
                LogManager.Warning($"Face expression mapping file not found at: {filePath}");
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
            UnityWebRequest www = UnityWebRequest.Get(MappingFileCharacter);
            www.SendWebRequest().completed += _ =>
            {
                if (www.result == UnityWebRequest.Result.Success)
                {
                    string[] lines = www.downloadHandler.text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    ProcessCharacterFile(lines, ref _characterMapping);
                }
                else
                {
                    Application.ExternalCall("logMessage", "Error loading character mapping: " + www.error);
                }
            };
#else
            var filePath = MappingFileCharacter;
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                ProcessCharacterFile(lines, ref _characterMapping);
            }
            else
            {
                LogManager.Warning($"Character mapping file not found at: {filePath}");
            }
#endif
        }

        // NEU: Sound-Mapping laden
        private static void LoadSoundMappingAsync()
        {
#if UNITY_WEBGL
            UnityWebRequest www = UnityWebRequest.Get(MappingFileSound);
            www.SendWebRequest().completed += _ =>
            {
                if (www.result == UnityWebRequest.Result.Success)
                {
                    string[] lines = www.downloadHandler.text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    ProcessSoundFile(lines, ref _soundMapping);
                }
                else
                {
                    Application.ExternalCall("logMessage", "Error loading sound mapping: " + www.error);
                }
            };
#else
            var filePath = MappingFileSound;
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                ProcessSoundFile(lines, ref _soundMapping);
            }
            else
            {
                LogManager.Warning($"Sound mapping file not found at: {filePath}");
            }
#endif
        }

// ----------------- BIASES (JSON) -----------------

        /// <summary>
        /// Loads bias data from a JSON file and populates the internal dictionary of biases.
        /// This method is responsible for parsing the JSON, mapping the bias types,
        /// and logging any errors or warnings for invalid or unknown bias types.
        /// </summary>
        private static void LoadBiasesFromJson()
        {
            TextAsset json = Resources.Load<TextAsset>("KnowledgeBase");
            if (json == null)
            {
                LogManager.Error("Bias JSON not found in Resources!");
                return;
            }

            var wrapper = JsonUtility.FromJson<BiasJsonWrapper>(json.text);

            BIASES.Clear();

            foreach (var item in wrapper.items)
            {
                if (string.IsNullOrWhiteSpace(item.type))
                {
                    LogManager.Warning("Bias ohne gültigen Key gefunden, wird übersprungen.");
                    continue;
                }
                
                string key = ExtractBiasKey(item.type);
                if (!string.IsNullOrEmpty(key))
                {
                    BIASES[key] = new Bias
                    {
                        type = key,
                        category = item.category,
                        headline = item.headline,
                        preview = item.preview,
                        description = item.description
                    };
                }
                else
                {
                    LogManager.Warning($"Ungültiger Bias-Key: {item.type}");
                }
            }
        }

        /// <summary>
        /// Extracts the specific key value from a raw string input that is formatted with a predefined prefix and suffix syntax.
        /// Returns the extracted key if the input matches the expected format, otherwise null.
        /// </summary>
        /// <param name="raw">The raw input string containing the key with a specific prefix and suffix.</param>
        /// <returns>The extracted key if the input is valid and follows the expected format; otherwise, null.</returns>
        private static string ExtractBiasKey(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                return null;

            const string prefix = ">>Bias|";
            const string suffix = "<<";

            if (raw.StartsWith(prefix) && raw.EndsWith(suffix))
                return raw.Substring(prefix.Length, raw.Length - prefix.Length - suffix.Length).Trim();

            return null;
        }

        /// <summary>
        /// Processes the face expression mapping file lines and populates the given dictionary with mappings.
        /// Each line is expected to have a type-value pair in the format "type:value".
        /// Invalid or malformed lines are ignored, and warnings are logged for invalid entries.
        /// </summary>
        /// <param name="lines">An array of strings representing lines from the face expression mapping file.</param>
        /// <param name="mapping">A reference to the dictionary where the mappings will be stored. Keys are strings, and values are integers.</param>
        private static void ProcessFaceExpressionFile(string[] lines, ref Dictionary<string, int> mapping)
        {
            foreach (string raw in lines)
            {
                if (string.IsNullOrWhiteSpace(raw)) continue;

                string line = raw.Trim();
                int colonIndex = line.IndexOf(':');
                if (colonIndex > 0 && colonIndex < line.Length - 1)
                {
                    string key = line.Substring(0, colonIndex).Trim();
                    string valueStr = line.Substring(colonIndex + 1).Trim();

                    if (int.TryParse(valueStr, out int id) && !string.IsNullOrEmpty(key))
                    {
                        mapping[key] = id; // Dictionary ist case-insensitive
                    }
                    else
                    {
                        LogManager.Warning($"Invalid face expression mapping value (not an integer): {line}");
                    }
                }
                else
                {
                    LogManager.Warning($"Invalid mapping line: {line}");
                }
            }
        }

        /// <summary>
        /// Processes character file lines to populate a character mapping dictionary.
        /// Parses each line of the file to extract type-value pairs representing character names and their corresponding integer IDs.
        /// </summary>
        /// <param name="lines">An array of strings representing lines from the character mapping file.</param>
        /// <param name="mapping">A reference to the dictionary where the parsed mappings will be stored.</param>
        private static void ProcessCharacterFile(string[] lines, ref Dictionary<string, int> mapping)
        {
            foreach (string raw in lines)
            {
                if (string.IsNullOrWhiteSpace(raw)) continue;

                string line = raw.Trim();
                int colonIndex = line.IndexOf(':');
                if (colonIndex > 0 && colonIndex < line.Length - 1)
                {
                    string key = line.Substring(0, colonIndex).Trim();
                    string valueStr = line.Substring(colonIndex + 1).Trim();

                    if (int.TryParse(valueStr, out int id) && !string.IsNullOrEmpty(key))
                    {
                        mapping[key] = id; // case-insensitive
                    }
                    else
                    {
                        LogManager.Warning($"Invalid character mapping value (not an integer): {line}");
                    }
                }
                else
                {
                    LogManager.Warning($"Invalid mapping line: {line}");
                }
            }
        }

        private static void ProcessSoundFile(string[] lines, ref Dictionary<string, int> mapping)
        {
            foreach (string raw in lines)
            {
                if (string.IsNullOrWhiteSpace(raw)) continue;

                string line = raw.Trim();
                int colonIndex = line.IndexOf(':');
                if (colonIndex > 0 && colonIndex < line.Length - 1)
                {
                    string key = line.Substring(0, colonIndex).Trim();
                    string valueStr = line.Substring(colonIndex + 1).Trim();

                    if (int.TryParse(valueStr, out int id) && !string.IsNullOrEmpty(key))
                    {
                        mapping[key] = id; // case-insensitive
                    }
                    else
                    {
                        LogManager.Warning($"Invalid sound mapping value (not an integer): {line}");
                    }
                }
                else
                {
                    LogManager.Warning($"Invalid mapping line: {line}");
                }
            }
        }

        // ----------------- PUBLIC API -----------------

        /// <summary>
        /// Maps a given type to its corresponding bias headline using the predefined bias mappings.
        /// If the type is not found, the original type is returned, and a warning is logged.
        /// </summary>
        /// <param name="key">The type representing the bias to be mapped.</param>
        /// <returns>
        /// The mapped bias headline if the type exists in the mapping;
        /// otherwise, the original type if no mapping is found.
        /// </returns>
        public static string MapBias(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return key;

            if (BIASES.TryGetValue(key, out Bias bias))
            {
                return bias.headline;
            }

            LogManager.Warning($"Bias mapping not found for type: {key}");
            return key;
        }

        /// <summary>
        /// Retrieves a dictionary containing all biases where the keys are bias names represented as strings
        /// and the values are Bias objects that encapsulate detailed information about each bias.
        /// </summary>
        /// <returns>
        /// A dictionary mapping string keys to Bias objects, containing all available biases in the system.
        /// </returns>
        public static Dictionary<string, Bias> GetAllBiases()
        {
            return BIASES;
        }

        /// <summary>
        /// Maps a given face expression string to its corresponding integer value based on the loaded mapping dictionary.
        /// Returns a fallback value if the mapping is not found or if the input is null/empty.
        /// </summary>
        /// <param name="faceExpression">The face expression string to be mapped.</param>
        /// <returns>The integer value corresponding to the face expression, or -1 if the mapping does not exist.</returns>
        public static int MapFaceExpressions(string faceExpression)
        {
            if (string.IsNullOrWhiteSpace(faceExpression)) return -1;

            var key = faceExpression.Trim();
            return _faceExpressionMapping.GetValueOrDefault(key, -1);
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
            if (string.IsNullOrWhiteSpace(character)) return -1;

            var key = character.Trim();
            return _characterMapping.GetValueOrDefault(key, -1);
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
        
        /// <summary>
        /// Liest die NovelMapping.txt (StreamingAssets/NovelMapping.txt) ein
        /// und liefert die Zuordnung Ordnername → Novel-ID.
        /// Im Editor/Standalone synchron, unter WebGL asynchron via UnityWebRequest.
        /// </summary>
        public IEnumerator LoadNovelMapping(Action<Dictionary<string,int>> onComplete)
        {
            var mappingPath = Path.Combine(Application.streamingAssetsPath, "NovelMapping.txt");

#if UNITY_WEBGL
            using var www = UnityWebRequest.Get(mappingPath);
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"LoadNovelMapping: Fehler beim Laden von {mappingPath}: {www.error}");
                onComplete?.Invoke(new Dictionary<string,int>(StringComparer.OrdinalIgnoreCase));
                yield break;
            }

            var lines = www.downloadHandler.text
                .Split(new[]{'\r','\n'}, StringSplitOptions.RemoveEmptyEntries);
#else
            if (!File.Exists(mappingPath))
            {
                Debug.LogError($"LoadNovelMapping: Datei nicht gefunden: {mappingPath}");
                onComplete?.Invoke(new Dictionary<string,int>(StringComparer.OrdinalIgnoreCase));
                yield break;
            }
            var lines = File.ReadAllLines(mappingPath);
#endif

            var dict = new Dictionary<string,int>(StringComparer.OrdinalIgnoreCase);
            foreach (var raw in lines)
            {
                var line = raw.Trim();
                if (line.Length == 0 || !line.Contains(':'))
                    continue;

                var parts = line.Split(new[]{':'}, 2);
                var key = parts[0].Trim();
                if (int.TryParse(parts[1].Trim(), out var id))
                    dict[key] = id;
                else
                    Debug.LogWarning($"LoadNovelMapping: Ungültige ID in Zeile: {line}");
            }

            onComplete?.Invoke(dict);
        }
    }
}