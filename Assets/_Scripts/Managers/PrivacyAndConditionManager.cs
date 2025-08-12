using Assets._Scripts.Utilities;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    /// <summary>
    /// Manages the acceptance and storage of privacy and terms of use conditions.
    /// Ensures a singleton instance for centralized management of user agreement data.
    /// </summary>
    public class PrivacyAndConditionManager
    {
        private static PrivacyAndConditionManager _instance;

        private PrivacyAndConditionWrapper _wrapper;
        private const string Key = "PrivacyAndConditionWrapper";

        /// <summary>
        /// Manages user agreements for privacy, terms of use, and data collection.
        /// Provides a singleton instance to retrieve or update the current user consent status.
        /// Responsible for loading, saving, and checking consent data as needed.
        /// </summary>
        private PrivacyAndConditionManager()
        {
            _wrapper = LoadPrivacyAndConditionWrapper();
        }

        /// <summary>
        /// Provides a singleton instance of the PrivacyAndConditionManager.
        /// Ensures centralized access to manage user consent for terms, privacy, and data collection.
        /// </summary>
        /// <returns>
        /// The singleton instance of PrivacyAndConditionManager.
        /// </returns>
        public static PrivacyAndConditionManager Instance()
        {
            if (_instance == null)
            {
                _instance = new PrivacyAndConditionManager();
            }

            return _instance;
        }

        /// <summary>
        /// Marks the conditions of use as accepted by the user.
        /// Updates the internal state to reflect the acceptance status, ensuring it is saved persistently.
        /// Loads the privacy and condition wrapper if not already initialized and sets the acceptance flag.
        /// </summary>
        public void AcceptConditionsOfUsage()
        {
            if (_wrapper == null)
            {
                _wrapper = LoadPrivacyAndConditionWrapper();
            }

            _wrapper.acceptedConditions = true;
            Save();
        }

        /// <summary>
        /// Marks the user's acceptance of the privacy terms in the application's data management component.
        /// Updates the internal storage to set the acceptance status for privacy terms to true and persists the change.
        /// Ensures the relevant wrapper is loaded and prepared before processing the acceptance.
        /// </summary>
        public void AcceptTermsOfPrivacy()
        {
            if (_wrapper == null)
            {
                _wrapper = LoadPrivacyAndConditionWrapper();
            }

            _wrapper.acceptedPrivacyTerms = true;
            Save();
        }

        /// <summary>
        /// Marks the user's consent to data collection as accepted.
        /// Updates the internal privacy wrapper to reflect the acceptance status for data collection.
        /// Triggers saving of the updated consent data to persistent storage.
        /// </summary>
        public void AcceptDataCollection()
        {
            if (_wrapper == null)
            {
                _wrapper = LoadPrivacyAndConditionWrapper();
            }

            _wrapper.acceptedDataCollection = true;
            Save();
        }

        /// <summary>
        /// Marks the conditions of usage as not accepted.
        /// Updates the internal state of the PrivacyAndConditionWrapper to reflect
        /// that the conditions of usage have not been agreed upon.
        /// Responsible for saving the updated state to persistent storage.
        /// </summary>
        public void UnacceptedConditionsOfUsage()
        {
            if (_wrapper == null)
            {
                _wrapper = LoadPrivacyAndConditionWrapper();
            }

            _wrapper.acceptedConditions = false;
            Save();
        }

        /// <summary>
        /// Marks the terms of privacy as unaccepted by updating the relevant data wrapper.
        /// Ensures that the acceptance status for privacy terms is set to false,
        /// and triggers saving the updated status to persistent storage.
        /// </summary>
        public void UnacceptedTermsOfPrivacy()
        {
            if (_wrapper == null)
            {
                _wrapper = LoadPrivacyAndConditionWrapper();
            }

            _wrapper.acceptedPrivacyTerms = false;
            Save();
        }

        /// <summary>
        /// Marks the user's data collection preference as unaccepted.
        /// Updates the PrivacyAndConditionWrapper instance to reflect that data collection
        /// has not been accepted and ensures that the updated data is saved persistently.
        /// </summary>
        public void UnacceptedDataCollection()
        {
            if (_wrapper == null)
            {
                _wrapper = LoadPrivacyAndConditionWrapper();
            }

            _wrapper.acceptedDataCollection = false;
            Save();
        }

        /// <summary>
        /// Loads the current state of privacy and condition settings from persistent storage.
        /// Returns a deserialized PrivacyAndConditionWrapper instance if data exists,
        /// or creates a new default instance with all options set to unaccepted if no data is found.
        /// </summary>
        /// <returns>
        /// A PrivacyAndConditionWrapper instance containing the user's accepted privacy, terms, and data collection settings.
        /// </returns>
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

        /// <summary>
        /// Saves the current state of privacy and terms of use data.
        /// Serializes the PrivacyAndConditionWrapper object into JSON format and stores it persistently using PlayerDataManager.
        /// Ensures updated data is safely written to storage.
        /// </summary>
        private void Save()
        {
            string json = JsonUtility.ToJson(_wrapper);
            PlayerDataManager.Instance().SavePlayerData(Key, json);
        }

        /// <summary>
        /// Determines if the user has accepted the required conditions of use.
        /// Retrieves the current state of acceptance from the privacy and condition data storage.
        /// </summary>
        /// <returns>
        /// True if the conditions of use have been accepted; otherwise, false.
        /// </returns>
        public bool IsConditionsAccepted()
        {
            PrivacyAndConditionWrapper wrapper = LoadPrivacyAndConditionWrapper();
            return wrapper.acceptedConditions;
        }

        /// <summary>
        /// Checks if the user has accepted the privacy terms.
        /// Retrieves the acceptance status from the stored privacy and condition wrapper.
        /// </summary>
        /// <returns>True if the user has accepted the privacy terms; otherwise, false.</returns>
        public bool IsPrivacyTermsAccepted()
        {
            PrivacyAndConditionWrapper wrapper = LoadPrivacyAndConditionWrapper();
            return wrapper.acceptedPrivacyTerms;
        }

        /// <summary>
        /// Determines whether the user has accepted data collection policies.
        /// Retrieves the acceptance status from the stored privacy and condition wrapper.
        /// </summary>
        /// <returns>True if data collection is accepted; otherwise, false.</returns>
        public bool IsDataCollectionAccepted()
        {
            PrivacyAndConditionWrapper wrapper = LoadPrivacyAndConditionWrapper();
            return wrapper.acceptedDataCollection;
        }
    }
}