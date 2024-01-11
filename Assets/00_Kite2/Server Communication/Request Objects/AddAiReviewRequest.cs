using System;

[Serializable]
public class AddAiReviewRequest
{
    public long novelId;
    public string novelName;
    public string prompt;
    public string aiFeedback;
    public string reviewText;
}
