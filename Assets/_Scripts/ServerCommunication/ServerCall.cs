using System.Collections;
using System.Text;
using Assets._Scripts.Controller.SceneControllers;
using Assets._Scripts.Messages;
using Assets._Scripts.Utilities;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets._Scripts.ServerCommunication
{
    public abstract class ServerCall : MonoBehaviour
    {
        public IOnSuccessHandler OnSuccessHandler;
        public IOnErrorHandler OnErrorHandler;
        public SceneController sceneController;
        private Coroutine _serverCallCoroutine;

        public void SendRequest()
        {
            _serverCallCoroutine = StartCoroutine(RequestRegistration());
        }

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

        protected void HandleWebRequestResult(UnityWebRequest webRequest)
        {
            Debug.Log("UnityWebRequest Result: " + webRequest.result);
            Debug.Log("UnityWebRequest Error: " + webRequest.error);
            Debug.Log("WebRequest URL: " + webRequest.url);

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

        public bool StopWebRequest()
        {
            if (_serverCallCoroutine != null)
            {
                StopCoroutine(_serverCallCoroutine);
                _serverCallCoroutine = null;
                return true;
            }

            return false;
        }

        protected abstract UnityWebRequest CreateUnityWebRequestObject();
        protected abstract object CreateRequestObject();
        protected abstract void OnResponse(Response response);

        private IEnumerator DestroyInSeconds(long seconds)
        {
            yield return new WaitForSeconds(seconds);
            Destroy(this.gameObject);
        }
    }
}