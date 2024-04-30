using UnityEngine;
using System;

public class ExpertFeedbackQuestionManager
{
    private static ExpertFeedbackQuestionManager instance;

    public static ExpertFeedbackQuestionManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ExpertFeedbackQuestionManager();
            }
            return instance;
        }
    }

    private string uuid;

    private ExpertFeedbackQuestionManager()
    {
        LoadUUID();
    }

    public string GetUUID()
    {
        return uuid;
    }

    private void SaveUUID(string uuid)
    {
        PlayerPrefs.SetString("UUID", uuid);
        PlayerPrefs.Save();
    }

    private void LoadUUID()
    {
        string storedUUIDs = PlayerPrefs.GetString("UUID", "");

        if (!string.IsNullOrEmpty(storedUUIDs))
        {
            Guid newGuid = Guid.NewGuid();
            storedUUIDs = newGuid.ToString();
            SaveUUID(storedUUIDs);
        }

        uuid = storedUUIDs;
    }
}
