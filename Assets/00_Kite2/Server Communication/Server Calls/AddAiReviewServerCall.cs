using _00_Kite2.Common.Messages;
using _00_Kite2.Server_Communication;
using UnityEngine.Networking;

public class AddAiReviewServerCall : ServerCall
{
    public long novelId;
    public string novelName;
    public string prompt;
    public string aiFeedback;
    public string reviewText;

    protected override object CreateRequestObject()
    {
        AddAiReviewRequest request = new AddAiReviewRequest();
        request.novelId = novelId;
        request.novelName = novelName;
        request.prompt = prompt;
        request.aiFeedback = aiFeedback;
        request.reviewText = reviewText;
        return request;
    }

    protected override UnityWebRequest CreateUnityWebRequestObject()
    {
        return UnityWebRequest.PostWwwForm(ConnectionLink.AI_REVIEW_LINK, string.Empty);
    }

    protected override void OnResponse(Response response)
    {
        switch (ResultCodeHelper.ValueOf(response.GetResultCode()))
        {
            case ResultCode.SUCCESSFULLY_ADDED_AI_REVIEW:
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