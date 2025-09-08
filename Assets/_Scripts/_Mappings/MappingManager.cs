using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets._Scripts.Biases;
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
        private static readonly string MappingFileFaceExpression;
        private static readonly string MappingFileCharacter;
        private static readonly string MappingFileSound; // NEU

        // Dictionaries
        private static Dictionary<BiasType, Bias> _biases = new Dictionary<BiasType, Bias>();
        private static Dictionary<string, int> _faceExpressionMapping = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        private static Dictionary<string, int> _characterMapping      = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        private static Dictionary<string, int> _soundMapping          = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase); // NEU

        /// <summary>
        /// Provides functionality to manage and load mappings for biases, face expressions, and character data.
        /// </summary>
        static MappingManager()
        {
            // StreamingAssets-Pfade
            MappingFileFaceExpression = Path.Combine(Application.streamingAssetsPath, "FaceExpressionMapping.txt");
            MappingFileCharacter      = Path.Combine(Application.streamingAssetsPath, "CharacterMapping.txt");
            MappingFileSound          = Path.Combine(Application.streamingAssetsPath, "SoundMapping.txt"); // NEU

            LoadBiasesFromJson();
            LoadFaceExpressionMappingAsync();
            LoadCharacterMappingAsync();
            LoadSoundMappingAsync(); // NEU
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
                        var obj = new GameObject("MappingManager");
                        _instance = obj.AddComponent<MappingManager>();
                        DontDestroyOnLoad(obj);
                    }
                }
                return _instance;
            }
        }

        // ----------------- LOADERS -----------------

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
                Debug.LogWarning($"Face expression mapping file not found at: {filePath}");
            }
#endif
        }

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
                Debug.LogWarning($"Character mapping file not found at: {filePath}");
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
                Debug.LogWarning($"Sound mapping file not found at: {filePath}");
            }
#endif
        }

        // ----------------- BIASES (JSON) -----------------

        /// <summary>
        /// Loads bias data from a JSON file in Resources/KnowledgeBase.json (TextAsset "KnowledgeBase").
        /// </summary>
        private static void LoadBiasesFromJson()
        {
            TextAsset json = Resources.Load<TextAsset>("KnowledgeBase");
            if (json == null)
            {
                Debug.LogError("Bias JSON not found in Resources!");
                return;
            }

            var wrapper = JsonUtility.FromJson<BiasJsonWrapper>(json.text);
            _biases = new Dictionary<BiasType, Bias>();

            foreach (var item in wrapper.items)
            {
                string typeStr = item.type ?? string.Empty;

                // Optionales Präfix im JSON tolerieren, case-insensitive
                if (typeStr.StartsWith(">>Bias|", StringComparison.OrdinalIgnoreCase) && typeStr.EndsWith("<<", StringComparison.Ordinal))
                {
                    typeStr = typeStr.Substring(7, typeStr.Length - 9).Trim();
                }

                if (Enum.TryParse(typeStr, ignoreCase: true, out BiasType biasType))
                {
                    _biases[biasType] = new Bias
                    {
                        biasType = biasType,
                        headline = item.headline,
                        preview = item.preview,
                        description = item.description
                    };
                }
                else
                {
                    Debug.LogWarning($"Unknown BiasType in JSON: {typeStr}");
                }
            }
        }

        // ----------------- PARSER -----------------

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
                        Debug.LogWarning($"Invalid face expression mapping value (not an integer): {line}");
                    }
                }
                else
                {
                    Debug.LogWarning($"Invalid mapping line: {line}");
                }
            }
        }

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
                        Debug.LogWarning($"Invalid character mapping value (not an integer): {line}");
                    }
                }
                else
                {
                    Debug.LogWarning($"Invalid mapping line: {line}");
                }
            }
        }

        // NEU: Parser für SoundMapping.txt
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
                        Debug.LogWarning($"Invalid sound mapping value (not an integer): {line}");
                    }
                }
                else
                {
                    Debug.LogWarning($"Invalid mapping line: {line}");
                }
            }
        }

        // ----------------- PUBLIC API -----------------

        /// <summary>
        /// Maps a bias type represented by a string to its corresponding headline if it exists.
        /// </summary>
        public static string MapBias(string typeString)
        {
            if (string.IsNullOrWhiteSpace(typeString)) return typeString;

            // Enum Parse case-insensitive
            if (Enum.TryParse(typeString.Trim(), ignoreCase: true, out BiasType type))
            {
                if (_biases.TryGetValue(type, out Bias bias))
                {
                    return bias.headline;
                }
                Debug.LogWarning($"Bias mapping not found for enum value: {type}");
                return type.ToString();
            }

            Debug.LogWarning($"Invalid BiasType string: {typeString}");
            return typeString;
        }

        /// <summary>Returns all biases.</summary>
        public static Dictionary<BiasType, Bias> GetAllBiases() => _biases;

        /// <summary>
        /// Case-insensitive mapping of face expression → int id.
        /// </summary>
        public static int MapFaceExpressions(string faceExpression)
        {
            if (string.IsNullOrWhiteSpace(faceExpression)) return -1;

            var key = faceExpression.Trim();
            return _faceExpressionMapping.TryGetValue(key, out int id) ? id : -1;
        }

        /// <summary>
        /// Case-insensitive mapping of character name → int id.
        /// </summary>
        public static int MapCharacter(string character)
        {
            if (string.IsNullOrWhiteSpace(character)) return -1;

            var key = character.Trim();
            return _characterMapping.TryGetValue(key, out int id) ? id : -1;
        }

        /// <summary>
        /// Map numeric character id → name (first match).
        /// </summary>
        public static string MapCharacterToString(int character)
        {
            return character == -1 ? "" : _characterMapping.FirstOrDefault(x => x.Value == character).Key;
        }

        /// <summary>
        /// Case-insensitive mapping of sound clip name → int index (from StreamingAssets/SoundMapping.txt).
        /// </summary>
        public static int MapSound(string clipName)
        {
            if (string.IsNullOrWhiteSpace(clipName)) return -1;

            var key = clipName.Trim();
            return _soundMapping.TryGetValue(key, out int id) ? id : -1;
        }
    }
}
