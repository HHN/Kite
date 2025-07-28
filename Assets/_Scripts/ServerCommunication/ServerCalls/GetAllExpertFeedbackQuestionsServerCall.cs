using Assets._Scripts.Messages;
using UnityEngine.Networking;

namespace Assets._Scripts.ServerCommunication.ServerCalls
{
    public class GetAllExpertFeedbackQuestionsServerCall : ServerCall
    {
        protected override object CreateRequestObject()
        {
            return null;
        }

        protected override UnityWebRequest CreateUnityWebRequestObject()
        {
            return UnityWebRequest.Get(ConnectionLink.EXPERT_FEEDBACK_QUESTION);
        }

        protected override void OnResponse(Response response)
        {
            switch (ResultCodeHelper.ValueOf(response.GetResultCode()))
            {
                case ResultCode.SuccessfullyGotAllExpertFeedbackQuestions:
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
}