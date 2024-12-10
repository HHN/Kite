namespace _00_Kite2.Common.Novel.Character.CharacterController
{
    public class AnimationFlagSingleton
    {
        // Singleton instance
        private static AnimationFlagSingleton _instance;

        // Flag indicating the state of the animation
        private bool _animationFlag;

        // Object used for thread safety when creating the instance
        private static readonly object LockObject = new object();

        // Returns the singleton instance, creates it if it doesn't exist
        public static AnimationFlagSingleton Instance()
        {
            // Ensure thread-safe initialization with double-checked locking
            if (_instance == null)
            {
                lock (LockObject)
                {
                    _instance ??= new AnimationFlagSingleton();
                }
            }
            return _instance;
        }

        // Returns the current value of the animation flag
        public bool GetFlag()
        {
            return _animationFlag;
        }

        // Sets the animation flag
        public void SetFlag(bool flag)
        {
            _animationFlag = flag;
        }
    }
}
