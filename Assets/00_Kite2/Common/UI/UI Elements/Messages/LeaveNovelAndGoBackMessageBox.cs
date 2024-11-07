using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;

public class LeaveNovelAndGoBackMessageBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageBoxHeadline;
    [SerializeField] private TextMeshProUGUI messageBoxBody;
    [SerializeField] private Button continueButton; // Continue with the novel
    [SerializeField] private Button cancelButton; // Cancel the novel
    [SerializeField] private Button endButton; // End the novel and mark it as completed
    [SerializeField] private GameObject backgroundContinue;
    [SerializeField] private GameObject backgroundCancel;
    [SerializeField] private GameObject backgroundEnd;
    [SerializeField] private GameObject textStay;
    [SerializeField] private GameObject person;

    private PlayNovelSceneController playNovelSceneController; // Reference to the PlayNovelSceneController to manage novel actions

    void Start()
    {
        continueButton.onClick.AddListener(delegate { OnContinueButton(); });
        cancelButton.onClick.AddListener(delegate { OnCancelButton(); });
        endButton.onClick.AddListener(delegate { OnEndButton(); });

        InitUI();
        FontSizeManager.Instance().UpdateAllTextComponents();

        // Find and assign the PlayNovelSceneController component for novel control actions
        playNovelSceneController = GameObject.Find("Controller").GetComponent<PlayNovelSceneController>();
    }

    private void InitUI()
    {
        // Retrieve the colour from the NovelColorManager instance
        Color colour = NovelColorManager.Instance().GetColor();

        backgroundContinue.GetComponent<Image>().color = colour;
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
        playNovelSceneController.isPaused = false; // Resume the novel progression

        this.CloseMessageBox();

        playNovelSceneController.PlayNextEvent();
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
        playNovelSceneController.playThroughHistory.Add("Das Gespräch wurde vorzeitig beendet.");

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