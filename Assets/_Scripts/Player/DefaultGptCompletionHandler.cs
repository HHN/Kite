namespace Assets._Scripts.Player
{
    public class DefaultGptCompletionHandler : IGptCompletionHandler
    {
        public string ProcessCompletion(string completion)
        {
            return completion.Trim();
        }
    }
}