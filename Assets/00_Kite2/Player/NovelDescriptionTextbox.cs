using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NovelDescriptionTextbox : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private GameObject smalHead;
    [SerializeField] private GameObject bigHead;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private VisualNovel visualNovelToDisplay;
    [SerializeField] private VisualNovelNames visualNovelName;
    [SerializeField] private Button playButton;
    [SerializeField] private Button bookMarkButton;
    [SerializeField] private GameObject selectNovelSoundPrefab;

    void Start()
    {
        playButton.onClick.AddListener(delegate { OnPlayButton(); });
        bookMarkButton.onClick.AddListener(delegate { OnBookmarkButton(); });
    }

    public void SetColorOfImage(Color color)
    {
        image.color = color;
        smalHead.GetComponent<Image>().color = color;
        bigHead.GetComponent<Image>().color = color;
    }

    private void SetBigHead()
    {
        bigHead.SetActive(true);
        smalHead.SetActive(false);
    }

    private void SetSmalHead()
    {
        bigHead.SetActive(false);
        smalHead.SetActive(true);
    }

    public void SetText(string text)
    {
        this.text.text = text;
    }

    public void SetVisualNovelName(VisualNovelNames visualNovelName)
    {
        this.visualNovelName = visualNovelName;
    }

    public void SetVisualNovel(VisualNovel visualNovel)
    {
        this.visualNovelToDisplay = visualNovel;
    }

    public void OnPlayButton()
    {
        PlayManager.Instance().SetVisualNovelToPlay(visualNovelToDisplay);
        PlayManager.Instance().SetForegroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetForegrundColorOfNovel(visualNovelName));
        PlayManager.Instance().SetBackgroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(visualNovelName));
        PlayManager.Instance().SetDiplayNameOfNovelToPlay(FoundersBubbleMetaInformation.GetDisplayNameOfNovelToPlay(visualNovelName));
        GameObject buttonSound = Instantiate(selectNovelSoundPrefab);
        DontDestroyOnLoad(buttonSound);

        if (ShowPlayInstructionManager.Instance().ShowInstruction())
        {
            SceneLoader.LoadPlayInstructionScene();

        } else
        {
            SceneLoader.LoadPlayNovelScene();
        }
        return;
    }

    public void OnBookmarkButton()
    {

    }

    public void SetHead(bool isHigh)
    {
        if (isHigh)
        {
            SetBigHead();
        }
        else
        {
            SetSmalHead();
        }
    }
}
