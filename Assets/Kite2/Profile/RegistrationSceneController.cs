using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RegistrationSceneController : SceneController, OnSuccessHandler
{
    public TMP_InputField usernameInputField;
    public TMP_InputField emailInputField;
    public TMP_InputField passwordInputField;
    public Button performRegistrationButton;
    public GameObject registerServerCall;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.REGISTRATION_SCENE);

        performRegistrationButton.onClick.AddListener(delegate { OnPerformRegistrationButton(); });
    }

    public void OnPerformRegistrationButton()
    {
        if (usernameInputField.text.Trim() == string.Empty || emailInputField.text.Trim() == string.Empty || passwordInputField.text.Trim() == string.Empty)
        {
            this.DisplayErrorMessage(ErrorMessages.NOT_EVERYTHING_ENTERED);
        }
        else
        {
            this.DisplayInfoMessage(InfoMessages.WAIT_FOR_REGISTRATION);
            RegisterServerCall registerServerCall = Instantiate(this.registerServerCall).GetComponent<RegisterServerCall>();
            registerServerCall.sceneController = this;
            registerServerCall.onSuccessHandler = this;
            registerServerCall.username = usernameInputField.text.Trim();
            registerServerCall.email = emailInputField.text.Trim();
            registerServerCall.password = passwordInputField.text.Trim();
            registerServerCall.SendRequest();
        }
    }

    public void OnSuccess(Response response)
    {
        CloseMessageBox();
        SceneLoader.LoadMainMenuScene();
    }
}
