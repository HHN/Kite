using Assets._Scripts.Messages;
using Assets._Scripts.ServerCommunication.RequestObjects;
using UnityEngine.Networking;

namespace Assets._Scripts.ServerCommunication.ServerCalls
{
    public class DeleteNovelReviewServerCall : ServerCall
    {
        public long id;

        protected override object CreateRequestObject()
        {
            DeleteNovelReviewRequest request = new DeleteNovelReviewRequest();
            request.id = id;
            return request;
        }

        protected override UnityWebRequest CreateUnityWebRequestObject()
        {
            UnityWebRequest request = UnityWebRequest.Delete(ConnectionLink.NOVEL_REVIEW_LINK);
            request.downloadHandler = new DownloadHandlerBuffer();
            return request;
        }

        protected override void OnResponse(Response response)
        {
            switch (ResultCodeHelper.ValueOf(response.GetResultCode()))
            {
                case ResultCode.SuccessfullyDeletedNovelReview:
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