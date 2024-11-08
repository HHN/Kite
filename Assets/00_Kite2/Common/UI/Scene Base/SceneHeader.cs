using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneHeader : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject warningMessageBox;
    [SerializeField] private GameObject warningMessageBoxClose;
    [SerializeField] private LeaveNovelAndGoBackMessageBox warningMessageBoxObject;
    [SerializeField] private LeaveNovelAndGoBackToMainmenuMessageBox warningMessageBoxObjectClose;
    [SerializeField] private Canvas canvas;
    [SerializeField] private bool isNovelScene;

    private PlayNovelSceneController playNovelSceneController; // Reference to the PlayNovelSceneController to manage novel actions

    void Start()
    {
        GameObject controllerObject = GameObject.Find("Controller");
        if (controllerObject != null)
        {
            playNovelSceneController = controllerObject.GetComponent<PlayNovelSceneController>();
        }

        if (playNovelSceneController == null && isNovelScene)
        {
            Debug.LogWarning("PlayNovelSceneController not found in the scene. Make sure this script is attached to the correct scene.");
        }

        backButton.onClick.AddListener(delegate { OnBackButton(); });
    }

    public void OnBackButton()
    {
        if (isNovelScene && playNovelSceneController != null)
        {
            playNovelSceneController.IsPaused = true; // Pause the novel progression
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

            return;
        }
    }
}
