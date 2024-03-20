using UnityEngine;

public class ApplicationModeManager
{
    private static ApplicationModeManager instance;

    private ApplicationModeWrapper wrapper;
    private const string key = "ApplicationModeWrapper";

    private ApplicationModeManager() {}

    public static ApplicationModeManager Instance()
    {
        if (instance == null)
        {
            instance = new ApplicationModeManager();
        }
        return instance;
    }

    private void SetApplicationMode(ApplicationModes applicationMode)
    {
        if (wrapper == null)
        {
            wrapper = LoadApplicationModeWrapper();
        }
        wrapper.SetApplicationMode(ApplicationModeHelper.ToInt(applicationMode));
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

    public ApplicationModeWrapper LoadApplicationModeWrapper()
    {
        if (PlayerPrefs.HasKey(key))
        {
            string json = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<ApplicationModeWrapper>(json);
        }
        else
        {
            ApplicationModeWrapper wrapper = new ApplicationModeWrapper();
            wrapper.SetApplicationMode(ApplicationModeHelper.ToInt(ApplicationModes.ONLINE_MODE));
            return wrapper;
        }
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(wrapper);
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }
}