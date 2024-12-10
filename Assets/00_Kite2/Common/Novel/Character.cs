public enum Character
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
    BEKANNTER,
    NOTARIN,
    SACHBEARBEITER
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
    private const string BEKANNTER = "Bekannter";
    private const string NOTARIN = "Notarin";
    private const string SACHBEARBEITER = "Sachbearbeiter";

    public static int ToInt(Character character)
    {
        switch (character)
        {
            case Character.NONE:
            {
                return 0;
            }
            case Character.PLAYER:
            {
                return 1;
            }
            case Character.INTRO:
            {
                return 2;
            }
            case Character.OUTRO:
            {
                return 3;
            }
            case Character.INFO:
            {
                return 4;
            }
            case Character.REPORTERIN:
            {
                return 5;
            }
            case Character.VERMIETER:
            {
                return 6;
            }
            case Character.VATER:
            {
                return 7;
            }
            case Character.MUTTER:
            {
                return 8;
            }
            case Character.BEKANNTER:
            {
                return 9;
            }
            case Character.NOTARIN:
            {
                return 10;
            }
            case Character.SACHBEARBEITER:
            {
                return 11;
            }
            default:
            {
                return 0;
            }
        }
    }

    public static Character ValueOf(int i)
    {
        switch (i)
        {
            case 0:
            {
                return Character.NONE;
            }
            case 1:
            {
                return Character.PLAYER;
            }
            case 2:
            {
                return Character.INTRO;
            }
            case 3:
            {
                return Character.OUTRO;
            }
            case 4:
            {
                return Character.INFO;
            }
            case 5:
            {
                return Character.REPORTERIN;
            }
            case 6:
            {
                return Character.VERMIETER;
            }
            case 7:
            {
                return Character.VATER;
            }
            case 8:
            {
                return Character.MUTTER;
            }
            case 9:
            {
                return Character.BEKANNTER;
            }
            case 10:
            {
                return Character.NOTARIN;
            }
            case 11:
            {
                return Character.SACHBEARBEITER;
            }
            default: return Character.NONE;
        }
    }

    public static string GetNameOfCharacter(Character character)
    {
        switch (character)
        {
            case Character.NONE:
            {
                return null;
            }
            case Character.PLAYER:
            {
                return PLAYER;
            }
            case Character.INTRO:
            {
                return INTRO;
            }
            case Character.OUTRO:
            {
                return OUTRO;
            }
            case Character.INFO:
            {
                return INFO;
            }
            case Character.REPORTERIN:
            {
                return REPORTERIN;
            }
            case Character.VERMIETER:
            {
                return VERMIETER;
            }
            case Character.VATER:
            {
                return VATER;
            }
            case Character.MUTTER:
            {
                return MUTTER;
            }
            case Character.BEKANNTER:
            {
                return BEKANNTER;
            }
            case Character.NOTARIN:
            {
                return NOTARIN;
            }
            case Character.SACHBEARBEITER:
            {
                return SACHBEARBEITER;
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
                return BEKANNTER;
            }
            case 10:
            {
                return NOTARIN;
            }
            case 11:
            {
                return SACHBEARBEITER;
            }
            default: return null;
        }
    }

    public static Character ValueOf(string name)
    {
        switch (name)
        {
            case null:
            {
                return Character.NONE;
            }
            case PLAYER:
            {
                return Character.PLAYER;
            }
            case INTRO:
            {
                return Character.INTRO;
            }
            case OUTRO:
            {
                return Character.OUTRO;
            }
            case INFO:
            {
                return Character.INFO;
            }
            case REPORTERIN:
            {
                return Character.REPORTERIN;
            }
            case VERMIETER:
            {
                return Character.VERMIETER;
            }
            case VATER:
            {
                return Character.VATER;
            }
            case MUTTER:
            {
                return Character.MUTTER;
            }
            case BEKANNTER:
            {
                return Character.BEKANNTER;
            }
            case NOTARIN:
            {
                return Character.NOTARIN;
            }
            case SACHBEARBEITER:
            {
                return Character.SACHBEARBEITER;
            }
            default: return Character.NONE;
        }
    }
}