using Assets._Scripts.Messages;
using Assets._Scripts.Server_Communication.Request_Objects;
using UnityEngine.Networking;

namespace Assets._Scripts.Server_Communication.Server_Calls
{
    public class AddNovelReviewServerCall : ServerCall
    {
        public long novelId;
        public string novelName;
        public long rating;
        public string reviewText;

        protected override object CreateRequestObject()
        {
            AddNovelReviewRequest request = new AddNovelReviewRequest();
            request.novelId = novelId;
            request.novelName = novelName;
            request.rating = rating;
            request.reviewText = reviewText;
            return request;
        }

        protected override UnityWebRequest CreateUnityWebRequestObject()
        {
            return UnityWebRequest.PostWwwForm(ConnectionLink.NOVEL_REVIEW_LINK, string.Empty);
        }

        protected override void OnResponse(Response response)
        {
            switch (ResultCodeHelper.ValueOf(response.GetResultCode()))
            {
                case ResultCode.SUCCESSFULLY_ADDED_NOVEL_REVIEW:
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