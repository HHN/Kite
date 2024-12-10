using _00_Kite2.Common.Messages;
using _00_Kite2.Server_Communication;
using UnityEngine.Networking;

public class GetDataServerCall : ServerCall
{
    protected override object CreateRequestObject()
    {
        return null;
    }

    protected override UnityWebRequest CreateUnityWebRequestObject()
    {
        return UnityWebRequest.Get(ConnectionLink.DATA_LINK);
    }

    protected override void OnResponse(Response response)
    {
        switch (ResultCodeHelper.ValueOf(response.GetResultCode()))
        {
            case ResultCode.SUCCESSFULLY_GOT_ALL_DATA_OBJECTS:
                {
                    OnSuccessHandler.OnSuccess(response);
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
