using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using LeastSquares.Overtone;
using System.Collections;
using System;
using System.Globalization;

public class FeedbackSceneController : SceneController, OnSuccessHandler, OnErrorHandler
{
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private OfflineFeedbackLoader offlineFeedbackLoader;
    [SerializeField] private GameObject gptServercallPrefab;
    [SerializeField] private VisualNovel novelToPlay;
    [SerializeField] private RectTransform layout;
    [SerializeField] private AudioSource waitingLoopMusic;
    [SerializeField] private AudioSource resultMusic;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Button finishButton;
    [SerializeField] private Button finishButtonBottom;
    [SerializeField] private GameObject finishButtonContainer;
    [SerializeField] private GameObject finishButtonBottomContainer;
    [SerializeField] private TTSEngine engine;
    [SerializeField] private GameObject loadingAnimation;

    [SerializeField] private GameObject progressBarObject; // Das GameObject, das das Image enthält
    private Image progressBar;
    private float fillDuration = 40f;
    private bool responseReceived = false;
    private Coroutine fillCoroutine;

    private void Start()
    {
        BackStackManager.Instance().Push(SceneNames.FEEDBACK_SCENE);

        novelToPlay = PlayManager.Instance().GetVisualNovelToPlay();

        if (novelToPlay == null)
        {
            return;
        }

        if (ApplicationModeManager.Instance().IsOfflineModeActive())
        {
            feedbackText.SetText("Sie befinden sich im Offline Modus. Es ist kein Feedback verfügbar.");
            loadingAnimation.SetActive(false);
            finishButtonContainer.SetActive(false);
            finishButtonBottomContainer.SetActive(true);
            return;
        }
        if (string.IsNullOrEmpty(novelToPlay.feedback))
        {
            StartWaitingMusic();
            GetProgressBarAndStartCoroutine();
            VisualNovel novel = PlayManager.Instance().GetVisualNovelToPlay();
            feedbackText.SetText("Bitte warten, Feedback wird geladen...");
            GetCompletionServerCall call = Instantiate(gptServercallPrefab).GetComponent<GetCompletionServerCall>();
            call.sceneController = this;
            FeedbackHandler feedbackHandler = new FeedbackHandler()
            {
                feedbackSceneController = this,
                id = PlayManager.Instance().GetVisualNovelToPlay().id,
                dialog = PromptManager.Instance().GetDialog()
            };
            call.onSuccessHandler = feedbackHandler;
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
        loadingAnimation.SetActive(false);
    }

    private void GetProgressBarAndStartCoroutine()
    {
        // Hole die Image-Komponente vom angegebenen GameObject
        if (progressBarObject != null)
        {
            progressBar = progressBarObject.GetComponent<Image>();
        }
        if (progressBar == null)
        {
            Debug.LogError("Das angegebene GameObject enthält kein Image-Component.");
            return;
        }
        fillCoroutine = StartCoroutine(FillProgressBar());
    }

    private IEnumerator FillProgressBar()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fillDuration && !responseReceived)
        {
            elapsedTime += Time.deltaTime;
            float fillAmount = Mathf.Clamp01(elapsedTime / fillDuration);
            progressBar.fillAmount = fillAmount;
            yield return null;
        }

        if (!responseReceived)
        {
            progressBar.fillAmount = 0.99f;
        }
    }

    public void OnFinishButton()
    {
        AnalyticsServiceHandler.Instance().SendWaitedForAIFeedback();

        int userRole = FeedbackRoleManager.Instance.GetFeedbackRole();
        if ((userRole == 2 || userRole == 3 || userRole == 4 || userRole == 5) && ApplicationModeManager.Instance().IsOnlineModeActive())
        {
            SceneLoader.LoadReviewAiScene();
        }
        else
        {
            BackStackManager.Instance().Clear(); // we go back to the explorer and don't want
                                                 // the back-button to bring us to the feedback scene aggain
            SceneLoader.LoadFoundersBubbleScene();
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

        responseReceived = true;
        if (fillCoroutine != null)
        {
            StopCoroutine(fillCoroutine);
        }
        StartCoroutine(FillBarInstantly());

        finishButtonContainer.SetActive(false);
        finishButtonBottomContainer.SetActive(true);
        if (TextToSpeechManager.Instance().IsTextToSpeechActivated())
        {
            TextToSpeechService.Instance().TextToSpeechReadLive(response.GetCompletion().Trim(), engine);
        }
        feedbackText.SetText(response.GetCompletion().Trim());
        loadingAnimation.SetActive(false);
        novelToPlay.feedback = (response.GetCompletion().Trim());
        PlayerDataManager.Instance().SaveEvaluation(novelToPlay.title, response.GetCompletion().Trim());
        AnalyticsServiceHandler.Instance().SetWaitedForAiFeedbackTrue();
        LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
        PlayResultMusic();
        //requestExpertFeedbackButton.interactable = true;
    }

    public IEnumerator SaveDialogToHistory(string response)
    {
        DialogHistoryEntry dialogHistoryEntry = new DialogHistoryEntry();
        dialogHistoryEntry.SetNovelId(PlayManager.Instance().GetVisualNovelToPlay().id);
        dialogHistoryEntry.SetDialog(PromptManager.Instance().GetDialog());
        dialogHistoryEntry.SetCompletion(response.Trim());
        DateTime now = DateTime.Now;
        CultureInfo culture = new CultureInfo("de-DE");
        string formattedDateTime = now.ToString("dddd | dd.MM.yyyy | HH:mm", culture);
        dialogHistoryEntry.SetDateAndTime(formattedDateTime); DialogHistoryManager.Instance().AddEntry(dialogHistoryEntry);
        yield return null;
    }

    public void OnError(Response response)
    {
        StopWaitingMusic();
        DisplayErrorMessage(ErrorMessages.UNEXPECTED_SERVER_ERROR);
        finishButtonContainer.SetActive(false);
        finishButtonBottomContainer.SetActive(true);
        feedbackText.SetText("Leider ist aktuell keine KI-Analyse verfügbar.");
        loadingAnimation.SetActive(false);
    }

    private IEnumerator FillBarInstantly()
    {
        float fillSpeed = 2f; // Geschwindigkeit des schnellen Füllens
        while (progressBar.fillAmount < 1f)
        {
            progressBar.fillAmount += fillSpeed * Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        progressBarObject.SetActive(false);
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

    public void DeactivateAskButton()
    {
        //requestExpertFeedbackButton.interactable = false;
    }
}

public class FeedbackHandler : OnSuccessHandler
{
    public FeedbackSceneController feedbackSceneController;
    public long id;
    public string dialog;

    public void OnSuccess(Response response)
    {
        SaveDialogToHistory(response.GetCompletion());

        if (!DestroyValidator.IsNullOrDestroyed(feedbackSceneController))
        {
            feedbackSceneController.OnSuccess(response);
        }
    }

    public void SaveDialogToHistory(string response)
    {
        DialogHistoryEntry dialogHistoryEntry = new DialogHistoryEntry();
        dialogHistoryEntry.SetNovelId(id);
        dialogHistoryEntry.SetDialog(dialog);
        dialogHistoryEntry.SetCompletion(response.Trim());
        DateTime now = DateTime.Now;
        CultureInfo culture = new CultureInfo("de-DE");
        string formattedDateTime = now.ToString("dddd | dd.MM.yyyy | HH:mm", culture);
        dialogHistoryEntry.SetDateAndTime(formattedDateTime); DialogHistoryManager.Instance().AddEntry(dialogHistoryEntry);
        Debug.Log("Feedback Saved in Novel Archive");
    }
}