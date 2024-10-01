using UnityEngine;
using UnityEngine.UI;
using LeastSquares.Overtone;
using TMPro;

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

    private int minFontSize = 10;   // Minimale Schriftgröße
    private int maxFontSize = 50;   // Maximale Schriftgröße

    private int updatedFontSize = 0;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.BARRIEREFREIHEIT_SCENE);

        LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
        InitializeToggleTextToSpeech();
        toggleTextToSpeech.onValueChanged.AddListener(delegate { OnToggleTextToSpeech(toggleTextToSpeech); });
        toggleTextToSpeechInfoButton.onClick.AddListener(delegate { OnToggleTextToSpeechInfoButton(); }); 
        adjustFontSizeInfoButton.onClick.AddListener(delegate { OnAdjustFontSizeInfoButton(); });
        fontSizeSlider.onValueChanged.AddListener(UpdateFontSize);
        confirmButton.onClick.AddListener(delegate { SetFontSize(); });
    }

    public void InitializeToggleTextToSpeech()
    {
        if (TextToSpeechManager.Instance().IsTextToSpeechActivated())
        {
            toggleTextToSpeech.isOn = true;
        }
        else
        {
            toggleTextToSpeech.isOn = false;
        }
    }

    private void OnToggleTextToSpeech(Toggle toggle)
    {
        if (toggleTextToSpeech.isOn)
        {
            TextToSpeechManager.Instance().ActivateTextToSpeech();
            DisplayInfoMessage(InfoMessages.STARTED_TOGGLETEXTTOSPEECH_BUTTON);
            TextToSpeechService.Instance().TextToSpeechReadLive("text wird nun vorgelesen", engine, true);
        }
        else
        {
            TextToSpeechManager.Instance().DeactivateTextToSpeech();
            DisplayInfoMessage(InfoMessages.STOPPED_TOGGLETEXTTOSPEECH_BUTTON);
            TextToSpeechService.Instance().TextToSpeechReadLive("text wird nicht mehr vorgelesen", engine, true);
        }
    }

    private void OnAdjustFontSizeInfoButton()
    {
        DisplayInfoMessage(InfoMessages.EXPLANATION_ADJUST_FONT_SIZE_BUTTON);
    }

    private void OnToggleTextToSpeechInfoButton()
    {
        TextToSpeechService.Instance().TextToSpeechReadLive("textToSpeechInfo", engine);
        DisplayInfoMessage(InfoMessages.EXPLANATION_TEXTTOSPEECH_BUTTON);
    }

    private void UpdateFontSize(float sliderValue)
    {
        // Berechne die neue Schriftgröße basierend auf dem Slider-Wert
        int newFontSize = Mathf.RoundToInt(Mathf.Lerp(minFontSize, maxFontSize, sliderValue));

        // Falls du TextMeshPro verwendest, setze stattdessen die TMP_Text FontSize
        exampleText.fontSize = newFontSize;

        updatedFontSize = newFontSize;
    }

    public void SetFontSize()
    {
        FontSizeManager.Instance().SetFontSize(updatedFontSize);
        DisplayInfoMessage(InfoMessages.CONFIRM_FONT_SIZE_ADJUSTMENT);
    }
}
