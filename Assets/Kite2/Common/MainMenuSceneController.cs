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
    public GameObject logoutServerCall;
    public Sprite loginSprite;
    public Sprite logoutSprite;
    public Image loginLogoutImage;

    void Start()
    {
        BackStackManager.Instance().Clear();
        SceneMemoryManager.Instance().ClearMemory();

        novelPlayerButton.onClick.AddListener(delegate { OnNovelPlayerButton(); });
        novelMakerButton.onClick.AddListener(delegate { OnNovelMakerButton(); });
        registerButton.onClick.AddListener(delegate { OnRegisterButton(); });
        settingsButton.onClick.AddListener(delegate { OnSettingsButton(); });
 
        if (string.IsNullOrEmpty(AuthenticationManager.Instance().GetAuthToken()))
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
        loginLogoutImage.sprite = loginSprite;
        logInLogOutButton.onClick.AddListener(delegate { OnLogInButton(); });
    }

    private void OnLoggedInUserMode()
    {
        GameManager.Instance().applicationMode = ApplicationModes.LOGGED_IN_USER_MODE;
        registerButton.gameObject.SetActive(false);
        settingsButton.gameObject.SetActive(true);
        logInLogOutButton.GetComponentInChildren<TMP_Text>().text = "AUSLOGGEN";
        loginLogoutImage.sprite = logoutSprite;
        logInLogOutButton.onClick.AddListener(delegate { OnLogOutButton(); });
    }

    public void OnNovelPlayerButton()
    {
        SceneLoader.LoadNovelExplorerScene();
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
}
