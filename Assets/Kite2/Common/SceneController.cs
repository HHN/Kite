using UnityEngine;

public class SceneController : MonoBehaviour
{
    public GameObject messageBox;
    public GameObject canvas;
    public MessageBox messageObject;

    public MessageBox DisplayInfoMessage(string errorMessage)
    {
        return DisplayMessage("INFORMATION", errorMessage);
    }

    public MessageBox DisplayErrorMessage(string errorMessage)
    {
        return DisplayMessage("FEHLER-MELDUNG", errorMessage);
    }

    private MessageBox DisplayMessage(string headline, string message)
    {
        if (!DestroyValidator.IsNullOrDestroyed(messageObject))
        {
            messageObject.CloseMessageBox();
        }
        messageObject = null;
        messageObject = Instantiate(messageBox, canvas.transform).GetComponent<MessageBox>();
        messageObject.SetHeadline(headline);
        messageObject.SetBody(message);
        messageObject.Activate();
        return messageObject;
    }

    public virtual void OnStop()
    {
    }
}
