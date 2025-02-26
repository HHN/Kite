using System;
using UnityEngine;

namespace Assets._Scripts.Common
{
    [Serializable]
    public class ApplicationModeWrapper
    {
        [SerializeField] private int applicationMode;

        public void SetApplicationMode(int applicationMode)
        {
            this.applicationMode = applicationMode;
        }

        public int GetApplicationMode()
        {
            return this.applicationMode;
        }
    }
}