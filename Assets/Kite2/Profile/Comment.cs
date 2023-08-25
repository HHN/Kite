using System;

[Serializable]
public class Comment
{
    public long id;
    public string comment;
    public VisualNovel visualNovel;
    public string author;
    public long likeCount;
    public bool liked;
}
