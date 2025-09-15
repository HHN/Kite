using System;
using Assets._Scripts.Novel;
using Assets._Scripts.Player;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    /// <summary>
    /// Represents the legacy data structure for storing animation states associated with various visual novels.
    /// This class was previously used to manage the state of playthrough animations for specific visual novels.
    /// While no longer actively used, it exists for backward compatibility and data migration purposes.
    /// </summary>
    [Serializable]
    public class OldPlayThroughAnimationData
    {
        public bool animateNumberForBankkreditNovel;
        public bool animateNumberForInvestorNovel;
        public bool animateNumberForElternNovel;
        public bool animateNumberForNotariatNovel;
        public bool animateNumberForPresseNovel;
        public bool animateNumberForHonorarNovel;
        public bool animateNumberForIntroNovel;
    }

    /// <summary>
    /// Manages the playthrough counter-animations within the application, providing mechanisms
    /// for enabling, disabling, and querying the state of animations for specific visual novels.
    /// This class is implemented as a singleton to ensure a single global point of access.
    /// </summary>
    public class PlayThroughCounterAnimationManager
    {
        private static PlayThroughCounterAnimationManager _instance;
        private const string Key = "PlayThroughAnimationManager";
        private const string OldKey = "OldPlayThroughAnimationManager";
        
        private PlayThroughAnimationData _wrapper;

        /// <summary>
        /// Manages animations related to the playthrough counters of various visual novels.
        /// </summary>
        private PlayThroughCounterAnimationManager()
        {
            _wrapper = Load();
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
        /// Sets the animation state for a specified visual novel within the playthrough counter animations.
        /// </summary>
        /// <param name="value">A boolean indicating whether the animation should be enabled or disabled.</param>
        /// <param name="novel">The visual novel for which the animation state is being set.</param>
        public void SetAnimation(bool value, VisualNovelNames novel)
        {
            if (novel == VisualNovelNames.None) return;
            _wrapper.SetAnimation(novel, value);
            Save();
        }

        /// <summary>
        /// Determines whether the animation associated with the specified visual novel
        /// is currently active.
        /// </summary>
        /// <param name="novel">The visual novel for which the animation state is checked.</param>
        /// <returns>True if the animation is active for the specified novel; otherwise, false.</returns>
        public bool IsAnimationTrue(VisualNovelNames novel)
        {
            if (novel == VisualNovelNames.None) return false;
            return _wrapper.GetAnimation(novel);
        }

        /// <summary>
        /// Resets all animation data within the playthrough context to their default states.
        /// This clears any previously set animation configurations and ensures that no animations remain active.
        /// </summary>
        public void ClearAllAnimations()
        {
            _wrapper = new PlayThroughAnimationData();
            Save();
        }

        /// <summary>
        /// Loads the playthrough animation data from persistent storage. If no data exists with the
        /// current key, this method attempts to migrate data from an older key format if available.
        /// If both current and old data formats are unavailable, this method initializes a new
        /// instance of playthrough animation data with default values.
        /// </summary>
        /// <returns>
        /// An instance of <c>PlayThroughAnimationData</c> containing the loaded animations or
        /// an initialized default instance if no data was found.
        /// </returns>
        private PlayThroughAnimationData Load()
        {
            if (PlayerPrefs.HasKey(Key))
            {
                string json = PlayerPrefs.GetString(Key);
                return JsonUtility.FromJson<PlayThroughAnimationData>(json);
            }

            if (PlayerPrefs.HasKey(OldKey))
            {
                string oldJson = PlayerPrefs.GetString(OldKey);
                var oldData = JsonUtility.FromJson<OldPlayThroughAnimationData>(oldJson);

                var newData = new PlayThroughAnimationData();
                newData.SetAnimation(VisualNovelNames.BankKreditNovel, oldData.animateNumberForBankkreditNovel);
                newData.SetAnimation(VisualNovelNames.InvestorNovel, oldData.animateNumberForInvestorNovel);
                newData.SetAnimation(VisualNovelNames.ElternNovel, oldData.animateNumberForElternNovel);
                newData.SetAnimation(VisualNovelNames.NotariatNovel, oldData.animateNumberForNotariatNovel);
                newData.SetAnimation(VisualNovelNames.PresseNovel, oldData.animateNumberForPresseNovel);
                newData.SetAnimation(VisualNovelNames.HonorarNovel, oldData.animateNumberForHonorarNovel);
                newData.SetAnimation(VisualNovelNames.EinstiegsNovel, oldData.animateNumberForIntroNovel);

                string newJson = JsonUtility.ToJson(newData);
                PlayerPrefs.SetString(Key, newJson);
                PlayerPrefs.Save();
                PlayerPrefs.DeleteKey(OldKey);

                return newData;
            }

            return new PlayThroughAnimationData();
        }

        /// <summary>
        /// Persists the current state of playthrough animations to player preferences
        /// by serializing the data and saving it using a unique key.
        /// </summary>
        private void Save()
        {
            string json = JsonUtility.ToJson(_wrapper);
            PlayerPrefs.SetString(Key, json);
            PlayerPrefs.Save();
        }
    }
}