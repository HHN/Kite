using Assets._Scripts.SceneManagement;
using Assets._Scripts.UIElements.Messages;
using Assets._Scripts.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets._Scripts.UIElements.SceneBase
{
    public class SceneBase : MonoBehaviour
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button homeButton;
        [SerializeField] private Button settingsButton;

        [SerializeField] private GameObject leaveGameAndGoToMainMenuMessageBox;
        [SerializeField] private GameObject leaveGameAndGoToSettingsMessageBox;
        [SerializeField] private GameObject leaveGameAndGoBackMessageBox;
        [SerializeField] private GameObject leaveGameAndCloseMessageBox;
        
        [SerializeField] private LeaveNovelAndGoBackToMainMenuMessageBox leaveGameAndGoToMainMenuMessageBoxObject;
        [SerializeField] private LeaveNovelAndGoToSettingsMessageBox leaveGameAndGoToSettingsMessageBoxObject;
        [SerializeField] private LeaveNovelAndGoBackMessageBox leaveGameAndGoBackMessageBoxObject;
        [SerializeField] private CloseNovelAndGoBackMessageBox leaveNovelAndCloseMessageBoxObject;
        
        [SerializeField] private Canvas canvas;

        private void Start()
        {
            OnGuestMode();
            string sceneName = SceneManager.GetActiveScene().name;

            if (sceneName.Equals(SceneNames.PlayNovelScene))
            {
                homeButton.onClick.AddListener(OnHomeButtonForNovelPlayer);
                settingsButton.onClick.AddListener(OnSettingsButtonForNovelPlayer);
                backButton.onClick.AddListener(OnBackButtonForNovelPlayer);
                closeButton.onClick.AddListener(OnBackButtonForNovelPlayer);
            }
            else if (sceneName.Equals(SceneNames.MainMenuScene))
            {
            }
            else
            {
                homeButton.onClick.AddListener(OnHomeButton);
                settingsButton.onClick.AddListener(OnSettingsButton);
                backButton.onClick.AddListener(OnBackButton);
            }
        }

        private static void OnGuestMode()
        {
        }

        private static void OnBackButton()
        {
            string lastScene = SceneRouter.GetTargetSceneForBackButton();

            SceneLoader.LoadScene(string.IsNullOrEmpty(lastScene) ? SceneNames.MainMenuScene : lastScene);
        }

        private void OnBackButtonForNovelPlayer()
        {
            if (!leaveGameAndGoBackMessageBoxObject.IsNullOrDestroyed())
            {
                leaveGameAndGoBackMessageBoxObject.CloseMessageBox();
            }

            if (canvas.IsNullOrDestroyed())
            {
                return;
            }

            leaveGameAndGoBackMessageBoxObject = null;
            leaveGameAndGoBackMessageBoxObject = Instantiate(leaveGameAndGoBackMessageBox,
                canvas.transform).GetComponent<LeaveNovelAndGoBackMessageBox>();
            leaveGameAndGoBackMessageBoxObject.Activate();
        }

        private static void OnHomeButton() => SceneLoader.LoadMainMenuScene();

        private void OnHomeButtonForNovelPlayer()
        {
            if (!leaveGameAndGoToMainMenuMessageBoxObject.IsNullOrDestroyed())
            {
                leaveGameAndGoToMainMenuMessageBoxObject.CloseMessageBox();
            }

            if (canvas.IsNullOrDestroyed())
            {
                return;
            }

            leaveGameAndGoToMainMenuMessageBoxObject = null;
            leaveGameAndGoToMainMenuMessageBoxObject = Instantiate(leaveGameAndGoToMainMenuMessageBox,
                canvas.transform).GetComponent<LeaveNovelAndGoBackToMainMenuMessageBox>();
            leaveGameAndGoToMainMenuMessageBoxObject.Activate();
        }

        private static void OnSettingsButton() => SceneLoader.LoadSettingsScene();

        private void OnSettingsButtonForNovelPlayer()
        {
            if (!leaveGameAndGoToSettingsMessageBoxObject.IsNullOrDestroyed())
            {
                leaveGameAndGoToSettingsMessageBoxObject.CloseMessageBox();
            }

            if (canvas.IsNullOrDestroyed())
            {
                return;
            }

            leaveGameAndGoToSettingsMessageBoxObject = null;
            leaveGameAndGoToSettingsMessageBoxObject = Instantiate(leaveGameAndGoToSettingsMessageBox,
                canvas.transform).GetComponent<LeaveNovelAndGoToSettingsMessageBox>();
            leaveGameAndGoToSettingsMessageBoxObject.Activate();
        }
    }
}