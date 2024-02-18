using UnityEngine;

public class PrivacyAndConditionManager
{
    private static PrivacyAndConditionManager instance;

    private PrivacyAndConditionWrapper wrapper;
    private const string key = "PrivacyAndConditionWrapper";

    private PrivacyAndConditionManager()
    {
        wrapper = LoadPrivacyAndConditionWrapper();
    }

    public static PrivacyAndConditionManager Instance()
    {
        if (instance == null)
        {
            instance = new PrivacyAndConditionManager();
        }
        return instance;
    }

    public void AcceptConditionsOfUssage()
    {
        if (wrapper == null)
        {
            wrapper = LoadPrivacyAndConditionWrapper();
        }
        wrapper.acceptedConditions = true;
        Save();
    }

    public void AcceptTermsOfPrivacy()
    {
        if (wrapper == null)
        {
            wrapper = LoadPrivacyAndConditionWrapper();
        }
        wrapper.acceptedPrivacyTerms = true;
        Save();
    }

    public void AcceptDataCollection()
    {
        if (wrapper == null)
        {
            wrapper = LoadPrivacyAndConditionWrapper();
        }
        wrapper.acceptedDataCollection = true;
        Save();
    }

    public void UnacceptConditionsOfUssage()
    {
        if (wrapper == null)
        {
            wrapper = LoadPrivacyAndConditionWrapper();
        }
        wrapper.acceptedConditions = false;
        Save();
    }

    public void UnacceptTermsOfPrivacy()
    {
        if (wrapper == null)
        {
            wrapper = LoadPrivacyAndConditionWrapper();
        }
        wrapper.acceptedPrivacyTerms = false;
        Save();
    }

    public void UnacceptDataCollection()
    {
        if (wrapper == null)
        {
            wrapper = LoadPrivacyAndConditionWrapper();
        }
        wrapper.acceptedDataCollection = false;
        Save();
    }

    public PrivacyAndConditionWrapper LoadPrivacyAndConditionWrapper()
    {
        if (PlayerPrefs.HasKey(key))
        {
            string json = PlayerPrefs.GetString(key);
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

    public void Save()
    {
        string json = JsonUtility.ToJson(wrapper);
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }

    public bool IsConditionsAccepted()
    {
        PrivacyAndConditionWrapper wrapper = LoadPrivacyAndConditionWrapper();
        return wrapper.acceptedConditions;
    }

    public bool IsPriavcyTermsAccepted()
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
