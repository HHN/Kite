using System;
using System.Collections;
using System.Globalization;
using _00_Kite2.Common;
using _00_Kite2.Common.Managers;
using _00_Kite2.Common.Messages;
using _00_Kite2.Common.Novel;
using _00_Kite2.Common.SceneManagement;
using _00_Kite2.Common.Utilities;
using _00_Kite2.OfflineAiFeedback;
using _00_Kite2.SaveNovelData;
using _00_Kite2.Server_Communication;
using _00_Kite2.Server_Communication.Server_Calls;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _00_Kite2.Player
{
    public class FeedbackSceneController : SceneController, IOnSuccessHandler, IOnErrorHandler
    {
        [SerializeField] private TextMeshProUGUI feedbackText;
        [SerializeField] private OfflineFeedbackLoader offlineFeedbackLoader;
        [SerializeField] private GameObject gptServercallPrefab;
        [SerializeField] private VisualNovel novelToPlay;
        [SerializeField] private RectTransform layout;
        [SerializeField] private AudioClip waitingLoopMusic;
        [SerializeField] private AudioClip resultMusic;
        [SerializeField] private Button finishButton;
        [SerializeField] private Button finishButtonTop;
        [SerializeField] private Button finishButtonBottom;
        [SerializeField] private Button copyButton;
        [SerializeField] private GameObject finishButtonContainer;
        [SerializeField] private GameObject finishButtonTopContainer;
        [SerializeField] private GameObject finishButtonBottomContainer;
        [SerializeField] private GameObject copyButtonContainer;
        [SerializeField] private GameObject copyNotificationContainer;
        //[SerializeField] private TTSEngine engine;
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
                finishButtonTopContainer.SetActive(true);
                finishButtonBottomContainer.SetActive(true);
                return;
            }

            if (string.IsNullOrEmpty(novelToPlay.feedback))
            {
                StartWaitingMusic();
                VisualNovel novel = PlayManager.Instance().GetVisualNovelToPlay();
                feedbackText.SetText(
                    "Das Feedback wird gerade geladen. Dies dauert durchschnittlich zwischen 30 und 60 Sekunden. Solltest du nicht so lange warten wollen, kannst du dir das Feedback einfach im Archiv anschauen, sobald es fertig ist.");
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
                
                Debug.Log(dialog);

                call.OnSuccessHandler = feedbackHandler;
                call.OnErrorHandler = this;

                call.prompt = PromptManager.Instance().GetPrompt(novel != null ? novel.context : "");

                call.SendRequest();
                DontDestroyOnLoad(call.gameObject);
                
                string novelId = novelToPlay.id.ToString();
                NovelSaveData savedData = SaveLoadManager.Load(novelId);

                if (savedData == null)
                {
                    Debug.LogWarning("No saved data found for the novel.");
                    return;
                }
            
                SaveLoadManager.DeleteNovelSaveData(novelId);
                
                return;
            }

            feedbackText.SetText(novelToPlay.feedback);
            loadingAnimation.SetActive(false);
        }

        public void OnFinishButton()
        {
            AnalyticsServiceHandler.Instance().SendWaitedForAIFeedback();

            int userRole = FeedbackRoleManager.Instance.GetFeedbackRole();
            if (userRole is 2 or 3 or 4 or 5 && ApplicationModeManager.Instance().IsOnlineModeActive())
            {
                SceneLoader.LoadReviewAiScene();
            }
            else
            {
                BackStackManager.Instance()
                    .Clear(); // we go back to the explorer and don't want the back-button to bring us to the feedback scene again
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
            finishButtonTopContainer.SetActive(true);
            finishButtonBottomContainer.SetActive(true);
            copyButtonContainer.SetActive(true);

            string completion = response.GetCompletion().Replace("#", "").Replace("*", "").Trim();

            StartCoroutine(TextToSpeechManager.Instance.Speak(completion));

            feedbackText.SetText(completion);
            loadingAnimation.SetActive(false);
            novelToPlay.feedback = (completion);
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
            dialogHistoryEntry.SetDateAndTime(formattedDateTime);
            DialogHistoryManager.Instance().AddEntry(dialogHistoryEntry);
            yield return null;
        }

        public void OnError(Response response)
        {
            StopWaitingMusic();
            DisplayErrorMessage(ErrorMessages.UNEXPECTED_SERVER_ERROR);
            finishButtonContainer.SetActive(false);
            finishButtonTopContainer.SetActive(true);
            finishButtonBottomContainer.SetActive(true);
            feedbackText.SetText("Leider ist aktuell keine KI-Analyse verfügbar.");
            loadingAnimation.SetActive(false);
        }

        private void StartWaitingMusic()
        {
            GlobalVolumeManager.Instance.PlaySound(waitingLoopMusic, true);
        }

        private void StopWaitingMusic()
        {
            GlobalVolumeManager.Instance.StopSound();
        }

        private void PlayResultMusic()
        {
            GlobalVolumeManager.Instance.PlaySound(resultMusic);
        }

        public void DeactivateAskButton()
        {
            //requestExpertFeedbackButton.interactable = false;
        }
    }

    public class FeedbackHandler : IOnSuccessHandler
    {
        public FeedbackSceneController FeedbackSceneController;
        public long ID;
        public string Dialog;

        public void OnSuccess(Response response)
        {
            SaveDialogToHistory(response.GetCompletion());

            if (!FeedbackSceneController.IsNullOrDestroyed())
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
            dialogHistoryEntry.SetDateAndTime(formattedDateTime);
            DialogHistoryManager.Instance().AddEntry(dialogHistoryEntry);
            
            Debug.Log("Feedback Saved in Novel Archive");
        }
    }
}