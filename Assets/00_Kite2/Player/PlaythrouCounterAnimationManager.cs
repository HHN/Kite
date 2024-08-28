using UnityEngine;

public class PlaythrouCounterAnimationManager
{
    private static PlaythrouCounterAnimationManager instance;

    public bool animateNumberForBankkreditNovel;
    public bool animateNumberForBekannteTreffenNovel;
    public bool animateNumberForbankkontoNovel;
    public bool animateNumberForFoerderantragNovel;
    public bool animateNumberForElternNovel;
    public bool animateNumberForNotariatNovel;
    public bool animateNumberForPresseNovel;
    public bool animateNumberForBueroNovel;
    public bool animateNumberForGruendungszuschussNovel;
    public bool animateNumberForHonorarNovel;
    public bool animateNumberForLebenspartnerNovel;
    public bool animateNumberForIntroNovel;

    private PlaythrouCounterAnimationManager() { }

    public static PlaythrouCounterAnimationManager Instance()
    {
        if (instance == null)
        {
            instance = new PlaythrouCounterAnimationManager();
        }
        return instance;
    }

    public void SetAnimation(bool value, VisualNovelNames novel)
    {
        switch (novel)
        {
            case VisualNovelNames.ELTERN_NOVEL:
                {
                    animateNumberForElternNovel = value;
                    break;
                }
            case VisualNovelNames.PRESSE_NOVEL:
                {                   
                    animateNumberForPresseNovel = value;
                    break;
                }
            case VisualNovelNames.NOTARIAT_NOVEL:
                {
                    animateNumberForNotariatNovel = value;
                    break;
                }
            case VisualNovelNames.BANK_KONTO_NOVEL:
                {
                    animateNumberForbankkontoNovel = value;
                    break;
                }
            case VisualNovelNames.BUERO_NOVEL:
                {
                    animateNumberForBueroNovel = value;
                    break;
                }
            case VisualNovelNames.FOERDERANTRAG_NOVEL:
                {
                    animateNumberForFoerderantragNovel = value;
                    break;
                }
            case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                {
                    animateNumberForGruendungszuschussNovel = value;
                    break;
                }
            case VisualNovelNames.BEKANNTE_TREFFEN_NOVEL:
                {
                    animateNumberForBekannteTreffenNovel = value;
                    break;
                }
            case VisualNovelNames.BANK_KREDIT_NOVEL:
                {
                    animateNumberForBankkreditNovel = value;
                    break;
                }
            case VisualNovelNames.HONORAR_NOVEL:
                {
                    animateNumberForHonorarNovel = value;
                    break;
                }
            case VisualNovelNames.INTRO_NOVEL:
                {
                    animateNumberForIntroNovel = value;
                    break;
                }
            case VisualNovelNames.LEBENSPARTNER_NOVEL:
                {
                    animateNumberForLebenspartnerNovel = value;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    public bool IsAnimationTrue(VisualNovelNames novel)
    {
        switch (novel)
        {
            case VisualNovelNames.ELTERN_NOVEL:
                {
                    return animateNumberForElternNovel;
                }
            case VisualNovelNames.PRESSE_NOVEL:
                {
                    return animateNumberForPresseNovel;
                }
            case VisualNovelNames.NOTARIAT_NOVEL:
                {
                    return animateNumberForNotariatNovel;
                }
            case VisualNovelNames.BANK_KONTO_NOVEL:
                {
                    return animateNumberForbankkontoNovel;
                }
            case VisualNovelNames.BUERO_NOVEL:
                {
                    return animateNumberForBueroNovel;
                }
            case VisualNovelNames.FOERDERANTRAG_NOVEL:
                {
                    return animateNumberForFoerderantragNovel;
                }
            case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                {
                    return animateNumberForGruendungszuschussNovel;
                }
            case VisualNovelNames.BEKANNTE_TREFFEN_NOVEL:
                {
                    return animateNumberForBekannteTreffenNovel;
                }
            case VisualNovelNames.BANK_KREDIT_NOVEL:
                {
                    return animateNumberForBankkreditNovel;
                }
            case VisualNovelNames.HONORAR_NOVEL:
                {
                    return animateNumberForHonorarNovel;
                }
            case VisualNovelNames.INTRO_NOVEL:
                {
                    return animateNumberForIntroNovel;
                }
            case VisualNovelNames.LEBENSPARTNER_NOVEL:
                {
                    return animateNumberForLebenspartnerNovel;
                }
            default:
                {
                    return false;
                }
        }
    }
}
