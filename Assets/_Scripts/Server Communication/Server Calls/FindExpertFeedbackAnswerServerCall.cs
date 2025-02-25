using Assets._Scripts.Common.Messages;
using Assets._Scripts.Server_Communication.Request_Objects;
using UnityEngine.Networking;

namespace Assets._Scripts.Server_Communication.Server_Calls
{
    public class FindExpertFeedbackAnswerServerCall : ServerCall
    {
        public long id;

        protected override object CreateRequestObject()
        {
            FindExpertFeedbackAnswerRequest request = new FindExpertFeedbackAnswerRequest();
            request.id = id;
            return request;
        }

        protected override UnityWebRequest CreateUnityWebRequestObject()
        {
            return UnityWebRequest.Put(ConnectionLink.EXPERT_FEEDBACK_ANSWER, string.Empty);
        }

        protected override void OnResponse(Response response)
        {
            switch (ResultCodeHelper.ValueOf(response.GetResultCode()))
            {
                case ResultCode.SUCCESSFULLY_FOUND_EXPERT_FEEDBACK_ANSWER:
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