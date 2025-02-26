using Assets._Scripts.Common.Managers;
using Assets._Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Common.UI.Founders_Bubble.Bookmarks
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