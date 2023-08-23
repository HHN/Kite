using UnityEngine.Networking;

public class ChangeCommentServerCall : ServerCall
{
    public long id;
    public string comment;

    protected override object CreateRequestObject()
    {
        ChangeCommentRequest changeCommentRequest = new ChangeCommentRequest();
        changeCommentRequest.id = id;
        changeCommentRequest.comment = comment;
        return changeCommentRequest;
    }

    protected override UnityWebRequest CreateUnityWebRequestObject()
    {
        return UnityWebRequest.Put(ConnectionLink.COMMENT_LINK, string.Empty);
    }

    protected override void OnResponse(Response response)
    {
        switch (ResultCodeHelper.ValueOf(response.resultCode))
        {
            case ResultCode.SUCCESSFULLY_UPDATED_COMMENT:
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
