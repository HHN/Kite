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
                case VisualNovelNames.ELTERN_NOVEL:
                {
                    return false;
                }
                case VisualNovelNames.PRESSE_NOVEL:
                {
                    return false;
                }
                case VisualNovelNames.NOTARIAT_NOVEL:
                {
                    return true;
                }
                // case VisualNovelNames.BANK_KONTO_NOVEL:
                // {
                //     return false;
                // }
                case VisualNovelNames.BUERO_NOVEL:
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
                case VisualNovelNames.INVESTOR_NOVEL:
                {
                    return false;
                }
                case VisualNovelNames.BANK_KREDIT_NOVEL:
                {
                    return true;
                }
                case VisualNovelNames.HONORAR_NOVEL:
                {
                    return false;
                }
                case VisualNovelNames.EINSTIEGS_NOVEL:
                {
                    return true;
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
                case VisualNovelNames.ELTERN_NOVEL:
                {
                    return Green;
                }
                case VisualNovelNames.PRESSE_NOVEL:
                {
                    return Violet;
                }
                case VisualNovelNames.NOTARIAT_NOVEL:
                {
                    return Turquoise;
                }
                // case VisualNovelNames.BANK_KONTO_NOVEL:
                // {
                //     return Green;
                // }
                case VisualNovelNames.BUERO_NOVEL:
                {
                    return DarkBlue;
                }
                // case VisualNovelNames.FOERDERANTRAG_NOVEL:
                // {
                //     return Turquoise;
                // }
                // case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                // {
                //     return Brown;
                // }
                case VisualNovelNames.INVESTOR_NOVEL:
                {
                    return Green;
                }
                case VisualNovelNames.BANK_KREDIT_NOVEL:
                {
                    return DarkBlue;
                }
                case VisualNovelNames.HONORAR_NOVEL:
                {
                    return Turquoise2;
                }
                case VisualNovelNames.EINSTIEGS_NOVEL:
                {
                    return Violet;
                }
                // case VisualNovelNames.LEBENSPARTNER_NOVEL:
                // {
                //     return DarkBlue;
                // }
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
                case VisualNovelNames.ELTERN_NOVEL:
                {
                    return Green;
                }
                case VisualNovelNames.PRESSE_NOVEL:
                {
                    return Violet;
                }
                case VisualNovelNames.NOTARIAT_NOVEL:
                {
                    return Turquoise;
                }
                // case VisualNovelNames.BANK_KONTO_NOVEL:
                // {
                //     return Green;
                // }
                case VisualNovelNames.BUERO_NOVEL:
                {
                    return DarkBlue;
                }
                // case VisualNovelNames.FOERDERANTRAG_NOVEL:
                // {
                //     return Turquoise;
                // }
                // case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                // {
                //     return Brown;
                // }
                case VisualNovelNames.INVESTOR_NOVEL:
                {
                    return GreenBrown;
                }
                case VisualNovelNames.BANK_KREDIT_NOVEL:
                {
                    return DarkBlue;
                }
                case VisualNovelNames.HONORAR_NOVEL:
                {
                    return Turquoise2;
                }
                case VisualNovelNames.EINSTIEGS_NOVEL:
                {
                    return Violet;
                }
                // case VisualNovelNames.LEBENSPARTNER_NOVEL:
                // {
                //     return DarkBlue;
                // }
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
                case VisualNovelNames.ELTERN_NOVEL:
                {
                    return "Eltern";
                }
                case VisualNovelNames.PRESSE_NOVEL:
                {
                    return "Presse";
                }
                case VisualNovelNames.NOTARIAT_NOVEL:
                {
                    return "Notarin";
                }
                // case VisualNovelNames.BANK_KONTO_NOVEL:
                // {
                //     return "Bankkonto";
                // }
                case VisualNovelNames.BUERO_NOVEL:
                {
                    return "Büro";
                }
                // case VisualNovelNames.FOERDERANTRAG_NOVEL:
                // {
                //     return "Förderantrag";
                // }
                // case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                // {
                //     return "Gründungs-\r\nzuschuss";
                // }
                case VisualNovelNames.INVESTOR_NOVEL:
                {
                    return "Investor";
                }
                case VisualNovelNames.BANK_KREDIT_NOVEL:
                {
                    return "Bankkredit";
                }
                case VisualNovelNames.HONORAR_NOVEL:
                {
                    return "Honorar";
                }
                case VisualNovelNames.EINSTIEGS_NOVEL:
                {
                    return "Intro-\r\nNovel";
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
                case VisualNovelNames.ELTERN_NOVEL:
                {
                    return 2;
                }
                case VisualNovelNames.PRESSE_NOVEL:
                {
                    return 4;
                }
                case VisualNovelNames.NOTARIAT_NOVEL:
                {
                    return 3;
                }
                // case VisualNovelNames.BANK_KONTO_NOVEL:
                // {
                //     return 2;
                // }
                case VisualNovelNames.BUERO_NOVEL:
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
                case VisualNovelNames.INVESTOR_NOVEL:
                {
                    return 6;
                }
                case VisualNovelNames.BANK_KREDIT_NOVEL:
                {
                    return 1;
                }
                case VisualNovelNames.HONORAR_NOVEL:
                {
                    return 8;
                }
                case VisualNovelNames.EINSTIEGS_NOVEL:
                {
                    return 7;
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