using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using Assets._Scripts.Managers;
using Assets._Scripts.Messages;
using Assets._Scripts.Novel;
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
    /// <summary>
    /// FeedbackSceneController is responsible for managing the feedback scene's behavior and interactions.
    /// </summary>
    /// <remarks>
    /// This class extends the <see cref="SceneController"/> and implements the <see cref="IOnSuccessHandler"/>
    /// and <see cref="IOnErrorHandler"/> interfaces, enabling it to handle successful or erroneous server responses.
    /// </remarks>
    /// <example>
    /// Use this class to handle button interactions, manage UI visibility, and process text-to-speech actions
    /// in the feedback scene. Handles operations like copying text, navigating back, and playing audio feedback.
    /// </example>
    [Preserve]
    public class FeedbackSceneController : SceneController, IOnSuccessHandler, IOnErrorHandler
    {
        [SerializeField] private TextMeshProUGUI feedbackLinkText;
        [SerializeField] private TextMeshProUGUI feedbackText;
        [SerializeField] private TextMeshProUGUI hintText;
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
        
        private static readonly string LinkColor = "#F5944E";

    #if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void CopyTextToClipboard(string text);
    #endif

        private readonly CultureInfo _culture = new CultureInfo("de-DE");

        private VisualNovel _novel;
        private string _dialog;

        /// <summary>
        /// Initializes the feedback scene by performing the following actions:
        /// - Adds the current scene to the back stack for navigation support.
        /// - Updates all text components with the latest font size configurations.
        /// - Identifies and retrieves the visual novel to be played.
        /// - Handles offline mode scenarios by showing appropriate messages and configuring UI elements.
        /// - Manages and displays feedback content, either by fetching it from the server or using pre-existing data.
        /// </summary>
        private void Start()
        {
            // Add current scene to back stack for navigation
            BackStackManager.Instance().Push(SceneNames.FeedbackScene);

            // Update text components with current font size settings
            FontSizeManager.Instance().UpdateAllTextComponents();

            // Get the visual novel that should be played
            novelToPlay = PlayManager.Instance().GetVisualNovelToPlay();

            if (novelToPlay == null)
            {
                return;
            }

            // If no feedback exists yet, request it from the server
            if (string.IsNullOrEmpty(novelToPlay.feedback))
            {
                StartWaitingMusic();
                _novel = PlayManager.Instance().GetVisualNovelToPlay();
                feedbackLinkText.SetText("Erzähl uns fünf Minuten was Dir aufgefallen ist – dein Feedback hilft uns, KITE II weiterzuentwickeln.\n" +
                                         $"<color={LinkColor}><link=\"https://9bxji5742ys.typeform.com/to/bNxpDQWc\"><u>[Zur Umfrage]</u></link></color>");
                feedbackText.SetText("Das Feedback wird gerade geladen. Dies dauert durchschnittlich zwischen 30 und 60 Sekunden. Solltest du nicht so lange warten wollen, kannst du dir das Feedback einfach im Archiv anschauen, sobald es fertig ist.");
                hintText.SetText("Hinweis: Analyse und Feedback wurden durch KI künstlich erzeugt. Eine individuelle Beratung wird hierdurch nicht ersetzt.");
                
                // Create a server call to get AI feedback
                GetCompletionServerCall call = Instantiate(gptServercallPrefab).GetComponent<GetCompletionServerCall>();
                call.sceneController = this;

                // Get and clean up dialog prompt
                _dialog = PromptManager.Instance().GetDialog();
                _dialog = _dialog.Replace("Bitte beachte, dass kein Teil des Dialogs in das Feedback darf.", "");

                // Setup feedback handler
                FeedbackHandler feedbackHandler = new FeedbackHandler()
                {
                    feedbackSceneController = this,
                    id = PlayManager.Instance().GetVisualNovelToPlay().id,
                    dialog = _dialog
                };

                call.OnSuccessHandler = feedbackHandler;
                call.OnErrorHandler = this;

                // Set prompt and send request
                call.prompt = PromptManager.Instance().GetPrompt(_novel != null ? _novel.context : "");
                call.SendRequest();
                DontDestroyOnLoad(call.gameObject);
                
                // Clean up saved data
                string novelId = novelToPlay.id.ToString();
                NovelSaveData savedData = SaveLoadManager.Load(novelId);

                if (savedData == null)
                {
                    return;
                }
            
                SaveLoadManager.DeleteNovelSaveData(novelId);
                
                return;
            }

            // If feedback already exists, display it
            feedbackText.SetText(novelToPlay.feedback);
            loadingAnimation.SetActive(false);
        }

        /// <summary>
        /// Handles vertical scrolling using the mouse wheel for the feedback scene.
        /// Adjusts the position of the feedback content displayed within the scroll rect
        /// based on the user's scroll input, ensuring the vertical position remains
        /// between the minimum and maximum allowed values.
        /// </summary>
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

        /// <summary>
        /// Handles the actions performed when the finish button is clicked.
        /// This includes stopping any ongoing text-to-speech actions,
        /// clearing the navigation back stack, and loading the Founder's Bubble scene.
        /// </summary>
        /// <remarks>
        /// This method ensures that the feedback scene is not revisited when navigating back and
        /// transitions the user to the Founder's Bubble scene after handling the necessary cleanup.
        /// </remarks>
        public void OnFinishButton()
        {
        #if UNITY_IOS
                    TextToSpeechManager.Instance.CancelSpeak();
        #endif
                    
            // We go back to the explorer and don't want the back-button to bring us to the feedback scene again
            BackStackManager.Instance().Clear();
            SceneLoader.LoadFoundersBubbleScene();
        }

        /// <summary>
        /// Handles the action of the copy button being clicked.
        /// Copies the feedback text to the system clipboard or invokes the appropriate platform-specific functionality
        /// to achieve the same result. On web builds, uses a custom method for copying to the clipboard.
        /// Additionally, it displays a temporary popup notification indicating the text has been copied successfully.
        /// </summary>
        public void OnCopyButton()
        {
        #if UNITY_WEBGL && !UNITY_EDITOR
                CopyTextToClipboard(feedbackText.text);
        #else
                    GUIUtility.systemCopyBuffer = feedbackText.text;
        #endif
            StartCoroutine(ShowCopyPopup());
        }

        /// <summary>
        /// Displays a popup notification to indicate that the text has been copied to the clipboard, keeping the popup visible for a short duration
        /// before automatically hiding it.
        /// </summary>
        /// <returns>
        /// A coroutine that manages the visibility of the copy notification popup
        /// by showing it, waiting for the defined duration, and then hiding it.
        /// </returns>
        private IEnumerator ShowCopyPopup()
        {
            // Activate popup
            copyNotificationContainer.SetActive(true);

            // Wait for a specified time (e.g., 2 seconds)
            yield return new WaitForSeconds(2);

            // Hide popup
            copyNotificationContainer.SetActive(false);
        }

        /// <summary>
        /// Handles the successful response from the server and processes the feedback content.
        /// Performs operations such as managing UI elements, updating text components, and
        /// playing audio feedback.
        /// </summary>
        /// <param name="response">The response object containing the feedback data and additional information.</param>
        public void OnSuccess(Response response)
        {
            // Check if we're still in the feedback scene
            if (SceneManager.GetActiveScene().name != SceneNames.FeedbackScene)
            {
                return;
            }

            // Stop waiting music and update UI visibility
            StopWaitingMusic();
            finishButtonContainer.SetActive(false);
            finishButtonTopContainer.SetActive(true);
            finishButtonBottomContainer.SetActive(true);
            copyButtonContainer.SetActive(true);

            // Clean up and format completion text
            string completion = response.GetCompletion().Replace("#", "").Replace("*", "").Trim();

            // Start text-to-speech for the feedback
            StartCoroutine(TextToSpeechManager.Instance.Speak(completion));

            // Update UI elements with feedback
            feedbackText.SetText(completion);
            loadingAnimation.SetActive(false);
            novelToPlay.feedback = completion;

            // Find and update layout if not assigned
            if (layout == null)
            {
                GameObject contentGo = GameObject.Find("Content");
                if (contentGo != null)
                    layout = contentGo.GetComponent<RectTransform>();
            }
            if (layout != null)
                LayoutRebuilder.ForceRebuildLayoutImmediate(layout);

            // Play result sound and update text sizes
            PlayResultMusic();
            FontSizeManager.Instance().UpdateAllTextComponents();
        }

        /// <summary>
        /// Handles error responses received from the server and updates the UI elements accordingly.
        /// Performs operations such as managing UI elements, updating text components, and playing audio feedback.
        /// </summary>
        /// <param name="response">The server response object containing data about the error.</param>
        public void OnError(Response response)
        {
            // Check if we're still in the feedback scene
            if (SceneManager.GetActiveScene().name != SceneNames.FeedbackScene)
            {
                return;
            }
            
            // Stop background music and show an error message
            StopWaitingMusic();
            DisplayErrorMessage(ErrorMessages.UNEXPECTED_SERVER_ERROR);
            
            // Update button visibility
            finishButtonContainer.SetActive(false);
            finishButtonTopContainer.SetActive(true);
            finishButtonBottomContainer.SetActive(true);
            
            // Set error feedback text
            string completion = "Keine Analyse verfügbar – Serverfehler.";
            
            // Read error message via text-to-speech
            StartCoroutine(TextToSpeechManager.Instance.Speak(completion));
            
            // Update UI elements to show the error state
            feedbackText.SetText("Leider ist aktuell keine KI-Analyse verfügbar.");
            loadingAnimation.SetActive(false);
            novelToPlay.feedback = completion;
            
            // Save the error dialog to history
            SaveDialogToHistory(completion);
            
            // Find and update layout if not already assigned
            if (layout == null)
            {
                GameObject contentGo = GameObject.Find("Content");
                if (contentGo != null)
                    layout = contentGo.GetComponent<RectTransform>();
            }
            if (layout != null)
                LayoutRebuilder.ForceRebuildLayoutImmediate(layout);

            // Play result sound
            PlayResultMusic();

        }

        /// <summary>
        /// Saves the provided dialog response to the user's dialog history by creating an entry
        /// containing the visual novel ID, dialog content, response completion, and formatted timestamp.
        /// </summary>
        /// <param name="response">The response text to be saved as part of the dialog history entry.</param>
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

        /// <summary>
        /// Uses the global audio manager to play the assigned `waitingLoopMusic` clip in a continuous loop.
        /// </summary>
        private void StartWaitingMusic()
        {
            GlobalVolumeManager.Instance.PlaySound(waitingLoopMusic, true);
        }

        /// <summary>
        /// Stops the currently playing waiting music by invoking the global sound manager.
        /// </summary>
        private void StopWaitingMusic()
        {
            GlobalVolumeManager.Instance.StopSound();
        }

        /// <summary>
        /// Plays the result music, using the global volume manager to handle audio playback.
        /// This method is typically triggered after completing a feedback process or handling an error in the feedback scene.
        /// </summary>
        private void PlayResultMusic()
        {
            GlobalVolumeManager.Instance.PlaySound(resultMusic);
        }
    }

    /// <summary>
    /// FeedbackHandler processes the success and error responses for feedback server calls.
    /// </summary>
    /// <remarks>
    /// This class implements the <see cref="IOnSuccessHandler"/> interface to handle successful server responses.
    /// It also includes error handling logic for unsuccessful call scenarios.
    /// FeedbackHandler interacts closely with the <see cref="feedbackSceneController"/> to manage server responses
    /// tied to the feedback feature.
    /// </remarks>
    public class FeedbackHandler : IOnSuccessHandler
    {
        public FeedbackSceneController feedbackSceneController;
        public long id;
        public string dialog;
        
        private readonly CultureInfo _culture = new("de-DE");

        /// <summary>
        /// Processes the successful response received from the server by performing the following actions:
        /// - Saves the dialog content to the history for record-keeping.
        /// - Notifies the <see cref="feedbackSceneController"/> if it is available, forwarding the response for additional handling.
        /// </summary>
        /// <param name="response">The server response containing the completion data to be processed.</param>
        public void OnSuccess(Response response)
        {
            SaveDialogToHistory(response.GetCompletion());

            if (!feedbackSceneController.IsNullOrDestroyed())
            {
                feedbackSceneController.OnSuccess(response);
            }
        }

        /// <summary>
        /// Handles error scenarios during server communication, processes the error response,
        /// and delegates error handling to the associated FeedbackSceneController if available.
        /// </summary>
        /// <param name="message">The error response received from the server.</param>
        public void OnError(Response message)
        {
            SaveDialogToHistory(message.GetCompletion());

            if (!feedbackSceneController.IsNullOrDestroyed())
            {
                feedbackSceneController.OnError(message);
            }
        }

        /// <summary>
        /// Saves a dialog entry to the history by creating a new history object,
        /// populating it with relevant dialog information, and adding it to the dialog history manager.
        /// </summary>
        /// <param name="response">The response completion text to be trimmed and stored in the dialog history entry.</param>
        private void SaveDialogToHistory(string response)
        {
            DialogHistoryEntry dialogHistoryEntry = new DialogHistoryEntry();
            dialogHistoryEntry.SetNovelId(id);
            dialogHistoryEntry.SetDialog(dialog);
            dialogHistoryEntry.SetCompletion(response.Trim());
            DateTime now = DateTime.Now;
            string formattedDateTime = now.ToString("ddd | dd.MM.yyyy | HH:mm", _culture);
            dialogHistoryEntry.SetDateAndTime(formattedDateTime);
            DialogHistoryManager.Instance().AddEntry(dialogHistoryEntry);
        }
    }
}
