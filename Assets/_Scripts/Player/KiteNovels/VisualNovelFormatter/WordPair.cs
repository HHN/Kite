namespace Assets._Scripts.Player.Kite_Novels.Visual_Novel_Formatter
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