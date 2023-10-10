using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReportNovelButton : MonoBehaviour, OnSuccessHandler
{

    public GameObject reportPopup; // Das Pop-Up-Fenster



    public Sprite[] sprites;
    private VisualNovel novel;
    public bool isLiked;
    public GameObject likeNovelServerCallPrefab;
    public GameObject unlikeNovelServerCallPrefab;
    public GameObject getNovelLikeServerCallPrefab;
    public Button button;
    public SceneController sceneController;
    public TextMeshProUGUI count;
    public GameObject parent;

    void Start()
    {
        // Das Pop-Up-Fenster zu Beginn deaktivieren
        reportPopup.SetActive(false);
        this.gameObject.GetComponent<Button>().onClick.AddListener(delegate { OnClick(); });
        Init();
    }

    public void Init()
    {
        novel = PlayManager.Instance().GetVisualNovelToPlay();
        if (novel == null)
        {
            return;
        }
        if (novel.id == 0)
        {
            parent.SetActive(false);
            return;
        }
        if (GameManager.Instance().applicationMode != ApplicationModes.LOGGED_IN_USER_MODE)
        {
            this.GetComponent<Button>().interactable = false;
        }
        this.gameObject.GetComponent<Button>().image.sprite = sprites[0];
        isLiked = false;
    }

    public void OpenReportPopup()
    {
        // Pop-Up-Fenster öffnen
        reportPopup.SetActive(true);
    }

    public void CloseReportPopup()
    {
        // Pop-Up-Fenster schließen
        reportPopup.SetActive(false);
    }

    public void SendReport()
    {
        // Hier kannst du den Report an den Server senden
        // Implementiere die Logik zum Senden des Berichts an den Server hier
        // Du kannst z.B. WebRequests verwenden, um Daten an den Server zu übertragen
        // Nachdem der Bericht gesendet wurde, kannst du das Pop-Up-Fenster schließen
        CloseReportPopup();
    }

    public void SendLikeRequest()
    {
        LikeNovelServerCall call = Instantiate(likeNovelServerCallPrefab).GetComponent<LikeNovelServerCall>();
        call.sceneController = sceneController;
        call.onSuccessHandler = this;
        call.id = novel.id;
        call.SendRequest();
        DontDestroyOnLoad(call.gameObject);
    }

    public void OnSuccess(Response response)
    {
        
    }

    public void OnClick()
    {
        
    }


}
