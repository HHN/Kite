using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    private GameObject copyNotification;

    private static GameObjectManager instance;

    public static GameObjectManager Instance()
    {
        if (instance == null)
        {
            // Suche nach einer vorhandenen Instanz in der Szene
            instance = FindObjectOfType<GameObjectManager>();

            // Wenn keine Instanz gefunden wurde, erstelle eine neue
            if (instance == null)
            {
                GameObject singletonObject = new GameObject("GameObjectManager");
                instance = singletonObject.AddComponent<GameObjectManager>();
                DontDestroyOnLoad(singletonObject); // Optional: Beibehalten, wenn die Szene wechselt
            }
        }
        return instance;
    }

    // Setter-Methode
    public void SetCopyNotification(GameObject value)
    {
        copyNotification = value;
        if (copyNotification != null)
        {
            Debug.Log("copyNotification has been set");
        }
    }

    // Getter-Methode
    public GameObject GetCopyNotification()
    {
        if (copyNotification != null)
        {
            Debug.Log("copyNotification is NOT null");
        }
        return copyNotification;
    }
}
