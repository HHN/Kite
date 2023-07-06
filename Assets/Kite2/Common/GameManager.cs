public class GameManager
{
    private static GameManager instance;

    public ApplicationModes applicationMode;

    private GameManager() { }

    public static GameManager Instance()
    {
        if (instance == null)
        {
            instance = new GameManager();
        }
        return instance;
    }
}
