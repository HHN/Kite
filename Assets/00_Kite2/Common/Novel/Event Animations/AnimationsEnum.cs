public enum AnimationsEnum
{
    NONE,
    WATER_POURING
}

public class AnimationsEnumHelper
{
    public static int ToInt(AnimationsEnum sound)
    {
        switch (sound)
        {
            case AnimationsEnum.NONE: { return 0; }
            case AnimationsEnum.WATER_POURING: { return 1; }
            default: { return -1; }
        }
    }

    public static AnimationsEnum ValueOf(int i)
    {
        switch (i)
        {
            case 0: { return AnimationsEnum.NONE; }
            case 1: { return AnimationsEnum.WATER_POURING; }
            default: { return AnimationsEnum.NONE; }
        }
    }
}
