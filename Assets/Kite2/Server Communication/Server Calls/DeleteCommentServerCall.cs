using UnityEngine.Networking;

public class DeleteCommentServerCall : ServerCall
{
    public long id;

    protected override object CreateRequestObject()
    {
        DeleteCommentRequest deleteCommentRequest = new DeleteCommentRequest();
        deleteCommentRequest.id = id;
        return deleteCommentRequest;
    }

    protected override UnityWebRequest CreateUnityWebRequestObject()
    {
        UnityWebRequest request = UnityWebRequest.Delete(ConnectionLink.COMMENT_LINK);
        request.downloadHandler = new DownloadHandlerBuffer();
        return request;
    }

    protected override void OnResponse(Response response)
    {
        switch (ResultCodeHelper.ValueOf(response.resultCode))
        {
            case ResultCode.SUCCESSFULLY_DELETED_COMMENT:
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
