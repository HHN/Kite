using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailsViewSceneController : SceneController
{
    public Image novelImage;
    public Button playButton;
    public TextMeshProUGUI novelTitle;
    public TextMeshProUGUI novelDescription;
    public VisualNovel novelToDisplay;
    public Sprite[] novelSprites;

    private void Start()
    {
        AnalyticsServiceHandler.Instance().StartStopwatch();
        BackStackManager.Instance().Push(SceneNames.DETAILS_VIEW_SCENE);

        playButton.onClick.AddListener(delegate { OnPlayButton(); });
        Initialize();
    }

    public void Initialize()
    {
        novelToDisplay = PlayManager.Instance().GetVisualNovelToPlay();

        if (novelToDisplay == null )
        {
            return;
        }
        long idOfNovelSprite = novelToDisplay.image;
        Sprite spriteOfNovel = FindBigSpriteById(idOfNovelSprite);
        novelImage.sprite = spriteOfNovel;
        novelTitle.text = novelToDisplay.title;
        novelDescription.text = novelToDisplay.description;
    }

    public void OnPlayButton()
    {
        AnalyticsServiceHandler.Instance().SendDetailViewStatistics(novelToDisplay.id);
        PlayManager.Instance().SetVisualNovelToPlay(novelToDisplay);
        SceneLoader.LoadPlayNovelScene(); 
    }

    private Sprite FindBigSpriteById(long id)
    {
        if (novelSprites.Length <= id)
        {
            return null;
        }
        return novelSprites[id];
    }
}
