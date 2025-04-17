namespace Assets._Scripts.Novel.VisualNovelFormatter
{
    public class TweeLink
    {
        public TweeLink(string text, string target, bool showAfterSelection)
        {
            Text = text;
            Target = target;
            ShowAfterSelection = showAfterSelection;
        }

        public string Text { get; set; }

        public string Target { get; set; }

        public bool ShowAfterSelection { get; set; }
    }
}