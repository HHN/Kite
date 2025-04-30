using System;
using System.Collections;
using System.Globalization;
using Assets._Scripts.Managers;
using Assets._Scripts.Messages;
using Assets._Scripts.Novel;
using Assets._Scripts.OfflineAiFeedback;
using Assets._Scripts.Player;
using Assets._Scripts.SaveNovelData;
using Assets._Scripts.SceneManagement;
using Assets._Scripts.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets._Scripts.ServerCommunication;
using Assets._Scripts.ServerCommunication.ServerCalls;
using System.Runtime.InteropServices;

namespace Assets._Scripts.Controller.SceneControllers
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

        #if UNITY_WEBGL && !UNITY_EDITOR
            [DllImport("__Internal")]
            private static extern void CopyTextToClipboard(string text);
        #endif

        private readonly CultureInfo _culture = new CultureInfo("de-DE");

        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.FeedbackScene);

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
                feedbackText.SetText("Das Feedback wird gerade geladen. Dies dauert durchschnittlich zwischen 30 und 60 Sekunden. Solltest du nicht so lange warten wollen, kannst du dir das Feedback einfach im Archiv anschauen, sobald es fertig ist." +
                                     "\n" +
                                     "\n" +
                                     "<align=center><i>Hinweis:</i> Analyse und Feedback wurden durch KI künstlich erzeugt. Eine individuelle Beratung wird hierdurch nicht ersetzt.</align>");
                GetCompletionServerCall call = Instantiate(gptServercallPrefab).GetComponent<GetCompletionServerCall>();
                call.sceneController = this;

                string dialog = PromptManager.Instance().GetDialog();

#if UNITY_WEBGL
                Application.ExternalCall("logMessage", "string dialog = PromptManager.Instance().GetDialog();" + dialog);
#endif

                dialog = dialog.Replace("Bitte beachte, dass kein Teil des Dialogs in das Feedback darf.", "");

                FeedbackHandler feedbackHandler = new FeedbackHandler()
                {
                    FeedbackSceneController = this,
                    ID = PlayManager.Instance().GetVisualNovelToPlay().id,
                    Dialog = dialog
                };

                call.OnSuccessHandler = feedbackHandler;
                call.OnErrorHandler = this;

                call.prompt = PromptManager.Instance().GetPrompt(novel != null ? novel.context : "");

                call.SendRequest();
                DontDestroyOnLoad(call.gameObject);
                
                string novelId = novelToPlay.id.ToString();
                NovelSaveData savedData = SaveLoadManager.Load(novelId);

                if (savedData == null)
                {
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

            #if UNITY_IOS
                    TextToSpeechManager.Instance.CancelSpeak();
            #endif
            
            BackStackManager.Instance().Clear(); // we go back to the explorer and don't want the back-button to bring us to the feedback scene again
            SceneLoader.LoadFoundersBubbleScene();
        }

        public void OnCopyButton()
        {
        #if UNITY_WEBGL && !UNITY_EDITOR
                CopyTextToClipboard(feedbackText.text);
        #else
                    GUIUtility.systemCopyBuffer = feedbackText.text;
        #endif
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
            if (SceneManager.GetActiveScene().name != SceneNames.FeedbackScene)
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
            Debug.Log("RESPONSE: " + response);
            dialogHistoryEntry.SetCompletion(response.Trim());
            DateTime now = DateTime.Now;
            string formattedDateTime = now.ToString("ddd | dd.MM.yyyy | HH:mm", _culture);
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
        
        private readonly CultureInfo _culture = new CultureInfo("de-DE");

        public void OnSuccess(Response response)
        {
#if UNITY_WEBGL
                Application.ExternalCall("logMessage", "public void OnSuccess(Response response)" + response);
#endif
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
            Debug.Log("DIALOG: " + Dialog);
#if UNITY_WEBGL
                Application.ExternalCall("logMessage", Dialog);
#endif
            dialogHistoryEntry.SetCompletion(response.Trim());
            DateTime now = DateTime.Now;
            string formattedDateTime = now.ToString("ddd | dd.MM.yyyy | HH:mm", _culture);
            dialogHistoryEntry.SetDateAndTime(formattedDateTime);
            DialogHistoryManager.Instance().AddEntry(dialogHistoryEntry);
        }
    }
}