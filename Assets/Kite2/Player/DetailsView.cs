using System.Collections;
using System.Collections.Generic;
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
    public SelectNovelSceneController sceneController;

    private void Start()
    {
        playButton.onClick.AddListener(delegate { OnPlayButton(); });
    }

    public void Initialize()
    {
        long idOfNovelSprite = novelToDisplay.image;
        Sprite spriteOfNovel = sceneController.FindBigSpriteById(idOfNovelSprite);
        novelImage.sprite = spriteOfNovel;
        novelTitle.text = novelToDisplay.headline;
        novelDescription.text = novelToDisplay.description;
    }

    public void OnPlayButton()
    {
        PlayManager.Instance().SetVisualNovelToPlay(novelToDisplay);
        SceneLoader.LoadPlayNovelScene();
    }
}
