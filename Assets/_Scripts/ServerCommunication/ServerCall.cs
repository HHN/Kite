using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
using Assets._Scripts.Controller.SceneControllers;
using Assets._Scripts.Messages;
using Assets._Scripts.Utilities;
using UnityEngine;
using UnityEngine.Networking;
using System.Security.Cryptography;

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
        [DllImport("__Internal")]
        private static extern string GetPassphraseFromBrowser();
        
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
                webRequest.certificateHandler = new BypassCertificate();

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
            
            string secret = "";
#if UNITY_WEBGL && !UNITY_EDITOR
    secret = GetPassphraseFromBrowser();
#else
            secret = "secret"; // Hier kannst du dein Passwort zum Testen im Editor lassen
#endif
            
            // Create HMAC-Signature
            string requestBody = "";
            object req = CreateRequestObject();
            if (req != null)
            {
                requestBody = JsonUtility.ToJson(req); // Jetzt wird requestBody korrekt gefüllt
                webRequest.uploadHandler = new UploadHandlerRaw(new UTF8Encoding(false).GetBytes(requestBody));
            }
            
            string timestamp = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            string dataToSign = timestamp /*+ requestBody*/;
            string signature = CalculateHMAC(dataToSign, secret);

            webRequest.SetRequestHeader("X-Kite-Timestamp", timestamp);
            webRequest.SetRequestHeader("X-Kite-Signature", signature);
            
            // Wenn ein temporäres Token vorhanden ist, Authorization-Header setzen
            if (TokenManager.HasToken())
            {
                webRequest.SetRequestHeader("Authorization", $"Bearer {TokenManager.Token}");
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
                    // Safety: OnSuccessHandler kann null sein.
                    if (OnSuccessHandler == null || OnSuccessHandler.IsNullOrDestroyed())
                    {
                        // Versuche trotzdem Response zu parsen (für Logs/Debugging oder interne Logik)
                        Response response = null;
                        try
                        {
                            if (webRequest.downloadHandler != null)
                                response = JsonUtility.FromJson<Response>(webRequest.downloadHandler.text);
                        }
                        catch (System.Exception ex)
                        {
                            Debug.LogWarning("[ServerCall] Fehler beim Parsen der Response: " + ex.Message);
                        }

                        if (response != null)
                            OnResponse(response);
                        break;
                    }

                    // Normaler Pfad
                    Response fullResponse = null;
                    try
                    {
                        if (webRequest.downloadHandler != null)
                            fullResponse = JsonUtility.FromJson<Response>(webRequest.downloadHandler.text);
                    }
                    catch (System.Exception ex)
                    {
                        Debug.LogWarning("[ServerCall] Fehler beim Parsen der Response: " + ex.Message);
                    }

                    OnResponse(fullResponse);
                    break;
                }
                
                // Behandle Fehlerfälle spezifischer
                case UnityWebRequest.Result.ProtocolError:
                {
                    string errorMessage;
                    switch (webRequest.responseCode)
                    {
                        case 401:
                            errorMessage = "Deine Sitzung ist abgelaufen. Bitte lade das Spiel neu.";
                            break;
                        case 404:
                            errorMessage = "Die angeforderte Ressource wurde nicht gefunden.";
                            break;
                        case 500:
                        case 502:
                        case 503:
                        case 504:
                            errorMessage = "Ein Server-Problem ist aufgetreten. Bitte versuche es später erneut.";
                            break;
                        default:
                            errorMessage = $"Ein Verbindungsfehler ist aufgetreten (Code: {webRequest.responseCode}).";
                            break;
                    }

                    if (sceneController != null)
                        sceneController.DisplayErrorMessage(errorMessage);
                    else
                        Debug.LogWarning($"[ServerCall] {errorMessage}");

                    try { OnResponse(new Response()); } catch { }
                    break;
                }
                default:
                {
                    if (Application.internetReachability == NetworkReachability.NotReachable)
                    {
                        if (sceneController != null)
                            sceneController.DisplayErrorMessage(ErrorMessages.NO_INTERNET);
                        else
                            Debug.LogWarning("[ServerCall] Fehler: Keine Internetverbindung.");
                    }
                    else
                    {
                        // Sonstige Fehler (DNS, ConnectionRefused etc.)
                        string webErr = webRequest != null ? webRequest.error : "Unbekannter Fehler";
                        if (sceneController != null)
                            sceneController.DisplayErrorMessage($"Verbindungsfehler: {webErr}");
                        else
                            Debug.LogWarning($"[ServerCall] Verbindungsfehler: {webErr}");
                    }
                    
                    try { OnResponse(new Response()); } catch { }

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
        
        /// <summary>
        /// Calculates an HMACSHA256 hash for the given data and key.
        /// </summary>
        /// <param name="data">The data to be signed.</param>
        /// <param name="key">The secret key (passphrase).</param>
        /// <returns>The HMACSHA256 hash as a lowercase hexadecimal string.</returns>
        private string CalculateHMAC(string data, string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            
            using (var hmac = new HMACSHA256(keyBytes))
            {
                byte[] hashBytes = hmac.ComputeHash(dataBytes);
                // Convert the byte array to a hex string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2")); // "x2" formats as a two-digit hexadecimal number
                }
                return sb.ToString();
            }
        }
    }
}