using UnityEngine;

namespace Assets._Scripts.Managers
{
    public class GameObjectManager : MonoBehaviour
    {
        private GameObject _copyNotification;

        private static GameObjectManager _instance;

        public static GameObjectManager Instance()
        {
            if (_instance == null)
            {
                // Suche nach einer vorhandenen Instanz in der Szene
                _instance = FindObjectOfType<GameObjectManager>();

                // Wenn keine Instanz gefunden wurde, erstelle eine neue
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("GameObjectManager");
                    _instance = singletonObject.AddComponent<GameObjectManager>();
                    DontDestroyOnLoad(singletonObject); // Optional: Beibehalten, wenn die Szene wechselt
                }
            }

            return _instance;
        }

        // Setter-Methode
        public void SetCopyNotification(GameObject value)
        {
            _copyNotification = value;
        }

        // Getter-Methode
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