using System;
using Assets._Scripts.Novel;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    public class PlayRecordManager
    {
        private static PlayRecordManager _instance;

        private PlayRecordManagerWrapper _wrapper;
        private const string Key = "PlayRecordManagerWrapper";

        private PlayRecordManager()
        {
        }

        public static PlayRecordManager Instance()
        {
            if (_instance == null) _instance = new PlayRecordManager();

            return _instance;
        }

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
                case VisualNovelNames.VermieterNovel:
                {
                    int numberOfPlays = _wrapper.GetNumberOfPlaysForBueroNovel();
                    numberOfPlays++;
                    _wrapper.SetNumberOfPlaysForBueroNovel(numberOfPlays);
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
                case VisualNovelNames.None:
                    break;
                case VisualNovelNames.VertriebNovel:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(playedNovel), playedNovel, null);
            }

            Save();
        }

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
                case VisualNovelNames.VermieterNovel:
                {
                    return _wrapper.GetNumberOfPlaysForBueroNovel();
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
                case VisualNovelNames.None:
                case VisualNovelNames.VertriebNovel:
                default:
                {
                    return -1;
                }
            }
        }

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

        private void Save()
        {
            string json = JsonUtility.ToJson(_wrapper);
            PlayerDataManager.Instance().SavePlayerData(Key, json);
        }

        public void ClearData()
        {
            _wrapper.ClearData();
        }
    }
}