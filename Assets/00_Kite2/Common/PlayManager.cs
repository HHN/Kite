using UnityEngine;

public class PlayManager
{
    private static PlayManager instance;
    private VisualNovel novelToPlay;
    private Color colorOfNovel;

    private PlayManager() { }

    public static PlayManager Instance()
    {
        if (instance == null)
        {
            instance = new PlayManager();
        }
        return instance;
    }

    public void SetVisualNovelToPlay(VisualNovel novelToPlay)
    {
        this.novelToPlay = novelToPlay;
    }

    public VisualNovel GetVisualNovelToPlay()
    {
        if (novelToPlay == null)
        {
            return null;
        }
        return novelToPlay;
    }

    public void SetColorOfVisualNovelToPlay(Color colorOfNovel)
    {
        this.colorOfNovel = colorOfNovel;
    }

    public Color GetColorOfVisualNovelToPlay()
    {
        if (colorOfNovel == null)
        {
            return new Color(0, 0, 0);
        }
        return colorOfNovel;
    }
}