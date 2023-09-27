using UnityEngine.Networking;

public class GetTextOfAudioServerCall : ServerCall
{
    public byte[] file;

    protected override object CreateRequestObject()
    {
        WhisperRequest call = new WhisperRequest();
        call.file = file;
        return call;
    }

    protected override UnityWebRequest CreateUnityWebRequestObject()
    {
        return UnityWebRequest.Post(ConnectionLink.VOICE_TO_TEXT_LINK, string.Empty);
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
