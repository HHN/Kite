using System;
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

        /// <summary>
        /// Manages the tracking of playthrough records for visual novels.
        /// </summary>
        /// <remarks>
        /// This class is used to record and retrieve the number of times specific visual novels have been played,
        /// as well as to clear playthrough data when needed. It implements functionality such as incrementing play counters,
        /// fetching play data for different novels, and resetting stored play data.
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
            if (_instance == null) _instance = new PlayRecordManager();

            return _instance;
        }

        /// <summary>
        /// Increases the play counter for a specified visual novel.
        /// </summary>
        /// <param name="playedNovel">The visual novel for which the play count should be incremented.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided novel does not match any known visual novel names.
        /// </exception>
        public void IncreasePlayCounterForNovel(VisualNovelNames playedNovel)
        {
            _wrapper ??= LoadPlayRecordManagerWrapper();

            switch (playedNovel)
            {
                case VisualNovelNames.ElternNovel:
                {
                    int numberOfPlays = _wrapper.GetNumberOfPlaysForElternNovel();
                    numberOfPlays++;
                    _wrapper.SetNumberOfPlaysForElternNovel(numberOfPlays);
                    break;
                }
                case VisualNovelNames.PresseNovel:
                {
                    int numberOfPlays = _wrapper.GetNumberOfPlaysForPresseNovel();
                    numberOfPlays++;
                    _wrapper.SetNumberOfPlaysForPresseNovel(numberOfPlays);
                    break;
                }
                case VisualNovelNames.NotariatNovel:
                {
                    int numberOfPlays = _wrapper.GetNumberOfPlaysForNotarinNovel();
                    numberOfPlays++;
                    _wrapper.SetNumberOfPlaysForNotarinNovel(numberOfPlays);
                    break;
                }
                case VisualNovelNames.InvestorNovel:
                {
                    int numberOfPlays = _wrapper.GetNumberOfPlaysForInvestorNovel();
                    numberOfPlays++;
                    _wrapper.SetNumberOfPlaysForInvestorNovel(numberOfPlays);
                    break;
                }
                case VisualNovelNames.BankKreditNovel:
                {
                    int numberOfPlays = _wrapper.GetNumberOfPlaysForBankkreditNovel();
                    numberOfPlays++;
                    _wrapper.SetNumberOfPlaysForBankkreditNovel(numberOfPlays);
                    break;
                }
                case VisualNovelNames.HonorarNovel:
                {
                    int numberOfPlays = _wrapper.GetNumberOfPlaysForHonorarNovel();
                    numberOfPlays++;
                    _wrapper.SetNumberOfPlaysForHonorarNovel(numberOfPlays);
                    break;
                }
                case VisualNovelNames.EinstiegsNovel:
                {
                    int numberOfPlays = _wrapper.GetNumberOfPlaysForIntroNovel();
                    numberOfPlays++;
                    _wrapper.SetNumberOfPlaysForIntroNovel(numberOfPlays);
                    break;
                }
                case VisualNovelNames.VermieterNovel:
                {
                    int numberOfPlays = _wrapper.GetNumberOfPlaysForVermieterNovel();
                    numberOfPlays++;
                    _wrapper.SetNumberOfPlaysForVermieterNovel(numberOfPlays);
                    break;
                }

                case VisualNovelNames.None:
                    break;
                case VisualNovelNames.VertriebNovel:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(playedNovel), playedNovel, null);
            }

            Save();
        }

        /// <summary>
        /// Retrieves the number of times a specific visual novel has been played.
        /// </summary>
        /// <param name="novel">The <see cref="VisualNovelNames"/> enumeration value specifying the visual novel.</param>
        /// <returns>
        /// An integer representing the total number of playthroughs for the specified visual novel.
        /// Returns -1 if the specified visual novel is not tracked.
        /// </returns>
        public int GetNumberOfPlaysForNovel(VisualNovelNames novel)
        {
            _wrapper ??= LoadPlayRecordManagerWrapper();

            switch (novel)
            {
                case VisualNovelNames.ElternNovel:
                {
                    return _wrapper.GetNumberOfPlaysForElternNovel();
                }
                case VisualNovelNames.PresseNovel:
                {
                    return _wrapper.GetNumberOfPlaysForPresseNovel();
                }
                case VisualNovelNames.NotariatNovel:
                {
                    return _wrapper.GetNumberOfPlaysForNotarinNovel();
                }
                case VisualNovelNames.InvestorNovel:
                {
                    return _wrapper.GetNumberOfPlaysForInvestorNovel();
                }
                case VisualNovelNames.BankKreditNovel:
                {
                    return _wrapper.GetNumberOfPlaysForBankkreditNovel();
                }
                case VisualNovelNames.HonorarNovel:
                {
                    return _wrapper.GetNumberOfPlaysForHonorarNovel();
                }
                case VisualNovelNames.EinstiegsNovel:
                {
                    return _wrapper.GetNumberOfPlaysForIntroNovel();
                }
                case VisualNovelNames.VermieterNovel:
                {
                    return _wrapper.GetNumberOfPlaysForVermieterNovel();
                }
                case VisualNovelNames.None:
                case VisualNovelNames.VertriebNovel:
                default:
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// Represents a wrapper used for storing and managing play record data.
        /// </summary>
        /// <remarks>
        /// This class is used as a container for serialized playthrough data, which is accessed and updated by the
        /// PlayRecordManager class. It provides a structure for preserving the state of play records across multiple sessions.
        /// </remarks>
        private PlayRecordManagerWrapper LoadPlayRecordManagerWrapper()
        {
            if (PlayerDataManager.Instance().HasKey(Key))
            {
                string json = PlayerDataManager.Instance().GetPlayerData(Key);
                return JsonUtility.FromJson<PlayRecordManagerWrapper>(json);
            }
            else
            {
                PlayRecordManagerWrapper wrapper = new PlayRecordManagerWrapper();
                return wrapper;
            }
        }

        /// <summary>
        /// Saves the current state of the playthrough data to persistent storage.
        /// </summary>
        /// <remarks>
        /// This method serializes the playthrough data stored in the internal wrapper object
        /// and uses the PlayerDataManager to save it under a predefined key. It ensures
        /// that the latest playthrough information is securely stored and can be retrieved later.
        /// </remarks>
        private void Save()
        {
            string json = JsonUtility.ToJson(_wrapper);
            PlayerDataManager.Instance().SavePlayerData(Key, json);
        }

        /// <summary>
        /// Clears all records of playthrough data for visual novels.
        /// </summary>
        /// <remarks>
        /// This method removes all stored data related to the number of plays for each visual novel.
        /// It is used to reset the playthrough data, typically during actions such as clearing user data or application resets.
        /// </remarks>
        public void ClearData()
        {
            _wrapper ??= LoadPlayRecordManagerWrapper();

            _wrapper.ClearData();
        }
    }
}