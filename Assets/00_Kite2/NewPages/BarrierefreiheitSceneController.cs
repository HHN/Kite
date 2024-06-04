using UnityEngine;
using UnityEngine.UI;

public class BarrierefreiheitSceneController : SceneController
{
    [SerializeField] private Toggle toggleTextToSpeech;
    [SerializeField] private Button toggleTextToSpeechInfoButton;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private RectTransform layout;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.BARRIEREFREIHEIT_SCENE);

        LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
        InitializeToggleTextToSpeech();
        toggleTextToSpeech.onValueChanged.AddListener(delegate { OnToggleTextToSpeech(toggleTextToSpeech); });
        toggleTextToSpeechInfoButton.onClick.AddListener(delegate { OnToggleTextToSpeechInfoButton(); });
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
    
    public void OnToggleTextToSpeech(Toggle toggle)
    {
        if (toggleTextToSpeech.isOn)
        {
            TextToSpeechManager.Instance().ActivateTextToSpeech();
            DisplayInfoMessage(InfoMessages.STARTED_TOGGLETEXTTOSPEECH_BUTTON);
            TextToSpeechService.Instance().TextToSpeech("textWirdVorgelesen");
        }
        else
        {
            TextToSpeechManager.Instance().DeactivateTextToSpeech();
            DisplayInfoMessage(InfoMessages.STOPPED_TOGGLETEXTTOSPEECH_BUTTON);
            TextToSpeechService.Instance().TextToSpeech("textWirdNichtMehrVorgelesen", true);
        }
    }

    public void OnToggleTextToSpeechInfoButton()
    {
        TextToSpeechService.Instance().TextToSpeech("textToSpeechInfo");
        DisplayInfoMessage(InfoMessages.EXPLANATION_TEXTTOSPEECH_BUTTON);
    }
}
