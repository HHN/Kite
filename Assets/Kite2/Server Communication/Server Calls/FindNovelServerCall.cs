using UnityEngine.Networking;

public class FindNovelServerCall : ServerCall
{
    public string query;
    protected override object CreateRequestObject()
    {
        FindNovelRequest request = new FindNovelRequest();
        request.query = query;
        return request;
    }

    protected override UnityWebRequest CreateUnityWebRequestObject()
    {
        return UnityWebRequest.Get(ConnectionLink.FIND_NOVEL_LINK);
    }

    protected override void OnResponse(Response response)
    {
        switch (ResultCodeHelper.ValueOf(response.resultCode))
        {
            case ResultCode.SUCCESSFULLY_FOUND_NOVEL:
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
