using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuSceneController : SceneController, OnSuccessHandler
{
    public Button novelPlayerButton;
    public Button novelMakerButton;
    public Button logOutButton;
    public GameObject logoutServerCall;

    void Start()
    {
        novelPlayerButton.onClick.AddListener(delegate { OnNovelPlayerButton(); });
        novelMakerButton.onClick.AddListener(delegate { OnNovelMakerButton(); });
        logOutButton.onClick.AddListener(delegate { OnLogOutButton(); });

        if (AuthenticationManager.Instance().GetAuthToken() == "")
        {
            logOutButton.GetComponentInChildren<TMP_Text>().text = "ZURÜCK";
            novelMakerButton.gameObject.SetActive(false);
            logOutButton.gameObject.transform.position = novelMakerButton.gameObject.transform.position;
        }
    }

    public void OnNovelPlayerButton()
    {
        SceneLoader.LoadSelectNovelScene();
    }

    public void OnNovelMakerButton()
    {
        SceneLoader.LoadGenerateNovelScene();
    }

    public void OnLogOutButton()
    {
        if (AuthenticationManager.Instance().GetAuthToken() != "")
        {
            this.DisplayInfoMessage(InfoMessages.WAIT_FOR_LOG_OUT);
            LogOutServerCall call = Instantiate(logoutServerCall).GetComponent<LogOutServerCall>();
            call.sceneController = this;
            call.onSuccessHandler = this;
            call.SendRequest();
        }
        else
        {
            SceneLoader.LoadWelcomeScene();
        }
    }

    public void OnSuccess(Response response)
    {
        messageObject.CloseMessageBox();
        AuthenticationManager.Instance().RemoveAuthTokens();
        SceneLoader.LoadWelcomeScene();
    }
}
