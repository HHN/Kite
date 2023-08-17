using UnityEngine.UI;

public class DialogueMakerSceneController : SceneController
{
    public Button nextButton;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.DIALOGUE_MAKER_SCENE);

        nextButton.onClick.AddListener(delegate { OnNextButton(); });
    }

    public void OnNextButton()
    {
        SceneLoader.LoadSaveNovelScene();
    }
}
