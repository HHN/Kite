using UnityEngine;

namespace Assets._Scripts.Common.Managers
{
    public class PrivacyAndConditionManager
    {
        private static PrivacyAndConditionManager _instance;

        private PrivacyAndConditionWrapper _wrapper;
        private const string Key = "PrivacyAndConditionWrapper";

        private PrivacyAndConditionManager()
        {
            _wrapper = LoadPrivacyAndConditionWrapper();
        }

        public static PrivacyAndConditionManager Instance()
        {
            if (_instance == null)
            {
                _instance = new PrivacyAndConditionManager();
            }

            return _instance;
        }

        public void AcceptConditionsOfUsage()
        {
            if (_wrapper == null)
            {
                _wrapper = LoadPrivacyAndConditionWrapper();
            }

            _wrapper.acceptedConditions = true;
            Save();
        }

        public void AcceptTermsOfPrivacy()
        {
            if (_wrapper == null)
            {
                _wrapper = LoadPrivacyAndConditionWrapper();
            }

            _wrapper.acceptedPrivacyTerms = true;
            Save();
        }

        public void AcceptDataCollection()
        {
            if (_wrapper == null)
            {
                _wrapper = LoadPrivacyAndConditionWrapper();
            }

            _wrapper.acceptedDataCollection = true;
            Save();
        }

        public void UnaccepedConditionsOfUsage()
        {
            if (_wrapper == null)
            {
                _wrapper = LoadPrivacyAndConditionWrapper();
            }

            _wrapper.acceptedConditions = false;
            Save();
        }

        public void UnaccepedTermsOfPrivacy()
        {
            if (_wrapper == null)
            {
                _wrapper = LoadPrivacyAndConditionWrapper();
            }

            _wrapper.acceptedPrivacyTerms = false;
            Save();
        }

        public void UnaccepedDataCollection()
        {
            if (_wrapper == null)
            {
                _wrapper = LoadPrivacyAndConditionWrapper();
            }

            _wrapper.acceptedDataCollection = false;
            Save();
        }

        private PrivacyAndConditionWrapper LoadPrivacyAndConditionWrapper()
        {
            if (PlayerDataManager.Instance().HasKey(Key))
            {
                string json = PlayerDataManager.Instance().GetPlayerData(Key);
                return JsonUtility.FromJson<PrivacyAndConditionWrapper>(json);
            }
            else
            {
                return new PrivacyAndConditionWrapper()
                {
                    acceptedConditions = false,
                    acceptedPrivacyTerms = false,
                    acceptedDataCollection = false
                };
            }
        }

        private void Save()
        {
            string json = JsonUtility.ToJson(_wrapper);
            PlayerDataManager.Instance().SavePlayerData(Key, json);
        }

        public bool IsConditionsAccepted()
        {
            PrivacyAndConditionWrapper wrapper = LoadPrivacyAndConditionWrapper();
            return wrapper.acceptedConditions;
        }

        public bool IsPrivacyTermsAccepted()
        {
            PrivacyAndConditionWrapper wrapper = LoadPrivacyAndConditionWrapper();
            return wrapper.acceptedPrivacyTerms;
        }

        public bool IsDataCollectionAccepted()
        {
            PrivacyAndConditionWrapper wrapper = LoadPrivacyAndConditionWrapper();
            return wrapper.acceptedDataCollection;
        }
    }
}