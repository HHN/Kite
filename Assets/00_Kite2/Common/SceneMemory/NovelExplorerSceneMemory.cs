using System.Collections.Generic;

public class NovelExplorerSceneMemory
{
    private int tabIndex;
    private float scrollPositionKiteGallery;
    private float scrollPositionUserGallery;
    private float scrollPositionAccountGallery;
    private float scrollPositionFavoritesGallery;
    private float scrollPositionFilterGallery;
    private string searchPhrase;
    private int radioButtonIndex;
    private List<VisualNovel> userNovels;
    private bool novelsSet;

    public NovelExplorerSceneMemory()
    {
        tabIndex = 0;
        scrollPositionKiteGallery = 0f;
        scrollPositionUserGallery = 0f;
        scrollPositionAccountGallery = 0f;
        scrollPositionFavoritesGallery = 0f;
        scrollPositionFilterGallery = 0f;
        searchPhrase = "";
        radioButtonIndex = 0;
        userNovels = new List<VisualNovel>();
        novelsSet = false;
    }

    public int GetTabIndex()
    {
        return tabIndex;
    }

    public void SetTabIndex(int tabIndex)
    {
        this.tabIndex = tabIndex;
    }

    public float GetScrollPositionOfKiteGallery()
    {
        return scrollPositionKiteGallery;
    }

    public void SetScrollPositionOfKiteGallery(float scrollPosition)
    {
        this.scrollPositionKiteGallery = scrollPosition;
    }

    public float GetScrollPositionOfUserGallery()
    {
        return scrollPositionUserGallery;
    }

    public void SetScrollPositionOfUserGallery(float scrollPosition)
    {
        this.scrollPositionUserGallery = scrollPosition;
    }

    public float GetScrollPositionOfAccountGallery()
    {
        return scrollPositionAccountGallery;
    }

    public void SetScrollPositionOfAccountGallery(float scrollPosition)
    {
        this.scrollPositionAccountGallery = scrollPosition;
    }

    public float GetScrollPositionOfFavoritesGallery()
    {
        return scrollPositionFavoritesGallery;
    }

    public void SetScrollPositionOfFavoritesGallery(float scrollPosition)
    {
        this.scrollPositionFavoritesGallery = scrollPosition;
    }

    public float GetScrollPositionOfFilterGallery()
    {
        return scrollPositionFilterGallery;
    }

    public void SetScrollPositionOfFilterGallery(float scrollPosition)
    {
        this.scrollPositionFilterGallery = scrollPosition;
    }

    public string GetSearchPhrase()
    {
        return searchPhrase;
    }

    public void SetSearchPhrase(string searchPhrase)
    {
        this.searchPhrase = searchPhrase;
    }

    public int GetRadioButtonIndex()
    {
        return radioButtonIndex;
    }

    public void SetRadioButtonIndex(int radioButtonIndex)
    {
        this.radioButtonIndex = radioButtonIndex;
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
