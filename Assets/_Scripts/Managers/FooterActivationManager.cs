using UnityEngine;

namespace Assets._Scripts.Managers
{
    public class FooterActivationManager : MonoBehaviour
    {
        
        private static FooterActivationManager _instance;
        
        private bool _footerActivated = true;
        
        public static FooterActivationManager Instance()
        {
            if (_instance == null)
            {
                _instance = new FooterActivationManager();
            }

            return _instance;
        }

        private void SetFooterActivated(bool activated)
        {
            _footerActivated = activated;
        }

        private bool IsActivated()
        {
            return _footerActivated;
        }
    }
}
