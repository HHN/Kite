using _00_Kite2.Common.UI.UI_Elements.Messages;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _00_Kite2.Common.UI.Scene_Base
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
        [SerializeField] private LeaveNovelAndGoBackToMainmenuMessageBox leaveGameAndGoToMainMenuMessageBoxObject;
        [SerializeField] private LeaveNovelAndGoToSettingsMessageBox leaveGameAndGoToSettingsMessageBoxObject;
        [SerializeField] private LeaveNovelAndGoBackMessageBox leaveGameAndGoBackMessageBoxObject;
        [SerializeField] private CloseNovelAndGoBackMessageBox leaveNovelAndCloseMessageBoxObject;
        [SerializeField] private Canvas canvas;

        void Start()
        {
            OnGuestMode();

            if (SceneManager.GetActiveScene().name.Equals(SceneNames.PLAY_NOVEL_SCENE))
            {
                homeButton.onClick.AddListener(OnHomeButtonForNovelPlayer);
                settingsButton.onClick.AddListener(OnSettingsButtonForNovelPlayer);
                backButton.onClick.AddListener(OnBackButtonForNovelPlayer);
                closeButton.onClick.AddListener(OnBackButtonForNovelPlayer);
            } 
            else if (SceneManager.GetActiveScene().name.Equals(SceneNames.MAIN_MENU_SCENE))
            {
            } 
            else
            {
                homeButton.onClick.AddListener(OnHomeButton);
                settingsButton.onClick.AddListener(OnSettingsButton);
                backButton.onClick.AddListener(OnBackButton);
            }
        }

        private void OnGuestMode()
        {
        }

        private void OnBackButton()
        {
            string lastScene = SceneRouter.GetTargetSceneForBackButton();

            if (string.IsNullOrEmpty(lastScene))
            {
                SceneLoader.LoadMainMenuScene();
                return;
            }
            SceneLoader.LoadScene(lastScene);
        }

        private void OnBackButtonForNovelPlayer()
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

        private void OnHomeButton()
        {
            SceneLoader.LoadMainMenuScene();
        }

        private void OnHomeButtonForNovelPlayer()
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

        private void OnSettingsButton()
        {
            SceneLoader.LoadSettingsScene();
        }

        private void OnSettingsButtonForNovelPlayer()
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
}
