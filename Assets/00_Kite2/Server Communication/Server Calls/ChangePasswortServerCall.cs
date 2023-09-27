using UnityEngine.Networking;

public class ChangePasswortServerCall : ServerCall
{
    public string oldPassword;
    public string newPassword;

    protected override object CreateRequestObject()
    {
        ChangePasswordRequest changePasswortRequest = new ChangePasswordRequest();
        changePasswortRequest.oldPassword = oldPassword;
        changePasswortRequest.newPassword = newPassword;
        return changePasswortRequest;
    }

    protected override UnityWebRequest CreateUnityWebRequestObject()
    {
        return UnityWebRequest.Post(ConnectionLink.CHANGE_PASSWORD_LINK, string.Empty);
    }

    protected override void OnResponse(Response response)
    {
        switch (ResultCodeHelper.ValueOf(response.resultCode))
        {
            case ResultCode.SUCCESSFULLY_CHANGED_PASSWORD:
                {
                    onSuccessHandler.OnSuccess(response);
                    return;
                }
            case ResultCode.CHANGE_OF_PASSWORD_FAILED:
                {
                    sceneController.DisplayErrorMessage(ErrorMessages.WRONG_PASSWORD);
                    return;
                }
            case ResultCode.FAILURE:
                {
                    sceneController.DisplayErrorMessage(ErrorMessages.UNEXPECTED_SERVER_ERROR);
                    return;
                }
        }
    }
}
