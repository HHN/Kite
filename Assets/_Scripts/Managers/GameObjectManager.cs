using UnityEngine;

namespace Assets._Scripts.Managers
{
    /// <summary>
    /// The GameObjectManager is a singleton class responsible for managing
    /// specific GameObjects within the scene, ensuring they are accessible
    /// and persist across scene transitions.
    /// </summary>
    public class GameObjectManager : MonoBehaviour
    {
        private GameObject _copyNotification;

        private static GameObjectManager _instance;

        /// <summary>
        /// Gets the singleton instance of the GameObjectManager class. This method ensures that
        /// there is always a single instance of GameObjectManager in the application. If an instance
        /// does not already exist in the scene, it creates a new one and makes it persist across
        /// scene changes.
        /// </summary>
        /// <returns>The singleton instance of the GameObjectManager class.</returns>
        public static GameObjectManager Instance()
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameObjectManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("GameObjectManager");
                    _instance = singletonObject.AddComponent<GameObjectManager>();
                    DontDestroyOnLoad(singletonObject);
                }
            }

            return _instance;
        }

        /// <summary>
        /// Sets the reference to the copy notification GameObject within the GameObjectManager.
        /// </summary>
        /// <param name="value">The GameObject to be used as the copy notification.</param>
        public void SetCopyNotification(GameObject value)
        {
            _copyNotification = value;
        }

        /// <summary>
        /// Retrieves the reference to the GameObject used as the copy notification within the scene.
        /// This method ensures the GameObject's accessibility and logs a message if the reference
        /// is not null.
        /// </summary>
        /// <returns>The GameObject representing the copy notification, or null if it has not been set.</returns>
        public GameObject GetCopyNotification()
        {
            if (_copyNotification != null)
            {
                Debug.Log("copyNotification is NOT null");
            }

            return _copyNotification;
        }
    }
}