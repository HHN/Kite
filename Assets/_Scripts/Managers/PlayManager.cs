using Assets._Scripts.Novel;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    /// <summary>
    /// Manages the state and properties related to the visual novel being played.
    /// Provides methods to set and retrieve the visual novel, its display information, and associated data.
    /// Implements a singleton pattern for easy access throughout the application.
    /// </summary>
    public class PlayManager
    {
        private static PlayManager _instance;
        private VisualNovel _novelToPlay;
        private Color _foregroundColorForNovel;

        /// <summary>
        /// Manages the state and configuration of the visual novel to be played in the application.
        /// This class ensures that details such as the target visual novel, its properties,
        /// and associated metadata are easily set and retrieved throughout the game lifecycle.
        /// It follows a singleton pattern, ensuring a single instance is responsible
        /// for managing the currently active novel state.
        /// </summary>
        private PlayManager()
        {
        }

        /// <summary>
        /// Provides a globally accessible instance of the PlayManager, ensuring that a single instance
        /// of the PlayManager is used throughout the application in alignment with the singleton pattern.
        /// </summary>
        /// <returns>The shared instance of the PlayManager, initialized if it does not exist.</returns>
        public static PlayManager Instance()
        {
            _instance ??= new PlayManager();

            return _instance;
        }

        /// <summary>
        /// Sets the visual novel to be played in the application. This method assigns the provided
        /// visual novel instance to the PlayManager, enabling further configurations and gameplay
        /// functionalities based on the selected novel.
        /// </summary>
        /// <param name="novelToPlay">An instance of the <see cref="VisualNovel"/> class representing the visual novel to be played.</param>
        public void SetVisualNovelToPlay(VisualNovel novelToPlay)
        {
            _novelToPlay = novelToPlay;
        }

        /// <summary>
        /// Retrieves the currently set visual novel to be played in the application.
        /// Ensures access to the visual novel object that includes its metadata, storyline,
        /// and associated properties. Returns null if no visual novel has been set.
        /// </summary>
        /// <returns>
        /// The currently configured VisualNovel object, or null if none is set.
        /// </returns>
        public VisualNovel GetVisualNovelToPlay()
        {
            if (_novelToPlay == null)
            {
                return null;
            }

            return _novelToPlay;
        }
    }
}