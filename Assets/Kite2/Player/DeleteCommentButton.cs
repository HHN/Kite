using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteCommentButton : MonoBehaviour, OnSuccessHandler
{
    [SerializeField] private Button button;
    [SerializeField] private GameObject deleteCommentServerCallPrefab;
    [SerializeField] private GameObject parent;

    private long commentId;
    private CommentSectionSceneController commentSectionSceneController;

    private void Start()
    {
        if (GameManager.Instance().applicationMode != ApplicationModes.LOGGED_IN_USER_MODE)
        {
            this.parent.SetActive(false);
            return;
        }
        this.button.onClick.AddListener(delegate { this.OnClick(); });
    }

    public void SetCommentId(long id)
    {
        this.commentId = id;
    }

    public void SetController(CommentSectionSceneController controller)
    {
        this.commentSectionSceneController = controller;
    }

    public void OnClick()
    {
        DeleteCommentServerCall call = Instantiate(this.deleteCommentServerCallPrefab).GetComponent<DeleteCommentServerCall>();
        call.sceneController = this.commentSectionSceneController;
        call.onSuccessHandler = this;
        call.id = this.commentId;
        call.SendRequest();
    }

    public void OnSuccess(Response response)
    {
        List<Comment> comments = response.comments;
        this.commentSectionSceneController.UpdatePage(comments);
    }
}
