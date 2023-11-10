using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;

public class RoomMenuSceneController : SceneController, OnSuccessHandler
{

    [SerializeField] private Button leftChairButton1;
    [SerializeField] private Button leftChairButton2;
    [SerializeField] private Button bookButton;
    [SerializeField] private Button flowerPotButton;
    [SerializeField] private Button waterCarafeButton;
    [SerializeField] private Button rightChairButton1;
    [SerializeField] private Button  rightChairButton2;

    public Button novelPlayerButton;
    public Button novelMakerButton;
    public Button logInLogOutButton;
    public Button registerButton;
    public Button settingsButton;
    public GameObject logoutServerCall;
    public Sprite loginSprite;
    public Sprite logoutSprite;
    public Image loginLogoutImage;


    // public Texture2D sourceImage; // Only for colour mask approach

    private TouchControls touchControls;

    // private void Awake() {
    //     touchControls = new TouchControls();
    // }

    // private void OnEnable() {
    //     touchControls.Enable();
    // }

    // private void OnDisable() {
    //     touchControls.Disable();
    // }

    void Start()
    {
        
        BackStackManager.Instance().Clear();
        SceneMemoryManager.Instance().ClearMemory();
        leftChairButton1.onClick.AddListener(delegate {OnNovelPlayerButton();});
        leftChairButton2.onClick.AddListener(delegate {OnNovelPlayerButton();});

        rightChairButton1.onClick.AddListener(delegate {OnNovelMakerButton();});
        rightChairButton2.onClick.AddListener(delegate {OnNovelMakerButton();});

        bookButton.onClick.AddListener(delegate {OnSettingsButton();});
        flowerPotButton.onClick.AddListener(delegate {OnLogInButton();});
        waterCarafeButton.onClick.AddListener(delegate {OnRegisterButton();});

        // sourceImage.filterMode = FilterMode.Point;

        // touchControls.Touch.TouchPress.started += ctx => StartTouch(ctx);
        
 
        if (string.IsNullOrEmpty(AuthenticationManager.Instance().GetAuthToken()))
        { 
            OnGuestMode();
        }
        else
        {
            OnLoggedInUserMode();
        }
    }

    // private void StartTouch(InputAction.CallbackContext context) {
        
    // }

    // private void EndTouch(InputAction.CallbackContext context) {
    //     Debug.Log("Ended touch");
    // }

    private void OnGuestMode()
    {
        GameManager.Instance().applicationMode = ApplicationModes.GUEST_MODE;
        // logInLogOutButton.onClick.AddListener(delegate { OnLogInButton(); });

        MoneyManager.Instance().SetMoney(0);
        ScoreManager.Instance().SetScore(0);
    }

    private void OnLoggedInUserMode()
    {
        GameManager.Instance().applicationMode = ApplicationModes.LOGGED_IN_USER_MODE;
        logInLogOutButton.onClick.AddListener(delegate { OnLogOutButton(); });

        //GetMoneyServerCall getMoneyServerCall = Instantiate(getMoneyServerCallPrefab).GetComponent<GetMoneyServerCall>();
        // getMoneyServerCall.sceneController = this;
        // getMoneyServerCall.onSuccessHandler = new GetMoneySuccessHandler();
        // getMoneyServerCall.SendRequest();
        //DontDestroyOnLoad(getMoneyServerCall.gameObject);

        // GetScoreServerCall getScoreServerCall = Instantiate(getScoreServerCallPrefab).GetComponent<GetScoreServerCall>();
        // getScoreServerCall.sceneController = this;
        // getScoreServerCall.onSuccessHandler = new GetScoreSuccessHandler();
        // getScoreServerCall.SendRequest();
        // DontDestroyOnLoad(getScoreServerCall.gameObject);
    }

    public void OnNovelPlayerButton()
    {
        Debug.Log("OnNovelPlayerButton");
        SceneLoader.LoadNovelExplorerScene();
    }

    public void OnNovelMakerButton()
    {
        Debug.Log("OnNovelMakerButton");
        SceneLoader.LoadGenerateNovelScene();
    }

    public void OnLogInButton()
    {
        Debug.Log("OnLogInButton");
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
        Debug.Log("OnSettingsButton");
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

    // private void OnClick(){
    //      Debug.Log("TEST");
    // }
}
