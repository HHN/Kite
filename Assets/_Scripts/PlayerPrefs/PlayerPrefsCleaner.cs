using UnityEditor;
using UnityEngine;

namespace Assets._Scripts.PlayerPrefs
{
    public class PlayerPrefsCleaner : MonoBehaviour
    {
        [MenuItem("Tools/Delete All PlayerPrefs")]
        public static void DeleteAllPlayerPrefs()
        {
            UnityEngine.PlayerPrefs.DeleteAll();
            UnityEngine.PlayerPrefs.Save();
            Debug.Log("PlayerPrefs deleted.");
        }
    }
}