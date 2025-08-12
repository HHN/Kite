using Assets._Scripts.Messages;
using Assets._Scripts.ServerCommunication.RequestObjects;
using UnityEngine.Networking;

namespace Assets._Scripts.ServerCommunication.ServerCalls
{
    /// <summary>
    /// Represents a server call specifically designed to request a completion from a GPT model.
    /// This class extends <see cref="ServerCall"/> to handle the creation of the request object,
    /// the <see cref="UnityWebRequest"/> setup for posting the prompt, and the processing of the AI's response.
    /// </summary>
    public class GetCompletionServerCall : ServerCall
    {
        public string prompt;

        /// <summary>
        /// Creates and populates a <see cref="GptRequest"/> object with the provided prompt.
        /// This object serves as the payload for the GPT completion request.
        /// </summary>
        /// <returns>An object representing the data for the GPT completion request.</returns>
        protected override object CreateRequestObject()
        {
            GptRequest call = new GptRequest();
            call.prompt = prompt;
            return call;
        }

        /// <summary>
        /// Creates and configures a <see cref="UnityWebRequest"/> object for posting
        /// the GPT prompt to the completion server endpoint.
        /// </summary>
        /// <returns>A configured <see cref="UnityWebRequest"/> for the GPT completion request.</returns>
        protected override UnityWebRequest CreateUnityWebRequestObject()
        {
            return UnityWebRequest.PostWwwForm(ConnectionLink.COMPLETION_LINK, string.Empty);
        }

        /// <summary>
        /// Processes the <see cref="Response"/> received from the server after requesting a GPT completion.
        /// It checks the <see cref="ResultCode"/> to determine if the completion was successfully retrieved
        /// and calls the appropriate success or error handler.
        /// </summary>
        /// <param name="response">The <see cref="Response"/> object containing the server's reply.</param>
        protected override void OnResponse(Response response)
        {
            switch (ResultCodeHelper.ValueOf(response.GetResultCode()))
            {
                case ResultCode.SuccessfullyGotCompletion:
                {
                    OnSuccessHandler.OnSuccess(response);
                    return;
                }
                default:
                {
                    if (OnErrorHandler != null)
                    {
                        OnErrorHandler.OnError(response);
                    }
                    else
                    {
                        sceneController.DisplayErrorMessage(ErrorMessages.UNEXPECTED_SERVER_ERROR);
                    }

                    return;
                }
            }
        }
    }
}