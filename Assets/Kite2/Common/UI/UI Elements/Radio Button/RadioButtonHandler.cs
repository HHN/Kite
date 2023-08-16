using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadioButtonHandler : MonoBehaviour
{
    public Toggle kiteNovelsToggle;
    public Toggle userNovelsToggle;
    public Toggle accountNovelsToggle;
    public Toggle Favorites;

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

}
