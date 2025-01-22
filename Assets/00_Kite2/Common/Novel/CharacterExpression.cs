namespace _00_Kite2.Common.Novel
{
    public enum CharacterExpression
    {
         NONE,
         SCHAUT_ERSCHROCKEN,
         SCHAUT_GENERVT,
         SCHAUT_UNZUFRIEDEN,
         SCHAUT_ABLEHNEND,
         SCHAUT_ERSTAUNT,
         SCHAUT_FRAGEND,
         SCHAUT_KRITISCH,
         SCHAUT_LAECHELN_GROSS,
         SCHAUT_LACHEND,
         SCHAUT_LAECHELN,
         SCHAUT_NEUTRAL_ENTSPANNT,
         SCHAUT_NEUTRAL,
         SCHAUT_STOLZ,
         SPRICHT_ERSCHROCKEN,
         SPRICHT_GENERVT,
         SPRICHT_UNZUFRIEDEN,
         SPRICHT_ABLEHNEND,
         SPRICHT_ERSTAUNT,
         SPRICHT_FRAGEND,
         SPRICHT_KRITISCH,
         SPRICHT_LAECHELN_GROSS,
         SPRICHT_LACHEND,
         SPRICHT_LAECHELN,
         SPRICHT_NEUTRAL_ENTSPANNT,
         SPRICHT_NEUTRAL,
         SPRICHT_STOLZ
     }
  
     public class CharacterExpressionHelper
     {
         private const string NONE = "None";
         private const string SCHAUT_ERSCHROCKEN = "Schaut erschrocken";
         private const string SCHAUT_GENERVT = "Schaut genervt";
         private const string SCHAUT_UNZUFRIEDEN = "Schaut unzufrieden";
         private const string SCHAUT_ABLEHNEND = "Schaut ablehnend";
         private const string SCHAUT_ERSTAUNT = "Schaut erstaunt";
         private const string SCHAUT_FRAGEND = "Schaut fragend";
         private const string SCHAUT_KRITISCH = "Schaut kritisch";
         private const string SCHAUT_LAECHELN_GROSS = "Schaut laecheln gross";
         private const string SCHAUT_LACHEND = "Schaut lachend";
         private const string SCHAUT_LAECHELN = "Schaut laecheln";
         private const string SCHAUT_NEUTRAL_ENTSPANNT = "Schaut neutral entspannt";
         private const string SCHAUT_NEUTRAL = "Schaut neutral";
         private const string SCHAUT_STOLZ = "Schaut stolz";
         
         private const string SPRICHT_ERSCHROCKEN = "Spricht erschrocken";
         private const string SPRICHT_GENERVT = "Spricht genervt";
         private const string SPRICHT_UNZUFRIEDEN = "Spricht unzufrieden";
         private const string SPRICHT_ABLEHNEND = "Spricht ablehnend";
         private const string SPRICHT_ERSTAUNT = "Spricht erstaunt";
         private const string SPRICHT_FRAGEND = "Spricht fragend";
         private const string SPRICHT_KRITISCH = "Spricht kritisch";
         private const string SPRICHT_LAECHELN_GROSS = "Spricht laecheln gross";
         private const string SPRICHT_LACHEND = "Spricht lachend";
         private const string SPRICHT_LAECHELN = "Spricht laecheln";
         private const string SPRICHT_NEUTRAL_ENTSPANNT = "Spricht neutral entspannt";
         private const string SPRICHT_NEUTRAL = "Spricht neutral";
         private const string SPRICHT_STOLZ = "Spricht stolz";

         public static int ToInt(CharacterExpression expressionType)
         {
             switch (expressionType)
             {
                 case CharacterExpression.NONE: { return 0; }
                 case CharacterExpression.SCHAUT_ERSCHROCKEN: { return 1; }
                 case CharacterExpression.SCHAUT_GENERVT: { return 2; }
                 case CharacterExpression.SCHAUT_UNZUFRIEDEN: { return 3; }
                 case CharacterExpression.SCHAUT_ABLEHNEND: { return 4; }
                 case CharacterExpression.SCHAUT_ERSTAUNT: { return 5; }
                 case CharacterExpression.SCHAUT_FRAGEND: { return 6; }
                 case CharacterExpression.SCHAUT_KRITISCH: { return 7; }
                 case CharacterExpression.SCHAUT_LAECHELN_GROSS: { return 8; }
                 case CharacterExpression.SCHAUT_LACHEND: { return 9; }
                 case CharacterExpression.SCHAUT_LAECHELN: { return 10; }
                 case CharacterExpression.SCHAUT_NEUTRAL_ENTSPANNT: { return 11; }
                 case CharacterExpression.SCHAUT_NEUTRAL: { return 12; }
                 case CharacterExpression.SCHAUT_STOLZ: { return 13; }
                 case CharacterExpression.SPRICHT_ERSCHROCKEN: { return 14; }
                 case CharacterExpression.SPRICHT_GENERVT: { return 15; }
                 case CharacterExpression.SPRICHT_UNZUFRIEDEN: { return 16; }
                 case CharacterExpression.SPRICHT_ABLEHNEND: { return 17; }
                 case CharacterExpression.SPRICHT_ERSTAUNT: { return 18; }
                 case CharacterExpression.SPRICHT_FRAGEND: { return 19; }
                 case CharacterExpression.SPRICHT_KRITISCH: { return 20; }
                 case CharacterExpression.SPRICHT_LAECHELN_GROSS: { return 21; }
                 case CharacterExpression.SPRICHT_LACHEND: { return 22; }
                 case CharacterExpression.SPRICHT_LAECHELN: { return 23; }
                 case CharacterExpression.SPRICHT_NEUTRAL_ENTSPANNT: { return 24; }
                 case CharacterExpression.SPRICHT_NEUTRAL: { return 25; }
                 case CharacterExpression.SPRICHT_STOLZ: { return 26; }
                 
                 default: { return 0; }
             }
         }

         public static CharacterExpression ValueOf(int i)
         {
             switch (i)
             {
                 case 0: { return CharacterExpression.NONE; }
                 case 1: { return CharacterExpression.SCHAUT_ERSCHROCKEN; }
                 case 2: { return CharacterExpression.SCHAUT_GENERVT; }
                 case 3: { return CharacterExpression.SCHAUT_UNZUFRIEDEN; }
                 case 4: { return CharacterExpression.SCHAUT_ABLEHNEND; }
                 case 5: { return CharacterExpression.SCHAUT_ERSTAUNT; }
                 case 6: { return CharacterExpression.SCHAUT_FRAGEND; }
                 case 7: { return CharacterExpression.SCHAUT_KRITISCH; }
                 case 8: { return CharacterExpression.SCHAUT_LAECHELN_GROSS; }
                 case 9: { return CharacterExpression.SCHAUT_LACHEND; }
                 case 10: { return CharacterExpression.SCHAUT_LAECHELN; }
                 case 11: { return CharacterExpression.SCHAUT_NEUTRAL_ENTSPANNT; }
                 case 12: { return CharacterExpression.SCHAUT_NEUTRAL; }
                 case 13: { return CharacterExpression.SCHAUT_STOLZ; }
                 case 14: { return CharacterExpression.SPRICHT_ERSCHROCKEN; }
                 case 15: { return CharacterExpression.SPRICHT_GENERVT; }
                 case 16: { return CharacterExpression.SPRICHT_UNZUFRIEDEN; }
                 case 17: { return CharacterExpression.SPRICHT_ABLEHNEND; }
                 case 18: { return CharacterExpression.SPRICHT_ERSTAUNT; }
                 case 19: { return CharacterExpression.SPRICHT_FRAGEND; }
                 case 20: { return CharacterExpression.SPRICHT_KRITISCH; }
                 case 21: { return CharacterExpression.SPRICHT_LAECHELN_GROSS; }
                 case 22: { return CharacterExpression.SPRICHT_LACHEND; }
                 case 23: { return CharacterExpression.SPRICHT_LAECHELN; }
                 case 24: { return CharacterExpression.SPRICHT_NEUTRAL_ENTSPANNT; }
                 case 25: { return CharacterExpression.SPRICHT_NEUTRAL; }
                 case 26: { return CharacterExpression.SPRICHT_STOLZ; }
                 
                 default: { return CharacterExpression.NONE; }
             }
         }

         public static CharacterExpression ValueOf(string i)
         {
             switch (i)
             {
                 case NONE: { return CharacterExpression.NONE; }
                 case SCHAUT_ERSCHROCKEN: { return CharacterExpression.SCHAUT_ERSCHROCKEN; }
                 case SCHAUT_GENERVT: { return CharacterExpression.SCHAUT_GENERVT; }
                 case SCHAUT_UNZUFRIEDEN: { return CharacterExpression.SCHAUT_UNZUFRIEDEN; }
                 case SCHAUT_ABLEHNEND: { return CharacterExpression.SCHAUT_ABLEHNEND; }
                 case SCHAUT_ERSTAUNT: { return CharacterExpression.SCHAUT_ERSTAUNT; }
                 case SCHAUT_FRAGEND: { return CharacterExpression.SCHAUT_FRAGEND; }
                 case SCHAUT_KRITISCH: { return CharacterExpression.SCHAUT_KRITISCH; }
                 case SCHAUT_LAECHELN_GROSS: { return CharacterExpression.SCHAUT_LAECHELN_GROSS; }
                 case SCHAUT_LACHEND: { return CharacterExpression.SCHAUT_LACHEND; }
                 case SCHAUT_LAECHELN: { return CharacterExpression.SCHAUT_LAECHELN; }
                 case SCHAUT_NEUTRAL_ENTSPANNT: { return CharacterExpression.SCHAUT_NEUTRAL_ENTSPANNT; }
                 case SCHAUT_NEUTRAL: { return CharacterExpression.SCHAUT_NEUTRAL; }
                 case SCHAUT_STOLZ: { return CharacterExpression.SCHAUT_STOLZ; }
                 case SPRICHT_ERSCHROCKEN: { return CharacterExpression.SPRICHT_ERSCHROCKEN; }
                 case SPRICHT_GENERVT: { return CharacterExpression.SPRICHT_GENERVT; }
                 case SPRICHT_UNZUFRIEDEN: { return CharacterExpression.SPRICHT_UNZUFRIEDEN; }
                 case SPRICHT_ABLEHNEND: { return CharacterExpression.SPRICHT_ABLEHNEND; }
                 case SPRICHT_ERSTAUNT: { return CharacterExpression.SPRICHT_ERSTAUNT; }
                 case SPRICHT_FRAGEND: { return CharacterExpression.SPRICHT_FRAGEND; }
                 case SPRICHT_KRITISCH: { return CharacterExpression.SPRICHT_KRITISCH; }
                 case SPRICHT_LAECHELN_GROSS: { return CharacterExpression.SPRICHT_LAECHELN_GROSS; }
                 case SPRICHT_LACHEND: { return CharacterExpression.SPRICHT_LACHEND; }
                 case SPRICHT_LAECHELN: { return CharacterExpression.SPRICHT_LAECHELN; }
                 case SPRICHT_NEUTRAL_ENTSPANNT: { return CharacterExpression.SPRICHT_NEUTRAL_ENTSPANNT; }
                 case SPRICHT_NEUTRAL: { return CharacterExpression.SPRICHT_NEUTRAL; }
                 case SPRICHT_STOLZ: { return CharacterExpression.SPRICHT_STOLZ; }
                 
                 default: { return CharacterExpression.NONE; }
             }
         }
     }
}