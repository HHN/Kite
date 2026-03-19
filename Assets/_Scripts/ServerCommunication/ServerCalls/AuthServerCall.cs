using UnityEngine;
using UnityEngine.Networking;

namespace Assets._Scripts.ServerCommunication.ServerCalls
{
    public class AuthServerCall : ServerCall
    {
        protected override UnityWebRequest CreateUnityWebRequestObject()
        {
            return UnityWebRequest.PostWwwForm(ConnectionLink.AUTH_LINK, "");
        }

        protected override object CreateRequestObject()
        {
            return null; // Kein Body nötig für diesen Request
        }

        protected override void OnResponse(Response response)
        {
            if (response != null && !string.IsNullOrEmpty(response.GetCompletion()))
            {
                TokenManager.SetToken(response.GetCompletion());
                Debug.Log("[AuthServerCall] Token erhalten und gespeichert.");
            }
            else
            {
                Debug.LogWarning("[AuthServerCall] Keine gültige Antwort oder kein Token vom Server erhalten.");
            }
        }
    }
}