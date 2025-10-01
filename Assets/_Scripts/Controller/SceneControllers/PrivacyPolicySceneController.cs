using Assets._Scripts.Managers;
using Assets._Scripts.Messages;
using Assets._Scripts.SceneManagement;
using Assets._Scripts.UIElements.Buttons;
using Assets._Scripts.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Controller.SceneControllers
{
    /// <summary>
    /// Responsible for managing the privacy policy scene in the application.
    /// Inherits from the <c>SceneController</c> base class and provides functionality to handle
    /// the display of the privacy policy or related interactions specific to the privacy policy scene.
    /// </summary>
    public class PrivacyPolicySceneController : SceneController
    {
        [SerializeField] private Button resetAppButton;
        [SerializeField] private Button resetAppInfoButton;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private RectTransform layout;

        [SerializeField] private RectTransform layout02;

        [SerializeField] private DeleteUserDataConfirmation deleteUserDataConfirmDialogObject;
        [SerializeField] private GameObject deleteUserDataConfirmDialog;

        private const string Origin = "reset";

        /// <summary>
        /// Initializes the privacy policy scene when it starts.
        /// This method sets up the backstack for navigation, updates UI components,
        /// initializes the audio source, and configures interactions such as button clicks
        /// and toggle changes.
        /// </summary>
        private void Start()
        {
            BackStackManager.Instance.Push(SceneNames.PrivacyPolicyScene);

            LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout02);
            audioSource = GetComponent<AudioSource>();

            resetAppButton.onClick.AddListener(OnResetAppButton);
            resetAppInfoButton.onClick.AddListener(OnResetAppInfoButton);
            FontSizeManager.Instance().UpdateAllTextComponents();
        }

        /// <summary>
        /// Handles the functionality triggered by pressing the reset application button.
        /// This method manages the deletion confirmation dialog, resets its state if necessary,
        /// and links the dialog to the canvas. It then initializes and activates the confirmation
        /// dialog, ensuring the required setup for reset operations is in place.
        /// </summary>
        private void OnResetAppButton()
        {
            if (!deleteUserDataConfirmDialogObject.IsNullOrDestroyed())
            {
                deleteUserDataConfirmDialogObject.CloseMessageBox();
            }

            if (canvas.IsNullOrDestroyed())
            {
                return;
            }

            deleteUserDataConfirmDialogObject = null;
            deleteUserDataConfirmDialogObject = Instantiate(deleteUserDataConfirmDialog, canvas.transform).GetComponent<DeleteUserDataConfirmation>();
            deleteUserDataConfirmDialogObject.Initialize(Origin);
            deleteUserDataConfirmDialogObject.Activate();

            GameManager.Instance.canvas = canvas;
        }

        /// <summary>
        /// Handles the action performed when the reset app info button is clicked.
        /// This method provides an explanation of the reset app functionality by
        /// using text-to-speech to read the provided explanation aloud and
        /// displaying the same explanation as an informational message on the UI.
        /// </summary>
        private void OnResetAppInfoButton()
        {
            StartCoroutine(TextToSpeechManager.Instance.Speak(InfoMessages.EXPLANATION_RESET_APP_BUTTON));
            DisplayInfoMessage(InfoMessages.EXPLANATION_RESET_APP_BUTTON);
        }
    }
}