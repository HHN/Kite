using System;

[Serializable]
public class AddNovelReviewRequest
{
    public long novelId;
    public string novelName;
    public long rating;
    public string reviewText;
}
