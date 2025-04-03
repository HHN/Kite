namespace Assets._Scripts.Novel
{
    public enum KiteAnimation
    {
        None,
        WaterPouring
    }

    public class KiteAnimationHelper
    {
        public static int ToInt(KiteAnimation sound)
        {
            return sound switch
            {
                KiteAnimation.None => 0,
                KiteAnimation.WaterPouring => 1,
                _ => -1
            };
        }

        public static KiteAnimation ValueOf(int i)
        {
            return i switch
            {
                0 => KiteAnimation.None,
                1 => KiteAnimation.WaterPouring,
                _ => KiteAnimation.None
            };
        }
    }
}