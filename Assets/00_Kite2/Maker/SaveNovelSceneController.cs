using UnityEngine.UI;

public class SaveNovelSceneController : SceneController
{
    public Button nextButton;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.SAVE_NOVEL_SCENE);

        nextButton.onClick.AddListener(delegate { OnNextButton(); });
    }

    public void OnNextButton()
    {
        SceneLoader.LoadMainMenuScene();
    }
}
