using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayInstructionSceneController : SceneController
{
    [SerializeField] private Image novelImage;
    [SerializeField] private Image textBoxImage;
    [SerializeField] private Image headerImage;
    [SerializeField] private TextMeshProUGUI novelname;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Color backgroundColor;

    [SerializeField] private Button playButton;
    [SerializeField] private Button playButton2;
    [SerializeField] private Button backButton;
    [SerializeField] private Toggle toggle;
    [SerializeField] private Toggle toggle2;

    private bool isSyncing = false;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.PLAY_INSTRUCTION_SCENE);

        backgroundColor = PlayManager.Instance().GetBackgroundColorOfVisualNovelToPlay();
        novelname.text = PlayManager.Instance().GetDisplayNameOfNovelToPlay();
        novelImage.color = backgroundColor;
        toggle.isOn = false;
        toggle2.isOn = false;

        playButton.onClick.AddListener(delegate { OnPlayButton(); });
        playButton2.onClick.AddListener(delegate { OnPlayButton(); });

        toggle.onValueChanged.AddListener((value) => SyncToggles(toggle, toggle2, value));
        toggle2.onValueChanged.AddListener((value) => SyncToggles(toggle2, toggle, value));

        FontSizeManager.Instance().UpdateAllTextComponents();
    }

    private void SyncToggles(Toggle changedToggle, Toggle otherToggle, bool isOn)
    {
        if (isSyncing) return; // Prevent recursive calls

        isSyncing = true; // Start syncing
        otherToggle.isOn = isOn; // Update the other toggle to match the changed one
        isSyncing = false; // End syncing
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
