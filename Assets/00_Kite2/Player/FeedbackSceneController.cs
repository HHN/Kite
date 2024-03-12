using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FeedbackSceneController : SceneController, OnSuccessHandler, OnErrorHandler
{
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private TextMeshProUGUI novelTitle;
    [SerializeField] private GameObject gptServercallPrefab;
    [SerializeField] private FavoriteButton favoriteButton;
    [SerializeField] private VisualNovel novelToPlay;
    [SerializeField] private RectTransform layout;
    [SerializeField] private AudioSource waitingLoopMusic;
    [SerializeField] private AudioSource resultMusic;

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
            StartWaitingMusic();
            VisualNovel novel = PlayManager.Instance().GetVisualNovelToPlay();
            feedbackText.SetText("Bitte warten, Feedback wird geladen...");
            GetCompletionServerCall call = Instantiate(gptServercallPrefab).GetComponent<GetCompletionServerCall>();
            call.sceneController = this;
            call.onSuccessHandler = this;
            call.onErrorHandler = this;
            if (novel != null)
            {
                call.prompt = PromptManager.Instance().GetPrompt(novel.context);
            } else
            {
                call.prompt = PromptManager.Instance().GetPrompt("");
            }
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
        StopWaitingMusic();
        feedbackText.SetText(response.GetCompletion().Trim());
        novelToPlay.feedback = (response.GetCompletion().Trim());
        AnalyticsServiceHandler.Instance().SetWaitedForAiFeedbackTrue();
        LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
        PlayResultMusic();
    }

    public void OnError(Response response)
    {
        StopWaitingMusic();
        DisplayErrorMessage(ErrorMessages.UNEXPECTED_SERVER_ERROR);
    }

    public void StartWaitingMusic()
    {
        waitingLoopMusic.Play();
    }

    public void StopWaitingMusic()
    {
        waitingLoopMusic.Stop();
    }

    public void PlayResultMusic()
    {
        resultMusic.Play();
    }
}
