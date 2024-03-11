using System.Collections.Generic;

public class NovelExplorerSceneMemory
{
    private float scrollPositionKiteGallery;
    private string searchPhrase;
    private List<VisualNovel> userNovels;
    private bool novelsSet;

    public NovelExplorerSceneMemory()
    {
        scrollPositionKiteGallery = 0f;
        searchPhrase = "";
        userNovels = new List<VisualNovel>();
        novelsSet = false;
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

    public List<VisualNovel> GetUserNovels()
    {
        return new List<VisualNovel>(userNovels);
    }

    public void SetUserNovels(List<VisualNovel> visualNovels)
    {
        this.userNovels = new List<VisualNovel>(visualNovels);
        novelsSet = true;
    }

    public bool IsUserNovelsSet()
    {
        return novelsSet;
    }
}
