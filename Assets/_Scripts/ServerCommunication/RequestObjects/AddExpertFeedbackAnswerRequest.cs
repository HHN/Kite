using System;

namespace Assets._Scripts.ServerCommunication.RequestObjects
{
    [Serializable]
    public class AddExpertFeedbackAnswerRequest
    {
        public string expertName;
        public string expertFeedbackAnswer;
        public long idOfExpertFeedbackQuestion;
    }
}