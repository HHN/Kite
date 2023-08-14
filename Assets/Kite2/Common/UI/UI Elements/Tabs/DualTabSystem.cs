using UnityEngine;
using UnityEngine.UI;

public class DualTabSystem : MonoBehaviour
{
    public Sprite selectedTabSprite;
    public Sprite notSelectedTabSprite;
    public Button tab1;
    public Button tab2;
    public GameObject contentTab1;
    public GameObject contentTab2;
    private int selectedTab;
    public SearchBar searchBar;

    private void Start()
    {
        selectedTab = 1;
        tab1.onClick.AddListener(delegate { OnTab1Button(); });
        tab2.onClick.AddListener(delegate { OnTab2Button(); });
    }

    public void OnTab1Button()
    {
        if (selectedTab == 1)
        {
            return;
        }
        tab1.image.sprite = selectedTabSprite;
        tab2.image.sprite = notSelectedTabSprite;
        contentTab1.SetActive(true);
        contentTab2.SetActive(false);
        selectedTab = 1;
        searchBar.gallery = contentTab1.GetComponent<VisualNovelGallery>();
    }

    public void OnTab2Button()
    {
        if (selectedTab == 2)
        {
            return;
        }
        tab1.image.sprite = notSelectedTabSprite;
        tab2.image.sprite = selectedTabSprite;
        contentTab1.SetActive(false);
        contentTab2.SetActive(true);
        selectedTab = 2;
        searchBar.gallery = contentTab2.GetComponent<VisualNovelGallery>();
    }
}
