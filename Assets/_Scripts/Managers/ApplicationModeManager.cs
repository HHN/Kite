using UnityEngine;

namespace Assets._Scripts.Managers
{
    /// <summary>
    /// Manages the application modes, such as offline and online modes, and provides methods to activate
    /// and query the current mode of the application.
    /// </summary>
    public class ApplicationModeManager
    {
        private static ApplicationModeManager _instance;

        private ApplicationModeWrapper _wrapper;
        private const string Key = "ApplicationModeWrapper";

        /// <summary>
        /// Manages the application modes, such as offline and online, by providing methods to activate, store, and retrieve the current mode.
        /// Implements a singleton pattern to ensure a single, shared instance throughout the application.
        /// </summary>
        private ApplicationModeManager()
        {
        }

        /// <summary>
        /// Gets the singleton instance of the ApplicationModeManager. If the instance does
        /// not already exist, a new instance is created and returned.
        /// </summary>
        /// <returns>The singleton instance of ApplicationModeManager.</returns>
        public static ApplicationModeManager Instance()
        {
            if (_instance == null)
            {
                _instance = new ApplicationModeManager();
            }

            return _instance;
        }

        /// <summary>
        /// Sets the current application mode to the provided mode. If the application mode wrapper
        /// is not initialized, it loads the wrapper before updating the mode. The new mode is then saved.
        /// </summary>
        /// <param name="applicationMode">The desired application mode to be set.</param>
        private void SetApplicationMode(ApplicationModes applicationMode)
        {
            if (_wrapper == null)
            {
                _wrapper = LoadApplicationModeWrapper();
            }

            _wrapper.SetApplicationMode(ApplicationModeHelper.ToInt(applicationMode));
            Save();
        }

        /// <summary>
        /// Activates the offline mode for the application by setting the application mode to OfflineMode.
        /// This method ensures that the necessary changes are applied internally to switch the application's state.
        /// </summary>
        public void ActivateOfflineMode()
        {
            SetApplicationMode(ApplicationModes.OfflineMode);
        }

        /// <summary>
        /// Activates the online mode for the application by setting
        /// the current application mode to online.
        /// Ensures the mode is updated within the application's environment
        /// using the internal mode management system.
        /// </summary>
        public void ActivateOnlineMode()
        {
            SetApplicationMode(ApplicationModes.OnlineMode);
        }

        /// <summary>
        /// Retrieves the current application mode by loading the stored mode information
        /// and converting it to the corresponding ApplicationModes enumeration value.
        /// </summary>
        /// <returns>The current application mode as an ApplicationModes enumeration.</returns>
        private ApplicationModes GetApplicationMode()
        {
            ApplicationModeWrapper wrapper = LoadApplicationModeWrapper();
            return ApplicationModeHelper.ValueOf(wrapper.GetApplicationMode());
        }

        /// <summary>
        /// Determines whether the application is currently operating in offline mode.
        /// </summary>
        /// <returns>
        /// A boolean value indicating whether the offline mode is active. Returns true if the offline mode is active, otherwise false.
        /// </returns>
        public bool IsOfflineModeActive()
        {
            return GetApplicationMode() == ApplicationModes.OfflineMode;
        }

        /// <summary>
        /// Determines whether the application is currently in online mode
        /// by verifying the current application mode.
        /// </summary>
        /// <returns>True if the application is in online mode; otherwise, false.</returns>
        public bool IsOnlineModeActive()
        {
            return GetApplicationMode() == ApplicationModes.OnlineMode;
        }

        /// <summary>
        /// Loads and returns an instance of the ApplicationModeWrapper,
        /// ensuring the application's mode is properly initialized and managed.
        /// If the data exists in storage, it retrieves and deserializes it.
        /// Otherwise, it creates a new instance with a default mode and returns it.
        /// </summary>
        /// <returns>A deserialized or newly instantiated ApplicationModeWrapper object that encapsulates the application's current mode.</returns>
        private ApplicationModeWrapper LoadApplicationModeWrapper()
        {
            if (PlayerDataManager.Instance().HasKey(Key))
            {
                string json = PlayerDataManager.Instance().GetPlayerData(Key);
                return JsonUtility.FromJson<ApplicationModeWrapper>(json);
            }
            else
            {
                ApplicationModeWrapper wrapper = new ApplicationModeWrapper();
                wrapper.SetApplicationMode(ApplicationModeHelper.ToInt(ApplicationModes.OnlineMode));
                return wrapper;
            }
        }

        /// <summary>
        /// Persists the current application mode by serializing the ApplicationModeWrapper object into JSON format
        /// and saving it using the PlayerDataManager. Ensures that the application mode data is properly stored
        /// for retrieval across sessions.
        /// </summary>
        private void Save()
        {
            string json = JsonUtility.ToJson(_wrapper);
            PlayerDataManager.Instance().SavePlayerData(Key, json);
        }
    }
}