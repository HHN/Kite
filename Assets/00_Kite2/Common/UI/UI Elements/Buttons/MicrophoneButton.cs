using System.Linq;
using System.Threading.Tasks;
using LeastSquares.Undertone;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.Common.UI.UI_Elements.Buttons
{
    public class MicrophoneButton : MonoBehaviour
    {
        [SerializeField] private SpeechEngine engine;
        [SerializeField] private int maxRecordingTime;
        [SerializeField] private AudioClip clip;
        [SerializeField] private bool isRecording;
        [SerializeField] private TMP_InputField textField;
        [SerializeField] private Button microphoneButton;
        [SerializeField] private Sprite spriteWhileNotRecording;
        [SerializeField] private Sprite spriteWhileRecording;

        void Start()
        {
            maxRecordingTime = 30;
            microphoneButton.onClick.AddListener(OnClicked);
        }

        private async void OnClicked()
        {
            if (!engine.Loaded) return;

            if (!isRecording)
            {
                StartRecording();
                isRecording = true;
                microphoneButton.image.sprite = spriteWhileRecording;
            }
            else
            {
                microphoneButton.interactable = false;
                string transcription = await StopRecording();
                textField.text = transcription;
                microphoneButton.interactable = true;
                isRecording = false;
                microphoneButton.image.sprite = spriteWhileNotRecording;
            }
        }

        private void StartRecording()
        {
            clip = Microphone.Start(null, false, maxRecordingTime, SpeechEngine.SampleFrequency);
        }

        private async Task<string> StopRecording()
        {
            var str = engine.TranscribeClip(clip, 0, Microphone.GetPosition(null));
            Microphone.End(null);
            var segments = await str;
            return string.Join("\n", segments.Select(s => s.text).ToArray());
        }
    }
}