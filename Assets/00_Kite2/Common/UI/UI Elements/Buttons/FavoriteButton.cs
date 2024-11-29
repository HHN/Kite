using _00_Kite2.Common.Managers;
using UnityEngine;
using UnityEngine.UI;

public class FavoriteButton : MonoBehaviour
{
    public Sprite[] sprites;
    public bool isFavorite = false;
    public VisualNovel novel;

    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(delegate { OnClick(); });
        Init();
    }

    public void OnClick()
    {
        if (this.isFavorite)
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
        FavoritesManager.Instance().MarkAsFavorite(novel);
    }

    public void UnmarkAsFavorite()
    {
        this.gameObject.GetComponent<Button>().image.sprite = sprites[0];
        isFavorite = false;
        FavoritesManager.Instance().UnmarkAsFavorite(novel);
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
            this.gameObject.SetActive(false);
            return;
        }
        this.gameObject.GetComponent<Button>().image.sprite = sprites[0];
        isFavorite = false;

        if (FavoritesManager.Instance().IsFavorite(novel))
        {
            isFavorite = true;
            MarkAsFavorite();
        }
    }
}
