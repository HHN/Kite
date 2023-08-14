using UnityEngine;
using UnityEditor;

public class PlayerPrefsCleaner : MonoBehaviour
{
    [MenuItem("Tools/Delete All PlayerPrefs")]
    public static void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs deleted.");
    }
}