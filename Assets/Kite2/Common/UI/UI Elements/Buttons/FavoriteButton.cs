using UnityEngine;
using UnityEngine.UI;

public class FavoriteButton : MonoBehaviour
{
    public Sprite[] sprites;
    public bool isFavorite = false;
    public VisualNovel novel;
    public VisualNovelGallery favoritesGallery;
    public bool hasTransformedIntoEditButton = false;

    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(delegate { OnClick(); });
        Init();
    }

    public void OnClick()
    {
        if (hasTransformedIntoEditButton)
        {
            // Start Editing.
        }
        else if (this.isFavorite)
        {
            UnmarkAsFavorite();
        } 
        else
        {
            MarkAsFavorite();
        }
    }

    public void MarkAsFavorite()
    {
        this.gameObject.GetComponent<Button>().image.sprite = sprites[1];
        isFavorite = true;
        FavoritesManager.MarkAsFavorite(novel);
    }

    public void UnmarkAsFavorite()
    {
        this.gameObject.GetComponent<Button>().image.sprite = sprites[0];
        isFavorite = false;
        FavoritesManager.UnmarkAsFavorite(novel);
    }

    public void TransformIntoEditButton()
    {
        this.gameObject.GetComponent<Button>().image.sprite = sprites[2];
        hasTransformedIntoEditButton = true;
    }

    public void Init()
    {
        novel = PlayManager.Instance().GetVisualNovelToPlay();
        if (novel == null)
        {
            return;
        }
        if (novel.id == 0)
        {
            TransformIntoEditButton();
            return;
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
