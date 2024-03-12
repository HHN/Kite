public class GameManager
{
    private static GameManager instance;

    private ApplicationModes applicationMode;

    private GameManager() { }

    public static GameManager Instance()
    {
        if (instance == null)
        {
            instance = new GameManager();
        }
        return instance;
    }

    public void SetApplicationMode(ApplicationModes applicationMode)
    {
        this.applicationMode = applicationMode;
    }

    public ApplicationModes GetApplicationModes()
    {
        return applicationMode;
    }
}