using UnityEngine.Networking;

public class UnlikeNovelServerCall : ServerCall
{
    public long id;

    protected override object CreateRequestObject()
    {
        UnlikeNovelRequest unlikeNovelRequest = new UnlikeNovelRequest();
        unlikeNovelRequest.id = id;
        return unlikeNovelRequest;
    }

    protected override UnityWebRequest CreateUnityWebRequestObject()
    {
        UnityWebRequest request = UnityWebRequest.Delete(ConnectionLink.NOVEL_LIKE_LINK);
        request.downloadHandler = new DownloadHandlerBuffer();
        return request;
    }

    protected override void OnResponse(Response response)
    {
        switch (ResultCodeHelper.ValueOf(response.resultCode))
        {
            case ResultCode.SUCCESSFULLY_UNLIKED_NOVEL:
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
