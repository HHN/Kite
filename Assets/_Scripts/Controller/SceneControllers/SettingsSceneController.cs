using System;
using System.Collections.Generic;
using Assets._Scripts.Managers;
using Assets._Scripts.Messages;
using Assets._Scripts.SceneManagement;
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
        /// Called when the script instance is being loaded.
        /// Initializes the _isTextToSpeechActive and _isSoundActive fields based on the saved player preferences.
        /// Ensures the persistence of user settings related to text-to-speech and sound effects across sessions.
        /// </summary>
        private void Awake()
        {
            _isTextToSpeechActive = PlayerPrefs.GetInt("TTS") == 1; // 1 = true, 0 = false
            _isSoundActive = PlayerPrefs.GetInt("IsSoundEffectVolumeOn") == 1; // 1 = true, 0 = false
        }

        /// <summary>
        /// Initializes the settings scene and its UI components, ensuring proper configuration and behavior of various
        /// elements such as toggles, sliders, and version display. Adds listeners to interactive components,
        /// sets up sound settings, and updates the text-to-speech toggle visuals.
        /// </summary>
        public void Start()
        {
            BackStackManager.Instance().Push(SceneNames.SettingsScene);
            
            InitializeButtonActions();
            AddButtonListeners();

            if (!toggleTextToSpeechButton || !activeTextToSpeechImage || !inactiveTextToSpeechImage || !soundEffectsVolumeSlider || !sliderBackgroundImage || !sliderFillImage ||
                !sliderHandleImage || !sliderMinIcon || !sliderMaxIcon)
            {
                Debug.LogError("Mindestens eine UI-Referenz für die Sound-Einstellungen ist nicht zugewiesen!", this);
                return;
            }
            
            UpdateToggleImages(activeTextToSpeechImage, inactiveTextToSpeechImage, _isTextToSpeechActive);

            SetSliderVisuals(_isSoundActive);
            soundEffectsSliderHandler.OnSliderReleasedEvent += HandleSoundEffectsSliderReleased;
            
            InitializeSoundEffectsVolumeSlider();
            InitializeFontSizeSlider();

            versionInfo.text = "Version: " + Application.version;
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
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
            _buttonActions = new Dictionary<Button, Action>
            {
                { toggleTextToSpeechButton, OnToggleTextToSpeechButton },
                { toggleSoundEffectsButton, OnToggleSoundEffectsButton },
                { confirmButton, SetFontSize }
            };
            
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
        /// Configures the sound effects volume slider with the saved volume setting and
        /// updates the UI to reflect the current sound state.
        /// Loads the saved sound effects volume from player preferences
        /// and applies it to the slider and global volume manager.
        /// </summary>
        private void InitializeSoundEffectsVolumeSlider()
        {
            UpdateToggleImages(activeSoundEffectsImage, inactiveSoundEffectsImage, _isSoundActive);
            
            _soundEffectVolume = PlayerPrefs.GetFloat("SavedSoundEffectVolume", 1f);
            soundEffectsVolumeSlider.value = _soundEffectVolume;

            GlobalVolumeManager.Instance.SetGlobalVolume(_soundEffectVolume);
        }

        /// <summary>
        /// Initializes the font size slider based on previously saved player preferences.
        /// Retrieves the saved font size or uses the default size if no preferences exist,
        /// calculates the corresponding slider value, and updates the slider and UI text components accordingly.
        /// Forces a layout rebuild to ensure the slider and text components are displayed properly.
        /// </summary>
        private void InitializeFontSizeSlider()
        {
            int savedFontSize = PlayerPrefs.GetInt("SavedFontSize", MinFontSize);

            float sliderValue = (float)(savedFontSize - MinFontSize) / (MaxFontSize - MinFontSize);

            fontSizeSlider.value = sliderValue;

            FontSizeManager.Instance().UpdateAllTextComponents();
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
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
            
            if (activeSoundEffectsImage)
            {
                activeSoundEffectsImage.gameObject.SetActive(_isSoundActive);
            }

            if (inactiveSoundEffectsImage)
            {
                inactiveSoundEffectsImage.gameObject.SetActive(!_isSoundActive);
            }
            
            SetSliderVisuals(_isSoundActive);
            
            // Handles turning sound effects off
            if (!_isSoundActive)
            {
                PlayerPrefs.SetFloat("SavedSoundEffectVolume", _soundEffectVolume);
                GlobalVolumeManager.Instance.SetGlobalVolume(0f);
                _soundEffectVolume = 0f;
                soundEffectsVolumeSlider.value = 0;
                DisplayInfoMessage(InfoMessages.DEACTIVATED_SOUNDEFFECTS_BUTTON);
                PlayerPrefs.SetInt("IsSoundEffectVolumeOn", 0);
            }
            // Handles turning sound effects on
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
            // Berechne die neue Schriftgröße basierend auf dem Slider-Wert
            int newFontSize = Mathf.RoundToInt(Mathf.Lerp(MinFontSize, MaxFontSize, sliderValue));

            // Falls du TextMeshPro verwendest, setze stattdessen die TMP_Text FontSize
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