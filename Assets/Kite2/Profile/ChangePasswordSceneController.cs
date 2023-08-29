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
        BackStackManager.Instance().Push(SceneNames.CHANGE_PASSWORD_SCENE);

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
        SceneLoader.LoadMainMenuScene();
    }
}
