using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Assets._Scripts._Mappings
{
    public class MappingManager : MonoBehaviour
    {
        private static MappingManager _instance;

        // Mapping-Files
        private static readonly string MappingFileBias = Path.Combine(Application.dataPath, "_Scripts/_Mappings/BiasMapping.txt");
        private static readonly string MappingFileFaceExpression = Path.Combine(Application.dataPath, "_Scripts/_Mappings/FaceExpressionMapping.txt");
        private static readonly string MappingFileCharacter = Path.Combine(Application.dataPath, "_Scripts/_Mappings/CharacterMapping.txt");

        // Dictionaries
        private static Dictionary<string, string> _biasMapping = LoadBiasMapping();
        private static Dictionary<string, int> _faceExpressionMapping = LoadFaceExpressionMapping();
        private static Dictionary<string, int> _characterMapping = LoadCharacterMapping();

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

        private static Dictionary<string, string> LoadBiasMapping()
        {
            Dictionary<string, string> mapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            try
            {
                // Verwende den bereits vollst�ndig definierten MappingFileFullPath.
                if (!File.Exists(MappingFileBias))
                {
                    Debug.LogWarning("Bias mapping file not found at: " + MappingFileBias);
                    return mapping; // Leeres Mapping zur�ckgeben.
                }

                // Lese alle Zeilen der Datei.
                string[] lines = File.ReadAllLines(MappingFileBias);
                foreach (string line in lines)
                {
                    // �berspringe leere oder nur aus Leerzeichen bestehende Zeilen.
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Suche nach dem ersten Doppelpunkt als Trenner.
                    int colonIndex = line.IndexOf(':');
                    if (colonIndex > 0 && colonIndex < line.Length - 1)
                    {
                        // Extrahiere den englischen Bias (Key) und den deutschen Bias (Value).
                        string key = line.Substring(0, colonIndex).Trim();
                        string value = line.Substring(colonIndex + 1).Trim();
                        if (!string.IsNullOrEmpty(key) && !mapping.ContainsKey(key))
                        {
                            mapping.Add(key, value);
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Invalid mapping line: " + line);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error loading bias mapping: " + ex.Message);
            }

            return mapping;
        }

        /// <summary>
        /// Liest das FaceExpression-Mapping aus einer externen Textdatei ein.
        /// Das Mapping-File muss Zeilen im Format "[FaceExpression]:[Id]" enthalten.
        /// </summary>
        /// <returns>Ein Dictionary, das den FaceExpression-String (Key) auf den zugeh�rigen Integer-Wert (Value) abbildet.</returns>
        private static Dictionary<string, int> LoadFaceExpressionMapping()
        {
            Dictionary<string, int> mapping = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            try
            {
                // Pr�fe, ob die Mapping-Datei existiert.
                if (!File.Exists(MappingFileFaceExpression))
                {
                    Debug.LogWarning("Face expression mapping file not found at: " + MappingFileFaceExpression);
                    return mapping; // Gib ein leeres Mapping zur�ck.
                }

                // Lese alle Zeilen der Datei.
                string[] lines = File.ReadAllLines(MappingFileFaceExpression);
                foreach (string line in lines)
                {
                    // �berspringe leere oder nur aus Leerzeichen bestehende Zeilen.
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Suche nach dem ersten Doppelpunkt als Trenner.
                    int colonIndex = line.IndexOf(':');
                    if (colonIndex > 0 && colonIndex < line.Length - 1)
                    {
                        // Extrahiere den FaceExpression-String (Key) und den dazugeh�rigen Wert als String.
                        string key = line.Substring(0, colonIndex).Trim();
                        string valueStr = line.Substring(colonIndex + 1).Trim();

                        // Versuche, den Wert in einen Integer zu parsen.
                        if (int.TryParse(valueStr, out int id))
                        {
                            // F�ge den Key und den geparsten Wert dem Dictionary hinzu, sofern der Key nicht leer ist
                            // und noch nicht im Dictionary enthalten ist.
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
            catch (Exception ex)
            {
                Debug.LogError("Error loading face expression mapping: " + ex.Message);
            }

            return mapping;
        }

        /// <summary>
        /// Liest das Character-Mapping aus einer externen Textdatei ein.
        /// Das Mapping-File muss Zeilen im Format "[Character]:[Id]" enthalten.
        /// </summary>
        /// <returns>Ein Dictionary, das den Character-String (Key) auf den zugeh�rigen Integer-Wert (Value) abbildet.</returns>
        private static Dictionary<string, int> LoadCharacterMapping()
        {
            Dictionary<string, int> mapping = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            try
            {
                // Pr�fe, ob die Mapping-Datei existiert.
                if (!File.Exists(MappingFileCharacter))
                {
                    Debug.LogWarning("Character mapping file not found at: " + MappingFileCharacter);
                    return mapping; // Gib ein leeres Mapping zur�ck.
                }

                // Lese alle Zeilen der Datei.
                string[] lines = File.ReadAllLines(MappingFileCharacter);
                foreach (string line in lines)
                {
                    // �berspringe leere oder nur aus Leerzeichen bestehende Zeilen.
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Suche nach dem ersten Doppelpunkt als Trenner.
                    int colonIndex = line.IndexOf(':');
                    if (colonIndex > 0 && colonIndex < line.Length - 1)
                    {
                        // Extrahiere den FaceExpression-String (Key) und den dazugeh�rigen Wert als String.
                        string key = line.Substring(0, colonIndex).Trim();
                        string valueStr = line.Substring(colonIndex + 1).Trim();

                        // Versuche, den Wert in einen Integer zu parsen.
                        if (int.TryParse(valueStr, out int id))
                        {
                            // F�ge den Key und den geparsten Wert dem Dictionary hinzu, sofern der Key nicht leer ist
                            // und noch nicht im Dictionary enthalten ist.
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
            catch (Exception ex)
            {
                Debug.LogError("Error loading character mapping: " + ex.Message);
            }

            return mapping;
        }

        /// <summary>
        /// Internal mapping method � looks up the English bias in the dictionary and returns the German translation.
        /// </summary>
        public static string MapBias(string englishBias)
        {
            if (_biasMapping.TryGetValue(englishBias, out string germanBias))
            {
                return germanBias;
            }

            Debug.LogWarning("Bias mapping not found for: " + englishBias);
            return englishBias; // Fallback to original.
        }

        /// <summary>
        /// �bersetzt den �bergebenen FaceExpression-String in seinen zugeh�rigen Integer-Wert anhand des geladenen Mappings.
        /// Falls kein Mapping gefunden wird, wird -1 zur�ckgegeben.
        /// </summary>
        /// <param name="faceExpression">Der FaceExpression-String, der abgebildet werden soll.</param>
        /// <returns>Den Integer-Wert, der dem FaceExpression zugeordnet ist, oder -1, wenn nichts gefunden wurde.</returns>
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
            return -1; // Fallback-Wert, falls der FaceExpression-String nicht gefunden wird.
        }

        /// <summary>
        /// �bersetzt den �bergebenen Character-String in seinen zugeh�rigen Integer-Wert anhand des geladenen Mappings.
        /// Falls kein Mapping gefunden wird, wird -1 zur�ckgegeben.
        /// </summary>
        /// <param name="character">Der Character-String, der abgebildet werden soll.</param>
        /// <returns>Den Integer-Wert, der dem Character zugeordnet ist, oder -1, wenn nichts gefunden wurde.</returns>
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
            return -1; // Fallback-Wert, falls der Character-String nicht gefunden wird.
        }

        /// <summary>
        /// �bersetzt den �bergebenen Character-String in seinen zugeh�rigen Integer-Wert anhand des geladenen Mappings.
        /// Falls kein Mapping gefunden wird, wird -1 zur�ckgegeben.
        /// </summary>
        /// <param name="character">Der Character-String, der abgebildet werden soll.</param>
        /// <returns>Den Integer-Wert, der dem Character zugeordnet ist, oder -1, wenn nichts gefunden wurde.</returns>
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