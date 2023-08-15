using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        OnKiteNovelsButton();
        kiteNovelsButton.onClick.AddListener(delegate { OnKiteNovelsButton(); });
        userNovelsButton.onClick.AddListener(delegate { OnUserNovelsButton(); });
        accountNovelsButton.onClick.AddListener(delegate { OnAccountNovelsButton(); });
        favoriteNovelsButton.onClick.AddListener(delegate { OnFavoritNovelsButton(); });
        filterNovelsButton.onClick.AddListener(delegate { OnFilterNovelsButton(); });
    }

    public void OnKiteNovelsButton()
    {
        kiteNovelsUnterline.gameObject.SetActive(true);
        userNovelsUnterline.gameObject.SetActive(false);
        accountNovelsUnterline.gameObject.SetActive(false);
        favoriteNovelsUnterline.gameObject.SetActive(false);
        filterNovelsUnterline.gameObject.SetActive(false);
        contentInfoText.SetText("KITE NOVELS");

        List<VisualNovel> visualNovels = KiteNovelManager.GetAllKiteNovels();
        gallery.RemoveAll();
        gallery.AddNovelsToGallery(visualNovels);
    }

    public void OnUserNovelsButton()
    {
        kiteNovelsUnterline.gameObject.SetActive(false);
        userNovelsUnterline.gameObject.SetActive(true);
        accountNovelsUnterline.gameObject.SetActive(false);
        favoriteNovelsUnterline.gameObject.SetActive(false);
        filterNovelsUnterline.gameObject.SetActive(false);
        contentInfoText.SetText("USER NOVELS");

        List<VisualNovel> visualNovels = sceneController.userNovels;
        gallery.RemoveAll();
        gallery.AddNovelsToGallery(visualNovels);
    }

    public void OnAccountNovelsButton()
    {
        kiteNovelsUnterline.gameObject.SetActive(false);
        userNovelsUnterline.gameObject.SetActive(false);
        accountNovelsUnterline.gameObject.SetActive(true);
        favoriteNovelsUnterline.gameObject.SetActive(false);
        filterNovelsUnterline.gameObject.SetActive(false);
        contentInfoText.SetText("EIGENE NOVELS");

        List<VisualNovel> visualNovels = AccountNovelManager.GetAllAccountNovels();
        gallery.RemoveAll();
        gallery.AddNovelsToGallery(visualNovels);
    }

    public void OnFavoritNovelsButton()
    {
        kiteNovelsUnterline.gameObject.SetActive(false);
        userNovelsUnterline.gameObject.SetActive(false);
        accountNovelsUnterline.gameObject.SetActive(false);
        favoriteNovelsUnterline.gameObject.SetActive(true);
        filterNovelsUnterline.gameObject.SetActive(false);
        contentInfoText.SetText("FAVORITEN");

        List<VisualNovel> visualNovels = GetFavoriteNovels();
        gallery.RemoveAll();
        gallery.AddNovelsToGallery(visualNovels);
    }

    public void OnFilterNovelsButton()
    {
        kiteNovelsUnterline.gameObject.SetActive(false);
        userNovelsUnterline.gameObject.SetActive(false);
        accountNovelsUnterline.gameObject.SetActive(false);
        favoriteNovelsUnterline.gameObject.SetActive(false);
        filterNovelsUnterline.gameObject.SetActive(true);
        contentInfoText.SetText("FILTER SUCHE");

        List<VisualNovel> visualNovels = new List<VisualNovel>();
        gallery.RemoveAll();
        gallery.AddNovelsToGallery(visualNovels);
    }

    private List<VisualNovel> GetFavoriteNovels()
    {
        List<long> ids = FavoritesManager.GetFavoritesIds();
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
}
