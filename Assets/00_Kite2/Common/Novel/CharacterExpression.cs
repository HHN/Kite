namespace _00_Kite2.Common.Novel
{
    public enum CharacterExpression
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
 
    public class CharacterExpressionHelper
    {
        private const string NONE = "None";
        private const string RELAXED = "Relaxed";
        private const string ASTONISHED = "Astonished";
        private const string REFUSING = "Refusing";
        private const string SMILING = "Smiling";
        private const string FRIENDLY = "Friendly";
        private const string LAUGHING = "Laughing";
        private const string CRITICAL = "Critical";
        private const string DECISION_NO = "Decision no";
        private const string HAPPY = "Happy";
        private const string PROUD = "Proud";
        private const string SCARED = "Scared";
        private const string QUESTIONING = "Questioning";
        private const string DEFEATED = "Defeated";

        public static int ToInt(CharacterExpression expressionType)
        {
            switch (expressionType)
            {
                case CharacterExpression.NONE: { return 0; }
                case CharacterExpression.RELAXED: { return 1; }
                case CharacterExpression.ASTONISHED: { return 2; }
                case CharacterExpression.REFUSING: { return 3; }
                case CharacterExpression.SMILING: { return 4; }
                case CharacterExpression.FRIENDLY: { return 5; }
                case CharacterExpression.LAUGHING: { return 6; }
                case CharacterExpression.CRITICAL: { return 7; }
                case CharacterExpression.DECISION_NO: { return 8; }
                case CharacterExpression.HAPPY: { return 9; }
                case CharacterExpression.PROUD: { return 10; }
                case CharacterExpression.SCARED: { return 11; }
                case CharacterExpression.QUESTIONING: { return 12; }
                case CharacterExpression.DEFEATED: { return 13; }
                default: { return 0; }
            }
        }

        public static CharacterExpression ValueOf(int i)
        {
            switch (i)
            {
                case 0: { return CharacterExpression.NONE; }
                case 1: { return CharacterExpression.RELAXED; }
                case 2: { return CharacterExpression.ASTONISHED; }
                case 3: { return CharacterExpression.REFUSING; }
                case 4: { return CharacterExpression.SMILING; }
                case 5: { return CharacterExpression.FRIENDLY; }
                case 6: { return CharacterExpression.LAUGHING; }
                case 7: { return CharacterExpression.CRITICAL; }
                case 8: { return CharacterExpression.DECISION_NO; }
                case 9: { return CharacterExpression.HAPPY; }
                case 10: { return CharacterExpression.PROUD; }
                case 11: { return CharacterExpression.SCARED; }
                case 12: { return CharacterExpression.QUESTIONING; }
                case 13: { return CharacterExpression.DEFEATED; }
                default: { return CharacterExpression.NONE; }
            }
        }

        public static CharacterExpression ValueOf(string i)
        {
            switch (i)
            {
                case NONE: { return CharacterExpression.NONE; }
                case RELAXED: { return CharacterExpression.RELAXED; }
                case ASTONISHED: { return CharacterExpression.ASTONISHED; }
                case REFUSING: { return CharacterExpression.REFUSING; }
                case SMILING: { return CharacterExpression.SMILING; }
                case FRIENDLY: { return CharacterExpression.FRIENDLY; }
                case LAUGHING: { return CharacterExpression.LAUGHING; }
                case CRITICAL: { return CharacterExpression.CRITICAL; }
                case DECISION_NO: { return CharacterExpression.DECISION_NO; }
                case HAPPY: { return CharacterExpression.HAPPY; }
                case PROUD: { return CharacterExpression.PROUD; }
                case SCARED: { return CharacterExpression.SCARED; }
                case QUESTIONING: { return CharacterExpression.QUESTIONING; }
                case DEFEATED: { return CharacterExpression.DEFEATED; }
                default: { return CharacterExpression.NONE; }
            }
        }
    }
}