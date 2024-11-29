using _00_Kite2.Player;
using UnityEngine;

namespace _00_Kite2.Common.Managers
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
            if (_instance == null)
            {
                _instance = new PlayRecordManager();
            }

            return _instance;
        }

        public void IncreasePlayCounterForNovel(VisualNovelNames playedNovel)
        {
            if (_wrapper == null)
            {
                _wrapper = LoadPlayRecordManagerWrapper();
            }

            switch (playedNovel)
            {
                case VisualNovelNames.ELTERN_NOVEL:
                {
                    int numberOfPlays = _wrapper.GetNumberOfPlaysForElternNovel();
                    numberOfPlays++;
                    _wrapper.SetNumberOfPlaysForElternNovel(numberOfPlays);
                    break;
                }
                case VisualNovelNames.PRESSE_NOVEL:
                {
                    int numberOfPlays = _wrapper.GetNumberOfPlaysForPresseNovel();
                    numberOfPlays++;
                    _wrapper.SetNumberOfPlaysForPresseNovel(numberOfPlays);
                    break;
                }
                case VisualNovelNames.NOTARIAT_NOVEL:
                {
                    int numberOfPlays = _wrapper.GetNumberOfPlaysForNotarinNovel();
                    numberOfPlays++;
                    _wrapper.SetNumberOfPlaysForNotarinNovel(numberOfPlays);
                    break;
                }
                case VisualNovelNames.BANK_KONTO_NOVEL:
                {
                    int numberOfPlays = _wrapper.GetNumberOfPlaysForBankkontoNovel();
                    numberOfPlays++;
                    _wrapper.SetNumberOfPlaysForBankkontoNovel(numberOfPlays);
                    break;
                }
                case VisualNovelNames.BUERO_NOVEL:
                {
                    int numberOfPlays = _wrapper.GetNumberOfPlaysForBueroNovel();
                    numberOfPlays++;
                    _wrapper.SetNumberOfPlaysForBueroNovel(numberOfPlays);
                    break;
                }
                case VisualNovelNames.FOERDERANTRAG_NOVEL:
                {
                    int numberOfPlays = _wrapper.GetNumberOfPlaysForFoerderantragNovel();
                    numberOfPlays++;
                    _wrapper.SetNumberOfPlaysForFoerderantragNovel(numberOfPlays);
                    break;
                }
                case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                {
                    int numberOfPlays = _wrapper.GetNumberOfPlaysForGruenungszuschussNovel();
                    numberOfPlays++;
                    _wrapper.SetNumberOfPlaysForGruendungszuschussNovel(numberOfPlays);
                    break;
                }
                case VisualNovelNames.BEKANNTE_TREFFEN_NOVEL:
                {
                    int numberOfPlays = _wrapper.GetNumberOfPlaysForBekannteTreffenNovel();
                    numberOfPlays++;
                    _wrapper.SetNumberOfPlaysForBekannteTreffenNovel(numberOfPlays);
                    break;
                }
                case VisualNovelNames.BANK_KREDIT_NOVEL:
                {
                    int numberOfPlays = _wrapper.GetNumberOfPlaysForBankkreditNovel();
                    numberOfPlays++;
                    _wrapper.SetNumberOfPlaysForBankkreditNovel(numberOfPlays);
                    break;
                }
                case VisualNovelNames.HONORAR_NOVEL:
                {
                    int numberOfPlays = _wrapper.GetNumberOfPlaysForHonorarNovel();
                    numberOfPlays++;
                    _wrapper.SetNumberOfPlaysForHonorarNovel(numberOfPlays);
                    break;
                }
                case VisualNovelNames.INTRO_NOVEL:
                {
                    int numberOfPlays = _wrapper.GetNumberOfPlaysForIntroNovel();
                    numberOfPlays++;
                    _wrapper.SetNumberOfPlaysForIntroNovel(numberOfPlays);
                    break;
                }
                case VisualNovelNames.LEBENSPARTNER_NOVEL:
                {
                    int numberOfPlays = _wrapper.GetNumberOfPlaysForLebenspartnerNovel();
                    numberOfPlays++;
                    _wrapper.SetNumberOfPlaysForLebenspartnerNovel(numberOfPlays);
                    break;
                }
            }

            Save();
        }

        public int GetNumberOfPlaysForNovel(VisualNovelNames novel)
        {
            if (_wrapper == null)
            {
                _wrapper = LoadPlayRecordManagerWrapper();
            }

            switch (novel)
            {
                case VisualNovelNames.ELTERN_NOVEL:
                {
                    return _wrapper.GetNumberOfPlaysForElternNovel();
                }
                case VisualNovelNames.PRESSE_NOVEL:
                {
                    return _wrapper.GetNumberOfPlaysForPresseNovel();
                }
                case VisualNovelNames.NOTARIAT_NOVEL:
                {
                    return _wrapper.GetNumberOfPlaysForNotarinNovel();
                }
                case VisualNovelNames.BANK_KONTO_NOVEL:
                {
                    return _wrapper.GetNumberOfPlaysForBankkontoNovel();
                }
                case VisualNovelNames.BUERO_NOVEL:
                {
                    return _wrapper.GetNumberOfPlaysForBueroNovel();
                }
                case VisualNovelNames.FOERDERANTRAG_NOVEL:
                {
                    return _wrapper.GetNumberOfPlaysForFoerderantragNovel();
                }
                case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                {
                    return _wrapper.GetNumberOfPlaysForGruenungszuschussNovel();
                }
                case VisualNovelNames.BEKANNTE_TREFFEN_NOVEL:
                {
                    return _wrapper.GetNumberOfPlaysForBekannteTreffenNovel();
                }
                case VisualNovelNames.BANK_KREDIT_NOVEL:
                {
                    return _wrapper.GetNumberOfPlaysForBankkreditNovel();
                }
                case VisualNovelNames.HONORAR_NOVEL:
                {
                    return _wrapper.GetNumberOfPlaysForHonorarNovel();
                }
                case VisualNovelNames.INTRO_NOVEL:
                {
                    return _wrapper.GetNumberOfPlaysForIntroNovel();
                }
                case VisualNovelNames.LEBENSPARTNER_NOVEL:
                {
                    return _wrapper.GetNumberOfPlaysForLebenspartnerNovel();
                }
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