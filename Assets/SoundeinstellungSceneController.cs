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
        [SerializeField] private Button toggleTextToSpeechButton;
        [SerializeField] private Button toggleSoundEffectsButton;
        [SerializeField] private Button toggleTextToSpeechInfoButton;
        [SerializeField] private Button toggleSoundEffectsInfoButton;
        [SerializeField] private Slider soundEffectsVolumeSlider;
        [SerializeField] private Button testVolumeSoundEffects;
        [SerializeField] private GameObject checkMarkTTS;
        [SerializeField] private GameObject checkMarkSoundeffects;
        [SerializeField] private SliderEventHandler soundEffectsSliderHandler;
        [SerializeField] private AudioClip soundCheckClip;
        private float soundeffectsVolume = 1;

        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.SOUNDEINSTELLUNG_SCENE);

            InitializeToggleTextToSpeech();
            InitializeToggleSoundeffects();

            InitializeSoundeffectSlider();

            soundEffectsSliderHandler.OnSliderReleasedEvent += HandleSoundEffectsSliderReleased;

            toggleTextToSpeech.onValueChanged.AddListener(delegate { OnToggleTextToSpeech(); });
            toggleSoundEffects.onValueChanged.AddListener(delegate { OnToggleSoundEffects(); });

            toggleTextToSpeechButton.onClick.AddListener(delegate { ToggleTextToSpeech(); });
            toggleSoundEffectsButton.onClick.AddListener(delegate { ToggleSoundEffects(); });

            testVolumeSoundEffects.onClick.AddListener(delegate { TestVolumeSoundEffects(); });

            toggleTextToSpeechInfoButton.onClick.AddListener(delegate { OnToggleTextToSpeechInfoButton(); });
            toggleSoundEffectsInfoButton.onClick.AddListener(delegate { OnToggleSoundEffectsInfoButton(); }); 

            soundEffectsVolumeSlider.onValueChanged.AddListener(UpdateSoundeffectsVolume);
        }

        private void OnDestroy()
        {
            soundEffectsSliderHandler.OnSliderReleasedEvent -= HandleSoundEffectsSliderReleased;
        }

        private void ToggleTextToSpeech()
        {
            if (toggleTextToSpeech.isOn)
            {
                toggleTextToSpeech.isOn = false;

            }
            else
            {
                toggleTextToSpeech.isOn = true;
            }
        }

        private void ToggleSoundEffects()
        {
            if (toggleSoundEffects.isOn)
            {
                toggleSoundEffects.isOn = false;
                PlayerPrefs.SetInt("IsSoundeffectsVolumeOn", 0);
            }
            else
            {
                toggleSoundEffects.isOn = true;
                PlayerPrefs.SetInt("IsSoundeffectsVolumeOn", 1);
            }
        }

        private void TestVolumeSoundEffects()
        {
            GlobalVolumeManager.Instance.PlaySound(soundCheckClip);
        }

        private void HandleSoundEffectsSliderReleased(float value)
        {
            Debug.Log("Triggert HandleSoundEffectsSliderReleased mit: " + value);
            PlayerPrefs.SetFloat("SavedSoundeffectsVolume", value);

            GlobalVolumeManager.Instance.SetGlobalVolume(value);
            if (soundeffectsVolume > 0)
            {
                toggleSoundEffects.isOn = true;
                PlayerPrefs.SetInt("IsSoundeffectsVolumeOn", 1);
            }
            else
            {
                toggleSoundEffects.isOn = false;
                PlayerPrefs.SetInt("IsSoundeffectsVolumeOn", 0);
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
            soundeffectsVolume = PlayerPrefs.GetFloat("SavedSoundeffectsVolume", 1);
            bool isSoundeffectsVolumeOn = PlayerPrefs.GetInt("IsSoundeffectsVolumeOn", 0) == 1;
            if (soundeffectsVolume == 0f || !isSoundeffectsVolumeOn)
            {
                toggleSoundEffects.isOn = false;
                ToggleVisibilityCheckmarkSoundeffects(false);
            }
            else
            {
                Debug.Log("soundeffectsVolume: " + soundeffectsVolume);
                toggleSoundEffects.isOn = true;
                ToggleVisibilityCheckmarkSoundeffects(true);
            }
        }

        private void InitializeSoundeffectSlider()
        {
            float savedSoundeffectsVolume = PlayerPrefs.GetFloat("SavedSoundeffectsVolume", 1);
            soundeffectsVolume = savedSoundeffectsVolume;
            soundEffectsVolumeSlider.value = soundeffectsVolume;
            Debug.Log("InitializeSoundeffectSlider: " + soundeffectsVolume);
            GlobalVolumeManager.Instance.SetGlobalVolume(soundeffectsVolume);
        }

        private void OnToggleTextToSpeech()
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

        private void OnToggleSoundEffects()
        {
            if (!toggleSoundEffects.isOn)
            {
                Debug.Log("Deactivate Soundeffects");
                PlayerPrefs.SetFloat("SavedSoundeffectsVolume", soundeffectsVolume);
                GlobalVolumeManager.Instance.SetGlobalVolume(0f);
                soundeffectsVolume = 0f;
                soundEffectsVolumeSlider.value = 0;
                DisplayInfoMessage(InfoMessages.DEACTIVATED_SOUNDEFFECTS_BUTTON);
                ToggleVisibilityCheckmarkSoundeffects(false);
            }
            else
            {                    
                Debug.Log("Activate Soundeffects");
                soundeffectsVolume = PlayerPrefs.GetFloat("SavedSoundeffectsVolume", soundeffectsVolume);
                if (soundeffectsVolume == 0f)
                {
                    soundeffectsVolume = 1;
                } else if (soundeffectsVolume < 0.1f)
                {
                    soundeffectsVolume = 0.1f;
                }
                Debug.Log("OnToggleSoundEffects: " + soundeffectsVolume);
                soundEffectsVolumeSlider.value = soundeffectsVolume;
                GlobalVolumeManager.Instance.SetGlobalVolume(soundeffectsVolume);
                DisplayInfoMessage(InfoMessages.ACTIVATED_SOUNDEFFECTS_BUTTON);
                ToggleVisibilityCheckmarkSoundeffects(true);
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

        private void UpdateSoundeffectsVolume(float sliderValue)
        {
            if (sliderValue == 0f)
            {
                ToggleVisibilityCheckmarkSoundeffects(false);
            }
            else
            {
                ToggleVisibilityCheckmarkSoundeffects(true);
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