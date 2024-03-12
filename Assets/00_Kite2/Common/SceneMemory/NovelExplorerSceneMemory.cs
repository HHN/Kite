
public class NovelExplorerSceneMemory
{
    private float scrollPositionKiteGallery;
    private string searchPhrase;

    public NovelExplorerSceneMemory()
    {
        scrollPositionKiteGallery = 0f;
        searchPhrase = "";
    }

    public float GetScrollPositionOfKiteGallery()
    {
        return scrollPositionKiteGallery;
    }

    public void SetScrollPositionOfKiteGallery(float scrollPosition)
    {
        this.scrollPositionKiteGallery = scrollPosition;
    }

    public string GetSearchPhrase()
    {
        return searchPhrase;
    }

    public void SetSearchPhrase(string searchPhrase)
    {
        this.searchPhrase = searchPhrase;
    }
}
