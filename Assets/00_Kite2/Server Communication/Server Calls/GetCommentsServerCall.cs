using UnityEngine.Networking;
using UnityEngine;

public class GetCommentsServerCall : ServerCall
{
    public long visualNovelId;

    protected override object CreateRequestObject()
    {
        GetCommentsOfNovelRequest getCommentsRequest = new GetCommentsOfNovelRequest();
        getCommentsRequest.visualNovelId = visualNovelId;
        return getCommentsRequest;
    }

    protected override UnityWebRequest CreateUnityWebRequestObject()
    {
        return UnityWebRequest.Get(ConnectionLink.COMMENT_LINK);
    }

    protected override void OnResponse(Response response)
    {
        switch (ResultCodeHelper.ValueOf(response.resultCode))
        {
            case ResultCode.SUCCESSFULLY_GOT_COMMENTS_FOR_NOVEL:
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
