using System;

namespace Assets._Scripts.Utilities
{
    /// <summary>
    /// A serializable wrapper class to store the user's acceptance status for various terms and conditions.
    /// This is typically used for saving and loading user consent data, often in conjunction with
    /// Unity's JsonUtility or other serialization methods.
    /// </summary>
    [Serializable]
    public class PrivacyAndConditionWrapper
    {
        public bool acceptedConditions;
        public bool acceptedPrivacyTerms;
    }
}