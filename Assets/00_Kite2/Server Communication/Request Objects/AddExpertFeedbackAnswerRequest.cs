using System;

[Serializable]
public class AddExpertFeedbackAnswerRequest
{
    public string expertName;
    public string expertFeedbackAnswer;
    public long idOfExpertFeedbackQuestion;
}
