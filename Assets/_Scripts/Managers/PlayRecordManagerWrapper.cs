using System;
using System.Collections.Generic;
using Assets._Scripts.Novel;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    /// <summary>
    /// The PlayRecordManagerWrapper class provides functionality to manage the count of plays
    /// for different types of novels. It includes methods to get and set the number of plays
    /// for specific novel categories and to reset all the data.
    /// </summary>
    [Serializable]
    public class PlayRecordManagerWrapper
    {
        
        [Serializable]
        private struct PlayCountEntry
        {
            public VisualNovelNames novel;
            public int count;
        }
        
        [SerializeField] private List<PlayCountEntry> playCounts = new List<PlayCountEntry>();
        
        private Dictionary<VisualNovelNames, int> _cache;

        /// <summary>
        /// Ensures that the internal cache for play counts is initialized and populated.
        /// </summary>
        /// <remarks>
        /// This method checks if the cache is null. If it is not initialized,
        /// it populates the cache with play counts from the serialized data.
        /// </remarks>
        private void EnsureCache()
        {
            if (_cache != null) return;

            _cache = new Dictionary<VisualNovelNames, int>();
            foreach (var entry in playCounts)
            {
                _cache[entry.novel] = entry.count;
            }
        }

        /// <summary>
        /// Gets the number of plays for the specified novel.
        /// </summary>
        /// <param name="novel">The novel for which to retrieve the number of plays.</param>
        /// <returns>The number of plays registered for the specified novel. If no plays are recorded, returns 0.</returns>
        public int GetNumberOfPlays(VisualNovelNames novel)
        {
            EnsureCache();
            return _cache.GetValueOrDefault(novel, 0);
        }

        /// <summary>
        /// Sets the number of plays for the specified novel.
        /// </summary>
        /// <param name="novel">The novel for which to set the number of plays.</param>
        /// <param name="numberOfPlays">The number of plays to set for the specified novel.</param>
        public void SetNumberOfPlays(VisualNovelNames novel, int numberOfPlays)
        {
            EnsureCache();
            _cache[novel] = numberOfPlays;
            SyncList();
        }

        /// <summary>
        /// Increments the play counter for the specified novel.
        /// </summary>
        /// <param name="novel">The novel for which to increase the play counter.</param>
        public void IncreasePlayCounter(VisualNovelNames novel)
        {
            EnsureCache();

            _cache.TryAdd(novel, 0);

            _cache[novel]++;
            SyncList();
        }

        /// <summary>
        /// Clears all recorded play statistics for all novels.
        /// </summary>
        public void ClearData()
        {
            _cache = new Dictionary<VisualNovelNames, int>();
            playCounts.Clear();
        }

        /// <summary>
        /// Synchronizes the internal dictionary cache with the serialized list of play counts.
        /// </summary>
        /// <remarks>
        /// This method updates the serialized `playCounts` list to reflect the current state
        /// of the `_cache` dictionary. Each entry in the dictionary is converted into a
        /// `PlayCountEntry` structure and added to the list, ensuring persistence of the play count data.
        /// </remarks>
        private void SyncList()
        {
            playCounts.Clear();
            foreach (var kvp in _cache)
            {
                playCounts.Add(new PlayCountEntry { novel = kvp.Key, count = kvp.Value });
            }
        }
    }
}