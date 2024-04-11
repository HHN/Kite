public class TweeLink
{
    private string text;
    private string target;
    private bool showAfterSelection;

    public TweeLink(string text, string target, bool showAfterSelection)
    {
        this.Text = text;
        this.Target = target;
        this.ShowAfterSelection = showAfterSelection;
    }

    public TweeLink() 
    {
        this.Text = "";
        this.Target = "";
        this.ShowAfterSelection = false;
    }

    public string Text
    {
        get { return text; }
        set { text = value; }
    }

    public string Target
    {
        get { return target; }
        set { target = value; }
    }

    public bool ShowAfterSelection
    {
        get { return showAfterSelection; }
        set { showAfterSelection = value; }
    }
}
