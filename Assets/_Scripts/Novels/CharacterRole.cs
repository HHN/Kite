namespace Assets._Scripts.Novels
{
    public enum CharacterRole
    {
        NONE,
        PLAYER,
        INTRO,
        OUTRO,
        INFO,
        REPORTERIN,
        VERMIETER,
        VATER,
        MUTTER,
        INVESTOR,
        NOTARIN,
        SACHBEARBEITER,
        KUNDIN
    }

    public class CharacterTypeHelper
    {
        private const string PLAYER = "Spielerin";
        private const string INTRO = "Intro";
        private const string OUTRO = "Outro";
        private const string INFO = "Info";
        private const string REPORTERIN = "Journalistin";
        private const string VERMIETER = "Vermieter";
        private const string VATER = "Vater";
        private const string MUTTER = "Mutter";
        private const string INVESTOR = "Investor";
        private const string NOTARIN = "Notarin";
        private const string SACHBEARBEITER = "Sachbearbeiter";
        private const string KUNDIN = "Kundin";

        public static int ToInt(CharacterRole characterRole)
        {
            switch (characterRole)
            {
                case CharacterRole.NONE:
                {
                    return 0;
                }
                case CharacterRole.PLAYER:
                {
                    return 1;
                }
                case CharacterRole.INTRO:
                {
                    return 2;
                }
                case CharacterRole.OUTRO:
                {
                    return 3;
                }
                case CharacterRole.INFO:
                {
                    return 4;
                }
                case CharacterRole.REPORTERIN:
                {
                    return 5;
                }
                case CharacterRole.VERMIETER:
                {
                    return 6;
                }
                case CharacterRole.VATER:
                {
                    return 7;
                }
                case CharacterRole.MUTTER:
                {
                    return 8;
                }
                case CharacterRole.INVESTOR:
                {
                    return 9;
                }
                case CharacterRole.NOTARIN:
                {
                    return 10;
                }
                case CharacterRole.SACHBEARBEITER:
                {
                    return 11;
                }
                case CharacterRole.KUNDIN:
                {
                    return 12;
                }
                default:
                {
                    return 0;
                }
            }
        }

        public static CharacterRole ValueOf(int i)
        {
            switch (i)
            {
                case 0:
                {
                    return CharacterRole.NONE;
                }
                case 1:
                {
                    return CharacterRole.PLAYER;
                }
                case 2:
                {
                    return CharacterRole.INTRO;
                }
                case 3:
                {
                    return CharacterRole.OUTRO;
                }
                case 4:
                {
                    return CharacterRole.INFO;
                }
                case 5:
                {
                    return CharacterRole.REPORTERIN;
                }
                case 6:
                {
                    return CharacterRole.VERMIETER;
                }
                case 7:
                {
                    return CharacterRole.VATER;
                }
                case 8:
                {
                    return CharacterRole.MUTTER;
                }
                case 9:
                {
                    return CharacterRole.INVESTOR;
                }
                case 10:
                {
                    return CharacterRole.NOTARIN;
                }
                case 11:
                {
                    return CharacterRole.SACHBEARBEITER;
                }
                case 12:
                {
                    return CharacterRole.KUNDIN;
                }
                default: return CharacterRole.NONE;
            }
        }

        public static string GetNameOfCharacter(CharacterRole characterRole)
        {
            switch (characterRole)
            {
                case CharacterRole.NONE:
                {
                    return null;
                }
                case CharacterRole.PLAYER:
                {
                    return PLAYER;
                }
                case CharacterRole.INTRO:
                {
                    return INTRO;
                }
                case CharacterRole.OUTRO:
                {
                    return OUTRO;
                }
                case CharacterRole.INFO:
                {
                    return INFO;
                }
                case CharacterRole.REPORTERIN:
                {
                    return REPORTERIN;
                }
                case CharacterRole.VERMIETER:
                {
                    return VERMIETER;
                }
                case CharacterRole.VATER:
                {
                    return VATER;
                }
                case CharacterRole.MUTTER:
                {
                    return MUTTER;
                }
                case CharacterRole.INVESTOR:
                {
                    return INVESTOR;
                }
                case CharacterRole.NOTARIN:
                {
                    return NOTARIN;
                }
                case CharacterRole.SACHBEARBEITER:
                {
                    return SACHBEARBEITER;
                }
                case CharacterRole.KUNDIN:
                {
                    return KUNDIN;
                }
                default:
                {
                    return null;
                }
            }
        }

        public static string GetNameOfCharacter(int i)
        {
            switch (i)
            {
                case 0:
                {
                    return null;
                }
                case 1:
                {
                    return PLAYER;
                }
                case 2:
                {
                    return INTRO;
                }
                case 3:
                {
                    return OUTRO;
                }
                case 4:
                {
                    return INFO;
                }
                case 5:
                {
                    return REPORTERIN;
                }
                case 6:
                {
                    return VERMIETER;
                }
                case 7:
                {
                    return VATER;
                }
                case 8:
                {
                    return MUTTER;
                }
                case 9:
                {
                    return INVESTOR;
                }
                case 10:
                {
                    return NOTARIN;
                }
                case 11:
                {
                    return SACHBEARBEITER;
                }
                case 12:
                {
                    return KUNDIN;
                }
                default: return null;
            }
        }

        public static CharacterRole ValueOf(string name)
        {
            switch (name)
            {
                case null:
                {
                    return CharacterRole.NONE;
                }
                case PLAYER:
                {
                    return CharacterRole.PLAYER;
                }
                case INTRO:
                {
                    return CharacterRole.INTRO;
                }
                case OUTRO:
                {
                    return CharacterRole.OUTRO;
                }
                case INFO:
                {
                    return CharacterRole.INFO;
                }
                case REPORTERIN:
                {
                    return CharacterRole.REPORTERIN;
                }
                case VERMIETER:
                {
                    return CharacterRole.VERMIETER;
                }
                case VATER:
                {
                    return CharacterRole.VATER;
                }
                case MUTTER:
                {
                    return CharacterRole.MUTTER;
                }
                case INVESTOR:
                {
                    return CharacterRole.INVESTOR;
                }
                case NOTARIN:
                {
                    return CharacterRole.NOTARIN;
                }
                case SACHBEARBEITER:
                {
                    return CharacterRole.SACHBEARBEITER;
                }
                case KUNDIN:
                {
                    return CharacterRole.KUNDIN;
                }
                default: return CharacterRole.NONE;
            }
        }
    }
}