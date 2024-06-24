using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayInstructionSceneController : SceneController
{
    [SerializeField] private Image background;
    [SerializeField] private Image novelImage;
    [SerializeField] private Image textBoxImage;
    [SerializeField] private Image headerImage;
    [SerializeField] private TextMeshProUGUI novelname;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Color backgroundColor;
    [SerializeField] private Color foregroundColor;

    [SerializeField] private Button playButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Toggle toggle;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.PLAY_INSTRUCTION_SCENE);

        backgroundColor = PlayManager.Instance().GetBackgroundColorOfVisualNovelToPlay();
        foregroundColor = PlayManager.Instance().GetForegroundColorOfVisualNovelToPlay();
        novelname.text = PlayManager.Instance().GetDisplayNameOfNovelToPlay();
        background.color = backgroundColor;
        novelImage.color = foregroundColor;
        textBoxImage.color = foregroundColor;
        buttonText.color = foregroundColor;
        toggle.isOn = false;

        playButton.onClick.AddListener(delegate { OnPlayButton(); });    }

    public void OnPlayButton()
    {
        if (toggle.isOn)
        {
            NeverShowAgain();
        }

        SceneLoader.LoadPlayNovelScene();
    }

    public void NeverShowAgain()
    {
        ShowPlayInstructionManager.Instance().SetShowInstruction(false);
    }

    public void OnBackButton()
    {
        string lastScene = SceneRouter.GetTargetSceneForBackButton();

        if (string.IsNullOrEmpty(lastScene))
        {
            SceneLoader.LoadMainMenuScene();
            return;
        }
        SceneLoader.LoadScene(lastScene);
    }
}
