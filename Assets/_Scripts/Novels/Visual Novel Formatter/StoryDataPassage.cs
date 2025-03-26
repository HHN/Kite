namespace Assets._Scripts.Novels.Visual_Novel_Formatter
{
    public class StoryDataPassage
    {
        public StoryDataPassage(string start)
        {
            this.Start = start;
        }

        public StoryDataPassage()
        {
            this.Start = "";
        }

        public string Start { get; set; }
    }
}