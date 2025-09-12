using System;
using System.Collections.Generic;
using Assets._Scripts.Novel;

namespace Assets._Scripts.Managers
{
    /// <summary>
    /// Manages animations related to the playthrough counters of various visual novels.
    /// </summary>
    public class PlayThroughCounterAnimationManager
    {
        private static PlayThroughCounterAnimationManager _instance;
        
        // Holds animation state per visual novel
        private readonly Dictionary<VisualNovelNames, bool> _animationStates = new Dictionary<VisualNovelNames, bool>();

        private bool _animateNumberForBankkreditNovel;
        private bool _animateNumberForInvestorNovel;
        private bool _animateNumberForElternNovel;
        private bool _animateNumberForNotariatNovel;
        private bool _animateNumberForPresseNovel;
        private bool _animateNumberForHonorarNovel;
        private bool _animateNumberForIntroNovel;

        /// <summary>
        /// Handles the management and control of playthrough counter-animations
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
            return _instance ??= new PlayThroughCounterAnimationManager();
        }

        /// <summary>
        /// Updates the animation state for the specified visual novel.
        /// The animation can be enabled or disabled based on the provided value.
        /// </summary>
        /// <param name="value">A boolean value indicating whether to enable or disable the animation.</param>
        /// <param name="novel">The specific visual novel for which the animation state is being updated.</param>
        public void SetAnimation(bool value, VisualNovelNames novel)
        {
            if (novel == VisualNovelNames.None) return;

            _animationStates[novel] = value;
        }

        /// <summary>
        /// Determines whether the animation is currently active for a specific visual novel.
        /// </summary>
        /// <param name="novel">The visual novel for which the animation state is being queried.</param>
        /// <returns>True if the animation is active for the specified visual novel; otherwise, false.</returns>
        public bool IsAnimationTrue(VisualNovelNames novel)
        {
            if (novel == VisualNovelNames.None) return false;

            return _animationStates.TryGetValue(novel, out var value) && value;
        }

        /// <summary>
        /// Clears all animation states for all visual novels, removing any active
        /// animations and resetting the internal state tracking their statuses.
        /// </summary>
        public void ClearAllAnimations()
        {
            _animationStates.Clear();
        }
    }
}