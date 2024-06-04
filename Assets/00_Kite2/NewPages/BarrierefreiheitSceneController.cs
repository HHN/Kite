using UnityEngine;
using UnityEngine.UI;
using LeastSquares.Overtone;

public class BarrierefreiheitSceneController : SceneController
{
    [SerializeField] private Toggle toggleTextToSpeech;
    [SerializeField] private Button toggleTextToSpeechInfoButton;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TTSEngine engine;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.BARRIEREFREIHEIT_SCENE);

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
        Debug.Log("Button works");
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

    public void OnToggleTextToSpeechInfoButton()
    {
        TextToSpeechService.Instance().TextToSpeech("textToSpeechInfo");
        DisplayInfoMessage(InfoMessages.EXPLANATION_TEXTTOSPEECH_BUTTON);
    }
}
