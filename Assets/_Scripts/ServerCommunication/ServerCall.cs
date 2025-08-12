using System.Collections;
using System.Text;
using Assets._Scripts.Controller.SceneControllers;
using Assets._Scripts.Messages;
using Assets._Scripts.Utilities;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets._Scripts.ServerCommunication
{
    /// <summary>
    /// An abstract base class for all server communication operations in the game.
    /// It provides common functionality for sending web requests, handling responses,
    /// and managing success/error callbacks. Server calls are typically MonoBehaviour
    /// instances that are instantiated to perform a single request.
    /// </summary>
    public abstract class ServerCall : MonoBehaviour
    {
        public IOnSuccessHandler OnSuccessHandler;
        public IOnErrorHandler OnErrorHandler;
        public SceneController sceneController;
        private Coroutine _serverCallCoroutine;

        /// <summary>
        /// Initiates the server request by starting the <see cref="RequestRegistration"/> coroutine.
        /// This is the entry point for sending a server call.
        /// </summary>
        public void SendRequest()
        {
            _serverCallCoroutine = StartCoroutine(RequestRegistration());
        }

        /// <summary>
        /// Coroutine that handles the entire web request lifecycle:
        /// creates the <see cref="UnityWebRequest"/>, assigns a certificate handler,
        /// sends the request, and then processes the result.
        /// </summary>
        /// <returns>An IEnumerator for coroutine execution.</returns>
        private IEnumerator RequestRegistration()
        {
            using (UnityWebRequest webRequest = CreateRequest())
            {
                // Hier wird der Custom-CertificateHandler zugewiesen:
                webRequest.certificateHandler = new BypassCertificate();
                //Debug.Log("RequestRegistration");

                yield return webRequest.SendWebRequest();
                HandleWebRequestResult(webRequest);
            }

            _serverCallCoroutine = null;
        }

        /// <summary>
        /// Creates and configures a <see cref="UnityWebRequest"/> object.
        /// It sets the Content-Type header to JSON and, if a request object is provided,
        /// serializes it to JSON and sets it as the upload handler's data.
        /// </summary>
        /// <returns>A configured <see cref="UnityWebRequest"/> instance ready to be sent.</returns>
        private UnityWebRequest CreateRequest()
        {
            UnityWebRequest webRequest = CreateUnityWebRequestObject();
            webRequest.SetRequestHeader("Content-Type", "application/json; charset=utf-8");
            object req = CreateRequestObject();
            if (req != null)
            {
                string jsonData = JsonUtility.ToJson(req);
                webRequest.uploadHandler = new UploadHandlerRaw(new UTF8Encoding(false).GetBytes(jsonData));
            }

            return webRequest;
        }

        /// <summary>
        /// Handles the result of a completed <see cref="UnityWebRequest"/>.
        /// It checks for success or various error conditions (e.g., no internet, unexpected server errors)
        /// and dispatches the response to the appropriate handler or displays an error message.
        /// </summary>
        /// <param name="webRequest">The completed <see cref="UnityWebRequest"/> object.</param>
        protected void HandleWebRequestResult(UnityWebRequest webRequest)
        {
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.Success:
                {
                    if (OnSuccessHandler.IsNullOrDestroyed())
                    {
                        break;
                    }

                    Response response = JsonUtility.FromJson<Response>(webRequest.downloadHandler.text);
                    OnResponse(response);
                    break;
                }
                default:
                {
                    if (Application.internetReachability == NetworkReachability.NotReachable)
                    {
                        sceneController.DisplayErrorMessage(ErrorMessages.NO_INTERNET);
                        Response response = new Response();
                        OnResponse(response);
                    }
                    else
                    {
                        sceneController.DisplayErrorMessage($"Unerwarteter Fehler: {webRequest.error}");
                        Response response = new Response();
                        OnResponse(response);
                    }

                    break;
                }
            }

            StartCoroutine(DestroyInSeconds(5));
        }

        /// <summary>
        /// Abstract method to be implemented by derived classes to create the specific
        /// <see cref="UnityWebRequest"/> object (e.g., GET, POST, DELETE).
        /// </summary>
        /// <returns>A <see cref="UnityWebRequest"/> instance specific to the server call type.</returns>
        protected abstract UnityWebRequest CreateUnityWebRequestObject();
       
        /// <summary>
        /// Abstract method to be implemented by derived classes to create the specific
        /// request object (payload) that will be serialized to JSON and sent to the server.
        /// Returns null if no request body is needed (e.g., for simple GET requests).
        /// </summary>
        /// <returns>An object representing the request payload, or null if none.</returns>
        protected abstract object CreateRequestObject();
        
        /// <summary>
        /// Abstract method to be implemented by derived classes to process the <see cref="Response"/>
        /// received from the server. This is where specific success/error logic based on the response's
        /// content or result code should be handled.
        /// </summary>
        /// <param name="response">The <see cref="Response"/> object deserialized from the server's reply.</param>
        protected abstract void OnResponse(Response response);

        /// <summary>
        /// A private coroutine that waits for a specified number of seconds and then destroys
        /// the GameObject this script is attached to. Used for self-cleanup after a server call.
        /// </summary>
        /// <param name="seconds">The number of seconds to wait before destroying the GameObject.</param>
        /// <returns>An IEnumerator for coroutine execution.</returns>
        private IEnumerator DestroyInSeconds(long seconds)
        {
            yield return new WaitForSeconds(seconds);
            Destroy(gameObject);
        }
    }
}