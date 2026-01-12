using UnityEngine.Networking;

namespace Assets._Scripts
{
    /// <summary>
    /// A custom <see cref="CertificateHandler"/> designed to bypass standard SSL/TLS certificate validation.
    /// This is typically used in development or testing environments where self-signed or invalid certificates 
    /// would otherwise prevent <c>UnityWebRequest</c> from connecting.
    /// </summary>
    public class BypassCertificate : CertificateHandler
    {
        /// <summary>
        /// Overrides the base certificate validation method.
        /// </summary>
        /// <param name="certificateData">The raw certificate data received from the server.</param>
        /// <returns>
        /// Returns <c>true</c> unconditionally to bypass certificate validation.
        /// NOTE: This should only be used for testing and development purposes.
        /// </returns>
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            // Only for testing: Always return true
            return true;
        }
    }
}
