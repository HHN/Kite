using UnityEngine;
using UnityEngine.Networking;

namespace Assets._Scripts
{
    public class BypassCertificate : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            // Nur zum Testen: Immer true zurückgeben
            return true;
        }
    }
}
