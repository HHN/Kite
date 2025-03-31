namespace Assets._Scripts.Novel
{
    public enum CompletionHandler
    {
        None,
        DefaultCompletionHandler
    }

    public class CompletionHandlerHelper
    {
        public static int ToInt(CompletionHandler completionHandler)
        {
            return completionHandler switch
            {
                CompletionHandler.DefaultCompletionHandler => 1,
                _ => 0
            };
        }

        public static CompletionHandler ValueOf(int completionHandler)
        {
            return completionHandler switch
            {
                1 => CompletionHandler.DefaultCompletionHandler,
                _ => CompletionHandler.None
            };
        }
    }
}