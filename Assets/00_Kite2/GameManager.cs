using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private bool isBankkreditNovelSaved;
    [SerializeField] private bool isBekannteTreffenNovelSaved;
    [SerializeField] private bool isElternNovelSaved;
    [SerializeField] private bool isNotarinNovelSaved;
    [SerializeField] private bool isPresseNovelSaved;
    [SerializeField] private bool isBueroNovelSaved;
    [SerializeField] private bool isIntroNovelSaved;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Duplicate GameManager instance detected and destroyed.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Bleibt beim Szenenwechsel bestehen
        Debug.Log("GameManager instance created.");
    }

    // Method to set the correct bool based on the novel name
    public void SetNovelSavedFlag(string novelName, bool isSaved)
    {
        switch (novelName)
        {
            case "Banktermin wegen Kreditbeantragung":
                isBankkreditNovelSaved = isSaved;
                break;
            case "Gespräch mit einem Bekannten":
                isBekannteTreffenNovelSaved = isSaved;
                break;
            case "Telefonat mit den Eltern":
                isElternNovelSaved = isSaved;
                break;
            case "Telefonat mit der Notarin":
                isNotarinNovelSaved = isSaved;
                break;
            case "Pressegespräch":
                isPresseNovelSaved = isSaved;
                break;
            case "Anmietung eines Büros":
                isBueroNovelSaved = isSaved;
                break;
            default:
                Debug.LogWarning("Unrecognized novel name: " + novelName);
                break;
        }
    }

    // Method to check if a novel is saved based on its name
    public bool IsNovelSaved(string novelName)
    {
        return novelName switch
        {
            "Banktermin wegen Kreditbeantragung" => isBankkreditNovelSaved,
            "Gespräch mit einem Bekannten" => isBekannteTreffenNovelSaved,
            "Telefonat mit den Eltern" => isElternNovelSaved,
            "Telefonat mit der Notarin" => isNotarinNovelSaved,
            "Pressegespräch" => isPresseNovelSaved,
            "Anmietung eines Büros" => isBueroNovelSaved,
            "Einstiegsdialog" => isIntroNovelSaved,
            _ => throw new ArgumentException("Unrecognized novel name: " + novelName)
        };
    }
}
