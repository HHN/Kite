using Assets._Scripts._Mappings;
using Assets._Scripts.Managers;
using Assets._Scripts.Novel;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UIElements.FoundersBubble
{
    /// <summary>
    /// Updates the visibility of a UI element (typically an image) to indicate
    /// whether a specific visual novel is bookmarked.
    /// </summary>
    public class BookmarkUpdater : MonoBehaviour
    {
        [SerializeField] private string visualNovel;
        
        /// <summary>
        /// Gets or sets the <see cref="VisualNovel"/> associated with this updater.
        /// </summary>
        public string VisualNovel
        {
            get => visualNovel;
            set => visualNovel = value;
        }

        /// <summary>
        /// Called once per frame.
        /// Updates the 'enabled' state of the attached <see cref="Image"/> component
        /// based on whether the assigned <see cref="visualNovel"/> is marked as a favorite
        /// in the <see cref="FavoritesManager"/>.
        /// </summary>
        private void Update()
        {
            long currentNovel = MappingManager.allNovels.Find(n => n.title == visualNovel).id;
            gameObject.GetComponent<Image>().enabled = FavoritesManager.Instance().IsFavorite(currentNovel);

        }
    }
}