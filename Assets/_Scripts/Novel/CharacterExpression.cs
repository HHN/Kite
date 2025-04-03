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
             return expressionType switch
             {
                 CharacterExpression.None => 0,
                 CharacterExpression.SchautErschrocken => 1,
                 CharacterExpression.SchautGenervt => 2,
                 CharacterExpression.SchautUnzufrieden => 3,
                 CharacterExpression.SchautAblehnend => 4,
                 CharacterExpression.SchautErstaunt => 5,
                 CharacterExpression.SchautFragend => 6,
                 CharacterExpression.SchautKritisch => 7,
                 CharacterExpression.SchautLaechelnGross => 8,
                 CharacterExpression.SchautLachend => 9,
                 CharacterExpression.SchautLaecheln => 10,
                 CharacterExpression.SchautNeutralEntspannt => 11,
                 CharacterExpression.SchautNeutral => 12,
                 CharacterExpression.SchautStolz => 13,
                 CharacterExpression.SprichtErschrocken => 14,
                 CharacterExpression.SprichtGenervt => 15,
                 CharacterExpression.SprichtUnzufrieden => 16,
                 CharacterExpression.SprichtAblehnend => 17,
                 CharacterExpression.SprichtErstaunt => 18,
                 CharacterExpression.SprichtFragend => 19,
                 CharacterExpression.SprichtKritisch => 20,
                 CharacterExpression.SprichtLaechelnGross => 21,
                 CharacterExpression.SprichtLachend => 22,
                 CharacterExpression.SprichtLaecheln => 23,
                 CharacterExpression.SprichtNeutralEntspannt => 24,
                 CharacterExpression.SprichtNeutral => 25,
                 CharacterExpression.SprichtStolz => 26,
                 _ => 0
             };
         }

         public static CharacterExpression ValueOf(int i)
         {
             return i switch
             {
                 0 => CharacterExpression.None,
                 1 => CharacterExpression.SchautErschrocken,
                 2 => CharacterExpression.SchautGenervt,
                 3 => CharacterExpression.SchautUnzufrieden,
                 4 => CharacterExpression.SchautAblehnend,
                 5 => CharacterExpression.SchautErstaunt,
                 6 => CharacterExpression.SchautFragend,
                 7 => CharacterExpression.SchautKritisch,
                 8 => CharacterExpression.SchautLaechelnGross,
                 9 => CharacterExpression.SchautLachend,
                 10 => CharacterExpression.SchautLaecheln,
                 11 => CharacterExpression.SchautNeutralEntspannt,
                 12 => CharacterExpression.SchautNeutral,
                 13 => CharacterExpression.SchautStolz,
                 14 => CharacterExpression.SprichtErschrocken,
                 15 => CharacterExpression.SprichtGenervt,
                 16 => CharacterExpression.SprichtUnzufrieden,
                 17 => CharacterExpression.SprichtAblehnend,
                 18 => CharacterExpression.SprichtErstaunt,
                 19 => CharacterExpression.SprichtFragend,
                 20 => CharacterExpression.SprichtKritisch,
                 21 => CharacterExpression.SprichtLaechelnGross,
                 22 => CharacterExpression.SprichtLachend,
                 23 => CharacterExpression.SprichtLaecheln,
                 24 => CharacterExpression.SprichtNeutralEntspannt,
                 25 => CharacterExpression.SprichtNeutral,
                 26 => CharacterExpression.SprichtStolz,
                 _ => CharacterExpression.None
             };
         }

         public static CharacterExpression ValueOf(string i)
         {
             return i switch
             {
                 NONE => CharacterExpression.None,
                 SCHAUT_ERSCHROCKEN => CharacterExpression.SchautErschrocken,
                 SCHAUT_GENERVT => CharacterExpression.SchautGenervt,
                 SCHAUT_UNZUFRIEDEN => CharacterExpression.SchautUnzufrieden,
                 SCHAUT_ABLEHNEND => CharacterExpression.SchautAblehnend,
                 SCHAUT_ERSTAUNT => CharacterExpression.SchautErstaunt,
                 SCHAUT_FRAGEND => CharacterExpression.SchautFragend,
                 SCHAUT_KRITISCH => CharacterExpression.SchautKritisch,
                 SCHAUT_LAECHELN_GROSS => CharacterExpression.SchautLaechelnGross,
                 SCHAUT_LACHEND => CharacterExpression.SchautLachend,
                 SCHAUT_LAECHELN => CharacterExpression.SchautLaecheln,
                 SCHAUT_NEUTRAL_ENTSPANNT => CharacterExpression.SchautNeutralEntspannt,
                 SCHAUT_NEUTRAL => CharacterExpression.SchautNeutral,
                 SCHAUT_STOLZ => CharacterExpression.SchautStolz,
                 SPRICHT_ERSCHROCKEN => CharacterExpression.SprichtErschrocken,
                 SPRICHT_GENERVT => CharacterExpression.SprichtGenervt,
                 SPRICHT_UNZUFRIEDEN => CharacterExpression.SprichtUnzufrieden,
                 SPRICHT_ABLEHNEND => CharacterExpression.SprichtAblehnend,
                 SPRICHT_ERSTAUNT => CharacterExpression.SprichtErstaunt,
                 SPRICHT_FRAGEND => CharacterExpression.SprichtFragend,
                 SPRICHT_KRITISCH => CharacterExpression.SprichtKritisch,
                 SPRICHT_LAECHELN_GROSS => CharacterExpression.SprichtLaechelnGross,
                 SPRICHT_LACHEND => CharacterExpression.SprichtLachend,
                 SPRICHT_LAECHELN => CharacterExpression.SprichtLaecheln,
                 SPRICHT_NEUTRAL_ENTSPANNT => CharacterExpression.SprichtNeutralEntspannt,
                 SPRICHT_NEUTRAL => CharacterExpression.SprichtNeutral,
                 SPRICHT_STOLZ => CharacterExpression.SprichtStolz,
                 _ => CharacterExpression.None
             };
         }
     }
}