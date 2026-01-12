namespace Assets._Scripts.Managers
{
    /// <summary>
    /// Manages the display of play instructions in the application.
    /// Maintains and controls the setting related to whether instructions should
    /// be shown and provides methods for accessing and updating this setting.
    /// Implements a singleton pattern to ensure a single instance is used across
    /// the application.
    /// </summary>
    public class ShowPlayInstructionManager
    {
        private static ShowPlayInstructionManager _instance;

        private bool _showInstruction;
        private const string Key = "ShowPlayInstruction";

        /// <summary>
        /// Manages the display of play instructions in the application.
        /// Maintains and controls the setting related to whether instructions should
        /// be shown and provides methods for accessing and updating this setting.
        /// Implements a singleton pattern to ensure a single instance is used across
        /// the application.
        /// </summary>
        private ShowPlayInstructionManager()
        {
            _showInstruction = LoadValue();
        }

        /// <summary>
        /// Provides a static instance of the <see cref="ShowPlayInstructionManager"/> class.
        /// Ensures a single, globally accessible instance is used across the application.
        /// Implements a lazy initialization pattern for creating the instance when first accessed.
        /// </summary>
        /// <returns>The singleton instance of <see cref="ShowPlayInstructionManager"/>.</returns>
        public static ShowPlayInstructionManager Instance()
        {
            if (_instance == null)
            {
                _instance = new ShowPlayInstructionManager();
            }

            return _instance;
        }

        /// <summary>
        /// Updates the setting that determines whether play instructions should be shown.
        /// Invokes the internal process to persist the updated value.
        /// </summary>
        /// <param name="value">A boolean indicating whether play instructions should be displayed.</param>
        public void SetShowInstruction(bool value)
        {
            _showInstruction = value;
            Save();
        }

        /// <summary>
        /// Loads the value of the setting based on the previously saved player data.
        /// Retrieves a type from persistent storage to determine if the play instruction should be displayed.
        /// </summary>
        /// <returns>
        /// A boolean value indicating whether the play instruction should be shown.
        /// Returns true if the type does not exist or its value is set to true; otherwise, false.
        /// </returns>
        private bool LoadValue()
        {
            if (PlayerDataManager.Instance().HasKey(Key))
            {
                string value = PlayerDataManager.Instance().GetPlayerData(Key);
                return bool.Parse(value);
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Persists the current play instruction visibility setting to the storage system.
        /// Uses the PlayerDataManager to save the setting with an associated type,
        /// ensuring changes to the instructions' visibility are not lost across sessions.
        /// </summary>
        private void Save()
        {
            PlayerDataManager.Instance().SavePlayerData(Key, _showInstruction.ToString());
        }
    }
}