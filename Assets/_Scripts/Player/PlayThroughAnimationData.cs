using System;
using System.Collections.Generic;
using Assets._Scripts.Novel;

namespace Assets._Scripts.Player
{
    /// <summary>
    /// Represents animation data associated with a playthrough for visual novels.
    /// This class facilitates the management of animations tied to specific visual novel scenarios
    /// and provides methods for querying and updating the animation states.
    /// </summary>
    [Serializable]
    public class PlayThroughAnimationData
    {
        public List<AnimationEntry> animations = new();

        /// <summary>
        /// Represents an individual entry for animation settings tied to a specific visual novel.
        /// Each entry links a visual novel identifier with its associated animation state.
        /// </summary>
        [Serializable]
        public struct AnimationEntry
        {
            public string novel;
            public bool animate;
        }

        private Dictionary<string, bool> _cache;

        /// <summary>
        /// Ensures that the cache mapping visual novel names to their animation states is initialized.
        /// This method initializes the cache dictionary if it is null by populating it with data
        /// from the list of animation entries.
        /// </summary>
        private void EnsureCache()
        {
            if (_cache != null) return;
            _cache = new Dictionary<string, bool>();
            foreach (var entry in animations) _cache[entry.novel] = entry.animate;
        }

        /// <summary>
        /// Retrieves the animation state for a specified visual novel.
        /// This method checks the animation cache and returns whether the specified novel
        /// should have animations enabled.
        /// </summary>
        /// <param name="novel">The visual novel for which the animation state is being retrieved.</param>
        /// <returns>True if animations are enabled for the specified novel; otherwise, false.</returns>
        public bool GetAnimation(string novel)
        {
            EnsureCache();
            return _cache.GetValueOrDefault(novel, false);
        }

        /// <summary>
        /// Updates the animation state for a specific visual novel.
        /// This method sets the specified animation state for the given visual novel
        /// and ensures that the animation state data remains synchronized between the
        /// internal cache and the animation entries list.
        /// </summary>
        /// <param name="novel">The visual novel for which the animation state is being set.</param>
        /// <param name="value">The new animation state to apply to the specified visual novel.</param>
        public void SetAnimation(string novel, bool value)
        {
            EnsureCache();
            _cache[novel] = value;
            SyncList();
        }

        /// <summary>
        /// Synchronizes the animation list with the internal cache.
        /// This method updates the animation list by clearing its current content and repopulating it
        /// with entries from the cache. It ensures that the list reflects the latest state of animations
        /// linked to visual novels.
        /// </summary>
        private void SyncList()
        {
            animations.Clear();
            foreach (var kvp in _cache) animations.Add(new AnimationEntry { novel = kvp.Key, animate = kvp.Value });
        }
    }
}