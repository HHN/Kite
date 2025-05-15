using System.Collections.Generic;
using Assets._Scripts.Novel;
using Assets._Scripts.OfflineAiFeedback;

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
                case VisualNovelNames.ElternNovel:
                {
                    return _feedbackForElternNovel;
                }
                case VisualNovelNames.PresseNovel:
                {
                    return _feedbackForPresseNovel;
                }
                case VisualNovelNames.NotariatNovel:
                {
                    return _feedbackForNotariatNovel;
                }
                case VisualNovelNames.VermieterNovel:
                {
                    return _feedbackForBueroNovel;
                }
                case VisualNovelNames.InvestorNovel:
                {
                    return _feedbackForInvestorNovel;
                }
                case VisualNovelNames.BankKreditNovel:
                {
                    return _feedbackForBankKreditNovel;
                }
                case VisualNovelNames.HonorarNovel:
                {
                    return _feedbackForHonorarNovel;
                }
                case VisualNovelNames.EinstiegsNovel:
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
                case VisualNovelNames.ElternNovel:
                {
                    _feedbackForElternNovel = feedback;
                    break;
                }
                case VisualNovelNames.PresseNovel:
                {
                    _feedbackForPresseNovel = feedback;
                    break;
                }
                case VisualNovelNames.NotariatNovel:
                {
                    _feedbackForNotariatNovel = feedback;
                    break;
                }
                // case VisualNovelNames.BANK_KONTO_NOVEL:
                // {
                //     _feedbackForBankKontoNovel = feedback;
                //     break;
                // }
                case VisualNovelNames.VermieterNovel:
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
                case VisualNovelNames.InvestorNovel:
                {
                    _feedbackForInvestorNovel = feedback;
                    break;
                }
                case VisualNovelNames.BankKreditNovel:
                {
                    _feedbackForBankKreditNovel = feedback;
                    break;
                }
                case VisualNovelNames.HonorarNovel:
                {
                    _feedbackForHonorarNovel = feedback;
                    break;
                }
                case VisualNovelNames.EinstiegsNovel:
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
                case VisualNovelNames.ElternNovel:
                {
                    return _feedbackForElternNovel is { Count: > 0 };
                }
                case VisualNovelNames.PresseNovel:
                {
                    return _feedbackForPresseNovel is { Count: > 0 };
                }
                case VisualNovelNames.NotariatNovel:
                {
                    return _feedbackForNotariatNovel is { Count: > 0 };
                }
                // case VisualNovelNames.BANK_KONTO_NOVEL:
                // {
                //     return _feedbackForBankKontoNovel is { Count: > 0 };
                // }
                case VisualNovelNames.VermieterNovel:
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
                case VisualNovelNames.InvestorNovel:
                {
                    return _feedbackForInvestorNovel is { Count: > 0 };
                }
                case VisualNovelNames.BankKreditNovel:
                {
                    return _feedbackForBankKreditNovel is { Count: > 0 };
                }
                case VisualNovelNames.HonorarNovel:
                {
                    return _feedbackForHonorarNovel is { Count: > 0 };
                }
                case VisualNovelNames.EinstiegsNovel:
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