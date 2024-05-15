using System;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

public class FoundersBubbleMetaInformation : MonoBehaviour
{

    public static bool IsHighInGui(VisualNovelNames value)
    {
        switch (value)
        {
            case VisualNovelNames.ELTERN_NOVEL:
                {
                    return true;
                }
            case VisualNovelNames.PRESSE_NOVEL:
                {
                    return true;
                }
            case VisualNovelNames.NOTARIAT_NOVEL:
                {
                    return false;
                }
            case VisualNovelNames.BANK_KONTO_NOVEL:
                {
                    return true;
                }
            case VisualNovelNames.BUERO_NOVEL:
                {
                    return false;
                }
            case VisualNovelNames.FOERDERANTRAG_NOVEL:
                {
                    return false;
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

    internal static int GetIndexOfNovel(VisualNovelNames value)
    {
        switch (value)
        {
            case VisualNovelNames.ELTERN_NOVEL:
                {
                    return 5;
                }
            case VisualNovelNames.PRESSE_NOVEL:
                {
                    return 7;
                }
            case VisualNovelNames.NOTARIAT_NOVEL:
                {
                    return 6;
                }
            case VisualNovelNames.BANK_KONTO_NOVEL:
                {
                    return 3;
                }
            case VisualNovelNames.BUERO_NOVEL:
                {
                    return 8;
                }
            case VisualNovelNames.FOERDERANTRAG_NOVEL:
                {
                    return 4;
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
                    return 12;
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
