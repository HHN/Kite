using _00_Kite2.UserFeedback.FeedbackRoleManager;
using UnityEngine;

namespace _00_Kite2.Common.Managers
{
    public class FeedbackRoleManager
    {
        private static FeedbackRoleManager _instance;
        private const string Key = "FeedbackRole";
        private FeedbackRoleWrapper _feedbackRole;

        private FeedbackRoleManager()
        {
            _feedbackRole = LoadFeedbackRole();
        }

        public static FeedbackRoleManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FeedbackRoleManager();
                }

                return _instance;
            }
        }

        private FeedbackRoleWrapper LoadFeedbackRole()
        {
            if (PlayerDataManager.Instance().HasKey(Key))
            {
                string json = PlayerDataManager.Instance().GetPlayerData(Key);
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

        private void SetFeedbackRole(int role)
        {
            if (_feedbackRole == null)
            {
                _feedbackRole = LoadFeedbackRole();
            }

            _feedbackRole.role = role;
            Save();
        }

        private void Save()
        {
            string json = JsonUtility.ToJson(_feedbackRole);
            PlayerDataManager.Instance().SavePlayerData(Key, json);
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
                case 0:
                {
                    SetFeedbackRole(0);
                    return "Rollen-Zuweisung erfolgreich! Neue Rolle: Standard Benutzer*in";
                }
                case 1:
                {
                    SetFeedbackRole(1);
                    return "Rollen-Zuweisung erfolgreich! Neue Rolle: Novel-Tester*in";
                }
                case 2:
                {
                    SetFeedbackRole(2);
                    return "Rollen-Zuweisung erfolgreich! Neue Rolle: KI-Tester*in";
                }
                case 3:
                {
                    SetFeedbackRole(3);
                    return "Rollen-Zuweisung erfolgreich! Neue Rolle: Tester*in";
                }
                case 4:
                {
                    SetFeedbackRole(4);
                    return "Rollen-Zuweisung erfolgreich! Neue Rolle: Mitarbeiter*in";
                }
                case 5:
                {
                    SetFeedbackRole(5);
                    return "Rollen-Zuweisung erfolgreich! Neue Rolle: Administrator*in";
                }
                default: return "Code nicht erkannt!";
            }
        }
    }
}