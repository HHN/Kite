using _00_Kite2.Common.UI.UI_Elements.Messages;
using _00_Kite2.Player;
using UnityEngine;
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
        [SerializeField] private LeaveNovelAndGoBackToMainmenuMessageBox warningMessageBoxObjectClose;
        [SerializeField] private Canvas canvas;
        [SerializeField] private bool isNovelScene;

        private PlayNovelSceneController _playNovelSceneController; // Reference to the PlayNovelSceneController to manage novel actions

        private void Start()
        {
            GameObject controllerObject = GameObject.Find("Controller");
            if (controllerObject != null)
            {
                _playNovelSceneController = controllerObject.GetComponent<PlayNovelSceneController>();
            }

            if (_playNovelSceneController == null && isNovelScene)
            {
                Debug.LogWarning("PlayNovelSceneController not found in the scene. Make sure this script is attached to the correct scene.");
            }

            backButton.onClick.AddListener(OnBackButton);
        }

        private void OnBackButton()
        {
            if (isNovelScene && _playNovelSceneController != null)
            {
                _playNovelSceneController.IsPaused = true; // Pause the novel progression
            }

            if (!this.isNovelScene)
            {
                string lastScene = SceneRouter.GetTargetSceneForBackButton();

                if (string.IsNullOrEmpty(lastScene))
                {
                    SceneLoader.LoadMainMenuScene();
                    return;
                }
                SceneLoader.LoadScene(lastScene);
            }

            if (_playNovelSceneController.NovelToPlay.id == 13)
            {
                if (warningMessageBoxIntro != null)
                {
                    if (!DestroyValidator.IsNullOrDestroyed(warningMessageBoxObjectIntro))
                    {
                        warningMessageBoxObjectIntro.CloseMessageBox();
                    }
                    if (DestroyValidator.IsNullOrDestroyed(canvas))
                    {
                        return;
                    }
                    warningMessageBoxObjectIntro = null;
                    warningMessageBoxObjectIntro = Instantiate(warningMessageBoxIntro, canvas.transform).GetComponent<LeaveNovelAndGoBackMessageBox>();
                    warningMessageBoxObjectIntro.Activate();
                }
            }
            else
            {
                if (warningMessageBox != null)
                {
                    if (!DestroyValidator.IsNullOrDestroyed(warningMessageBoxObject))
                    {
                        warningMessageBoxObject.CloseMessageBox();
                    }
                    if (DestroyValidator.IsNullOrDestroyed(canvas))
                    {
                        return;
                    }
                    warningMessageBoxObject = null;
                    warningMessageBoxObject = Instantiate(warningMessageBox, canvas.transform).GetComponent<LeaveNovelAndGoBackMessageBox>();
                    warningMessageBoxObject.Activate();
                }
            }

        }
    }
}
