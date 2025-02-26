using Assets._Scripts.Common.Messages;
using Assets._Scripts.Server_Communication.Request_Objects;
using UnityEngine.Networking;

namespace Assets._Scripts.Server_Communication.Server_Calls
{
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
}