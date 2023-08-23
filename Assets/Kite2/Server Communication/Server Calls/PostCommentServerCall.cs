using UnityEngine.Networking;

public class PostCommentServerCall : ServerCall
{
    public string comment;
    public long visualNovelId;

    protected override object CreateRequestObject()
    {
        PostCommentRequest postCommentRequest = new PostCommentRequest();
        postCommentRequest.comment = comment;
        postCommentRequest.visualNovelId = visualNovelId;
        return postCommentRequest;
    }

    protected override UnityWebRequest CreateUnityWebRequestObject()
    {
        return UnityWebRequest.Post(ConnectionLink.COMMENT_LINK, string.Empty);
    }

    protected override void OnResponse(Response response)
    {
        switch (ResultCodeHelper.ValueOf(response.resultCode))
        {
            case ResultCode.SUCCESSFULLY_POSTED_COMMENT:
                {
                    onSuccessHandler.OnSuccess(response);
                    return;
                }
            default:
                {
                    sceneController.DisplayErrorMessage(ErrorMessages.UNEXPECTED_SERVER_ERROR);
                    return;
                }
        }
    }
}
