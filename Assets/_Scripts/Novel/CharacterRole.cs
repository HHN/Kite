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
        private const string Player = "Spielerin";
        private const string Intro = "Intro";
        private const string Outro = "Outro";
        private const string Info = "Info";
        private const string Reporterin = "Journalistin";
        private const string Vermieter = "Vermieter";
        private const string Vater = "Vater";
        private const string Mutter = "Mutter";
        private const string Investor = "Investor";
        private const string Notarin = "Notarin";
        private const string Sachbearbeiter = "Sachbearbeiter";
        private const string Kundin = "Kundin";

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
                CharacterRole.Player => Player,
                CharacterRole.Intro => Intro,
                CharacterRole.Outro => Outro,
                CharacterRole.Info => Info,
                CharacterRole.Reporterin => Reporterin,
                CharacterRole.Vermieter => Vermieter,
                CharacterRole.Vater => Vater,
                CharacterRole.Mutter => Mutter,
                CharacterRole.Investor => Investor,
                CharacterRole.Notarin => Notarin,
                CharacterRole.Sachbearbeiter => Sachbearbeiter,
                CharacterRole.Kundin => Kundin,
                _ => null
            };
        }

        public static string GetNameOfCharacter(int i)
        {
            return i switch
            {
                0 => null,
                1 => Player,
                2 => Intro,
                3 => Outro,
                4 => Info,
                5 => Reporterin,
                6 => Vermieter,
                7 => Vater,
                8 => Mutter,
                9 => Investor,
                10 => Notarin,
                11 => Sachbearbeiter,
                12 => Kundin,
                _ => null
            };
        }

        public static CharacterRole ValueOf(string name)
        {
            return name switch
            {
                null => CharacterRole.None,
                Player => CharacterRole.Player,
                Intro => CharacterRole.Intro,
                Outro => CharacterRole.Outro,
                Info => CharacterRole.Info,
                Reporterin => CharacterRole.Reporterin,
                Vermieter => CharacterRole.Vermieter,
                Vater => CharacterRole.Vater,
                Mutter => CharacterRole.Mutter,
                Investor => CharacterRole.Investor,
                Notarin => CharacterRole.Notarin,
                Sachbearbeiter => CharacterRole.Sachbearbeiter,
                Kundin => CharacterRole.Kundin,
                _ => CharacterRole.None
            };
        }
    }
}