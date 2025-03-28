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
        EinstiegsNovel
    }

    public class VisualNovelNamesHelper
    {
        public static VisualNovelNames ValueOf(int value)
        {
            switch (value)
            {
                case 2:
                {
                    return VisualNovelNames.ElternNovel;
                }
                case 3:
                {
                    return VisualNovelNames.PresseNovel;
                }
                case 4:
                {
                    return VisualNovelNames.NotariatNovel;
                }
                case 6:
                {
                    return VisualNovelNames.VermieterNovel;
                }
                case 9:
                {
                    return VisualNovelNames.InvestorNovel;
                }
                case 10:
                {
                    return VisualNovelNames.BankKreditNovel;
                }
                case 11:
                {
                    return VisualNovelNames.HonorarNovel;
                }
                case 13:
                {
                    return VisualNovelNames.EinstiegsNovel;
                }
                default:
                {
                    return VisualNovelNames.None;
                }
            }
        }

        public static string GetName(long value)
        {
            switch (value)
            {
                case 2:
                {
                    return "Eltern";
                }
                case 3:
                {
                    return "Presse";
                }
                case 4:
                {
                    return "Notarin";
                }
                case 6:
                {
                    return "Vermieter";
                }
                case 9:
                {
                    return "Investor";
                }
                case 10:
                {
                    return "Bankkredit";
                }
                case 11:
                {
                    return "Honorar";
                }
                case 13:
                {
                    return "Einstieg";
                }
                default:
                {
                    return "";
                }
            }
        }

        public static int ToInt(VisualNovelNames value)
        {
            switch (value)
            {
                case VisualNovelNames.ElternNovel:
                {
                    return 2;
                }
                case VisualNovelNames.PresseNovel:
                {
                    return 3;
                }
                case VisualNovelNames.NotariatNovel:
                {
                    return 4;
                }
                case VisualNovelNames.VermieterNovel:
                {
                    return 6;
                }
                case VisualNovelNames.InvestorNovel:
                {
                    return 9;
                }
                case VisualNovelNames.BankKreditNovel:
                {
                    return 10;
                }
                case VisualNovelNames.HonorarNovel:
                {
                    return 11;
                }
                case VisualNovelNames.EinstiegsNovel:
                {
                    return 13;
                }
                default:
                {
                    return 0;
                }
            }
        }
    }
}