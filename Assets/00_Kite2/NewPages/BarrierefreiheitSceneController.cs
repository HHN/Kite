using _00_Kite2.Common;
using _00_Kite2.Common.Managers;
using _00_Kite2.Common.Messages;
using _00_Kite2.Common.SceneManagement;
using LeastSquares.Overtone;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.NewPages
{
    public class BarrierefreiheitSceneController : SceneController
    {
        [SerializeField] private Toggle toggleTextToSpeech;
        [SerializeField] private Button toggleTextToSpeechInfoButton;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private RectTransform layout;
        [SerializeField] private TTSEngine engine;
        [SerializeField] private Button adjustFontSizeInfoButton;
        [SerializeField] private Slider fontSizeSlider;
        [SerializeField] private TMP_Text exampleText;
        [SerializeField] private Button confirmButton;
        [SerializeField] private RectTransform layoutGroupContainer; // Container mit der Vertical Layout Group

        private const int MinFontSize = 35; // Minimale Schriftgröße
        private const int MaxFontSize = 50; // Maximale Schriftgröße

        // Entsprechendes minimales und maximales Spacing
        private const float MinSpacing = 30f; // Minimales Spacing
        private const float MaxSpacing = 40f; // Maximales Spacing

        private int _updatedFontSize;

        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.BARRIEREFREIHEIT_SCENE);

            LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
            InitializeToggleTextToSpeech();
            InitializeFontSizeSlider(); // Neue Methode zum Initialisieren des Sliders und der Schriftgröße
            toggleTextToSpeech.onValueChanged.AddListener(delegate { OnToggleTextToSpeech(); });
            toggleTextToSpeechInfoButton.onClick.AddListener(OnToggleTextToSpeechInfoButton);
            adjustFontSizeInfoButton.onClick.AddListener(OnAdjustFontSizeInfoButton);
            fontSizeSlider.onValueChanged.AddListener(UpdateFontSize);
            confirmButton.onClick.AddListener(SetFontSize);
            FontSizeManager.Instance().UpdateAllTextComponents();
            AdjustSpacing(FontSizeManager.Instance().FontSize);
        }

        private void InitializeToggleTextToSpeech()
        {
            if (TextToSpeechManager.Instance.IsTextToSpeechActivated())
            {
                toggleTextToSpeech.isOn = true;
            }
            else
            {
                toggleTextToSpeech.isOn = false;
            }
        }

        private void OnToggleTextToSpeech()
        {
            //DisplayInfoMessage(InfoMessages.TTS_IS_CURRENTLY_DEACTIVATED);
            //toggleTextToSpeech.isOn = false;
            if (toggleTextToSpeech.isOn)
            {
                TextToSpeechManager.Instance.ActivateTTS();
                DisplayInfoMessage(InfoMessages.STARTED_TOGGLETEXTTOSPEECH_BUTTON);
                StartCoroutine(TextToSpeechManager.Instance.Speak("Text wird nun vorgelesen"));
            }
            else
            {
                StartCoroutine(TextToSpeechManager.Instance.Speak("Text wird nicht mehr vorgelesen"));
                TextToSpeechManager.Instance.DeactivateTTS();
                DisplayInfoMessage(InfoMessages.STOPPED_TOGGLETEXTTOSPEECH_BUTTON);
            }
        }

        private void OnAdjustFontSizeInfoButton()
        {
            DisplayInfoMessage(InfoMessages.EXPLANATION_ADJUST_FONT_SIZE_BUTTON);
        }

        private void OnToggleTextToSpeechInfoButton()
        {
            //TextToSpeechService.Instance().TextToSpeechReadLive("TextToSpeechInfo", engine);
            DisplayInfoMessage(InfoMessages.EXPLANATION_TEXTTOSPEECH_BUTTON);
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

        private void UpdateFontSize(float sliderValue)
        {
            // Berechne die neue Schriftgröße basierend auf dem Slider-Wert
            int newFontSize = Mathf.RoundToInt(Mathf.Lerp(MinFontSize, MaxFontSize, sliderValue));

            // Falls du TextMeshPro verwendest, setze stattdessen die TMP_Text FontSize
            exampleText.fontSize = newFontSize;

            _updatedFontSize = newFontSize;
        }

        private void SetFontSize()
        {
            FontSizeManager.Instance().SetFontSize(_updatedFontSize);
            DisplayInfoMessage(InfoMessages.CONFIRM_FONT_SIZE_ADJUSTMENT);
            AdjustSpacing(_updatedFontSize);
        }

        private void AdjustSpacing(int fontSize)
        {
            // Schriftgröße zwischen minFontSize und maxFontSize begrenzen
            fontSize = Mathf.Clamp(fontSize, MinFontSize, MaxFontSize);

            // Verhältnis der Schriftgröße innerhalb des Bereichs berechnen
            float ratio = (fontSize - MinFontSize) / (float)(MaxFontSize - MinFontSize);

            // Neues Spacing basierend auf dem Verhältnis berechnen
            float newSpacing = MinSpacing + ratio * (MaxSpacing - MinSpacing);

            // Spacing in der Layout Group aktualisieren
            if (layoutGroupContainer != null)
            {
                VerticalLayoutGroup verticalLayoutGroup = layoutGroupContainer.GetComponent<VerticalLayoutGroup>();
                if (verticalLayoutGroup != null)
                {
                    verticalLayoutGroup.spacing = newSpacing;
                    // Optional: Layout-Update erzwingen
                    LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroupContainer);
                }
                else
                {
                    Debug.LogWarning("Keine VerticalLayoutGroup im angegebenen Container gefunden.");
                }
            }
            else
            {
                Debug.LogWarning("layoutGroupContainer ist nicht zugewiesen.");
            }
        }
    }
}