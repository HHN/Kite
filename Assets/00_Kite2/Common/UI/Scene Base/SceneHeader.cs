using _00_Kite2.Common.SceneManagement;
using _00_Kite2.Common.UI.UI_Elements.Messages;
using _00_Kite2.Common.Utilities;
using _00_Kite2.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _00_Kite2.Common.UI.Scene_Base
{
    public class SceneHeader : MonoBehaviour
    {
        [SerializeField] private Button backButton;
        [SerializeField] private GameObject warningMessageBox;
        [SerializeField] private GameObject warningMessageBoxIntro;
        [SerializeField] private GameObject warningMessageBoxClose;
        [SerializeField] private LeaveNovelAndGoBackMessageBox warningMessageBoxObject;
        [SerializeField] private LeaveNovelAndGoBackMessageBox warningMessageBoxObjectIntro;
        [SerializeField] private LeaveNovelAndGoBackToMainMenuMessageBox warningMessageBoxObjectClose;
        [SerializeField] private Canvas canvas;

        private PlayNovelSceneController _playNovelSceneController; // Reference to the PlayNovelSceneController to manage novel actions

        private void Start()
        {
            GameObject controllerObject = GameObject.Find("Controller");
            if (controllerObject != null)
            {
                _playNovelSceneController = controllerObject.GetComponent<PlayNovelSceneController>();
            }

            // if (_playNovelSceneController == null && isNovelScene)
            // {
            //     Debug.LogWarning(
            //         "PlayNovelSceneController not found in the scene. Make sure this script is attached to the correct scene.");
            // }

            backButton.onClick.AddListener(OnBackButton);
        }

        private void OnBackButton()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            
            if (sceneName.Contains("PlayNovelScene") && _playNovelSceneController != null)
            {
                _playNovelSceneController.IsPaused = true; // Pause the novel progression
            }

            if (!sceneName.Contains("PlayNovelScene"))
            {
                string lastScene = SceneRouter.GetTargetSceneForBackButton();

                if (string.IsNullOrEmpty(lastScene))
                {
                    SceneLoader.LoadMainMenuScene();
                    return;
                }

                #if UNITY_IOS
                    TextToSpeechManager.Instance.CancelSpeak();
                #endif

                SceneLoader.LoadScene(lastScene);
            }

            if (sceneName.Contains("PlayNovelScene") && _playNovelSceneController.NovelToPlay.id == 13)
            {
                if (warningMessageBoxIntro != null)
                {
                    if (!warningMessageBoxObjectIntro.IsNullOrDestroyed())
                    {
                        warningMessageBoxObjectIntro.CloseMessageBox();
                    }

                    if (canvas.IsNullOrDestroyed())
                    {
                        return;
                    }

                    warningMessageBoxObjectIntro = null;
                    warningMessageBoxObjectIntro = Instantiate(warningMessageBoxIntro, canvas.transform)
                        .GetComponent<LeaveNovelAndGoBackMessageBox>();
                    warningMessageBoxObjectIntro.Activate();
                }
            }
            else
            {
                if (warningMessageBox != null)
                {
                    if (!warningMessageBoxObject.IsNullOrDestroyed())
                    {
                        warningMessageBoxObject.CloseMessageBox();
                    }

                    if (canvas.IsNullOrDestroyed())
                    {
                        return;
                    }

                    warningMessageBoxObject = null;
                    warningMessageBoxObject = Instantiate(warningMessageBox, canvas.transform)
                        .GetComponent<LeaveNovelAndGoBackMessageBox>();
                    warningMessageBoxObject.Activate();
                }
            }
        }
    }
}