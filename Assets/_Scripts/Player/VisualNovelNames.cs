namespace Assets._Scripts.Player
{
    public enum VisualNovelNames
    {
        None,
        BankKreditNovel,
        InvestorNovel,
        ElternNovel,
        NotariatNovel,
        PresseNovel,
        VermieterNovel,
        HonorarNovel,
        EinstiegsNovel,
        VertriebNovel,
    }

    public class VisualNovelNamesHelper
    {
        public static VisualNovelNames ValueOf(int value)
        {
            return value switch
            {
                2 => VisualNovelNames.ElternNovel,
                3 => VisualNovelNames.PresseNovel,
                4 => VisualNovelNames.NotariatNovel,
                6 => VisualNovelNames.VermieterNovel,
                9 => VisualNovelNames.InvestorNovel,
                10 => VisualNovelNames.BankKreditNovel,
                11 => VisualNovelNames.HonorarNovel,
                13 => VisualNovelNames.EinstiegsNovel,
                14 => VisualNovelNames.VertriebNovel,
                _ => VisualNovelNames.None
            };
        }

        public static string GetName(long value)
        {
            return value switch
            {
                2 => "Eltern",
                3 => "Presse",
                4 => "Notarin",
                6 => "Vermieter",
                9 => "Investor",
                10 => "Bankkredit",
                11 => "Honorar",
                13 => "Einstieg",
                14 => "Vertrieb",
                _ => ""
            };
        }

        public static int ToInt(VisualNovelNames value)
        {
            return value switch
            {
                VisualNovelNames.ElternNovel => 2,
                VisualNovelNames.PresseNovel => 3,
                VisualNovelNames.NotariatNovel => 4,
                VisualNovelNames.VermieterNovel => 6,
                VisualNovelNames.InvestorNovel => 9,
                VisualNovelNames.BankKreditNovel => 10,
                VisualNovelNames.HonorarNovel => 11,
                VisualNovelNames.EinstiegsNovel => 13,
                VisualNovelNames.VertriebNovel => 14,
                _ => 0
            };
        }
    }
}