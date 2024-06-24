using UnityEngine;
using UnityEngine.UI;

public class FoundersWell2SceneController : SceneController
{
    [SerializeField] private Button ressourcenButton;
    [SerializeField] private Button playHistoryButton;
    [SerializeField] private Button gemerkteNovelsButton;
    [SerializeField] private Button einstellungenButton;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.FOUNDERS_WELL_2_SCENE);

        ressourcenButton.onClick.AddListener(delegate { OnRessourcenButton(); });
        playHistoryButton.onClick.AddListener(delegate { OnPlayHistoryButton(); });
        gemerkteNovelsButton.onClick.AddListener(delegate { OnGemerkteNovelsButton(); });
        einstellungenButton.onClick.AddListener(delegate { OnEinstellungenButton(); });

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
}
