using Plugins.Febucci.Text_Animator.Scripts.Runtime.Components.Typewriter._Core;
using TMPro;
using UnityEngine;

namespace Assets._Scripts.UIElements.TextBoxes
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