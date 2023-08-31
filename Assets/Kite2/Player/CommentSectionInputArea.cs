using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommentSectionInputArea : MonoBehaviour, OnSuccessHandler
{
    [SerializeField] private Button postButton;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject postCommentServerCallPrefab;
    [SerializeField] private CommentSectionSceneController commentSectionSceneController;

    void Start()
    {
        this.postButton.onClick.AddListener(delegate { this.OnClick(); });

        if (GameManager.Instance().applicationMode != ApplicationModes.LOGGED_IN_USER_MODE)
        {
            this.OnGuestMode();
        }
    }

    public void OnGuestMode()
    {
        this.postButton.interactable = false;
        this.postButton.gameObject.SetActive(false);
        this.inputField.text = "Du musst eingeloggt sein zum kommentieren!";
        this.inputField.interactable = false;
    }

    public void OnClick()
    {
        this.OnPostButton();
    }

    public void OnPostButton()
    {
        if (string.IsNullOrEmpty(this.inputField.text.Trim()))
        {
            this.commentSectionSceneController.DisplayErrorMessage(ErrorMessages.NO_COMMENT_ENTERED);
            return;
        }
        this.SendPostRequest();
    }

    public void SendPostRequest()
    {
        PostCommentServerCall call = Instantiate(this.postCommentServerCallPrefab).GetComponent<PostCommentServerCall>();
        call.sceneController = this.commentSectionSceneController;
        call.onSuccessHandler = this;
        call.visualNovelId = PlayManager.Instance().GetVisualNovelToPlay().id;
        call.comment = this.inputField.text.Trim();
        call.SendRequest();
        this.inputField.text = string.Empty;
    }

    public void OnSuccess(Response response)
    {
        List<Comment> comments = response.comments;
        this.commentSectionSceneController.UpdatePage(comments);
    }
}
