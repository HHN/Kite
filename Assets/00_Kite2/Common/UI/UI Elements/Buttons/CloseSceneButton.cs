using UnityEngine;
using UnityEngine.UI;

public class CloseSceneButton : MonoBehaviour
{
    public Button button;

    void Start()
    {
        button.onClick.AddListener(delegate { OnCloseButton(); });
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
}
