namespace Assets._Scripts.Managers
{
    public class SceneMemoryManager
    {
        private static SceneMemoryManager _instance;

        private float _scrollPosition;

        private SceneMemoryManager()
        {
            ClearMemory();
        }

        public static SceneMemoryManager Instance()
        {
            if (_instance == null)
            {
                _instance = new SceneMemoryManager();
            }

            return _instance;
        }

        public float GetMemoryOfFoundersBubbleScene()
        {
            return _scrollPosition;
        }

        public void SetMemoryOfFoundersBubbleScene(float scrollPosition)
        {
            _scrollPosition = scrollPosition;
        }

        private void ClearMemory()
        {
            _scrollPosition = 0;
        }
    }
}