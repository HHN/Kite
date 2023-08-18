using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatMessageBox : MonoBehaviour
{
    public TextMeshProUGUI textBox;

    public void SetMessage(string message)
    {
        bool active = SetInactiveIfMessageIsNull(message);

        if (!active) { return; }
 
        textBox.text = message;
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
