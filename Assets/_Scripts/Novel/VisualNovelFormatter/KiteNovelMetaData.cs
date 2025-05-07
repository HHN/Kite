using System.Collections.Generic;
using Assets._Scripts.Player.KiteNovels.VisualNovelFormatter;
using UnityEngine;

namespace Assets._Scripts.Novel.VisualNovelFormatter
{
    public class KiteNovelMetaData
    {
        public long IdNumberOfNovel { get; set; } = 0;

        public string TitleOfNovel { get; set; } = "";

        public string DescriptionOfNovel { get; set; } = "";

        public string ContextForPrompt { get; set; } = "";

        public string StartTalkingPartnerExpression { get; set; } = "";

        public string TalkingPartner01 { get; set; } = "";

        public string TalkingPartner02 { get; set; } = "";

        public string TalkingPartner03 { get; set; } = "";
        public bool IsKiteNovel { get; set; } = true;
        public string NovelColor { get; set; } = "#000000";

        public List<WordPair> WordsToReplace { get; set; } = new();
    }
}