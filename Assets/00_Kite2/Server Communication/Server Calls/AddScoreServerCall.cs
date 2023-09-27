using UnityEngine.Networking;

public class AddScoreServerCall : ServerCall
{
    public long value;

    protected override object CreateRequestObject()
    {
        AddScoreRequest addScoreRequest = new AddScoreRequest();
        addScoreRequest.value = value;
        return addScoreRequest;
    }

    protected override UnityWebRequest CreateUnityWebRequestObject()
    {
        return UnityWebRequest.Post(ConnectionLink.SCORE_LINK, string.Empty);
    }

    protected override void OnResponse(Response response)
    {
        switch (ResultCodeHelper.ValueOf(response.resultCode))
        {
            case ResultCode.SUCCESSFULLY_UPDATED_SCORE:
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
