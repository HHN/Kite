namespace _00_Kite2.Common.Novel.Event_Animations
{
    public enum KiteAnimation
    {
        NONE,
        WATER_POURING
    }

    public class KiteAnimationHelper
    {
        public static int ToInt(KiteAnimation sound)
        {
            switch (sound)
            {
                case KiteAnimation.NONE: { return 0; }
                case KiteAnimation.WATER_POURING: { return 1; }
                default: { return -1; }
            }
        }

        public static KiteAnimation ValueOf(int i)
        {
            switch (i)
            {
                case 0: { return KiteAnimation.NONE; }
                case 1: { return KiteAnimation.WATER_POURING; }
                default: { return KiteAnimation.NONE; }
            }
        }
    }
}