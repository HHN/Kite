namespace Assets._Scripts.Player
{
    public interface IGptCompletionHandler
    {
        string ProcessCompletion(string completion);
    }
}