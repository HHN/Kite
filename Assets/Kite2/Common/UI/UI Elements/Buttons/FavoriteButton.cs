using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FavoriteButton : MonoBehaviour
{
    public Sprite[] sprites;
    public bool isFavorite = false;
    public VisualNovel novel;
    public VisualNovelGallery favoritesGallery;

    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(delegate { OnClick(); });
    }

    public void OnClick()
    {
        if (this.isFavorite)
        {
            UnmarkAsFavorite();
        } else
        {
            MarkAsFavorite();
        }
    }

    public void MarkAsFavorite()
    {
        this.gameObject.GetComponent<Button>().image.sprite = sprites[1];
        isFavorite = true;
        FavoritesManager.MarkAsFavorite(novel);

        if (favoritesGallery != null)
        {
            favoritesGallery.AddNovelToFavoritesWithRealtimeUpdate(novel);
        }
    }

    public void UnmarkAsFavorite()
    {
        this.gameObject.GetComponent<Button>().image.sprite = sprites[0];
        isFavorite = false;
        FavoritesManager.UnmarkAsFavorite(novel);


        if (favoritesGallery != null)
        {
            favoritesGallery.RemoveNovelFromFavoritesWithRealitmeUpdate(novel);
        }
    }

    public void Init()
    {
        if (novel.id == 0)
        {
            this.gameObject.SetActive(false);
            return;
        } else
        {
            this.gameObject.SetActive(true);
        }
        this.gameObject.GetComponent<Button>().image.sprite = sprites[0];
        isFavorite = false;

        if (FavoritesManager.IsFavorite(novel))
        {
            isFavorite = true;
            MarkAsFavorite();
        }
    }
}
