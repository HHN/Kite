using UnityEngine.Networking;

public class ResetPasswordServerCall : ServerCall
{
    public string username;

    protected override object CreateRequestObject()
    {
        ResetPasswordRequest resetPaswordRequest = new ResetPasswordRequest();
        resetPaswordRequest.username = username;
        return resetPaswordRequest;
    }

    protected override UnityWebRequest CreateUnityWebRequestObject()
    {
        return UnityWebRequest.Post(ConnectionLink.RESET_PASSWORD_LINK, string.Empty);
    }

    protected override void OnResponse(Response response)
    {
        switch (ResultCodeHelper.ValueOf(response.resultCode))
        {
            case ResultCode.SUCCESSFULLY_INITIATED_RESET:
                {
                    onSuccessHandler.OnSuccess(response);
                    return;
                }
            case ResultCode.USER_NOT_FOUND:
                {
                    sceneController.DisplayErrorMessage(ErrorMessages.USER_NOT_FOUND);
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
