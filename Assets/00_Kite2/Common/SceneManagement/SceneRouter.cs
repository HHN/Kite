using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRouter
{
    public static string GetTargetSceneForBackButton()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        switch (currentScene.name)
        {
            case (SceneNames.MAIN_MENU_SCENE):
                {
                    return SceneNames.MAIN_MENU_SCENE;
                }
            case (SceneNames.FOUNDERS_BUBBLE_SCENE):
                {
                    return SceneNames.MAIN_MENU_SCENE;
                }
            case (SceneNames.FOUNDERS_WELL_2_SCENE):
                {
                    return SceneNames.FOUNDERS_BUBBLE_SCENE;
                }
            case (SceneNames.PLAY_INSTRUCTION_SCENE):
                {
                    return SceneNames.FOUNDERS_BUBBLE_SCENE;
                }
            case (SceneNames.PLAY_NOVEL_SCENE):
                {
                    return SceneNames.FOUNDERS_BUBBLE_SCENE;
                }
            case (SceneNames.FEEDBACK_SCENE):
                {
                    return SceneNames.FOUNDERS_BUBBLE_SCENE;
                }
            case (SceneNames.NUTZUNGSBEDINGUNGEN_SCENE):
                {
                    return SceneNames.FOUNDERS_WELL_2_SCENE;
                }
            case (SceneNames.NOVEL_HISTORY_SCENE):
                {
                    return SceneNames.FOUNDERS_WELL_2_SCENE;
                }
            case (SceneNames.DATENSCHUTZ_SCENE):
                {
                    return SceneNames.FOUNDERS_WELL_2_SCENE;
                }
            case (SceneNames.IMPRESSUM_SCENE):
                {
                    return SceneNames.FOUNDERS_WELL_2_SCENE;
                }
            case (SceneNames.RESSOURCEN_SCENE):
                {
                    return SceneNames.FOUNDERS_WELL_2_SCENE;
                }
            case (SceneNames.BARRIEREFREIHEIT_SCENE):
                {
                    return SceneNames.FOUNDERS_WELL_2_SCENE;
                }            
            case (SceneNames.PLAYER_PREFS_SCENE):
                {
                    return SceneNames.DATENSCHUTZ_SCENE;
                }

            default: return SceneNames.MAIN_MENU_SCENE;
        }
    }

    public static string GetTargetSceneForCloseButton()
    {
        return SceneNames.MAIN_MENU_SCENE;
    }
}
