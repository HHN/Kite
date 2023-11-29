
public class DefaultGptCompletionHandler : GptCompletionHandler
{
    public string ProcessCompletion(string completion)
    {
        return completion.Trim();
    }
}
