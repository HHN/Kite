using System.Collections.Generic;

namespace _00_Kite2.Player.Kite_Novels.Visual_Novel_Formatter
{
    public class TweePassage
    {
        public TweePassage(string label, string passage, List<TweeLink> links)
        {
            this.Label = label;
            this.Passage = passage;
            this.Links = links ?? new List<TweeLink>();
        }

        public TweePassage()
        {
            this.Label = "";
            this.Passage = "";
            this.Links = new List<TweeLink>();
        }

        public string Label { get; set; }

        public string Passage { get; set; }

        public List<TweeLink> Links { get; set; }
    }
}