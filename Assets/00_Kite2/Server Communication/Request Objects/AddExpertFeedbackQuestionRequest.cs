using System;

[Serializable]
public class AddExpertFeedbackQuestionRequest
{
    public long novelId;
    public string novelName;
    public string userUuid;
    public string prompt;
    public string aiFeedback;
    public string dialogue;
    public string expertFeedbackQuestion;
}
