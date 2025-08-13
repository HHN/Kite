using System.Collections.Generic;

namespace Assets._Scripts.Novel.VisualNovelFormatter
{
    /// <summary>
    /// Represents the metadata information of a visual novel within the Kite Novel system.
    /// This class stores key properties and descriptors for a visual novel, including its title,
    /// description, character details, and configurable parameters used in processing.
    /// </summary>
    public class KiteNovelMetaData
    {
        /// <summary>
        /// Gets or sets the unique identifier number of the novel within the Kite Novel system.
        /// This property is used to distinguish individual novels and is integral for proper processing
        /// and organization of novel metadata.
        /// </summary>
        public long IdNumberOfNovel { get; set; } = 0;

        /// <summary>
        /// Gets or sets the title of the novel as defined within the Kite Novel system.
        /// This property represents the primary name or identifier for the novel,
        /// used in various processes including display, organization, and system operations.
        /// </summary>
        public string TitleOfNovel { get; set; } = "";

        /// <summary>
        /// Gets or sets the designation or subtitle of the novel within the Kite Novel system.
        /// This property provides an additional descriptive or identifying label for the novel,
        /// often used to convey supplemental information about its theme or series.
        /// </summary>
        public string DesignationOfNovel { get; set; } = "";

        /// <summary>
        /// Gets or sets the description of the visual novel.
        /// This property provides a textual summary or narrative about the novel,
        /// which helps to convey its theme, setting, and overall storyline within the Kite Novel system.
        /// </summary>
        public string DescriptionOfNovel { get; set; } = "";

        /// <summary>
        /// Gets or sets the context used for generating prompts related to the visual novel.
        /// This property provides supplementary background or framing details necessary
        /// to ensure prompt relevance and accuracy during novel processing.
        /// </summary>
        public string ContextForPrompt { get; set; } = "";

        /// <summary>
        /// Gets or sets the initial expression for the first talking partner in the visual novel.
        /// This property determines the facial expression or mood conveyed by the character at the beginning of the storyline,
        /// aiding in character portrayal and mood-setting within the Kite Novel system.
        /// </summary>
        public string StartTalkingPartnerExpression { get; set; } = "";

        /// <summary>
        /// Gets or sets the name of the primary talking partner associated with the novel.
        /// This property defines the identity of the first character involved in dialogue,
        /// assisting in parsing and organizing character interactions within the novel.
        /// </summary>
        public string TalkingPartner01 { get; set; } = "";

        /// <summary>
        /// Gets or sets the name of the second talking partner in the novel.
        /// This property is used to define and associate a secondary character who is involved
        /// in dialogue or interactions within the visual novel storyline.
        /// </summary>
        public string TalkingPartner02 { get; set; } = "";

        /// <summary>
        /// Gets or sets the name of the third talking partner associated with the novel.
        /// This property is utilized for defining and managing the dialogue interactions of the third character
        /// within the novel's metadata and narrative structure.
        /// </summary>
        public string TalkingPartner03 { get; set; } = "";

        /// <summary>
        /// Gets or sets a value indicating whether the current novel is associated with the Kite Novel system.
        /// This property is used to differentiate Kite Novels from other types of content
        /// and ensures proper handling during processes such as conversion and metadata validation.
        /// </summary>
        public bool IsKiteNovel { get; set; } = true;

        /// <summary>
        /// Gets or sets the color representation of the novel in a hexadecimal string format.
        /// This property is used to define the visual appearance or theme color associated
        /// with the novel for display purposes. If not specified, a default color of black (`#000000`)
        /// is applied.
        /// </summary>
        public string NovelColor { get; set; } = "#000000";
        
        /// <summary>
        /// Gets or sets the color representation of the novel frame in a hexadecimal string format.
        /// This property is used to define the visual appearance or theme color associated
        /// with the novel frame for display purposes. If not specified, a default color of black (`#000000`)
        /// is applied.
        /// </summary>
        public string NovelFrameColor { get; set; } = "#000000";

        /// <summary>
        /// Gets or sets the list of word replacement rules for the visual novel.
        /// Each rule specifies a pair of words, consisting of a target word and its replacement value,
        /// which are applied to the novel's text content during processing.
        /// </summary>
        public List<WordPair> WordsToReplace { get; set; } = new();
    }
}