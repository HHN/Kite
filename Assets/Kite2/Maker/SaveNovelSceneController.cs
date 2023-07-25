using UnityEngine.UI;

public class SaveNovelSceneController : SceneController
{
    public Button nextButton;

    void Start()
    {
        nextButton.onClick.AddListener(delegate { OnNextButton(); });
    }

    public void OnNextButton()
    {
        SceneLoader.LoadMainMenuScene();
    }
}
