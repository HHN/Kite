using System;
using System.Collections.Generic;
using Assets._Scripts.ExpertFeedback;
using Assets._Scripts.UserFeedback;
using UnityEngine;

namespace Assets._Scripts.ServerCommunication
{
    /// <summary>
    /// Represents a generic response object received from the server.
    /// This class can encapsulate various types of data depending on the server call,
    /// including result codes, text, AI completions, lists of reviews, and other data objects.
    /// It is <see cref="Serializable"/> to allow for easy deserialization from JSON.
    /// </summary>
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

        /// <summary>
        /// Sets the numerical result code of the server response.
        /// </summary>
        /// <param name="resultCode">The integer result code.</param>
        public void SetResultCode(int resultCode)
        {
            this.resultCode = resultCode;
        }

        /// <summary>
        /// Gets the numerical result code of the server response.
        /// </summary>
        /// <returns>The integer result code.</returns>
        public int GetResultCode()
        {
            return resultCode;
        }

        /// <summary>
        /// Sets the descriptive result text of the server response.
        /// </summary>
        /// <param name="resultText">The string result text.</param>
        public void SetResultText(string resultText)
        {
            this.resultText = resultText;
        }

        /// <summary>
        /// Gets the descriptive result text of the server response.
        /// </summary>
        /// <returns>The string result text.</returns>
        public string GetResultText()
        {
            return this.resultText;
        }

        /// <summary>
        /// Sets the AI completion string received in the response.
        /// </summary>
        /// <param name="completion">The AI-generated completion string.</param>
        public void SetCompletion(string completion)
        {
            this.completion = completion;
        }

        /// <summary>
        /// Gets the AI completion string received in the response.
        /// </summary>
        /// <returns>The AI-generated completion string.</returns>
        public string GetCompletion()
        {
            return completion;
        }

        /// <summary>
        /// Sets the list of <see cref="NovelReview"/> objects in the response.
        /// </summary>
        /// <param name="novelReviews">A list of novel review objects.</param>
        public void SetNovelReviews(List<NovelReview> novelReviews)
        {
            this.novelReviews = novelReviews;
        }

        /// <summary>
        /// Gets the list of <see cref="NovelReview"/> objects from the response.
        /// </summary>
        /// <returns>A list of novel review objects.</returns>
        public List<NovelReview> GetNovelReviews()
        {
            return novelReviews;
        }

        /// <summary>
        /// Sets the list of <see cref="AiReview"/> objects in the response.
        /// </summary>
        /// <param name="aiReviews">A list of AI review objects.</param>
        public void SetAiReviews(List<AiReview> aiReviews)
        {
            this.aiReviews = aiReviews;
        }

        /// <summary>
        /// Gets the list of <see cref="AiReview"/> objects from the response.
        /// </summary>
        /// <returns>A list of AI review objects.</returns>
        public List<AiReview> GetAiReviews()
        {
            return aiReviews;
        }

        /// <summary>
        /// Sets the list of <see cref="ReviewObserver"/> objects in the response.
        /// </summary>
        /// <param name="reviewObservers">A list of review observer objects.</param>
        public void SetReviewObserver(List<ReviewObserver> reviewObservers)
        {
            this.reviewObservers = reviewObservers;
        }

        /// <summary>
        /// Gets the list of <see cref="ReviewObserver"/> objects from the response.
        /// </summary>
        /// <returns>A list of review observer objects.</returns>
        public List<ReviewObserver> GetReviewObserver()
        {
            return reviewObservers;
        }

        /// <summary>
        /// Sets the list of <see cref="DataObject"/> objects in the response.
        /// </summary>
        /// <param name="dataObjects">A list of general data objects.</param>
        public void SetDataObjects(List<DataObject> dataObjects)
        {
            this.dataObjects = dataObjects;
        }

        /// <summary>
        /// Gets the list of <see cref="DataObject"/> objects from the response.
        /// </summary>
        /// <returns>A list of general data objects.</returns>
        public List<DataObject> GetDataObjects()
        {
            return dataObjects;
        }

        /// <summary>
        /// Sets the version integer in the response.
        /// </summary>
        /// <param name="version">The integer version number.</param>
        public void SetVersion(int version)
        {
            this.version = version;
        }

        /// <summary>
        /// Gets the version integer from the response.
        /// </summary>
        /// <returns>The integer version number.</returns>
        public int GetVersion()
        {
            return version;
        }

        /// <summary>
        /// Sets the user role integer in the response.
        /// </summary>
        /// <param name="userRole">The integer user role.</param>
        public void SetUserRole(int userRole)
        {
            this.userRole = userRole;
        }

        /// <summary>
        /// Gets the user role integer from the response.
        /// </summary>
        /// <returns>The integer user role.</returns>
        public int GetUserRole()
        {
            return userRole;
        }

        /// <summary>
        /// Gets the list of <see cref="ExpertFeedbackQuestion"/> objects from the response.
        /// </summary>
        /// <returns>A list of expert feedback question objects.</returns>
        public List<ExpertFeedbackQuestion> GetExpertFeedbackQuestions()
        {
            return expertFeedbackQuestions;
        }

        /// <summary>
        /// Sets the list of <see cref="ExpertFeedbackQuestion"/> objects in the response.
        /// </summary>
        /// <param name="expertFeedbackQuestions">A list of expert feedback question objects.</param>
        public void SetExpertFeedbackQuestions(List<ExpertFeedbackQuestion> expertFeedbackQuestions)
        {
            this.expertFeedbackQuestions = expertFeedbackQuestions;
        }

        /// <summary>
        /// Gets the list of <see cref="ExpertFeedbackAnswer"/> objects from the response.
        /// </summary>
        /// <returns>A list of expert feedback answer objects.</returns>
        public List<ExpertFeedbackAnswer> GetExpertFeedbackAnswers()
        {
            return expertFeedbackAnswers;
        }

        /// <summary>
        /// Sets the list of <see cref="ExpertFeedbackAnswer"/> objects in the response.
        /// </summary>
        /// <param name="expertFeedbackAnswers">A list of expert feedback answer objects.</param>
        public void SetExpertFeedbackAnswers(List<ExpertFeedbackAnswer> expertFeedbackAnswers)
        {
            this.expertFeedbackAnswers = expertFeedbackAnswers;
        }
    }
}