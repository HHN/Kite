using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExplorerButtons : MonoBehaviour
{
    public Button kiteNovelsButton;
    public Image kiteNovelsUnterline;
    public Button userNovelsButton;
    public Image userNovelsUnterline;
    public Button accountNovelsButton;
    public Image accountNovelsUnterline;
    public Button favoriteNovelsButton;
    public Image favoriteNovelsUnterline;
    public Button filterNovelsButton;
    public Image filterNovelsUnterline;
    public TextMeshProUGUI contentInfoText;
    public VisualNovelGallery gallery;
    public NovelExplorerSceneController sceneController;
    public GameObject filter;
    public RadioButtonHandler radioButtonHandler;
    public float kiteGaleryPosition = 1;
    public float userGaleryPosition = 1;
    public float accountGaleryPosition = 1;
    public float favoritesGaleryPosition = 1;
    public float filterGaleryPosition = 1;
    public GalleryType openGallery = GalleryType.NONE;

    // Start is called before the first frame update
    void Start()
    {
        AnalyticsServiceHandler.Instance().StartStopwatch();
        kiteNovelsButton.onClick.AddListener(delegate { OnKiteNovelsButton(); });
        userNovelsButton.onClick.AddListener(delegate { OnUserNovelsButton(); });
        accountNovelsButton.onClick.AddListener(delegate { OnAccountNovelsButton(); });
        favoriteNovelsButton.onClick.AddListener(delegate { OnFavoritNovelsButton(); });
        filterNovelsButton.onClick.AddListener(delegate { OnFilterNovelsButton(); });
    }

    public void OnKiteNovelsButton()
    {
        AnalyticsServiceHandler.Instance().SetFromWhereIsNovelSelected("KITE NOVELS");
        SaveCurrentPosition();
        filter.SetActive(false);
        kiteNovelsUnterline.gameObject.SetActive(true);
        userNovelsUnterline.gameObject.SetActive(false);
        accountNovelsUnterline.gameObject.SetActive(false);
        favoriteNovelsUnterline.gameObject.SetActive(false);
        filterNovelsUnterline.gameObject.SetActive(false);
        contentInfoText.SetText("KITE NOVELS");
        sceneController.tabIndex = 0;

        List<VisualNovel> visualNovels = KiteNovelManager.GetAllKiteNovels();
        gallery.RemoveAll();
        gallery.AddNovelsToGallery(visualNovels);

        openGallery = GalleryType.KITE_GALLERY;
        StartCoroutine(gallery.EnsureCorrectScrollPosition(kiteGaleryPosition));
    }

    public void OnUserNovelsButton()
    {
        AnalyticsServiceHandler.Instance().SetFromWhereIsNovelSelected("USER NOVELS");
        SaveCurrentPosition();
        filter.SetActive(false);
        kiteNovelsUnterline.gameObject.SetActive(false);
        userNovelsUnterline.gameObject.SetActive(true);
        accountNovelsUnterline.gameObject.SetActive(false);
        favoriteNovelsUnterline.gameObject.SetActive(false);
        filterNovelsUnterline.gameObject.SetActive(false);
        contentInfoText.SetText("USER NOVELS");
        sceneController.tabIndex = 1;

        List<VisualNovel> visualNovels = sceneController.userNovels;
        gallery.RemoveAll();
        gallery.AddNovelsToGallery(visualNovels);

        openGallery = GalleryType.USER_GALLERY;
        StartCoroutine(gallery.EnsureCorrectScrollPosition(userGaleryPosition));
    }

    public void OnAccountNovelsButton()
    {
        AnalyticsServiceHandler.Instance().SetFromWhereIsNovelSelected("EIGENE NOVELS");
        SaveCurrentPosition();
        filter.SetActive(false);
        kiteNovelsUnterline.gameObject.SetActive(false);
        userNovelsUnterline.gameObject.SetActive(false);
        accountNovelsUnterline.gameObject.SetActive(true);
        favoriteNovelsUnterline.gameObject.SetActive(false);
        filterNovelsUnterline.gameObject.SetActive(false);
        contentInfoText.SetText("EIGENE NOVELS");
        sceneController.tabIndex = 2;

        List<VisualNovel> visualNovels = AccountNovelManager.Instance().GetAllAccountNovels();
        gallery.RemoveAll();
        gallery.AddNovelsToGallery(visualNovels);

        openGallery = GalleryType.ACCOUNT_GALLERY;
        StartCoroutine(gallery.EnsureCorrectScrollPosition(accountGaleryPosition));
    }

    public void OnFavoritNovelsButton()
    {
        AnalyticsServiceHandler.Instance().SetFromWhereIsNovelSelected("FAVORITEN");
        SaveCurrentPosition();
        filter.SetActive(false);
        kiteNovelsUnterline.gameObject.SetActive(false);
        userNovelsUnterline.gameObject.SetActive(false);
        accountNovelsUnterline.gameObject.SetActive(false);
        favoriteNovelsUnterline.gameObject.SetActive(true);
        filterNovelsUnterline.gameObject.SetActive(false);
        contentInfoText.SetText("FAVORITEN");
        sceneController.tabIndex = 3;

        List<VisualNovel> visualNovels = GetFavoriteNovels();
        gallery.RemoveAll();
        gallery.AddNovelsToGallery(visualNovels);

        openGallery = GalleryType.FAVORITES_GALLERY;
        StartCoroutine(gallery.EnsureCorrectScrollPosition(favoritesGaleryPosition));
    }

    public void OnFilterNovelsButton()
    {
        AnalyticsServiceHandler.Instance().SetFromWhereIsNovelSelected("FILTER SUCHE");
        SaveCurrentPosition();
        kiteNovelsUnterline.gameObject.SetActive(false);
        userNovelsUnterline.gameObject.SetActive(false);
        accountNovelsUnterline.gameObject.SetActive(false);
        favoriteNovelsUnterline.gameObject.SetActive(false);
        filterNovelsUnterline.gameObject.SetActive(true);
        contentInfoText.SetText("FILTER SUCHE");
        sceneController.tabIndex = 4;

        List<VisualNovel> visualNovels = new List<VisualNovel>();
        gallery.RemoveAll();
        gallery.AddNovelsToGallery(visualNovels);
        filter.SetActive(true);

        openGallery = GalleryType.FILTER_GALLERY;
        StartCoroutine(gallery.EnsureCorrectScrollPosition(filterGaleryPosition));
    }

    public List<VisualNovel> GetFavoriteNovels()
    {
        List<long> ids = FavoritesManager.Instance().GetFavoritesIds();
        List<VisualNovel> favorites = new List<VisualNovel>();

        foreach (long id in ids)
        {
            if (id < 0)
            {
                favorites.Add(KiteNovelManager.GetKiteNovelById(id));
            }
            else
            {
                favorites.Add(sceneController.GetUserNovelById(id));
            }
        }
        return favorites;
    }

    public void ActivateTabByIndex(int index)
    {
        switch (index)
        {
            case 0:
                {
                    OnKiteNovelsButton();
                    return;
                }
            case 1:
                {
                    OnUserNovelsButton();
                    return;
                }
            case 2:
                {
                    OnAccountNovelsButton();
                    return;
                }
            case 3:
                {
                    OnFavoritNovelsButton();
                    return;
                }
            case 4:
                {
                    OnFilterNovelsButton();
                    radioButtonHandler.Init();
                    filter.GetComponent<SearchFilter>().OnSearchButton();
                    return;
                }
            default:
                {
                    OnKiteNovelsButton();
                    return;
                }
        }
    }

    public void SaveCurrentPosition()
    {
        switch (openGallery)
        {
            case GalleryType.KITE_GALLERY:
                {
                    kiteGaleryPosition = gallery.GetCurrentScrollPosition();
                    return;
                }
            case GalleryType.USER_GALLERY:
                {
                    userGaleryPosition = gallery.GetCurrentScrollPosition();
                    return;
                }
            case GalleryType.ACCOUNT_GALLERY:
                {
                    accountGaleryPosition = gallery.GetCurrentScrollPosition();
                    return;
                }
            case GalleryType.FAVORITES_GALLERY:
                {
                    favoritesGaleryPosition = gallery.GetCurrentScrollPosition();
                    return;
                }
            case GalleryType.FILTER_GALLERY:
                {
                    filterGaleryPosition = gallery.GetCurrentScrollPosition();
                    return;
                }
            default:
                {
                    return;
                }
        }
    }
}
