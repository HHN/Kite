using System.Text;

namespace _00_Kite2.Common.Managers
{
    public class OfflineFeedbackManager
    {
        private static OfflineFeedbackManager _instance;
        private StringBuilder _offlineFeedback;

        private OfflineFeedbackManager()
        {
        }

        public static OfflineFeedbackManager Instance()
        {
            if (_instance == null)
            {
                _instance = new OfflineFeedbackManager();
            }

            return _instance;
        }

        public string GetOfflineFeedback()
        {
            if (_offlineFeedback == null)
            {
                return "";
            }

            return _offlineFeedback.ToString().Trim();
        }

        public void AddLineToPrompt(string line)
        {
            if (_offlineFeedback == null)
            {
                _offlineFeedback = new StringBuilder();
            }

            _offlineFeedback.AppendLine(line);
        }

        public void Clear()
        {
            _offlineFeedback = null;
        }
    }
}