using UnityEngine;
using UnityEngine.UI;

public class RadioButtonHandler : MonoBehaviour
{
    public Toggle kiteNovelsToggle;
    public Toggle userNovelsToggle;
    public Toggle accountNovelsToggle;
    public Toggle Favorites;
    private int indexToActivateOnStart = 0;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        ActivateIndex(indexToActivateOnStart);
    }

    public bool IsKiteNovelsOn()
    {
        return kiteNovelsToggle.isOn; 
    }

    public bool IsUserNovelsOn()
    {
        return userNovelsToggle.isOn;
    }

    public bool IsAccountNovelsOn()
    {
        return accountNovelsToggle.isOn;
    }

    public bool IsFavoritesOn()
    {
        return Favorites.isOn;
    }

    public int GetIndex()
    {
        if (IsKiteNovelsOn())
        {
            return 0;
        } 
        else if (IsUserNovelsOn())
        {
            return 1;
        } 
        else if (IsAccountNovelsOn())
        {
            return 2;
        } 
        else if (IsFavoritesOn())
        {
            return 3;
        }
        return 0;
    }

    public void SetIndex(int index)
    {
        this.indexToActivateOnStart = index;
    }

    private void ActivateIndex(int index)
    {
        switch (index)
        {
            case 0:
                {
                    kiteNovelsToggle.isOn = true;
                    return;
                }
            case 1:
                {
                    userNovelsToggle.isOn = true;
                    return;
                }
            case 2:
                {
                    accountNovelsToggle.isOn = true;
                    return;
                }
            case 3:
                {
                    Favorites.isOn = true;
                    return;
                }
            default:
                {
                    kiteNovelsToggle.isOn = true; 
                    return;
                }
        }
    }
}
