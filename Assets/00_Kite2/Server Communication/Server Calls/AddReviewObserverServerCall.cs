using _00_Kite2.Common.Messages;
using _00_Kite2.Server_Communication.Request_Objects;
using UnityEngine.Networking;

namespace _00_Kite2.Server_Communication.Server_Calls
{
    public class AddReviewObserverServerCall : ServerCall
    {
        public string email;

        protected override object CreateRequestObject()
        {
            AddObserverRequest request = new AddObserverRequest();
            request.email = email;
            return request;
        }

        protected override UnityWebRequest CreateUnityWebRequestObject()
        {
            return UnityWebRequest.PostWwwForm(ConnectionLink.OBSERVER_LINK, string.Empty);
        }

        protected override void OnResponse(Response response)
        {
            switch (ResultCodeHelper.ValueOf(response.GetResultCode()))
            {
                case ResultCode.SUCCESSFULLY_ADDED_REVIEW_OBSERVER:
                {
                    OnSuccessHandler.OnSuccess(response);
                    return;
                }
                case ResultCode.REVIEW_OBSERVER_ALREADY_EXISTS:
                {
                    sceneController.DisplayInfoMessage(ErrorMessages.REVIEW_OBSERVER_ALREADY_EXISTS);
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