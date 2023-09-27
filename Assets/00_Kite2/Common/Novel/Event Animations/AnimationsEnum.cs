public enum AnimationsEnum
{
    NONE,
    WATER_POURING
}

public class AnimationsEnumHelper
{
    public static int ToInt(SoundsEnum sound)
    {
        switch (sound)
        {
            case SoundsEnum.NONE: { return 0; }
            case SoundsEnum.WATER_POURING: { return 1; }
            default: { return -1; }
        }
    }

    public static SoundsEnum ValueOf(int i)
    {
        switch (i)
        {
            case 0: { return SoundsEnum.NONE; }
            case 1: { return SoundsEnum.WATER_POURING; }
            default: { return SoundsEnum.NONE; }
        }
    }
}
