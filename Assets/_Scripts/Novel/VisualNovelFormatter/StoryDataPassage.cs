namespace Assets._Scripts.Novel.VisualNovelFormatter
{
    /// <summary>
    /// Represents a passage of story data in a visual novel.
    /// </summary>
    public class StoryDataPassage
    {
        /// <summary>
        /// Represents a passage of story data in a visual novel.
        /// This class encapsulates the data related to a single story passage.
        /// </summary>
        public StoryDataPassage(string start)
        {
            Start = start;
        }

        /// <summary>
        /// Represents a passage of story data in a visual novel.
        /// This class encapsulates the data related to a single story passage.
        /// </summary>
        public StoryDataPassage()
        {
            Start = "";
        }

        /// <summary>
        /// Gets or sets the starting passage or label for a story data object in a visual novel.
        /// This property typically identifies the entry point of the story or the initial state.
        /// </summary>
        public string Start { get; set; }
    }
}