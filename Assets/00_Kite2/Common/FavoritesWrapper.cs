using System;
using System.Collections.Generic;

[Serializable]
public class FavoritesWrapper
{
    public List<long> favorites;

    public void ResetFavorites()
    {
        favorites = new List<long>();
    }
}
