public enum ExpressionType
{
    NONE,
    RELAXED,
    ASTONISHED,
    REFUSING,
    SMILING,
    FRIENDLY,
    LAUGHING,
    CRITICAL,
    DECISION_NO,
    HAPPY,
    PROUD,
    SCARED,
    QUESTIONING,
    DEFEATED
}

public class ExpressionTypeHelper
{
    public static int ToInt(ExpressionType expressionType)
    {
        switch (expressionType)
        {
            case ExpressionType.NONE: { return 0; }
            case ExpressionType.RELAXED: { return 1; }
            case ExpressionType.ASTONISHED: { return 2; }
            case ExpressionType.REFUSING: { return 3; }
            case ExpressionType.SMILING: { return 4; }
            case ExpressionType.FRIENDLY: { return 5; }
            case ExpressionType.LAUGHING: { return 6; }
            case ExpressionType.CRITICAL: { return 7; }
            case ExpressionType.DECISION_NO: { return 8; }
            case ExpressionType.HAPPY: { return 9; }
            case ExpressionType.PROUD: { return 10; }
            case ExpressionType.SCARED: { return 11; }
            case ExpressionType.QUESTIONING: { return 12; }
            case ExpressionType.DEFEATED: { return 13; }
            default: { return 0; }
        }
    }

    public static ExpressionType ValueOf(int i)
    {
        switch (i)
        {
            case 0: { return ExpressionType.NONE; }
            case 1: { return ExpressionType.RELAXED; }
            case 2: { return ExpressionType.ASTONISHED; }
            case 3: { return ExpressionType.REFUSING; }
            case 4: { return ExpressionType.SMILING; }
            case 5: { return ExpressionType.FRIENDLY; }
            case 6: { return ExpressionType.LAUGHING; }
            case 7: { return ExpressionType.CRITICAL; }
            case 8: { return ExpressionType.DECISION_NO; }
            case 9: { return ExpressionType.HAPPY; }
            case 10: { return ExpressionType.PROUD; }
            case 11: { return ExpressionType.SCARED; }
            case 12: { return ExpressionType.QUESTIONING; }
            case 13: { return ExpressionType.DEFEATED; }
            default: { return ExpressionType.NONE; }
        }
    }
}
