using _00_Kite2.Common.Messages;
using _00_Kite2.Server_Communication;
using UnityEngine.Networking;

public class DeleteExpertFeedbackAnswerServerCall : ServerCall
{
    public long id;

    protected override object CreateRequestObject()
    {
        DeleteExpertFeedbackAnswerRequest request = new DeleteExpertFeedbackAnswerRequest();
        request.id = id;
        return request;
    }

    protected override UnityWebRequest CreateUnityWebRequestObject()
    {
        UnityWebRequest request = UnityWebRequest.Delete(ConnectionLink.EXPERT_FEEDBACK_ANSWER);
        request.downloadHandler = new DownloadHandlerBuffer();
        return request;
    }

    protected override void OnResponse(Response response)
    {
        switch (ResultCodeHelper.ValueOf(response.GetResultCode()))
        {
            case ResultCode.SUCCESSFULLY_DELETED_EXPERT_FEEDBACK_ANSWER:
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
