using UnityEngine.Networking;

public class RegisterServerCall : ServerCall
{
    public string username;
    public string email;
    public string password;

    protected override object CreateRequestObject()
    {
        RegistrationRequest request = new RegistrationRequest();
        request.username = username;
        request.email = email;
        request.password = password;
        return request;
    }

    protected override UnityWebRequest CreateUnityWebRequestObject()
    {
        return UnityWebRequest.Post(ConnectionLink.REGISTRATION_LINK, string.Empty);
    }

    protected override void OnResponse(Response response)
    {
        switch (ResultCodeHelper.valueOf(response.resultCode))
        {
            case ResultCode.SUCCESSFULLY_REGISTERED_NEW_USER:
                {
                    this.onSuccessHandler.OnSuccess(response);
                    return;
                }
            case ResultCode.USERNAME_ALREADY_TAKEN:
                {
                    sceneController.DisplayErrorMessage(ErrorMessages.USERNAME_ALREADY_TAKEN);
                    return;
                }
            case ResultCode.EMAIL_ALREADY_REGISTERED:
                {
                    sceneController.DisplayErrorMessage(ErrorMessages.EMAIL_ALREADY_REGISTERED);
                    return;
                }
            case ResultCode.INVALID_EMAIL:
                {
                    sceneController.DisplayErrorMessage(ErrorMessages.EMAIL_INVALID);
                    return;
                }
        }
    }
}
