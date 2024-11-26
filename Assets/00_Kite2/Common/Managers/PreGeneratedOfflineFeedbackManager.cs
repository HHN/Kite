using System.Collections.Generic;
using _00_Kite2.Player;

public class PreGeneratedOfflineFeedbackManager
{
    private static PreGeneratedOfflineFeedbackManager instance;

    private Dictionary<string, FeedbackNodeContainer> feedbackForBankKreditNovel;
    private Dictionary<string, FeedbackNodeContainer> feedbackForBekannteTreffenNovel;
    private Dictionary<string, FeedbackNodeContainer> feedbackForBankKontoNovel;
    private Dictionary<string, FeedbackNodeContainer> feedbackForFoerderantragNovel;
    private Dictionary<string, FeedbackNodeContainer> feedbackForElternNovel;
    private Dictionary<string, FeedbackNodeContainer> feedbackForNotariatNovel;
    private Dictionary<string, FeedbackNodeContainer> feedbackForPresseNovel;
    private Dictionary<string, FeedbackNodeContainer> feedbackForBueroNovel;
    private Dictionary<string, FeedbackNodeContainer> feedbackForGruenderZuschussNovel;
    private Dictionary<string, FeedbackNodeContainer> feedbackForHonorarNovel;
    private Dictionary<string, FeedbackNodeContainer> feedbackForLebenspartnerNovel;
    private Dictionary<string, FeedbackNodeContainer> feedbackForIntroNovel;

    private PreGeneratedOfflineFeedbackManager()
    {
        this.feedbackForBankKreditNovel = null;
        this.feedbackForBekannteTreffenNovel = null;
        this.feedbackForBankKontoNovel = null;
        this.feedbackForFoerderantragNovel = null;
        this.feedbackForElternNovel = null;
        this.feedbackForNotariatNovel = null;
        this.feedbackForPresseNovel = null;
        this.feedbackForBueroNovel = null;
        this.feedbackForGruenderZuschussNovel = null;
        this.feedbackForHonorarNovel = null;
        this.feedbackForLebenspartnerNovel = null;
        this.feedbackForIntroNovel = null;
    }

    public static PreGeneratedOfflineFeedbackManager Instance()
    {
        if (instance == null)
        {
            instance = new PreGeneratedOfflineFeedbackManager();
        }
        return instance;
    }

    public Dictionary<string, FeedbackNodeContainer> GetPreGeneratedOfflineFeedback(VisualNovelNames visualNovel)
    {
        switch (visualNovel)
        {
            case VisualNovelNames.ELTERN_NOVEL:
                {
                    return feedbackForElternNovel;
                }
            case VisualNovelNames.PRESSE_NOVEL:
                {
                    return feedbackForPresseNovel;
                }
            case VisualNovelNames.NOTARIAT_NOVEL:
                {
                    return feedbackForNotariatNovel;
                }
            case VisualNovelNames.BANK_KONTO_NOVEL:
                {
                    return feedbackForBankKontoNovel;
                }
            case VisualNovelNames.BUERO_NOVEL:
                {
                    return feedbackForBueroNovel;
                }
            case VisualNovelNames.FOERDERANTRAG_NOVEL:
                {
                    return feedbackForFoerderantragNovel;
                }
            case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                {
                    return feedbackForGruenderZuschussNovel;
                }
            case VisualNovelNames.BEKANNTE_TREFFEN_NOVEL:
                {
                    return feedbackForBekannteTreffenNovel;
                }
            case VisualNovelNames.BANK_KREDIT_NOVEL:
                {
                    return feedbackForBankKreditNovel;
                }
            case VisualNovelNames.HONORAR_NOVEL:
                {
                    return feedbackForHonorarNovel;
                }
            case VisualNovelNames.INTRO_NOVEL:
                {
                    return feedbackForIntroNovel;
                }
            case VisualNovelNames.LEBENSPARTNER_NOVEL:
                {
                    return feedbackForLebenspartnerNovel;
                }
            default:
                {
                    return null;
                }
        }
    }

    public void SetPreGeneratedOfflineFeedback(VisualNovelNames visualNovel, Dictionary<string, FeedbackNodeContainer> feedback)
    {
        switch (visualNovel)
        {
            case VisualNovelNames.ELTERN_NOVEL:
                {
                    feedbackForElternNovel = feedback;
                    break;
                }
            case VisualNovelNames.PRESSE_NOVEL:
                {
                    feedbackForPresseNovel = feedback;
                    break;
                }
            case VisualNovelNames.NOTARIAT_NOVEL:
                {
                    feedbackForNotariatNovel = feedback;
                    break;
                }
            case VisualNovelNames.BANK_KONTO_NOVEL:
                {
                    feedbackForBankKontoNovel = feedback;
                    break;
                }
            case VisualNovelNames.BUERO_NOVEL:
                {
                    feedbackForBueroNovel = feedback;
                    break;
                }
            case VisualNovelNames.FOERDERANTRAG_NOVEL:
                {
                    feedbackForFoerderantragNovel = feedback;
                    break;
                }
            case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                {
                    feedbackForGruenderZuschussNovel = feedback;
                    break;
                }
            case VisualNovelNames.BEKANNTE_TREFFEN_NOVEL:
                {
                    feedbackForBekannteTreffenNovel = feedback;
                    break;
                }
            case VisualNovelNames.BANK_KREDIT_NOVEL:
                {
                    feedbackForBankKreditNovel = feedback;
                    break;
                }
            case VisualNovelNames.HONORAR_NOVEL:
                {
                    feedbackForHonorarNovel = feedback;
                    break;
                }
            case VisualNovelNames.INTRO_NOVEL:
                {
                    feedbackForIntroNovel = feedback;
                    break;
                }
            case VisualNovelNames.LEBENSPARTNER_NOVEL:
                {
                    feedbackForLebenspartnerNovel = feedback;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    public bool IsFeedbackLoaded(VisualNovelNames visualNovel)
    {
        switch (visualNovel)
        {
            case VisualNovelNames.ELTERN_NOVEL:
                {
                    return (feedbackForElternNovel != null && feedbackForElternNovel.Count > 0);
                }
            case VisualNovelNames.PRESSE_NOVEL:
                {
                    return (feedbackForPresseNovel != null && feedbackForPresseNovel.Count > 0);
                }
            case VisualNovelNames.NOTARIAT_NOVEL:
                {
                    return (feedbackForNotariatNovel != null && feedbackForNotariatNovel.Count > 0);
                }
            case VisualNovelNames.BANK_KONTO_NOVEL:
                {
                    return (feedbackForBankKontoNovel != null && feedbackForBankKontoNovel.Count > 0);
                }
            case VisualNovelNames.BUERO_NOVEL:
                {
                    return (feedbackForBueroNovel != null && feedbackForBueroNovel.Count > 0);
                }
            case VisualNovelNames.FOERDERANTRAG_NOVEL:
                {
                    return (feedbackForFoerderantragNovel != null && feedbackForFoerderantragNovel.Count > 0);
                }
            case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                {
                    return (feedbackForGruenderZuschussNovel != null && feedbackForGruenderZuschussNovel.Count > 0);
                }
            case VisualNovelNames.BEKANNTE_TREFFEN_NOVEL:
                {
                    return (feedbackForBekannteTreffenNovel != null && feedbackForBekannteTreffenNovel.Count > 0);
                }
            case VisualNovelNames.BANK_KREDIT_NOVEL:
                {
                    return (feedbackForBankKreditNovel != null && feedbackForBankKreditNovel.Count > 0);
                }
            case VisualNovelNames.HONORAR_NOVEL:
                {
                    return (feedbackForHonorarNovel != null && feedbackForHonorarNovel.Count > 0);
                }
            case VisualNovelNames.INTRO_NOVEL:
                {
                    return (feedbackForIntroNovel != null && feedbackForIntroNovel.Count > 0);
                }
            case VisualNovelNames.LEBENSPARTNER_NOVEL:
                {
                    return (feedbackForLebenspartnerNovel != null && feedbackForLebenspartnerNovel.Count > 0);
                }
            default:
                {
                    return false;
                }
        }
    }
}
