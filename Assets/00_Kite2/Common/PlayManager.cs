public class PlayManager
{
    private static PlayManager instance;
    private VisualNovel novelToPlay;

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
}