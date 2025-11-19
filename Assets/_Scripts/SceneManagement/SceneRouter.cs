using UnityEngine.SceneManagement;

namespace Assets._Scripts.SceneManagement
{
    /// <summary>
    /// Provides static utility methods for routing scene navigation, specifically
    /// determining the target scene when a "back" or "close" action is performed.
    /// This helps manage the flow of scenes within the application.
    /// </summary>
    public abstract class SceneRouter
    {
        /// <summary>
        /// Determines the appropriate target scene to navigate to when a "back" button is pressed,
        /// based on the currently active scene. This creates a specific, non-linear navigation path.
        /// </summary>
        /// <returns>The string name of the target scene for a "back" action.</returns>
        public static string GetTargetSceneForBackButton()
        {
            Scene currentScene = SceneManager.GetActiveScene();

            return currentScene.name switch
            {
                SceneNames.MainMenuScene => SceneNames.MainMenuScene, // Stays on the Main Menu if already there.
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
                _ => SceneNames.MainMenuScene // Default fallback to the Main Menu for any unhandled scene.
            };
        }
    }
}