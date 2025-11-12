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
        private Color _colorForNovel;
        private Color _foregroundColorForNovel;
        private string _displayName;
        private string _designation;

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
            if (_instance == null)
            {
                _instance = new PlayManager();
            }

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

        /// <summary>
        /// Sets the color associated with the visual novel to be played.
        /// This method is used to store the visual novel's primary color,
        /// allowing consistent visual representation across different scenes or UI elements.
        /// </summary>
        /// <param name="colorOfNovel">The color of the visual novel to be set, typically representing its thematic value or identity.</param>
        public void SetColorOfVisualNovelToPlay(Color colorOfNovel)
        {
            _colorForNovel = colorOfNovel;
        }

        /// <summary>
        /// Retrieves the color associated with the visual novel currently set to be played.
        /// This color is used to represent or style elements related to the selected visual novel,
        /// ensuring consistency in appearance across the application.
        /// </summary>
        /// <returns>The color assigned to the currently selected visual novel.</returns>
        public Color GetColorOfVisualNovelToPlay()
        {
            return _colorForNovel;
        }

        /// <summary>
        /// Sets the display name of the visual novel to be played in the application.
        /// This display name is used to identify and present the novel throughout the game lifecycle.
        /// </summary>
        /// <param name="v">The display name of the visual novel to play.</param>
        public void SetDisplayNameOfNovelToPlay(string v)
        {
            _displayName = v;
        }

        /// <summary>
        /// Retrieves the display name of the visual novel currently set to be played.
        /// This value represents the title or identifier as designated for display purposes in the application.
        /// </summary>
        /// <returns>The display name of the visual novel set in the PlayManager.</returns>
        public string GetDisplayNameOfNovelToPlay()
        {
            return _displayName;
        }
    }
}