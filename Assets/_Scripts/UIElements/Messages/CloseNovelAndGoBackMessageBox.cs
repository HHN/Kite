using Assets._Scripts.Managers;
using Assets._Scripts.SceneManagement;
using Assets._Scripts.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UIElements.Messages
{
    /// <summary>
    /// Manages a message box that prompts the user to confirm closing a novel
    /// and navigating back to the main menu. It handles the display of the message
    /// and the actions associated with its buttons.
    /// </summary>
    public class CloseNovelAndGoBackMessageBox : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI messageBoxHeadline;
        [SerializeField] private TextMeshProUGUI messageBoxBody;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button cancelButton;
        [SerializeField] private Button confirmButton;

        /// <summary>
        /// Called when the script instance is being loaded.
        /// Adds listeners to all interactive buttons and updates the font sizes of all text components.
        /// </summary>
        private void Start()
        {
            closeButton.onClick.AddListener(OnCloseButton);
            cancelButton.onClick.AddListener(OnCancelButton);
            confirmButton.onClick.AddListener(OnConfirmButton);
            FontSizeManager.Instance().UpdateAllTextComponents();
        }

        /// <summary>
        /// Handles the action when the "Close" button is clicked.
        /// It simply closes the message box.
        /// </summary>
        private void OnCloseButton()
        {
            CloseMessageBox();
        }

        /// <summary>
        /// Handles the action when the "Cancel" button is clicked.
        /// It simply closes the message box.
        /// </summary>
        private void OnCancelButton()
        {
            CloseMessageBox();
        }

        /// <summary>
        /// Handles the action when the "Confirm" button is clicked.
        /// It initiates loading the main menu scene, effectively closing the current novel.
        /// </summary>
        private void OnConfirmButton()
        {
            SceneLoader.LoadMainMenuScene();
        }

        /// <summary>
        /// Closes and destroys the message box GameObject.
        /// Includes a safety check to prevent errors if the GameObject or script instance is already destroyed.
        /// </summary>
        private void CloseMessageBox()
        {
            if (this.IsNullOrDestroyed() || gameObject.IsNullOrDestroyed())
            {
                return;
            }

            Destroy(gameObject);
        }
    }
}