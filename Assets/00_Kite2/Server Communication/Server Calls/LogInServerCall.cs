using UnityEngine.Networking;

public class LogInServerCall : ServerCall
{
    public string username;
    public string password;

    protected override object CreateRequestObject()
    {
        LoginRequest loginRequest = new LoginRequest();
        loginRequest.username = username;
        loginRequest.email = username;
        loginRequest.password = password;
        return loginRequest;
    }

    protected override UnityWebRequest CreateUnityWebRequestObject()
    {
        return UnityWebRequest.Post(ConnectionLink.LOG_IN_LINK, string.Empty);
    }

    protected override void OnResponse(Response response)
    {
        switch (ResultCodeHelper.ValueOf(response.resultCode))
        {
            case ResultCode.SUCCESSFULLY_LOGGED_IN:
                {
                    onSuccessHandler.OnSuccess(response);
                    return;
                }
            case ResultCode.INVALID_CREDENTIALS:
                {
                    sceneController.DisplayErrorMessage(ErrorMessages.INVALID_CREDENTIALS);
                    return;
                }
            case ResultCode.EMAIL_NOT_CONFIRMED:
                {
                    sceneController.DisplayErrorMessage(ErrorMessages.EMAIL_NOT_CONFIRMED);
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
