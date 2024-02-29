using System.Security.Cryptography.X509Certificates;
using UnityEngine.Networking;
using UnityEngine;

public class CustomCertificateHandler : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        //Check for the finger print of our self signed server certificate
        var certificate = new X509Certificate2(certificateData);
        return certificate.GetCertHashString() == "448CDE2E5E0E5B42498C2E9FAC9553C0ED600325"; 
    }
}