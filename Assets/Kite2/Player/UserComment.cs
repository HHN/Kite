using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserComment : MonoBehaviour
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
    private bool isOwnComment;

    private void Start()
    {
        likeButton.onClick.AddListener(delegate { OnLikeButton(); });

        if (GameManager.Instance().applicationMode != ApplicationModes.LOGGED_IN_USER_MODE) 
        {
            likeButton.interactable = false;
        }
    }

    public void OnLikeButton()
    {
        if (liked)
        {
            SetLiked(false);
            SendUnlikeRequest();
        }
        else
        {
            SetLiked(true);
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
        isOwnComment = comment.isOwnComment;

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

    }

    public void SendUnlikeRequest()
    {

    }
}
