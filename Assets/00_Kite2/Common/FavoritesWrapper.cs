using System;
using System.Collections.Generic;

namespace _00_Kite2.Common
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