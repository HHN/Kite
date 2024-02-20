public class ImprintSceneController : SceneController
{
    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.IMPRINT_SCENE);
    }
}
