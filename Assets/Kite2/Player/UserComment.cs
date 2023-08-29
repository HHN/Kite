using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserComment : MonoBehaviour, OnSuccessHandler
{
    public TextMeshProUGUI authorName;
    public TextMeshProUGUI comment;
    public TextMeshProUGUI likeCount;
    public Button likeButton;
    public Button deleteButton;
    public Button editButton;
    public Sprite likeButtonUnselected;
    public Sprite likeButtonSelected;
    private bool liked = false;
    private long commentId;
    public GameObject likeCommentServerCallPrefab;
    public GameObject unlikeCommentServerCallPrefab;
    public CommentSectionSceneController commentSectionSceneController;

    private void Start()
    {
        likeButton.onClick.AddListener(delegate { OnLikeButton(); });
        commentSectionSceneController = GameObject.Find("Controller").GetComponent<CommentSectionSceneController>();

        if (GameManager.Instance().applicationMode != ApplicationModes.LOGGED_IN_USER_MODE) 
        {
            likeButton.interactable = false;
        }
    }

    public void OnLikeButton()
    {
        if (liked)
        {
            SendUnlikeRequest();
        }
        else
        {
            SendLikeRequest();
        }
    }

    public void Initialize(Comment comment)
    {
        authorName.text = comment.author;
        this.comment.text = comment.comment;
        likeCount.text = comment.likeCount.ToString();
        SetLiked(comment.liked);
        commentId = comment.id;

        EditCommentButton editCommentButton = editButton.GetComponent<EditCommentButton>();
        DeleteCommentButton deleteCommentButton = deleteButton.GetComponent<DeleteCommentButton>();
        editCommentButton.commentId = commentId;
        editCommentButton.isOwnComment = comment.isOwnComment;
        editCommentButton.commentText = comment.comment;
        deleteCommentButton.commentId = commentId;

    }

    public void SetLiked(bool liked)
    {
        this.liked = liked;

        if (liked)
        {
            likeButton.image.sprite = likeButtonSelected;

        } 
        else
        {
            likeButton.image.sprite = likeButtonUnselected;
        }
    }

    public void SendLikeRequest()
    {
        LikeCommentServerCall call = Instantiate(likeCommentServerCallPrefab).GetComponent<LikeCommentServerCall>();
        call.sceneController = commentSectionSceneController;
        call.onSuccessHandler = this;
        call.commentId = commentId;
        call.SendRequest();
    }

    public void SendUnlikeRequest()
    {
        UnlikeCommentServerCall call = Instantiate(unlikeCommentServerCallPrefab).GetComponent<UnlikeCommentServerCall>();
        call.sceneController = commentSectionSceneController;
        call.onSuccessHandler = this;
        call.commentId = commentId;
        call.SendRequest();
    }

    public void OnSuccess(Response response)
    {
        List<Comment> comments = response.comments;
        commentSectionSceneController.SetComments(comments);
    }
}
