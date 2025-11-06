using System.Collections.Generic;
using Assets._Scripts.Novel;
using Assets._Scripts.Utilities;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    /// <summary>
    /// Represents an entry for a novel, containing its unique identifier
    /// and whether it is included in the current version.
    /// </summary>
    [System.Serializable]
    public class NovelEntry
    {
        public long novelId;
        public bool isContained;
    }
    
    /// <summary>
    /// Singleton manager class for handling operations related to Kite visual novels.
    /// Provides methods to manage, access, and verify the state of loaded Kite visual novels.
    /// </summary>
    public class KiteNovelManager
    {
        private static KiteNovelManager _instance;
        private List<VisualNovel> _kiteNovels;

        /// <summary>
        /// Singleton manager responsible for managing and monitoring the state of Kite visual novels within the application.
        /// Provides functionality to retrieve, set, and check the status of visual novels related to the Kite framework.
        /// </summary>
        private KiteNovelManager()
        {
            _kiteNovels = new List<VisualNovel>();
        }

        /// <summary>
        /// Gets the singleton instance of the KiteNovelManager class.
        /// Ensures that only one instance of the manager exists during the application's runtime,
        /// creating it if necessary.
        /// </summary>
        /// <returns>The singleton instance of KiteNovelManager.</returns>
        public static KiteNovelManager Instance()
        {
            if (_instance == null)
            {
                _instance = new KiteNovelManager();
            }

            return _instance;
        }

        /// <summary>
        /// Retrieves a list of all visual novels managed by the KiteNovelManager.
        /// Provides access to all currently loaded visual novel objects.
        /// </summary>
        /// <returns>A list of VisualNovel objects representing all novels managed by the system.</returns>
        public List<VisualNovel> GetAllKiteNovels()
        {
            return _kiteNovels;
        }

        /// <summary>
        /// Sets the complete list of Kite visual novels managed by the class.
        /// Allows replacement or initialization of the internal collection of Kite novels.
        /// </summary>
        /// <param name="kiteNovels">
        /// The list of Kite visual novels to be assigned. If null is provided, a warning will be logged.
        /// </param>
        public void SetAllKiteNovels(List<VisualNovel> kiteNovels)
        {
            if (kiteNovels == null)
            {
                LogManager.Warning("List<VisualNovel> kiteNovels is null ");
            }
            _kiteNovels = kiteNovels;
        }

        /// <summary>
        /// Checks whether any Kite visual novels are currently loaded in the system.
        /// Determines the loaded state by verifying if the internal collection of visual novels is not null and contains items.
        /// </summary>
        /// <returns>True if there are visual novels loaded, otherwise false.</returns>
        public bool AreNovelsLoaded()
        {
            return _kiteNovels != null && _kiteNovels.Count > 0;
        }
    }
}