using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserComment : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI authorName;
    [SerializeField] private TextMeshProUGUI comment;
    [SerializeField] private TextMeshProUGUI likeCount;
    [SerializeField] private Button deleteButton;
    [SerializeField] private Button editButton;
    [SerializeField] private Button likeButton;

    private long commentId;
    private CommentSectionSceneController commentSectionSceneController;

    public void Initialize(Comment comment, CommentSectionSceneController controller)
    {
        this.authorName.text = comment.author;
        this.comment.text = comment.comment;
        this.likeCount.text = comment.likeCount.ToString();
        this.commentId = comment.id;
        this.commentSectionSceneController = controller;

        InitializeDeleteButton(comment);
        InitializeEditCommentButton(comment);
        InitializeLikeButton(comment);
    }

    private void InitializeEditCommentButton(Comment comment)
    {
        EditCommentButton editCommentButton = this.editButton.GetComponent<EditCommentButton>();
        editCommentButton.SetCommentId(this.commentId);
        editCommentButton.SetOwnComment(comment.isOwnComment);
        editCommentButton.SetControllerAndCheckForCommentInEdit(this.commentSectionSceneController);
    }

    private void InitializeDeleteButton(Comment comment)
    {
        DeleteCommentButton deleteCommentButton = this.deleteButton.GetComponent<DeleteCommentButton>();
        deleteCommentButton.SetCommentId(this.commentId);
        deleteCommentButton.SetController(this.commentSectionSceneController);
    }

    private void InitializeLikeButton(Comment comment)
    {
        LikeCommentButton likeCommentButton = this.likeButton.GetComponent<LikeCommentButton>();
        likeCommentButton.MarkAsLiked(comment.liked);
        likeCommentButton.SetCommentId(this.commentId);
        likeCommentButton.SetSceneController(this.commentSectionSceneController);

    }
}
