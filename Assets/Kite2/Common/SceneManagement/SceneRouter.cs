public class SceneRouter
{
    public static string GetTargetSceneForBackButton(string currentScene)
    {
        switch (currentScene)
        {
            case (SceneNames.GENERATE_NOVEL_SCENE): return SceneNames.MAIN_MENU_SCENE;
            case (SceneNames.CHARACTER_MAKER_SCENE): return SceneNames.GENERATE_NOVEL_SCENE;
            case (SceneNames.ENVIRONMENT_MAKER_SCENE): return SceneNames.CHARACTER_MAKER_SCENE;
            case (SceneNames.DIALOGUE_MAKER_SCENE): return SceneNames.ENVIRONMENT_MAKER_SCENE;
            case (SceneNames.SAVE_NOVEL_SCENE): return SceneNames.DIALOGUE_MAKER_SCENE;
            case (SceneNames.SELECT_NOVEL_SCENE): return SceneNames.MAIN_MENU_SCENE;
            case (SceneNames.PLAY_NOVEL_SCENE): return SceneNames.SELECT_NOVEL_SCENE;
            case (SceneNames.FEEDBACK_SCENE): return SceneNames.PLAY_NOVEL_SCENE;
            case (SceneNames.REGISTRATION_SCENE): return SceneNames.MAIN_MENU_SCENE;
            case (SceneNames.LOG_IN_SCENE): return SceneNames.MAIN_MENU_SCENE;
            case (SceneNames.RESET_PASSWORD_SCENE): return SceneNames.LOG_IN_SCENE;
            case (SceneNames.SETTINGS_SCENE): return SceneNames.MAIN_MENU_SCENE;
            case (SceneNames.CHANGE_PASSWORD_SCENE): return SceneNames.SETTINGS_SCENE;
            case (SceneNames.GALLERY_SCENE): return SceneNames.MAIN_MENU_SCENE;
            default: return "";
        }
    }

    public static string GetTargetSceneForCloseButton(string currentScene)
    {
        switch (currentScene)
        {
            case (SceneNames.GENERATE_NOVEL_SCENE): return SceneNames.MAIN_MENU_SCENE;
            case (SceneNames.CHARACTER_MAKER_SCENE): return SceneNames.MAIN_MENU_SCENE;
            case (SceneNames.ENVIRONMENT_MAKER_SCENE): return SceneNames.MAIN_MENU_SCENE;
            case (SceneNames.DIALOGUE_MAKER_SCENE): return SceneNames.MAIN_MENU_SCENE;
            case (SceneNames.SAVE_NOVEL_SCENE): return SceneNames.MAIN_MENU_SCENE;
            case (SceneNames.SELECT_NOVEL_SCENE): return SceneNames.MAIN_MENU_SCENE;
            case (SceneNames.PLAY_NOVEL_SCENE): return SceneNames.MAIN_MENU_SCENE;
            case (SceneNames.FEEDBACK_SCENE): return SceneNames.MAIN_MENU_SCENE;
            case (SceneNames.REGISTRATION_SCENE): return SceneNames.MAIN_MENU_SCENE;
            case (SceneNames.LOG_IN_SCENE): return SceneNames.MAIN_MENU_SCENE;
            case (SceneNames.RESET_PASSWORD_SCENE): return SceneNames.MAIN_MENU_SCENE;
            case (SceneNames.SETTINGS_SCENE): return SceneNames.MAIN_MENU_SCENE;
            case (SceneNames.CHANGE_PASSWORD_SCENE): return SceneNames.MAIN_MENU_SCENE;
            case (SceneNames.GALLERY_SCENE): return SceneNames.MAIN_MENU_SCENE;
            default: return "";
        }
    }
}
