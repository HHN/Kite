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

        public static void LoadPlayerPrefsScene()
        {
            LoadScene(SceneNames.PlayerPrefsScene);
        }

        public static void LoadFoundersBubbleScene()
        {
            LoadScene(SceneNames.FoundersBubbleScene);
        }

        public static void LoadFoundersWell2Scene()
        {
            LoadScene(SceneNames.FoundersWell2Scene);
        }

        public static void LoadPlayInstructionScene()
        {
            LoadScene(SceneNames.PlayInstructionScene);
        }

        public static void LoadNovelHistoryScene()
        {
            LoadScene(SceneNames.NovelHistoryScene);
        }

        public static void LoadRessourcenScene()
        {
            LoadScene(SceneNames.RessourcenScene);
        }

        public static void LoadBarrierefreiheitScene()
        {
            LoadScene(SceneNames.BarrierefreiheitScene);
        }

        public static void LoadDatenschutzScene()
        {
            LoadScene(SceneNames.DatenschutzScene);
        }

        public static void LoadImpressumScene()
        {
            LoadScene(SceneNames.ImpressumScene);
        }

        public static void LoadNutzungsbedingungenScene()
        {
            LoadScene(SceneNames.NutzungsbedingungenScene);
        }

        public static void LoadEinstellungenScene()
        {
            LoadScene(SceneNames.EinstellungenScene);
        }

        public static void LoadGemerkteNovelsScene()
        {
            LoadScene(SceneNames.GemerkteNovelsScene);
        }

        public static void SoundeinstellungScene()
        {
            LoadScene(SceneNames.SoundeinstellungScene);
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