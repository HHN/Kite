using UnityEngine.UI;

public class EnvironmentMakerSceneController : SceneController
{
    public Button nextButton;

    void Start()
    {
        nextButton.onClick.AddListener(delegate { OnNextButton(); });
    }

    public void OnNextButton()
    {
        SceneLoader.LoadDialogueMakerScene();
    }
}
