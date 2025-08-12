using System;
using Assets._Scripts.Novel;

namespace Assets._Scripts.Managers
{
    /// <summary>
    /// Manages animations related to the playthrough counters of various visual novels.
    /// </summary>
    public class PlayThroughCounterAnimationManager
    {
        private static PlayThroughCounterAnimationManager _instance;

        private bool _animateNumberForBankkreditNovel;
        private bool _animateNumberForInvestorNovel;
        private bool _animateNumberForElternNovel;
        private bool _animateNumberForNotariatNovel;
        private bool _animateNumberForPresseNovel;
        private bool _animateNumberForHonorarNovel;
        private bool _animateNumberForIntroNovel;

        /// <summary>
        /// Handles the management and control of playthrough counter animations
        /// for different visual novels. Animations can be toggled for specific
        /// visual novels and their current states can be queried.
        /// </summary>
        private PlayThroughCounterAnimationManager()
        {
        }

        /// <summary>
        /// Provides a singleton instance of the <see cref="PlayThroughCounterAnimationManager"/> class,
        /// ensuring that only one instance of the manager is created and accessed globally.
        /// </summary>
        /// <returns>The singleton instance of <see cref="PlayThroughCounterAnimationManager"/>.</returns>
        public static PlayThroughCounterAnimationManager Instance()
        {
            if (_instance == null)
            {
                _instance = new PlayThroughCounterAnimationManager();
            }

            return _instance;
        }

        /// <summary>
        /// Updates the animation state for the specified visual novel.
        /// The animation can be enabled or disabled based on the provided value.
        /// </summary>
        /// <param name="value">A boolean value indicating whether to enable or disable the animation.</param>
        /// <param name="novel">The specific visual novel for which the animation state is being updated.</param>
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
                case VisualNovelNames.None:
                    break;
                case VisualNovelNames.VermieterNovel:
                    break;
                case VisualNovelNames.VertriebNovel:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(novel), novel, null);
            }
        }

        /// <summary>
        /// Determines whether the animation is currently active for a specific visual novel.
        /// </summary>
        /// <param name="novel">The visual novel for which the animation state is being queried.</param>
        /// <returns>True if the animation is active for the specified visual novel; otherwise, false.</returns>
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
                case VisualNovelNames.None:
                case VisualNovelNames.VermieterNovel:
                case VisualNovelNames.VertriebNovel:
                default:
                {
                    return false;
                }
            }
        }
    }
}