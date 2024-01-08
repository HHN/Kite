using UnityEngine.Networking;
using UnityEngine;

public class GetCompletionServerCall : ServerCall
{
    public string prompt;

    protected override object CreateRequestObject()
    {
        GptRequest call = new GptRequest(); 
        call.prompt = prompt;
        Debug.Log("Prompt: " + prompt);
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
                    if (onErrorHandler != null)
                    {
                        onErrorHandler.OnError(response);
                    } 
                    else
                    {
                        sceneController.DisplayErrorMessage(ErrorMessages.UNEXPECTED_SERVER_ERROR);
                    }
                    return;
                }
        }
    }
}
