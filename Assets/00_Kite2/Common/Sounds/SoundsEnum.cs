public enum SoundsEnum
{
    NONE,
    WATER_POURING,
    LEAVE_SCENE,
    TELEPHONE_CALL
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
            case SoundsEnum.TELEPHONE_CALL: { return 3; }
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
            case 3: { return SoundsEnum.TELEPHONE_CALL; }
            default: { return SoundsEnum.NONE; }
        }
    }
}
