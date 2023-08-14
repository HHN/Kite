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
    public NovelProvider novelProvider;

    void Start()
    {
        backButton.onClick.AddListener(delegate { OnBackButton(); });
        closeButton.onClick.AddListener(delegate { OnCloseButton(); });
        homeButton.onClick.AddListener(delegate { OnHomeButton(); });
        novelPlayerButton.onClick.AddListener(delegate { OnNovelPlayerButton(); });
        novelMakerButton.onClick.AddListener(delegate { OnNovelMakerButton(); });
        settingsButton.onClick.AddListener(delegate { OnSettingsButton(); });

        if (GameManager.Instance().applicationMode == ApplicationModes.GUEST_MODE)
        {
            OnGuestMode();
        }
        else
        {
            OnLoggedInUserMode();
        }
    }

    public void OnGuestMode()
    {
    }

    public void OnLoggedInUserMode()
    {
    }

    public void OnBackButton()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        string lastScene = SceneRouter.GetTargetSceneForBackButton(currentScene);

        if (currentScene == SceneNames.SELECT_NOVEL_SCENE || currentScene == SceneNames.GALLERY_SCENE)
        {
            if (novelProvider.isDisplayingDetails)
            {
                novelProvider.ShowAllNovelsView();
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
