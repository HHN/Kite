public enum CompletionHandler
{
    NONE,
    DEFAULT_COMPLETION_HANDLER
}

public class CompletionHandlerHelper
{
    public static int ToInt(CompletionHandler completionHandler)
    {
        switch (completionHandler)
        {
            case CompletionHandler.DEFAULT_COMPLETION_HANDLER: return 1;
            default: return 0;
        }
    }

    public static CompletionHandler ValueOf(int completionHandler)
    {
        switch (completionHandler)
        {
            case 1: return CompletionHandler.DEFAULT_COMPLETION_HANDLER;
            default: return CompletionHandler.NONE;
        }
    }
}