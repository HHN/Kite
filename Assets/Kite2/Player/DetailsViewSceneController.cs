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
    public FavoriteButton favoriteButton;
    public UploadButton uploadButton;
    public Sprite[] novelSprites;

    private void Start()
    {
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
