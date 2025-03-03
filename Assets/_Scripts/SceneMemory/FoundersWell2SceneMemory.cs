using Assets._Scripts.Managers;
using Assets._Scripts.SceneManagement;

namespace Assets._Scripts.SceneMemory
{
    public abstract class FoundersWell2SceneMemory
    {
        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.FOUNDERS_WELL_2_SCENE);
        }
    }
}