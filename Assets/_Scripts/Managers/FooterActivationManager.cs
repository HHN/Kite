using System;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    /// <summary>
    /// Manages the activation state of the footer component and broadcasts changes to subscribers through events.
    /// </summary>
    public class FooterActivationManager
    {
        private static FooterActivationManager _instance;

        public event Action<bool> OnActivationChanged;

        private bool _footerActivated = true;

        private FooterActivationManager() { }

        /// <summary>
        /// Provides a singleton instance of the FooterActivationManager, ensuring a single shared instance throughout the application.
        /// </summary>
        /// <returns>A singleton instance of the FooterActivationManager.</returns>
        public static FooterActivationManager Instance()
        {
            if (_instance == null)
            {
                _instance = new FooterActivationManager();
            }

            return _instance;
        }

        /// <summary>
        /// Sets the activation state of the footer component and notifies all subscribers through the OnActivationChanged event.
        /// </summary>
        /// <param name="activated">A boolean indicating whether the footer component should be activated (true) or deactivated (false).</param>
        public void SetFooterActivated(bool activated)
        {
            _footerActivated = activated;

            OnActivationChanged?.Invoke(_footerActivated);
        }

        /// <summary>
        /// Indicates whether the footer component is currently activated.
        /// </summary>
        /// <returns>A boolean value where true represents the footer being activated, and false represents it being deactivated.</returns>
        public bool IsActivated()
        {
            return _footerActivated;
        }
    }
}