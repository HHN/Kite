using System.Collections.Generic;

public class KiteNovelList
{
    private List<string> visualNovels;

    public KiteNovelList(List<string> visualNovels)
    {
        this.visualNovels = visualNovels;
    }

    public KiteNovelList()
    {
        visualNovels = new List<string>();
    }

    public List<string> VisualNovels
    {
        get { return visualNovels; }
        set { visualNovels = value; }
    }
}
