using UnityEngine;

public class ScoreManager
{
    private static ScoreManager instance;
    private long score;

    private ScoreManager() { }

    public static ScoreManager Instance()
    {
        if (instance == null)
        {
            instance = new ScoreManager();
        }
        return instance;
    }

    public long GetScore() 
    {
        return score;
    }

    public void SetScore(long score) 
    {  
        this.score = score;
    }
}
