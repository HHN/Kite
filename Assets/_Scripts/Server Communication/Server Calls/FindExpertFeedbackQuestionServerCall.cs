using Assets._Scripts.Common.Messages;
using Assets._Scripts.Server_Communication.Request_Objects;
using UnityEngine.Networking;

namespace Assets._Scripts.Server_Communication.Server_Calls
{
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
                    OnSuccessHandler.OnSuccess(response);
                    return;
                }
                case ResultCode.NO_SUCH_EXPERT_FEEDBACK_QUESTION:
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