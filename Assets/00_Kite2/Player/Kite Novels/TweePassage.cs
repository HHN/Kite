using System.Collections.Generic;

public class TweePassage
{
    private string label;
    private string passage;
    private List<TweeLink> links;

    public TweePassage(string label, string passage, List<TweeLink> links)
    {
        this.Label = label;
        this.Passage = passage;
        this.Links = links ?? new List<TweeLink>();
    }

    public TweePassage()
    {
        this.Label = "";
        this.Passage = "";
        this.Links = new List<TweeLink>();
    }

    public string Label
    {
        get { return label; }
        set { label = value; }
    }

    public string Passage
    {
        get { return passage; }
        set { passage = value; }
    }

    public List<TweeLink> Links
    {
        get { return links; }
        set { links = value; }
    }
}
