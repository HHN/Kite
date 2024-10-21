using System.Collections;
using UnityEngine;
using LeastSquares.Overtone;
using System.Threading;
using System.Threading.Tasks;


public class TextToSpeechService
{
    private static TextToSpeechService instance;
    private string choicesForTextToSpeech;
    private AudioSource audioSource;
    private string novelTitle;
    private int optionCounter;
    private CancellationTokenSource cancellationTokenSource;

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
        optionCounter++;
        switch(optionCounter)
        {
            case 1:
                choicesForTextToSpeech += "Erste Option: ";
                break;
            case 2:
                choicesForTextToSpeech += "Zweite Option: ";
                break;
            case 3:
                choicesForTextToSpeech += "Dritte Option: ";
                break;
            case 4:
                choicesForTextToSpeech += "Vierte Option: ";
                break;
            case 5:
                choicesForTextToSpeech += "Fünfte Option: ";
                break;
        }
        choicesForTextToSpeech += choice;
    }

    public string returnChoicesForTextToSpeech()
    {
        return choicesForTextToSpeech;
    }

    public async Task TextToSpeechReadLive(string text, bool readAnyway = false)
    {
        Debug.Log("TALKING");
        //if (audioSource != null)
        //{
        //    if (TextToSpeechManager.Instance().IsTextToSpeechActivated() || readAnyway)
        //    {
        //        //Debug.Log(text);
        //        cancellationTokenSource = new CancellationTokenSource();
        //        try
        //        {
        //            AudioClip audioClip = await engine.Speak(text, TTSVoiceNative.LoadVoiceFromResources("de-de-thorsten-high"));
        //            if (audioSource != null && !cancellationTokenSource.IsCancellationRequested)
        //            {
        //                audioSource.clip = audioClip;
        //                audioSource.Play();
        //            }
        //        }
        //        catch (TaskCanceledException)
        //        {
        //            Debug.Log("Text-to-Speech operation was cancelled.");
        //        }
        //    }
        //}
    }

    public void CancelSpeechAndAudio()
    {
        if (cancellationTokenSource != null)
        {
            cancellationTokenSource.Cancel();
        }
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}