using System;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

public class FoundersBubbleMetaInformation : MonoBehaviour
{
    public static int numerOfNovelsToDisplay = 8;

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
            case VisualNovelNames.BANK_KONTO_NOVEL:
                {
                    return false;
                }
            case VisualNovelNames.BUERO_NOVEL:
                {
                    return true;
                }
            case VisualNovelNames.FOERDERANTRAG_NOVEL:
                {
                    return true;
                }
            case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                {
                    return true;
                }
            case VisualNovelNames.BEKANNTE_TREFFEN_NOVEL:
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
            case VisualNovelNames.INTRO_NOVEL:
                {
                    return false;
                }
            case VisualNovelNames.LEBENSPARTNER_NOVEL:
                {
                    return true;
                }
            default:
                {
                    return false;
                }
        }
    }

    internal static Color GetColorOfNovel(VisualNovelNames value)
    {
        switch (value)
        {
            case VisualNovelNames.ELTERN_NOVEL:
                {
                    return new Color(83 / 255f, 32 / 255f, 83 / 255f);
                }
            case VisualNovelNames.PRESSE_NOVEL:
                {
                    return new Color(48 / 255f, 72 / 255f, 15 / 255f);
                }
            case VisualNovelNames.NOTARIAT_NOVEL:
                {
                    return new Color(12 / 255f, 26 / 255f, 46 / 255f);
                }
            case VisualNovelNames.BANK_KONTO_NOVEL:
                {
                    return new Color(37 / 255f, 101 / 255f, 14 / 255f);
                }
            case VisualNovelNames.BUERO_NOVEL:
                {
                    return new Color(67 / 255f, 15 / 255f, 53 / 255f);
                }
            case VisualNovelNames.FOERDERANTRAG_NOVEL:
                {
                    return new Color(17 / 255f, 69 / 255f, 74 / 255f);
                }
            case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                {
                    return new Color(46 / 255f, 37 / 255f, 12 / 255f);
                }
            case VisualNovelNames.BEKANNTE_TREFFEN_NOVEL:
                {
                    return new Color(41 / 255f, 18 / 255f, 69 / 255f);
                }
            case VisualNovelNames.BANK_KREDIT_NOVEL:
                {
                    return new Color(12 / 255f, 26 / 255f, 46 / 255f);
                }
            case VisualNovelNames.HONORAR_NOVEL:
                {
                    return new Color(15 / 255f, 69 / 255f, 60 / 255f);
                }
            case VisualNovelNames.INTRO_NOVEL:
                {
                    return new Color(48 / 255f, 72 / 255f, 15 / 255f);
                }
            case VisualNovelNames.LEBENSPARTNER_NOVEL:
                {
                    return new Color(12 / 255f, 26 / 255f, 46 / 255f);
                }
            default:
                {
                    return new Color(0 / 255f, 0 / 255f, 0 / 255f);
                }
        }
    }

    internal static int GetIndexOfNovel(VisualNovelNames value)
    {
        switch (value)
        {
            case VisualNovelNames.ELTERN_NOVEL:
                {
                    return 4;
                }
            case VisualNovelNames.PRESSE_NOVEL:
                {
                    return 6;
                }
            case VisualNovelNames.NOTARIAT_NOVEL:
                {
                    return 5;
                }
            case VisualNovelNames.BANK_KONTO_NOVEL:
                {
                    return 2;
                }
            case VisualNovelNames.BUERO_NOVEL:
                {
                    return 7;
                }
            case VisualNovelNames.FOERDERANTRAG_NOVEL:
                {
                    return 3;
                }
            case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                {
                    return 9;
                }
            case VisualNovelNames.BEKANNTE_TREFFEN_NOVEL:
                {
                    return 2;
                }
            case VisualNovelNames.BANK_KREDIT_NOVEL:
                {
                    return 1;
                }
            case VisualNovelNames.HONORAR_NOVEL:
                {
                    return 10;
                }
            case VisualNovelNames.INTRO_NOVEL:
                {
                    return 8;
                }
            case VisualNovelNames.LEBENSPARTNER_NOVEL:
                {
                    return 11;
                }
            default:
                {
                    return -1;
                }
        }
    }
}
