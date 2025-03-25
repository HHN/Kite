using System;
using System.Collections.Generic;

namespace Assets._Scripts
{
    [Serializable]
    public class FavoritesWrapper
    {
        public List<long> favorites;

        public void ResetFavorites()
        {
            favorites = new List<long>();
        }
    }
}