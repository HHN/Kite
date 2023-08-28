using UnityEngine.Networking;

public class UnlikeCommentServerCall : ServerCall
{
    public long commentId;

    protected override object CreateRequestObject()
    {
        UnlikeCommentRequest likeCommentRequest = new UnlikeCommentRequest();
        likeCommentRequest.id = commentId;
        return likeCommentRequest;
    }

    protected override UnityWebRequest CreateUnityWebRequestObject()
    {
        UnityWebRequest request = UnityWebRequest.Delete(ConnectionLink.COMMENT_LIKE_LINK);
        request.downloadHandler = new DownloadHandlerBuffer();
        return request;
    }

    protected override void OnResponse(Response response)
    {
        switch (ResultCodeHelper.ValueOf(response.resultCode))
        {
            case ResultCode.SUCCESSFULLY_UNLIKED_COMMENT:
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
