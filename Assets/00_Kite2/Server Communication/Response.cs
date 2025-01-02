using System;
using System.Collections.Generic;
using _00_Kite2.ExpertFeedback;
using _00_Kite2.UserFeedback;
using UnityEngine;

namespace _00_Kite2.Server_Communication
{
    [Serializable]
    public class Response
    {
        [SerializeField] private int resultCode;
        [SerializeField] private string resultText;
        [SerializeField] private string completion;
        [SerializeField] private List<NovelReview> novelReviews;
        [SerializeField] private List<AiReview> aiReviews;
        [SerializeField] private List<ReviewObserver> reviewObservers;
        [SerializeField] private List<DataObject> dataObjects;
        [SerializeField] private int version;
        [SerializeField] private int userRole;
        [SerializeField] private List<ExpertFeedbackQuestion> expertFeedbackQuestions;
        [SerializeField] private List<ExpertFeedbackAnswer> expertFeedbackAnswers;

        public void SetResultCode(int resultCode)
        {
            this.resultCode = resultCode;
        }

        public int GetResultCode()
        {
            return this.resultCode;
        }

        public void SetResultText(string resultText)
        {
            this.resultText = resultText;
        }

        public string GetResultText()
        {
            return this.resultText;
        }

        public void SetCompletion(string completion)
        {
            this.completion = completion;
        }

        public string GetCompletion()
        {
            return this.completion;
        }

        public void SetNovelReviews(List<NovelReview> novelReviews)
        {
            this.novelReviews = novelReviews;
        }

        public List<NovelReview> GetNovelReviews()
        {
            return this.novelReviews;
        }

        public void SetAiReviews(List<AiReview> aiReviews)
        {
            this.aiReviews = aiReviews;
        }

        public List<AiReview> GetAiReviews()
        {
            return this.aiReviews;
        }

        public void SetReviewObserver(List<ReviewObserver> reviewObservers)
        {
            this.reviewObservers = reviewObservers;
        }

        public List<ReviewObserver> GetReviewObserver()
        {
            return this.reviewObservers;
        }

        public void SetDataObjects(List<DataObject> dataObjects)
        {
            this.dataObjects = dataObjects;
        }

        public List<DataObject> GetDataObjects()
        {
            return this.dataObjects;
        }

        public void SetVersion(int version)
        {
            this.version = version;
        }

        public int GetVersion()
        {
            return this.version;
        }

        public void SetUserRole(int userRole)
        {
            this.userRole = userRole;
        }

        public int GetUserRole()
        {
            return this.userRole;
        }

        public List<ExpertFeedbackQuestion> GetExpertFeedbackQuestions()
        {
            return expertFeedbackQuestions;
        }

        public void SetExpertFeedbackQuestions(List<ExpertFeedbackQuestion> expertFeedbackQuestions)
        {
            this.expertFeedbackQuestions = expertFeedbackQuestions;
        }

        public List<ExpertFeedbackAnswer> GetExpertFeedbackAnswers()
        {
            return expertFeedbackAnswers;
        }

        public void SetExpertFeedbackAnswers(List<ExpertFeedbackAnswer> expertFeedbackAnswers)
        {
            this.expertFeedbackAnswers = expertFeedbackAnswers;
        }
    }
}