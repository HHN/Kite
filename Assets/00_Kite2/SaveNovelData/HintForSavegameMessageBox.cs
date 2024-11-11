using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HintForSavegameMessageBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageBoxHeadline; // Text component for the headline of the message box
    [SerializeField] private TextMeshProUGUI messageBoxBody; // Text component for the body of the message box
    [SerializeField] private Button continueButton; // Button to continue with the novel
    [SerializeField] private Button restartButton; // Button to restart the novel
    [SerializeField] private GameObject textContinue; // GameObject containing the "Continue" text
    [SerializeField] private GameObject textRestart; // GameObject containing the "Restart" text
    [SerializeField] private GameObject person; // GameObject representing the person or character related to the message

    private PlayNovelSceneController playNovelSceneController; // Reference to the PlayNovelSceneController to manage novel actions
    private FoundersBubbleSceneController foundersBubbleSceneController;
    private NovelDescriptionTextbox novelDescriptionTextbox;

    void Start()
    {
        // Attach listeners to buttons to handle click events
        continueButton.onClick.AddListener(delegate { OnContinueButton(); });
        restartButton.onClick.AddListener(delegate { OnRestartButton(); });

        // Initialize UI settings
        InitUI();

        // Update all text components with the proper font size
        FontSizeManager.Instance().UpdateAllTextComponents();

        // Find and assign the PlayNovelSceneController component for novel control actions
        playNovelSceneController = GameObject.Find("Controller").GetComponent<PlayNovelSceneController>();

        //foundersBubbleSceneController = FindAnyObjectByType<FoundersBubbleSceneController>();
        //novelDescriptionTextbox = FindAnyObjectByType<NovelDescriptionTextbox>();
    }

    /// <summary>
    /// Initialize the UI elements by setting up appropriate colors and other properties.
    /// </summary>
    private void InitUI()
    {
        // Log the current color from the NovelColorManager instance to the console
        Debug.Log("NovelColorManager.Instance().GetColor(): " + NovelColorManager.Instance().GetColor());

        // Retrieve the color from the NovelColorManager instance
        Color color = NovelColorManager.Instance().GetColor();

        // Set the color of the text components to match the selected novel color theme
        textContinue.GetComponent<TextMeshProUGUI>().color = color;
        textRestart.GetComponent<TextMeshProUGUI>().color = color;
    }

    /// <summary>
    /// Set the headline text for the message box.
    /// </summary>
    /// <param name="headline">The headline text to be displayed in the message box.</param>
    public void SetHeadline(string headline)
    {
        messageBoxHeadline.text = headline;
    }

    /// <summary>
    /// Set the body text for the message box.
    /// </summary>
    /// <param name="body">The body text to be displayed in the message box.</param>
    public void SetBody(string body)
    {
        messageBoxBody.text = body;
    }

    /// <summary>
    /// Activate the message box to make it visible on the screen.
    /// </summary>
    public void Activate()
    {
        this.gameObject.SetActive(true);
    }

    /// <summary>
    /// Handle the event when the continue button is clicked.
    /// </summary>
    public void OnContinueButton()
    {
        //Debug.Log("[HintForSavegameMessageBox] - [OnContinueButton] - Continue the novel.");

        //// Beispiel: Überprüfen und Setzen des Flags für eine bestimmte Novel
        //GameManager.Instance.SetNovelSavedFlag(novelDescriptionTextbox.NovelToPlay.title.ToString(), true);

        //LoadNovelScene();

        Debug.Log("Fortsetzen an der letzten gespeicherten Stelle.");
        playNovelSceneController.ResumeFromSavedState();
        CloseMessageBox();
    }

    /// <summary>
    /// Handle the event when the restart button is clicked.
    /// </summary>
    public void OnRestartButton()
    {
        //Debug.Log("[HintForSavegameMessageBox] - [OnRestartButton] - Restart the novel.");

        //GameManager.Instance.SetNovelSavedFlag(novelDescriptionTextbox.NovelToPlay.title.ToString(), false);

        //LoadNovelScene();

        Debug.Log("Neustart der Novel.");
        playNovelSceneController.RestartNovel();
        CloseMessageBox();
    }

    /// <summary>
    /// Close and destroy the message box.
    /// </summary>
    public void CloseMessageBox()
    {
        // Ensure the object is not null or destroyed before attempting to destroy it
        if (DestroyValidator.IsNullOrDestroyed(this) || DestroyValidator.IsNullOrDestroyed(this.gameObject))
        {
            return;
        }

        Destroy(this.gameObject);
    }

    private void LoadNovelScene()
    {
        PlayManager.Instance().SetVisualNovelToPlay(novelDescriptionTextbox.NovelToPlay);
        PlayManager.Instance().SetForegroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetForegrundColorOfNovel(novelDescriptionTextbox.NovelName));
        PlayManager.Instance().SetBackgroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(novelDescriptionTextbox.NovelName));
        PlayManager.Instance().SetDiplayNameOfNovelToPlay(FoundersBubbleMetaInformation.GetDisplayNameOfNovelToPlay(novelDescriptionTextbox.NovelName));
        GameObject buttonSound = Instantiate(novelDescriptionTextbox.SelectNovelSoundPrefab);
        DontDestroyOnLoad(buttonSound);

        if (ShowPlayInstructionManager.Instance().ShowInstruction())
        {
            SceneLoader.LoadPlayInstructionScene();

        }
        else
        {
            SceneLoader.LoadPlayNovelScene();
        }
        return;
    }
}