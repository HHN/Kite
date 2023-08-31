using System.Collections.Generic;
using UnityEngine;

public class CommentSectionSceneController : SceneController, OnSuccessHandler
{
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject commentPrefab;
    [SerializeField] private GameObject noCommentPrefab;
    [SerializeField] private GameObject getCommentsServerCallPrefab;
    [SerializeField] private GameObject inputArea;

    private EditCommentButton commentInEdit;
    private string commentEditText;

    private void Start()
    {
        BackStackManager.Instance().Push(SceneNames.COMMENT_SECTION_SCENE);
        this.RequestComments();
        commentInEdit = null;
    }

    private void RequestComments()
    {
        if (PlayManager.Instance().GetVisualNovelToPlay() == null) { return; }

        GetCommentsServerCall call = Instantiate(this.getCommentsServerCallPrefab).GetComponent<GetCommentsServerCall>();
        call.sceneController = this;
        call.onSuccessHandler = this;
        call.visualNovelId = PlayManager.Instance().GetVisualNovelToPlay().id;
        call.SendRequest();
    }

    public void SetCommentInEdit(EditCommentButton commentInEdit)
    {
        this.commentInEdit = commentInEdit;
    }

    public EditCommentButton GetCommentInEdit()
    {
        return commentInEdit;
    }

    public void OnSuccess(Response response)
    {
        List<Comment> comments = response.comments;
        this.UpdatePage(comments);
    }

    public void UpdatePage(List<Comment> comments)
    {
        this.BeforeUpdate();
        this.SetComments(comments);
        this.AfterUpdate();
    }

    public void BeforeUpdate()
    {
        if (this.commentInEdit == null)
        {
            return;
        }
        this.commentEditText = commentInEdit.GetInputText();
    }

    public void AfterUpdate()
    {
        if (this.commentInEdit == null)
        {
            return;
        }
        commentInEdit.ActivateEditMode();
        commentInEdit.SetInputText(commentEditText);
    }

    public void SetComments(List<Comment> comments)
    {
        foreach (Transform transform in content.transform)
        {
            Destroy(transform.gameObject);
        }
        if (comments == null || comments.Count == 0)
        {
            Instantiate(this.noCommentPrefab, this.content.transform);
            return;
        }
        foreach (Comment comment in comments)
        {
            GameObject commentGameObject = Instantiate(this.commentPrefab, this.content.transform);
            UserComment userComment = commentGameObject.GetComponent<UserComment>();
            userComment.Initialize(comment, this);
        }
    }
}
