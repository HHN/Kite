using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuSceneController : SceneController, OnSuccessHandler
{
    public Button novelPlayerButton;
    public Button novelMakerButton;
    public Button logInLogOutButton;
    public Button registerButton;
    public Button settingsButton;
    public Button galleryButton;
    public GameObject logoutServerCall;

    void Start()
    {
        novelPlayerButton.onClick.AddListener(delegate { OnNovelPlayerButton(); });
        novelMakerButton.onClick.AddListener(delegate { OnNovelMakerButton(); });
        registerButton.onClick.AddListener(delegate { OnRegisterButton(); });
        settingsButton.onClick.AddListener(delegate { OnSettingsButton(); });
        galleryButton.onClick.AddListener(delegate { OnGalleryButton(); });

        if (AuthenticationManager.Instance().GetAuthToken() == "")
        { 
            OnGuestMode();
        }
        else
        {
            OnLoggedInUserMode();
        }
    }

    private void OnGuestMode()
    {
        GameManager.Instance().applicationMode = ApplicationModes.GUEST_MODE;
        registerButton.gameObject.SetActive(true);
        settingsButton.gameObject.SetActive(false);
        logInLogOutButton.GetComponentInChildren<TMP_Text>().text = "EINLOGGEN";
        logInLogOutButton.onClick.AddListener(delegate { OnLogInButton(); });
    }

    private void OnLoggedInUserMode()
    {
        GameManager.Instance().applicationMode = ApplicationModes.LOGGED_IN_USER_MODE;
        registerButton.gameObject.SetActive(false);
        settingsButton.gameObject.SetActive(true);
        logInLogOutButton.GetComponentInChildren<TMP_Text>().text = "AUSLOGGEN";
        logInLogOutButton.onClick.AddListener(delegate { OnLogOutButton(); });
    }

    public void OnNovelPlayerButton()
    {
        SceneLoader.LoadSelectNovelScene();
    }

    public void OnNovelMakerButton()
    {
        SceneLoader.LoadGenerateNovelScene();
    }

    public void OnLogInButton()
    {
        SceneLoader.LoadLogInScene();
    }

    public void OnLogOutButton()
    {
        this.DisplayInfoMessage(InfoMessages.WAIT_FOR_LOG_OUT);
        LogOutServerCall call = Instantiate(logoutServerCall).GetComponent<LogOutServerCall>();
        call.sceneController = this;
        call.onSuccessHandler = this;
        call.SendRequest();
    }

    public void OnSettingsButton()
    {
        SceneLoader.LoadSettingsScene();
    }

    public void OnSuccess(Response response)
    {
        messageObject.CloseMessageBox();
        AuthenticationManager.Instance().RemoveAuthTokens();
        OnGuestMode();
    }

    public void OnRegisterButton()
    {
        SceneLoader.LoadRegistrationScene();
    }

    public void OnGalleryButton()
    {
        SceneLoader.LoadGalleryScene();
    }
}
