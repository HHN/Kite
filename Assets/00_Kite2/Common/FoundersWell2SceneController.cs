using _00_Kite2.Common.Managers;
using UnityEngine;
using UnityEngine.UI;

public class FoundersWell2SceneController : SceneController
{
    [SerializeField] private Button ressourcenButton;
    [SerializeField] private Button playHistoryButton;
    [SerializeField] private Button gemerkteNovelsButton;
    [SerializeField] private Button einstellungenButton;
    [SerializeField] private Button foundersWellButton;
    [SerializeField] private Button editButton;
    [SerializeField] private Button profilePicture;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.FOUNDERS_WELL_2_SCENE);

        ressourcenButton.onClick.AddListener(delegate { OnRessourcenButton(); });
        playHistoryButton.onClick.AddListener(delegate { OnPlayHistoryButton(); });
        gemerkteNovelsButton.onClick.AddListener(delegate { OnGemerkteNovelsButton(); });
        einstellungenButton.onClick.AddListener(delegate { OnEinstellungenButton(); });
        foundersWellButton.onClick.AddListener(delegate { OnFoundersWellButton(); });
        editButton.onClick.AddListener(delegate { OnEditButton(); });
        profilePicture.onClick.AddListener(delegate { OnProfilePicture(); });

        // Load Screen size for later use
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        NovelColorManager.Instance().SetCanvasHeight(canvasRect.rect.height);
        NovelColorManager.Instance().SetCanvasWidth(canvasRect.rect.width);
    }

    public void OnRessourcenButton()
    {
        SceneLoader.LoadRessourcenScene();
    }
        
    public void OnPlayHistoryButton()
    {
        SceneLoader.LoadNovelHistoryScene();
    }

    public void OnGemerkteNovelsButton()
    {
        SceneLoader.LoadGemerkteNovelsScene();
    }    
    
    public void OnEinstellungenButton()
    {
        SceneLoader.LoadEinstellungenScene();
    }    
    
    public void OnFoundersWellButton()
    {
        SceneLoader.LoadFoundersBubbleScene();
    }

    public void OnEditButton()
    {
        DisplayInfoMessage("Diese Funktionalitäten sind derzeit noch nicht verfügbar, da sich die App noch in der Entwicklung befindet. Wir arbeiten daran, die volle Funktionalität bald bereitzustellen."); DisplayInfoMessage("Diese Funktionalitäten sind derzeit noch nicht verfügbar, da sich die App noch in der Entwicklung befindet. Wir arbeiten daran, die volle Funktionalität bald bereitzustellen.");
    }

    public void OnProfilePicture()
    {
        DisplayInfoMessage("Diese Funktionalitäten sind derzeit noch nicht verfügbar, da sich die App noch in der Entwicklung befindet. Wir arbeiten daran, die volle Funktionalität bald bereitzustellen.");
    }
}
