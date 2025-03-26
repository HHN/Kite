namespace Assets._Scripts.Novel
{
    public enum CharacterExpression
    {
         None,
         SchautErschrocken,
         SchautGenervt,
         SchautUnzufrieden,
         SchautAblehnend,
         SchautErstaunt,
         SchautFragend,
         SchautKritisch,
         SchautLaechelnGross,
         SchautLachend,
         SchautLaecheln,
         SchautNeutralEntspannt,
         SchautNeutral,
         SchautStolz,
         SprichtErschrocken,
         SprichtGenervt,
         SprichtUnzufrieden,
         SprichtAblehnend,
         SprichtErstaunt,
         SprichtFragend,
         SprichtKritisch,
         SprichtLaechelnGross,
         SprichtLachend,
         SprichtLaecheln,
         SprichtNeutralEntspannt,
         SprichtNeutral,
         SprichtStolz
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
                 case CharacterExpression.None: { return 0; }
                 case CharacterExpression.SchautErschrocken: { return 1; }
                 case CharacterExpression.SchautGenervt: { return 2; }
                 case CharacterExpression.SchautUnzufrieden: { return 3; }
                 case CharacterExpression.SchautAblehnend: { return 4; }
                 case CharacterExpression.SchautErstaunt: { return 5; }
                 case CharacterExpression.SchautFragend: { return 6; }
                 case CharacterExpression.SchautKritisch: { return 7; }
                 case CharacterExpression.SchautLaechelnGross: { return 8; }
                 case CharacterExpression.SchautLachend: { return 9; }
                 case CharacterExpression.SchautLaecheln: { return 10; }
                 case CharacterExpression.SchautNeutralEntspannt: { return 11; }
                 case CharacterExpression.SchautNeutral: { return 12; }
                 case CharacterExpression.SchautStolz: { return 13; }
                 case CharacterExpression.SprichtErschrocken: { return 14; }
                 case CharacterExpression.SprichtGenervt: { return 15; }
                 case CharacterExpression.SprichtUnzufrieden: { return 16; }
                 case CharacterExpression.SprichtAblehnend: { return 17; }
                 case CharacterExpression.SprichtErstaunt: { return 18; }
                 case CharacterExpression.SprichtFragend: { return 19; }
                 case CharacterExpression.SprichtKritisch: { return 20; }
                 case CharacterExpression.SprichtLaechelnGross: { return 21; }
                 case CharacterExpression.SprichtLachend: { return 22; }
                 case CharacterExpression.SprichtLaecheln: { return 23; }
                 case CharacterExpression.SprichtNeutralEntspannt: { return 24; }
                 case CharacterExpression.SprichtNeutral: { return 25; }
                 case CharacterExpression.SprichtStolz: { return 26; }
                 
                 default: { return 0; }
             }
         }

         public static CharacterExpression ValueOf(int i)
         {
             switch (i)
             {
                 case 0: { return CharacterExpression.None; }
                 case 1: { return CharacterExpression.SchautErschrocken; }
                 case 2: { return CharacterExpression.SchautGenervt; }
                 case 3: { return CharacterExpression.SchautUnzufrieden; }
                 case 4: { return CharacterExpression.SchautAblehnend; }
                 case 5: { return CharacterExpression.SchautErstaunt; }
                 case 6: { return CharacterExpression.SchautFragend; }
                 case 7: { return CharacterExpression.SchautKritisch; }
                 case 8: { return CharacterExpression.SchautLaechelnGross; }
                 case 9: { return CharacterExpression.SchautLachend; }
                 case 10: { return CharacterExpression.SchautLaecheln; }
                 case 11: { return CharacterExpression.SchautNeutralEntspannt; }
                 case 12: { return CharacterExpression.SchautNeutral; }
                 case 13: { return CharacterExpression.SchautStolz; }
                 case 14: { return CharacterExpression.SprichtErschrocken; }
                 case 15: { return CharacterExpression.SprichtGenervt; }
                 case 16: { return CharacterExpression.SprichtUnzufrieden; }
                 case 17: { return CharacterExpression.SprichtAblehnend; }
                 case 18: { return CharacterExpression.SprichtErstaunt; }
                 case 19: { return CharacterExpression.SprichtFragend; }
                 case 20: { return CharacterExpression.SprichtKritisch; }
                 case 21: { return CharacterExpression.SprichtLaechelnGross; }
                 case 22: { return CharacterExpression.SprichtLachend; }
                 case 23: { return CharacterExpression.SprichtLaecheln; }
                 case 24: { return CharacterExpression.SprichtNeutralEntspannt; }
                 case 25: { return CharacterExpression.SprichtNeutral; }
                 case 26: { return CharacterExpression.SprichtStolz; }
                 
                 default: { return CharacterExpression.None; }
             }
         }

         public static CharacterExpression ValueOf(string i)
         {
             switch (i)
             {
                 case NONE: { return CharacterExpression.None; }
                 case SCHAUT_ERSCHROCKEN: { return CharacterExpression.SchautErschrocken; }
                 case SCHAUT_GENERVT: { return CharacterExpression.SchautGenervt; }
                 case SCHAUT_UNZUFRIEDEN: { return CharacterExpression.SchautUnzufrieden; }
                 case SCHAUT_ABLEHNEND: { return CharacterExpression.SchautAblehnend; }
                 case SCHAUT_ERSTAUNT: { return CharacterExpression.SchautErstaunt; }
                 case SCHAUT_FRAGEND: { return CharacterExpression.SchautFragend; }
                 case SCHAUT_KRITISCH: { return CharacterExpression.SchautKritisch; }
                 case SCHAUT_LAECHELN_GROSS: { return CharacterExpression.SchautLaechelnGross; }
                 case SCHAUT_LACHEND: { return CharacterExpression.SchautLachend; }
                 case SCHAUT_LAECHELN: { return CharacterExpression.SchautLaecheln; }
                 case SCHAUT_NEUTRAL_ENTSPANNT: { return CharacterExpression.SchautNeutralEntspannt; }
                 case SCHAUT_NEUTRAL: { return CharacterExpression.SchautNeutral; }
                 case SCHAUT_STOLZ: { return CharacterExpression.SchautStolz; }
                 case SPRICHT_ERSCHROCKEN: { return CharacterExpression.SprichtErschrocken; }
                 case SPRICHT_GENERVT: { return CharacterExpression.SprichtGenervt; }
                 case SPRICHT_UNZUFRIEDEN: { return CharacterExpression.SprichtUnzufrieden; }
                 case SPRICHT_ABLEHNEND: { return CharacterExpression.SprichtAblehnend; }
                 case SPRICHT_ERSTAUNT: { return CharacterExpression.SprichtErstaunt; }
                 case SPRICHT_FRAGEND: { return CharacterExpression.SprichtFragend; }
                 case SPRICHT_KRITISCH: { return CharacterExpression.SprichtKritisch; }
                 case SPRICHT_LAECHELN_GROSS: { return CharacterExpression.SprichtLaechelnGross; }
                 case SPRICHT_LACHEND: { return CharacterExpression.SprichtLachend; }
                 case SPRICHT_LAECHELN: { return CharacterExpression.SprichtLaecheln; }
                 case SPRICHT_NEUTRAL_ENTSPANNT: { return CharacterExpression.SprichtNeutralEntspannt; }
                 case SPRICHT_NEUTRAL: { return CharacterExpression.SprichtNeutral; }
                 case SPRICHT_STOLZ: { return CharacterExpression.SprichtStolz; }
                 
                 default: { return CharacterExpression.None; }
             }
         }
     }
}