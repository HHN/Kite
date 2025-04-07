namespace Assets._Scripts.Novel.VisualNovelFormatter
{
    public class TweeLink
    {
        public TweeLink(string text, string target, bool showAfterSelection)
        {
            this.Text = text;
            this.Target = target;
            this.ShowAfterSelection = showAfterSelection;
        }

        public TweeLink()
        {
            this.Text = "";
            this.Target = "";
            this.ShowAfterSelection = false;
        }

        public string Text { get; set; }

        public string Target { get; set; }

        public bool ShowAfterSelection { get; set; }
    }
}