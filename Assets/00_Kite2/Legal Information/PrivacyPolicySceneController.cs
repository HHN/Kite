public class PrivacyPolicySceneController : SceneController
{
    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.PRIVACY_POLICY_SCENE);
    }
}
