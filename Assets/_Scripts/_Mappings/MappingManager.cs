using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking; // Add the UnityWebRequest namespace

namespace Assets._Scripts._Mappings
{
    public class MappingManager : MonoBehaviour
    {
        private static MappingManager _instance;

        // Mapping-Files - Different paths for Unity Editor and WebGL
        private static readonly string MappingFileBias;
        private static readonly string MappingFileFaceExpression;
        private static readonly string MappingFileCharacter;

        // Dictionaries to store the mappings for bias, face expression, and characters
        private static Dictionary<string, string> _biasMapping = new Dictionary<string, string>();
        private static Dictionary<string, int> _faceExpressionMapping = new Dictionary<string, int>();
        private static Dictionary<string, int> _characterMapping = new Dictionary<string, int>();

        // Static constructor to determine file paths based on platform (WebGL vs Editor/Standalone)
        static MappingManager()
        {
#if UNITY_WEBGL
            // For WebGL, we use the StreamingAssets folder, but WebGL files need to be accessed asynchronously
            MappingFileBias = Path.Combine(Application.streamingAssetsPath, "BiasMapping.txt");
            MappingFileFaceExpression = Path.Combine(Application.streamingAssetsPath, "FaceExpressionMapping.txt");
            MappingFileCharacter = Path.Combine(Application.streamingAssetsPath, "CharacterMapping.txt");
#else
            // For Unity Editor and other platforms, use the original path
            MappingFileBias = Path.Combine(Application.dataPath, "_Scripts/_Mappings/BiasMapping.txt");
            MappingFileFaceExpression = Path.Combine(Application.dataPath, "_Scripts/_Mappings/FaceExpressionMapping.txt");
            MappingFileCharacter = Path.Combine(Application.dataPath, "_Scripts/_Mappings/CharacterMapping.txt");
#endif
            LoadBiasMappingAsync();
            LoadFaceExpressionMappingAsync();
            LoadCharacterMappingAsync();
        }

        // Singleton pattern to ensure only one instance of MappingManager exists
        public static MappingManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<MappingManager>();
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

        // Loads the Bias mapping from the corresponding file
        private static void LoadBiasMappingAsync()
        {
            string filePath = MappingFileBias;

#if UNITY_WEBGL
            // For WebGL, use UnityWebRequest to load the file asynchronously
            UnityWebRequest www = UnityWebRequest.Get(MappingFileBias);
            www.SendWebRequest().completed += (asyncOperation) =>
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
            // For other platforms, read directly from file system
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                ProcessMappingFile(lines, ref _biasMapping);
            }
            else
            {
                Debug.LogWarning("Bias mapping file not found at: " + filePath);
            }
#endif
        }

        // Loads the FaceExpression mapping from the corresponding file
        private static void LoadFaceExpressionMappingAsync()
        {
            string filePath = MappingFileFaceExpression;

#if UNITY_WEBGL
            UnityWebRequest www = UnityWebRequest.Get(MappingFileFaceExpression);
            www.SendWebRequest().completed += (asyncOperation) =>
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
                Debug.LogWarning("Face expression mapping file not found at: " + filePath);
            }
#endif
        }

        // Loads the Character mapping from the corresponding file
        private static void LoadCharacterMappingAsync()
        {
            string filePath = MappingFileCharacter;

#if UNITY_WEBGL
            UnityWebRequest www = UnityWebRequest.Get(MappingFileCharacter);
            www.SendWebRequest().completed += (asyncOperation) =>
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
                Debug.LogWarning("Character mapping file not found at: " + filePath);
            }
#endif
        }

        // Helper method to process mapping file (for Bias, FaceExpression, and Character mappings)
        private static void ProcessMappingFile(string[] lines, ref Dictionary<string, string> mapping)
        {
            int addedPairsCount = 0; // Zähler für hinzugefügte Paare.

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue; // Leere Zeilen überspringen.
                }

                int colonIndex = line.IndexOf(':');
                if (colonIndex > 0 && colonIndex < line.Length - 1)
                {
                    string key = line.Substring(0, colonIndex).Trim();
                    string value = line.Substring(colonIndex + 1).Trim();

                    if (!string.IsNullOrEmpty(key) && !mapping.ContainsKey(key))
                    {
                        mapping.Add(key, value);
                        addedPairsCount++; // Zähler erhöhen, wenn ein neues Paar hinzugefügt wird.
                    }
                }
                else
                {
                    Debug.LogWarning("Invalid mapping line (missing colon): " + line);
                }
            }
        }


        // Helper method to process FaceExpression mapping file
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
                        if (!string.IsNullOrEmpty(key) && !mapping.ContainsKey(key))
                        {
                            mapping.Add(key, id);
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Invalid face expression mapping value (not an integer): " + line);
                    }
                }
                else
                {
                    Debug.LogWarning("Invalid mapping line: " + line);
                }
            }
        }

        // Helper method to process Character mapping file
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
                        if (!string.IsNullOrEmpty(key) && !mapping.ContainsKey(key))
                        {
                            mapping.Add(key, id);
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Invalid character mapping value (not an integer): " + line);
                    }
                }
                else
                {
                    Debug.LogWarning("Invalid mapping line: " + line);
                }
            }
        }

        // Maps a given bias (in English) to its German translation from the loaded dictionary
        public static string MapBias(string englishBias)
        {
            if (_biasMapping.TryGetValue(englishBias, out string germanBias))
            {
                return germanBias;
            }

            Debug.LogWarning("Bias mapping not found for: " + englishBias);
            return englishBias; // Fallback to original if no mapping is found
        }

        // Maps a given face expression string to its corresponding integer value from the loaded dictionary
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

            Debug.LogWarning("Face expression mapping not found for: " + faceExpression);
            return -1; // Fallback value if no mapping is found
        }

        // Maps a given character string to its corresponding integer value from the loaded dictionary
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

        // Maps a given character integer value to its corresponding character string from the loaded dictionary
        public static string MapCharacterToString(int character)
        {
            if (character == -1)
            {
                return "";
            }
            return _characterMapping.FirstOrDefault(x => x.Value == character).Key;
        }
    }
}
