namespace Assets._Scripts.Novel.VisualNovelFormatter
{
    /// <summary>
    /// Represents a pair of words consisting of a target word to replace and a corresponding replacement value.
    /// This class is primarily used for substituting specific words in a given string,
    /// enabling dynamic content modifications within a visual novel context.
    /// </summary>
    public class WordPair
    {
        /// <summary>
        /// Represents a pair of words used in text replacement operations, where
        /// one word is specified as the word to be replaced, and the other is its replacement value.
        /// This class is useful for dynamic text manipulation.
        /// </summary>
        protected WordPair(string wordToReplace, string replaceByValue)
        {
            WordToReplace = wordToReplace;
            ReplaceByValue = replaceByValue;
        }

        /// <summary>
        /// Represents a pair of words consisting of a target word to be replaced and its corresponding replacement.
        /// This class facilitates text substitution for dynamic modifications in string-based operations.
        /// </summary>
        protected WordPair()
        {
            WordToReplace = "";
            ReplaceByValue = "";
        }

        /// <summary>
        /// Gets or sets the target word in a text that is intended to be replaced during string manipulation operations.
        /// This property represents the specific word to be identified in text processing tasks.
        /// </summary>
        public string WordToReplace { get; set; }

        /// <summary>
        /// Gets or sets the value that will replace the target word during string manipulation operations.
        /// This property specifies the replacement text in a word pair object, allowing for dynamic content modification.
        /// </summary>
        public string ReplaceByValue { get; set; }
    }
}