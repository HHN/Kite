using _00_Kite2.Common;
using _00_Kite2.Common.Managers;
using _00_Kite2.Common.Messages;
using _00_Kite2.Common.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace _00_Kite2.NewPages
{
    public class SoundeinstellungSceneController : SceneController
    {
        [SerializeField] private Toggle toggleTextToSpeech;
        [SerializeField] private Toggle toggleSoundEffects;
        [SerializeField] private Button toggleTextToSpeechInfoButton;
        [SerializeField] private Button toggleSoundEffectsInfoButton;
        [SerializeField] private Slider tTSVolumeSlider;
        [SerializeField] private Slider soundEffectsVolumeSlider;
        [SerializeField] private Button testVolumeSoundEffects;
        [SerializeField] private GameObject checkMarkTTS;
        [SerializeField] private GameObject checkMarkSoundeffects;
        [SerializeField] private SliderEventHandler soundEffectsSliderHandler;
        [SerializeField] private AudioClip soundCheckClip;
        private float tTSVolume = 100;
        private float soundeffectsVolume = 100;
        private bool ignoreNextValueChange = false;

        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.SOUNDEINSTELLUNG_SCENE);

            InitializeToggleTextToSpeech();
            InitializeToggleSoundeffects();

            InitializeSoundeffectSlider();

            soundEffectsSliderHandler.OnSliderReleasedEvent += HandleSoundEffectsSliderReleased;

            toggleTextToSpeech.onValueChanged.AddListener(delegate { OnToggleTextToSpeech(); });
            toggleSoundEffects.onValueChanged.AddListener(delegate { OnToggleSoundEffects(); });

            testVolumeSoundEffects.onClick.AddListener(delegate {  TestVolumeSoundEffects(); });

            toggleTextToSpeechInfoButton.onClick.AddListener(OnToggleTextToSpeechInfoButton);
            toggleSoundEffectsInfoButton.onClick.AddListener(OnToggleSoundEffectsInfoButton);

            soundEffectsVolumeSlider.onValueChanged.AddListener(UpdateSoundeffectsVolume);
        }

        private void OnDestroy()
        {
            soundEffectsSliderHandler.OnSliderReleasedEvent -= HandleSoundEffectsSliderReleased;
            //ttsSliderHandler.OnSliderReleasedEvent -= HandleTTSVolumeSliderReleased;
        }

        private void TestVolumeSoundEffects()
        {
            GlobalVolumeManager.Instance.PlaySound(soundCheckClip);
        }

        private void HandleTTSVolumeSliderReleased(float value)
        {
            Debug.Log($"TTS-Slider losgelassen, Wert: {value}");
            // Füge hier die Logik ein, die ausgeführt werden soll
            UpdateTTSVolume(value); // Beispiel: Slider-Wert verarbeiten
        }

        private void HandleSoundEffectsSliderReleased(float value)
        {
            if (soundeffectsVolume > 0)
            {
                ignoreNextValueChange = true;
                toggleSoundEffects.isOn = true;
            }
            else
            {
                ignoreNextValueChange = true;
                toggleSoundEffects.isOn = false;
            }
        }

        private void InitializeToggleTextToSpeech()
        {
            if (TextToSpeechManager.Instance.IsTextToSpeechActivated())
            {
                toggleTextToSpeech.isOn = true;
                ToggleVisibilityCheckmarkTTS(true);
            }
            else
            {
                toggleTextToSpeech.isOn = false;
                ToggleVisibilityCheckmarkTTS(false);
            }
        }

        private void InitializeToggleSoundeffects()
        {
            soundeffectsVolume = PlayerPrefs.GetFloat("SavedSoundeffectsVolume", 100);
            if (soundeffectsVolume > 0)
            {
                Debug.Log("soundeffectsVolume: "+ soundeffectsVolume);
                toggleSoundEffects.isOn = true;
                ToggleVisibilityCheckmarkSoundeffects(true);
            }
            else
            {
                toggleSoundEffects.isOn = false;
                ToggleVisibilityCheckmarkSoundeffects(false);
            }
        }

        private void InitializeSoundeffectSlider()
        {
            float savedSoundeffectsVolume = PlayerPrefs.GetFloat("LastSoundeffectsVolume", 1);
            soundeffectsVolume = savedSoundeffectsVolume;
            soundEffectsVolumeSlider.value = soundeffectsVolume;
            Debug.Log("InitializeSoundeffectSlider: " + soundeffectsVolume);
            GlobalVolumeManager.Instance.SetGlobalVolume(soundeffectsVolume);
        }

        private void OnToggleTextToSpeech()
        {
            if (ignoreNextValueChange)
            {
                ignoreNextValueChange = false;
            } else
            {
                if (toggleTextToSpeech.isOn)
                {
                    TextToSpeechManager.Instance.ActivateTTS();
                    DisplayInfoMessage(InfoMessages.STARTED_TOGGLETEXTTOSPEECH_BUTTON);
                    StartCoroutine(TextToSpeechManager.Instance.Speak("Text wird nun vorgelesen"));
                    ToggleVisibilityCheckmarkTTS(true);
                }
                else
                {
                    StartCoroutine(TextToSpeechManager.Instance.Speak("Text wird nicht mehr vorgelesen"));
                    TextToSpeechManager.Instance.DeactivateTTS();
                    DisplayInfoMessage(InfoMessages.STOPPED_TOGGLETEXTTOSPEECH_BUTTON);
                    ToggleVisibilityCheckmarkTTS(false);
                }
            }
        }

        private void OnToggleSoundEffects()
        {
            if (toggleSoundEffects.isOn)
            {
                soundeffectsVolume = PlayerPrefs.GetFloat("SavedSoundeffectsVolume", 1);
                Debug.Log("OnToggleSoundEffects: " + soundeffectsVolume);
                soundEffectsVolumeSlider.value = soundeffectsVolume;
                GlobalVolumeManager.Instance.SetGlobalVolume(soundeffectsVolume);
                DisplayInfoMessage(InfoMessages.ACTIVATED_SOUNDEFFECTS_BUTTON);
                ToggleVisibilityCheckmarkSoundeffects(true);
            }
            else
            {
                PlayerPrefs.SetFloat("SavedSoundeffectsVolume", soundeffectsVolume);
                GlobalVolumeManager.Instance.SetGlobalVolume(0f);
                soundeffectsVolume = 0f;
                PlayerPrefs.GetFloat("LastSoundeffectsVolume", soundeffectsVolume);
                soundEffectsVolumeSlider.value = soundeffectsVolume;
                DisplayInfoMessage(InfoMessages.DEACTIVATED_SOUNDEFFECTS_BUTTON);
                ToggleVisibilityCheckmarkSoundeffects(false);
            }
        }


        private void OnToggleTextToSpeechInfoButton()
        {
            DisplayInfoMessage(InfoMessages.EXPLANATION_TEXTTOSPEECH_BUTTON);
        }

        private void OnToggleSoundEffectsInfoButton()
        {
            DisplayInfoMessage(InfoMessages.EXPLANATION_SOUNDEFFECTS_BUTTON);
        }

        private void UpdateTTSVolume(float sliderValue)
        {
            tTSVolume = sliderValue;
            PlayerPrefs.SetFloat("SavedTTSVolume", tTSVolume);
        }

        private void UpdateSoundeffectsVolume(float sliderValue)
        {
            soundeffectsVolume = sliderValue; 
            PlayerPrefs.SetFloat("LastSoundeffectsVolume", soundeffectsVolume);
            Debug.Log("sliderValue: " + sliderValue);

            GlobalVolumeManager.Instance.SetGlobalVolume(soundeffectsVolume);
            if (soundeffectsVolume > 0)
            {
                ToggleVisibilityCheckmarkSoundeffects(true);
            }
            else
            {
                ToggleVisibilityCheckmarkSoundeffects(false);
            }
        }

        private void ToggleVisibilityCheckmarkTTS(bool show)
        {
            checkMarkTTS.SetActive(show);
        }

        private void ToggleVisibilityCheckmarkSoundeffects(bool show)
        {
            checkMarkSoundeffects.SetActive(show);
        }

    }
}