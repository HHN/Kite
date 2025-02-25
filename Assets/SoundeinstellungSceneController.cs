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
            // WENN SAFEDSOUNDEFFECTVOLUME NOCH NICHT VORHANDEN SOLL DIESES AUF 1 GESETZT WERDEN
            InitializeToggleTextToSpeech();
            InitializeToggleSoundeffects();

            InitializeSoundeffectSlider();

            soundEffectsSliderHandler.OnSliderReleasedEvent += HandleSoundEffectsSliderReleased;

            soundEffectsVolumeSlider.onValueChanged.AddListener(UpdateSoundeffectsVolume);

            toggleTextToSpeech.onValueChanged.AddListener(delegate { OnToggleTextToSpeech(); });
            toggleSoundEffects.onValueChanged.AddListener(delegate { OnToggleSoundEffects(); });

            toggleTextToSpeechButton.onClick.AddListener(delegate { ToggleTextToSpeech(); });
            toggleSoundEffectsButton.onClick.AddListener(delegate { ToggleSoundEffects(); });

            testVolumeSoundEffects.onClick.AddListener(delegate { TestVolumeSoundEffects(); });

            toggleTextToSpeechInfoButton.onClick.AddListener(delegate { OnToggleTextToSpeechInfoButton(); });
            toggleSoundEffectsInfoButton.onClick.AddListener(delegate { OnToggleSoundEffectsInfoButton(); }); 
        }

        private void OnDestroy()
        {
            soundEffectsSliderHandler.OnSliderReleasedEvent -= HandleSoundEffectsSliderReleased;
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
            bool isSoundeffectsVolumeOn = PlayerPrefs.GetInt("IsSoundeffectsVolumeOn", 1) == 1;
            if (!isSoundeffectsVolumeOn)
            {
                toggleSoundEffects.isOn = false;
                ToggleVisibilityCheckmarkSoundeffects(false);
                soundeffectsVolume = 0f;
            }
            else
            {
                toggleSoundEffects.isOn = true;
                ToggleVisibilityCheckmarkSoundeffects(true);
                soundeffectsVolume = PlayerPrefs.GetFloat("SavedSoundeffectsVolume", 1);
            }
        }

        private void InitializeSoundeffectSlider()
        {
            soundEffectsVolumeSlider.value = soundeffectsVolume;
            GlobalVolumeManager.Instance.SetGlobalVolume(soundeffectsVolume);
        }

        private void HandleSoundEffectsSliderReleased(float value)
        {
            PlayerPrefs.SetFloat("SavedSoundeffectsVolume", value);

            GlobalVolumeManager.Instance.SetGlobalVolume(value);

            soundeffectsVolume = value;

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

        private void UpdateSoundeffectsVolume(float sliderValue)
        {
            if (sliderValue > 0f)
            {
                ToggleVisibilityCheckmarkSoundeffects(true);
            }
            else
            {
                ToggleVisibilityCheckmarkSoundeffects(false);
            }
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
                PlayerPrefs.SetFloat("SavedSoundeffectsVolume", soundeffectsVolume);
                GlobalVolumeManager.Instance.SetGlobalVolume(0f);
                soundeffectsVolume = 0f;
                soundEffectsVolumeSlider.value = 0;
                DisplayInfoMessage(InfoMessages.DEACTIVATED_SOUNDEFFECTS_BUTTON);
                ToggleVisibilityCheckmarkSoundeffects(false);
                PlayerPrefs.SetInt("IsSoundeffectsVolumeOn", 0);
            }
            else
            {                    
                soundeffectsVolume = PlayerPrefs.GetFloat("SavedSoundeffectsVolume");
                if (soundeffectsVolume == 0f)
                {
                    soundeffectsVolume = 1;
                } else if (soundeffectsVolume < 0.1f)
                {
                    soundeffectsVolume = 0.1f;
                }
                soundEffectsVolumeSlider.value = soundeffectsVolume;
                GlobalVolumeManager.Instance.SetGlobalVolume(soundeffectsVolume);
                DisplayInfoMessage(InfoMessages.ACTIVATED_SOUNDEFFECTS_BUTTON);
                ToggleVisibilityCheckmarkSoundeffects(true);
                PlayerPrefs.SetInt("IsSoundeffectsVolumeOn", 1);
            }
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
            }
            else
            {
                toggleSoundEffects.isOn = true;
            }
        }

        private void TestVolumeSoundEffects()
        {
            GlobalVolumeManager.Instance.PlaySound(soundCheckClip);
        }

        private void OnToggleTextToSpeechInfoButton()
        {
            DisplayInfoMessage(InfoMessages.EXPLANATION_TEXTTOSPEECH_BUTTON);
        }

        private void OnToggleSoundEffectsInfoButton()
        {
            DisplayInfoMessage(InfoMessages.EXPLANATION_SOUNDEFFECTS_BUTTON);
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