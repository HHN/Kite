using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    public static void LoadMainMenuScene() {
        LoadScene(SceneNames.MAIN_MENU_SCENE);
    }

    public static void LoadPlayNovelScene() {
        LoadScene(SceneNames.PLAY_NOVEL_SCENE);
    }

    public static void LoadFeedbackScene() {
        LoadScene(SceneNames.FEEDBACK_SCENE);
    }

    public static void LoadNovelExplorerScene() {
        LoadScene(SceneNames.NOVEL_EXPLORER_SCENE);
    }

    public static void LoadLogInScene() {
        LoadScene(SceneNames.LOG_IN_SCENE);
    }

    public static void LoadRegistrationScene() {
        LoadScene(SceneNames.REGISTRATION_SCENE);
    }

    public static void LoadResetPasswordScene() {
        LoadScene(SceneNames.RESET_PASSWORD_SCENE);
    }

    public static void LoadSettingsScene()
    {
        LoadScene(SceneNames.SETTINGS_SCENE);
    }

    public static void LoadChangePasswordSceneScene()
    {
        LoadScene(SceneNames.CHANGE_PASSWORD_SCENE);
    }

    public static void LoadDetailsViewScene()
    {
        LoadScene(SceneNames.DETAILS_VIEW_SCENE);
    }

    public static void LoadInfoScene()
    {
        LoadScene(SceneNames.INFO_SCENE);
    }

    public static void LoadCommentSectionScene()
    {
        LoadScene(SceneNames.COMMENT_SECTION_SCENE);
    }

    public static void LoadNovelMakerScene()
    {
        LoadScene(SceneNames.NOVEL_MAKER_SCENE);
    }

    public static void LoadCharacterExplorerScene()
    {
        LoadScene(SceneNames.CHARACTER_EXPLORER_SCENE);
    }

    public static void LoadEnvironmentExplorerScene()
    {
        LoadScene(SceneNames.ENVIRONMENT_EXPLORER_SCENE);
    }

    public static void LoadFinishNovelScene()
    {
        LoadScene(SceneNames.FINISH_NOVEL_SCENE);
    }

    public static void LoadHelpForNovelMakerScene()
    {
        LoadScene(SceneNames.HELP_FOR_NOVEL_MAKER_SCENE);
    }

    public static void LoadNovelPreviewScene()
    {
        LoadScene(SceneNames.NOVEL_PREVIEW_SCENE);
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
