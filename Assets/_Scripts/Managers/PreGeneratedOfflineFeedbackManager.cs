using System.Collections.Generic;
using Assets._Scripts.OfflineAiFeedback;
using Assets._Scripts.Player;

namespace Assets._Scripts.Managers
{
    public class PreGeneratedOfflineFeedbackManager
    {
        private static PreGeneratedOfflineFeedbackManager _instance;

        private Dictionary<string, FeedbackNodeContainer> _feedbackForBankKreditNovel;
        private Dictionary<string, FeedbackNodeContainer> _feedbackForInvestorNovel;
        private Dictionary<string, FeedbackNodeContainer> _feedbackForBankKontoNovel;
        private Dictionary<string, FeedbackNodeContainer> _feedbackForFoerderantragNovel;
        private Dictionary<string, FeedbackNodeContainer> _feedbackForElternNovel;
        private Dictionary<string, FeedbackNodeContainer> _feedbackForNotariatNovel;
        private Dictionary<string, FeedbackNodeContainer> _feedbackForPresseNovel;
        private Dictionary<string, FeedbackNodeContainer> _feedbackForBueroNovel;
        private Dictionary<string, FeedbackNodeContainer> _feedbackForGruendungszuschussNovel;
        private Dictionary<string, FeedbackNodeContainer> _feedbackForHonorarNovel;
        private Dictionary<string, FeedbackNodeContainer> _feedbackForLebenspartnerNovel;
        private Dictionary<string, FeedbackNodeContainer> _feedbackForIntroNovel;

        private PreGeneratedOfflineFeedbackManager()
        {
            this._feedbackForBankKreditNovel = null;
            this._feedbackForInvestorNovel = null;
            this._feedbackForBankKontoNovel = null;
            this._feedbackForFoerderantragNovel = null;
            this._feedbackForElternNovel = null;
            this._feedbackForNotariatNovel = null;
            this._feedbackForPresseNovel = null;
            this._feedbackForBueroNovel = null;
            this._feedbackForGruendungszuschussNovel = null;
            this._feedbackForHonorarNovel = null;
            this._feedbackForLebenspartnerNovel = null;
            this._feedbackForIntroNovel = null;
        }

        public static PreGeneratedOfflineFeedbackManager Instance()
        {
            if (_instance == null)
            {
                _instance = new PreGeneratedOfflineFeedbackManager();
            }

            return _instance;
        }

        public Dictionary<string, FeedbackNodeContainer> GetPreGeneratedOfflineFeedback(VisualNovelNames visualNovel)
        {
            switch (visualNovel)
            {
                case VisualNovelNames.ELTERN_NOVEL:
                {
                    return _feedbackForElternNovel;
                }
                case VisualNovelNames.PRESSE_NOVEL:
                {
                    return _feedbackForPresseNovel;
                }
                case VisualNovelNames.NOTARIAT_NOVEL:
                {
                    return _feedbackForNotariatNovel;
                }
                case VisualNovelNames.BANK_KONTO_NOVEL:
                {
                    return _feedbackForBankKontoNovel;
                }
                case VisualNovelNames.BUERO_NOVEL:
                {
                    return _feedbackForBueroNovel;
                }
                case VisualNovelNames.FOERDERANTRAG_NOVEL:
                {
                    return _feedbackForFoerderantragNovel;
                }
                case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                {
                    return _feedbackForGruendungszuschussNovel;
                }
                case VisualNovelNames.INVESTOR_NOVEL:
                {
                    return _feedbackForInvestorNovel;
                }
                case VisualNovelNames.BANK_KREDIT_NOVEL:
                {
                    return _feedbackForBankKreditNovel;
                }
                case VisualNovelNames.HONORAR_NOVEL:
                {
                    return _feedbackForHonorarNovel;
                }
                case VisualNovelNames.INTRO_NOVEL:
                {
                    return _feedbackForIntroNovel;
                }
                case VisualNovelNames.LEBENSPARTNER_NOVEL:
                {
                    return _feedbackForLebenspartnerNovel;
                }
                default:
                {
                    return null;
                }
            }
        }

        public void SetPreGeneratedOfflineFeedback(VisualNovelNames visualNovel,
            Dictionary<string, FeedbackNodeContainer> feedback)
        {
            switch (visualNovel)
            {
                case VisualNovelNames.ELTERN_NOVEL:
                {
                    _feedbackForElternNovel = feedback;
                    break;
                }
                case VisualNovelNames.PRESSE_NOVEL:
                {
                    _feedbackForPresseNovel = feedback;
                    break;
                }
                case VisualNovelNames.NOTARIAT_NOVEL:
                {
                    _feedbackForNotariatNovel = feedback;
                    break;
                }
                case VisualNovelNames.BANK_KONTO_NOVEL:
                {
                    _feedbackForBankKontoNovel = feedback;
                    break;
                }
                case VisualNovelNames.BUERO_NOVEL:
                {
                    _feedbackForBueroNovel = feedback;
                    break;
                }
                case VisualNovelNames.FOERDERANTRAG_NOVEL:
                {
                    _feedbackForFoerderantragNovel = feedback;
                    break;
                }
                case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                {
                    _feedbackForGruendungszuschussNovel = feedback;
                    break;
                }
                case VisualNovelNames.INVESTOR_NOVEL:
                {
                    _feedbackForInvestorNovel = feedback;
                    break;
                }
                case VisualNovelNames.BANK_KREDIT_NOVEL:
                {
                    _feedbackForBankKreditNovel = feedback;
                    break;
                }
                case VisualNovelNames.HONORAR_NOVEL:
                {
                    _feedbackForHonorarNovel = feedback;
                    break;
                }
                case VisualNovelNames.INTRO_NOVEL:
                {
                    _feedbackForIntroNovel = feedback;
                    break;
                }
                case VisualNovelNames.LEBENSPARTNER_NOVEL:
                {
                    _feedbackForLebenspartnerNovel = feedback;
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
                    return _feedbackForElternNovel is { Count: > 0 };
                }
                case VisualNovelNames.PRESSE_NOVEL:
                {
                    return _feedbackForPresseNovel is { Count: > 0 };
                }
                case VisualNovelNames.NOTARIAT_NOVEL:
                {
                    return _feedbackForNotariatNovel is { Count: > 0 };
                }
                case VisualNovelNames.BANK_KONTO_NOVEL:
                {
                    return _feedbackForBankKontoNovel is { Count: > 0 };
                }
                case VisualNovelNames.BUERO_NOVEL:
                {
                    return _feedbackForBueroNovel is { Count: > 0 };
                }
                case VisualNovelNames.FOERDERANTRAG_NOVEL:
                {
                    return _feedbackForFoerderantragNovel is { Count: > 0 };
                }
                case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                {
                    return _feedbackForGruendungszuschussNovel is { Count: > 0 };
                }
                case VisualNovelNames.INVESTOR_NOVEL:
                {
                    return _feedbackForInvestorNovel is { Count: > 0 };
                }
                case VisualNovelNames.BANK_KREDIT_NOVEL:
                {
                    return _feedbackForBankKreditNovel is { Count: > 0 };
                }
                case VisualNovelNames.HONORAR_NOVEL:
                {
                    return _feedbackForHonorarNovel is { Count: > 0 };
                }
                case VisualNovelNames.INTRO_NOVEL:
                {
                    return _feedbackForIntroNovel is { Count: > 0 };
                }
                case VisualNovelNames.LEBENSPARTNER_NOVEL:
                {
                    return _feedbackForLebenspartnerNovel is { Count: > 0 };
                }
                default:
                {
                    return false;
                }
            }
        }
    }
}