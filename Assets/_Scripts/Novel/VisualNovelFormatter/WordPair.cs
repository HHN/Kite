namespace Assets._Scripts.Player.KiteNovels.VisualNovelFormatter
{
    public class WordPair
    {
        protected WordPair(string wordToReplace, string replaceByValue)
        {
            WordToReplace = wordToReplace;
            ReplaceByValue = replaceByValue;
        }

        protected WordPair()
        {
            WordToReplace = "";
            ReplaceByValue = "";
        }

        public string WordToReplace { get; set; }

        public string ReplaceByValue { get; set; }
    }
}