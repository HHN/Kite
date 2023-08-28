using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteCommentButton : MonoBehaviour, OnSuccessHandler
{
    public long commentId;
    public Button button;
    public GameObject deleteCommentServerCallPrefab;
    public CommentSectionSceneController commentSectionSceneController;
    public GameObject parent;

    private void Start()
    {
        if (GameManager.Instance().applicationMode != ApplicationModes.LOGGED_IN_USER_MODE)
        {
            parent.SetActive(false);
            return;
        }
        button.onClick.AddListener(delegate { OnClick(); });
        commentSectionSceneController = GameObject.Find("Controller").GetComponent<CommentSectionSceneController>();
    }

    public void OnClick()
    {
        commentSectionSceneController.DisplayInfoMessage(InfoMessages.WAIT_FOR_DELETE_COMMENT);
        DeleteCommentServerCall call = Instantiate(deleteCommentServerCallPrefab).GetComponent<DeleteCommentServerCall>();
        call.sceneController = commentSectionSceneController;
        call.onSuccessHandler = this;
        call.id = commentId;
        call.SendRequest();
    }

    public void OnSuccess(Response response)
    {
        commentSectionSceneController.messageObject.CloseMessageBox();
        List<Comment> comments = response.comments;
        commentSectionSceneController.SetComments(comments);
    }
}
