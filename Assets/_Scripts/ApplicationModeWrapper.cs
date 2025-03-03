using System;
using UnityEngine;

namespace Assets._Scripts
{
    [Serializable]
    public class ApplicationModeWrapper
    {
        [SerializeField] private int applicationMode;

        public void SetApplicationMode(int mode)
        {
            applicationMode = mode;
        }

        public int GetApplicationMode()
        {
            return applicationMode;
        }
    }
}