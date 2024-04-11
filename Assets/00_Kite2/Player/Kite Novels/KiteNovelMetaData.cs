using System.Collections.Generic;

public class KiteNovelMetaData
{
    private long idNumberOfNovel;
    private string titleOfNovel;
    private string descriptionOfNovel;
    private long idNumberOfRepresentationImage;
    private string contextForPrompt;
    private bool isKite2Novel;
    private string startLocation;
    private bool isWithStartValues;
    private string startTalkingPartnerEmotion;
    private string talkingPartner01;
    private string talkingPartner02;
    private string talkingPartner03;
    private List<WordPair> wordsToReplace;

    public KiteNovelMetaData()
    {
        IdNumberOfNovel = 0;
        TitleOfNovel = "";
        DescriptionOfNovel = "";
        IdNumberOfRepresentationImage = 0;
        ContextForPrompt = "";
        IsKite2Novel = false;
        StartLocation = "";
        IsWithStartValues = false;
        StartTalkingPartnerEmotion = "";
        TalkingPartner01 = "";
        TalkingPartner02 = "";
        TalkingPartner03 = "";
        WordsToReplace = new List<WordPair>();
    }

    public long IdNumberOfNovel
    {
        get { return idNumberOfNovel; }
        set { idNumberOfNovel = value; }
    }

    public string TitleOfNovel
    {
        get { return titleOfNovel; }
        set { titleOfNovel = value; }
    }

    public string DescriptionOfNovel
    {
        get { return descriptionOfNovel; }
        set { descriptionOfNovel = value; }
    }

    public long IdNumberOfRepresentationImage
    {
        get { return idNumberOfRepresentationImage; }
        set { idNumberOfRepresentationImage = value; }
    }

    public string ContextForPrompt
    {
        get { return contextForPrompt; }
        set { contextForPrompt = value; }
    }

    public bool IsKite2Novel
    {
        get { return isKite2Novel; }
        set { isKite2Novel = value; }
    }

    public string StartLocation
    {
        get { return startLocation; }
        set { startLocation = value; }
    }

    public bool IsWithStartValues
    {
        get { return isWithStartValues; }
        set { isWithStartValues = value; }
    }

    public string StartTalkingPartnerEmotion
    {
        get { return startTalkingPartnerEmotion; }
        set { startTalkingPartnerEmotion = value; }
    }

    public string TalkingPartner01
    {
        get { return talkingPartner01; }
        set { talkingPartner01 = value; }
    }

    public string TalkingPartner02
    {
        get { return talkingPartner02; }
        set { talkingPartner02 = value; }
    }

    public string TalkingPartner03
    {
        get { return talkingPartner03; }
        set { talkingPartner03 = value; }
    }

    public List<WordPair> WordsToReplace
    {
        get { return wordsToReplace; }
        set { wordsToReplace = value; }
    }
}