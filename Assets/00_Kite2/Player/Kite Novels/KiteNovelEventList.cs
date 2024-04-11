using System.Collections.Generic;

public class KiteNovelEventList
{
    private List<VisualNovelEvent> novelEvents;

    public KiteNovelEventList()
    {
        novelEvents = new List<VisualNovelEvent>();
    }

    public KiteNovelEventList(List<VisualNovelEvent> novelEvents)
    {
        this.novelEvents = novelEvents;
    }

    public List<VisualNovelEvent> NovelEvents
    {
        get { return novelEvents; }
        set { novelEvents = value; }
    }
}
