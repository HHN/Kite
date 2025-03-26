namespace Assets._Scripts.Novels
{
    public enum CharacterExpression
    {
         None,
         LooksScared,
         LooksDefeated,
         LooksDissatisfied,
         LooksRejecting,
         LooksAmazed,
         LooksQuestioning,
         LooksCritical,
         LooksSmilingBig,
         LooksLaughing,
         LooksSmiling,
         LooksNeutralRelaxed,
         LooksNeutral,
         LooksProud,
         SpeaksScared,
         SpeaksDefeated,
         SpeaksDissatisfied,
         SpeaksRejecting,
         SpeaksAmazed,
         SpeaksQuestioning,
         SpeaksCritical,
         SpeaksSmilingBig,
         SpeaksLaughing,
         SpeaksSmiling,
         SpeaksNeutralRelaxed,
         SpeaksNeutral,
         SpeaksProud
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
                 case CharacterExpression.LooksScared: { return 1; }
                 case CharacterExpression.LooksDefeated: { return 2; }
                 case CharacterExpression.LooksDissatisfied: { return 3; }
                 case CharacterExpression.LooksRejecting: { return 4; }
                 case CharacterExpression.LooksAmazed: { return 5; }
                 case CharacterExpression.LooksQuestioning: { return 6; }
                 case CharacterExpression.LooksCritical: { return 7; }
                 case CharacterExpression.LooksSmilingBig: { return 8; }
                 case CharacterExpression.LooksLaughing: { return 9; }
                 case CharacterExpression.LooksSmiling: { return 10; }
                 case CharacterExpression.LooksNeutralRelaxed: { return 11; }
                 case CharacterExpression.LooksNeutral: { return 12; }
                 case CharacterExpression.LooksProud: { return 13; }
                 case CharacterExpression.SpeaksScared: { return 14; }
                 case CharacterExpression.SpeaksDefeated: { return 15; }
                 case CharacterExpression.SpeaksDissatisfied: { return 16; }
                 case CharacterExpression.SpeaksRejecting: { return 17; }
                 case CharacterExpression.SpeaksAmazed: { return 18; }
                 case CharacterExpression.SpeaksQuestioning: { return 19; }
                 case CharacterExpression.SpeaksCritical: { return 20; }
                 case CharacterExpression.SpeaksSmilingBig: { return 21; }
                 case CharacterExpression.SpeaksLaughing: { return 22; }
                 case CharacterExpression.SpeaksSmiling: { return 23; }
                 case CharacterExpression.SpeaksNeutralRelaxed: { return 24; }
                 case CharacterExpression.SpeaksNeutral: { return 25; }
                 case CharacterExpression.SpeaksProud: { return 26; }
                 
                 default: { return 0; }
             }
         }

         public static CharacterExpression ValueOf(int i)
         {
             switch (i)
             {
                 case 0: { return CharacterExpression.None; }
                 case 1: { return CharacterExpression.LooksScared; }
                 case 2: { return CharacterExpression.LooksDefeated; }
                 case 3: { return CharacterExpression.LooksDissatisfied; }
                 case 4: { return CharacterExpression.LooksRejecting; }
                 case 5: { return CharacterExpression.LooksAmazed; }
                 case 6: { return CharacterExpression.LooksQuestioning; }
                 case 7: { return CharacterExpression.LooksCritical; }
                 case 8: { return CharacterExpression.LooksSmilingBig; }
                 case 9: { return CharacterExpression.LooksLaughing; }
                 case 10: { return CharacterExpression.LooksSmiling; }
                 case 11: { return CharacterExpression.LooksNeutralRelaxed; }
                 case 12: { return CharacterExpression.LooksNeutral; }
                 case 13: { return CharacterExpression.LooksProud; }
                 case 14: { return CharacterExpression.SpeaksScared; }
                 case 15: { return CharacterExpression.SpeaksDefeated; }
                 case 16: { return CharacterExpression.SpeaksDissatisfied; }
                 case 17: { return CharacterExpression.SpeaksRejecting; }
                 case 18: { return CharacterExpression.SpeaksAmazed; }
                 case 19: { return CharacterExpression.SpeaksQuestioning; }
                 case 20: { return CharacterExpression.SpeaksCritical; }
                 case 21: { return CharacterExpression.SpeaksSmilingBig; }
                 case 22: { return CharacterExpression.SpeaksLaughing; }
                 case 23: { return CharacterExpression.SpeaksSmiling; }
                 case 24: { return CharacterExpression.SpeaksNeutralRelaxed; }
                 case 25: { return CharacterExpression.SpeaksNeutral; }
                 case 26: { return CharacterExpression.SpeaksProud; }
                 
                 default: { return CharacterExpression.None; }
             }
         }

         public static CharacterExpression ValueOf(string i)
         {
             switch (i)
             {
                 case NONE: { return CharacterExpression.None; }
                 case SCHAUT_ERSCHROCKEN: { return CharacterExpression.LooksScared; }
                 case SCHAUT_GENERVT: { return CharacterExpression.LooksDefeated; }
                 case SCHAUT_UNZUFRIEDEN: { return CharacterExpression.LooksDissatisfied; }
                 case SCHAUT_ABLEHNEND: { return CharacterExpression.LooksRejecting; }
                 case SCHAUT_ERSTAUNT: { return CharacterExpression.LooksAmazed; }
                 case SCHAUT_FRAGEND: { return CharacterExpression.LooksQuestioning; }
                 case SCHAUT_KRITISCH: { return CharacterExpression.LooksCritical; }
                 case SCHAUT_LAECHELN_GROSS: { return CharacterExpression.LooksSmilingBig; }
                 case SCHAUT_LACHEND: { return CharacterExpression.LooksLaughing; }
                 case SCHAUT_LAECHELN: { return CharacterExpression.LooksSmiling; }
                 case SCHAUT_NEUTRAL_ENTSPANNT: { return CharacterExpression.LooksNeutralRelaxed; }
                 case SCHAUT_NEUTRAL: { return CharacterExpression.LooksNeutral; }
                 case SCHAUT_STOLZ: { return CharacterExpression.LooksProud; }
                 case SPRICHT_ERSCHROCKEN: { return CharacterExpression.SpeaksScared; }
                 case SPRICHT_GENERVT: { return CharacterExpression.SpeaksDefeated; }
                 case SPRICHT_UNZUFRIEDEN: { return CharacterExpression.SpeaksDissatisfied; }
                 case SPRICHT_ABLEHNEND: { return CharacterExpression.SpeaksRejecting; }
                 case SPRICHT_ERSTAUNT: { return CharacterExpression.SpeaksAmazed; }
                 case SPRICHT_FRAGEND: { return CharacterExpression.SpeaksQuestioning; }
                 case SPRICHT_KRITISCH: { return CharacterExpression.SpeaksCritical; }
                 case SPRICHT_LAECHELN_GROSS: { return CharacterExpression.SpeaksSmilingBig; }
                 case SPRICHT_LACHEND: { return CharacterExpression.SpeaksLaughing; }
                 case SPRICHT_LAECHELN: { return CharacterExpression.SpeaksSmiling; }
                 case SPRICHT_NEUTRAL_ENTSPANNT: { return CharacterExpression.SpeaksNeutralRelaxed; }
                 case SPRICHT_NEUTRAL: { return CharacterExpression.SpeaksNeutral; }
                 case SPRICHT_STOLZ: { return CharacterExpression.SpeaksProud; }
                 
                 default: { return CharacterExpression.None; }
             }
         }
     }
}