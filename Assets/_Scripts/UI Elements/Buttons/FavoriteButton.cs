using Assets._Scripts.Managers;
using Assets._Scripts.Novels;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UI_Elements.Buttons
{
    public class FavoriteButton : MonoBehaviour
    {
        public Sprite[] sprites;
        public bool isFavorite;
        public VisualNovel novel;

        private void Start()
        {
            this.gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
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

        private void MarkAsFavorite()
        {
            this.gameObject.GetComponent<Button>().image.sprite = sprites[1];
            isFavorite = true;
            FavoritesManager.Instance().MarkAsFavorite(novel);
        }

        private void UnmarkAsFavorite()
        {
            this.gameObject.GetComponent<Button>().image.sprite = sprites[0];
            isFavorite = false;
            FavoritesManager.Instance().UnmarkAsFavorite(novel);
        }

        private void Init()
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
}