using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace _00_Kite2.Common.Managers
{
    public class PlayerDataManager
    {
        private static PlayerDataManager _instance;

        private PlayerDataWrapper _playerData = new PlayerDataWrapper();

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
            else if (PlayerPrefs.HasKey(key))
            {
                return true;
            }

            return false;
        }

        public void SetNovelHistory(List<string> novelHistory)
        {
            this._novelHistory = novelHistory;
        }

        public void SavePlayerData(string key, string content)
        {
            PlayerPrefs.SetString(key, content);
            PlayerPrefs.Save();
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
            PlayerPrefs.SetString("keys", string.Join(",", _keys));
            PlayerPrefs.Save();
        }

        public void LoadAllPlayerPrefs()
        {
            if (_keys.Count == 0)
            {
                string keysString = PlayerPrefs.GetString("keys", "");
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
                if (PlayerPrefs.HasKey(key))
                {
                    // Gibt den Wert zurück, der dem Schlüssel zugeordnet ist
                    return PlayerPrefs.GetString(key);
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
            if (PlayerPrefs.HasKey(key))
            {
                // Gibt den Wert zurück, der dem Schlüssel zugeordnet ist
                return PlayerPrefs.GetString(key);
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
            string key = "evaluations";

            // Alte Auswertungen laden
            string existingEvaluations = PlayerPrefs.GetString(key, "");
            string novelHistoryString = CombineSentences(_novelHistory);

            // Trennzeichen definieren
            string mainSeparator = "|"; // Trennt verschiedene Auswertungen
            string subSeparator = "~"; // Trennt Novel-Name von Ablauf und Bewertung
            string flowSeparator = "^"; // Trennt Ablauf von der Bewertung

            // Neue Auswertung vorbereiten, indem der Name der Novel, der Ablauf und die Auswertung mit Trennzeichen verbunden werden
            string newEvaluation = novelName + subSeparator + novelHistoryString + flowSeparator + evaluation;

            // Neue Auswertung an bestehende anhängen
            if (!string.IsNullOrEmpty(existingEvaluations))
            {
                existingEvaluations += mainSeparator;
            }

            existingEvaluations += newEvaluation;

            // Alles zurück in PlayerPrefs speichern
            PlayerPrefs.SetString(key, existingEvaluations);
            PlayerPrefs.Save(); // Änderungen sichern
            //Debug.Log("Saved: " + existingEvaluations);
        }

        private string CombineSentences(List<string> sentences)
        {
            // Trennzeichen wählen, das sicher nicht in den Sätzen vorkommt
            string separator = "##";

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
            if (PlayerPrefs.HasKey("PlayerName"))
            {
                PlayerPrefs.DeleteKey("PlayerName");
            }

            if (PlayerPrefs.HasKey("CompanyName"))
            {
                PlayerPrefs.DeleteKey("CompanyName");
            }

            if (PlayerPrefs.HasKey("ElevatorPitch"))
            {
                PlayerPrefs.DeleteKey("ElevatorPitch");
            }

            if (PlayerPrefs.HasKey("Preverences"))
            {
                PlayerPrefs.DeleteKey("Preverences");
            }

            if (PlayerPrefs.HasKey("GPTAnswerForPreverences"))
            {
                PlayerPrefs.DeleteKey("GPTAnswerForPreverences");
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

            if (_playerPrefs.ContainsKey("Preverences"))
            {
                _playerPrefs.Remove("Preverences");
            }

            if (_playerPrefs.ContainsKey("GPTAnswerForPreverences"))
            {
                _playerPrefs.Remove("GPTAnswerForPreverences");
            }
        }
    }
}