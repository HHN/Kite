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
    public class SettingsSceneController : SceneController
    {
        [Header("UI References - Text-to-Speech")] 
        [SerializeField] private Button toggleTextToSpeechButton;
        [SerializeField] private Image activeTextToSpeechImage;
        [SerializeField] private Image inactiveTextToSpeechImage;

        [Header("UI References - Sound Effects")]
        [SerializeField] private Button toggleSoundEffectsButton; // Button zum Umschalten der Soundeffekte
        [SerializeField] private Image activeSoundEffectsImage;
        [SerializeField] private Image inactiveSoundEffectsImage;
        [SerializeField] private Slider soundEffectsVolumeSlider; // Dein Lautstärke-Slider
        [SerializeField] private SliderEventHandler soundEffectsSliderHandler;
        [SerializeField] private Image sliderBackgroundImage; // Das Hintergrund-Image des Sliders
        [SerializeField] private Image sliderFillImage; // Das Füll-Image des Sliders
        [SerializeField] private Image sliderHandleImage; // Das Handle-Image des Sliders
        [SerializeField] private Image sliderMinIcon; // Das Notensymbol links vom Slider
        [SerializeField] private Image sliderMaxIcon; // Das Notensymbol rechts vom Slider
        [SerializeField] private AudioClip soundCheckClip;
        
        [Header("UI References - Font Size")]
        [SerializeField] private Slider fontSizeSlider;
        [SerializeField] private TMP_Text exampleText;
        [SerializeField] private Button confirmButton;
        
        [Header("Other UI")]
        [SerializeField] private TMP_Text versionInfo;
        [SerializeField] private RectTransform layout;
        [SerializeField] private RectTransform layoutGroupContainer; // Container mit der Vertical Layout Group

        [Header("Visual States")] 
        [SerializeField] private Color activeColor = Color.white; // Farbe, wenn der Slider aktiv ist
        [SerializeField] private Color disabledColor = Color.gray; // Farbe, wenn der Slider deaktiviert ist (ausgegraut)
        [SerializeField] private float disabledAlpha = 0.5f; // Transparenz, wenn der Slider deaktiviert ist

        private Dictionary<Button, Action> _buttonActions;
        private bool _isTextToSpeechActive;
        private bool _isSoundActive;
        
        private float _soundEffectVolume = 1;
        
        private const int MinFontSize = 35; // Minimale Schriftgröße
        private const int MaxFontSize = 50; // Maximale Schriftgröße
        private int _updatedFontSize;

        private void Awake()
        {
            _isTextToSpeechActive = PlayerPrefs.GetInt("TTS", 1) == 1; // 1 = true, 0 = false
            _isSoundActive = PlayerPrefs.GetInt("IsSoundEffectVolumeOn", 1) == 1; // 1 = true, 0 = false
        }

        public void Start()
        {
            BackStackManager.Instance().Push(SceneNames.SettingsScene);
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
            
            InitializeButtonActions();
            AddButtonListeners();

            // Stelle sicher, dass alle notwendigen UI-Elemente zugewiesen sind.
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
            InitializeFontSizeSlider(); // Neue Methode zum Initialisieren des Sliders und der Schriftgröße

            versionInfo.text = "Version: " + Application.version;
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
        }
        
        private void OnDestroy()
        {
            if (soundEffectsSliderHandler) // Null-Check ist wichtig
            {
                soundEffectsSliderHandler.OnSliderReleasedEvent -= HandleSoundEffectsSliderReleased;
            }
        }

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

        private void AddButtonListeners()
        {
            foreach (var buttonAction in _buttonActions)
            {
                buttonAction.Key.onClick.AddListener(() => buttonAction.Value.Invoke());
            }
        }
        
        private void InitializeSoundEffectsVolumeSlider()
        {
            UpdateToggleImages(activeSoundEffectsImage, inactiveSoundEffectsImage, _isSoundActive);
            
            // Lade die gespeicherte Lautstärke aus PlayerPrefs, Standardwert ist 1 (volle Lautstärke)
            _soundEffectVolume = PlayerPrefs.GetFloat("SavedSoundEffectVolume", 1f);
            soundEffectsVolumeSlider.value = _soundEffectVolume;

            // Setze die Lautstärke des AudioListeners
            GlobalVolumeManager.Instance.SetGlobalVolume(_soundEffectVolume);
        }
        
        // Neue Methode zum Initialisieren des Sliders basierend auf der gespeicherten Schriftgröße
        private void InitializeFontSizeSlider()
        {
            // Schriftgröße aus PlayerPrefs laden, Standardwert ist die minimale Schriftgröße
            int savedFontSize = PlayerPrefs.GetInt("SavedFontSize", MinFontSize);

            // Berechne den Slider-Wert basierend auf der gespeicherten Schriftgröße
            float sliderValue = (float)(savedFontSize - MinFontSize) / (MaxFontSize - MinFontSize);

            // Slider auf den entsprechenden Wert setzen
            fontSizeSlider.value = sliderValue;

            // Text sofort auf die gespeicherte Schriftgröße setzen
            UpdateFontSize(sliderValue);
        }

        /// <summary>
        /// Diese Methode wird aufgerufen, wenn der toggleTextToSpeechButton geklickt wird.
        /// Sie schaltet die Sichtbarkeit von activeTextToSpeechImage und inactiveTextToSpeechImage um.
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
        /// Diese Methode wird aufgerufen, wenn der Sounds-Aktivierungs-Button geklickt wird.
        /// Sie schaltet den internen Zustand 'isSoundActive' um.
        /// </summary>
        private void OnToggleSoundEffectsButton()
        {
            _isSoundActive = !_isSoundActive;
            
            // Setze die Sichtbarkeit der Images basierend auf dem neuen Zustand
            if (activeSoundEffectsImage)
            {
                activeSoundEffectsImage.gameObject.SetActive(_isSoundActive);
            }

            if (inactiveSoundEffectsImage)
            {
                inactiveSoundEffectsImage.gameObject.SetActive(!_isSoundActive);
            }
            
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
        }
        
        private void SetFontSize()
        {
            FontSizeManager.Instance().SetFontSize(_updatedFontSize);
            DisplayInfoMessage(InfoMessages.CONFIRM_FONT_SIZE_ADJUSTMENT);
        }
        
        private void UpdateFontSize(float sliderValue)
        {
            // Berechne die neue Schriftgröße basierend auf dem Slider-Wert
            int newFontSize = Mathf.RoundToInt(Mathf.Lerp(MinFontSize, MaxFontSize, sliderValue));

            // Falls du TextMeshPro verwendest, setze stattdessen die TMP_Text FontSize
            exampleText.fontSize = newFontSize;

            _updatedFontSize = newFontSize;
        }

        /// <summary>
        /// Passt die Farben und die Interaktionsfähigkeit der Slider-Komponenten an.
        /// </summary>
        /// <param name="active">True für aktiven (normalen) Zustand, False für inaktiven (ausgegrauten) Zustand.</param>
        private void SetSliderVisuals(bool active)
        {
            soundEffectsVolumeSlider.interactable = active; // Setzt die Interaktivität des Sliders
            Color targetColor = active ? activeColor : disabledColor;
            // Für das Ausgrauen nutzen wir die Transparenz (Alpha-Wert)
            targetColor.a = active ? 1f : disabledAlpha; // Wenn aktiv, volle Deckkraft, sonst reduzierte

            // Slider Background
            if (sliderBackgroundImage)
            {
                sliderBackgroundImage.color = targetColor;
            }

            // Slider Fill
            if (sliderFillImage)
            { 
                sliderFillImage.color = targetColor;
            }

            // Slider Handle
            if (sliderHandleImage) 
            {
                sliderHandleImage.color = targetColor;
            }

            // Icons neben dem Slider (angenommen TextMeshPro)
            if (sliderMinIcon)
            {
                sliderMinIcon.color = targetColor;
            }

            if (sliderMaxIcon)
            {
                sliderMaxIcon.color = targetColor;
            }
        }
        
        private void HandleSoundEffectsSliderReleased(float value)
        {
            PlayerPrefs.SetFloat("SavedSoundEffectVolume", value);

            GlobalVolumeManager.Instance.SetGlobalVolume(value);

            _soundEffectVolume = value;

            GlobalVolumeManager.Instance.PlaySound(soundCheckClip);
        }
        
        /// <summary>
        /// Aktualisiert die Sichtbarkeit zweier Images basierend auf einem booleschen Zustand.
        /// Nützlich für Toggle-ähnliche Button-Implementierungen.
        /// </summary>
        /// <param name="activeImage">Das Image, das bei 'true' sichtbar sein soll.</param>
        /// <param name="inactiveImage">Das Image, das bei 'true' unsichtbar sein soll.</param>
        /// <param name="state">Der Zustand (true/false).</param>
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