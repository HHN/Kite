using UnityEngine.Networking;

public class GetNovelLikeInformationServerCall : ServerCall
{
    public long id;

    protected override object CreateRequestObject()
    {
        GetNovelLikeInformationRequest getNovelLikeInformationRequest = new GetNovelLikeInformationRequest();
        getNovelLikeInformationRequest.id = id;
        return getNovelLikeInformationRequest;
    }

    protected override UnityWebRequest CreateUnityWebRequestObject()
    {
        return UnityWebRequest.Get(ConnectionLink.NOVEL_LIKE_LINK);
    }

    protected override void OnResponse(Response response)
    {
        switch (ResultCodeHelper.ValueOf(response.resultCode))
        {
            case ResultCode.SUCCESSFULLY_GOT_NOVEL_LIKE_INFORMATION:
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
