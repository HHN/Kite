using _00_Kite2.Common;
using _00_Kite2.Common.Managers;
using _00_Kite2.Common.Messages;
using _00_Kite2.Common.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.Einstellungen
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
        [SerializeField] private GameObject checkMarkSoundEffects;
        [SerializeField] private SliderEventHandler soundEffectsSliderHandler;
        [SerializeField] private AudioClip soundCheckClip;
        private float _soundEffectsVolume = 1;

        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.SOUNDEINSTELLUNG_SCENE);
            // WENN SAFEDSOUNDEFFECTVOLUME NOCH NICHT VORHANDEN SOLL DIESES AUF 1 GESETZT WERDEN
            InitializeToggleTextToSpeech();
            InitializeToggleSoundEffects();

            InitializeSoundEffectSlider();

            soundEffectsSliderHandler.OnSliderReleasedEvent += HandleSoundEffectsSliderReleased;

            soundEffectsVolumeSlider.onValueChanged.AddListener(UpdateSoundEffectsVolume);

            toggleTextToSpeech.onValueChanged.AddListener(delegate { OnToggleTextToSpeech(); });
            toggleSoundEffects.onValueChanged.AddListener(delegate { OnToggleSoundEffects(); });

            toggleTextToSpeechButton.onClick.AddListener(ToggleTextToSpeech);
            toggleSoundEffectsButton.onClick.AddListener(ToggleSoundEffects);

            testVolumeSoundEffects.onClick.AddListener(TestVolumeSoundEffects);

            toggleTextToSpeechInfoButton.onClick.AddListener(OnToggleTextToSpeechInfoButton);
            toggleSoundEffectsInfoButton.onClick.AddListener(OnToggleSoundEffectsInfoButton); 
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

        private void InitializeToggleSoundEffects()
        {
            bool isSoundEffectsVolumeOn = PlayerPrefs.GetInt("IsSoundeffectsVolumeOn", 1) == 1;
            if (!isSoundEffectsVolumeOn)
            {
                toggleSoundEffects.isOn = false;
                ToggleVisibilityCheckmarkSoundEffects(false);
                _soundEffectsVolume = 0f;
            }
            else
            {
                toggleSoundEffects.isOn = true;
                ToggleVisibilityCheckmarkSoundEffects(true);
                _soundEffectsVolume = PlayerPrefs.GetFloat("SavedSoundeffectsVolume", 1);
            }
        }

        private void InitializeSoundEffectSlider()
        {
            soundEffectsVolumeSlider.value = _soundEffectsVolume;
            GlobalVolumeManager.Instance.SetGlobalVolume(_soundEffectsVolume);
        }

        private void HandleSoundEffectsSliderReleased(float value)
        {
            PlayerPrefs.SetFloat("SavedSoundeffectsVolume", value);

            GlobalVolumeManager.Instance.SetGlobalVolume(value);

            _soundEffectsVolume = value;

            if (_soundEffectsVolume > 0)
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

        private void UpdateSoundEffectsVolume(float sliderValue)
        {
            if (sliderValue > 0f)
            {
                ToggleVisibilityCheckmarkSoundEffects(true);
            }
            else
            {
                ToggleVisibilityCheckmarkSoundEffects(false);
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
                PlayerPrefs.SetFloat("SavedSoundeffectsVolume", _soundEffectsVolume);
                GlobalVolumeManager.Instance.SetGlobalVolume(0f);
                _soundEffectsVolume = 0f;
                soundEffectsVolumeSlider.value = 0;
                DisplayInfoMessage(InfoMessages.DEACTIVATED_SOUNDEFFECTS_BUTTON);
                ToggleVisibilityCheckmarkSoundEffects(false);
                PlayerPrefs.SetInt("IsSoundeffectsVolumeOn", 0);
            }
            else
            {                    
                _soundEffectsVolume = PlayerPrefs.GetFloat("SavedSoundeffectsVolume");
                if (_soundEffectsVolume == 0f)
                {
                    _soundEffectsVolume = 1;
                } else if (_soundEffectsVolume < 0.1f)
                {
                    _soundEffectsVolume = 0.1f;
                }
                soundEffectsVolumeSlider.value = _soundEffectsVolume;
                GlobalVolumeManager.Instance.SetGlobalVolume(_soundEffectsVolume);
                DisplayInfoMessage(InfoMessages.ACTIVATED_SOUNDEFFECTS_BUTTON);
                ToggleVisibilityCheckmarkSoundEffects(true);
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

        private void ToggleVisibilityCheckmarkSoundEffects(bool show)
        {
            checkMarkSoundEffects.SetActive(show);
        }
    }
}