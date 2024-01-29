public enum SoundsEnum
{
    NONE,
    WATER_POURING,
    LEAVE_SCENE
}

public class SoundEnumHelper
{
    public static int ToInt(SoundsEnum sound)
    {
        switch (sound)
        {
            case SoundsEnum.NONE: { return 0; }
            case SoundsEnum.WATER_POURING: { return 1; }
            case SoundsEnum.LEAVE_SCENE: { return 2; }
            default: { return -1; }
        }
    }

    public static SoundsEnum ValueOf(int i)
    {
        switch (i)
        {
            case 0: { return SoundsEnum.NONE; }
            case 1: { return SoundsEnum.WATER_POURING; }
            case 2: { return SoundsEnum.LEAVE_SCENE; }
            default: { return SoundsEnum.NONE; }
        }
    }
}
