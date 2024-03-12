using System;
using UnityEngine;

[Serializable]
public class AiReview
{
    [SerializeField] private long id;
    [SerializeField] private long novelId;
    [SerializeField] private string novelName;
    [SerializeField] private string prompt;
    [SerializeField] private string aiFeedback;
    [SerializeField] private string reviewText;

    public void SetId(long id)
    {
        this.id = id;
    }

    public long GetId()
    {
        return id;
    }

    public void SetNovelId(long novelId)
    {
        this.novelId = novelId;
    }

    public long GetNovelId()
    {
        return novelId;
    }

    public void SetNovelName(string novelName)
    {
        this.novelName = novelName;
    }

    public string GetNovelName()
    {
        return novelName;
    }

    public void SetPrompt(string prompt)
    {
        this.prompt = prompt;
    }

    public string GetPrompt()
    {
        return prompt;
    }

    public void SetAiFeedback(string aiFeedback)
    {
        this.aiFeedback = aiFeedback;
    }

    public string GetAiFeedback()
    {
        return aiFeedback;
    }

    public void SetReviewText(string reviewText)
    {
        this.reviewText = reviewText;
    }

    public string GetReviewText()
    {
        return reviewText;
    }
}
