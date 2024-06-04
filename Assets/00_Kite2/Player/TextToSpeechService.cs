using UnityEngine;
using LeastSquares.Overtone;


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

    public void TextToSpeech(string titleAndId, bool readAnyway = false)
    {
        //Debug.Log(titleAndId);
        if(TextToSpeechManager.Instance().IsTextToSpeechActivated() || readAnyway)
        {
            audioSource.clip = Resources.Load<AudioClip>(returnNameOfAudioFileFromTitelAndId(titleAndId));
            if(audioSource.clip != null)
            {
                audioSource.Play();
            } else
            {
                Debug.LogError("AudioClip couldn't be found. Check the path.");
            } 
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

    public async void TextToSpeechReadLive(string text, TTSEngine engine, bool readAnyway = false)
    {
        if(audioSource != null)
        {
            if(TextToSpeechManager.Instance().IsTextToSpeechActivated() || readAnyway)
                {
                    Debug.Log(text);
                    AudioClip audioClip = await engine.Speak(text, TTSVoiceNative.LoadVoiceFromResources("de-de-thorsten-high"));
                    audioSource.clip = audioClip;
                    audioSource.Play();
                    // AudioClip clip = Overtone.GenerateAudio(text);
                    // yield return new WaitUntil(() => clip != null);
                    // audioSource.clip = Resources.Load<AudioClip>();
                    // if(audioSource.clip != null)
                    // {
                    //     audioSource.Play();
                    // } else
                    // {
                    //     Debug.LogError("There has been an error creating an audio clip.");
                    // } 
                }
        }        
    }
}