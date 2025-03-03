using Assets._Scripts.Player;

namespace Assets._Scripts.Managers
{
    public class PlayThroughCounterAnimationManager
    {
        private static PlayThroughCounterAnimationManager _instance;

        private bool _animateNumberForBankkreditNovel;
        private bool _animateNumberForBekannteTreffenNovel;
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
                case VisualNovelNames.ELTERN_NOVEL:
                {
                    _animateNumberForElternNovel = value;
                    break;
                }
                case VisualNovelNames.PRESSE_NOVEL:
                {
                    _animateNumberForPresseNovel = value;
                    break;
                }
                case VisualNovelNames.NOTARIAT_NOVEL:
                {
                    _animateNumberForNotariatNovel = value;
                    break;
                }
                case VisualNovelNames.BANK_KONTO_NOVEL:
                {
                    _animateNumberForBankkontoNovel = value;
                    break;
                }
                case VisualNovelNames.BUERO_NOVEL:
                {
                    _animateNumberForBueroNovel = value;
                    break;
                }
                case VisualNovelNames.FOERDERANTRAG_NOVEL:
                {
                    _animateNumberForFoerderantragNovel = value;
                    break;
                }
                case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                {
                    _animateNumberForGruendungszuschussNovel = value;
                    break;
                }
                case VisualNovelNames.BEKANNTEN_TREFFEN_NOVEL:
                {
                    _animateNumberForBekannteTreffenNovel = value;
                    break;
                }
                case VisualNovelNames.BANK_KREDIT_NOVEL:
                {
                    _animateNumberForBankkreditNovel = value;
                    break;
                }
                case VisualNovelNames.HONORAR_NOVEL:
                {
                    _animateNumberForHonorarNovel = value;
                    break;
                }
                case VisualNovelNames.INTRO_NOVEL:
                {
                    _animateNumberForIntroNovel = value;
                    break;
                }
                case VisualNovelNames.LEBENSPARTNER_NOVEL:
                {
                    _animateNumberForLebenspartnerNovel = value;
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
                    return _animateNumberForElternNovel;
                }
                case VisualNovelNames.PRESSE_NOVEL:
                {
                    return _animateNumberForPresseNovel;
                }
                case VisualNovelNames.NOTARIAT_NOVEL:
                {
                    return _animateNumberForNotariatNovel;
                }
                case VisualNovelNames.BANK_KONTO_NOVEL:
                {
                    return _animateNumberForBankkontoNovel;
                }
                case VisualNovelNames.BUERO_NOVEL:
                {
                    return _animateNumberForBueroNovel;
                }
                case VisualNovelNames.FOERDERANTRAG_NOVEL:
                {
                    return _animateNumberForFoerderantragNovel;
                }
                case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                {
                    return _animateNumberForGruendungszuschussNovel;
                }
                case VisualNovelNames.BEKANNTEN_TREFFEN_NOVEL:
                {
                    return _animateNumberForBekannteTreffenNovel;
                }
                case VisualNovelNames.BANK_KREDIT_NOVEL:
                {
                    return _animateNumberForBankkreditNovel;
                }
                case VisualNovelNames.HONORAR_NOVEL:
                {
                    return _animateNumberForHonorarNovel;
                }
                case VisualNovelNames.INTRO_NOVEL:
                {
                    return _animateNumberForIntroNovel;
                }
                case VisualNovelNames.LEBENSPARTNER_NOVEL:
                {
                    return _animateNumberForLebenspartnerNovel;
                }
                default:
                {
                    return false;
                }
            }
        }
    }
}