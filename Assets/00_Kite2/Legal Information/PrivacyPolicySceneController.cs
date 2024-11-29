using _00_Kite2.Common.Managers;

public class PrivacyPolicySceneController : SceneController
{
    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.PRIVACY_POLICY_SCENE);
    }
}
