using System.Collections.Generic;
using Assets._Scripts.Novel;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    /// <summary>
    /// Manages favorite items and provides functionality to mark and unmark items as favorites.
    /// </summary>
    public class FavoritesManager
    {
        private static FavoritesManager _instance;

        private FavoritesWrapper _favorites;
        private const string Key = "FavoritesWrapper";

        /// <summary>
        /// Manages the user's favorite items within the application, allowing items to be marked and unmarked as favorites,
        /// and maintaining the persistence of these favorites across sessions.
        /// </summary>
        private FavoritesManager()
        {
            _favorites = LoadFavorites();
        }

        /// <summary>
        /// Provides a singleton instance of the FavoritesManager, ensuring only one instance exists throughout the application's lifecycle.
        /// </summary>
        /// <returns>
        /// The singleton instance of FavoritesManager.
        /// </returns>
        public static FavoritesManager Instance()
        {
            if (_instance == null)
            {
                _instance = new FavoritesManager();
            }

            return _instance;
        }

        /// <summary>
        /// Determines whether the user has marked a specified visual novel as a favorite.
        /// </summary>
        /// <param name="novel">The visual novel to check.</param>
        /// <returns>True if the visual novel is marked as a favorite, otherwise false.</returns>
        public bool IsFavorite(VisualNovel novel)
        {
            return _favorites.favorites.Contains(novel.id);
        }

        /// <summary>
        /// Determines whether the user has marked a specified visual novel as a favorite based on its unique identifier.
        /// </summary>
        /// <param name="novel">The unique identifier of the visual novel to check.</param>
        /// <returns>True if the visual novel is marked as a favorite, otherwise false.</returns>
        public bool IsFavorite(long novel)
        {
            return _favorites.favorites.Contains(novel);
        }

        /// <summary>
        /// Marks a specific visual novel as a favorite by adding its unique identifier
        /// to the list of favorites, if not already present, and persists the updated list.
        /// </summary>
        /// <param name="novel">The visual novel to be marked as a favorite.</param>
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

        /// <summary>
        /// Removes the specified visual novel from the list of favorite items.
        /// </summary>
        /// <param name="novel">The visual novel to be unmarked as a favorite.</param>
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

        /// <summary>
        /// Loads and retrieves the current user's favorite data from persistent storage. If no saved data exists,
        /// initializes a new empty favorites' wrapper.
        /// </summary>
        /// <returns>
        /// An instance of the FavoritesWrapper containing the list of user's favorite item IDs.
        /// </returns>
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

        /// <summary>
        /// Saves the current state of the favorites to persistent storage.
        /// </summary>
        private void Save()
        {
            string json = JsonUtility.ToJson(_favorites);
            PlayerDataManager.Instance().SavePlayerData(Key, json);
        }

        /// <summary>
        /// Retrieves a list of IDs corresponding to items marked as favorites.
        /// </summary>
        /// <returns>A list of IDs for the favorite items.</returns>
        public List<long> GetFavoritesIds()
        {
            FavoritesWrapper wrapper = LoadFavorites();
            return wrapper.favorites;
        }

        /// <summary>
        /// Clears all favorite items by resetting the internal collection, effectively removing all marked favorites.
        /// </summary>
        public void ClearFavorites()
        {
            _favorites.ResetFavorites();
        }
    }
}