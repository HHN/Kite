using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.Services.Analytics;
using System.Net.Security;
using System.Net;

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
    [SerializeField] private GameObject buttonSoundPrefab;

    [SerializeField] private GameObject termsAndConditionPanel;
    [SerializeField] private Button continuetermsAndConditionsButton;
    [SerializeField] private CustomToggle termsOfUseToggle;
    [SerializeField] private CustomToggle dataPrivacyToggle;
    [SerializeField] private CustomToggle collectDataToggle;
    [SerializeField] private TextMeshProUGUI infoTextTermsAndConditions;

    [SerializeField] private AudioSource kiteAudioLogo;

    public bool generated = false;
    public GenerateNovelPipeline pipeline;

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
        continuetermsAndConditionsButton.onClick.AddListener(delegate { OnContinueTermsAndConditionsButton(); });

        if (PrivacyAndConditionManager.Instance().IsConditionsAccepted() && PrivacyAndConditionManager.Instance().IsPriavcyTermsAccepted())
        {
            termsAndConditionPanel.SetActive(false);
            kiteAudioLogo.Play();

            if (PrivacyAndConditionManager.Instance().IsDataCollectionAccepted())
            {
                AnalyticsServiceHandler.Instance().CollectData();
            }
        }
        if (string.IsNullOrEmpty(AuthenticationManager.Instance().GetAuthToken()))
        { 
            OnGuestMode();
        }
        else
        {
            OnLoggedInUserMode();
        }
    }

    void Update()
    {
        // ---- Generate Novel Pipeline Code ----
        // if (Input.GetKeyDown(KeyCode.G))
        // {
        //     if (generated == false)
        //     {
        //         string userAnswer01 = "Ich will mich selbstständig machen als Spiele-Entwicklerin und habe meiner Mutter davon erzählt. Sie war nicht sehr begeistert. Mein Entwicklungs-Studio heißt Knights Gambit Development Studio.";
        //         string userAnswer02 = "Ich und meine Mutter. Ich bin eine Frau und möchte mich selbstständig machen. Dies habe ich meiner Mutter erzählt. Sie war nicht sehr begeistert davon und hatm ir empfohlen dass ich mich mit meinem Bruder zusammen tue.";
        //         string userAnswer03 = "Meine Mutter war zunächst skeptisch und hat mir empfohlen, dass ich mich mit meinem Bruder zusammen tun sollte. Ihm traut sie die selbstständigkeit wohl er zu, weil er ein Mann ist. Ich habe ihr gesagt, dass ich das auch alleine packen kann und packen will. Am Ende hat sie doch gesagt, dass sie mich dabei unterstützt.";
        //         generated = true;
        //         pipeline.GenerateNovel(userAnswer01, userAnswer02, userAnswer03);
        //     }
        // }
        // ---- Generate Novel Pipeline Code Ende ----
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
        AnalyticsServiceHandler.Instance().SetFromWhereIsNovelSelected("KITE NOVELS");

        GameObject buttonSound = Instantiate(buttonSoundPrefab);
        DontDestroyOnLoad(buttonSound);

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

    public void OnContinueTermsAndConditionsButton()
    {
        bool acceptedTermsOfUse = termsOfUseToggle.IsClicked();
        bool acceptedDataPrivacyTerms = dataPrivacyToggle.IsClicked();
        bool acceptedDataCollection = collectDataToggle.IsClicked();

        if (acceptedTermsOfUse)
        {
            PrivacyAndConditionManager.Instance().AcceptConditionsOfUssage();
        } 
        else
        {
            PrivacyAndConditionManager.Instance().UnacceptConditionsOfUssage();
        }
        if (acceptedDataPrivacyTerms)
        {
            PrivacyAndConditionManager.Instance().AcceptTermsOfPrivacy();
        }
        else
        {
            PrivacyAndConditionManager.Instance().UnacceptTermsOfPrivacy();
        }
        if (acceptedDataCollection)
        {
            PrivacyAndConditionManager.Instance().AcceptDataCollection();
        }
        else
        {
            PrivacyAndConditionManager.Instance().UnacceptDataCollection();
        }

        if (acceptedTermsOfUse && acceptedDataPrivacyTerms)
        {
            SceneLoader.LoadMainMenuScene();
        } 
        else
        {
            infoTextTermsAndConditions.gameObject.SetActive(true);
        }
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
