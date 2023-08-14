using System.Collections.Generic;
using UnityEngine;

public class NovelProvider : MonoBehaviour, OnSuccessHandler
{
    public Sprite[] smalNovelSprites;
    public Sprite[] bigNovelSprites;
    public GameObject allNovelsView;
    public GameObject detailsView;
    public bool isDisplayingDetails = false;
    public List<VisualNovel> userNovels;
    public GameObject FindVisualNovelServerCall;
    public VisualNovelGallery gallery;
    public SceneController sceneController;

    // 400 * 400 sprites for representation next to each other.
    public Sprite FindSmalSpriteById(long id)
    {
        if (smalNovelSprites.Length <= id)
        {
            return null;
        }
        return smalNovelSprites[id];
    }

    // 800 * 800 sprites for displaying it on big view.
    public Sprite FindBigSpriteById(long id)
    {
        if (bigNovelSprites.Length <= id)
        {
            return null;
        }
        return bigNovelSprites[id];
    }

    public List<VisualNovel> GetKiteNovels()
    {
        return KiteNovelManager.GetAllKiteNovels();
    }

    public List<VisualNovel> GetUserNovels()
    {
        return userNovels;
    }

    public List<VisualNovel> GetAccountNovels()
    {
        return new List<VisualNovel>
        {
            new ConversationWithAcquaintancesNovel() { id = 0},
            new BankAppointmentNovel(),
            new FeeNegotiationNovel() { id = 0},
        };
    }

    public List<VisualNovel> GetFavoriteNovels(VisualNovelGallery gallery)
    {
        this.gallery = gallery;
        List<long> ids = FavoritesManager.GetFavoritesIds();
        List<VisualNovel> result = new List<VisualNovel>();

        foreach (long id in ids)
        {
            if (id < 0)
            {
                result.Add(KiteNovelManager.GetKiteNovelById(id));
            } 
            else
            {
                FindNovelServerCall call = Instantiate(FindVisualNovelServerCall).GetComponent<FindNovelServerCall>();
                call.sceneController = sceneController;
                call.onSuccessHandler = this;
                call.SendRequest();
            }
        }
        return result;
    }

    public void ShowDetailsViewWithNovel(VisualNovel novel)
    {
        isDisplayingDetails = true;
        DetailsView view = detailsView.GetComponent<DetailsView>();
        view.novelToDisplay = novel;
        view.Initialize();
        allNovelsView.SetActive(false);
        detailsView.SetActive(true);
    }

    public void ShowAllNovelsView()
    {
        isDisplayingDetails = false;
        detailsView.SetActive(false);
        allNovelsView.SetActive(true);
    }

    public void OnSuccess(Response response)
    {
        gallery.AddNovelToGallery(response.specifiedNovel);
    }
}
