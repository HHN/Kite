namespace _00_Kite2.Common.Novel
{
    public enum CharacterExpression
    {
         NONE,
         ERSCHROCKEN,
         GENERVT,
         UNZUFRIEDEN,
         ABLEHNEND,
         ERSTAUNT,
         FRAGEND,
         KRITISCH,
         LAECHELN_GROSS,
         LACHEND,
         LAECHELN,
         NEUTRAL_ENTSPANNT,
         NEUTRAL,
         STOLZ
         // RELAXED,
         // ASTONISHED,
         // REFUSING,
         // SMILING,
         // FRIENDLY,
         // LAUGHING,
         // CRITICAL,
         // DECISION_NO,
         // HAPPY,
         // PROUD,
         // SCARED,
         // QUESTIONING,
         // DEFEATED
     }
  
     public class CharacterExpressionHelper
     {
         private const string NONE = "None";
         private const string ERSCHROCKEN = "Erschrocken";
         private const string GENERVT = "Genervt";
         private const string UNZUFRIEDEN = "Unzufrieden";
         private const string ABLEHNEND = "Ablehnend";
         private const string ERSTAUNT = "Erstaunt";
         private const string FRAGEND = "Fragend";
         private const string KRITISCH = "Kritisch";
         private const string LAECHELN_GROSS = "Lacheln gross";
         private const string LACHEND = "Lachend";
         private const string LAECHELN = "Laecheln";
         private const string NEUTRAL_ENTSPANNT = "Neutral entspannt";
         private const string NEUTRAL = "Neutral";
         private const string STOLZ = "Stolz";
         
         // private const string RELAXED = "Relaxed";
         // private const string ASTONISHED = "Astonished";
         // private const string REFUSING = "Refusing";
         // private const string SMILING = "Smiling";
         // private const string FRIENDLY = "Friendly";
         // private const string LAUGHING = "Laughing";
         // private const string CRITICAL = "Critical";
         // private const string DECISION_NO = "Decision no";
         // private const string HAPPY = "Happy";
         // private const string PROUD = "Proud";
         // private const string SCARED = "Scared";
         // private const string QUESTIONING = "Questioning";
         // private const string DEFEATED = "Defeated";

         public static int ToInt(CharacterExpression expressionType)
         {
             switch (expressionType)
             {
                 case CharacterExpression.NONE: { return 0; }
                 case CharacterExpression.ERSCHROCKEN: { return 1; }
                 case CharacterExpression.GENERVT: { return 2; }
                 case CharacterExpression.UNZUFRIEDEN: { return 3; }
                 case CharacterExpression.ABLEHNEND: { return 4; }
                 case CharacterExpression.ERSTAUNT: { return 5; }
                 case CharacterExpression.FRAGEND: { return 6; }
                 case CharacterExpression.KRITISCH: { return 7; }
                 case CharacterExpression.LAECHELN_GROSS: { return 8; }
                 case CharacterExpression.LACHEND: { return 9; }
                 case CharacterExpression.LAECHELN: { return 10; }
                 case CharacterExpression.NEUTRAL_ENTSPANNT: { return 11; }
                 case CharacterExpression.NEUTRAL: { return 12; }
                 case CharacterExpression.STOLZ: { return 13; }
                 
                 // case CharacterExpression.RELAXED: { return 1; }
                 // case CharacterExpression.ASTONISHED: { return 2; }
                 // case CharacterExpression.REFUSING: { return 3; }
                 // case CharacterExpression.SMILING: { return 4; }
                 // case CharacterExpression.FRIENDLY: { return 5; }
                 // case CharacterExpression.LAUGHING: { return 6; }
                 // case CharacterExpression.CRITICAL: { return 7; }
                 // case CharacterExpression.DECISION_NO: { return 8; }
                 // case CharacterExpression.HAPPY: { return 9; }
                 // case CharacterExpression.PROUD: { return 10; }
                 // case CharacterExpression.SCARED: { return 11; }
                 // case CharacterExpression.QUESTIONING: { return 12; }
                 // case CharacterExpression.DEFEATED: { return 13; }
                 default: { return 0; }
             }
         }

         public static CharacterExpression ValueOf(int i)
         {
             switch (i)
             {
                 case 0: { return CharacterExpression.NONE; }
                 case 1: { return CharacterExpression.ERSCHROCKEN; }
                 case 2: { return CharacterExpression.GENERVT; }
                 case 3: { return CharacterExpression.UNZUFRIEDEN; }
                 case 4: { return CharacterExpression.ABLEHNEND; }
                 case 5: { return CharacterExpression.ERSTAUNT; }
                 case 6: { return CharacterExpression.FRAGEND; }
                 case 7: { return CharacterExpression.KRITISCH; }
                 case 8: { return CharacterExpression.LAECHELN_GROSS; }
                 case 9: { return CharacterExpression.LACHEND; }
                 case 10: { return CharacterExpression.LAECHELN; }
                 case 11: { return CharacterExpression.NEUTRAL_ENTSPANNT; }
                 case 12: { return CharacterExpression.NEUTRAL; }
                 case 13: { return CharacterExpression.STOLZ; }
                 
                 // case 1: { return CharacterExpression.RELAXED; }
                 // case 2: { return CharacterExpression.ASTONISHED; }
                 // case 3: { return CharacterExpression.REFUSING; }
                 // case 4: { return CharacterExpression.SMILING; }
                 // case 5: { return CharacterExpression.FRIENDLY; }
                 // case 6: { return CharacterExpression.LAUGHING; }
                 // case 7: { return CharacterExpression.CRITICAL; }
                 // case 8: { return CharacterExpression.DECISION_NO; }
                 // case 9: { return CharacterExpression.HAPPY; }
                 // case 10: { return CharacterExpression.PROUD; }
                 // case 11: { return CharacterExpression.SCARED; }
                 // case 12: { return CharacterExpression.QUESTIONING; }
                 // case 13: { return CharacterExpression.DEFEATED; }
                 default: { return CharacterExpression.NONE; }
             }
         }

         public static CharacterExpression ValueOf(string i)
         {
             switch (i)
             {
                 case NONE: { return CharacterExpression.NONE; }
                 case ERSCHROCKEN: { return CharacterExpression.ERSCHROCKEN; }
                 case GENERVT: { return CharacterExpression.GENERVT; }
                 case UNZUFRIEDEN: { return CharacterExpression.UNZUFRIEDEN; }
                 case ABLEHNEND: { return CharacterExpression.ABLEHNEND; }
                 case ERSTAUNT: { return CharacterExpression.ERSTAUNT; }
                 case FRAGEND: { return CharacterExpression.FRAGEND; }
                 case KRITISCH: { return CharacterExpression.KRITISCH; }
                 case LAECHELN_GROSS: { return CharacterExpression.LAECHELN_GROSS; }
                 case LACHEND: { return CharacterExpression.LACHEND; }
                 case LAECHELN: { return CharacterExpression.LAECHELN; }
                 case NEUTRAL_ENTSPANNT: { return CharacterExpression.NEUTRAL_ENTSPANNT; }
                 case NEUTRAL: { return CharacterExpression.NEUTRAL; }
                 case STOLZ: { return CharacterExpression.STOLZ; }
                 
                 // case RELAXED: { return CharacterExpression.RELAXED; }
                 // case ASTONISHED: { return CharacterExpression.ASTONISHED; }
                 // case REFUSING: { return CharacterExpression.REFUSING; }
                 // case SMILING: { return CharacterExpression.SMILING; }
                 // case FRIENDLY: { return CharacterExpression.FRIENDLY; }
                 // case LAUGHING: { return CharacterExpression.LAUGHING; }
                 // case CRITICAL: { return CharacterExpression.CRITICAL; }
                 // case DECISION_NO: { return CharacterExpression.DECISION_NO; }
                 // case HAPPY: { return CharacterExpression.HAPPY; }
                 // case PROUD: { return CharacterExpression.PROUD; }
                 // case SCARED: { return CharacterExpression.SCARED; }
                 // case QUESTIONING: { return CharacterExpression.QUESTIONING; }
                 // case DEFEATED: { return CharacterExpression.DEFEATED; }
                 default: { return CharacterExpression.NONE; }
             }
         }
     }
}