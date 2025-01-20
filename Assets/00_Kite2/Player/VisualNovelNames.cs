namespace _00_Kite2.Player
{
    public enum VisualNovelNames
    {
        NONE,
        BANK_KREDIT_NOVEL,
        BEKANNTE_TREFFEN_NOVEL,
        BANK_KONTO_NOVEL,
        FOERDERANTRAG_NOVEL,
        ELTERN_NOVEL,
        NOTARIAT_NOVEL,
        PRESSE_NOVEL,
        BUERO_NOVEL,
        GRUENDER_ZUSCHUSS_NOVEL,
        HONORAR_NOVEL,
        LEBENSPARTNER_NOVEL,
        INTRO_NOVEL
    }

    public class VisualNovelNamesHelper
    {
        public static VisualNovelNames ValueOf(int value)
        {
            switch (value)
            {
                case 2:
                {
                    return VisualNovelNames.ELTERN_NOVEL;
                }
                case 3:
                {
                    return VisualNovelNames.PRESSE_NOVEL;
                }
                case 4:
                {
                    return VisualNovelNames.NOTARIAT_NOVEL;
                }
                case 5:
                {
                    return VisualNovelNames.BANK_KONTO_NOVEL;
                }
                case 6:
                {
                    return VisualNovelNames.BUERO_NOVEL;
                }
                case 7:
                {
                    return VisualNovelNames.FOERDERANTRAG_NOVEL;
                }
                case 8:
                {
                    return VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL;
                }
                case 9:
                {
                    return VisualNovelNames.BEKANNTE_TREFFEN_NOVEL;
                }
                case 10:
                {
                    return VisualNovelNames.BANK_KREDIT_NOVEL;
                }
                case 11:
                {
                    return VisualNovelNames.HONORAR_NOVEL;
                }
                case 13:
                {
                    return VisualNovelNames.INTRO_NOVEL;
                }
                case -10:
                {
                    return VisualNovelNames.LEBENSPARTNER_NOVEL;
                }
                default:
                {
                    return VisualNovelNames.NONE;
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
                case 5:
                {
                    return "Bankkonto";
                }
                case 6:
                {
                    return "Büro";
                }
                case 7:
                {
                    return "Förderantrag";
                }
                case 8:
                {
                    return "Gründungs-zuschuss";
                }
                case 9:
                {
                    return "Bekannten treffen";
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
                    return "Intro-Novel";
                }
                case -10:
                {
                    return "Lebens-partner*in";
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
                case VisualNovelNames.ELTERN_NOVEL:
                {
                    return 2;
                }
                case VisualNovelNames.PRESSE_NOVEL:
                {
                    return 3;
                }
                case VisualNovelNames.NOTARIAT_NOVEL:
                {
                    return 4;
                }
                case VisualNovelNames.BANK_KONTO_NOVEL:
                {
                    return 5;
                }
                case VisualNovelNames.BUERO_NOVEL:
                {
                    return 6;
                }
                case VisualNovelNames.FOERDERANTRAG_NOVEL:
                {
                    return 7;
                }
                case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                {
                    return 8;
                }
                case VisualNovelNames.BEKANNTE_TREFFEN_NOVEL:
                {
                    return 9;
                }
                case VisualNovelNames.BANK_KREDIT_NOVEL:
                {
                    return 10;
                }
                case VisualNovelNames.HONORAR_NOVEL:
                {
                    return 11;
                }
                case VisualNovelNames.INTRO_NOVEL:
                {
                    return 13;
                }
                case VisualNovelNames.LEBENSPARTNER_NOVEL:
                {
                    return -10;
                }
                default:
                {
                    return 0;
                }
            }
        }
    }
}