public class WordPair
{
    private string wordToReplace;
    private string replaceByValue;

    public WordPair(string wordToReplace, string replaceByValue)
    {
        WordToReplace = wordToReplace;
        ReplaceByValue = replaceByValue;
    }

    public WordPair()
    {
        WordToReplace = "";
        ReplaceByValue = "";
    }

    public string WordToReplace
    {
        get { return wordToReplace; }
        set { wordToReplace = value; }
    }

    public string ReplaceByValue
    {
        get { return replaceByValue; }
        set { replaceByValue = value; }
    }
}
