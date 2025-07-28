using Assets._Scripts.Messages;
using UnityEngine.Networking;

namespace Assets._Scripts.ServerCommunication.ServerCalls
{
    /// <summary>
    /// Represents a server call specifically designed to retrieve all AI reviews from the backend.
    /// This class extends <see cref="ServerCall"/> to handle the creation of the request object (or lack thereof),
    /// the <see cref="UnityWebRequest"/> setup for a GET operation, and the processing of the server response.
    /// </summary>
    public class GetAiReviewsServerCall : ServerCall
    {
        /// <summary>
        /// Creates the request object for the server call. For a GET request that fetches all reviews,
        /// no specific request body is typically needed, so this method returns null.
        /// </summary>
        /// <returns>Null, as no specific request object is required for this GET operation.</returns>
        protected override object CreateRequestObject()
        {
            return null;
        }

        /// <summary>
        /// Creates and configures a <see cref="UnityWebRequest"/> object for performing a GET operation
        /// on the specified server endpoint for AI reviews.
        /// </summary>
        /// <returns>A configured <see cref="UnityWebRequest"/> for fetching AI reviews.</returns>
        protected override UnityWebRequest CreateUnityWebRequestObject()
        {
            return UnityWebRequest.Get(ConnectionLink.AI_REVIEW_LINK);
        }

        /// <summary>
        /// Processes the <see cref="Response"/> received from the server after attempting to get all AI reviews.
        /// It checks the <see cref="ResultCode"/> to determine if the operation was successful
        /// and calls the appropriate handler or displays an error message.
        /// </summary>
        /// <param name="response">The <see cref="Response"/> object containing the server's reply.</param>
        protected override void OnResponse(Response response)
        {
            switch (ResultCodeHelper.ValueOf(response.GetResultCode()))
            {
                case ResultCode.SuccessfullyGotAllAIReviews:
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