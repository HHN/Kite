using _00_Kite2.Player;

namespace _00_Kite2.Common.Managers
{
    public class GptCompletionHandlerManager
    {
        private static GptCompletionHandlerManager _instance;

        private GptCompletionHandlerManager()
        {
        }

        public static GptCompletionHandlerManager Instance()
        {
            if (_instance == null)
            {
                _instance = new GptCompletionHandlerManager();
            }

            return _instance;
        }

        public IGptCompletionHandler GetCompletionHandlerById(int id)
        {
            switch (id)
            {
                case 1:
                {
                    return new DefaultGptCompletionHandler();
                }

                default:
                {
                    return new DefaultGptCompletionHandler();
                }
            }
        }
    }
}