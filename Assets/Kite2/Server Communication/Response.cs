using System;
using System.Collections.Generic;

[Serializable]
public class Response
{
    public int resultCode;
    public string resultText;
    public string authToken;
    public string refreshToken;
    public string completion;
    public List<VisualNovel> novels;
    public List<Comment> comments;
    public VisualNovel specifiedNovel;
    public int numberOfNovelLikes;
    public bool novelLikedByUser;
    public long score;
    public long money;
}
