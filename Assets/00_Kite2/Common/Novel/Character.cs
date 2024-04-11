public enum Character
{
    NONE,
    PLAYER,
    INTRO,
    OUTRO,
    INFO,
    MAYER
}
public class CharacterTypeHelper
{
    public const string PLAYER = "Lea";
    public const string INTRO = "Intro";
    public const string OUTRO = "Outro";
    public const string INFO = "Info";
    public const string MAYER = "Mayer";

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
            default: return Character.NONE;
        }
    }
}