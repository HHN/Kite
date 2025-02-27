using UnityEngine;

namespace Assets._Scripts.Common.Managers
{
    public class ApplicationModeManager
    {
        private static ApplicationModeManager _instance;

        private ApplicationModeWrapper _wrapper;
        private const string KEY = "ApplicationModeWrapper";

        private ApplicationModeManager()
        {
        }

        public static ApplicationModeManager Instance()
        {
            if (_instance == null)
            {
                _instance = new ApplicationModeManager();
            }

            return _instance;
        }

        private void SetApplicationMode(ApplicationModes applicationMode)
        {
            if (_wrapper == null)
            {
                _wrapper = LoadApplicationModeWrapper();
            }

            _wrapper.SetApplicationMode(ApplicationModeHelper.ToInt(applicationMode));
            Save();
        }

        public void ActivateOfflineMode()
        {
            SetApplicationMode(ApplicationModes.OFFLINE_MODE);
        }

        public void ActivateOnlineMode()
        {
            SetApplicationMode(ApplicationModes.ONLINE_MODE);
        }

        private ApplicationModes GetApplicationMode()
        {
            ApplicationModeWrapper wrapper = LoadApplicationModeWrapper();
            return ApplicationModeHelper.ValueOf(wrapper.GetApplicationMode());
        }

        public bool IsOfflineModeActive()
        {
            return GetApplicationMode() == ApplicationModes.OFFLINE_MODE;
        }

        public bool IsOnlineModeActive()
        {
            return GetApplicationMode() == ApplicationModes.ONLINE_MODE;
        }

        private ApplicationModeWrapper LoadApplicationModeWrapper()
        {
            if (PlayerDataManager.Instance().HasKey(KEY))
            {
                string json = PlayerDataManager.Instance().GetPlayerData(KEY);
                return JsonUtility.FromJson<ApplicationModeWrapper>(json);
            }
            else
            {
                ApplicationModeWrapper wrapper = new ApplicationModeWrapper();
                wrapper.SetApplicationMode(ApplicationModeHelper.ToInt(ApplicationModes.ONLINE_MODE));
                return wrapper;
            }
        }

        private void Save()
        {
            string json = JsonUtility.ToJson(_wrapper);
            PlayerDataManager.Instance().SavePlayerData(KEY, json);
        }
    }
}