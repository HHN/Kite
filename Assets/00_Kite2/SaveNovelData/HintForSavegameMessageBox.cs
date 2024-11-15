using _00_Kite2.Player;
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

    [SerializeField]
    private GameObject person; // GameObject representing the person or character related to the message

    private PlayNovelSceneController
        _playNovelSceneController; // Reference to the PlayNovelSceneController to manage novel actions

    private void Awake()
    {
        // Find and assign the PlayNovelSceneController component for novel control actions
        _playNovelSceneController = FindObjectOfType<PlayNovelSceneController>();
    }

    void Start()
    {
        // Attach listeners to buttons to handle click events
        continueButton.onClick.AddListener(OnContinueButton);
        restartButton.onClick.AddListener(OnRestartButton);

        // Initialize UI settings
        InitUI();

        // Update all text components with the proper font size
        FontSizeManager.Instance().UpdateAllTextComponents();
    }

    /// <summary>
    /// Initialize the UI elements by setting up appropriate colors and other properties.
    /// </summary>
    private void InitUI()
    {
        // Retrieve the color from the NovelColorManager instance
        Color color = NovelColorManager.Instance().GetColor();

        // Set the color of the text components to match the selected novel color theme
        textContinue.GetComponent<TextMeshProUGUI>().color = color;
        textRestart.GetComponent<TextMeshProUGUI>().color = color;
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
    private void OnContinueButton()
    {
        _playNovelSceneController.ResumeFromSavedState();
        CloseMessageBox();
    }

    /// <summary>
    /// Handle the event when the restart button is clicked.
    /// </summary>
    private void OnRestartButton()
    {
        _playNovelSceneController.RestartNovel();
        CloseMessageBox();
    }

    /// <summary>
    /// Close and destroy the message box.
    /// </summary>
    public void CloseMessageBox()
    {
        // Ensure the object is not null or destroyed before attempting to destroy it
        if (this.IsNullOrDestroyed() || this.gameObject.IsNullOrDestroyed())
        {
            return;
        }

        Destroy(this.gameObject);
    }
}