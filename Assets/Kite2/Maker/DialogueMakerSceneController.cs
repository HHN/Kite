using UnityEngine.UI;

public class DialogueMakerSceneController : SceneController
{
    public Button nextButton;

    void Start()
    {
        nextButton.onClick.AddListener(delegate { OnNextButton(); });
    }

    public void OnNextButton()
    {
        SceneLoader.LoadSaveNovelScene();
    }
}
