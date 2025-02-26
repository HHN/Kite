using Assets._Scripts.Common.Messages;
using Assets._Scripts.Server_Communication.Request_Objects;
using UnityEngine.Networking;

namespace Assets._Scripts.Server_Communication.Server_Calls
{
    public class AddExpertFeedbackQuestionServerCall : ServerCall
    {
        public long novelId;
        public string novelName;
        public string userUuid;
        public string prompt;
        public string aiFeedback;
        public string dialogue;
        public string expertFeedbackQuestion;

        protected override object CreateRequestObject()
        {
            AddExpertFeedbackQuestionRequest request = new AddExpertFeedbackQuestionRequest();
            request.novelId = novelId;
            request.novelName = novelName;
            request.userUuid = userUuid;
            request.prompt = prompt;
            request.aiFeedback = aiFeedback;
            request.dialogue = dialogue;
            request.expertFeedbackQuestion = expertFeedbackQuestion;
            return request;
        }

        protected override UnityWebRequest CreateUnityWebRequestObject()
        {
            return UnityWebRequest.PostWwwForm(ConnectionLink.EXPERT_FEEDBACK_QUESTION, string.Empty);
        }

        protected override void OnResponse(Response response)
        {
            switch (ResultCodeHelper.ValueOf(response.GetResultCode()))
            {
                case ResultCode.SUCCESSFULLY_POSTET_EXPERT_FEEDBACK_QUESTION:
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