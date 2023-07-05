using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetCompletionServerCall : ServerCall
{
    protected override object CreateRequestObject()
    {
        throw new System.NotImplementedException();
    }

    protected override UnityWebRequest CreateUnityWebRequestObject()
    {
        return UnityWebRequest.Post(ConnectionLink.COMPLETION_LINK, string.Empty);
    }

    protected override void OnResponse(Response response)
    {
        throw new System.NotImplementedException();
    }
}
