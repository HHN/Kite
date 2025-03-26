using Assets._Scripts.Utilities;

namespace Assets._Scripts.NovelData
{
    public class NovelMetaData
    {
        // Novel Data
        public long ID = System.DateTime.Now.Ticks; // Einzigartige ID generieren
        public string TitleOfNovel;
        public string DescriptionOfNovel; // große textfelder möglich?
        public string NameOfMainCharacter;
        public string ContextForPrompt; // große textfelder möglich?

        // Dialog Partners
        public FaceExpressions StartTalkingPartnerEmotion;
        public string[] TalkingPartners; // Mindestens 1 Element // Alle Felder müssen ausgefüllt sein

        public void SetTitleOfNovel(string title) => TitleOfNovel = title;
        public void SetDescriptionOfNovel(string description) => DescriptionOfNovel = description;
        public void SetNameOfMainCharacter(string characterName) => NameOfMainCharacter = characterName;
        public void SetContextForPrompt(string context) => ContextForPrompt = context;
        public void SetStartTalkingPartnerEmotion(FaceExpressions expression) => StartTalkingPartnerEmotion = expression;
        public void SetTalkingPartners(string[] newTalkingPartners) => TalkingPartners = newTalkingPartners;
    }
}