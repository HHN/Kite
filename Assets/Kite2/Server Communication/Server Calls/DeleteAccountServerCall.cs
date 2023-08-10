using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DeleteAccountServerCall : ServerCall
{
    protected override object CreateRequestObject()
    {
        DeleteAccountRequest logoutRequest = new DeleteAccountRequest();
        return logoutRequest;
    }

    protected override UnityWebRequest CreateUnityWebRequestObject()
    {
        UnityWebRequest request = UnityWebRequest.Delete(ConnectionLink.DELETE_ACCOUNT_LINK);
        request.downloadHandler = new DownloadHandlerBuffer();
        return request;
    }

    protected override void OnResponse(Response response)
    {
        switch (ResultCodeHelper.ValueOf(response.resultCode))
        {
            case ResultCode.SUCCESSFULLY_DELETED_USER:
                {
                    onSuccessHandler.OnSuccess(response);
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
