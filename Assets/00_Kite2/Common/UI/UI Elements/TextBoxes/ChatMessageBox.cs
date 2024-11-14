using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatMessageBox : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    // public TypewriterCore typewriter;
    // public bool calledFromReload = false;

    public ChatMessageBox(TypewriterCore typewriter, bool calledFromReload)
    {
        typewriter = typewriter;
        calledFromReload = calledFromReload;
    }

    public void SetMessage(string message)
    {
        bool active = SetInactiveIfMessageIsNull(message);

        if (!active) { return; }
 
        textBox.text = message;

        // if (calledFromReload)
        // {
        //     typewriter.SkipTypewriter();
        // }
    }

    public bool SetInactiveIfMessageIsNull(string message)
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
