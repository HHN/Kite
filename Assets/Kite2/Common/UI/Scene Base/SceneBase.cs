using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneBase : MonoBehaviour
{
    public Button backButton;
    public Button closeButton;
    public Button homeButton;
    public Button novelPlayerButton;
    public Button novelMakerButton;
    public Button settingsButton;
    public SceneController sceneController;

    void Start()
    {
        backButton.onClick.AddListener(delegate { OnBackButton(); });
        closeButton.onClick.AddListener(delegate { OnCloseButton(); });
        homeButton.onClick.AddListener(delegate { OnHomeButton(); });
        novelPlayerButton.onClick.AddListener(delegate { OnNovelPlayerButton(); });
        novelMakerButton.onClick.AddListener(delegate { OnNovelMakerButton(); });
        settingsButton.onClick.AddListener(delegate { OnSettingsButton(); });
    }

    public void OnBackButton()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        string lastScene = SceneRouter.GetTargetSceneForBackButton(currentScene);

        if (currentScene == SceneNames.SELECT_NOVEL_SCENE)
        {
            SelectNovelSceneController selectNovelSceneController = (SelectNovelSceneController) sceneController;
            if (selectNovelSceneController.isDisplayingDetails)
            {
                selectNovelSceneController.ShowAllNovelsView();
                return;
            }
        }
        if (lastScene == "")
        {
            return;
        }
        SceneLoader.LoadScene(lastScene);
    }

    public void OnCloseButton()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        string lastScene = SceneRouter.GetTargetSceneForCloseButton(currentScene);

        if (lastScene == "")
        {
            return;
        }
        SceneLoader.LoadScene(lastScene);
    }

    public void OnHomeButton()
    {
        SceneLoader.LoadMainMenuScene();
    }

    public void OnNovelPlayerButton()
    {
        SceneLoader.LoadSelectNovelScene();
    }

    public void OnNovelMakerButton()
    {
        SceneLoader.LoadGenerateNovelScene();
    }

    public void OnSettingsButton()
    {
        SceneLoader.LoadSettingsScene();
    }
}
