using Plugins.Febucci.Text_Animator.Scripts.Runtime.Components.Typewriter._Core;
using TMPro;
using UnityEngine;

namespace Assets._Scripts.Common.UI.UI_Elements.TextBoxes
{
    public class ChatMessageBox : MonoBehaviour
    {
        public TextMeshProUGUI textBox;

        public ChatMessageBox(TypewriterCore typewriter, bool calledFromReload)
        {
            typewriter = typewriter;
            calledFromReload = calledFromReload;
        }

        public void SetMessage(string message)
        {
            bool active = SetInactiveIfMessageIsNull(message);

            if (!active)
            {
                return;
            }

            textBox.text = message;
        }

        private bool SetInactiveIfMessageIsNull(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                this.gameObject.SetActive(false);
                return false;
            }
            else
            {
                this.gameObject.SetActive(true);
                return true;
            }
        }
    }
}