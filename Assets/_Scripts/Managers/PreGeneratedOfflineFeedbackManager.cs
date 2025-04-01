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
        private Dictionary<string, FeedbackNodeContainer> _feedbackForElternNovel;
        private Dictionary<string, FeedbackNodeContainer> _feedbackForNotariatNovel;
        private Dictionary<string, FeedbackNodeContainer> _feedbackForPresseNovel;
        private Dictionary<string, FeedbackNodeContainer> _feedbackForBueroNovel;
        private Dictionary<string, FeedbackNodeContainer> _feedbackForHonorarNovel;
        private Dictionary<string, FeedbackNodeContainer> _feedbackForIntroNovel;

        private PreGeneratedOfflineFeedbackManager()
        {
            _feedbackForBankKreditNovel = null;
            _feedbackForInvestorNovel = null;
            _feedbackForElternNovel = null;
            _feedbackForNotariatNovel = null;
            _feedbackForPresseNovel = null;
            _feedbackForBueroNovel = null;
            _feedbackForHonorarNovel = null;
            _feedbackForIntroNovel = null;
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
                case VisualNovelNames.VERMIETER_NOVEL:
                {
                    return _feedbackForBueroNovel;
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
                case VisualNovelNames.EINSTIEGS_NOVEL:
                {
                    return _feedbackForIntroNovel;
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
                // case VisualNovelNames.BANK_KONTO_NOVEL:
                // {
                //     _feedbackForBankKontoNovel = feedback;
                //     break;
                // }
                case VisualNovelNames.VERMIETER_NOVEL:
                {
                    _feedbackForBueroNovel = feedback;
                    break;
                }
                // case VisualNovelNames.FOERDERANTRAG_NOVEL:
                // {
                //     _feedbackForFoerderantragNovel = feedback;
                //     break;
                // }
                // case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                // {
                //     _feedbackForGruendungszuschussNovel = feedback;
                //     break;
                // }
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
                case VisualNovelNames.EINSTIEGS_NOVEL:
                {
                    _feedbackForIntroNovel = feedback;
                    break;
                }
                // case VisualNovelNames.LEBENSPARTNER_NOVEL:
                // {
                //     _feedbackForLebenspartnerNovel = feedback;
                //     break;
                // }
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
                // case VisualNovelNames.BANK_KONTO_NOVEL:
                // {
                //     return _feedbackForBankKontoNovel is { Count: > 0 };
                // }
                case VisualNovelNames.VERMIETER_NOVEL:
                {
                    return _feedbackForBueroNovel is { Count: > 0 };
                }
                // case VisualNovelNames.FOERDERANTRAG_NOVEL:
                // {
                //     return _feedbackForFoerderantragNovel is { Count: > 0 };
                // }
                // case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                // {
                //     return _feedbackForGruendungszuschussNovel is { Count: > 0 };
                // }
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
                case VisualNovelNames.EINSTIEGS_NOVEL:
                {
                    return _feedbackForIntroNovel is { Count: > 0 };
                }
                // case VisualNovelNames.LEBENSPARTNER_NOVEL:
                // {
                //     return _feedbackForLebenspartnerNovel is { Count: > 0 };
                // }
                default:
                {
                    return false;
                }
            }
        }
    }
}