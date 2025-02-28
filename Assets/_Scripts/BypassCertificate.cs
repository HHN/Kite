using UnityEngine;
using UnityEngine.Networking;

namespace Assets._Scripts
{
    public class BypassCertificate : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            Debug.Log("BypassCertificate");
            // Nur zum Testen: Immer true zurückgeben
            return true;
        }
    }
}
