using UnityEngine.Networking;

public class AddReviewObserverServerCall : ServerCall
{
    public string email;

    protected override object CreateRequestObject()
    {
        AddObserverRequest request = new AddObserverRequest();
        request.email = email;
        return request;
    }

    protected override UnityWebRequest CreateUnityWebRequestObject()
    {
        return UnityWebRequest.Post(ConnectionLink.OBSERVER_LINK, string.Empty);
    }

    protected override void OnResponse(Response response)
    {
        switch (ResultCodeHelper.ValueOf(response.resultCode))
        {
            case ResultCode.SUCCESSFULLY_ADDED_REVIEW_OBSERVER:
                {
                    onSuccessHandler.OnSuccess(response);
                    return;
                }
            case ResultCode.REVIEW_OBSERVER_ALREADY_EXISTS:
                {
                    sceneController.DisplayInfoMessage(ErrorMessages.REVIEW_OBSERVER_ALREADY_EXISTS);
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