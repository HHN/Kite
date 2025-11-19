using System.Collections.Generic;

namespace Assets._Scripts.Novel.VisualNovelFormatter
{
    /// <summary>
    /// Represents a folder containing visual novel metadata and associated events.
    /// </summary>
    public class KiteNovelFolder
    {
        /// <summary>
        /// Represents a folder that contains metadata and an associated list of visual novel events.
        /// </summary>
        public KiteNovelFolder(KiteNovelMetaData novelMetaData, List<VisualNovelEvent> novelEventList)
        {
            NovelMetaData = novelMetaData;
            NovelEventList = novelEventList;
        }

        /// <summary>
        /// Gets or sets the metadata associated with the visual novel, encapsulating details such as title,
        /// description, color, character information, and other relevant metadata attributes.
        /// </summary>
        public KiteNovelMetaData NovelMetaData { get; set; }

        /// <summary>
        /// Gets or sets the list of events associated with the visual novel, where each event
        /// represents a specific interaction, narrative progression, or visual component within the story.
        /// </summary>
        public List<VisualNovelEvent> NovelEventList { get; set; }
    }
}