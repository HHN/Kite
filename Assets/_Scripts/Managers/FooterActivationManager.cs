// FooterActivationManager.cs
using System;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    public class FooterActivationManager
    {
        private static FooterActivationManager _instance;

        // 1. Event, das feuert, wenn sich der Aktivierungszustand ändert.
        public event Action<bool> OnActivationChanged;

        private bool _footerActivated = true;

        private FooterActivationManager() { }

        public static FooterActivationManager Instance()
        {
            if (_instance == null)
            {
                _instance = new FooterActivationManager();
            }

            return _instance;
        }

        public void SetFooterActivated(bool activated)
        {
            // if (_footerActivated == activated)
            //     return;

            _footerActivated = activated;

            // 2. Event feuern, damit alle Abonnenten reagieren können.
            OnActivationChanged?.Invoke(_footerActivated);
        }

        public bool IsActivated()
        {
            return _footerActivated;
        }
    }
}