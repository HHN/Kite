namespace Assets._Scripts.Common.Novel
{
    public enum Location
    {
        NONE,
        OFFICE
    }

    public class LocationHelper
    {
        private const string OFFICE = "Office";

        public static int ToInt(Location location)
        {
            switch (location)
            {
                case Location.OFFICE: return 1;
                default: return -1;
            }
        }

        public static Location ValueOf(int location)
        {
            switch (location)
            {
                case 1: return Location.OFFICE;
                default: return Location.NONE;
            }
        }

        public static Location ValueOf(string location)
        {
            switch (location)
            {
                case OFFICE: return Location.OFFICE;
                default: return Location.NONE;
            }
        }
    }
}