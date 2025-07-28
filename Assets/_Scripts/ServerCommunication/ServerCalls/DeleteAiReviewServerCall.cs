using Assets._Scripts.Messages;
using Assets._Scripts.ServerCommunication.RequestObjects;
using UnityEngine.Networking;

namespace Assets._Scripts.ServerCommunication.ServerCalls
{
    /// <summary>
    /// Represents a server call specifically designed to delete an AI review from the backend.
    /// This class extends <see cref="ServerCall"/> to handle the creation of the request object,
    /// the <see cref="UnityWebRequest"/> setup for a DELETE operation, and the processing of the server response.
    /// </summary>
    public class DeleteAiReviewServerCall : ServerCall
    {
        public long id;

        /// <summary>
        /// Creates and populates a <see cref="DeleteAiReviewRequest"/> object with the ID
        /// of the AI review intended for deletion.
        /// </summary>
        /// <returns>An object representing the data for the AI review deletion request.</returns>
        protected override object CreateRequestObject()
        {
            DeleteAiReviewRequest request = new DeleteAiReviewRequest();
            request.id = id;
            return request;
        }

        /// <summary>
        /// Creates and configures a <see cref="UnityWebRequest"/> object for performing a DELETE operation
        /// on the specified server endpoint for AI reviews.
        /// </summary>
        /// <returns>A configured <see cref="UnityWebRequest"/> for the AI review deletion.</returns>
        protected override UnityWebRequest CreateUnityWebRequestObject()
        {
            UnityWebRequest request = UnityWebRequest.Delete(ConnectionLink.AI_REVIEW_LINK);
            request.downloadHandler = new DownloadHandlerBuffer();
            return request;
        }

        /// <summary>
        /// Processes the <see cref="Response"/> received from the server after attempting to delete an AI review.
        /// It checks the <see cref="ResultCode"/> to determine if the operation was successful
        /// and calls the appropriate handler or displays an error message.
        /// </summary>
        /// <param name="response">The <see cref="Response"/> object containing the server's reply.</param>
        protected override void OnResponse(Response response)
        {
            switch (ResultCodeHelper.ValueOf(response.GetResultCode()))
            {
                case ResultCode.SuccessfullyDeletedAIReview:
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