using Assets._Scripts.UIElements.Messages;
using Assets._Scripts.Utilities;
using UnityEngine;

namespace Assets._Scripts.Controller.SceneControllers
{
    /// <summary>
    /// Represents a base controller for managing scene behaviors in a Unity application.
    /// Provides core functionality common to all derived scene controllers, such as displaying messages or handling UI interactions.
    /// </summary>
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private GameObject messageBox;
        public GameObject canvas;
        private MessageBox _messageObject;

        /// <summary>
        /// Displays an informational message on the UI.
        /// </summary>
        /// <param name="infoMessage">The informational message to be displayed.</param>
        protected void DisplayInfoMessage(string infoMessage)
        {
            DisplayMessage("INFORMATION", infoMessage, false);
        }

        /// <summary>
        /// Displays an error message on the UI.
        /// </summary>
        /// <param name="errorMessage">The error message to be displayed.</param>
        public void DisplayErrorMessage(string errorMessage)
        {
            DisplayMessage("FEHLER-MELDUNG", errorMessage, true);
        }

        /// <summary>
        /// Displays a message on the UI with a specified headline, message content,
        /// and an option to indicate if it is an error message.
        /// </summary>
        /// <param name="headline">The headline or title of the message.</param>
        /// <param name="message">The content of the message to be displayed.</param>
        /// <param name="isError">A boolean value indicating whether the message represents an error.</param>
        private void DisplayMessage(string headline, string message, bool isError)
        {
            if (!_messageObject.IsNullOrDestroyed())
            {
                _messageObject.CloseMessageBox();
            }

            if (canvas.IsNullOrDestroyed())
            {
                return;
            }

            _messageObject = null;
            _messageObject = Instantiate(messageBox, canvas.transform).GetComponent<MessageBox>();
            _messageObject.SetHeadline(headline);
            _messageObject.SetBody(message);
            _messageObject.SetIsErrorMessage(isError);
            _messageObject.Activate();
        }
    }
}