using System.Collections.Generic;

namespace Assets._Scripts.Novels.Visual_Novel_Formatter
{
    public class KiteNovelMetaData
    {
        public long IdNumberOfNovel { get; set; } = 0;

        public string TitleOfNovel { get; set; } = "";

        public string DescriptionOfNovel { get; set; } = "";

        public long IdNumberOfRepresentationImage { get; set; } = 0;

        public string ContextForPrompt { get; set; } = "";

        public bool IsKite2Novel { get; set; } = false;

        public string StartLocation { get; set; } = "";

        public bool IsWithStartValues { get; set; } = false;

        public string StartTalkingPartnerEmotion { get; set; } = "";

        public string TalkingPartner01 { get; set; } = "";

        public string TalkingPartner02 { get; set; } = "";

        public string TalkingPartner03 { get; set; } = "";

        public List<WordPair> WordsToReplace { get; set; } = new();
    }
}