using UnityEngine;
public class FeedbackRoleManager
{
    private static FeedbackRoleManager instance;
    private const string KEY = "FeedbackRole";
    private FeedbackRoleWrapper feedbackRole;

    private FeedbackRoleManager() 
    {
        feedbackRole = LoadFeedbackRole();
    }

    public static FeedbackRoleManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new FeedbackRoleManager();
            }
            return instance;
        }
    }

    public FeedbackRoleWrapper LoadFeedbackRole()
    {
        if (PlayerDataManager.Instance().HasKey(KEY))
        {
            string json = PlayerDataManager.Instance().GetPlayerData(KEY);
            return JsonUtility.FromJson<FeedbackRoleWrapper>(json);
        }
        else
        {
            return new FeedbackRoleWrapper()
            {
                role = 0
            };
        }
    }

    public void SetFeedbackRole(int role)
    {
        if (feedbackRole == null)
        {
            feedbackRole = LoadFeedbackRole();
        }

        feedbackRole.role = role;
        Save();
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(feedbackRole);
        PlayerDataManager.Instance().SavePlayerData(KEY, json);
    }

    public int GetFeedbackRole()
    {
        FeedbackRoleWrapper wrapper = LoadFeedbackRole();
        return wrapper.role;
    }
    
    public string GetFeedbackRoleName()
    {
        FeedbackRoleWrapper wrapper = LoadFeedbackRole();
        
        switch (wrapper.role)
        {
            case 5: return "Administrator*in";
            case 4: return "Mitarbeiter*in";
            case 3: return "Tester*in";
            case 2: return "KI-Tester*in";
            case 1: return "Novel-Tester*in";
            default: return "Standard Benutzer*in";
        }
    }

    public string SubmitCode(int code)
    {
        switch (code)
        {
            case (0):
                {
                    SetFeedbackRole(0);
                    return "Rollen-Zuweisung erfolgreich! Neue Rolle: Standard Benutzer*in";
                }
            case (1):
                {
                    SetFeedbackRole(1);
                    return "Rollen-Zuweisung erfolgreich! Neue Rolle: Novel-Tester*in";
                }
            case (2):
                {
                    SetFeedbackRole(2);
                    return "Rollen-Zuweisung erfolgreich! Neue Rolle: KI-Tester*in";
                }
            case (3):
                {
                    SetFeedbackRole(3);
                    return "Rollen-Zuweisung erfolgreich! Neue Rolle: Tester*in";
                }
            case (4):
                {
                    SetFeedbackRole(4);
                    return "Rollen-Zuweisung erfolgreich! Neue Rolle: Mitarbeiter*in";
                }
            case (5):
                {
                    SetFeedbackRole(5);
                    return "Rollen-Zuweisung erfolgreich! Neue Rolle: Administrator*in";
                }
            default: return "Code nicht erkannt!";
        }
    }
}