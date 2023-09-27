using UnityEngine.Networking;

public class LogOutServerCall : ServerCall
{
    protected override object CreateRequestObject()
    {
        LogoutRequest logoutRequest = new LogoutRequest();
        return logoutRequest;
    }

    protected override UnityWebRequest CreateUnityWebRequestObject()
    {
        return UnityWebRequest.Post(ConnectionLink.LOG_OUT_LINK, string.Empty);
    }

    protected override void OnResponse(Response response)
    {
        switch (ResultCodeHelper.ValueOf(response.resultCode))
        {
            case ResultCode.SUCCESSFULLY_LOGGED_OUT:
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
