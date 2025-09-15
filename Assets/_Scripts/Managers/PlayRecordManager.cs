using Assets._Scripts.Novel;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    /// <summary>
    /// Manages the tracking and recording of playthroughs for visual novels.
    /// This class provides functionality for increasing the play counter,
    /// retrieving the number of plays for a specific visual novel, and clearing stored data.
    /// </summary>
    public class PlayRecordManager
    {
        private static PlayRecordManager _instance;
        private PlayRecordManagerWrapper _wrapper;
        private const string Key = "PlayRecordManagerWrapper";
        private const string OldKey = "OldPlayRecordManagerWrapper";

        /// <summary>
        /// Manages the tracking and recording of playthroughs for visual novels.
        /// </summary>
        /// <remarks>
        /// This class is responsible for increasing play counters, retrieving the number of plays, and clearing stored play data
        /// associated with different visual novels.
        /// </remarks>
        private PlayRecordManager()
        {
        }

        /// <summary>
        /// Provides a singleton instance of the PlayRecordManager class.
        /// </summary>
        /// <returns>
        /// The single instance of the PlayRecordManager, ensuring only one instance exists throughout the application lifespan.
        /// </returns>
        public static PlayRecordManager Instance()
        {
            return _instance ??= new PlayRecordManager();
        }

        /// <summary>
        /// Increments the playthrough counter for the specified visual novel.
        /// </summary>
        /// <remarks>
        /// This method updates the play data for a specific visual novel by increasing its corresponding counter
        /// and persists the updated data. If the provided novel is <c>VisualNovelNames.None</c>, the method performs no action.
        /// </remarks>
        /// <param name="playedNovel">The visual novel for which the play counter should be incremented.</param>
        public void IncreasePlayCounterForNovel(VisualNovelNames playedNovel)
        {
            if (playedNovel == VisualNovelNames.None) return;

            _wrapper ??= LoadPlayRecordManagerWrapper();
            _wrapper.IncreasePlayCounter(playedNovel);
            Save();
        }

        /// <summary>
        /// Retrieves the number of playthroughs for a given visual novel.
        /// </summary>
        /// <param name="novel">The visual novel whose playthrough count is to be retrieved.</param>
        /// <returns>The total number of times the specified visual novel has been played.</returns>
        public int GetNumberOfPlaysForNovel(VisualNovelNames novel)
        {
            _wrapper ??= LoadPlayRecordManagerWrapper();
            return _wrapper.GetNumberOfPlays(novel);
        }

        /// <summary>
        /// Handles the creation and initialization of the PlayRecordManagerWrapper.
        /// </summary>
        /// <remarks>
        /// This method is used to load play record data, migrating from an old format if applicable,
        /// and initializing a new PlayRecordManagerWrapper instance when no prior data exists.
        /// It ensures compatibility between different versions of stored play records.
        /// </remarks>
        /// <returns>
        /// A fully initialized PlayRecordManagerWrapper instance containing migrated or default play record data.
        /// </returns>
        private PlayRecordManagerWrapper LoadPlayRecordManagerWrapper()
        {
            if (PlayerDataManager.Instance().HasKey(Key))
            {
                string json = PlayerDataManager.Instance().GetPlayerData(Key);
                return JsonUtility.FromJson<PlayRecordManagerWrapper>(json);
            }

            if (PlayerPrefs.HasKey(OldKey))
            {
                string oldJson = PlayerPrefs.GetString(OldKey);
                OldPlayRecordManagerWrapper oldData = JsonUtility.FromJson<OldPlayRecordManagerWrapper>(oldJson);

                var newData = new PlayRecordManagerWrapper();

                newData.SetNumberOfPlays(VisualNovelNames.BankKreditNovel, oldData.numberOfPlaysForBankkreditNovel);
                newData.SetNumberOfPlays(VisualNovelNames.InvestorNovel, oldData.numberOfPlaysForInvestorNovel);
                newData.SetNumberOfPlays(VisualNovelNames.ElternNovel, oldData.numberOfPlaysForElternNovel);
                newData.SetNumberOfPlays(VisualNovelNames.NotariatNovel, oldData.numberOfPlaysForNotarinNovel);
                newData.SetNumberOfPlays(VisualNovelNames.PresseNovel, oldData.numberOfPlaysForPresseNovel);
                newData.SetNumberOfPlays(VisualNovelNames.HonorarNovel, oldData.numberOfPlaysForHonorarNovel);
                newData.SetNumberOfPlays(VisualNovelNames.EinstiegsNovel, oldData.numberOfPlaysForIntroNovel);
                newData.SetNumberOfPlays(VisualNovelNames.VermieterNovel, oldData.numberOfPlaysForVermieterNovel);

                string newJson = JsonUtility.ToJson(newData);
                PlayerDataManager.Instance().SavePlayerData(Key, newJson);

                PlayerPrefs.DeleteKey(OldKey);

                return newData;
            }

            return new PlayRecordManagerWrapper();
        }

        /// <summary>
        /// Saves the current state of playthrough records to persistent storage.
        /// </summary>
        /// <remarks>
        /// This method serializes the playthrough data held in the internal wrapper
        /// and stores it using the PlayerDataManager to ensure data persistence between sessions.
        /// It uses the PlayRecordManagerWrapper and assigns the serialized data to a unique key.
        /// </remarks>
        private void Save()
        {
            string json = JsonUtility.ToJson(_wrapper);
            PlayerDataManager.Instance().SavePlayerData(Key, json);
        }

        /// <summary>
        /// Clears all recorded playthrough statistics for visual novels.
        /// </summary>
        /// <remarks>
        /// This method resets the stored playthrough data by invoking the relevant functionality
        /// in the underlying PlayRecordManagerWrapper instance. Once the data is cleared, the changes are saved.
        /// </remarks>
        public void ClearData()
        {
            _wrapper ??= LoadPlayRecordManagerWrapper();
            _wrapper.ClearData();
            Save();
        }
    }
}