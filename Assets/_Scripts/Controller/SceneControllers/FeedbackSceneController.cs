using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
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
using UnityEngine.Scripting;

namespace Assets._Scripts.Controller.SceneControllers
{
    [Preserve]
    public class FeedbackSceneController : SceneController, IOnSuccessHandler, IOnErrorHandler
    {
        [SerializeField] private TextMeshProUGUI feedbackText;
        [SerializeField] private TextMeshProUGUI hintText;
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
        [SerializeField] private GameObject loadingAnimation;

        [SerializeField] private ScrollRect feedbackScrollRect;
        [SerializeField] private float wheelScrollSpeed = 0.2f;

    #if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void CopyTextToClipboard(string text);
    #endif

        private readonly CultureInfo _culture = new CultureInfo("de-DE");

        private VisualNovel _novel;
        private string _dialog;

        private void Start()
        {
            // BackStackManager.Instance().Clear();
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
                _novel = PlayManager.Instance().GetVisualNovelToPlay();
                feedbackText.SetText("Das Feedback wird gerade geladen. Dies dauert durchschnittlich zwischen 30 und 60 Sekunden. Solltest du nicht so lange warten wollen, kannst du dir das Feedback einfach im Archiv anschauen, sobald es fertig ist.");
                hintText.SetText("Hinweis: Analyse und Feedback wurden durch KI künstlich erzeugt. Eine individuelle Beratung wird hierdurch nicht ersetzt.");
                GetCompletionServerCall call = Instantiate(gptServercallPrefab).GetComponent<GetCompletionServerCall>();
                call.sceneController = this;

                _dialog = PromptManager.Instance().GetDialog();

                _dialog = _dialog.Replace("Bitte beachte, dass kein Teil des Dialogs in das Feedback darf.", "");

                FeedbackHandler feedbackHandler = new FeedbackHandler()
                {
                    FeedbackSceneController = this,
                    ID = PlayManager.Instance().GetVisualNovelToPlay().id,
                    Dialog = _dialog
                };

                call.OnSuccessHandler = feedbackHandler;
                call.OnErrorHandler = this;

                call.prompt = PromptManager.Instance().GetPrompt(_novel != null ? _novel.context : "");

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

        private void Update()
        {
            // Mouse wheel vertical scroll support
            float wheel = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(wheel) > 0f && feedbackScrollRect != null)
            {
                float pos = feedbackScrollRect.verticalNormalizedPosition + wheel * wheelScrollSpeed;
                feedbackScrollRect.verticalNormalizedPosition = Mathf.Clamp01(pos);
            }
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

            // Neuer Nullcheck und Auto-Find für layout
            if (layout == null)
            {
                GameObject contentGo = GameObject.Find("Content");
                if (contentGo != null)
                    layout = contentGo.GetComponent<RectTransform>();
            }
            if (layout != null)
                LayoutRebuilder.ForceRebuildLayoutImmediate(layout);

            PlayResultMusic();
            FontSizeManager.Instance().UpdateAllTextComponents();
            //requestExpertFeedbackButton.interactable = true;
        }

        public void OnError(Response response)
        {
            if (SceneManager.GetActiveScene().name != SceneNames.FeedbackScene)
            {
                return;
            }
            
            StopWaitingMusic();
            DisplayErrorMessage(ErrorMessages.UNEXPECTED_SERVER_ERROR);
            finishButtonContainer.SetActive(false);
            finishButtonTopContainer.SetActive(true);
            finishButtonBottomContainer.SetActive(true);
            
            string completion = "Keine Analyse verfügbar – Serverfehler.";
            
            StartCoroutine(TextToSpeechManager.Instance.Speak(completion));
            
            feedbackText.SetText("Leider ist aktuell keine KI-Analyse verfügbar.");
            loadingAnimation.SetActive(false);
            novelToPlay.feedback = completion;
            
            SaveDialogToHistory(completion);
            
            // Neuer Nullcheck und Auto-Find für layout
            if (layout == null)
            {
                GameObject contentGo = GameObject.Find("Content");
                if (contentGo != null)
                    layout = contentGo.GetComponent<RectTransform>();
            }
            if (layout != null)
                LayoutRebuilder.ForceRebuildLayoutImmediate(layout);

            PlayResultMusic();
            FontSizeManager.Instance().UpdateAllTextComponents();
        }
      
        private void SaveDialogToHistory(string response)
        {
            DialogHistoryEntry dialogHistoryEntry = new DialogHistoryEntry();
            dialogHistoryEntry.SetNovelId(_novel.id);
            dialogHistoryEntry.SetDialog(_dialog);
            dialogHistoryEntry.SetCompletion(response.Trim());
            DateTime now = DateTime.Now;
            string formattedDateTime = now.ToString("ddd | dd.MM.yyyy | HH:mm", _culture);
            dialogHistoryEntry.SetDateAndTime(formattedDateTime);
            DialogHistoryManager.Instance().AddEntry(dialogHistoryEntry);
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
    }

    public class FeedbackHandler : IOnSuccessHandler
    {
        public FeedbackSceneController FeedbackSceneController;
        public long ID;
        public string Dialog;
        
        private readonly CultureInfo _culture = new CultureInfo("de-DE");

        public void OnSuccess(Response response)
        {
            SaveDialogToHistory(response.GetCompletion());

            if (!FeedbackSceneController.IsNullOrDestroyed())
            {
                FeedbackSceneController.OnSuccess(response);
            }
        }
        
        public void OnError(Response message)
        {
            SaveDialogToHistory(message.GetCompletion());

            if (!FeedbackSceneController.IsNullOrDestroyed())
            {
                FeedbackSceneController.OnError(message);
            }
        }

        private void SaveDialogToHistory(string response)
        {
            DialogHistoryEntry dialogHistoryEntry = new DialogHistoryEntry();
            dialogHistoryEntry.SetNovelId(ID);
            dialogHistoryEntry.SetDialog(Dialog);
            dialogHistoryEntry.SetCompletion(response.Trim());
            DateTime now = DateTime.Now;
            string formattedDateTime = now.ToString("ddd | dd.MM.yyyy | HH:mm", _culture);
            dialogHistoryEntry.SetDateAndTime(formattedDateTime);
            DialogHistoryManager.Instance().AddEntry(dialogHistoryEntry);
        }
    }
}
