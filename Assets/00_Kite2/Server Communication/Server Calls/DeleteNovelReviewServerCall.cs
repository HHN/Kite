using _00_Kite2.Common.Messages;
using _00_Kite2.Server_Communication;
using UnityEngine.Networking;

public class DeleteNovelReviewServerCall : ServerCall
{
    public long id;

    protected override object CreateRequestObject()
    {
        DeleteNovelReviewRequest request = new DeleteNovelReviewRequest();
        request.id = id;
        return request;
    }

    protected override UnityWebRequest CreateUnityWebRequestObject()
    {
        UnityWebRequest request = UnityWebRequest.Delete(ConnectionLink.NOVEL_REVIEW_LINK);
        request.downloadHandler = new DownloadHandlerBuffer();
        return request;
    }

    protected override void OnResponse(Response response)
    {
        switch (ResultCodeHelper.ValueOf(response.GetResultCode()))
        {
            case ResultCode.SUCCESSFULLY_DELETED_NOVEL_REVIEW:
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
