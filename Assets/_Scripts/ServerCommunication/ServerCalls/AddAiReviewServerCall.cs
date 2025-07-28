using Assets._Scripts.Messages;
using Assets._Scripts.ServerCommunication.RequestObjects;
using UnityEngine.Networking;

namespace Assets._Scripts.ServerCommunication.ServerCalls
{
    /// <summary>
    /// Represents a server call specifically designed to add an AI review to the backend.
    /// This class extends <see cref="ServerCall"/> to handle the creation of the request object,
    /// the <see cref="UnityWebRequest"/> setup, and the processing of the server response.
    /// </summary>
    public class AddAiReviewServerCall : ServerCall
    {
        public long novelId;
        public string novelName;
        public string prompt;
        public string aiFeedback;
        public string reviewText;

        /// <summary>
        /// Creates and populates an <see cref="AddAiReviewRequest"/> object with the data
        /// intended to be sent to the server for adding an AI review.
        /// </summary>
        /// <returns>An object representing the data for the AI review request.</returns>
        protected override object CreateRequestObject()
        {
            AddAiReviewRequest request = new AddAiReviewRequest();
            request.novelId = novelId;
            request.novelName = novelName;
            request.prompt = prompt;
            request.aiFeedback = aiFeedback;
            request.reviewText = reviewText;
            return request;
        }

        /// <summary>
        /// Creates and configures a <see cref="UnityWebRequest"/> object for posting
        /// the AI review data to the specified server endpoint.
        /// </summary>
        /// <returns>A configured <see cref="UnityWebRequest"/> for the AI review submission.</returns>
        protected override UnityWebRequest CreateUnityWebRequestObject()
        {
            return UnityWebRequest.PostWwwForm(ConnectionLink.AI_REVIEW_LINK, string.Empty);
        }

        /// <summary>
        /// Processes the <see cref="Response"/> received from the server after attempting to add an AI review.
        /// It checks the <see cref="ResultCode"/> to determine if the operation was successful
        /// and calls the appropriate handler or displays an error message.
        /// </summary>
        /// <param name="response">The <see cref="Response"/> object containing the server's reply.</param>
        protected override void OnResponse(Response response)
        {
            switch (ResultCodeHelper.ValueOf(response.GetResultCode()))
            {
                case ResultCode.SuccessfullyAddedAIReview:
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