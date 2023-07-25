using UnityEngine.UI;

public class CharacterMakerSceneController : SceneController
{
    public Button nextButton;

    void Start()
    {
        nextButton.onClick.AddListener(delegate { OnNextButton(); });
    }

    public void OnNextButton()
    {
        SceneLoader.LoadEnvironmentMakerScene();
    }
}
