namespace Assets._Scripts.Novel
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
         private const string LOOKS_SCARED = "Looks scared";
         private const string LOOKS_DEFEATED = "Looks defeated";
         private const string LOOKS_DISSATISFIED = "Looks dissatisfied";
         private const string LOOKS_REJECTING = "Looks rejecting";
         private const string LOOKS_AMAZED = "Looks amazed";
         private const string LOOKS_QUESTIONING = "Looks questioning";
         private const string LOOKS_CRITICAL = "Looks critical";
         private const string LOOKS_SMILING_BIG = "Looks smiling big";
         private const string LOOKS_LAUGHING = "Looks laughing";
         private const string LOOKS_SMILING = "Looks smiling";
         private const string LOOKS_NEUTRAL_RELAXED = "Looks neutral relaxed";
         private const string LOOKS_NEUTRAL = "Looks neutral";
         private const string LOOKS_PROUD = "Looks proud";
         
         private const string SPEAKS_SCARED = "Speaks scared";
         private const string SPEAKS_DEFEATED = "Speaks defeated";
         private const string SPEAKS_DISSATISFIED = "Speaks dissatisfied";
         private const string SPEAKS_REJECTING = "Speaks rejecting";
         private const string SPEAKS_AMAZED = "Speaks amazed";
         private const string SPEAKS_QUESTIONING = "Speaks questioning";
         private const string SPEAKS_CRITICAL = "Speaks critical";
         private const string SPEAKS_SMILING_BIG = "Speaks smiling big";
         private const string SPEAKS_LAUGHING = "Spricht lachend";
         private const string SPEAKS_SMILING = "Speaks smiling";
         private const string SPEAKS_NEUTRAL_RELAXED = "Speaks neutral relaxed";
         private const string SPEAKS_NEUTRAL = "Speaks neutral";
         private const string SPEAKS_PROUD = "Speaks proud";

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
                 case LOOKS_SCARED: { return CharacterExpression.LooksScared; }
                 case LOOKS_DEFEATED: { return CharacterExpression.LooksDefeated; }
                 case LOOKS_DISSATISFIED: { return CharacterExpression.LooksDissatisfied; }
                 case LOOKS_REJECTING: { return CharacterExpression.LooksRejecting; }
                 case LOOKS_AMAZED: { return CharacterExpression.LooksAmazed; }
                 case LOOKS_QUESTIONING: { return CharacterExpression.LooksQuestioning; }
                 case LOOKS_CRITICAL: { return CharacterExpression.LooksCritical; }
                 case LOOKS_SMILING_BIG: { return CharacterExpression.LooksSmilingBig; }
                 case LOOKS_LAUGHING: { return CharacterExpression.LooksLaughing; }
                 case LOOKS_SMILING: { return CharacterExpression.LooksSmiling; }
                 case LOOKS_NEUTRAL_RELAXED: { return CharacterExpression.LooksNeutralRelaxed; }
                 case LOOKS_NEUTRAL: { return CharacterExpression.LooksNeutral; }
                 case LOOKS_PROUD: { return CharacterExpression.LooksProud; }
                 case SPEAKS_SCARED: { return CharacterExpression.SpeaksScared; }
                 case SPEAKS_DEFEATED: { return CharacterExpression.SpeaksDefeated; }
                 case SPEAKS_DISSATISFIED: { return CharacterExpression.SpeaksDissatisfied; }
                 case SPEAKS_REJECTING: { return CharacterExpression.SpeaksRejecting; }
                 case SPEAKS_AMAZED: { return CharacterExpression.SpeaksAmazed; }
                 case SPEAKS_QUESTIONING: { return CharacterExpression.SpeaksQuestioning; }
                 case SPEAKS_CRITICAL: { return CharacterExpression.SpeaksCritical; }
                 case SPEAKS_SMILING_BIG: { return CharacterExpression.SpeaksSmilingBig; }
                 case SPEAKS_LAUGHING: { return CharacterExpression.SpeaksLaughing; }
                 case SPEAKS_SMILING: { return CharacterExpression.SpeaksSmiling; }
                 case SPEAKS_NEUTRAL_RELAXED: { return CharacterExpression.SpeaksNeutralRelaxed; }
                 case SPEAKS_NEUTRAL: { return CharacterExpression.SpeaksNeutral; }
                 case SPEAKS_PROUD: { return CharacterExpression.SpeaksProud; }
                 
                 default: { return CharacterExpression.None; }
             }
         }
     }
}