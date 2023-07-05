using UnityEngine;
using UnityEngine.UI;

public class WelcomeSceneController : SceneController
{
    public Button continueAsGuestButton;
    public Button logInButton;
    public Button registerButton;

    void Start()
    {
        continueAsGuestButton.onClick.AddListener(delegate { OnContinueAsGuestButton(); });
        logInButton.onClick.AddListener(delegate { OnLogInButton(); });
        registerButton.onClick.AddListener(delegate { OnRegisterButton(); });
    }

    public void OnContinueAsGuestButton()
    {
        SceneLoader.LoadMainMenuScene();
    }

    public void OnLogInButton()
    {
        SceneLoader.LoadLogInScene();
    }

    public void OnRegisterButton()
    {
        SceneLoader.LoadRegistrationScene();
    }
}
