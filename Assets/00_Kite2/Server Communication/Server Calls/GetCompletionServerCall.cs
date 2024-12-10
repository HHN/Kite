using _00_Kite2.Common.Messages;
using _00_Kite2.Server_Communication;
using UnityEngine.Networking;
using UnityEngine;

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
        return UnityWebRequest.PostWwwForm(ConnectionLink.COMPLETION_LINK, string.Empty);
    }

    protected override void OnResponse(Response response)
    {
        switch (ResultCodeHelper.ValueOf(response.GetResultCode()))
        {
            case ResultCode.SUCCESSFULLY_GOT_COMPLETION:
                {
                    OnSuccessHandler.OnSuccess(response);
                    return;
                }
            default:
                {
                    if (OnErrorHandler != null)
                    {
                        OnErrorHandler.OnError(response);
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
