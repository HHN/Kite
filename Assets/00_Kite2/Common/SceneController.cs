using UnityEngine;

public class SceneController : MonoBehaviour
{
    public GameObject messageBox;
    public GameObject canvas;
    private MessageBox messageObject;

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
        if (!DestroyValidator.IsNullOrDestroyed(messageObject))
        {
            messageObject.CloseMessageBox();
        }
        if (DestroyValidator.IsNullOrDestroyed(canvas))
        {
            return null;
        }
        messageObject = null;
        messageObject = Instantiate(messageBox, canvas.transform).GetComponent<MessageBox>();
        messageObject.SetHeadline(headline);
        messageObject.SetBody(message);
        messageObject.SetIsErrorMessage(isError);
        messageObject.Activate();
        return messageObject;
    }

    public virtual void OnStop()
    {
    }

    public void CloseMessageBox()
    {
        if (messageObject == null)
        {
            return;
        }
        messageObject.CloseMessageBox();
    }
}
