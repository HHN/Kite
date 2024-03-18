using UnityEngine;

public class PlayerDataManager
{
    private static PlayerDataManager instance;

    private PlayerDataWrapper playerData = new PlayerDataWrapper();

    string[] keys = { "PlayerName", "CompanyName", "ElevatorPitch", "Preverences", "GPTAnswerForPreverences" };

    public static PlayerDataManager Instance()
    {
        if (instance == null)
        {
            instance = new PlayerDataManager();
        }
        return instance;
    }

    public void SavePlayerData(string key, string content)
    {
        // Speichert den Inhalt unter dem angegebenen Schlüssel in PlayerPrefs
        PlayerPrefs.SetString(key, content);
        PlayerPrefs.Save(); // Stellt sicher, dass die Änderungen gespeichert werden
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

    public void PrintAllPlayerPrefsToConsole()
    {
        foreach (string key in keys)
            {
                if (PlayerPrefs.HasKey(key))
                {
                    Debug.Log(key + ": " + PlayerPrefs.GetString(key));
                }
            }
    }

}