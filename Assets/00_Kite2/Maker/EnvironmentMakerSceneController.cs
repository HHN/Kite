using UnityEngine.UI;

public class EnvironmentMakerSceneController : SceneController
{
    public Button nextButton;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.ENVIRONMENT_MAKER_SCENE);

        nextButton.onClick.AddListener(delegate { OnNextButton(); });
    }

    public void OnNextButton()
    {
        SceneLoader.LoadDialogueMakerScene();
    }
}
