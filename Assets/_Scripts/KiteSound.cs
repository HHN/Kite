namespace Assets._Scripts
{
    public enum KiteSound
    {
        None,
        WaterPouring,
        LeaveScene,
        TelephoneCall,
        PaperSound,
        ManLaughing
    }

    public static class KiteSoundHelper
    {
        public static int ToInt(KiteSound sound)
        {
            return sound switch
            {
                KiteSound.None => 0,
                KiteSound.WaterPouring => 1,
                KiteSound.LeaveScene => 2,
                KiteSound.TelephoneCall => 3,
                KiteSound.PaperSound => 4,
                KiteSound.ManLaughing => 5,
                _ => -1
            };
        }

        public static KiteSound ValueOf(int i)
        {
            return i switch
            {
                0 => KiteSound.None,
                1 => KiteSound.WaterPouring,
                2 => KiteSound.LeaveScene,
                3 => KiteSound.TelephoneCall,
                4 => KiteSound.PaperSound,
                5 => KiteSound.ManLaughing,
                _ => KiteSound.None
            };
        }
    }
}