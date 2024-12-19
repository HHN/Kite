using _00_Kite2.Common.Managers;
using _00_Kite2.Common.SceneManagement;

namespace _00_Kite2.Common.SceneMemory
{
    public abstract class FoundersWell2SceneMemory
    {
        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.FOUNDERS_WELL_2_SCENE);
        }
    }
}