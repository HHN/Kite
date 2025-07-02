using Assets._Scripts.UIElements.Messages;
using Assets._Scripts.Utilities;
using UnityEngine;

namespace Assets._Scripts.Controller.SceneControllers
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private GameObject messageBox;
        public GameObject canvas;
        private MessageBox _messageObject;

        protected void DisplayInfoMessage(string infoMessage)
        {
            DisplayMessage("INFORMATION", infoMessage, false);
        }

        public void DisplayErrorMessage(string errorMessage)
        {
            DisplayMessage("FEHLER-MELDUNG", errorMessage, true);
        }

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

        protected virtual void OnStop()
        {
        }

        public void CloseMessageBox()
        {
            if (!_messageObject)
            {
                return;
            }

            _messageObject.CloseMessageBox();
        }
    }
}