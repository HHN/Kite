using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneBase : MonoBehaviour
{
    public Button backButton;
    public Button closeButton;
    public Button homeButton;
    public Button novelPlayerButton;
    public Button settingsButton;

    [SerializeField] private GameObject leaveGameAndGoToMainMenuMessageBox;
    [SerializeField] private GameObject leaveGameAndGoToSettingsMessageBox;
    [SerializeField] private GameObject leaveGameAndGoBackMessageBox;
    [SerializeField] private LeaveNovelAndGoBackToMainmenuMessageBox leaveGameAndGoToMainMenuMessageBoxObject;
    [SerializeField] private LeaveNovelAndGoToSettingsMessageBox leaveGameAndGoToSettingsMessageBoxObject;
    [SerializeField] private LeaveNovelAndGoBackMessageBox leaveGameAndGoBackMessageBoxObject;
    [SerializeField] private Canvas canvas;

    void Start()
    {
        closeButton.onClick.AddListener(delegate { OnCloseButton(); });
        novelPlayerButton.onClick.AddListener(delegate { OnNovelPlayerButton(); });

        OnGuestMode();

        if (SceneManager.GetActiveScene().name.Equals(SceneNames.PLAY_NOVEL_SCENE))
        {
            homeButton.onClick.AddListener(delegate { OnHomeButtonForNovelPlayer(); });
            settingsButton.onClick.AddListener(delegate { OnSettingsButtonForNovelPlayer(); });
            backButton.onClick.AddListener(delegate {OnBackButtonForNovelPlayer(); });
        } 
        else
        {
            homeButton.onClick.AddListener(delegate { OnHomeButton(); });
            settingsButton.onClick.AddListener(delegate { OnSettingsButton(); });
            backButton.onClick.AddListener(delegate { OnBackButton(); });
        }
    }

    public void OnGuestMode()
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

    public void OnBackButtonForNovelPlayer()
    {
        if (!DestroyValidator.IsNullOrDestroyed(leaveGameAndGoBackMessageBoxObject))
        {
            leaveGameAndGoBackMessageBoxObject.CloseMessageBox();
        }
        if (DestroyValidator.IsNullOrDestroyed(canvas))
        {
            return;
        }
        leaveGameAndGoBackMessageBoxObject = null;
        leaveGameAndGoBackMessageBoxObject = Instantiate(leaveGameAndGoBackMessageBox, 
            canvas.transform).GetComponent<LeaveNovelAndGoBackMessageBox>();
        leaveGameAndGoBackMessageBoxObject.Activate();
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

    public void OnSettingsButton()
    {
        SceneLoader.LoadSettingsScene();
    }

    public void OnSettingsButtonForNovelPlayer()
    {
        if (!DestroyValidator.IsNullOrDestroyed(leaveGameAndGoToSettingsMessageBoxObject))
        {
            leaveGameAndGoToSettingsMessageBoxObject.CloseMessageBox();
        }
        if (DestroyValidator.IsNullOrDestroyed(canvas))
        {
            return;
        }
        leaveGameAndGoToSettingsMessageBoxObject = null;
        leaveGameAndGoToSettingsMessageBoxObject = Instantiate(leaveGameAndGoToSettingsMessageBox,
            canvas.transform).GetComponent<LeaveNovelAndGoToSettingsMessageBox>();
        leaveGameAndGoToSettingsMessageBoxObject.Activate();
    }
}
