using System.Collections.Generic;
using UnityEngine;

public class FavoritesManager
{
    private static FavoritesManager instance;

    private FavoritesWrapper favorites;
    private const string key = "FavoritesWrapper";

    private FavoritesManager() 
    { 
        favorites = LoadFavorites();
    }

    public static FavoritesManager Instance()
    {
        if (instance == null)
        {
            instance = new FavoritesManager();
        }
        return instance;
    }

    public bool IsFavorite(VisualNovel novel)
    {
        return favorites.favorites.Contains(novel.id);
    }

    public bool IsFavorite(long novel)
    {
        return favorites.favorites.Contains(novel);
    }

    public void MarkAsFavorite(VisualNovel novel)
    {
        if (favorites == null)
        {
            favorites = LoadFavorites();
        }

        if (!favorites.favorites.Contains(novel.id))
        {
            favorites.favorites.Add(novel.id);
            Save();
        }
    }

    public void UnmarkAsFavorite(VisualNovel novel)
    {
        if (favorites == null)
        {
            favorites = LoadFavorites();
        }

        if (favorites.favorites.Contains(novel.id))
        {
            favorites.favorites.Remove(novel.id);
            Save();
        }
    }

    public FavoritesWrapper LoadFavorites()
    {
        if (PlayerPrefs.HasKey(key))
        {
            string json = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<FavoritesWrapper>(json);
        }
        else
        {
            return new FavoritesWrapper()
            {
                favorites = new List<long>()
            };
        }
    }
    
    public void Save()
    {
        string json = JsonUtility.ToJson(favorites);
        PlayerDataManager.Instance().SavePlayerData(key, json);
    }

    public List<long> GetFavoritesIds()
    {
        FavoritesWrapper wrapper = LoadFavorites();
        return wrapper.favorites;
    }

    public void ClearFavorites()
    {
        favorites.ResetFavorites();
    }
}
