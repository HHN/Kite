using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReviewNovelSceneController : SceneController, OnSuccessHandler
{
    [SerializeField] private Button skipButton;
    [SerializeField] private Button confirmButton;
    [SerializeField] private TextMeshProUGUI headline;
    [SerializeField] private TextMeshProUGUI instruction;
    [SerializeField] private string novelName;
    [SerializeField] private long novelID;
    [SerializeField] private long rating;
    [SerializeField] private string reviewText;

    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button star_01;
    [SerializeField] private Button star_02;
    [SerializeField] private Button star_03;
    [SerializeField] private Button star_04;
    [SerializeField] private Button star_05;

    [SerializeField] private Sprite starFullSprite;
    [SerializeField] private Sprite starEmptySprite;

    [SerializeField] private GameObject addNovelReviewServerCallPrefab;
    [SerializeField] private RectTransform layout;

    void Start()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
        BackStackManager.Instance().Push(SceneNames.REVIEW_NOVEL_SCENE);
        skipButton.onClick.AddListener(delegate { OnSkipButton(); });
        confirmButton.onClick.AddListener(delegate { OnConfirmButton(); });
        star_01.onClick.AddListener(delegate { OnStar01Button(); });
        star_02.onClick.AddListener(delegate { OnStar02Button(); });
        star_03.onClick.AddListener(delegate { OnStar03Button(); });
        star_04.onClick.AddListener(delegate { OnStar04Button(); });
        star_05.onClick.AddListener(delegate { OnStar05Button(); });

        VisualNovel novelToPlay = PlayManager.Instance().GetVisualNovelToPlay();

        if (novelToPlay == null )
        {
            return;
        }
        novelName = novelToPlay.title;
        novelID = novelToPlay.id;
        InitializeHeadline();

        rating = 1;
        star_01.image.sprite = starFullSprite;
        star_02.image.sprite = starEmptySprite;
        star_03.image.sprite = starEmptySprite;
        star_04.image.sprite = starEmptySprite;
        star_05.image.sprite = starEmptySprite;
    }

    public void InitializeHeadline()
    {
        headline.text = "Gespielte Novel: " + novelName;
        instruction.text = "Bitte teile uns deine Meinung zu der eben gespielten Novel mit!";
    }

    public void OnSkipButton()
    {
        SceneLoader.LoadFeedbackScene();
    }

    public void OnConfirmButton()
    {
        VisualNovel novelToPlay = PlayManager.Instance().GetVisualNovelToPlay();

        if (novelToPlay == null || string.IsNullOrEmpty(inputField.text))
        {
            return;
        }
        reviewText = inputField.text.Trim();
        inputField.text = "";
        SendReviewToServer();
        SceneLoader.LoadFeedbackScene();
    }

    public void SendReviewToServer()
    {
        AddNovelReviewServerCall call = Instantiate(addNovelReviewServerCallPrefab).GetComponent<AddNovelReviewServerCall>();
        call.sceneController = this;
        call.onSuccessHandler = this;
        call.novelId = novelID;
        call.novelName = novelName;
        call.rating = rating;
        call.reviewText = reviewText;
        call.SendRequest();
        DontDestroyOnLoad(call.gameObject);
    }

    public void OnStar01Button()
    {
        rating = 1;
        star_01.image.sprite = starFullSprite;
        star_02.image.sprite = starEmptySprite;
        star_03.image.sprite = starEmptySprite;
        star_04.image.sprite = starEmptySprite;
        star_05.image.sprite = starEmptySprite;
    }

    public void OnStar02Button()
    {
        rating = 2;
        star_01.image.sprite = starFullSprite;
        star_02.image.sprite = starFullSprite;
        star_03.image.sprite = starEmptySprite;
        star_04.image.sprite = starEmptySprite;
        star_05.image.sprite = starEmptySprite;
    }

    public void OnStar03Button()
    {
        rating = 3;
        star_01.image.sprite = starFullSprite;
        star_02.image.sprite = starFullSprite;
        star_03.image.sprite = starFullSprite;
        star_04.image.sprite = starEmptySprite;
        star_05.image.sprite = starEmptySprite;
    }

    public void OnStar04Button()
    {
        rating = 4;
        star_01.image.sprite = starFullSprite;
        star_02.image.sprite = starFullSprite;
        star_03.image.sprite = starFullSprite;
        star_04.image.sprite = starFullSprite;
        star_05.image.sprite = starEmptySprite;
    }

    public void OnStar05Button()
    {
        rating = 5;
        star_01.image.sprite = starFullSprite;
        star_02.image.sprite = starFullSprite;
        star_03.image.sprite = starFullSprite;
        star_04.image.sprite = starFullSprite;
        star_05.image.sprite = starFullSprite;
    }

    public void OnSuccess(Response response)
    {
        // nothing to do here
    }
}
