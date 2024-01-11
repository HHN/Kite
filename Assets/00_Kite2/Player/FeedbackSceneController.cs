using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FeedbackSceneController : SceneController, OnSuccessHandler
{
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI novelTitle;
    public GameObject gptServercallPrefab;
    public FavoriteButton favoriteButton;
    private VisualNovel novelToPlay;
    [SerializeField] private RectTransform layout;

    private void Start()
    {
        BackStackManager.Instance().Push(SceneNames.FEEDBACK_SCENE);

        novelToPlay = PlayManager.Instance().GetVisualNovelToPlay();

        if (novelToPlay == null)
        {
            return;
        }
        novelTitle.SetText(novelToPlay.title);
        favoriteButton.novel = novelToPlay;
        favoriteButton.Init();
        if (string.IsNullOrEmpty(novelToPlay.feedback))
        {
            feedbackText.SetText("Bitte warten, Feedback wird geladen...");
            GetCompletionServerCall call = Instantiate(gptServercallPrefab).GetComponent<GetCompletionServerCall>();
            call.sceneController = this;
            call.onSuccessHandler = this;
            call.prompt = PromptManager.Instance().GetPrompt();
            call.SendRequest();
            DontDestroyOnLoad(call.gameObject);
            return;
        }
        feedbackText.SetText(novelToPlay.feedback);
    }

    public void OnFinishButton()
    {
        AnalyticsServiceHandler.Instance().SendWaitedForAIFeedback();

        int userRole = FeedbackRoleManager.Instance.GetFeedbackRole();
        if (userRole == 2 || userRole == 3 || userRole == 4 || userRole == 5)
        {
            SceneLoader.LoadReviewAiScene();
        }
        else
        {
            BackStackManager.Instance().Clear(); // we go back to the explorer and don't want
                                                 // the back-button to bring us to the feedback scene aggain
            SceneLoader.LoadNovelExplorerScene();
        }
    }

    public void OnSuccess(Response response)
    {
        if (SceneManager.GetActiveScene().name != SceneNames.FEEDBACK_SCENE 
            && SceneManager.GetActiveScene().name != SceneNames.COMMENT_SECTION_SCENE)
        {
            return;
        }
        feedbackText.SetText(response.completion.Trim());
        novelToPlay.feedback = (response.completion.Trim());
        AnalyticsServiceHandler.Instance().SetWaitedForAiFeedbackTrue();
        LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
    }
}
