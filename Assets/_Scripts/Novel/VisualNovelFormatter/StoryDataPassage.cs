namespace Assets._Scripts.Novel.VisualNovelFormatter
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