namespace Assets._Scripts.Player.KiteNovels.VisualNovelFormatter
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