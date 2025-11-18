using Assets._Scripts.Controller.SceneControllers;
using Assets._Scripts.Managers;
using Assets._Scripts.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.SaveNovelData
{
    /// <summary>
    /// Manages a message box that prompts the player to either continue from a saved game state
    /// or restart the novel. It controls the visibility, button interactions, and UI styling.
    /// </summary>
    public class HintForSavegameMessageBox : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI messageBoxBody; // Text component for the body of the message box
        [SerializeField] private Button continueButton; // Button to continue with the novel
        [SerializeField] private Button restartButton; // Button to restart the novel
        [SerializeField] private TextMeshProUGUI textContinue; // GameObject containing the "Continue" text
        [SerializeField] private TextMeshProUGUI textRestart; // GameObject containing the "Restart" text
        [SerializeField] private GameObject person; // GameObject representing the person or character related to the message
       
        private PlayNovelSceneController _playNovelSceneController; // Reference to the PlayNovelSceneController to manage novel actions

        /// <summary>
        /// Called when the script instance is being loaded.
        /// Finds and assigns the <see cref="PlayNovelSceneController"/> component from the scene
        /// for managing novel control actions.
        /// </summary>
        private void Awake()
        {
            // Find and assign the PlayNovelSceneController component for novel control actions
            _playNovelSceneController = FindObjectOfType<PlayNovelSceneController>();
        }

        /// <summary>
        /// Called on the frame when a script is enabled just before any Update methods are called the first time.
        /// Attaches click listeners to the continue and restart buttons, initializes the UI colors,
        /// and updates all text components with the proper font size.
        /// </summary>
        private void Start()
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
            textContinue.color = color;
            textRestart.color = color;
        }

        /// <summary>
        /// Activate the message box to make it visible on the screen.
        /// </summary>
        public void Activate()
        {
            gameObject.SetActive(true);
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
            GameManager.Instance.CheckAndSetAllNovelsStatus();
            CloseMessageBox();
        }

        /// <summary>
        /// Close and destroy the message box.
        /// </summary>
        public void CloseMessageBox()
        {
            // Ensure the object is not null or destroyed before attempting to destroy it
            if (this.IsNullOrDestroyed() || gameObject.IsNullOrDestroyed())
            {
                return;
            }

            Destroy(gameObject);
        }
    }
}