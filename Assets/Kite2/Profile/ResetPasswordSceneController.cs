using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResetPasswordSceneController : SceneController, OnSuccessHandler
{
    public TMP_InputField usernameInputField;
    public Button resetPasswordButton;
    public GameObject resetPasswordServerCallPrefab;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.RESET_PASSWORD_SCENE);

        resetPasswordButton.onClick.AddListener(delegate { OnResetPasswordButton(); });
    }

    public void OnResetPasswordButton()
    {
        if (usernameInputField.text.Trim() == string.Empty)
        {
            this.DisplayErrorMessage(ErrorMessages.NOT_EVERYTHING_ENTERED);
        }
        else
        {
            this.DisplayInfoMessage(InfoMessages.WAIT_FOR_RESET_OF_PASSWORD);
            ResetPasswordServerCall call = Instantiate(resetPasswordServerCallPrefab).GetComponent<ResetPasswordServerCall>();
            call.sceneController = this;
            call.onSuccessHandler = this;
            call.username = usernameInputField.text.Trim();
            call.SendRequest();
        }
    }

    public void OnSuccess(Response response)
    {
        CloseMessageBox();
        SceneLoader.LoadMainMenuScene();
    }
}
