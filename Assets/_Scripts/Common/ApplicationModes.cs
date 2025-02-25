namespace Assets._Scripts.Common
{
    public enum ApplicationModes
    {
        NONE,
        OFFLINE_MODE,
        ONLINE_MODE
    }

    public class ApplicationModeHelper
    {
        public static int ToInt(ApplicationModes mode)
        {
            switch (mode)
            {
                case ApplicationModes.NONE: return 0;
                case ApplicationModes.OFFLINE_MODE: return 1;
                case ApplicationModes.ONLINE_MODE: return 2;
                default: return -1;
            }
        }

        public static ApplicationModes ValueOf(int mode)
        {
            switch (mode)
            {
                case 0: return ApplicationModes.NONE;
                case 1: return ApplicationModes.OFFLINE_MODE;
                case 2: return ApplicationModes.ONLINE_MODE;
                default: return ApplicationModes.NONE;
            }
        }
    }
}