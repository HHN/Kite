using Assets._Scripts.Controller.CharacterController;
using Assets._Scripts.Controller.SceneControllers;
using Assets._Scripts.Managers;
using Assets._Scripts.Player;
using Assets._Scripts.SaveNovelData;
using Assets._Scripts.SceneManagement;
using Assets._Scripts.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UIElements.Messages
{
    /// <summary>
    /// Manages a message box that appears when a user attempts to leave a novel.
    /// It provides options to continue, pause (save and leave), cancel (delete save and leave),
    /// or end the novel prematurely (mark as completed and leave).
    /// </summary>
    public class LeaveNovelAndGoBackMessageBox : MonoBehaviour
    {
        [Header("Message Box Text Components")] 
        [SerializeField] private TextMeshProUGUI messageBoxHeadline;
        [SerializeField] private TextMeshProUGUI messageBoxBody;

        [Header("Action Buttons")] 
        [SerializeField] private Button continueButton; // Continue with the novel
        [SerializeField] private Button pauseButton; // Pause the novel
        [SerializeField] private Button cancelButton; // Cancel the novel
        [SerializeField] private Button endButton; // End the novel and mark it as completed

        [Header("Background Elements")] 
        [SerializeField] private GameObject backgroundMessageBox;
        [SerializeField] private GameObject backgroundContinue;
        [SerializeField] private GameObject backgroundPause;
        [SerializeField] private GameObject backgroundCancel;
        [SerializeField] private GameObject backgroundEnd;

        [Header("Miscellaneous Elements")] 
        [SerializeField] private GameObject textContinueButton;
        [SerializeField] private GameObject person;
        [SerializeField] private GameObject messageBox;
        
        private ConversationContentGuiController _conversationContentGuiController; // Reference to the PlayNovelSceneController to manage novel actions
        private static PlayNovelSceneController _playNovelSceneController; // Reference to the PlayNovelSceneController to manage novel actions
        
        /// <summary>
        /// Called when the script instance is being loaded.
        /// Initializes button listeners, UI colors, finds relevant controllers,
        /// and cancels any ongoing text-to-speech.
        /// </summary>
        private void Start()
        {
            continueButton.onClick.AddListener(OnContinueButton);
            pauseButton.onClick.AddListener(OnPauseButton);
            cancelButton.onClick.AddListener(OnCancelButton);
            endButton.onClick.AddListener(OnEndButton);

            InitUI();
            FontSizeManager.Instance().UpdateAllTextComponents();

            // Find and assign the PlayNovelSceneController component for novel control actions
            GameObject persistentController = GameObject.Find("PlayNovelSceneController");
            if (persistentController != null)
            {
                _playNovelSceneController = persistentController.GetComponent<PlayNovelSceneController>();
            }
            
            _conversationContentGuiController = FindAnyObjectByType<ConversationContentGuiController>();

            TextToSpeechManager.Instance.CancelSpeak();
        }

        /// <summary>
        /// Initializes the UI elements, particularly setting colors based on the novel's theme.
        /// </summary>
        private void InitUI()
        {
            Color colour = NovelColorManager.Instance().GetColor();
            
            pauseButton.image.color = colour;

            backgroundMessageBox.GetComponent<Image>().color = colour;
            
            backgroundPause.GetComponent<Image>().color = Color.white;
            backgroundCancel.GetComponent<Image>().color = Color.white;
            backgroundEnd.GetComponent<Image>().color = Color.white;

            textContinueButton.GetComponent<TextMeshProUGUI>().color = colour;
        }

        /// <summary>
        /// Activates (makes visible) the message box GameObject.
        /// </summary>
        public void Activate()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Handles the action when the "Continue" button is clicked.
        /// Resumes novel progression and closes the message box.
        /// </summary>
        private void OnContinueButton()
        {
            _playNovelSceneController.isPaused = false; // Resume the novel progression
            StartCoroutine(_playNovelSceneController.ReadLast());
            AnimationFlagSingleton.Instance().SetFlag(false);
            CloseMessageBox();
        }

        /// <summary>
        /// Handles the action when the "Pause" button is clicked.
        /// Saves the novel's current progress and then leaves the novel scene.
        /// </summary>
        private void OnPauseButton()
        {
            SaveLoadManager.SaveNovelData(_playNovelSceneController, _conversationContentGuiController);
            GameManager.Instance.CheckAndSetAllNovelsStatus();
            LeaveNovel();
        }

        /// <summary>
        /// Handles the action when the "Cancel" button is clicked.
        /// Deletes the novel's save data and then leaves the novel scene.
        /// </summary>
        private void OnCancelButton()
        {
            SaveLoadManager.DeleteNovelSaveData(_playNovelSceneController.NovelToPlay.id.ToString());
            GameManager.Instance.CheckAndSetAllNovelsStatus();
            LeaveNovel();
        }

        /// <summary>
        /// Handles the action when the "End" button is clicked.
        /// Adds a special line to the prompt for AI feedback and then triggers the novel ending process.
        /// </summary>
        private void OnEndButton()
        {
            PromptManager.Instance().AddLineToPrompt("Das Gespräch wurde vorzeitig beendet. Bitte beachte, dass kein Teil des Dialogs in das Feedback darf.");

            _playNovelSceneController.HandleEndNovelEvent();
        }

        /// <summary>
        /// Closes and destroys the message box GameObject.
        /// Includes a safety check to prevent errors if the GameObject or script instance is already destroyed.
        /// </summary>
        public void CloseMessageBox()
        {
            if (this.IsNullOrDestroyed() || gameObject.IsNullOrDestroyed())
            {
                return;
            }

            Destroy(gameObject);
        }

        /// <summary>
        /// Static method to handle leaving the novel scene and navigating to the appropriate previous scene.
        /// Manages animation flags, text-to-speech, and scene loading based on the back stack.
        /// </summary>
        private static void LeaveNovel()
        {
            // Disable animations after confirmation
            AnimationFlagSingleton.Instance().SetFlag(false);

            // Cancel any ongoing speech and audio from the Text-to-Speech service
            TextToSpeechManager.Instance.CancelSpeak();

            // Retrieve the last scene for the back button functionality
            string lastScene = SceneRouter.GetTargetSceneForBackButton();

            // Check if there is no last scene, and if so, load the main menu scene
            if (string.IsNullOrEmpty(lastScene))
            {
                SceneLoader.LoadMainMenuScene();
                return;
            }

            // If the last scene is the PlayInstructionScene, load the FoundersBubbleScene instead
            if (lastScene == SceneNames.PlayInstructionScene)
            {
                SceneLoader.LoadScene(SceneNames.FoundersBubbleScene);
                BackStackManager.Instance.Pop(); // Remove the instruction scene from the back stack
                return;
            }

            // Load the last scene retrieved from the back button functionality
            SceneLoader.LoadScene(lastScene);
        }

        /// <summary>
        /// Updates the text and visibility of certain UI elements within the message box,
        /// particularly adjusting the text content and hiding the pause button.
        /// </summary>
        public void HandleButtons()
        {
            pauseButton.gameObject.SetActive(false);
            endButton.gameObject.SetActive(false);

            backgroundMessageBox.GetComponentInChildren<TextMeshProUGUI>().text = "Was möchtest du tun?\n\n<b>Weiterspielen:</b> Die Story fortsetzen.\n<b>Abbrechen:</b> Die Story ohne Speicherung abbrechen.";
        }
    }
}