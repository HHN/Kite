using System.Collections.Generic;
using UnityEngine;

public class FavoritesManager
{
    private static FavoritesWrapper favorites = LoadFavorites();
    private const string key = "FavoritesWrapper";
    public static bool IsFavorite(VisualNovel novel)
    {
        return favorites.favorites.Contains(novel.id);
    }

    public static void MarkAsFavorite(VisualNovel novel)
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

    public static void UnmarkAsFavorite(VisualNovel novel)
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

    public static FavoritesWrapper LoadFavorites()
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
    public static void Save()
    {
        string json = JsonUtility.ToJson(favorites);
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }

    public static List<long> GetFavoritesIds()
    {
        FavoritesWrapper wrapper = LoadFavorites();
        return wrapper.favorites;
    }
}
