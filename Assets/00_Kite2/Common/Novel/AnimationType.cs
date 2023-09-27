public enum AnimationType
{
    NONE,
    FLY_IN_FROM_ABOVE
}

public class AnimationTypeHelper
{
    public static int ToInt(AnimationType animationType)
    {
        switch (animationType)
        {
            case AnimationType.NONE: { return 0; }
            case AnimationType.FLY_IN_FROM_ABOVE: { return 1; }
            default: { return -1; }
        }
    }

    public static AnimationType ValueOf(int i)
    {
        switch (i)
        {
            case 0: { return AnimationType.NONE; }
            case 1: { return AnimationType.FLY_IN_FROM_ABOVE; }
            default: { return AnimationType.NONE; }
        }
    }
}