using Assets._Scripts.Common.Managers;
using Assets._Scripts.Common.SceneManagement;

namespace Assets._Scripts.Common.SceneMemory
{
    public abstract class FoundersWell2SceneMemory
    {
        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.FOUNDERS_WELL_2_SCENE);
        }
    }
}