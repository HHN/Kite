using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneHeader : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject warningMessageBox;
    [SerializeField] private LeaveNovelAndGoBackMessageBox warningMessageBoxObject;
    [SerializeField] private Canvas canvas;

    void Start()
    {
        closeButton.onClick.AddListener(delegate { OnCloseButton(); });
        backButton.onClick.AddListener(delegate { OnBackButton(); });
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

    public void OnBackButton()
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
            warningMessageBoxObject = Instantiate(warningMessageBox,
                canvas.transform).GetComponent<LeaveNovelAndGoBackMessageBox>();
            warningMessageBoxObject.Activate();

            return;
        }

        string lastScene = SceneRouter.GetTargetSceneForBackButton();

        if (string.IsNullOrEmpty(lastScene))
        {
            SceneLoader.LoadMainMenuScene();
            return;
        }
        SceneLoader.LoadScene(lastScene);
    }
}
