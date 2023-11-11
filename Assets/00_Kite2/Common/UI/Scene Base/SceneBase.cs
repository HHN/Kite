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
        string lastScene = SceneRouter.GetTargetSceneForBackButton();

        if (string.IsNullOrEmpty(lastScene))
        {
            SceneLoader.LoadMainMenuScene();
            return;
        }
        SceneLoader.LoadScene(lastScene);
    }

    public void OnCloseButton()
    {
        string lastScene = SceneRouter.GetTargetSceneForCloseButton();

        if (string.IsNullOrEmpty(lastScene))
        {
            SceneLoader.LoadMainMenuScene();
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
        SceneLoader.LoadNovelExplorerScene();
    }

    public void OnNovelMakerButton()
    {
        SceneLoader.LoadNovelMakerScene();
    }

    public void OnSettingsButton()
    {
        SceneLoader.LoadSettingsScene();
    }
}
