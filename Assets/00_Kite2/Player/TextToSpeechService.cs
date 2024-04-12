using UnityEngine;


public class TextToSpeechService
{
    private static TextToSpeechService instance;

    private string choicesForTextToSpeech;

    private AudioSource audioSource;

    private string novelTitle;

    public static TextToSpeechService Instance()
    {
        if (instance == null)
        {
            instance = new TextToSpeechService();
        }
        return instance;
    }

    public void SetAudioSource(AudioSource audioSource)
    {
        this.audioSource = audioSource;
    }

    public void SetNovelTitle(string novelTitle)
    {
        this.novelTitle = novelTitle;
    }

    public void TextToSpeech(string titleAndId, bool opendFromOptionsManager = false)
    {
        if(opendFromOptionsManager)
        {
            audioSource.clip = Resources.Load<AudioClip>(returnNameOfAudioFileFromTitelAndId(novelTitle + titleAndId));
        } else
        {
            audioSource.clip = Resources.Load<AudioClip>(returnNameOfAudioFileFromTitelAndId(titleAndId));
        }
        if(audioSource.clip != null)
        {
            audioSource.Play();
        } else
        {
            Debug.LogError("AudioClip couldn't be found. Check the path.");
        }
        choicesForTextToSpeech = "";
    }

    public string returnNameOfAudioFileFromTitelAndId(string titleAndId)
    {
        string returnstring = titleAndId.Replace("ä", "ae").Replace("ö", "oe").Replace("ü", "ue").Replace("ß", "ss");
        //Debug.Log(returnstring);
        return returnstring;
    }

    public void addChoiceToChoiceCollectionForTextToSpeech(string choice)
    {
        choicesForTextToSpeech += choice;
    }

    public string returnChoicesForTextToSpeech()
    {
        return choicesForTextToSpeech;
    }

}