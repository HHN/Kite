using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerPrefsSceneController : MonoBehaviour
{

    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform listViewParent;
    [SerializeField] private GameObject panel;
    void Start()
    {
        Debug.Log("Start");
        BackStackManager.Instance().Push(SceneNames.PLAYER_PREFS_SCENE);
        LoadPlayerPrefs();
        panel.SetActive(false);
    }

    void LoadPlayerPrefs()
    {
        Debug.Log("LoadPlayerPrefs");
        List<(string key, string value)> prefs = PlayerDataManager.Instance().GetPlayerPrefsList();
        Debug.Log(prefs.Count);

        foreach (var pref in prefs)
        {
            Debug.Log(pref.value);
            GameObject buttonGO = Instantiate(buttonPrefab, listViewParent);
            buttonGO.GetComponentInChildren<TextMeshProUGUI>().text = pref.key + ": " + pref.value;
            buttonGO.GetComponent<Button>().onClick.AddListener(() => OpenEditWindow(pref.key, pref.value));
        }
    }

    void OpenEditWindow(string key, string value)
    {
        // Logik zum Öffnen des Eingabefensters und Initialisieren mit dem aktuellen Wert
    }
}
