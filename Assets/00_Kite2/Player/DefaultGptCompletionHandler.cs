namespace _00_Kite2.Player
{
    public class DefaultGptCompletionHandler : IGptCompletionHandler
    {
        public string ProcessCompletion(string completion)
        {
            return completion.Trim();
        }
    }
}