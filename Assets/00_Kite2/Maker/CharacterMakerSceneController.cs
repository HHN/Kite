using UnityEngine.UI;

public class CharacterMakerSceneController : SceneController
{
    public Button nextButton;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.CHARACTER_MAKER_SCENE);

        nextButton.onClick.AddListener(delegate { OnNextButton(); });
    }

    public void OnNextButton()
    {
        SceneLoader.LoadEnvironmentMakerScene();
    }
}
