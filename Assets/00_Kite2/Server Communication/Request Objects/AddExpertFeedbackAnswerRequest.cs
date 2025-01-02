using System;

namespace _00_Kite2.Server_Communication.Request_Objects
{
    [Serializable]
    public class AddExpertFeedbackAnswerRequest
    {
        public string expertName;
        public string expertFeedbackAnswer;
        public long idOfExpertFeedbackQuestion;
    }
}