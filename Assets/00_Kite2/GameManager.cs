using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NovelSaveStatus
{
    public string novelId;
    public bool isSaved;
}

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

    // Dictionary zur dynamischen Verwaltung des Speicherstatus jeder Novel
    private Dictionary<string, bool> novelSaveStatus = new();

    // Liste zur Anzeige im Inspector (nur für Debugging, nicht direkt genutzt)
    [SerializeField] private List<NovelSaveStatus> novelSaveStatusList = new();

    //// Liste aller bekannten Novel-Namen
    //private readonly List<string> novelNames = new()
    //{
    //    "Banktermin wegen Kreditbeantragung",
    //    "Gespräch mit einem Bekannten",
    //    "Telefonat mit den Eltern",
    //    "Telefonat mit der Notarin",
    //    "Pressegespräch",
    //    "Anmietung eines Büros",
    //    "Einstiegsdialog"
    //};

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            //Debug.Log("Duplicate GameManager instance detected and destroyed.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Bleibt beim Szenenwechsel bestehen
        //Debug.Log("GameManager instance created.");

        // Überprüfe und setze den Speicherstatus für alle Novels beim Start
        CheckAndSetAllNovelsStatus();
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

    // Überprüft und setzt den Speicherstatus für alle Novels
    public void CheckAndSetAllNovelsStatus()
    {
        novelSaveStatus.Clear();
        novelSaveStatusList.Clear();

        foreach (VisualNovelNames novelName in Enum.GetValues(typeof(VisualNovelNames)))
        {
            string novelId = VisualNovelNamesHelper.ToInt(novelName).ToString();

            bool isSaved = SaveLoadManager.Load(novelId) != null;
            novelSaveStatus[novelId] = isSaved;

            // Aktualisiert die Liste für den Inspector
            novelSaveStatusList.Add(new NovelSaveStatus { novelId = novelId, isSaved = isSaved });
        }
    }

    // Methode, um den Status einer Novel direkt zu überprüfen
    public bool IsNovelSaved(string novelId)
    {
        //Debug.Log("[IsNovelSaved] - novelId: " + novelId);
        return novelSaveStatus.ContainsKey(novelId) && novelSaveStatus[novelId];
    }

    //// Method to check if a novel is saved based on its name
    //public bool IsNovelSaved(string novelName)
    //{
    //    return novelName switch
    //    {
    //        "Banktermin wegen Kreditbeantragung" => isBankkreditNovelSaved,
    //        "Gespräch mit einem Bekannten" => isBekannteTreffenNovelSaved,
    //        "Telefonat mit den Eltern" => isElternNovelSaved,
    //        "Telefonat mit der Notarin" => isNotarinNovelSaved,
    //        "Pressegespräch" => isPresseNovelSaved,
    //        "Anmietung eines Büros" => isBueroNovelSaved,
    //        "Einstiegsdialog" => isIntroNovelSaved,
    //        _ => throw new ArgumentException("Unrecognized novel name: " + novelName)
    //    };
    //}

    // Neue Methode zum Abrufen des Speicherstatus einer bestimmten Novel
    public bool HasSavedProgress(string novelId)
    {
        foreach(var id in novelSaveStatus.Keys)
        {
        }
        // Hier wird geprüft, ob der Speicherstand für die Novel im Dictionary vorhanden ist und `true` ist
        return novelSaveStatus.ContainsKey(novelId) && novelSaveStatus[novelId];
    }

    // Methode, um den Speicherstatus einer Novel bei Bedarf zu aktualisieren (z.B. nach einem Speichervorgang)
    public void UpdateNovelSaveStatus(string novelId, bool isSaved)
    {
        if (novelSaveStatus.ContainsKey(novelId))
        {
            novelSaveStatus[novelId] = isSaved;
            // Debug-Ausgabe für Bestätigung
            //Debug.Log($"Speicherstatus für Novel mit ID '{novelId}' auf '{isSaved}' gesetzt.");
        }
        //else
        //{
        //    Debug.LogWarning($"Novel ID '{novelId}' wurde im novelSaveStatus nicht gefunden.");
        //}
    }
}
