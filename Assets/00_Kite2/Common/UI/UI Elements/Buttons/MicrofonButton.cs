using LeastSquares.Undertone;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MicrofonButton : MonoBehaviour
{
    [SerializeField] private SpeechEngine engine;
    [SerializeField] private int maxRecordingTime;
    [SerializeField] private AudioClip clip;
    [SerializeField] private bool isRecording;
    [SerializeField] private TMP_InputField textField;
    [SerializeField] private Button microfonButton;
    [SerializeField] private Sprite spriteWhileNotRecording;
    [SerializeField] private Sprite spriteWhileRecording;

    void Start()
    {
        maxRecordingTime = 30;
        microfonButton.onClick.AddListener(OnClicked);
    }

    private async void OnClicked()
    {
        if (!engine.Loaded) return;

        if (!isRecording)
        {
            StartRecording();
            isRecording = true;
            microfonButton.image.sprite = spriteWhileRecording;
        }
        else
        {
            microfonButton.interactable = false;
            string transcription = await StopRecording();
            textField.text = transcription;
            microfonButton.interactable = true;
            isRecording = false;
            microfonButton.image.sprite = spriteWhileNotRecording;
        }
    }

    public void StartRecording()
    {
        clip = Microphone.Start(null, false, maxRecordingTime, SpeechEngine.SampleFrequency);
    }

    public async Task<string> StopRecording()
    {
        var str = engine.TranscribeClip(clip, 0, Microphone.GetPosition(null));
        Microphone.End(null);
        var segments = await str;
        return string.Join("\n", segments.Select(S => S.text).ToArray());
    }
}