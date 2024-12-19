namespace _00_Kite2.Player.Kite_Novels.Visual_Novel_Formatter
{
    public abstract class WordPair
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