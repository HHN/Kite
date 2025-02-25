using Assets._Scripts.Common.Messages;
using UnityEngine.Networking;

namespace Assets._Scripts.Server_Communication.Server_Calls
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
                case ResultCode.SUCCESSFULLY_GOT_ALL_EXPERT_FEEDBACK_QUESTIONS:
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