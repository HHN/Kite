using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using UnityEditor.PackageManager.Requests;

public abstract class ServerCall : MonoBehaviour
{
    public OnSuccessHandler onSuccessHandler;
    public SceneController sceneController;
    private Coroutine serverCallCoroutine;

    public void SendRequest()
    {
        serverCallCoroutine = StartCoroutine(RequestRegistration());
    }

    private IEnumerator RequestRegistration()
    {
        using (UnityWebRequest webRequest = CreateRequest())
        {
            yield return webRequest.SendWebRequest();
            HandleWebRequestResult(webRequest);
        }
        serverCallCoroutine = null;
    }

    private UnityWebRequest CreateRequest()
    {
        UnityWebRequest webRequest = CreateUnityWebRequestObject();
        webRequest.SetRequestHeader("Content-Type", "application/json; charset=utf-8");
        webRequest.SetRequestHeader("Authorization", "Bearer " + AuthenticationManager.Instance().GetAuthToken());
        object req = CreateRequestObject();
        string jsonData = JsonUtility.ToJson(req);
        webRequest.uploadHandler = new UploadHandlerRaw(new UTF8Encoding(false).GetBytes(jsonData));
        return webRequest;
    }

    protected void HandleWebRequestResult(UnityWebRequest webRequest)
    {
        switch (webRequest.result)
        {
            case UnityWebRequest.Result.Success:
                {
                    Response response = JsonUtility.FromJson<Response>(webRequest.downloadHandler.text);
                    OnResponse(response);
                    break;
                }
            default:
                {
                    if (Application.internetReachability == NetworkReachability.NotReachable)
                    {
                        sceneController.DisplayErrorMessage(ErrorMessages.NO_INTERNET);
                    }
                    else
                    {
                        sceneController.DisplayErrorMessage(ErrorMessages.UNEXPECTED_SERVER_ERROR);
                    }
                    break;
                }
        }
        StartCoroutine(DestroyInSeconds(5));
    }

    public bool StopWebRequest()
    {
        if (serverCallCoroutine != null)
        {
            StopCoroutine(serverCallCoroutine);
            serverCallCoroutine = null;
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
