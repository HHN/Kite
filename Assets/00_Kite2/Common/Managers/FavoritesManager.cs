using System.Collections.Generic;
using UnityEngine;

namespace _00_Kite2.Common.Managers
{
    public class FavoritesManager
    {
        private static FavoritesManager _instance;

        private FavoritesWrapper _favorites;
        private const string Key = "FavoritesWrapper";

        private FavoritesManager()
        {
            _favorites = LoadFavorites();
        }

        public static FavoritesManager Instance()
        {
            if (_instance == null)
            {
                _instance = new FavoritesManager();
            }

            return _instance;
        }

        public bool IsFavorite(VisualNovel novel)
        {
            return _favorites.favorites.Contains(novel.id);
        }

        public bool IsFavorite(long novel)
        {
            return _favorites.favorites.Contains(novel);
        }

        public void MarkAsFavorite(VisualNovel novel)
        {
            if (_favorites == null)
            {
                _favorites = LoadFavorites();
            }

            if (!_favorites.favorites.Contains(novel.id))
            {
                _favorites.favorites.Add(novel.id);
                Save();
            }
        }

        public void UnmarkAsFavorite(VisualNovel novel)
        {
            if (_favorites == null)
            {
                _favorites = LoadFavorites();
            }

            if (_favorites.favorites.Contains(novel.id))
            {
                _favorites.favorites.Remove(novel.id);
                Save();
            }
        }

        private FavoritesWrapper LoadFavorites()
        {
            if (PlayerPrefs.HasKey(Key))
            {
                string json = PlayerPrefs.GetString(Key);
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

        private void Save()
        {
            string json = JsonUtility.ToJson(_favorites);
            PlayerDataManager.Instance().SavePlayerData(Key, json);
        }

        public List<long> GetFavoritesIds()
        {
            FavoritesWrapper wrapper = LoadFavorites();
            return wrapper.favorites;
        }

        public void ClearFavorites()
        {
            _favorites.ResetFavorites();
        }
    }
}