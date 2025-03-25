using System;
using UnityEngine;

namespace Assets._Scripts.ExpertFeedback
{
    [Serializable]
    public class ExpertFeedbackQuestion
    {
        [SerializeField] private long id;
        [SerializeField] private long novelId;
        [SerializeField] private string novelName;
        [SerializeField] private string userUuid;
        [SerializeField] private string prompt;
        [SerializeField] private string aiFeedback;
        [SerializeField] private string dialogue;
        [SerializeField] private string expertFeedbackQuestion;
        [SerializeField] private ExpertFeedbackAnswer expertFeedbackAnswer;

        public long GetId()
        {
            return id;
        }

        public void SetId(long id)
        {
            this.id = id;
        }

        public long GetNovelId()
        {
            return novelId;
        }

        public void SetNovelId(long novelId)
        {
            this.novelId = novelId;
        }

        public string GetNovelName()
        {
            return novelName;
        }

        public void SetNovelName(string novelName)
        {
            this.novelName = novelName;
        }

        public string GetUserUuid()
        {
            return userUuid;
        }

        public void SetUserUuid(string userUuid)
        {
            this.userUuid = userUuid;
        }

        public string GetPrompt()
        {
            return prompt;
        }

        public void SetPrompt(string prompt)
        {
            this.prompt = prompt;
        }

        public string GetAiFeedback()
        {
            return aiFeedback;
        }

        public void SetAiFeedback(string aiFeedback)
        {
            this.aiFeedback = aiFeedback;
        }

        public string GetDialogue()
        {
            return dialogue;
        }

        public void SetDialogue(string dialogue)
        {
            this.dialogue = dialogue;
        }

        public string GetExpertFeedbackQuestion()
        {
            return expertFeedbackQuestion;
        }

        public void SetExpertFeedbackQuestion(string expertFeedbackQuestion)
        {
            this.expertFeedbackQuestion = expertFeedbackQuestion;
        }

        public ExpertFeedbackAnswer GetExpertFeedbackAnswer()
        {
            return expertFeedbackAnswer;
        }

        public void SetExpertFeedbackAnswer(ExpertFeedbackAnswer expertFeedbackAnswer)
        {
            this.expertFeedbackAnswer = expertFeedbackAnswer;
        }
    }
}