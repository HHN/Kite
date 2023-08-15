using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangePasswordSceneController : SceneController, OnSuccessHandler
{
    public TMP_InputField oldPasswordInputField;
    public TMP_InputField newPasswordInputField;
    public Button changePasswordButton;
    public GameObject changePasswordServerCall;

    void Start()
    {
        changePasswordButton.onClick.AddListener(delegate { OnChangePasswordButton(); });
    }

    public void OnChangePasswordButton()
    {
        if (oldPasswordInputField.text.Trim() == string.Empty || newPasswordInputField.text.Trim() == string.Empty)
        {
            this.DisplayErrorMessage(ErrorMessages.NOT_EVERYTHING_ENTERED);
        }
        else
        {
            this.DisplayInfoMessage(InfoMessages.WAIT_FOR_CHANGE_OF_PASSWORD);
            ChangePasswortServerCall call = Instantiate(changePasswordServerCall).GetComponent<ChangePasswortServerCall>();
            call.sceneController = this;
            call.onSuccessHandler = this;
            call.oldPassword = oldPasswordInputField.text.Trim();
            call.newPassword = newPasswordInputField.text.Trim();
            call.SendRequest();
        }
    }

    public void OnSuccess(Response response)
    {
        messageObject.CloseMessageBox();
        SceneLoader.LoadMainMenuScene();
    }
}
