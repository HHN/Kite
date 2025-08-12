namespace Assets._Scripts.SceneManagement
{
    /// <summary>
    /// A static abstract class that defines constant string names for all scenes
    /// used within the application. This centralizes scene name management,
    /// reducing the chance of typos and making scene references consistent.
    /// </summary>
    public abstract class SceneNames
    {
        // GENERAL SCENES
        public const string MainMenuScene = "MainMenuScene";
        public const string SettingsScene = "SettingsScene";

        // NOVEL-RELATED SCENES
        public const string PlayNovelScene = "PlayNovelScene";
        public const string PlayInstructionScene = "PlayInstructionScene";
        public const string NovelHistoryScene = "NovelHistoryScene";
        
        // FEEDBACK SYSTEM
        public const string FeedbackScene = "FeedbackScene";

        // FOUNDER-RELATED SCENES
        public const string FoundersBubbleScene = "FoundersBubbleTestScene";

        // LEGAL INFORMATION
        public const string LegalInformationScene = "LegalInformationScene";
        public const string LegalNoticeScene = "LegalNoticeScene";
        public const string PrivacyPolicyScene = "PrivacyPolicyScene";
        public const string TermsOfUseScene = "TermsOfUseScene";

        // OTHER PAGES
        public const string AccessibilityScene = "AccessibilityScene";
        public const string BookmarkedNovelsScene = "BookmarkedNovelsScene";
        public const string ResourcesScene = "ResourcesScene";
        public const string SoundSettingsScene = "SoundSettingsScene";
        public const string KnowledgeScene = "KnowledgeScene";
    }
}