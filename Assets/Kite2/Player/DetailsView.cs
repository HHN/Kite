using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DetailsView : MonoBehaviour
{
    public Image novelImage;
    public Button playButton;
    public TextMeshProUGUI novelTitle;
    public TextMeshProUGUI novelDescription;
    public VisualNovel novelToDisplay;
    public NovelProvider novelProvider;
    public FavoriteButton favoriteButton;

    private void Start()
    {
        playButton.onClick.AddListener(delegate { OnPlayButton(); });
    }

    public void Initialize()
    {
        long idOfNovelSprite = novelToDisplay.image;
        Sprite spriteOfNovel = novelProvider.FindBigSpriteById(idOfNovelSprite);
        novelImage.sprite = spriteOfNovel;
        novelTitle.text = novelToDisplay.title;
        novelDescription.text = novelToDisplay.description;
        favoriteButton.novel = novelToDisplay;
        favoriteButton.Init();
    }

    public void OnPlayButton()
    {
        PlayManager.Instance().SetVisualNovelToPlay(novelToDisplay);
        SceneLoader.LoadPlayNovelScene();
    }
}
