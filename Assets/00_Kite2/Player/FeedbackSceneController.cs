using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using LeastSquares.Overtone;
using System.Collections;
using System;
using System.Globalization;
using System.Collections.Generic;

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
            finishButtonContainer.SetActive(false);
            finishButtonBottomContainer.SetActive(true);
            return;
        }
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

    public IEnumerator LoadOnlineFeedback(VisualNovelNames visualNovelNames)
    {
        Debug.Log("Loading Offline Feedback");

        feedbackText.SetText("Bitte warten, Feedback wird geladen...");
        offlineFeedbackLoader.LoadOfflineFeedbackForNovel(visualNovelNames);
        
        while (!PreGeneratedOfflineFeedbackManager.Instance().IsFeedbackLoaded(visualNovelNames))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Dictionary<string, FeedbackNodeContainer> feedbackDictionary = PreGeneratedOfflineFeedbackManager.Instance().GetPreGeneratedOfflineFeedback(visualNovelNames);

        string feedback = feedbackDictionary[novelToPlay.GetPlayedPath()].completion;

        StopWaitingMusic();
        finishButtonContainer.SetActive(false);
        finishButtonBottomContainer.SetActive(true);
        if (TextToSpeechManager.Instance().IsTextToSpeechActivated())
        {
            TextToSpeechService.Instance().TextToSpeechReadLive(feedback, engine);
        }
        feedbackText.SetText(feedback.Trim());
        novelToPlay.feedback = (feedback.Trim());
        PlayerDataManager.Instance().SaveEvaluation(novelToPlay.title, feedback.Trim());
        AnalyticsServiceHandler.Instance().SetWaitedForAiFeedbackTrue();
        LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
        PlayResultMusic();
        //requestExpertFeedbackButton.interactable = true;

        Coroutine coroutine = StartCoroutine(SaveDialogToHistory(feedback));
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
        finishButtonContainer.SetActive(false);
        finishButtonBottomContainer.SetActive(true);
        if (TextToSpeechManager.Instance().IsTextToSpeechActivated())
        {
            TextToSpeechService.Instance().TextToSpeechReadLive(response.GetCompletion().Trim(), engine);
        }
        feedbackText.SetText(response.GetCompletion().Trim());
        novelToPlay.feedback = (response.GetCompletion().Trim());
        PlayerDataManager.Instance().SaveEvaluation(novelToPlay.title, response.GetCompletion().Trim());
        AnalyticsServiceHandler.Instance().SetWaitedForAiFeedbackTrue();
        LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
        PlayResultMusic();
        //requestExpertFeedbackButton.interactable = true;

        Coroutine coroutine = StartCoroutine(SaveDialogToHistory(response.GetCompletion()));
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
