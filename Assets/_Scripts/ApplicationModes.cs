namespace Assets._Scripts
{
    public enum ApplicationModes
    {
        None,
        OfflineMode,
        OnlineMode
    }

    public static class ApplicationModeHelper
    {
        public static int ToInt(ApplicationModes mode)
        {
            return mode switch
            {
                ApplicationModes.None => 0,
                ApplicationModes.OfflineMode => 1,
                ApplicationModes.OnlineMode => 2,
                _ => -1
            };
        }

        public static ApplicationModes ValueOf(int mode)
        {
            return mode switch
            {
                0 => ApplicationModes.None,
                1 => ApplicationModes.OfflineMode,
                2 => ApplicationModes.OnlineMode,
                _ => ApplicationModes.None
            };
        }
    }
}