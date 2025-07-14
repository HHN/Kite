using Assets._Scripts.Managers;
using Assets._Scripts.Novel;
using Assets._Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UIElements.FoundersBubble
{
    public class BookmarkUpdater : MonoBehaviour
    {
        [SerializeField] private VisualNovelNames visualNovel;
        
        public VisualNovelNames VisualNovel
        {
            get => visualNovel;
            set => visualNovel = value;
        }

        private void Update()
        {
            this.gameObject.GetComponent<Image>().enabled =
                FavoritesManager.Instance().IsFavorite(VisualNovelNamesHelper.ToInt(visualNovel));
        }
    }
}