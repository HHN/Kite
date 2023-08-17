using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
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

    /**
    public static void LoadScene(string sceneName)
    {
        GameObject oldSceneControllerGameObjectObject = GameObject.Find("Controller");
        SceneController oldSceneController = oldSceneControllerGameObjectObject.GetComponent<SceneController>();
        oldSceneController.OnStop();

        SceneManager.LoadScene(sceneName);

        GameObject newSceneControllerGameObjectObject = GameObject.Find("Controller");
        SceneController newSceneController = newSceneControllerGameObjectObject.GetComponent<SceneController>();
        newSceneController.OnStart();
    }
    */

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

        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneName);
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject newSceneControllerGameObject = GameObject.Find("Controller");
        if (newSceneControllerGameObject != null)
        {
            SceneController newSceneController = newSceneControllerGameObject.GetComponent<SceneController>();
            if (newSceneController != null)
            {
                newSceneController.OnStart();
            }
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
