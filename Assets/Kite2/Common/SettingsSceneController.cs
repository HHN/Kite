using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSceneController : SceneController, OnSuccessHandler
{
    public Button deleteAccountButton;
    public GameObject deleteAccountServerCallPrefab;

    void Start()
    {
        deleteAccountButton.onClick.AddListener(delegate { OnDeleteAccountButton(); });

        if (GameManager.Instance().applicationMode == ApplicationModes.LOGGED_IN_USER_MODE)
        {
            deleteAccountButton.gameObject.SetActive(true);
        } else
        {
            deleteAccountButton.gameObject.SetActive(false);
        }
    }

    public void OnDeleteAccountButton()
    {
        this.DisplayInfoMessage(InfoMessages.WAIT_FOR_DELETION_OF_ACCOUNT);
        DeleteAccountServerCall call = Instantiate(deleteAccountServerCallPrefab)
            .GetComponent<DeleteAccountServerCall>();
        call.sceneController = this;
        call.onSuccessHandler = this;
        call.SendRequest();
    }

    public void OnSuccess(Response response)
    {
        messageObject.CloseMessageBox();
        GameManager.Instance().LogOut();
        SceneLoader.LoadMainMenuScene();
    }
}
