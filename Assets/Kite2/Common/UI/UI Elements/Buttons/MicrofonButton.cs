using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MicrofonButton : MonoBehaviour, OnSuccessHandler
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
    public GameObject whisperRequestPrefab;
    public SceneController sceneController;

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

        GetTextOfAudioServerCall call = Instantiate(whisperRequestPrefab).GetComponent<GetTextOfAudioServerCall>();
        call.onSuccessHandler = this;
        call.file = data;
        call.sceneController = sceneController;
        call.SendRequest();
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
            sceneController.DisplayErrorMessage(ErrorMessages.MICROFON_NOT_CONNECTED_ERROR);
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
            sceneController.DisplayErrorMessage(ErrorMessages.MICROFON_NOT_CONNECTED_ERROR);
            return null;
        }
        if (Microphone.IsRecording(null))
        {
            Microphone.End(null); 
        }
        return result;
    }

    public void OnSuccess(Response response)
    {
        textField.text = response.completion;
    }
}
