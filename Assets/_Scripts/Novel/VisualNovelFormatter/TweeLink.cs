namespace Assets._Scripts.Novel.VisualNovelFormatter
{
    /// <summary>
    /// Represents a navigational link within a Twee passage.
    /// </summary>
    public class TweeLink
    {
        /// <summary>
        /// Represents a navigational link within a Twee passage, containing the text to be displayed,
        /// the target it points to, and whether the link should be shown after it has been selected.
        /// </summary>
        /// <param name="text">The display text associated with the Twee navigational link.</param>
        /// <param name="target">The identifier of the target passage or section to navigate to.</param>
        /// <param name="showAfterSelection">Whether the link should be displayed after selection.</param>
        public TweeLink(string text, string target, bool showAfterSelection)
        {
            Text = text;
            Target = target;
            ShowAfterSelection = showAfterSelection;
        }

        /// <summary>
        /// Gets or sets the display text associated with a Twee navigational link,
        /// typically describing the option shown to the user in a visual novel passage.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the target passage or section
        /// to navigate to within the visual novel's structure.
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the link should be displayed to the user only after it has been selected.
        /// </summary>
        public bool ShowAfterSelection { get; set; }
    }
}