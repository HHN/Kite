using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static void LoadMainMenuScene() {
        LoadScene(SceneNames.MAIN_MENU_SCENE);
    }

    public static void LoadCharacterMakerScene() {
        LoadScene(SceneNames.CHARACTER_MAKER_SCENE);
    }

    public static void LoadDialogueMakerScene() {
        LoadScene(SceneNames.DIALOGUE_MAKER_SCENE);
    }

    public static void LoadEnvironmentMakerScene() {
        LoadScene(SceneNames.ENVIRONMENT_MAKER_SCENE);
    }

    public static void LoadGenerateNovelScene() {
        LoadScene(SceneNames.GENERATE_NOVEL_SCENE);
    }

    public static void LoadSaveNovelScene() {
        LoadScene(SceneNames.SAVE_NOVEL_SCENE);
    }

    public static void LoadPlayNovelScene() {
        LoadScene(SceneNames.PLAY_NOVEL_SCENE);
    }

    public static void LoadFeedbackScene() {
        LoadScene(SceneNames.FEEDBACK_SCENE);
    }

    public static void LoadSelectNovelScene() {
        LoadScene(SceneNames.SELECT_NOVEL_SCENE);
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

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
