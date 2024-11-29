namespace _00_Kite2.Audio_Resources.Resources
{
    public enum KiteSound
    {
        NONE,
        WATER_POURING,
        LEAVE_SCENE,
        TELEPHONE_CALL,
        PAPER_SOUND,
        MAN_LAUGHING
    }

    public class KiteSoundHelper
    {
        public static int ToInt(KiteSound sound)
        {
            switch (sound)
            {
                case KiteSound.NONE: { return 0; }
                case KiteSound.WATER_POURING: { return 1; }
                case KiteSound.LEAVE_SCENE: { return 2; }
                case KiteSound.TELEPHONE_CALL: { return 3; }
                case KiteSound.PAPER_SOUND: { return 4; }
                case KiteSound.MAN_LAUGHING: { return 5; }
                default: { return -1; }
            }
        }

        public static KiteSound ValueOf(int i)
        {
            switch (i)
            {
                case 0: { return KiteSound.NONE; }
                case 1: { return KiteSound.WATER_POURING; }
                case 2: { return KiteSound.LEAVE_SCENE; }
                case 3: { return KiteSound.TELEPHONE_CALL; }
                case 4: { return KiteSound.PAPER_SOUND; }
                case 5: { return KiteSound.MAN_LAUGHING; }
                default: { return KiteSound.NONE; }
            }
        }
    }
}