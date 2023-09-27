using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateScoreAndMoneyManager : OnSuccessHandler
{
    private static UpdateScoreAndMoneyManager instance;

    private UpdateScoreAndMoneyManager() { }

    public static UpdateScoreAndMoneyManager Instance()
    {
        if (instance == null)
        {
            instance = new UpdateScoreAndMoneyManager();
        }
        return instance;
    }

    public void OnSuccess(Response response)
    {
        if (ResultCodeHelper.ValueOf(response.resultCode) == ResultCode.SUCCESSFULLY_UPDATED_MONEY)
        {
            MoneyManager.Instance().SetMoney(response.money);
        }
        else if (ResultCodeHelper.ValueOf(response.resultCode) == ResultCode.SUCCESSFULLY_UPDATED_SCORE)
        {
            ScoreManager.Instance().SetScore(response.score);
        }
    }
}
