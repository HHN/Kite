using System;
using System.Collections;
using System.Globalization;
using LeastSquares.Overtone;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _00_Kite2.Player
{
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
        [SerializeField] private Button copyButton;
        [SerializeField] private GameObject finishButtonContainer;
        [SerializeField] private GameObject finishButtonBottomContainer;
        [SerializeField] private GameObject copyButtonContainer;
        [SerializeField] private GameObject copyNotificationContainer;
        [SerializeField] private TTSEngine engine;
        [SerializeField] private GameObject loadingAnimation;

        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.FEEDBACK_SCENE);

            FontSizeManager.Instance().UpdateAllTextComponents();

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
                VisualNovel novel = PlayManager.Instance().GetVisualNovelToPlay();
                feedbackText.SetText("Das Feedback wird gerade geladen. Dies dauert durchschnittlich zwischen 30 und 60 Sekunden. Solltest du nicht so lange warten wollen, kannst du dir das Feedback einfach im Archiv anschauen, sobald es fertig ist.");
                GetCompletionServerCall call = Instantiate(gptServercallPrefab).GetComponent<GetCompletionServerCall>();
                call.sceneController = this;

                string dialog = PromptManager.Instance().GetDialog();
                dialog = dialog.Replace("Bitte beachte, dass kein Teil des Dialogs in das Feedback darf.", "");

                FeedbackHandler feedbackHandler = new FeedbackHandler()
                {
                    FeedbackSceneController = this,
                    ID = PlayManager.Instance().GetVisualNovelToPlay().id,
                    Dialog = dialog
                };

                Debug.Log("feedbackHandler.dialog: " + feedbackHandler.Dialog);

                call.onSuccessHandler = feedbackHandler;
                call.onErrorHandler = this;

                if (novel != null)
                {
                    //Debug.Log("novel.context: " + novel.context);
                    call.prompt = PromptManager.Instance().GetPrompt(novel.context);
                    //Debug.Log("call.prompt: " + call.prompt);
                } 
                else
                {
                    call.prompt = PromptManager.Instance().GetPrompt("");
                }

                call.SendRequest();
                DontDestroyOnLoad(call.gameObject);
                return;
            }

            //Debug.Log("novelToPlay.feedback: " + novelToPlay.feedback);
            feedbackText.SetText(novelToPlay.feedback);
            loadingAnimation.SetActive(false);
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
                // the back-button to bring us to the feedback scene again
                SceneLoader.LoadFoundersBubbleScene();
            }
        }

        public void OnCopyButton()
        {
            GUIUtility.systemCopyBuffer = feedbackText.text;
            StartCoroutine(ShowCopyPopup());
        }

        private IEnumerator ShowCopyPopup()
        {
            // Popup aktivieren
            copyNotificationContainer.SetActive(true);

            // Warte die angegebene Zeit (z. B. 2 Sekunden)
            yield return new WaitForSeconds(2);

            // Popup ausblenden
            copyNotificationContainer.SetActive(false);
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
            copyButtonContainer.SetActive(true);
            if (TextToSpeechManager.Instance.IsTextToSpeechActivated())
            {
                TextToSpeechService.Instance().TextToSpeechReadLive(response.GetCompletion().Trim(), engine);
            }
            feedbackText.SetText(response.GetCompletion().Trim());
            loadingAnimation.SetActive(false);
            novelToPlay.feedback = (response.GetCompletion().Trim());
            //PlayerDataManager.Instance().SaveEvaluation(novelToPlay.title, response.GetCompletion().Trim());
            AnalyticsServiceHandler.Instance().SetWaitedForAiFeedbackTrue();
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
            PlayResultMusic();
            FontSizeManager.Instance().UpdateAllTextComponents();
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
            string formattedDateTime = now.ToString("ddd | dd.MM.yyyy | HH:mm", culture);
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

        private void StartWaitingMusic()
        {
            waitingLoopMusic.Play();
        }

        private void StopWaitingMusic()
        {
            waitingLoopMusic.Stop();
        }

        private void PlayResultMusic()
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
        public FeedbackSceneController FeedbackSceneController;
        public long ID;
        public string Dialog;

        public void OnSuccess(Response response)
        {
            SaveDialogToHistory(response.GetCompletion());

            if (!DestroyValidator.IsNullOrDestroyed(FeedbackSceneController))
            {
                FeedbackSceneController.OnSuccess(response);
            }
        }

        private void SaveDialogToHistory(string response)
        {
            DialogHistoryEntry dialogHistoryEntry = new DialogHistoryEntry();
            dialogHistoryEntry.SetNovelId(ID);
            dialogHistoryEntry.SetDialog(Dialog);
            dialogHistoryEntry.SetCompletion(response.Trim());
            DateTime now = DateTime.Now;
            CultureInfo culture = new CultureInfo("de-DE");
            string formattedDateTime = now.ToString("ddd | dd.MM.yyyy | HH:mm", culture);
            dialogHistoryEntry.SetDateAndTime(formattedDateTime); DialogHistoryManager.Instance().AddEntry(dialogHistoryEntry);
            Debug.Log("Feedback Saved in Novel Archive");
        }
    }
}