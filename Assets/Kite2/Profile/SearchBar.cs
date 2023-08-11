using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchBar : MonoBehaviour
{
    public Button searchButton;
    public TMP_InputField inputField;

    private void Start()
    {
        searchButton.onClick.AddListener(delegate { OnSearchButton(); });
    }

    public void OnSearchButton()
    {

    }
}