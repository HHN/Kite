using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;
using System.IO;

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

    private PlayNovelSceneController playNovelSceneController; // Reference to the PlayNovelSceneController to manage novel actions
    private ConversationContentGuiController conversationContentGuiController; // Reference to the PlayNovelSceneController to manage novel actions


    void Start()
    {
        continueButton.onClick.AddListener(delegate { OnContinueButton(); });
        pauseButton.onClick.AddListener(delegate { OnPauseButton(); });
        cancelButton.onClick.AddListener(delegate { OnCancelButton(); });
        endButton.onClick.AddListener(delegate { OnEndButton(); });

        InitUI();
        FontSizeManager.Instance().UpdateAllTextComponents();

        playNovelSceneController = FindAnyObjectByType<PlayNovelSceneController>();
        conversationContentGuiController = FindAnyObjectByType<ConversationContentGuiController>();
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

    public void SetHeadline(string headline)
    {
        messageBoxHeadline.text = headline;
    }

    public void SetBody(string headline)
    {
        messageBoxBody.text = headline;
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
    }

    public void OnContinueButton()
    {
        playNovelSceneController.IsPaused = false; // Resume the novel progression

        this.CloseMessageBox();

        playNovelSceneController.PlayNextEvent();
    }

    public void OnPauseButton()
    {
        Debug.Log("OnPauseButton");

        // PlaythroughHistory speichern

        // nextEventId speichern

        // Speichere den Spielstand über den SaveLoadManager
        SaveLoadManager.SaveNovelData(playNovelSceneController, conversationContentGuiController);
    }

    public void OnCancelButton()
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
        if (lastScene == SceneNames.PLAY_INSTRUCTION_SCENE.ToString())
        {
            SceneLoader.LoadScene(SceneNames.FOUNDERS_BUBBLE_SCENE.ToString());
            BackStackManager.Instance().Pop(); // Remove the instruction scene from the back stack
            return; // Exit the method after loading the new scene
        }

        // Load the last scene retrieved from the back button functionality
        SceneLoader.LoadScene(lastScene);
    }

    public void OnEndButton()
    {
        //playNovelSceneController.playThroughHistory.Add("Das Gespräch wurde vorzeitig beendet. Bitte beachte, dass kein Teil des Dialogs in das Feedback darf.");

        PromptManager.Instance().AddLineToPrompt("Das Gespräch wurde vorzeitig beendet. Bitte beachte, dass kein Teil des Dialogs in das Feedback darf.");

        playNovelSceneController.HandleEndNovelEvent();
    }

    public void CloseMessageBox()
    {
        if (DestroyValidator.IsNullOrDestroyed(this) || DestroyValidator.IsNullOrDestroyed(this.gameObject))
        {
            return;
        }
        Destroy(this.gameObject);
    }
}