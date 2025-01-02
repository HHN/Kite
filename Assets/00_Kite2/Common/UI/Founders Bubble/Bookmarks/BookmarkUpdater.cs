using _00_Kite2.Common.Managers;
using _00_Kite2.Player;
using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.Common.UI.Founders_Bubble.Bookmarks
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