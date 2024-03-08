using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogInSceneController : SceneController, OnSuccessHandler
{
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private Button performLoginButton;
    [SerializeField] private Button forgotPasswordButton;
    [SerializeField] private GameObject loginServerCall;

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
