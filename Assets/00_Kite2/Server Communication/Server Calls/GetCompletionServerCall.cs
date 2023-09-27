using UnityEngine.Networking;

public class GetCompletionServerCall : ServerCall
{
    public string prompt;

    protected override object CreateRequestObject()
    {
        GptRequest call = new GptRequest();
        call.prompt = prompt;
        return call;
    }

    protected override UnityWebRequest CreateUnityWebRequestObject()
    {
        return UnityWebRequest.Post(ConnectionLink.COMPLETION_LINK, string.Empty);
    }

    protected override void OnResponse(Response response)
    {
        switch (ResultCodeHelper.ValueOf(response.resultCode))
        {
            case ResultCode.SUCCESSFULLY_GOT_COMPLETION:
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
