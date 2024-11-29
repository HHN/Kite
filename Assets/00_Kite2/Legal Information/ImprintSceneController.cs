using _00_Kite2.Common.Managers;

public class ImprintSceneController : SceneController
{
    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.IMPRINT_SCENE);
    }
}
