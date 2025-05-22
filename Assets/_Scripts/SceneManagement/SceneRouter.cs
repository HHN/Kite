using UnityEngine.SceneManagement;

namespace Assets._Scripts.SceneManagement
{
    public abstract class SceneRouter
    {
        public static string GetTargetSceneForBackButton()
        {
            Scene currentScene = SceneManager.GetActiveScene();

            return currentScene.name switch
            {
                SceneNames.MainMenuScene => SceneNames.MainMenuScene,
                SceneNames.FoundersBubbleScene => SceneNames.MainMenuScene,
                SceneNames.PlayInstructionScene => SceneNames.FoundersBubbleScene,
                SceneNames.PlayNovelScene => SceneNames.FoundersBubbleScene,
                SceneNames.FeedbackScene => SceneNames.FoundersBubbleScene,
                SceneNames.TermsOfUseScene => SceneNames.SettingsScene,
                SceneNames.NovelHistoryScene => SceneNames.FoundersBubbleScene,
                SceneNames.PrivacyPolicyScene => SceneNames.SettingsScene,
                SceneNames.LegalNoticeScene => SceneNames.SettingsScene,
                SceneNames.ResourcesScene => SceneNames.FoundersBubbleScene,
                SceneNames.AccessibilityScene => SceneNames.SettingsScene,
                SceneNames.SettingsScene => SceneNames.FoundersBubbleScene,
                SceneNames.BookmarkedNovelsScene => SceneNames.FoundersBubbleScene,
                SceneNames.SoundSettingsScene => SceneNames.SettingsScene,
                SceneNames.KnowledgeScene => SceneNames.FoundersBubbleScene,
                SceneNames.LegalInformationScene => SceneNames.FoundersBubbleScene,
                _ => SceneNames.MainMenuScene
            };
        }

        public static string GetTargetSceneForCloseButton()
        {
            return SceneNames.MainMenuScene;
        }
    }
}