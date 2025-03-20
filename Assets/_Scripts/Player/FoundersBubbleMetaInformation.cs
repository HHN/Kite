using UnityEngine;

namespace Assets._Scripts.Player
{
    public class FoundersBubbleMetaInformation : MonoBehaviour
    {
        public static int numberOfNovelsToDisplay = 8;

        private static readonly Color Green = new(37 / 255f, 101 / 255f, 14 / 255f);
        private static readonly Color Turquoise = new(17 / 255f, 69 / 255f, 74 / 255f);
        private static readonly Color Turquoise2 = new(15 / 255f, 69 / 255f, 60 / 255f);
        private static readonly Color DarkBlue = new(12 / 255f, 26 / 255f, 46 / 255f);
        private static readonly Color Violet = new(83 / 255f, 32 / 255f, 83 / 255f);
        private static readonly Color Brown = new(46 / 255f, 37 / 255f, 12 / 255f);
        private static readonly Color GreenBrown = new (48 / 255f, 72 / 255f, 15 / 255f);
        private static readonly Color Default = new(0 / 255f, 0 / 255f, 0 / 255f);

        public static bool IsHighInGui(VisualNovelNames value)
        {
            switch (value)
            {
                case VisualNovelNames.ElternNovel:
                {
                    return false;
                }
                case VisualNovelNames.PresseNovel:
                {
                    return false;
                }
                case VisualNovelNames.NotariatNovel:
                {
                    return true;
                }
                case VisualNovelNames.VermieterNovel:
                {
                    return true;
                }
                case VisualNovelNames.InvestorNovel:
                {
                    return false;
                }
                case VisualNovelNames.BankKreditNovel:
                {
                    return true;
                }
                case VisualNovelNames.HonorarNovel:
                {
                    return false;
                }
                case VisualNovelNames.EinstiegsNovel:
                {
                    return true;
                }
                default:
                {
                    return false;
                }
            }
        }

        internal static Color GetForegroundColorOfNovel(VisualNovelNames value)
        {
            switch (value)
            {
                case VisualNovelNames.ElternNovel:
                {
                    return Green;
                }
                case VisualNovelNames.PresseNovel:
                {
                    return Violet;
                }
                case VisualNovelNames.NotariatNovel:
                {
                    return Turquoise;
                }
                case VisualNovelNames.VermieterNovel:
                {
                    return DarkBlue;
                }
                case VisualNovelNames.InvestorNovel:
                {
                    return Green;
                }
                case VisualNovelNames.BankKreditNovel:
                {
                    return DarkBlue;
                }
                case VisualNovelNames.HonorarNovel:
                {
                    return Turquoise2;
                }
                case VisualNovelNames.EinstiegsNovel:
                {
                    return Violet;
                }
                default:
                {
                    return default;
                }
            }
        }

        internal static Color GetBackgroundColorOfNovel(VisualNovelNames value)
        {
            switch (value)
            {
                case VisualNovelNames.ElternNovel:
                {
                    return Green;
                }
                case VisualNovelNames.PresseNovel:
                {
                    return Violet;
                }
                case VisualNovelNames.NotariatNovel:
                {
                    return Turquoise;
                }
                case VisualNovelNames.VermieterNovel:
                {
                    return DarkBlue;
                }
                case VisualNovelNames.InvestorNovel:
                {
                    return GreenBrown;
                }
                case VisualNovelNames.BankKreditNovel:
                {
                    return DarkBlue;
                }
                case VisualNovelNames.HonorarNovel:
                {
                    return Turquoise2;
                }
                case VisualNovelNames.EinstiegsNovel:
                {
                    return Violet;
                }
                default:
                {
                    return Default;
                }
            }
        }

        internal static string GetDisplayNameOfNovelToPlay(VisualNovelNames value)
        {
            switch (value)
            {
                case VisualNovelNames.ElternNovel:
                {
                    return "Eltern";
                }
                case VisualNovelNames.PresseNovel:
                {
                    return "Presse";
                }
                case VisualNovelNames.NotariatNovel:
                {
                    return "Notarin";
                }
                case VisualNovelNames.VermieterNovel:
                {
                    return "Vermieter";
                }
                case VisualNovelNames.InvestorNovel:
                {
                    return "Investor";
                }
                case VisualNovelNames.BankKreditNovel:
                {
                    return "Bankkredit";
                }
                case VisualNovelNames.HonorarNovel:
                {
                    return "Honorar";
                }
                case VisualNovelNames.EinstiegsNovel:
                {
                    return "Einstieg";
                }
                default:
                {
                    return "";
                }
            }
        }

        internal static int GetIndexOfNovel(VisualNovelNames value)
        {
            switch (value)
            {
                case VisualNovelNames.ElternNovel:
                {
                    return 2;
                }
                case VisualNovelNames.PresseNovel:
                {
                    return 4;
                }
                case VisualNovelNames.NotariatNovel:
                {
                    return 3;
                }
                case VisualNovelNames.VermieterNovel:
                {
                    return 5;
                }
                case VisualNovelNames.InvestorNovel:
                {
                    return 6;
                }
                case VisualNovelNames.BankKreditNovel:
                {
                    return 1;
                }
                case VisualNovelNames.HonorarNovel:
                {
                    return 8;
                }
                case VisualNovelNames.EinstiegsNovel:
                {
                    return 7;
                }
                default:
                {
                    return -1;
                }
            }
        }
    }
}