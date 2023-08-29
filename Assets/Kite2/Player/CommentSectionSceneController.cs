using System.Collections.Generic;
using UnityEngine;

public class CommentSectionSceneController : SceneController, OnSuccessHandler
{
    public GameObject content;
    public GameObject commentPrefab;
    public GameObject noCommentPrefab;
    public GameObject getCommentsServerCallPrefab;
    public GameObject inputArea;

    private void Start()
    {
        BackStackManager.Instance().Push(SceneNames.COMMENT_SECTION_SCENE);
        RequestComments();
    }

    private void RequestComments()
    {
        if (PlayManager.Instance().GetVisualNovelToPlay() == null) { return; }

        GetCommentsServerCall call = Instantiate(getCommentsServerCallPrefab).GetComponent<GetCommentsServerCall>();
        call.sceneController = this;
        call.onSuccessHandler = this;
        call.visualNovelId = PlayManager.Instance().GetVisualNovelToPlay().id;
        call.SendRequest();
    }

    public void OnSuccess(Response response)
    {
        List<Comment> comments = response.comments;
        SetComments(comments);
    }

    public void SetComments(List<Comment> comments)
    {
        foreach (Transform transform in content.transform)
        {
            Destroy(transform.gameObject);
        }
        if (comments == null || comments.Count == 0)
        {
            Instantiate(noCommentPrefab, content.transform);
            return;
        }
        foreach (Comment comment in comments)
        {
            GameObject commentGameObject = Instantiate(commentPrefab, content.transform);
            UserComment userComment = commentGameObject.GetComponent<UserComment>();
            userComment.Initialize(comment);
        }
    }
}
