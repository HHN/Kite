using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetNovelsServerCall : ServerCall
{
    protected override object CreateRequestObject()
    {
        throw new System.NotImplementedException();
    }

    protected override UnityWebRequest CreateUnityWebRequestObject()
    {
        return UnityWebRequest.Get(ConnectionLink.NOVELS_LINK);
    }

    protected override void OnResponse(Response response)
    {
        throw new System.NotImplementedException();
    }
}
