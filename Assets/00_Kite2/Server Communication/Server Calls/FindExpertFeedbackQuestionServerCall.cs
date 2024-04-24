using UnityEngine.Networking;

public class FindExpertFeedbackQuestionServerCall : ServerCall
{
    public string userUuid;

    protected override object CreateRequestObject()
    {
        FindExpertFeedbackQuestionsRequest request = new FindExpertFeedbackQuestionsRequest();
        request.userUuid = userUuid;
        return request;
    }

    protected override UnityWebRequest CreateUnityWebRequestObject()
    {
        return UnityWebRequest.Put(ConnectionLink.EXPERT_FEEDBACK_QUESTION, string.Empty);
    }

    protected override void OnResponse(Response response)
    {
        switch (ResultCodeHelper.ValueOf(response.GetResultCode()))
        {
            case ResultCode.SUCCESSFULLY_FOUND_EXPERT_FEEDBACK_QUESTION:
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
