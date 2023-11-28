using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MainMenuSceneController : SceneController, OnSuccessHandler
{ 
    public Button novelPlayerButton;
    public Button novelMakerButton;
    public Button logInLogOutButton;
    public Button registerButton;
    public Button settingsButton;
    public Button kiteLogo;
    public GameObject logoutServerCall;
    public Sprite loginSprite;
    public Sprite logoutSprite;
    public Image loginLogoutImage;

    [SerializeField] private GameObject getMoneyServerCallPrefab;
    [SerializeField] private GameObject getScoreServerCallPrefab;

    void Start()
    {
        // Analytics 
        var analytics = AnalyticsServiceHandler.Instance();
        analytics.StartAnalytics();
        

        BackStackManager.Instance().Clear();
        SceneMemoryManager.Instance().ClearMemory();

        novelPlayerButton.onClick.AddListener(delegate { OnNovelPlayerButton(); });
        novelMakerButton.onClick.AddListener(delegate { OnNovelMakerButton(); });
        registerButton.onClick.AddListener(delegate { OnRegisterButton(); });
        settingsButton.onClick.AddListener(delegate { OnSettingsButton(); });
        kiteLogo.onClick.AddListener(delegate { OnKiteLogoButton(); });
 
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
        //registerButton.gameObject.SetActive(true);
        settingsButton.gameObject.SetActive(false);
        logInLogOutButton.GetComponentInChildren<TMP_Text>().text = "EINLOGGEN";
        loginLogoutImage.sprite = loginSprite;
        logInLogOutButton.onClick.AddListener(delegate { OnLogInButton(); });

        MoneyManager.Instance().SetMoney(0);
        ScoreManager.Instance().SetScore(0);
    }

    private void OnLoggedInUserMode()
    {
        GameManager.Instance().applicationMode = ApplicationModes.LOGGED_IN_USER_MODE;
        registerButton.gameObject.SetActive(false);
        settingsButton.gameObject.SetActive(true);
        logInLogOutButton.GetComponentInChildren<TMP_Text>().text = "AUSLOGGEN";
        loginLogoutImage.sprite = logoutSprite;
        logInLogOutButton.onClick.AddListener(delegate { OnLogOutButton(); });

        GetMoneyServerCall getMoneyServerCall = Instantiate(getMoneyServerCallPrefab).GetComponent<GetMoneyServerCall>();
        getMoneyServerCall.sceneController = this;
        getMoneyServerCall.onSuccessHandler = new GetMoneySuccessHandler();
        getMoneyServerCall.SendRequest();
        DontDestroyOnLoad(getMoneyServerCall.gameObject);

        GetScoreServerCall getScoreServerCall = Instantiate(getScoreServerCallPrefab).GetComponent<GetScoreServerCall>();
        getScoreServerCall.sceneController = this;
        getScoreServerCall.onSuccessHandler = new GetScoreSuccessHandler();
        getScoreServerCall.SendRequest();
        DontDestroyOnLoad(getScoreServerCall.gameObject);
    }

    public void OnNovelPlayerButton()
    {
        AnalyticsServiceHandler.Instance().SendMainMenuStatistics();
        SceneLoader.LoadNovelExplorerScene();
    }

    public void OnNovelMakerButton()
    {
        SceneLoader.LoadNovelMakerScene();
    }

    public void OnLogInButton()
    {
        SceneLoader.LoadLogInScene();
    }

    public void OnLogOutButton()
    {
        LogOutServerCall call = Instantiate(logoutServerCall).GetComponent<LogOutServerCall>();
        call.sceneController = this;
        call.onSuccessHandler = this;
        call.SendRequest();
        DontDestroyOnLoad(call.gameObject);
    }

    public void OnSettingsButton()
    {
        SceneLoader.LoadSettingsScene();
    }

    public void OnSuccess(Response response)
    {
        AuthenticationManager.Instance().RemoveAuthTokens();
        OnGuestMode();
    }

    public void OnRegisterButton()
    {
        SceneLoader.LoadRegistrationScene();
    }

    public void OnKiteLogoButton()
    {
        SceneLoader.LoadInfoScene();
    }

    public class GetMoneySuccessHandler : OnSuccessHandler
    {
        public void OnSuccess(Response response)
        {
            MoneyManager.Instance().SetMoney(response.money);
        }
    }

    public class GetScoreSuccessHandler : OnSuccessHandler 
    { 
        public void OnSuccess(Response response) 
        {
            ScoreManager.Instance().SetScore(response.score);
        } 
    }
}
