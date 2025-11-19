using System;
using System.Collections.Generic;

namespace Assets._Scripts.Utilities
{
    /// <summary>
    /// A serializable wrapper class designed to hold a list of favorite item IDs (represented as long integers).
    /// This class is primarily used for saving and loading favorite data, typically in formats like JSON,
    /// as it makes a list directly serializable by Unity's JsonUtility or other serialization systems.
    /// </summary>
    [Serializable]
    public class FavoritesWrapper
    {
        public List<long> favorites;

        /// <summary>
        /// Initializes or clears the <see cref="favorites"/> list, effectively resetting the user's favorites.
        /// </summary>
        public void ResetFavorites()
        {
            favorites = new List<long>();
        }
    }
}