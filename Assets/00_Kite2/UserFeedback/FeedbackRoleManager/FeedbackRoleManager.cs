using UnityEngine;
public class FeedbackRoleManager
{
    private static FeedbackRoleManager instance;
    private const string KEY = "FeedbackRole";
    private FeedbackRoleWrapper feedbackRole;

    private const string CODE_STANDARD_USER = "2579713217761";
    private const string CODE_NOVEL_TESTER = "09530779823";
    private const string CODE_AI_TESTER = "93515608172734";
    private const string CODE_TESTER = "6463423290";
    private const string CODE_INTERN = "436937269687567";
    private const string CODE_SUPER_USER = "602740213";

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
        if (PlayerPrefs.HasKey(KEY))
        {
            string json = PlayerPrefs.GetString(KEY);
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
        PlayerPrefs.SetString(KEY, json);
        PlayerPrefs.Save();
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

    public string SubmitCode(string code)
    {
        if (code.Equals(CODE_STANDARD_USER))
        {
            SetFeedbackRole(0);
            return "Rollen-Zuweisung erfolgreich! Neue Rolle: Standard Benutzer*in";
        }
        if (code.Equals(CODE_NOVEL_TESTER))
        {
            SetFeedbackRole(1);
            return "Rollen-Zuweisung erfolgreich! Neue Rolle: Novel-Tester*in";
        }
        if (code.Equals(CODE_AI_TESTER))
        {
            SetFeedbackRole(2);
            return "Rollen-Zuweisung erfolgreich! Neue Rolle: KI-Tester*in";
        }
        if (code.Equals(CODE_TESTER))
        {
            SetFeedbackRole(3);
            return "Rollen-Zuweisung erfolgreich! Neue Rolle: Tester*in";
        }
        if (code.Equals(CODE_INTERN))
        {
            SetFeedbackRole(4);
            return "Rollen-Zuweisung erfolgreich! Neue Rolle: Mitarbeiter*in";
        }
        if (code.Equals(CODE_SUPER_USER))
        {
            SetFeedbackRole(5);
            return "Rollen-Zuweisung erfolgreich! Neue Rolle: Administrator*in";
        }
        return "Code nicht erkannt!";
    }
}