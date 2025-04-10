using Assets._Scripts.Messages;
using Assets._Scripts.ServerCommunication.RequestObjects;
using UnityEngine.Networking;

namespace Assets._Scripts.ServerCommunication.ServerCalls
{
    public class AddExpertFeedbackAnswerServerCall : ServerCall
    {
        public string expertName;
        public string expertFeedbackAnswer;
        public long idOfExpertFeedbackQuestion;

        protected override object CreateRequestObject()
        {
            AddExpertFeedbackAnswerRequest request = new AddExpertFeedbackAnswerRequest();
            request.expertName = expertName;
            request.expertFeedbackAnswer = expertFeedbackAnswer;
            request.idOfExpertFeedbackQuestion = idOfExpertFeedbackQuestion;
            return request;
        }

        protected override UnityWebRequest CreateUnityWebRequestObject()
        {
            return UnityWebRequest.PostWwwForm(ConnectionLink.EXPERT_FEEDBACK_ANSWER, string.Empty);
        }

        protected override void OnResponse(Response response)
        {
            switch (ResultCodeHelper.ValueOf(response.GetResultCode()))
            {
                case ResultCode.SUCCESSFULLY_POSTET_EXPERT_FEEDBACK_ANSWER:
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