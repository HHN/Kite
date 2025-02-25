using UnityEngine;
using UnityEngine.Networking;

namespace _00_Kite2
{
    public class BypassCertificate : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            Debug.Log("BypassCertificate");
            // Nur zum Testen: Immer true zur�ckgeben
            return true;
        }
    }
}
