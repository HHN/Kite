using UnityEngine.Networking;

public class GetReviewObserversServerCall : ServerCall
{
    protected override object CreateRequestObject()
    {
        GetObserversRequest request = new GetObserversRequest();
        return request;
    }

    protected override UnityWebRequest CreateUnityWebRequestObject()
    {
        return UnityWebRequest.Get(ConnectionLink.OBSERVER_LINK);
    }

    protected override void OnResponse(Response response)
    {
        switch (ResultCodeHelper.ValueOf(response.GetResultCode()))
        {
            case ResultCode.SUCCESSFULLY_GOT_ALL_REVIEW_OBSERVER:
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