using System;

namespace Assets._Scripts.Server_Communication.Request_Objects
{
    [Serializable]
    public class AddExpertFeedbackAnswerRequest
    {
        public string expertName;
        public string expertFeedbackAnswer;
        public long idOfExpertFeedbackQuestion;
    }
}