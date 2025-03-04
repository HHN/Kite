using System;
using UnityEngine;

namespace Assets._Scripts.ExpertFeedback
{
    [Serializable]
    public class ExpertFeedbackAnswer
    {
        [SerializeField] private long id;
        [SerializeField] private string expertName;
        [SerializeField] private string expertFeedbackAnswer;
        [SerializeField] private long idOfExpertFeedbackQuestion;

        public long GetId()
        {
            return id;
        }

        public void SetId(long id)
        {
            this.id = id;
        }

        public string GetExpertName()
        {
            return expertName;
        }

        public void SetExpertName(string expertName)
        {
            this.expertName = expertName;
        }

        public string GetExpertFeedbackAnswer()
        {
            return expertFeedbackAnswer;
        }

        public void SetExpertFeedbackAnswer(string expertFeedbackAnswer)
        {
            this.expertFeedbackAnswer = expertFeedbackAnswer;
        }

        public long GetIdOfExpertFeedbackQuestion()
        {
            return idOfExpertFeedbackQuestion;
        }

        public void SetIdOfExpertFeedbackQuestion(long idOfExpertFeedbackQuestion)
        {
            this.idOfExpertFeedbackQuestion = idOfExpertFeedbackQuestion;
        }
    }
}