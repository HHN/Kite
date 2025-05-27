using Assets._Scripts.Novel;

namespace Assets._Scripts.Managers
{
    public class PlayThroughCounterAnimationManager
    {
        private static PlayThroughCounterAnimationManager _instance;

        private bool _animateNumberForBankkreditNovel;
        private bool _animateNumberForInvestorNovel;
        private bool _animateNumberForBankkontoNovel;
        private bool _animateNumberForFoerderantragNovel;
        private bool _animateNumberForElternNovel;
        private bool _animateNumberForNotariatNovel;
        private bool _animateNumberForPresseNovel;
        private bool _animateNumberForBueroNovel;
        private bool _animateNumberForGruendungszuschussNovel;
        private bool _animateNumberForHonorarNovel;
        private bool _animateNumberForLebenspartnerNovel;
        private bool _animateNumberForIntroNovel;

        private PlayThroughCounterAnimationManager()
        {
        }

        public static PlayThroughCounterAnimationManager Instance()
        {
            if (_instance == null)
            {
                _instance = new PlayThroughCounterAnimationManager();
            }

            return _instance;
        }

        public void SetAnimation(bool value, VisualNovelNames novel)
        {
            switch (novel)
            {
                case VisualNovelNames.ElternNovel:
                {
                    _animateNumberForElternNovel = value;
                    break;
                }
                case VisualNovelNames.PresseNovel:
                {
                    _animateNumberForPresseNovel = value;
                    break;
                }
                case VisualNovelNames.NotariatNovel:
                {
                    _animateNumberForNotariatNovel = value;
                    break;
                }
                case VisualNovelNames.VermieterNovel:
                {
                    _animateNumberForBueroNovel = value;
                    break;
                }
                case VisualNovelNames.InvestorNovel:
                {
                    _animateNumberForInvestorNovel = value;
                    break;
                }
                case VisualNovelNames.BankKreditNovel:
                {
                    _animateNumberForBankkreditNovel = value;
                    break;
                }
                case VisualNovelNames.HonorarNovel:
                {
                    _animateNumberForHonorarNovel = value;
                    break;
                }
                case VisualNovelNames.EinstiegsNovel:
                {
                    _animateNumberForIntroNovel = value;
                    break;
                }
            }
        }

        public bool IsAnimationTrue(VisualNovelNames novel)
        {
            switch (novel)
            {
                case VisualNovelNames.ElternNovel:
                {
                    return _animateNumberForElternNovel;
                }
                case VisualNovelNames.PresseNovel:
                {
                    return _animateNumberForPresseNovel;
                }
                case VisualNovelNames.NotariatNovel:
                {
                    return _animateNumberForNotariatNovel;
                }
                case VisualNovelNames.VermieterNovel:
                {
                    return _animateNumberForBueroNovel;
                }
                case VisualNovelNames.InvestorNovel:
                {
                    return _animateNumberForInvestorNovel;
                }
                case VisualNovelNames.BankKreditNovel:
                {
                    return _animateNumberForBankkreditNovel;
                }
                case VisualNovelNames.HonorarNovel:
                {
                    return _animateNumberForHonorarNovel;
                }
                case VisualNovelNames.EinstiegsNovel:
                {
                    return _animateNumberForIntroNovel;
                }
                default:
                {
                    return false;
                }
            }
        }
    }
}