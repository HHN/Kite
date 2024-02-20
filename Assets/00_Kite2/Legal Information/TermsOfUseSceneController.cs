public class TermsOfUseSceneController : SceneController
{
    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.TERMS_OF_USE_SCENE);
    }
}
