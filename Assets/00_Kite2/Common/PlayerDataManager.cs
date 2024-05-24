using UnityEngine;
using System.Collections.Generic;
using System.Text;

public class PlayerDataManager
{
    private static PlayerDataManager instance;

    private PlayerDataWrapper playerData = new PlayerDataWrapper();

    private Dictionary<string, string> playerPrefs = new Dictionary<string, string>();

    private List<string> keys = new List<string>();

    private List<string> novelHistory = new List<string>();

    public static PlayerDataManager Instance()
    {
        if (instance == null)
        {
            instance = new PlayerDataManager();
        }
        return instance;
    }

    public void SetNovelHistory(List<string> novelHistory)
    {
        this.novelHistory = novelHistory;
    }

    public void SavePlayerData(string key, string content)
    {
        Debug.Log("Saved key: " + key + ", content: " + content);
        PlayerPrefs.SetString(key, content);
        PlayerPrefs.Save();
        AddKeyToKeyList(key);
    }

    public void AddKeyToKeyList(string key)
    {
        if (!keys.Contains(key))
        {
            keys.Add(key);
            SaveKeys();
        }
    }

    public void SaveKeys()
    {
        PlayerPrefs.SetString("keys", string.Join(",", keys));
        PlayerPrefs.Save();
    }

    public void LoadAllPlayerPrefs()
    {
    if (keys.Count == 0)
    {
        string keysString = PlayerPrefs.GetString("keys", "");
        keys = new List<string>(keysString.Split(','));
    }
    foreach (string playerPref in keys)
    {
        // Überprüfen, ob der Key bereits existiert
        if (!playerPrefs.ContainsKey(playerPref))
        {
            playerPrefs.Add(playerPref, ReadPlayerData(playerPref));
            Debug.Log("Added key: " + playerPref);
        }
        else
        {
            playerPrefs[playerPref] = ReadPlayerData(playerPref);
            //Debug.Log("Updated key: " + playerPref);
        }
    }
}

    
    public string GetPlayerData(string key)
    {
        if (playerPrefs.TryGetValue(key, out string value))
        {
            return value;
        }
        else
        {
            Debug.Log("Asked for key: " + key);
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

    public string ReadPlayerData(string key)
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
        string novelHistoryString = CombineSentences(novelHistory);

        // Trennzeichen definieren
        string mainSeparator = "|";   // Trennt verschiedene Auswertungen
        string subSeparator = "~";    // Trennt Novel-Name von Ablauf und Bewertung
        string flowSeparator = "^";   // Trennt Ablauf von der Bewertung

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
        Debug.Log("Saved: " + existingEvaluations);
    }

    public string CombineSentences(List<string> sentences)
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
}