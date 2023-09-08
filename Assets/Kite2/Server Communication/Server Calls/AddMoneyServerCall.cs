using UnityEngine.Networking;

public class AddMoneyServerCall : ServerCall
{
    public long value;

    protected override object CreateRequestObject()
    {
        AddMoneyRequest addMoneyRequest = new AddMoneyRequest();
        addMoneyRequest.value = value;
        return addMoneyRequest;
    }

    protected override UnityWebRequest CreateUnityWebRequestObject()
    {
        return UnityWebRequest.Post(ConnectionLink.MONEY_LINK, string.Empty);
    }

    protected override void OnResponse(Response response)
    {
        switch (ResultCodeHelper.ValueOf(response.resultCode))
        {
            case ResultCode.SUCCESSFULLY_UPDATED_MONEY:
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
