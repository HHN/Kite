using UnityEngine.SceneManagement;

namespace Assets._Scripts.SceneManagement
{
    public abstract class SceneRouter
    {
        public static string GetTargetSceneForBackButton()
        {
            Scene currentScene = SceneManager.GetActiveScene();

            switch (currentScene.name)
            {
                case (SceneNames.MainMenuScene):
                {
                    return SceneNames.MainMenuScene;
                }
                case (SceneNames.FoundersBubbleScene):
                {
                    return SceneNames.MainMenuScene;
                }
                case (SceneNames.FoundersWell2Scene):
                {
                    return SceneNames.FoundersBubbleScene;
                }
                case (SceneNames.PlayInstructionScene):
                {
                    return SceneNames.FoundersBubbleScene;
                }
                case (SceneNames.PlayNovelScene):
                {
                    return SceneNames.FoundersBubbleScene;
                }
                case (SceneNames.FeedbackScene):
                {
                    return SceneNames.FoundersBubbleScene;
                }
                case (SceneNames.NutzungsbedingungenScene):
                {
                    return SceneNames.EinstellungenScene;
                }
                case (SceneNames.NovelHistoryScene):
                {
                    return SceneNames.FoundersBubbleScene;
                }
                case (SceneNames.DatenschutzScene):
                {
                    return SceneNames.EinstellungenScene;
                }
                case (SceneNames.ImpressumScene):
                {
                    return SceneNames.EinstellungenScene;
                }
                case (SceneNames.RessourcenScene):
                {
                    return SceneNames.FoundersBubbleScene;
                }
                case (SceneNames.BarrierefreiheitScene):
                {
                    return SceneNames.EinstellungenScene;
                }
                case (SceneNames.PlayerPrefsScene):
                {
                    return SceneNames.DatenschutzScene;
                }
                case (SceneNames.EinstellungenScene):
                {
                    return SceneNames.FoundersBubbleScene;
                }
                case (SceneNames.GemerkteNovelsScene):
                {
                    return SceneNames.FoundersBubbleScene;
                }
                case (SceneNames.SoundeinstellungScene):
                {
                    return SceneNames.EinstellungenScene;
                }
                case (SceneNames.KnowledgeScene):
                {
                    return SceneNames.FoundersBubbleScene;
                }

                default: return SceneNames.MainMenuScene;
            }
        }

        public static string GetTargetSceneForCloseButton()
        {
            return SceneNames.MainMenuScene;
        }
    }
}