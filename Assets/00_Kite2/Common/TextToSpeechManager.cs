using UnityEngine;

public class TextToSpeechManager {

    private static TextToSpeechManager instance;

    private TextToSpeechWrapper wrapper;
    
    private const string key = "TextToSpeechWrapper";

    private TextToSpeechManager()
    {
        wrapper = LoadTextToSpeechWrapper();
    }

    public static TextToSpeechManager Instance()
    {
        if (instance == null)
        {
            instance = new TextToSpeechManager();
        }
        return instance;
    }

    public TextToSpeechWrapper LoadTextToSpeechWrapper()
    {
        if (PlayerPrefs.HasKey(key))
        {
            string json = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<TextToSpeechWrapper>(json);
        }
        else
        {
            return new TextToSpeechWrapper()
            {
                activatedTextToSpeech = false
            };
        }
    }

    public void ActivateTextToSpeech()
    {
        if (wrapper == null)
        {
            wrapper = LoadTextToSpeechWrapper();
        }
        wrapper.activatedTextToSpeech = true;
        Save();
    }

    public void DeactivateTextToSpeech()
    {
        if (wrapper == null)
        {
            wrapper = LoadTextToSpeechWrapper();
        }
        wrapper.activatedTextToSpeech = false;
        Save();
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(wrapper);
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }

    public bool IsTextToSpeechActivated()
    {
        TextToSpeechWrapper wrapper = LoadTextToSpeechWrapper();
        return wrapper.activatedTextToSpeech;
    }

}