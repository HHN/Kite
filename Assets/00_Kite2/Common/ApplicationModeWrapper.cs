using System;
using UnityEngine;

namespace _00_Kite2.Common
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