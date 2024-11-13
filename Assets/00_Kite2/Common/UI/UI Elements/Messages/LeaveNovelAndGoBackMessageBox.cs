using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaveNovelAndGoBackMessageBox : MonoBehaviour
{
    [Header("Message Box Text Components")]
    [SerializeField] private TextMeshProUGUI messageBoxHeadline;

    [SerializeField] private TextMeshProUGUI messageBoxBody;

    [Header("Action Buttons")]
    [SerializeField] private Button continueButton;  // Continue with the novel

    [SerializeField] private Button pauseButton;     // Pause the novel
    [SerializeField] private Button cancelButton;    // Cancel the novel
    [SerializeField] private Button endButton;       // End the novel and mark it as completed

    [Header("Background Elements")]
    [SerializeField] private GameObject backgroundContinue;

    [SerializeField] private GameObject backgroundPause;
    [SerializeField] private GameObject backgroundCancel;
    [SerializeField] private GameObject backgroundEnd;

    [Header("Miscellaneous Elements")]
    [SerializeField] private GameObject textStay;

    [SerializeField] private GameObject person;
    private ConversationContentGuiController _conversationContentGuiController; // Reference to the PlayNovelSceneController to manage novel actions

    private PlayNovelSceneController _playNovelSceneController; // Reference to the PlayNovelSceneController to manage novel actions

    private void Start()
    {
        continueButton.onClick.AddListener(OnContinueButton);
        pauseButton.onClick.AddListener(OnPauseButton);
        cancelButton.onClick.AddListener(OnCancelButton);
        endButton.onClick.AddListener(OnEndButton);

        InitUI();
        FontSizeManager.Instance().UpdateAllTextComponents();

        _playNovelSceneController = FindAnyObjectByType<PlayNovelSceneController>();
        _conversationContentGuiController = FindAnyObjectByType<ConversationContentGuiController>();
    }

    private void InitUI()
    {
        // Retrieve the colour from the NovelColorManager instance
        Color colour = NovelColorManager.Instance().GetColor();

        backgroundContinue.GetComponent<Image>().color = colour;
        backgroundPause.GetComponent<Image>().color = colour;
        backgroundCancel.GetComponent<Image>().color = colour;
        backgroundEnd.GetComponent<Image>().color = colour;

        textStay.GetComponent<TextMeshProUGUI>().color = colour;
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
    }

    private void OnContinueButton()
    {
        _playNovelSceneController.IsPaused = false; // Resume the novel progression

        this.CloseMessageBox();

        _playNovelSceneController.PlayNextEvent();
    }

    private void OnPauseButton()
    {
        SaveLoadManager.SaveNovelData(_playNovelSceneController, _conversationContentGuiController);
    }

    private void OnCancelButton()
    {
        // Disable animations after confirmation
        AnimationFlagSingleton.Instance().SetFlag(false);

        // Cancel any ongoing speech and audio from the Text-to-Speech service
        TextToSpeechService.Instance().CancelSpeechAndAudio();

        // Retrieve the last scene for the back button functionality
        string lastScene = SceneRouter.GetTargetSceneForBackButton();

        // Check if there is no last scene, and if so, load the main menu scene
        if (string.IsNullOrEmpty(lastScene))
        {
            SceneLoader.LoadMainMenuScene();
            return; // Exit the method after loading the main menu
        }

        // If the last scene is the PLAY_INSTRUCTION_SCENE, load the FOUNDERS_BUBBLE_SCENE instead
        if (lastScene == SceneNames.PLAY_INSTRUCTION_SCENE)
        {
            SceneLoader.LoadScene(SceneNames.FOUNDERS_BUBBLE_SCENE);
            BackStackManager.Instance().Pop(); // Remove the instruction scene from the back stack
            return; // Exit the method after loading the new scene
        }

        // Load the last scene retrieved from the back button functionality
        SceneLoader.LoadScene(lastScene);
    }

    private void OnEndButton()
    {
        PromptManager.Instance().AddLineToPrompt("Das Gespräch wurde vorzeitig beendet. Bitte beachte, dass kein Teil des Dialogs in das Feedback darf.");

        _playNovelSceneController.HandleEndNovelEvent();
    }

    public void CloseMessageBox()
    {
        if (this.IsNullOrDestroyed() || this.gameObject.IsNullOrDestroyed())
        {
            return;
        }
        Destroy(this.gameObject);
    }
}