using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LikeCommentButton : MonoBehaviour, OnSuccessHandler
{
    [SerializeField] private Button button;
    [SerializeField] private Sprite likeButtonUnselected;
    [SerializeField] private Sprite likeButtonSelected;
    [SerializeField] private GameObject likeCommentServerCallPrefab;
    [SerializeField] private GameObject unlikeCommentServerCallPrefab;

    private bool liked;
    private CommentSectionSceneController commentSectionSceneController;
    private long commentId;

    void Start()
    {
        this.button.onClick.AddListener(delegate { OnClick(); });

        if (GameManager.Instance().applicationMode 
            != ApplicationModes.LOGGED_IN_USER_MODE)
        {
            this.button.interactable = false;
        }
    }

    public void SetSceneController(CommentSectionSceneController controller)
    {
        this.commentSectionSceneController = controller;
    }

    public void SetCommentId(long id)
    {
        this.commentId = id;
    }

    public void MarkAsLiked(bool liked)
    {
        this.liked = liked;

        if (this.liked)
        {
            this.button.image.sprite = this.likeButtonSelected;

        }
        else
        {
            this.button.image.sprite = this.likeButtonUnselected;
        }
    }

    public void OnClick()
    {
        if (this.liked)
        {
            this.SendUnlikeRequest();
        }
        else
        {
            this.SendLikeRequest();
        }
    }

    public void SendUnlikeRequest()
    {
        UnlikeCommentServerCall call = Instantiate(this.unlikeCommentServerCallPrefab)
            .GetComponent<UnlikeCommentServerCall>();
        call.sceneController = this.commentSectionSceneController;
        call.onSuccessHandler = this;
        call.commentId = this.commentId;
        call.SendRequest();
    }

    public void SendLikeRequest()
    {
        LikeCommentServerCall call = Instantiate(this.likeCommentServerCallPrefab)
            .GetComponent<LikeCommentServerCall>();
        call.sceneController = this.commentSectionSceneController;
        call.onSuccessHandler = this;
        call.commentId = this.commentId;
        call.SendRequest();
    }

    public void OnSuccess(Response response)
    {
        List<Comment> comments = response.comments;
        this.commentSectionSceneController.UpdatePage(comments);
    }
}
