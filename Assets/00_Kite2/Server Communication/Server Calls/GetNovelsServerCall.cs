using UnityEngine.Networking;

public class GetNovelsServerCall : ServerCall
{
    protected override object CreateRequestObject()
    {
        return new GetNovelsRequest();
    }

    protected override UnityWebRequest CreateUnityWebRequestObject()
    {
        return UnityWebRequest.Get(ConnectionLink.NOVELS_LINK);
    }

    protected override void OnResponse(Response response)
    {
        switch (ResultCodeHelper.ValueOf(response.resultCode))
        {
            case ResultCode.SUCCESSFULLY_GOT_NOVELS:
                {
                    onSuccessHandler.OnSuccess(response);
                    return;
                }
            case ResultCode.NO_NOVEL_AVAILABLE:
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
