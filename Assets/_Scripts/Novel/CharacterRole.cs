namespace Assets._Scripts.Novel
{
    public enum CharacterRole
    {
        None,
        Player,
        Intro,
        Outro,
        Info,
        Reporterin,
        Vermieter,
        Vater,
        Mutter,
        Investor,
        Notarin,
        Sachbearbeiter,
        Kundin
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
            return characterRole switch
            {
                CharacterRole.None => 0,
                CharacterRole.Player => 1,
                CharacterRole.Intro => 2,
                CharacterRole.Outro => 3,
                CharacterRole.Info => 4,
                CharacterRole.Reporterin => 5,
                CharacterRole.Vermieter => 6,
                CharacterRole.Vater => 7,
                CharacterRole.Mutter => 8,
                CharacterRole.Investor => 9,
                CharacterRole.Notarin => 10,
                CharacterRole.Sachbearbeiter => 11,
                CharacterRole.Kundin => 12,
                _ => 0
            };
        }

        public static CharacterRole ValueOf(int i)
        {
            return i switch
            {
                0 => CharacterRole.None,
                1 => CharacterRole.Player,
                2 => CharacterRole.Intro,
                3 => CharacterRole.Outro,
                4 => CharacterRole.Info,
                5 => CharacterRole.Reporterin,
                6 => CharacterRole.Vermieter,
                7 => CharacterRole.Vater,
                8 => CharacterRole.Mutter,
                9 => CharacterRole.Investor,
                10 => CharacterRole.Notarin,
                11 => CharacterRole.Sachbearbeiter,
                12 => CharacterRole.Kundin,
                _ => CharacterRole.None
            };
        }

        public static string GetNameOfCharacter(CharacterRole characterRole)
        {
            return characterRole switch
            {
                CharacterRole.None => null,
                CharacterRole.Player => PLAYER,
                CharacterRole.Intro => INTRO,
                CharacterRole.Outro => OUTRO,
                CharacterRole.Info => INFO,
                CharacterRole.Reporterin => REPORTERIN,
                CharacterRole.Vermieter => VERMIETER,
                CharacterRole.Vater => VATER,
                CharacterRole.Mutter => MUTTER,
                CharacterRole.Investor => INVESTOR,
                CharacterRole.Notarin => NOTARIN,
                CharacterRole.Sachbearbeiter => SACHBEARBEITER,
                CharacterRole.Kundin => KUNDIN,
                _ => null
            };
        }

        public static string GetNameOfCharacter(int i)
        {
            return i switch
            {
                0 => null,
                1 => PLAYER,
                2 => INTRO,
                3 => OUTRO,
                4 => INFO,
                5 => REPORTERIN,
                6 => VERMIETER,
                7 => VATER,
                8 => MUTTER,
                9 => INVESTOR,
                10 => NOTARIN,
                11 => SACHBEARBEITER,
                12 => KUNDIN,
                _ => null
            };
        }

        public static CharacterRole ValueOf(string name)
        {
            return name switch
            {
                null => CharacterRole.None,
                PLAYER => CharacterRole.Player,
                INTRO => CharacterRole.Intro,
                OUTRO => CharacterRole.Outro,
                INFO => CharacterRole.Info,
                REPORTERIN => CharacterRole.Reporterin,
                VERMIETER => CharacterRole.Vermieter,
                VATER => CharacterRole.Vater,
                MUTTER => CharacterRole.Mutter,
                INVESTOR => CharacterRole.Investor,
                NOTARIN => CharacterRole.Notarin,
                SACHBEARBEITER => CharacterRole.Sachbearbeiter,
                KUNDIN => CharacterRole.Kundin,
                _ => CharacterRole.None
            };
        }
    }
}