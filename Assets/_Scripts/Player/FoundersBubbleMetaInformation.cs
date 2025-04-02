using UnityEngine;

namespace Assets._Scripts.Player
{
    public class FoundersBubbleMetaInformation : MonoBehaviour
    {
        public static int NumberOfNovelsToDisplay = 8;

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
                // case VisualNovelNames.BANK_KONTO_NOVEL:
                // {
                //     return false;
                // }
                case VisualNovelNames.VermieterNovel:
                {
                    return true;
                }
                // case VisualNovelNames.FOERDERANTRAG_NOVEL:
                // {
                //     return true;
                // }
                // case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                // {
                //     return true;
                // }
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
                    return false;
                }
                // case VisualNovelNames.LEBENSPARTNER_NOVEL:
                // {
                //     return true;
                // }
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
                    return new Color(53 / 255f, 146 / 255f, 20 / 255f);
                }
                case VisualNovelNames.PresseNovel:
                {
                    return new Color(120 / 255f, 45 / 255f, 119 / 255f);
                }
                case VisualNovelNames.NotariatNovel:
                {
                    return new Color(27 / 255f, 108 / 255f, 115 / 255f);
                }
                // case VisualNovelNames.BANK_KONTO_NOVEL:
                // {
                //     return new Color(53 / 255f, 146 / 255f, 20 / 255f);
                // }
                case VisualNovelNames.VermieterNovel:
                {
                    return new Color(23 / 255f, 49 / 255f, 86 / 255f);
                }
                // case VisualNovelNames.FOERDERANTRAG_NOVEL:
                // {
                //     return new Color(27 / 255f, 108 / 255f, 115 / 255f);
                // }
                // case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                // {
                //     return new Color(86 / 255f, 70 / 255f, 23 / 255f);
                // }
                case VisualNovelNames.InvestorNovel:
                {
                    return new Color(76 / 255f, 114 / 255f, 42 / 255f);
                }
                case VisualNovelNames.BankKreditNovel:
                {
                    return new Color(23 / 255f, 49 / 255f, 86 / 255f);
                }
                case VisualNovelNames.HonorarNovel:
                {
                    return new Color(24 / 255f, 111 / 255f, 96 / 255f);
                }
                case VisualNovelNames.EinstiegsNovel:
                {
                    return new Color(120 / 255f, 45 / 255f, 119 / 255f);
                }
                // case VisualNovelNames.LEBENSPARTNER_NOVEL:
                // {
                //     return new Color(23 / 255f, 49 / 255f, 86 / 255f);
                // }
                default:
                {
                    return new Color(0 / 255f, 0 / 255f, 0 / 255f);
                }
            }
        }

        internal static Color GetBackgroundColorOfNovel(VisualNovelNames value)
        {
            switch (value)
            {
                case VisualNovelNames.ElternNovel:
                {
                    return new Color(37 / 255f, 101 / 255f, 14 / 255f);
                }
                case VisualNovelNames.PresseNovel:
                {
                    return new Color(83 / 255f, 32 / 255f, 83 / 255f);
                }
                case VisualNovelNames.NotariatNovel:
                {
                    return new Color(17 / 255f, 69 / 255f, 74 / 255f);
                }
                // case VisualNovelNames.BANK_KONTO_NOVEL:
                // {
                //     return new Color(37 / 255f, 101 / 255f, 14 / 255f);
                // }
                case VisualNovelNames.VermieterNovel:
                {
                    return new Color(12 / 255f, 26 / 255f, 46 / 255f);
                }
                // case VisualNovelNames.FOERDERANTRAG_NOVEL:
                // {
                //     return new Color(17 / 255f, 69 / 255f, 74 / 255f);
                // }
                // case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                // {
                //     return new Color(46 / 255f, 37 / 255f, 12 / 255f);
                // }
                case VisualNovelNames.InvestorNovel:
                {
                    return new Color(48 / 255f, 72 / 255f, 15 / 255f);
                }
                case VisualNovelNames.BankKreditNovel:
                {
                    return new Color(12 / 255f, 26 / 255f, 46 / 255f);
                }
                case VisualNovelNames.HonorarNovel:
                {
                    return new Color(15 / 255f, 69 / 255f, 60 / 255f);
                }
                case VisualNovelNames.EinstiegsNovel:
                {
                    return new Color(83 / 255f, 32 / 255f, 83 / 255f);
                }
                case VisualNovelNames.VertriebNovel:
                {
                    return new Color(83 / 255f, 32 / 255f, 83 / 255f);
                }
                // case VisualNovelNames.LEBENSPARTNER_NOVEL:
                // {
                //     return new Color(12 / 255f, 26 / 255f, 46 / 255f);
                // }
                default:
                {
                    return new Color(0 / 255f, 0 / 255f, 0 / 255f);
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
                // case VisualNovelNames.BANK_KONTO_NOVEL:
                // {
                //     return "Bankkonto";
                // }
                case VisualNovelNames.VermieterNovel:
                {
                    return "Vermieter";
                }
                // case VisualNovelNames.FOERDERANTRAG_NOVEL:
                // {
                //     return "Förderantrag";
                // }
                // case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                // {
                //     return "Gründungs-\r\nzuschuss";
                // }
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
                case VisualNovelNames.VertriebNovel:
                {
                    return "Vertrieb";
                }
                // case VisualNovelNames.LEBENSPARTNER_NOVEL:
                // {
                //     return "Lebens-\r\npartner*in";
                // }
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
                // case VisualNovelNames.BANK_KONTO_NOVEL:
                // {
                //     return 2;
                // }
                case VisualNovelNames.VermieterNovel:
                {
                    return 5;
                }
                // case VisualNovelNames.FOERDERANTRAG_NOVEL:
                // {
                //     return 3;
                // }
                // case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                // {
                //     return 7;
                // }
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
                case VisualNovelNames.VertriebNovel:
                {
                    return 9;
                }
                // case VisualNovelNames.LEBENSPARTNER_NOVEL:
                // {
                //     return 9;
                // }
                default:
                {
                    return -1;
                }
            }
        }
    }
}