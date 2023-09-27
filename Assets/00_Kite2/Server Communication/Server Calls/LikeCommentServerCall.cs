using UnityEngine.Networking;

public class LikeCommentServerCall : ServerCall
{
    public long commentId;

    protected override object CreateRequestObject()
    {
        LikeCommentRequest likeCommentRequest = new LikeCommentRequest();
        likeCommentRequest.id = commentId;
        return likeCommentRequest;
    }

    protected override UnityWebRequest CreateUnityWebRequestObject()
    {
        return UnityWebRequest.Post(ConnectionLink.COMMENT_LIKE_LINK, string.Empty);
    }

    protected override void OnResponse(Response response)
    {
        switch (ResultCodeHelper.ValueOf(response.resultCode))
        {
            case ResultCode.SUCCESSFULLY_LIKED_COMMENT:
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
