using _00_Kite2.Common;
using _00_Kite2.Common.Managers;
using _00_Kite2.Common.SceneManagement;

namespace _00_Kite2.Legal_Information
{
    public class PrivacyPolicySceneController : SceneController
    {
        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.PRIVACY_POLICY_SCENE);
        }
    }
}