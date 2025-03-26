using System.Collections.Generic;
using Assets._Scripts.Player.Kite_Novels.Visual_Novel_Formatter;

namespace Assets._Scripts.Player.KiteNovels.VisualNovelFormatter
{
    public class KiteNovelMetaData
    {
        public long IdNumberOfNovel { get; set; } = 0;

        public string TitleOfNovel { get; set; } = "";

        public string DescriptionOfNovel { get; set; } = "";

        public string ContextForPrompt { get; set; } = "";

        public string StartTalkingPartnerEmotion { get; set; } = "";

        public string TalkingPartner01 { get; set; } = "";

        public string TalkingPartner02 { get; set; } = "";

        public string TalkingPartner03 { get; set; } = "";

        public List<WordPair> WordsToReplace { get; set; } = new();
    }
}