using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneHeader : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject warningMessageBox;
    [SerializeField] private GameObject warningMessageBoxClose;
    [SerializeField] private LeaveNovelAndGoBackMessageBox warningMessageBoxObject;
    [SerializeField] private LeaveNovelAndGoBackToMainmenuMessageBox warningMessageBoxObjectClose;
    [SerializeField] private Canvas canvas;
    [SerializeField] private bool isNovelScene;

    void Start()
    {
        closeButton.onClick.AddListener(delegate { OnBackButton(); });
        backButton.onClick.AddListener(delegate { OnBackButton(); });
    }

//TODO: To delete
    public void OnCloseButton()
    {
        if (warningMessageBoxClose != null)
        {
            if (!DestroyValidator.IsNullOrDestroyed(warningMessageBoxObjectClose))
            {
                warningMessageBoxObjectClose.CloseMessageBox();
            }
            if (DestroyValidator.IsNullOrDestroyed(canvas))
            {
                return;
            }
            warningMessageBoxObjectClose = null;
            warningMessageBoxObjectClose = Instantiate(warningMessageBoxClose,
                canvas.transform).GetComponent<LeaveNovelAndGoBackToMainmenuMessageBox>();
            warningMessageBoxObjectClose.Activate();

            return;
        }
        SceneLoader.LoadMainMenuScene();
        return;
    }

    public void OnBackButton()
    {
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
            warningMessageBoxObject = Instantiate(warningMessageBox,
                canvas.transform).GetComponent<LeaveNovelAndGoBackMessageBox>();
            warningMessageBoxObject.Activate();

            return;
        }
    }
}
