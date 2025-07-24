using System.Collections.Generic;

namespace Assets._Scripts.Novel.VisualNovelFormatter
{
    /// <summary>
    /// Represents a passage from a Twee file used in the context of a visual novel.
    /// It contains a label identifying the passage, the passage text itself,
    /// and a collection of links to other passages.
    /// </summary>
    public class TweePassage
    {
        /// <summary>
        /// Represents a passage in a Twee script, containing a label, the passage text,
        /// and a collection of links to other passages.
        /// </summary>
        /// <param name="label">The label identifying the passage.</param>
        /// <param name="passage">The full text content of the passage.</param>
        /// <param name="links">A collection of links to other passages. If null, an empty list will be created.</param>
        public TweePassage(string label, string passage, List<TweeLink> links)
        {
            Label = label;
            Passage = passage;
            Links = links ?? new List<TweeLink>();
        }

        /// <summary>
        /// Gets or sets the label that uniquely identifies the TweePassage.
        /// </summary>
        /// <remarks>
        /// The Label serves as a unique identifier for the passage.
        /// It is used to reference the passage within the visual novel structure and ensure proper navigation or linkage
        /// between passages, such as during the creation of events or handling of passage loops.
        /// </remarks>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the text content of the passage.
        /// </summary>
        /// <remarks>
        /// The Passage represents the main body of the text within a TweePassage. It can include narrative, descriptions, or dialogue
        /// relevant to the visual novel's storyline. This property is used to display the content of the passage
        /// and may also play a role in parsing or linking to other passages.
        /// </remarks>
        public string Passage { get; set; }

        /// <summary>
        /// Gets or sets the collection of links associated with the TweePassage.
        /// </summary>
        /// <remarks>
        /// The Links property represents the connections from the current passage to other passages within the visual novel.
        /// Each link provides navigation or branching functionality, enabling the flow between different parts of the visual novel structure.
        /// This property is populated upon the creation of the TweePassage and may be used to handle navigation or
        /// event generation within the visual novel system.
        /// </remarks>
        public List<TweeLink> Links { get; set; }
    }
}