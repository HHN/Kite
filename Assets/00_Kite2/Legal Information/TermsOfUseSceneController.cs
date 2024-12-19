using _00_Kite2.Common;
using _00_Kite2.Common.Managers;
using _00_Kite2.Common.SceneManagement;

namespace _00_Kite2.Legal_Information
{
    public class TermsOfUseSceneController : SceneController
    {
        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.TERMS_OF_USE_SCENE);
        }
    }
}