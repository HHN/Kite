using _00_Kite2.Common.UI.UI_Elements.Messages;
using _00_Kite2.Common.Utilities;
using UnityEngine;

namespace _00_Kite2.Common
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private GameObject messageBox;
        public GameObject canvas;
        private MessageBox _messageObject;

        public MessageBox DisplayInfoMessage(string infoMessage)
        {
            return DisplayMessage("INFORMATION", infoMessage, false);
        }

        public MessageBox DisplayErrorMessage(string errorMessage)
        {
            return DisplayMessage("FEHLER-MELDUNG", errorMessage, true);
        }

        private MessageBox DisplayMessage(string headline, string message, bool isError)
        {
            if (!_messageObject.IsNullOrDestroyed())
            {
                _messageObject.CloseMessageBox();
            }

            if (canvas.IsNullOrDestroyed())
            {
                return null;
            }

            _messageObject = null;
            _messageObject = Instantiate(messageBox, canvas.transform).GetComponent<MessageBox>();
            _messageObject.SetHeadline(headline);
            _messageObject.SetBody(message);
            _messageObject.SetIsErrorMessage(isError);
            _messageObject.Activate();
            return _messageObject;
        }

        public virtual void OnStop()
        {
        }

        public void CloseMessageBox()
        {
            if (_messageObject == null)
            {
                return;
            }

            _messageObject.CloseMessageBox();
        }
    }
}