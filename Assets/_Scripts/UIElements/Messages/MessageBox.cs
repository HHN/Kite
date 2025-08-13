using Assets._Scripts.Managers;
using Assets._Scripts.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UIElements.Messages
{
    /// <summary>
    /// Manages a generic message box UI element, capable of displaying headlines and body text,
    /// playing sounds based on message type (error or info), and handling an "OK" button click.
    /// This component is designed for transient UI notifications.
    /// </summary>
    public class MessageBox : MonoBehaviour
    {
        [SerializeField] private AudioClip errorSound;
        [SerializeField] private AudioClip infoSound;
        [SerializeField] private bool isErrorMessage;
        
        public TextMeshProUGUI messageBoxHeadline;
        public TextMeshProUGUI messageBoxBody;
        public Button okButton;

        /// <summary>
        /// Called when the script instance is being loaded.
        /// Adds a listener to the "OK" button and updates the font sizes of all text components.
        /// </summary>
        private void Start()
        {
            okButton.onClick.AddListener(OnOkButton);
            FontSizeManager.Instance().UpdateAllTextComponents();
        }

        /// <summary>
        /// Sets the text content for the message box's headline.
        /// </summary>
        /// <param name="headline">The string text to set as the headline.</param>
        public void SetHeadline(string headline)
        {
            messageBoxHeadline.text = headline;
        }

        /// <summary>
        /// Sets the text content for the message box's main body.
        /// </summary>
        /// <param name="headline">The string text to set as the body.</param>
        public void SetBody(string headline)
        {
            messageBoxBody.text = headline;
        }

        /// <summary>
        /// Activates (makes visible) the message box GameObject and plays the appropriate sound
        /// (error or info) based on the <see cref="isErrorMessage"/> flag.
        /// </summary>
        public void Activate()
        {
            GlobalVolumeManager.Instance.PlaySound(isErrorMessage ? errorSound : infoSound);

            gameObject.SetActive(true);
        }

        /// <summary>
        /// Handles the action when the "OK" button is clicked.
        /// It simply closes the message box.
        /// </summary>
        private void OnOkButton()
        {
            CloseMessageBox();
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
        /// Sets whether the message box should be treated as an error message.
        /// This affects which sound is played upon activation.
        /// </summary>
        /// <param name="isError">True if it's an error message, false otherwise.</param>
        public void SetIsErrorMessage(bool isError)
        {
            isErrorMessage = isError;
        }
    }
}