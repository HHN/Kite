using System;
using UnityEngine;

public class PlayManager
{
    private static PlayManager instance;
    private VisualNovel novelToPlay;
    private Color backgroundColorForNovel;
    private Color foregroundColorForNovel;
    private string displayName;

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

    public void SetBackgroundColorOfVisualNovelToPlay(Color colorOfNovel)
    {
        this.backgroundColorForNovel = colorOfNovel;
    }

    public Color GetBackgroundColorOfVisualNovelToPlay()
    {
        if (backgroundColorForNovel == null)
        {
            return new Color(0, 0, 0);
        }
        return backgroundColorForNovel;
    }

    public void SetForegroundColorOfVisualNovelToPlay(Color colorOfNovel)
    {
        this.foregroundColorForNovel = colorOfNovel;
    }

    public Color GetForegroundColorOfVisualNovelToPlay()
    {
        if (foregroundColorForNovel == null)
        {
            return new Color(0, 0, 0);
        }
        return foregroundColorForNovel;
    }

    public void SetDiplayNameOfNovelToPlay(string v)
    {
        this.displayName = v;
    }

    public string GetDisplayNameOfNovelToPlay()
    {
        return this.displayName;
    }
}