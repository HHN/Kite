using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MicrofonButton : MonoBehaviour
{   
    private bool isMicrofonConnected = false;
    private int minFreq;
    private int maxFreq;
    private AudioClip clip;
    private bool isRecording = false;

    public Button microfonButton;
    public Sprite microfonImageWhite;
    public Sprite microfonImageRed;
    public TMP_InputField textField;

    void Start()
    {
        if (Microphone.devices.Length <= 0)
        {   
            Debug.LogWarning("Microphone not connected!");
        }
        else 
        {
            isMicrofonConnected = true;
            Microphone.GetDeviceCaps(null, out minFreq, out maxFreq);
  
            if (minFreq == 0 && maxFreq == 0)
            {
                maxFreq = 44100;
            }
        }

        microfonButton.onClick.AddListener(OnButtonPressed);
    }

    private void VoiceToText(AudioClip userInput)
    {
        byte[] data = SaveWav.Save("input", userInput);
        // TODO: Send Request to Server, translate the audio clip to text and put the text in the Textfield.
    }

    public void OnButtonPressed()
    {
        if (isRecording)
        {
            AudioClip userInput = this.FinishRecording();
            VoiceToText(userInput);
            this.isRecording = false;
            microfonButton.image.sprite = microfonImageWhite;
        }
        else
        {
            this.StartRecording();
            this.isRecording = true;
            microfonButton.image.sprite = microfonImageRed;
        }
    }

    public void StartRecording()
    {
        if (!isMicrofonConnected)
        {
            // TODO: Show error message to the user.
            return;
        }
        if (!Microphone.IsRecording(null))
        {
            clip = Microphone.Start(null, true, 20, maxFreq);
        }

    }

    public AudioClip FinishRecording()
    {
        AudioClip result = clip;
        clip = null;
        
        if (!isMicrofonConnected)
        {
            // TODO: Show error message to the user.
            return null;
        }
        if (Microphone.IsRecording(null))
        {
            Microphone.End(null); 
        }
        return result;
    }
}
