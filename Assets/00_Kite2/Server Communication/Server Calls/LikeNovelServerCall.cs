using UnityEngine.Networking;

public class LikeNovelServerCall : ServerCall
{
    public long id;

    protected override object CreateRequestObject()
    {
        LikeNovelRequest likeNovelRequest = new LikeNovelRequest();
        likeNovelRequest.id = id;
        return likeNovelRequest;
    }

    protected override UnityWebRequest CreateUnityWebRequestObject()
    {
        return UnityWebRequest.Post(ConnectionLink.NOVEL_LIKE_LINK, string.Empty);
    }

    protected override void OnResponse(Response response)
    {
        switch (ResultCodeHelper.ValueOf(response.resultCode))
        {
            case ResultCode.SUCCESSFULLY_LIKED_NOVEL:
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
