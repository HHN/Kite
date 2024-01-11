using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReviewAiSceneController : SceneController, OnSuccessHandler
{
    [SerializeField] private Button skipButton;
    [SerializeField] private Button confirmButton;
    [SerializeField] private TextMeshProUGUI aiFeedback;
    [SerializeField] private TextMeshProUGUI instruction;
    [SerializeField] private string novelName;
    [SerializeField] private long novelID;
    [SerializeField] private string reviewText;
    [SerializeField] public string prompt;
    [SerializeField] public string aiFeedbackString;

    [SerializeField] private TMP_InputField inputField;

    [SerializeField] private GameObject addAiReviewServerCallPrefab;
    [SerializeField] private RectTransform layout;

    // Start is called before the first frame update
    void Start()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
        BackStackManager.Instance().Push(SceneNames.REVIEW_AI_SCENE);
        skipButton.onClick.AddListener(delegate { OnSkipButton(); });

        confirmButton.onClick.AddListener(delegate { OnConfirmButton(); });

        VisualNovel novelToPlay = PlayManager.Instance().GetVisualNovelToPlay();

        if (novelToPlay == null)
        {
            return;
        }
        novelName = novelToPlay.title;
        novelID = novelToPlay.id;
        aiFeedbackString = novelToPlay.feedback;
        aiFeedback.text = aiFeedbackString;
        prompt = PromptManager.Instance().GetPrompt(novelToPlay.context);
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

        BackStackManager.Instance().Clear(); // we go back to the explorer and don't want
                                             // the back-button to bring us to the feedback scene aggain
        SceneLoader.LoadNovelExplorerScene();
    }

    public void OnSkipButton()
    {
        BackStackManager.Instance().Clear(); // we go back to the explorer and don't want
                                             // the back-button to bring us to the feedback scene aggain
        SceneLoader.LoadNovelExplorerScene();
    }

    public void SendReviewToServer()
    {
        AddAiReviewServerCall call = Instantiate(addAiReviewServerCallPrefab).GetComponent<AddAiReviewServerCall>();
        call.sceneController = this;
        call.onSuccessHandler = this;
        call.novelId = novelID;
        call.novelName = novelName;
        call.reviewText = reviewText;
        call.prompt = prompt;
        call.aiFeedback = aiFeedbackString;
        call.SendRequest();
        DontDestroyOnLoad(call.gameObject);
    }

    public void OnSuccess(Response response)
    {
        //Nothing to do here
    }
}
