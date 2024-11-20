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
    [SerializeField] private RectTransform layoutGroupContainer; // Container mit der Vertical Layout Group

    private int minFontSize = 35;   // Minimale Schriftgr��e
    private int maxFontSize = 50;   // Maximale Schriftgr��e

    // Entsprechendes minimales und maximales Spacing
    private float minSpacing = 30f;   // Minimales Spacing
    private float maxSpacing = 40f;  // Maximales Spacing

    private int updatedFontSize = 0;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.BARRIEREFREIHEIT_SCENE);

        LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
        InitializeToggleTextToSpeech();
        InitializeFontSizeSlider();  // Neue Methode zum Initialisieren des Sliders und der Schriftgr��e
        toggleTextToSpeech.onValueChanged.AddListener(delegate { OnToggleTextToSpeech(toggleTextToSpeech); });
        toggleTextToSpeechInfoButton.onClick.AddListener(delegate { OnToggleTextToSpeechInfoButton(); });
        adjustFontSizeInfoButton.onClick.AddListener(delegate { OnAdjustFontSizeInfoButton(); });
        fontSizeSlider.onValueChanged.AddListener(UpdateFontSize);
        confirmButton.onClick.AddListener(delegate { SetFontSize(); });
        FontSizeManager.Instance().UpdateAllTextComponents();
        AdjustSpacing(FontSizeManager.Instance().FontSize);
    }

    public void InitializeToggleTextToSpeech()
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

    private void OnToggleTextToSpeech(Toggle toggle)
    {
        DisplayInfoMessage(InfoMessages.TTS_IS_CURRENTLY_DEACTIVATET);
        toggleTextToSpeech.isOn = false;
        //if (toggleTextToSpeech.isOn)
        //{
        //    TextToSpeechManager.Instance().ActivateTextToSpeech();
        //    DisplayInfoMessage(InfoMessages.STARTED_TOGGLETEXTTOSPEECH_BUTTON);
        //    TextToSpeechService.Instance().TextToSpeechReadLive("Text wird nun vorgelesen", true);
        //}
        //else
        //{
        //    TextToSpeechManager.Instance().DeactivateTextToSpeech();
        //    DisplayInfoMessage(InfoMessages.STOPPED_TOGGLETEXTTOSPEECH_BUTTON);
        //    TextToSpeechService.Instance().TextToSpeechReadLive("Text wird nicht mehr vorgelesen", true);
        //}
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

    // Neue Methode zum Initialisieren des Sliders basierend auf der gespeicherten Schriftgr��e
    private void InitializeFontSizeSlider()
    {
        // Schriftgr��e aus PlayerPrefs laden, Standardwert ist die minimale Schriftgr��e
        int savedFontSize = PlayerPrefs.GetInt("SavedFontSize", minFontSize);

        // Berechne den Slider-Wert basierend auf der gespeicherten Schriftgr��e
        float sliderValue = (float)(savedFontSize - minFontSize) / (maxFontSize - minFontSize);

        // Slider auf den entsprechenden Wert setzen
        fontSizeSlider.value = sliderValue;

        // Text sofort auf die gespeicherte Schriftgr��e setzen
        UpdateFontSize(sliderValue);
    }

    private void UpdateFontSize(float sliderValue)
    {
        // Berechne die neue Schriftgr��e basierend auf dem Slider-Wert
        int newFontSize = Mathf.RoundToInt(Mathf.Lerp(minFontSize, maxFontSize, sliderValue));

        // Falls du TextMeshPro verwendest, setze stattdessen die TMP_Text FontSize
        exampleText.fontSize = newFontSize;

        updatedFontSize = newFontSize;
    }

    private void SetFontSize()
    {
        FontSizeManager.Instance().SetFontSize(updatedFontSize);
        DisplayInfoMessage(InfoMessages.CONFIRM_FONT_SIZE_ADJUSTMENT);
        AdjustSpacing(updatedFontSize);
    }

    private void AdjustSpacing(int fontSize)
    {
        // Schriftgr��e zwischen minFontSize und maxFontSize begrenzen
        fontSize = Mathf.Clamp(fontSize, minFontSize, maxFontSize);

        // Verh�ltnis der Schriftgr��e innerhalb des Bereichs berechnen
        float ratio = (float)(fontSize - minFontSize) / (float)(maxFontSize - minFontSize);

        // Neues Spacing basierend auf dem Verh�ltnis berechnen
        float newSpacing = minSpacing + ratio * (maxSpacing - minSpacing);

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
