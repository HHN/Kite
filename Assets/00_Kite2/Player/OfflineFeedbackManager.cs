using System.Text;

public class OfflineFeedbackManager
{
    private static OfflineFeedbackManager instance;
    public StringBuilder offlineFeedback;

    private OfflineFeedbackManager() { }

    public static OfflineFeedbackManager Instance()
    {
        if (instance == null)
        {
            instance = new OfflineFeedbackManager();
        }
        return instance;
    }

    public string GetOfflineFeedback()
    {
        if (offlineFeedback == null)
        {
            return "";
        }
        return offlineFeedback.ToString().Trim();
    }

    public void AddLineToPrompt(string line)
    {
        if (offlineFeedback == null)
        {
            offlineFeedback = new StringBuilder();
        }
        offlineFeedback.AppendLine(line);
    }

    public void Clear()
    {
        offlineFeedback = null;
    }
}
