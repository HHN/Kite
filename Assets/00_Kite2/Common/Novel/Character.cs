public enum Character
{
    NONE,
    PLAYER,
    INTRO,
    OUTRO,
    INFO,
    MAYER,
    REPORTERIN,
    VERMIETER,
    VATER,
    MUTTER
}
public class CharacterTypeHelper
{
    public const string PLAYER = "Lea";
    public const string INTRO = "Intro";
    public const string OUTRO = "Outro";
    public const string INFO = "Info";
    public const string MAYER = "Mayer";
    public const string REPORTERIN = "Reporterin";
    public const string VERMIETER = "Vermieter";
    public const string VATER = "Vater";
    public const string MUTTER = "Mutter";

    public static int ToInt(Character character)
    {
        switch (character)
        {
            case Character.NONE: { return 0; }
            case Character.PLAYER: { return 1; }
            case Character.INTRO: { return 2; }
            case Character.OUTRO: { return 3; }
            case Character.INFO: { return 4; }
            case Character.MAYER: { return 5; }
            case Character.REPORTERIN: { return 6; }
            case Character.VERMIETER: { return 7; }
            case Character.VATER: { return 8; }
            case Character.MUTTER: { return 9; }
            default: { return 0; }
        }
    }

    public static Character ValueOf(int i)
    {
        switch (i)
        {
            case 0: { return Character.NONE; }
            case 1:  { return Character.PLAYER; }
            case 2: { return Character.INTRO; }
            case 3: { return Character.OUTRO; }
            case 4: { return Character.INFO; }
            case 5: { return Character.MAYER; }
            case 6: { return Character.REPORTERIN; }
            case 7: { return Character.VERMIETER; }
            case 8: { return Character.VATER; }
            case 9: { return Character.MUTTER; }
            default: return Character.NONE;
        }
    }

    public static string GetNameOfCharacter(Character character)
    {
        switch (character)
        {
            case Character.NONE: { return null; }
            case Character.PLAYER: { return PLAYER; }
            case Character.INTRO: { return INTRO; }
            case Character.OUTRO: { return OUTRO; }
            case Character.INFO: { return INFO; }
            case Character.MAYER: { return MAYER; }
            case Character.REPORTERIN: { return REPORTERIN; }
            case Character.VERMIETER: { return VERMIETER; }
            case Character.VATER: { return VATER; }
            case Character.MUTTER: { return MUTTER; }
            default: { return null; }
        }
    }

    public static string GetNameOfCharacter(int i)
    {
        switch (i)
        {
            case 0: { return null; }
            case 1: { return PLAYER; }
            case 2: { return INTRO; }
            case 3: { return OUTRO; }
            case 4: { return INFO; }
            case 5: { return MAYER; }
            case 6: { return REPORTERIN; }
            case 7: { return VERMIETER; }
            case 8: { return VATER; }
            case 9: { return MUTTER; }
            default: return null;
        }
    }

    public static Character ValueOf(string name)
    {
        switch (name)
        {
            case null: { return Character.NONE; }
            case PLAYER: { return Character.PLAYER; }
            case INTRO: { return Character.INTRO; }
            case OUTRO: { return Character.OUTRO; }
            case INFO: { return Character.INFO; }
            case MAYER: { return Character.MAYER; }
            case REPORTERIN: { return Character.REPORTERIN; }
            case VERMIETER: { return Character.VERMIETER; }
            case VATER: { return Character.VATER; }
            case MUTTER: { return Character.MUTTER; }
            default: return Character.NONE;
        }
    }
}