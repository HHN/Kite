using UnityEngine.Networking;

public class GetUserRoleByCodeServerCall : ServerCall
{
    public string code;

    protected override object CreateRequestObject()
    {
        GetUserRoleByCodeRequest call = new GetUserRoleByCodeRequest();
        call.code = code;
        return call;
    }

    protected override UnityWebRequest CreateUnityWebRequestObject()
    {
        return UnityWebRequest.Post(ConnectionLink.USER_ROLE_LINK, string.Empty);
    }

    protected override void OnResponse(Response response)
    {
        switch (ResultCodeHelper.ValueOf(response.GetResultCode()))
        {
            case ResultCode.SUCCESSFULLY_GOT_USER_ROLE:
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
