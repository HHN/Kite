using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogInSceneController : SceneController, OnSuccessHandler
{
    public TMP_InputField usernameInputField;
    public TMP_InputField passwordInputField;
    public Button performLoginButton;
    public Button forgotPasswordButton;
    public GameObject loginServerCall;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.LOG_IN_SCENE);

        performLoginButton.onClick.AddListener(delegate { OnPerformLoginButton(); });
        forgotPasswordButton.onClick.AddListener(delegate { OnForgotPasswordButton(); });
    }

    public void OnPerformLoginButton()
    {
        if (usernameInputField.text.Trim() == string.Empty || passwordInputField.text.Trim() == string.Empty)
        {
            this.DisplayErrorMessage(ErrorMessages.NOT_EVERYTHING_ENTERED);
        }
        else
        {
            LogInServerCall call = Instantiate(loginServerCall).GetComponent<LogInServerCall>();
            call.sceneController = this;
            call.onSuccessHandler = this;
            call.username = usernameInputField.text.Trim();
            call.password = passwordInputField.text.Trim();
            call.SendRequest();
            DontDestroyOnLoad(call.gameObject);
        }
    }

    public void OnForgotPasswordButton()
    {
        SceneLoader.LoadResetPasswordScene();
    }

    public void OnSuccess(Response response)
    {
        AuthenticationManager.Instance().SetAuthToken(response.authToken);
        AuthenticationManager.Instance().SetRefreshToken(response.refreshToken);
        SceneLoader.LoadMainMenuScene();
    }
}
