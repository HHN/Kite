using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MoneyManager
{
    private static MoneyManager instance;
    private long money;

    private MoneyManager() { }

    public static MoneyManager Instance()
    {
        if (instance == null)
        {
            instance = new MoneyManager();
        }
        return instance;
    }
    public long GetMoney() 
    {
        return money; 
    }

    public void SetMoney(long money) 
    {  
        this.money = money;
    }
}
