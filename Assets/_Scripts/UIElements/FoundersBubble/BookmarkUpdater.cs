using Assets._Scripts.Managers;
using Assets._Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UI_Elements.Founders_Bubble
{
    public class BookmarkUpdater : MonoBehaviour
    {
        [SerializeField] private VisualNovelNames visualNovel;

        private void Update()
        {
            this.gameObject.GetComponent<Image>().enabled =
                FavoritesManager.Instance().IsFavorite(VisualNovelNamesHelper.ToInt(visualNovel));
        }
    }
}