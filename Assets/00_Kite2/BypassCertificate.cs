using UnityEngine.Networking;
using System.Collections;
using UnityEngine;

public class BypassCertificate : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        Debug.Log("BypassCertificate");
        // Nur zum Testen: Immer true zurückgeben
        return true;
    }
}
