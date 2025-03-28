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
            switch (completionHandler)
            {
                case CompletionHandler.DefaultCompletionHandler: return 1;
                default: return 0;
            }
        }

        public static CompletionHandler ValueOf(int completionHandler)
        {
            switch (completionHandler)
            {
                case 1: return CompletionHandler.DefaultCompletionHandler;
                default: return CompletionHandler.None;
            }
        }
    }
}