using System.Collections.Generic;
using System.Text;

namespace Assets._Scripts.Managers
{
    public class PlayerDataManager
    {
        private static PlayerDataManager _instance;

        private Dictionary<string, string> _playerPrefs = new Dictionary<string, string>();

        private List<string> _keys = new List<string>();

        private List<string> _novelHistory = new List<string>();

        public static PlayerDataManager Instance()
        {
            if (_instance == null)
            {
                _instance = new PlayerDataManager();
            }

            return _instance;
        }

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

        public void SetNovelHistory(List<string> novelHistory)
        {
            _novelHistory = novelHistory;
        }

        public void SavePlayerData(string key, string content)
        {
            UnityEngine.PlayerPrefs.SetString(key, content);
            UnityEngine.PlayerPrefs.Save();
            AddKeyToKeyList(key);
        }

        private void AddKeyToKeyList(string key)
        {
            if (!_keys.Contains(key))
            {
                _keys.Add(key);
                SaveKeys();
            }
        }

        private void SaveKeys()
        {
            UnityEngine.PlayerPrefs.SetString("keys", string.Join(",", _keys));
            UnityEngine.PlayerPrefs.Save();
        }

        public void LoadAllPlayerPrefs()
        {
            if (_keys.Count == 0)
            {
                string keysString = UnityEngine.PlayerPrefs.GetString("keys", "");
                _keys = new List<string>(keysString.Split(','));
            }

            foreach (string playerPref in _keys)
            {
                // Überprüfen, ob der Key bereits existiert
                if (!_playerPrefs.ContainsKey(playerPref))
                {
                    _playerPrefs.Add(playerPref, ReadPlayerData(playerPref));
                }
                else
                {
                    _playerPrefs[playerPref] = ReadPlayerData(playerPref);
                    //Debug.Log("Updated key: " + playerPref);
                }
            }
        }


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

        public void SaveEvaluation(string novelName, string evaluation)
        {
            // Schlüssel für PlayerPrefs
            const string key = "evaluations";

            // Alte Auswertungen laden
            string existingEvaluations = UnityEngine.PlayerPrefs.GetString(key, "");
            string novelHistoryString = CombineSentences(_novelHistory);

            // Trennzeichen definieren
            const string mainSeparator = "|"; // Trennt verschiedene Auswertungen
            const string subSeparator = "~"; // Trennt Novels-Name von Ablauf und Bewertung
            const string flowSeparator = "^"; // Trennt Ablauf von der Bewertung

            // Neue Auswertung vorbereiten, indem der Name der Novels, der Ablauf und die Auswertung mit Trennzeichen verbunden werden
            string newEvaluation = novelName + subSeparator + novelHistoryString + flowSeparator + evaluation;

            // Neue Auswertung an bestehende anhängen
            if (!string.IsNullOrEmpty(existingEvaluations))
            {
                existingEvaluations += mainSeparator;
            }

            existingEvaluations += newEvaluation;

            // Alles zurück in PlayerPrefs speichern
            UnityEngine.PlayerPrefs.SetString(key, existingEvaluations);
            UnityEngine.PlayerPrefs.Save(); // Änderungen sichern
            //Debug.Log("Saved: " + existingEvaluations);
        }

        private string CombineSentences(List<string> sentences)
        {
            // Trennzeichen wählen, das sicher nicht in den Sätzen vorkommt
            const string separator = "##";

            // StringBuilder verwenden, um Effizienz beim Zusammenfügen der Strings zu gewährleisten
            StringBuilder combined = new StringBuilder();

            // Alle Sätze durchlaufen
            foreach (string sentence in sentences)
            {
                // Jeden Satz mit dem Trennzeichen anhängen
                combined.Append(sentence);
                combined.Append(separator);
            }

            // Das letzte Trennzeichen entfernen
            if (combined.Length > 0)
            {
                combined.Remove(combined.Length - separator.Length, separator.Length);
            }

            // Den zusammengefügten String zurückgeben
            return combined.ToString();
        }

        public void ClearEverything()
        {
            _playerPrefs = new Dictionary<string, string>();
            _keys = new List<string>();
            _novelHistory = new List<string>();
        }

        public void ClearRelevantUserdata()
        {
            if (UnityEngine.PlayerPrefs.HasKey("PlayerName"))
            {
                UnityEngine.PlayerPrefs.DeleteKey("PlayerName");
            }

            if (UnityEngine.PlayerPrefs.HasKey("CompanyName"))
            {
                UnityEngine.PlayerPrefs.DeleteKey("CompanyName");
            }

            if (UnityEngine.PlayerPrefs.HasKey("ElevatorPitch"))
            {
                UnityEngine.PlayerPrefs.DeleteKey("ElevatorPitch");
            }

            if (UnityEngine.PlayerPrefs.HasKey("Preferences"))
            {
                UnityEngine.PlayerPrefs.DeleteKey("Preferences");
            }

            if (UnityEngine.PlayerPrefs.HasKey("GPTAnswerForPreferences"))
            {
                UnityEngine.PlayerPrefs.DeleteKey("GPTAnswerForPreferences");
            }

            if (_playerPrefs.ContainsKey("PlayerName"))
            {
                _playerPrefs.Remove("PlayerName");
            }

            if (_playerPrefs.ContainsKey("CompanyName"))
            {
                _playerPrefs.Remove("CompanyName");
            }

            if (_playerPrefs.ContainsKey("ElevatorPitch"))
            {
                _playerPrefs.Remove("ElevatorPitch");
            }

            if (_playerPrefs.ContainsKey("Preferences"))
            {
                _playerPrefs.Remove("Preferences");
            }

            if (_playerPrefs.ContainsKey("GPTAnswerForPreferences"))
            {
                _playerPrefs.Remove("GPTAnswerForPreferences");
            }
        }
    }
}