namespace Assets._Scripts.Controller.CharacterController
{
    /// <summary>
    /// A singleton class to manage and control the animation flag globally
    /// for character animations in the application.
    /// </summary>
    public class AnimationFlagSingleton
    {
        // Singleton instance
        private static AnimationFlagSingleton _instance;

        // Flag indicating the state of the animation
        private bool _animationFlag;

        // Object used for thread safety when creating the instance
        private static readonly object LockObject = new object();

        /// <summary>
        /// Provides access to the singleton instance of the AnimationFlagSingleton class.
        /// </summary>
        /// <returns>The singleton instance of the AnimationFlagSingleton class.</returns>
        public static AnimationFlagSingleton Instance()
        {
            // Ensure thread-safe initialization with double-checked locking
            if (_instance != null) return _instance;
            
            lock (LockObject)
            {
                _instance ??= new AnimationFlagSingleton();
            }

            return _instance;
        }

        /// <summary>
        /// Retrieves the current state of the animation flag.
        /// </summary>
        /// <returns>A boolean value indicating the state of the animation flag.
        /// Returns true if the animation flag is enabled; otherwise, false.</returns>
        public bool GetFlag()
        {
            return _animationFlag;
        }

        /// <summary>
        /// Sets the animation flag to the specified value.
        /// </summary>
        /// <param name="flag">The value to set for the animation flag.</param>
        public void SetFlag(bool flag)
        {
            _animationFlag = flag;
        }
    }
}