using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayInstructionSceneController : SceneController
{
    [SerializeField] private Image background;
    [SerializeField] private Image novelImage;
    [SerializeField] private Image textBoxImage;
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
        Color color = PlayManager.Instance().GetColorOfVisualNovelToPlay();
        novelname.text = PlayManager.Instance().GetVisualNovelToPlay().title;
        foregroundColor = color;
        backgroundColor = new Color(color.r - (19 / 255f), color.g - (41 / 255f), color.b - (8 / 255f));
        background.color = backgroundColor;
        novelImage.color = foregroundColor;
        textBoxImage.color = foregroundColor;
        buttonText.color = foregroundColor;
        toggle.isOn = false;

        playButton.onClick.AddListener(delegate { OnPlayButton(); });
        backButton.onClick.AddListener(delegate { OnBackButton(); });
    }

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
