using TMPro;
using UnityEngine;

namespace Assets._Scripts.UIElements.TextBoxes
{
    /// <summary>
    /// Manages a chat message box UI element, primarily responsible for setting and displaying text.
    /// It automatically deactivates the message box if the message content is null or empty.
    /// </summary>
    public class ChatMessageBox : MonoBehaviour
    {
        public TextMeshProUGUI textBox;

        /// <summary>
        /// Sets the text message for the chat box.
        /// If the message is null or empty, the message box GameObject will be deactivated.
        /// Otherwise, the message box will be activated, and the text will be displayed.
        /// </summary>
        /// <param name="message">The string content of the message to display.</param>
        public void SetMessage(string message)
        {
            bool active = SetInactiveIfMessageIsNull(message);

            if (!active)
            {
                return;
            }

            textBox.text = message;
        }

        /// <summary>
        /// Helper method to control the active state of the GameObject based on the message content.
        /// </summary>
        /// <param name="message">The message string to check.</param>
        /// <returns>True if the message is not null or empty (and GameObject is set active), false otherwise (and GameObject is set inactive).</returns>
        private bool SetInactiveIfMessageIsNull(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                gameObject.SetActive(false);
                return false;
            }
            else
            {
                gameObject.SetActive(true);
                return true;
            }
        }
    }
}