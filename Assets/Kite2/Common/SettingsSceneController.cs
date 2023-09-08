using UnityEngine;
using UnityEngine.UI;

public class SettingsSceneController : SceneController, OnSuccessHandler
{
    public Button deleteAccountButton;
    public Button changePasswordButton;
    public Button infoButton;
    public GameObject deleteAccountServerCallPrefab;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.SETTINGS_SCENE);

        deleteAccountButton.onClick.AddListener(delegate { OnDeleteAccountButton(); });
        changePasswordButton.onClick.AddListener(delegate { OnChangePasswordButton(); });
        infoButton.onClick.AddListener(delegate { OnInfoButton(); });

        if (GameManager.Instance().applicationMode == ApplicationModes.LOGGED_IN_USER_MODE)
        {
            deleteAccountButton.gameObject.SetActive(true);
            changePasswordButton.gameObject.SetActive(true);
        } else
        {
            deleteAccountButton.gameObject.SetActive(false);
            changePasswordButton.gameObject.SetActive(false);
        }
    }

    public void OnDeleteAccountButton()
    {
        DeleteAccountServerCall call = Instantiate(deleteAccountServerCallPrefab)
            .GetComponent<DeleteAccountServerCall>();
        call.sceneController = this;
        call.onSuccessHandler = this;
        call.SendRequest();
        DontDestroyOnLoad(call.gameObject);
    }

    public void OnChangePasswordButton()
    {
        SceneLoader.LoadChangePasswordSceneScene();
    }

    public void OnInfoButton()
    {
        SceneLoader.LoadInfoScene();
    }

    public void OnSuccess(Response response)
    {
        GameManager.Instance().LogOut();
        SceneLoader.LoadMainMenuScene();
    }
}
