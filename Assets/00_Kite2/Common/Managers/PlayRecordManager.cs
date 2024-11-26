using _00_Kite2.Player;
using UnityEngine;

public class PlayRecordManager
{
    private static PlayRecordManager instance;

    private PlayRecordManagerWrapper wrapper;
    private const string key = "PlayRecordManagerWrapper";

    private PlayRecordManager() { }

    public static PlayRecordManager Instance()
    {
        if (instance == null)
        {
            instance = new PlayRecordManager();
        }
        return instance;
    }

    public void IncrasePlayCounterForNovel(VisualNovelNames playedNovel)
    {
        if (wrapper == null)
        {
            wrapper = LoadPlayRecordManagerWrapper();
        }
        switch (playedNovel)
        {
            case VisualNovelNames.ELTERN_NOVEL:
                {
                    int numberOfPlays = wrapper.GetNumberOfPlaysForElternNovel();
                    numberOfPlays++;
                    wrapper.SetNumberOfPlaysForElternNovel(numberOfPlays);
                    break;
                }
            case VisualNovelNames.PRESSE_NOVEL:
                {
                    int numberOfPlays = wrapper.GetNumberOfPlaysForPresseNovel();
                    numberOfPlays++;
                    wrapper.SetNumberOfPlaysForPresseNovel(numberOfPlays);
                    break;
                }
            case VisualNovelNames.NOTARIAT_NOVEL:
                {
                    int numberOfPlays = wrapper.GetNumberOfPlaysForNotarinNovel();
                    numberOfPlays++;
                    wrapper.SetNumberOfPlaysForNotarinNovel(numberOfPlays);
                    break;
                }
            case VisualNovelNames.BANK_KONTO_NOVEL:
                {
                    int numberOfPlays = wrapper.GetNumberOfPlaysForBankkontoNovel();
                    numberOfPlays++;
                    wrapper.SetNumberOfPlaysForBankkontoNovel(numberOfPlays);
                    break;
                }
            case VisualNovelNames.BUERO_NOVEL:
                {
                    int numberOfPlays = wrapper.GetNumberOfPlaysForBueroNovel();
                    numberOfPlays++;
                    wrapper.SetNumberOfPlaysForBueroNovel(numberOfPlays);
                    break;
                }
            case VisualNovelNames.FOERDERANTRAG_NOVEL:
                {
                    int numberOfPlays = wrapper.GetNumberOfPlaysForFoerderantragNovel();
                    numberOfPlays++;
                    wrapper.SetNumberOfPlaysForFoerderantragNovel(numberOfPlays);
                    break;
                }
            case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                {
                    int numberOfPlays = wrapper.GetNumberOfPlaysForGruenungszuschussNovel();
                    numberOfPlays++;
                    wrapper.SetNumberOfPlaysForGruenungszuschussNovel(numberOfPlays);
                    break;
                }
            case VisualNovelNames.BEKANNTE_TREFFEN_NOVEL:
                {
                    int numberOfPlays = wrapper.GetNumberOfPlaysForBekannteTreffenNovel();
                    numberOfPlays++;
                    wrapper.SetNumberOfPlaysForBekannteTreffenNovel(numberOfPlays);
                    break;
                }
            case VisualNovelNames.BANK_KREDIT_NOVEL:
                {
                    int numberOfPlays = wrapper.GetNumberOfPlaysForBankkreditNovel();
                    numberOfPlays++;
                    wrapper.SetNumberOfPlaysForBankkreditNovel(numberOfPlays);
                    break;
                }
            case VisualNovelNames.HONORAR_NOVEL:
                {
                    int numberOfPlays = wrapper.GetNumberOfPlaysForHonorarNovel();
                    numberOfPlays++;
                    wrapper.SetNumberOfPlaysForHonorarNovel(numberOfPlays);
                    break;
                }
            case VisualNovelNames.INTRO_NOVEL:
                {
                    int numberOfPlays = wrapper.GetNumberOfPlaysForIntroNovel();
                    numberOfPlays++;
                    wrapper.SetNumberOfPlaysForIntroNovel(numberOfPlays);
                    break;
                }
            case VisualNovelNames.LEBENSPARTNER_NOVEL:
                {
                    int numberOfPlays = wrapper.GetNumberOfPlaysForLebenspartnerNovel();
                    numberOfPlays++;
                    wrapper.SetNumberOfPlaysForLebenspartnerNovel(numberOfPlays);
                    break;
                }
            default:
                {
                    break;
                }
        }
        Save();
    }

    public int GetNumberOfPlaysForNovel(VisualNovelNames novel)
    {
        if (wrapper == null)
        {
            wrapper = LoadPlayRecordManagerWrapper();
        }
        switch (novel)
        {
            case VisualNovelNames.ELTERN_NOVEL:
                {
                    return wrapper.GetNumberOfPlaysForElternNovel();
                }
            case VisualNovelNames.PRESSE_NOVEL:
                {
                    return wrapper.GetNumberOfPlaysForPresseNovel();
                }
            case VisualNovelNames.NOTARIAT_NOVEL:
                {
                    return wrapper.GetNumberOfPlaysForNotarinNovel();
                }
            case VisualNovelNames.BANK_KONTO_NOVEL:
                {
                    return wrapper.GetNumberOfPlaysForBankkontoNovel();
                }
            case VisualNovelNames.BUERO_NOVEL:
                {
                    return wrapper.GetNumberOfPlaysForBueroNovel();
                }
            case VisualNovelNames.FOERDERANTRAG_NOVEL:
                {
                    return wrapper.GetNumberOfPlaysForFoerderantragNovel();
                }
            case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                {
                    return wrapper.GetNumberOfPlaysForGruenungszuschussNovel();
                }
            case VisualNovelNames.BEKANNTE_TREFFEN_NOVEL:
                {
                    return wrapper.GetNumberOfPlaysForBekannteTreffenNovel();
                }
            case VisualNovelNames.BANK_KREDIT_NOVEL:
                {
                    return wrapper.GetNumberOfPlaysForBankkreditNovel();
                }
            case VisualNovelNames.HONORAR_NOVEL:
                {
                    return wrapper.GetNumberOfPlaysForHonorarNovel();
                }
            case VisualNovelNames.INTRO_NOVEL:
                {
                    return wrapper.GetNumberOfPlaysForIntroNovel();
                }
            case VisualNovelNames.LEBENSPARTNER_NOVEL:
                {
                    return wrapper.GetNumberOfPlaysForLebenspartnerNovel();
                }
            default:
                {
                    return -1;
                }
        }
    }

    public PlayRecordManagerWrapper LoadPlayRecordManagerWrapper()
    {
        if (PlayerDataManager.Instance().HasKey(key))
        {
            string json = PlayerDataManager.Instance().GetPlayerData(key);
            return JsonUtility.FromJson<PlayRecordManagerWrapper>(json);
        }
        else
        {
            PlayRecordManagerWrapper wrapper = new PlayRecordManagerWrapper();
            return wrapper;
        }
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(wrapper);
        PlayerDataManager.Instance().SavePlayerData(key, json);
    }

    public void ClearData()
    {
        wrapper.ClearData();
    }
}
