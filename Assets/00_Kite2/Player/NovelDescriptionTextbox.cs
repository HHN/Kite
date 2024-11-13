using System;
using System.Collections;
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
    [SerializeField] private TextMeshProUGUI playText;
    [SerializeField] private TextMeshProUGUI bookmarkText;
    [SerializeField] private Image bookmarkImage;
    [SerializeField] private Sprite bookmarkSprite;
    [SerializeField] private Sprite unBookmarkSprite;
    [SerializeField] private Color colorOfText;

    void Start()
    {
        playButton.onClick.AddListener(delegate { OnPlayButton(); });
        bookMarkButton.onClick.AddListener(delegate { OnBookmarkButton(); });
    }


    public void InitializeBookMarkButton(bool isFavorite)
    {
        playText.color = colorOfText;

        if (isFavorite)
        {
            bookmarkText.text = "GEMERKT";
            bookmarkImage.sprite = bookmarkSprite;
            bookmarkText.color = Color.white;
            return;
        }
        bookmarkImage.sprite = unBookmarkSprite;
        bookmarkText.text = "MERKEN";
        bookmarkText.color = colorOfText;
    }

    public void SetColorOfImage(Color color)
    {
        colorOfText = color;
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
        InitializeBookMarkButton(FavoritesManager.Instance().IsFavorite(visualNovel));
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

        }
        else
        {
            SceneLoader.LoadPlayNovelScene();
        }
        return;
    }

    public void StartNovel()
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

        }
        else
        {
            SceneLoader.LoadPlayNovelScene();
        }
        return;
    }

    public void OnBookmarkButton()
    {
        StartCoroutine(MarkAsFavorite(visualNovelToDisplay));
    }

    public IEnumerator MarkAsFavorite(VisualNovel visualNovel)
    {
        if (FavoritesManager.Instance().IsFavorite(visualNovel))
        {
            FavoritesManager.Instance().UnmarkAsFavorite(visualNovel);
            InitializeBookMarkButton(false);
            yield break;
        }
        FavoritesManager.Instance().MarkAsFavorite(visualNovel);
        InitializeBookMarkButton(true);
        yield return null;
    }

    public void SetButtonsActive(bool active)
    {
        playButton.gameObject.SetActive(active);
        bookMarkButton.gameObject.SetActive(active);
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

    public void UpdateSize()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }


    //public VisualNovel NovelToPlay
    //{
    //    get => visualNovelToDisplay;
    //    set => visualNovelToDisplay = value;
    //}

    //public VisualNovelNames NovelName
    //{
    //    get => visualNovelName;
    //    set => visualNovelName = value;
    //}

    //public GameObject SelectNovelSoundPrefab
    //{
    //    get => selectNovelSoundPrefab;
    //    set => selectNovelSoundPrefab = value;
    //}
}
