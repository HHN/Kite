using System;

namespace Assets._Scripts.Common.Managers
{
    public class ExpertFeedbackQuestionManager
    {
        private static ExpertFeedbackQuestionManager _instance;
        private string _uuid;

        public static ExpertFeedbackQuestionManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ExpertFeedbackQuestionManager();
                }

                return _instance;
            }
        }

        private ExpertFeedbackQuestionManager()
        {
            LoadUUID();
        }

        public string GetUUID()
        {
            return _uuid;
        }

        private void SaveUUID(string uuid)
        {
            PlayerDataManager.Instance().SavePlayerData("UUID", uuid);
        }

        private void LoadUUID()
        {
            string storedUUIDs = PlayerDataManager.Instance().GetPlayerData("UUID");

            if (!string.IsNullOrEmpty(storedUUIDs))
            {
                Guid newGuid = Guid.NewGuid();
                storedUUIDs = newGuid.ToString();
                SaveUUID(storedUUIDs);
            }

            _uuid = storedUUIDs;
        }
    }
}