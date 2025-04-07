using Assets._Scripts.SceneControllers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._Scripts.SceneManagement
{
    public abstract class SceneLoader
    {
        public static void LoadMainMenuScene()
        {
            LoadScene(SceneNames.MainMenuScene);
        }

        public static void LoadPlayNovelScene()
        {
            // Überprüfen, ob ein Speicherstand für die nächste Szene existiert
            GameManager.Instance.CheckAndSetAllNovelsStatus();

            LoadScene(SceneNames.PlayNovelScene);
        }

        public static void LoadFeedbackScene()
        {
            LoadScene(SceneNames.FeedbackScene);
        }

        public static void LoadSettingsScene()
        {
            LoadScene(SceneNames.SettingsScene);
        }

        public static void LoadFoundersBubbleScene()
        {
            LoadScene(SceneNames.FoundersBubbleScene);
        }

        public static void LoadPlayInstructionScene()
        {
            LoadScene(SceneNames.PlayInstructionScene);
        }

        public static void LoadNovelHistoryScene()
        {
            LoadScene(SceneNames.NovelHistoryScene);
        }

        public static void LoadResourcesScene()
        {
            LoadScene(SceneNames.ResourcesScene);
        }

        public static void LoadAccessibilityScene()
        {
            LoadScene(SceneNames.AccessibilityScene);
        }

        public static void LoadPrivacyPolicyScene()
        {
            LoadScene(SceneNames.PrivacyPolicyScene);
        }

        public static void LoadLegalNoticeScene()
        {
            LoadScene(SceneNames.LegalNoticeScene);
        }

        public static void LoadTermsOfUseScene()
        {
            LoadScene(SceneNames.TermsOfUseScene);
        }

        public static void LoadBookmarkedNovelsScene()
        {
            LoadScene(SceneNames.BookmarkedNovelsScene);
        }

        public static void LoadSoundSettingsScene()
        {
            LoadScene(SceneNames.SoundSettingsScene);
        }
        
        public static void LoadKnowledgeScene()
        {
            LoadScene(SceneNames.KnowledgeScene);
        }

        public static void LoadScene(string sceneName)
        {
            GameObject oldSceneControllerGameObject = GameObject.Find("Controller");
            if (oldSceneControllerGameObject != null)
            {
                SceneController oldSceneController = oldSceneControllerGameObject.GetComponent<SceneController>();
                if (oldSceneController != null)
                {
                    oldSceneController.OnStop();
                }
            }

            SceneManager.LoadScene(sceneName);
        }
    }
}