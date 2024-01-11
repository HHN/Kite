using UnityEngine.Networking;

public class GetNovelReviewsServerCall : ServerCall
{
    protected override object CreateRequestObject()
    {
        GetNovelReviewsRequest request = new GetNovelReviewsRequest();
        return request;
    }

    protected override UnityWebRequest CreateUnityWebRequestObject()
    {
        return UnityWebRequest.Get(ConnectionLink.NOVEL_REVIEW_LINK);
    }

    protected override void OnResponse(Response response)
    {
        switch (ResultCodeHelper.ValueOf(response.resultCode))
        {
            case ResultCode.SUCCESSFULLY_GOT_ALL_NOVEL_REVIEWS:
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