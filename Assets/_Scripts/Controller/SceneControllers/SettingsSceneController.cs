using System;
using System.Collections.Generic;
using Assets._Scripts.Managers;
using Assets._Scripts.Messages;
using Assets._Scripts.SceneManagement;
using Assets._Scripts.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Controller.SceneControllers
{
    /// <summary>
    /// Manages the behavior and interactions for the settings scene, including sound settings
    /// and text-to-speech toggling functionality.
    /// </summary>
    public class SettingsSceneController : SceneController
    {
        [Header("UI References - Text-to-Speech")] 
        [SerializeField] private Button toggleTextToSpeechButton;
        [SerializeField] private Image activeTextToSpeechImage;
        [SerializeField] private Image inactiveTextToSpeechImage;

        [Header("UI References - Sound Effects")]
        [SerializeField] private Button toggleSoundEffectsButton;
        [SerializeField] private Image activeSoundEffectsImage;
        [SerializeField] private Image inactiveSoundEffectsImage;
        [SerializeField] private Slider soundEffectsVolumeSlider;
        [SerializeField] private SliderEventHandler soundEffectsSliderHandler;
        [SerializeField] private Image sliderBackgroundImage; 
        [SerializeField] private Image sliderFillImage;
        [SerializeField] private Image sliderHandleImage;
        [SerializeField] private Image sliderMinIcon; 
        [SerializeField] private Image sliderMaxIcon;
        [SerializeField] private AudioClip soundCheckClip;
        
        [Header("UI References - Font Size")]
        [SerializeField] private Slider fontSizeSlider;
        [SerializeField] private TMP_Text exampleText;
        [SerializeField] private Button confirmButton;
        
        [Header("Other UI")]
        [SerializeField] private TMP_Text versionInfo;
        [SerializeField] private RectTransform layout;
        [SerializeField] private RectTransform layoutGroupContainer;

        [Header("Visual States")] 
        [SerializeField] private Color activeColor = Color.white;
        [SerializeField] private Color disabledColor = Color.gray;
        [SerializeField] private float disabledAlpha = 0.5f;

        private Dictionary<Button, Action> _buttonActions;
        private bool _isTextToSpeechActive;
        private bool _isSoundActive;
        
        private float _soundEffectVolume = 1;
        
        private const int MinFontSize = 35;
        private const int MaxFontSize = 50;
        private int _updatedFontSize;

        /// <summary>
        /// Initializes the settings scene by loading user preferences, setting up the user interface,
        /// and attaching necessary event handlers. Ensures the scene state reflects the saved settings
        /// and configures the scene for proper functionality, including maintaining the navigation stack
        /// for returning to the scene.
        /// </summary>
        public void Start()
        {
            LoadSettings();
            InitializeUI();
            HookEvents();
            
            BackStackManager.Instance().Push(SceneNames.SettingsScene);
        }

        /// <summary>
        /// Loads user preferences related to text-to-speech activation, sound effects settings,
        /// saved sound effect volume, and font size. Retrieves the stored values from persistent storage
        /// and updates the respective fields to ensure the settings scene reflects the last saved user preferences.
        /// </summary>
        private void LoadSettings()
        {
            _isTextToSpeechActive = PlayerPrefs.GetInt("TTS", 1) == 1;
            _isSoundActive = PlayerPrefs.GetInt("IsSoundEffectVolumeOn", 1) == 1;
            _soundEffectVolume = PlayerPrefs.GetFloat("SavedSoundEffectVolume", 1f);
            _updatedFontSize = PlayerPrefs.GetInt("SavedFontSize", MinFontSize);
        }

        /// <summary>
        /// Configures and initializes the user interface elements in the settings scene, ensuring
        /// all UI components reflect the current settings and functionality. This method updates
        /// text fields, toggles, sliders, and other UI components based on user preferences and
        /// application state. It also adjusts layout where necessary to maintain visual consistency.
        /// </summary>
        private void InitializeUI()
        {
            ValidateUI();
            
            // Version
            if (versionInfo != null)
                versionInfo.text = "Version: " + Application.version;

            // TTS
            UpdateToggleImages(activeTextToSpeechImage, inactiveTextToSpeechImage, _isTextToSpeechActive);

            // Sound
            UpdateToggleImages(activeSoundEffectsImage, inactiveSoundEffectsImage, _isSoundActive);
            if (soundEffectsVolumeSlider != null)
            {
                soundEffectsVolumeSlider.value = _soundEffectVolume;
                SetSliderVisuals(_isSoundActive);
            }

            // Font Size
            if (fontSizeSlider != null)
            {
                float sliderValue = (float)(_updatedFontSize - MinFontSize) / (MaxFontSize - MinFontSize);
                fontSizeSlider.value = sliderValue;
                exampleText.fontSize = _updatedFontSize;
                
                FontSizeManager.Instance().UpdateAllTextComponents();
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
        }

        /// <summary>
        /// Validates the existence of all required user interface references for the settings scene and logs
        /// error messages for any missing components. Ensures that all necessary UI elements are properly
        /// assigned in the inspector to prevent runtime issues.
        /// </summary>
        private void ValidateUI()
        {
            if (!toggleTextToSpeechButton) LogManager.Error("toggleTextToSpeechButton fehlt!", this);
            if (!activeTextToSpeechImage) LogManager.Error("activeTextToSpeechImage fehlt!", this);
            if (!inactiveTextToSpeechImage) LogManager.Error("inactiveTextToSpeechImage fehlt!", this);
            if (!soundEffectsVolumeSlider) LogManager.Error("soundEffectsVolumeSlider fehlt!", this);
            if (!sliderBackgroundImage) LogManager.Error("sliderBackgroundImage fehlt!", this);
            if (!sliderFillImage) LogManager.Error("sliderFillImage fehlt!", this);
            if (!sliderHandleImage) LogManager.Error("sliderHandleImage fehlt!", this);
            if (!sliderMinIcon) LogManager.Error("sliderMinIcon fehlt!", this);
            if (!sliderMaxIcon) LogManager.Error("sliderMaxIcon fehlt!", this);
        }

        /// <summary>
        /// Sets up event handlers and listeners required for the interactive UI elements in the settings scene.
        /// Binds button actions, initializes component-specific listeners, and connects functionality for the
        /// sound effects slider event to ensure proper behavior when users interact with the scene's controls.
        /// </summary>
        private void HookEvents()
        {
            InitializeButtonActions();
            AddButtonListeners();

            if (soundEffectsSliderHandler != null)
                soundEffectsSliderHandler.OnSliderReleasedEvent += HandleSoundEffectsSliderReleased;
        }

        /// <summary>
        /// Called when the SettingsSceneController is being destroyed.
        /// Ensures proper cleanup by detaching the event listener from the sound effects slider handler.
        /// Prevents potential memory leaks or unexpected behavior related to lingering event subscriptions.
        /// </summary>
        private void OnDestroy()
        {
            if (soundEffectsSliderHandler)
            {
                soundEffectsSliderHandler.OnSliderReleasedEvent -= HandleSoundEffectsSliderReleased;
            }
        }

        /// <summary>
        /// Initializes the button-to-action mappings for the settings scene.
        /// Binds specific buttons to their respective event handlers and associates the font size slider
        /// with its value change handling functionality.
        /// Ensures that UI interactions are connected to their corresponding logic during scene initialization.
        /// </summary>
        private void InitializeButtonActions()
        {
            _buttonActions = new Dictionary<Button, Action>();

            if (toggleTextToSpeechButton) _buttonActions[toggleTextToSpeechButton] = OnToggleTextToSpeechButton;
            if (toggleSoundEffectsButton) _buttonActions[toggleSoundEffectsButton] = OnToggleSoundEffectsButton;
            if (confirmButton) _buttonActions[confirmButton] = SetFontSize;

            if (fontSizeSlider != null)
                fontSizeSlider.onValueChanged.AddListener(UpdateFontSize);
        }

        /// <summary>
        /// Adds listeners to all buttons in the settings scene based on the predefined actions in the _buttonActions dictionary.
        /// Associates each button with its corresponding delegate to handle click events.
        /// Ensures button interactions execute the appropriate functionality tied to each button.
        /// </summary>
        private void AddButtonListeners()
        {
            foreach (var buttonAction in _buttonActions)
            {
                buttonAction.Key.onClick.AddListener(() => buttonAction.Value.Invoke());
            }
        }

        /// <summary>
        /// Toggles the state of text-to-speech functionality.
        /// Activates or deactivates the text-to-speech system based on its current status.
        /// Updates the UI elements to reflect the active or inactive state.
        /// Displays an informational message and triggers speech to notify the user about the change.
        /// </summary>
        private void OnToggleTextToSpeechButton()
        {
            _isTextToSpeechActive = !_isTextToSpeechActive;

            if (_isTextToSpeechActive)
            {
                TextToSpeechManager.Instance.ActivateTTS();
                DisplayInfoMessage(InfoMessages.STARTED_TOGGLETEXTTOSPEECH_BUTTON);
                StartCoroutine(TextToSpeechManager.Instance.Speak("Text wird nun vorgelesen"));
            }
            else
            {
                TextToSpeechManager.Instance.DeactivateTTS();
                DisplayInfoMessage(InfoMessages.STOPPED_TOGGLETEXTTOSPEECH_BUTTON);
                StartCoroutine(TextToSpeechManager.Instance.Speak("Text wird nicht mehr vorgelesen"));
                TextToSpeechManager.Instance.CancelSpeak();
            }
            
            UpdateToggleImages(activeTextToSpeechImage, inactiveTextToSpeechImage, _isTextToSpeechActive);
        }

        /// <summary>
        /// Toggles the sound effects settings between active and inactive states. Updates relevant UI components
        /// to reflect the current state and adjusts global sound volume accordingly.
        /// Handles saving and retrieving the user's sound volume preference from player preferences.
        /// Displays an appropriate message indicating whether sound effects have been activated or deactivated.
        /// </summary>
        private void OnToggleSoundEffectsButton()
        {
            // Toggles sound effects and updates visuals
            _isSoundActive = !_isSoundActive;
            
            UpdateToggleImages(activeSoundEffectsImage, inactiveSoundEffectsImage, _isSoundActive);
            
            SetSliderVisuals(_isSoundActive);
            
            if (!_isSoundActive)
            {
                PlayerPrefs.SetFloat("SavedSoundEffectVolume", _soundEffectVolume);
                GlobalVolumeManager.Instance.SetGlobalVolume(0f);
                _soundEffectVolume = 0f;
                soundEffectsVolumeSlider.value = 0;
                DisplayInfoMessage(InfoMessages.DEACTIVATED_SOUNDEFFECTS_BUTTON);
                PlayerPrefs.SetInt("IsSoundEffectVolumeOn", 0);
            }
            else
            {                    
                _soundEffectVolume = PlayerPrefs.GetFloat("SavedSoundEffectVolume");
                if (_soundEffectVolume == 0f)
                {
                    _soundEffectVolume = 1;
                } else if (_soundEffectVolume < 0.1f)
                {
                    _soundEffectVolume = 0.1f;
                }
                soundEffectsVolumeSlider.value = _soundEffectVolume;
                GlobalVolumeManager.Instance.SetGlobalVolume(_soundEffectVolume);
                DisplayInfoMessage(InfoMessages.ACTIVATED_SOUNDEFFECTS_BUTTON);
                PlayerPrefs.SetInt("IsSoundEffectVolumeOn", 1);
            }

            PlayerPrefs.Save();
        }

        /// <summary>
        /// Updates the font size for the application, ensuring the new value
        /// is applied across all text components. The layout is rebuilt to
        /// accommodate the changes, and a confirmation message is displayed
        /// to the user.
        /// </summary>
        private void SetFontSize()
        {
            FontSizeManager.Instance().SetFontSize(_updatedFontSize);
            FontSizeManager.Instance().UpdateAllTextComponents();
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
            
            DisplayInfoMessage(InfoMessages.CONFIRM_FONT_SIZE_ADJUSTMENT);
        }

        /// <summary>
        /// Updates the font size of the example text dynamically based on the provided slider value.
        /// Calculates the new font size using the slider range and applies it to the relevant text element.
        /// </summary>
        /// <param name="sliderValue">A normalized value (0 to 1) representing the position of the font size slider.</param>
        private void UpdateFontSize(float sliderValue)
        {
            int newFontSize = Mathf.RoundToInt(Mathf.Lerp(MinFontSize, MaxFontSize, sliderValue));

            exampleText.fontSize = newFontSize;

            _updatedFontSize = newFontSize;
        }

        /// <summary>
        /// Updates the visual state of the sound effects volume slider and its associated components
        /// based on the specified active status.
        /// Adjusts colors and interactivity to indicate whether the slider is enabled or disabled.
        /// </summary>
        /// <param name="active">A boolean value indicating if the slider should be active.
        /// When set to true, the slider is enabled with full opacity;
        /// when false, it is disabled and displayed with reduced opacity.</param>
        private void SetSliderVisuals(bool active)
        {
            soundEffectsVolumeSlider.interactable = active;
            Color targetColor = active ? activeColor : disabledColor;
            targetColor.a = active ? 1f : disabledAlpha;

            if (sliderBackgroundImage)
            {
                sliderBackgroundImage.color = targetColor;
            }

            if (sliderFillImage)
            { 
                sliderFillImage.color = targetColor;
            }

            if (sliderHandleImage) 
            {
                sliderHandleImage.color = targetColor;
            }
            
            if (sliderMinIcon)
            {
                sliderMinIcon.color = targetColor;
            }

            if (sliderMaxIcon)
            {
                sliderMaxIcon.color = targetColor;
            }
        }

        /// <summary>
        /// Handles the action triggered when the sound effects volume slider is released.
        /// Updates and saves the user preference for the sound effects volume,
        /// adjusts the global sound level, and plays a sound effect as feedback.
        /// </summary>
        /// <param name="value">The sound effects volume level set by the slider, ranging from 0 to 1.</param>
        private void HandleSoundEffectsSliderReleased(float value)
        {
            PlayerPrefs.SetFloat("SavedSoundEffectVolume", value);
            PlayerPrefs.Save();

            GlobalVolumeManager.Instance.SetGlobalVolume(value);

            _soundEffectVolume = value;

            GlobalVolumeManager.Instance.PlaySound(soundCheckClip);
        }
        
        /// <summary>
        /// Updates the visibility of two images based on a boolean state.
        /// Useful for toggle-like button implementations.
        /// </summary>
        /// <param name="activeImage">The image that should be visible when 'true'.</param>
        /// <param name="inactiveImage">The image that should be invisible when 'true'.</param>
        /// <param name="state">The state (true/false).</param>
        private void UpdateToggleImages(Image activeImage, Image inactiveImage, bool state)
        {
            if (activeImage)
            {
                activeImage.gameObject.SetActive(state);
            }
            if (inactiveImage)
            {
                inactiveImage.gameObject.SetActive(!state);
            }
        }
    }
}