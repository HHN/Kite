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

    [SerializeField] private GameObject leaveGameAndGoToMainMenuMessageBox;
    [SerializeField] private LeaveNovelAndGoBackToMainmenuMessageBox leaveGameAndGoToMainMenuMessageBoxObject;
    [SerializeField] private Canvas canvas;

    void Start()
    {
        backButton.onClick.AddListener(delegate { OnBackButton(); });
        closeButton.onClick.AddListener(delegate { OnCloseButton(); });
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

        if (SceneManager.GetActiveScene().name.Equals(SceneNames.PLAY_NOVEL_SCENE))
        {
            homeButton.onClick.AddListener(delegate { OnHomeButtonForNovelPlayer(); });
        } 
        else
        {
            homeButton.onClick.AddListener(delegate { OnHomeButton(); });
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

    public void OnHomeButtonForNovelPlayer()
    {
        if (!DestroyValidator.IsNullOrDestroyed(leaveGameAndGoToMainMenuMessageBoxObject))
        {
            leaveGameAndGoToMainMenuMessageBoxObject.CloseMessageBox();
        }
        if (DestroyValidator.IsNullOrDestroyed(canvas))
        {
            return;
        }
        leaveGameAndGoToMainMenuMessageBoxObject = null;
        leaveGameAndGoToMainMenuMessageBoxObject = Instantiate(leaveGameAndGoToMainMenuMessageBox, 
            canvas.transform).GetComponent<LeaveNovelAndGoBackToMainmenuMessageBox>();
        leaveGameAndGoToMainMenuMessageBoxObject.Activate();
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
