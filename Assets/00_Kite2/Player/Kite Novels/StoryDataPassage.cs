public class StoryDataPassage
{
    private string start;

    public StoryDataPassage(string start)
    {
        this.start = start;
    }

    public StoryDataPassage()
    {
        this.start = "";
    }

    public string Start
    {
        get { return start; }
        set { start = value; }
    }
}
