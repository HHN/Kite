using System.Collections.Generic;

namespace Assets._Scripts.Novel.VisualNovelFormatter
{
    /// <summary>
    /// Represents a collection of visual novel titles.
    /// Provides methods to manage and access a list of visual novel names.
    /// </summary>
    public class KiteNovelList
    {
        /// <summary>
        /// Represents a list of visual novel titles.
        /// </summary>
        public KiteNovelList(List<string> visualNovels)
        {
            VisualNovels = visualNovels;
        }

        /// <summary>
        /// Represents a collection of visual novel titles.
        /// Provides methods to manage and access a list of visual novel names.
        /// </summary>
        public KiteNovelList()
        {
            VisualNovels = new List<string>();
        }

        /// <summary>
        /// Gets or sets the collection of visual novel titles.
        /// This property is used to store a list of visual novel names associated with the KiteNovelList or other related operations.
        /// </summary>
        public List<string> VisualNovels { get; set; }
    }
}