public enum ExpressionType
{
    NONE,
    SMILING
}

public class ExpressionTypeHelper
{
    public static int ToInt(ExpressionType expressionType)
    {
        switch (expressionType)
        {
            case ExpressionType.NONE: { return 0; }
            case ExpressionType.SMILING: { return 1; }
            default: { return -1; }
        }
    }

    public static ExpressionType ValueOf(int i)
    {
        switch (i)
        {
            case 0: { return ExpressionType.NONE; }
            case 1: { return ExpressionType.SMILING; }
            default: { return ExpressionType.NONE; }
        }
    }
}
