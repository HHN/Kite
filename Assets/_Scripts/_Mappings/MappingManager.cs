using System;
using System.Collections;
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
        private static readonly string MappingFileNovels;
        private static readonly string MappingFileSound;

        // Dictionaries (ALLE case-insensitive)
        private static Dictionary<string, string> _biasMapping         = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private static Dictionary<string, int>    _faceExpressionMapping = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        private static Dictionary<string, int>    _characterMapping      = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        private static Dictionary<string, int>    _soundMapping          = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Provides functionality to manage and load mappings for biases, face expressions, and character data.
        /// This class uses platform-specific approaches to load mapping data from files and provides methods to map individual entries.
        /// </summary>
        static MappingManager()
        {
            // Für alle Plattformen: Pfade im StreamingAssets
            MappingFileBias            = Path.Combine(Application.streamingAssetsPath, "KnowledgeBase.txt");
            MappingFileFaceExpression  = Path.Combine(Application.streamingAssetsPath, "FaceExpressionMapping.txt");
            MappingFileCharacter       = Path.Combine(Application.streamingAssetsPath, "CharacterMapping.txt");
            MappingFileNovels          = Path.Combine(Application.streamingAssetsPath, "NovelMapping.txt");
            MappingFileSound           = Path.Combine(Application.streamingAssetsPath, "SoundMapping.txt");

            // Loader anstoßen
            LoadBiasMappingAsync();
            LoadFaceExpressionMappingAsync();
            LoadCharacterMappingAsync();
            LoadSoundMappingAsync();
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

        private static void LoadBiasMappingAsync()
        {
#if UNITY_WEBGL
            UnityWebRequest www = UnityWebRequest.Get(MappingFileBias);
            www.SendWebRequest().completed += _ =>
            {
                if (www.result == UnityWebRequest.Result.Success)
                {
                    string[] lines = www.downloadHandler.text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    ParseBiasMappingFromKnowledgeBase(lines, ref _biasMapping);
                }
                else
                {
                    Application.ExternalCall("logMessage", "Error loading bias mapping: " + www.error);
                }
            };
#else
            var filePath = MappingFileBias;
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                ParseBiasMappingFromKnowledgeBase(lines, ref _biasMapping);
            }
            else
            {
                Debug.LogWarning($"Bias mapping file not found at: {filePath}");
            }
#endif
        }

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

        // ---------- SOUND MAPPING ----------

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

                    if (int.TryParse(valueStr, out int id))
                    {
                        if (!string.IsNullOrEmpty(key))
                        {
                            mapping[key] = id; // case-insensitive dictionary
                        }
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

        public static int MapSound(string clipName)
        {
            if (string.IsNullOrWhiteSpace(clipName))
                return -1;

            var key = clipName.Trim();
            if (_soundMapping.TryGetValue(key, out int id))
                return id;

            Debug.LogWarning($"Sound mapping not found for: {clipName}");
            return -1;
        }

        // ---------- EXISTIERENDE Parser/Mapper ----------

        private static void ParseBiasMappingFromKnowledgeBase(string[] lines, ref Dictionary<string, string> mapping)
        {
            for (int i = 0; i < lines.Length - 1; i++)
            {
                string german = lines[i].Trim();
                string biasLine = lines[i + 1].Trim();

                if (biasLine.StartsWith(">>Bias|", StringComparison.OrdinalIgnoreCase) && // case-insensitive
                    biasLine.EndsWith("<<", StringComparison.Ordinal))
                {
                    string english = biasLine.Substring(7, biasLine.Length - 9).Trim(); // extrahiere Bias-Namen
                    if (!string.IsNullOrEmpty(english) && !string.IsNullOrEmpty(german))
                    {
                        mapping[english] = german; // Dictionary ist case-insensitive
                    }
                }
            }
        }

        private static void ProcessFaceExpressionFile(string[] lines, ref Dictionary<string, int> mapping)
        {
            foreach (string lineRaw in lines)
            {
                if (string.IsNullOrWhiteSpace(lineRaw)) continue;

                string line = lineRaw.Trim();
                int colonIndex = line.IndexOf(':');
                if (colonIndex > 0 && colonIndex < line.Length - 1)
                {
                    string key = line.Substring(0, colonIndex).Trim();
                    string valueStr = line.Substring(colonIndex + 1).Trim();

                    if (int.TryParse(valueStr, out int id))
                    {
                        if (!string.IsNullOrEmpty(key))
                        {
                            mapping[key] = id; // case-insensitive
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

        private static void ProcessCharacterFile(string[] lines, ref Dictionary<string, int> mapping)
        {
            foreach (string lineRaw in lines)
            {
                if (string.IsNullOrWhiteSpace(lineRaw)) continue;

                string line = lineRaw.Trim();
                int colonIndex = line.IndexOf(':');
                if (colonIndex > 0 && colonIndex < line.Length - 1)
                {
                    string key = line.Substring(0, colonIndex).Trim();
                    string valueStr = line.Substring(colonIndex + 1).Trim();

                    if (int.TryParse(valueStr, out int id))
                    {
                        if (!string.IsNullOrEmpty(key))
                        {
                            mapping[key] = id; // case-insensitive
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

        public static string MapBias(string englishBias)
        {
            if (string.IsNullOrWhiteSpace(englishBias))
                return englishBias;

            var key = englishBias.Trim();
            if (_biasMapping.TryGetValue(key, out string germanBias))
            {
                return germanBias;
            }

            Debug.LogWarning($"Bias mapping not found for: {englishBias}");
            return englishBias; // Fallback
        }

        public static int MapFaceExpressions(string faceExpression)
        {
            if (string.IsNullOrWhiteSpace(faceExpression))
            {
                return -1;
            }

            var key = faceExpression.Trim();
            if (_faceExpressionMapping.TryGetValue(key, out int id))
            {
                return id;
            }

            Debug.LogWarning($"Face expression mapping not found for: {faceExpression}");
            return -1; // Fallback
        }

        public static int MapCharacter(string character)
        {
            if (string.IsNullOrWhiteSpace(character))
            {
                return -1;
            }

            var key = character.Trim();
            if (_characterMapping.TryGetValue(key, out int id))
            {
                return id;
            }

            Debug.LogWarning("Character mapping not found for: " + character);
            return -1; // Fallback
        }

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
