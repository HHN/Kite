namespace Assets._Scripts.Utilities
{
    public enum FaceExpressions
    {
        None,
        Frightened,
        Annoyed,
        Unsatisfied,
        Rejecting,
        Astonished,
        Questioning,
        Critical,
        SmilingBig,
        Laughing,
        Smiling,
        NeutralRelaxed,
        Neutral,
        Proud,
    }

    public static class FaceExpressionHelper
    {
        public static int ToInt(FaceExpressions expression)
        {
            return (int) expression;
        }
    }
}