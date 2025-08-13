using Assets._Scripts.Controller.SceneControllers;
using Assets._Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UndoChoice
{
    /// <summary>
    /// Manages a message box that appears when a user is prompted to confirm
    /// an "undo choice" action within a novel. It allows the user to either
    /// cancel the undo operation or confirm it, restoring a previous state.
    /// </summary>
    public class UndoChoiceMessageBox : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI messageBoxBody;
        [SerializeField] private Button cancelButton;
        [SerializeField] private Button confirmButton;
        [SerializeField] private GameObject background;
        [SerializeField] private GameObject backgroundLeave;
        [SerializeField] private GameObject textStay;
        [SerializeField] private GameObject person;

        private Color _novelColor;
        private PlayNovelSceneController _playNovelSceneController;

        /// <summary>
        /// Called when the script instance is being loaded.
        /// Finds the <see cref="PlayNovelSceneController"/>, sets up button listeners,
        /// initializes the UI's colors, and updates text component font sizes.
        /// </summary>
        private void Start()
        {
            _playNovelSceneController = FindAnyObjectByType<PlayNovelSceneController>();

            cancelButton.onClick.AddListener(OnCancelButton);
            confirmButton.onClick.AddListener(OnConfirmButton);

            InitUI();
            FontSizeManager.Instance().UpdateAllTextComponents();
        }

        /// <summary>
        /// Initializes the UI elements' colors based on the current novel's theme color.
        /// </summary>
        private void InitUI()
        {
            // Retrieve the novel's theme color once to use for multiple elements
            _novelColor = NovelColorManager.Instance().GetColor();

            ApplyColorToUI(background, _novelColor);
            ApplyColorToUI(backgroundLeave, _novelColor);
            textStay.GetComponent<TextMeshProUGUI>().color = _novelColor;
        }

        /// <summary>
        /// Helper method to apply a specified color to a UI element's Image component.
        /// It safely checks if the GameObject and Image component exist.
        /// </summary>
        /// <param name="uiElement">The GameObject containing the Image component.</param>
        /// <param name="color">The color to apply.</param>
        private static void ApplyColorToUI(GameObject uiElement, Color color)
        {
            if (uiElement != null && uiElement.TryGetComponent(out Image image))
            {
                image.color = color;
            }
        }

        /// <summary>
        /// Activates (makes visible) the message box GameObject.
        /// </summary>
        public void Activate()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Handles the click event for the cancel button.
        /// It closes the message box without performing the undo action.
        /// </summary>
        private void OnCancelButton()
        {
            CloseMessageBox();
        }

        /// <summary>
        /// Handles the click event for the confirm button.
        /// It closes the message box and then triggers the <see cref="PlayNovelSceneController"/>
        /// to restore the previous novel state (undo the last choice).
        /// </summary>
        private void OnConfirmButton()
        {
            CloseMessageBox();
            _playNovelSceneController.RestoreChoice();
        }

        /// <summary>
        /// Deactivates the message box GameObject, effectively hiding it.
        /// This approach uses deactivation instead of destruction to allow for potential reuse.
        /// </summary>
        public void CloseMessageBox()
        {
            gameObject.SetActive(false); // Only deactivate instead of destroying
        }
    }
}