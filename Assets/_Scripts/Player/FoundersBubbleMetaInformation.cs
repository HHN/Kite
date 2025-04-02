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
                case VisualNovelNames.VERMIETER_NOVEL:
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
                case VisualNovelNames.ELTERN_NOVEL:
                {
                    return new Color(53 / 255f, 146 / 255f, 20 / 255f);
                }
                case VisualNovelNames.PRESSE_NOVEL:
                {
                    return new Color(120 / 255f, 45 / 255f, 119 / 255f);
                }
                case VisualNovelNames.NOTARIAT_NOVEL:
                {
                    return new Color(27 / 255f, 108 / 255f, 115 / 255f);
                }
                // case VisualNovelNames.BANK_KONTO_NOVEL:
                // {
                //     return new Color(53 / 255f, 146 / 255f, 20 / 255f);
                // }
                case VisualNovelNames.VERMIETER_NOVEL:
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
                case VisualNovelNames.INVESTOR_NOVEL:
                {
                    return new Color(76 / 255f, 114 / 255f, 42 / 255f);
                }
                case VisualNovelNames.BANK_KREDIT_NOVEL:
                {
                    return new Color(23 / 255f, 49 / 255f, 86 / 255f);
                }
                case VisualNovelNames.HONORAR_NOVEL:
                {
                    return new Color(24 / 255f, 111 / 255f, 96 / 255f);
                }
                case VisualNovelNames.EINSTIEGS_NOVEL:
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
                case VisualNovelNames.ELTERN_NOVEL:
                {
                    return new Color(37 / 255f, 101 / 255f, 14 / 255f);
                }
                case VisualNovelNames.PRESSE_NOVEL:
                {
                    return new Color(83 / 255f, 32 / 255f, 83 / 255f);
                }
                case VisualNovelNames.NOTARIAT_NOVEL:
                {
                    return new Color(17 / 255f, 69 / 255f, 74 / 255f);
                }
                // case VisualNovelNames.BANK_KONTO_NOVEL:
                // {
                //     return new Color(37 / 255f, 101 / 255f, 14 / 255f);
                // }
                case VisualNovelNames.VERMIETER_NOVEL:
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
                case VisualNovelNames.INVESTOR_NOVEL:
                {
                    return new Color(48 / 255f, 72 / 255f, 15 / 255f);
                }
                case VisualNovelNames.BANK_KREDIT_NOVEL:
                {
                    return new Color(12 / 255f, 26 / 255f, 46 / 255f);
                }
                case VisualNovelNames.HONORAR_NOVEL:
                {
                    return new Color(15 / 255f, 69 / 255f, 60 / 255f);
                }
                case VisualNovelNames.EINSTIEGS_NOVEL:
                {
                    return new Color(83 / 255f, 32 / 255f, 83 / 255f);
                }
                case VisualNovelNames.VERTRIEB_NOVEL:
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
                case VisualNovelNames.VERMIETER_NOVEL:
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
                    return "Einstieg";
                }
                case VisualNovelNames.VERTRIEB_NOVEL:
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
                case VisualNovelNames.VERMIETER_NOVEL:
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
                case VisualNovelNames.VERTRIEB_NOVEL:
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