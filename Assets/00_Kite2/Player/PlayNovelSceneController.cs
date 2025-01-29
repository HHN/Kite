using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using _00_Kite2.Audio_Resources.Resources;
using _00_Kite2.Common;
using _00_Kite2.Common.Managers;
using _00_Kite2.Common.Novel;
using _00_Kite2.Common.Novel.Character.CharacterController;
using _00_Kite2.Common.SceneManagement;
using _00_Kite2.Common.UI.UI_Elements.Free_Text_User_Input;
using _00_Kite2.Common.UI.UI_Elements.Messages;
using _00_Kite2.Common.Utilities;
using _00_Kite2.SaveNovelData;
using _00_Kite2.Server_Communication.Server_Calls;
//using LeastSquares.Overtone;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Components.Typewriter._Core;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _00_Kite2.Player
{
    public class PlayNovelSceneController : SceneController
    {
        private const float WaitingTime = 0.5f;

        [Header("UI-Komponenten")] [SerializeField]
        private GameObject viewPort;

        [SerializeField] private GameObject conversationViewport;
        [SerializeField] private Button closeButton;
        [SerializeField] private TextMeshProUGUI novelName;
        [SerializeField] private ConversationContentGuiController conversationContent;
        [SerializeField] public Button confirmArea;
        [SerializeField] public Button confirmArea2;
        [SerializeField] private ChatScrollView chatScroll;
        [SerializeField] private ImageScrollView imageScroll;
        [SerializeField] private GameObject backgroundBlur;
        [SerializeField] private GameObject imageAreaBlur;
        [SerializeField] private GameObject screenContentBlur;
        [SerializeField] private GameObject backgroundColor;
        [SerializeField] private GameObject imageAreaColor;
        [SerializeField] private GameObject screenContentColor;
        [SerializeField] private GameObject freeTextInputPrefab;
        [SerializeField] private GameObject headerImage;

        [Header("Novel-Visuals und Prefabs")] [SerializeField]
        private GameObject[] novelVisuals;

        [SerializeField] private GameObject novelImageContainer;
        [SerializeField] private GameObject novelBackgroundPrefab;
        [SerializeField] private GameObject backgroundContainer;
        [SerializeField] private GameObject deskContainer;
        [SerializeField] private GameObject decoDeskContainer;
        [SerializeField] private GameObject decoBackgroundContainer;
        [SerializeField] private GameObject[] backgroundPrefab;
        [SerializeField] private GameObject[] deskPrefab;
        [SerializeField] private GameObject[] decoDeskPrefab;
        [SerializeField] private GameObject[] decoBackgroundPrefab;
        [SerializeField] private GameObject currentBackground;
        [SerializeField] private GameObject currentDesk;
        [SerializeField] private GameObject currentDecoDesk;
        [SerializeField] private GameObject currentDecoBackground;
        [SerializeField] private GameObject characterContainer;
        [SerializeField] private GameObject[] novelAnimations;
        [SerializeField] private GameObject viewPortOfImages;
        [SerializeField] private GameObject currentAnimation;

        [Header("GPT und MessageBox")] [SerializeField]
        private GameObject gptServercallPrefab;

        [SerializeField] private LeaveNovelAndGoBackMessageBox leaveGameAndGoBackMessageBoxObject;
        [SerializeField] private GameObject leaveGameAndGoBackMessageBox;
        private GameObject hintForSavegameMessageBoxObject;
        [SerializeField] private GameObject hintForSavegameMessageBox;

        [Header("Skript- und Controller-Referenzen")] [SerializeField]
        private VisualNovel novelToPlay;

        [SerializeField] public TypewriterCore currentTypeWriter;
        [SerializeField] public SelectOptionContinueConversation selectOptionContinueConversation;

        [FormerlySerializedAs("currentTalkingCharacterController")] [SerializeField]
        private Kite2CharacterController currentTalkingKite2CharacterController;

        [Header("Audio-Komponenten")] [SerializeField]
        private AudioClip[] clips;

        [Header("Timing und Analytics")] [SerializeField]
        private float timerForHint = 12.0f; // Time after which the hint to tap on the screen is shown

        [SerializeField] private float timerForHintInitial = 3.0f;
        [SerializeField] private bool firstUserConfirmation = true; // Analytics flag for first confirmation

        [Header("Spielstatus und Logik")] [SerializeField]
        private bool isWaitingForConfirmation;

        [SerializeField] private VisualNovelEvent nextEventToPlay;
        [SerializeField] private bool isTyping;
        [SerializeField] private List<string> playThroughHistory = new();
        [SerializeField] private List<VisualNovelEvent> eventHistory = new();
        private readonly Dictionary<string, VisualNovelEvent> _novelEvents = new();
        private readonly string[] _optionsId = new string[2];
        private ConversationContentGuiController _conversationContentGuiController;
        private int _novelCharacter = -1;
        private NovelImageController _novelImagesController;
        private VisualNovelEvent _savedEventToResume; // Speichert das letzte Ereignis für das Fortsetzen
        private Coroutine _timerCoroutine;
        private bool _typingWasSkipped;
        private int _optionsCount;
        private IEnumerator _speakingCoroutine;

        // Character Expressions
        public Dictionary<int, int> CharacterExpressions { get; } = new();

        public bool IsPaused { get; set; }
        public VisualNovel NovelToPlay => novelToPlay;
        public List<string> PlayThroughHistory => playThroughHistory;
        public string[] OptionsId => _optionsId;
        public List<VisualNovelEvent> EventHistory => eventHistory;
        public NovelImageController NovelImageController => _novelImagesController;

        private void Start()
        {
            _conversationContentGuiController = FindAnyObjectByType<ConversationContentGuiController>();

            AnalyticsServiceHandler.Instance().StartStopwatch();
            BackStackManager.Instance().Push(SceneNames.PLAY_NOVEL_SCENE);
            novelToPlay = PlayManager.Instance().GetVisualNovelToPlay();
            NovelBiasManager.Clear();
            OfflineFeedbackManager.Instance().Clear();
            Initialize();
        }

        private void Initialize()
        {
            PromptManager.Instance().InitializePrompt();

            if (novelToPlay == null)
            {
                return;
            }

            AnalyticsServiceHandler.Instance().SetIdOfCurrentNovel(novelToPlay.id);
            novelToPlay.ClearGlobalVariables();
            novelToPlay.feedback = string.Empty;
            novelToPlay.playedPath = string.Empty;

            novelName.text = novelToPlay.title;

            if (novelToPlay.novelEvents.Count <= 0)
            {
                return;
            }

            SetVisualElements();

            // Check if the current novel is the introductory dialogue
            // Hide the header image, as it is not needed in the introductory dialogue
            if (novelToPlay.title != "Einstiegsdialog")
            {
                headerImage.SetActive(true);
            }
            else if (novelToPlay.title == "Einstiegsdialog" &&
                     GameManager.Instance.IsIntroNovelLoadedFromMainMenu == false)
            {
                headerImage.SetActive(true);
            }
            else
            {
                headerImage.SetActive(false);
            }

            List<int> characters = novelToPlay.novelEvents
                .Select(e => e.character) // Wähle das `character`-Feld aus
                .Where(c => c != 0 && c != 1 && c != 4) // Schließe die Werte 0, 1 und 4 aus
                .Distinct() // Optional: Entfernt Duplikate
                .ToList(); // Konvertiere das Ergebnis in eine Liste

            foreach (var characterId in characters)
            {
                CharacterExpressions[characterId] = -1;
            }

            // Show the header image for other novels
            foreach (VisualNovelEvent novelEvent in novelToPlay.novelEvents)
            {
                _novelEvents.Add(novelEvent.id, novelEvent);
            }

            // Überprüfung, ob es einen Speicherstand gibt, direkt über den GameManager
            string novelId = novelToPlay.id.ToString();
            if (GameManager.Instance.HasSavedProgress(novelId))
            {
                ShowHintForSavegameMessageBox();
            }
            else
            {
                nextEventToPlay = novelToPlay.novelEvents[0];

                StartCoroutine(PlayNextEvent());
            }
        }

        public void OnConfirm()
        {
            SkipSpeaking();
            Vector2 mousePosition = Input.mousePosition;

            if (_novelImagesController.HandleTouchEvent(mousePosition.x, mousePosition.y))
            {
                return;
            }

            if (isTyping)
            {
                // Überspringt den Typ-Effekt und zeigt den vollständigen Text an
                if (currentTypeWriter != null)
                {
                    currentTypeWriter.SkipTypewriter();
                    currentTypeWriter = null;
                }

                _typingWasSkipped = true; // Flag setzen
                SetTyping(false);
                SkipSpeaking();

                return; // Beendet die Methode, um nicht zum nächsten Event zu springen
            }

            if (!isWaitingForConfirmation)
            {
                return;
            }

            SetWaitingForConfirmation(false);
            StartCoroutine(PlayNextEvent());
        }

        private void SetVisualElements()
        {
            RectTransform canvasRect = canvas.GetComponent<RectTransform>();

            RectTransform conversationViewportTransform = conversationViewport.GetComponent<RectTransform>();

            conversationViewportTransform.sizeDelta = new Vector2(0, -canvasRect.rect.height * 0.5f);
            RectTransform viewPortTransform = viewPort.GetComponent<RectTransform>();

            switch (novelToPlay.title)
            {
                case "Banktermin wegen Kreditbeantragung":
                {
                    GameObject novelImagesInstance = Instantiate(novelVisuals[0], viewPortTransform);
                    Transform controllerTransform = novelImagesInstance.transform.Find("Controller");
                    _novelImagesController = controllerTransform.GetComponent<BankNovelImageController>();
                    break;
                }
                case "Anmietung eines Büros":
                {
                    GameObject novelImagesInstance = Instantiate(novelVisuals[1], viewPortTransform);
                    Transform controllerTransform = novelImagesInstance.transform.Find("Controller");
                    _novelImagesController = controllerTransform.GetComponent<BueroNovelImageController>();
                    break;
                }
                case "Pressegespräch":
                {
                    GameObject novelImagesInstance = Instantiate(novelVisuals[2], viewPortTransform);
                    Transform controllerTransform = novelImagesInstance.transform.Find("Controller");
                    _novelImagesController = controllerTransform.GetComponent<PresseNovelImageController>();
                    break;
                }
                case "Telefonat mit den Eltern":
                {
                    GameObject novelImagesInstance = Instantiate(novelVisuals[3], viewPortTransform);
                    Transform controllerTransform = novelImagesInstance.transform.Find("Controller");
                    _novelImagesController = controllerTransform.GetComponent<ElternNovelImageController>();
                    break;
                }
                case "Telefonat mit der Notarin":
                {
                    GameObject novelImagesInstance = Instantiate(novelVisuals[4], viewPortTransform);
                    Transform controllerTransform = novelImagesInstance.transform.Find("Controller");
                    _novelImagesController = controllerTransform.GetComponent<NotarinNovelImageController>();
                    break;
                }
                case "Gespräch mit einem Bekannten":
                {
                    GameObject novelImagesInstance = Instantiate(novelVisuals[5], viewPortTransform);
                    Transform controllerTransform = novelImagesInstance.transform.Find("Controller");
                    _novelImagesController = controllerTransform.GetComponent<BekannterNovelImageController>();
                    break;
                }
                case "Einstiegsdialog":
                {
                    GameObject novelImagesInstance = Instantiate(novelVisuals[6], viewPortTransform);
                    Transform controllerTransform = novelImagesInstance.transform.Find("Controller");
                    _novelImagesController = controllerTransform.GetComponent<IntroNovelImageController>();
                    break;
                }
                case "Honorarverhandlung mit Kundin":
                {
                    GameObject novelImagesInstance = Instantiate(novelVisuals[7], viewPortTransform);
                    Transform controllerTransform = novelImagesInstance.transform.Find("Controller");
                    _novelImagesController = controllerTransform.GetComponent<VerhandlungNovelImageController>();
                    break;
                }
                default:
                {
                    GameObject novelImagesInstance = Instantiate(novelVisuals[0], viewPortTransform);
                    Transform controllerTransform = novelImagesInstance.transform.Find("Controller");
                    _novelImagesController = controllerTransform.GetComponent<BankNovelImageController>();
                    break;
                }
            }

            _novelImagesController.SetCanvasRect(canvasRect);
        }

        public IEnumerator ReadLast()
        {
            StartCoroutine(TextToSpeechManager.Instance.Speak(TextToSpeechManager.Instance.GetLastMessage()));
            yield return StartCoroutine(PlayNextEvent());
        }

        private IEnumerator PlayNextEvent()
        {
            if (TextToSpeechManager.Instance.IsTextToSpeechActivated())
            {
                // Warten, bis die Sprachausgabe abgeschlossen oder übersprungen wurde
                if (_speakingCoroutine != null)
                {
                    while (TextToSpeechManager.Instance.IsSpeaking())
                    {
                        yield return null;
                    }
                }
            }

            // Stop if paused
            if (IsPaused)
            {
                yield break;
            }

            if (selectOptionContinueConversation != null)
            {
                selectOptionContinueConversation.alreadyPlayedNextEvent = true;
                selectOptionContinueConversation = null;
            }

            if (currentTypeWriter != null)
            {
                currentTypeWriter.SkipTypewriter(); // no check for isShowing necessary
                currentTypeWriter = null;
            }

            // Überprüfen, ob der Event den Bedingungen entspricht
            if (nextEventToPlay.id.StartsWith("OptionsLabel") && !GameManager.Instance.calledFromReload)
            {
                // Schneide "OptionsLabel" ab und speichere den Rest
                string numericPart = nextEventToPlay.id.Substring("OptionsLabel".Length);

                // Prüfe, ob der Rest eine Zahl ist
                if (int.TryParse(numericPart, out _))
                {
                    // Wenn der Rest eine Zahl ist, speichere das Event
                    _optionsId[0] = _optionsId[1]; // Verschiebe das letzte Event
                    _optionsId[1] = nextEventToPlay.id; // Speichere das aktuelle Event
                }
            }

            // Save the current event in the eventHistory list
            eventHistory.Add(nextEventToPlay);

            VisualNovelEventType type = VisualNovelEventTypeHelper.ValueOf(nextEventToPlay.eventType);

            switch (type)
            {
                case VisualNovelEventType.SET_BACKGROUND_EVENT:
                {
                    HandleBackgroundEvent(nextEventToPlay);
                    break;
                }
                case VisualNovelEventType.CHARAKTER_JOIN_EVENT:
                {
                    HandleCharacterJoinEvent(nextEventToPlay);
                    break;
                }
                case VisualNovelEventType.CHARAKTER_EXIT_EVENT:
                {
                    HandleCharacterExitEvent(nextEventToPlay);
                    break;
                }
                case VisualNovelEventType.SHOW_MESSAGE_EVENT:
                {
                    HandleShowMessageEvent(nextEventToPlay);
                    ScrollToBottom();
                    break;
                }
                case VisualNovelEventType.ADD_CHOICE_EVENT:
                {
                    HandleAddChoiceEvent(nextEventToPlay);
                    break;
                }
                case VisualNovelEventType.SHOW_CHOICES_EVENT:
                {
                    confirmArea.gameObject.SetActive(false);
                    confirmArea2.gameObject.SetActive(false);
                    HandleShowChoicesEvent(nextEventToPlay);
                    ScrollToBottom();
                    break;
                }
                case VisualNovelEventType.END_NOVEL_EVENT:
                {
                    HandleEndNovelEvent();
                    break;
                }
                case VisualNovelEventType.PLAY_SOUND_EVENT:
                {
                    HandlePlaySoundEvent(nextEventToPlay);
                    break;
                }
                case VisualNovelEventType.PLAY_ANIMATION_EVENT:
                {
                    HandlePlayAnimationEvent(nextEventToPlay);
                    break;
                }
                case VisualNovelEventType.FREE_TEXT_INPUT_EVENT:
                {
                    HandleFreeTextInputEvent(nextEventToPlay);
                    break;
                }
                case VisualNovelEventType.GPT_PROMPT_EVENT:
                {
                    HandleGptPromptEvent(nextEventToPlay);
                    break;
                }
                case VisualNovelEventType.SAVE_PERSISTENT_EVENT:
                {
                    HandleSavePersistentEvent(nextEventToPlay);
                    break;
                }
                case VisualNovelEventType.SAVE_VARIABLE_EVENT:
                {
                    HandleSaveVariableEvent(nextEventToPlay);
                    break;
                }
                case VisualNovelEventType.ADD_FEEDBACK_EVENT:
                {
                    HandleAddFeedbackEvent(nextEventToPlay);
                    break;
                }
                case VisualNovelEventType.ADD_FEEDBACK_UNDER_CONDITION_EVENT:
                {
                    HandleAddFeedbackUnderConditionEvent(nextEventToPlay);
                    break;
                }
                case VisualNovelEventType.MARK_BIAS_EVENT:
                {
                    HandleMarkBiasEvent(nextEventToPlay);
                    break;
                }
                case VisualNovelEventType.CALCULATE_VARIABLE_FROM_BOOLEAN_EXPRESSION_EVENT:
                {
                    HandleCalculateVariableFromBooleanExpressionEvent(nextEventToPlay);
                    break;
                }
                default:
                {
                    string nextEventID = nextEventToPlay.nextId;
                    nextEventToPlay = _novelEvents[nextEventID];
                    yield return StartCoroutine(PlayNextEvent()); // Rekursiver Coroutine-Aufruf
                    break;
                }
            }
        }

        private void HandlePlaySoundEvent(VisualNovelEvent novelEvent)
        {
            SetNextEvent(novelEvent);

            if (novelEvent.audioClipToPlay != 0)
            {
                GlobalVolumeManager.Instance.PlaySound(clips[novelEvent.audioClipToPlay]);
            }

            if (novelEvent.waitForUserConfirmation)
            {
                SetWaitingForConfirmation(true);
                return;
            }

            if (novelEvent.audioClipToPlay == KiteSoundHelper.ToInt(KiteSound.LEAVE_SCENE))
            {
                StartCoroutine(StartNextEventInOneSeconds(2.5f));
                return;
            }

            StartCoroutine(StartNextEventInOneSeconds(1));
        }

        private void HandlePlayAnimationEvent(VisualNovelEvent novelEvent)
        {
            SetNextEvent(novelEvent);

            if (novelEvent.animationToPlay != 0)
            {
                currentAnimation = Instantiate(novelAnimations[novelEvent.animationToPlay], viewPortOfImages.transform);
            }
        }

        private void HandleFreeTextInputEvent(VisualNovelEvent novelEvent)
        {
            SetNextEvent(novelEvent);

            if (novelEvent.questionForFreeTextInput == string.Empty
                || novelEvent.questionForFreeTextInput == ""
                || novelEvent.variablesName == string.Empty
                || novelEvent.variablesName == "")
            {
                return;
            }

            FreeTextInputController freeTextInputController = Instantiate(this.freeTextInputPrefab, canvas.transform)
                .GetComponent<FreeTextInputController>();
            freeTextInputController.Initialize(novelEvent.questionForFreeTextInput, novelEvent.variablesName);
        }

        private void HandleGptPromptEvent(VisualNovelEvent novelEvent)
        {
            SetNextEvent(novelEvent);

            if (novelEvent.gptPrompt == String.Empty
                || novelEvent.gptPrompt == ""
                || novelEvent.variablesNameForGptPrompt == String.Empty
                || novelEvent.variablesNameForGptPrompt == "")
            {
                return;
            }

            if (ApplicationModeManager.Instance().IsOfflineModeActive())
            {
                StartCoroutine(PlayNextEvent());
                return;
            }

            GetCompletionServerCall call = Instantiate(gptServercallPrefab).GetComponent<GetCompletionServerCall>();
            call.sceneController = this;
            GptRequestEventOnSuccessHandler onSuccessHandler = new GptRequestEventOnSuccessHandler
            {
                VariablesNameForGptPrompt = novelEvent.variablesNameForGptPrompt,
                CompletionHandler = GptCompletionHandlerManager.Instance()
                    .GetCompletionHandlerById(novelEvent.gptCompletionHandlerId)
            };
            call.OnSuccessHandler = onSuccessHandler;
            call.prompt = ReplacePlaceholders(novelEvent.gptPrompt, novelToPlay.GetGlobalVariables());
            call.SendRequest();
            DontDestroyOnLoad(call.gameObject);

            if (novelEvent.waitForUserConfirmation)
            {
                SetWaitingForConfirmation(true);
                return;
            }

            StartCoroutine(PlayNextEvent());
        }

        private void HandleSavePersistentEvent(VisualNovelEvent novelEvent)
        {
            SetNextEvent(novelEvent);
            WriteUserInputToFile(novelEvent.key,
                ReplacePlaceholders(novelEvent.value, novelToPlay.GetGlobalVariables()));
            StartCoroutine(PlayNextEvent());
        }

        private void HandleSaveVariableEvent(VisualNovelEvent novelEvent)
        {
            SetNextEvent(novelEvent);
            novelToPlay.AddGlobalVariable(novelEvent.key, novelEvent.value);
            StartCoroutine(PlayNextEvent());
        }

        private void HandleCalculateVariableFromBooleanExpressionEvent(VisualNovelEvent novelEvent)
        {
            SetNextEvent(novelEvent);
            string booleanExpression = ReplacePlaceholders(novelEvent.value, novelToPlay.GetGlobalVariables());
            novelToPlay.AddGlobalVariable(novelEvent.key, EvaluateBooleanExpression(booleanExpression).ToString());
            StartCoroutine(PlayNextEvent());
        }

        private static bool EvaluateBooleanExpression(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                return false;
            }

            expression = expression.Replace("true", "True").Replace("TRUE", "True").Replace("false", "False")
                .Replace("FALSE", "False");
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("expression", typeof(bool), expression);
                DataRow row = table.NewRow();
                table.Rows.Add(row);
                bool result = (bool)row["expression"];
                return result;
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"Error evaluating boolean expression: {ex.Message}");
                return false;
            }
        }

        private void HandleAddFeedbackEvent(VisualNovelEvent novelEvent)
        {
            SetNextEvent(novelEvent);
            OfflineFeedbackManager.Instance().AddLineToPrompt(novelEvent.value);
            StartCoroutine(PlayNextEvent());
        }

        private void HandleAddFeedbackUnderConditionEvent(VisualNovelEvent novelEvent)
        {
            SetNextEvent(novelEvent);

            if (novelToPlay.IsVariableExistent(novelEvent.key) &&
                (novelToPlay.GetGlobalVariable(novelEvent.key) == "True"
                 || novelToPlay.GetGlobalVariable(novelEvent.key) == "true" ||
                 novelToPlay.GetGlobalVariable(novelEvent.key) == "TRUE"))
            {
                OfflineFeedbackManager.Instance().AddLineToPrompt(novelEvent.value);
            }

            StartCoroutine(PlayNextEvent());
        }

        private void HandleMarkBiasEvent(VisualNovelEvent novelEvent)
        {
            SetNextEvent(novelEvent);
            string biasInformation =
                DiscriminationBiasHelper.GetInformationString(
                    DiscriminationBiasHelper.ValueOf(novelEvent.relevantBias));
            PromptManager.Instance().AddFormattedLineToPrompt("Hinweis", biasInformation);
            NovelBiasManager.Instance().MarkBiasAsRelevant(DiscriminationBiasHelper.ValueOf(novelEvent.relevantBias));
            StartCoroutine(PlayNextEvent());
        }

        private void WriteUserInputToFile(string key, string content)
        {
            PlayerDataManager.Instance().SavePlayerData(key, content);
        }

        private void HandleBackgroundEvent(VisualNovelEvent novelEvent)
        {
            SetNextEvent(novelEvent);

            _novelImagesController.SetBackground();
            StartCoroutine(imageScroll.ScrollToPoint(0.5f, 1f));
            StartCoroutine(StartNextEventInOneSeconds(1));
        }

        private void HandleCharacterJoinEvent(VisualNovelEvent novelEvent)
        {
            SetNextEvent(novelEvent);
            _novelImagesController.SetCharacter();

            StartCoroutine(StartNextEventInOneSeconds(1));
        }

        private void HandleCharacterExitEvent(VisualNovelEvent novelEvent)
        {
            SetNextEvent(novelEvent);

            _novelImagesController.DestroyCharacter();

            StartCoroutine(StartNextEventInOneSeconds(1));
        }

        private void HandleShowMessageEvent(VisualNovelEvent novelEvent)
        {
            CreateSpeakingCoroutine(novelEvent.text);

            SetNextEvent(novelEvent);

            _novelCharacter = novelEvent.character;

            if (!CharacterExpressions.ContainsKey(_novelCharacter) && _novelCharacter != 0 && _novelCharacter != 1 &&
                _novelCharacter != 4)
            {
                Debug.LogWarning($"Character ID {_novelCharacter} is not registered.");
                return;
            }

            // Speichere die neue Gesichtsanimation
            CharacterExpressions[_novelCharacter] = novelEvent.expressionType;

            _novelImagesController.SetFaceExpression(_novelCharacter, CharacterExpressions[_novelCharacter]);

            if (novelEvent.show)
            {
                conversationContent.AddContent(novelEvent, this);

                AddEntryToPlayThroughHistory(CharacterTypeHelper.ValueOf(novelEvent.character), novelEvent.text);
                AnalyticsServiceHandler.Instance().SetLastQuestionForChoice(novelEvent.text);
            }

            if (novelEvent.waitForUserConfirmation)
            {
                SetWaitingForConfirmation(true);
                return;
            }

            StartCoroutine(StartNextEventInOneSeconds(1));
        }

        private void HandleAddChoiceEvent(VisualNovelEvent novelEvent)
        {
            SetNextEvent(novelEvent);

            conversationContent.AddContent(novelEvent, this);

            if (novelEvent.waitForUserConfirmation)
            {
                SetWaitingForConfirmation(true);
                return;
            }

            AnalyticsServiceHandler.Instance().AddChoiceToList(novelEvent.text);
            TextToSpeechManager.Instance.AddChoiceToChoiceCollectionForTextToSpeech(novelEvent.text);
            StartCoroutine(PlayNextEvent());
        }

        private void HandleShowChoicesEvent(VisualNovelEvent novelEvent)
        {
            StartCoroutine(TextToSpeechManager.Instance.ReadChoice());

            // Enable animations when showing choices
            AnimationFlagSingleton.Instance().SetFlag(true);

            // Log the event to the playthrough history, including the character and their dialogue
            AddEntryToPlayThroughHistory(CharacterTypeHelper.ValueOf(novelEvent.character), novelEvent.text);

            // Add the conversation content to the UI or dialogue system
            conversationContent.AddContent(novelEvent, this);
        }

        public void HandleEndNovelEvent()
        {
            AnalyticsServiceHandler.Instance().SendNovelPlayTime();

            PlayRecordManager.Instance()
                .IncreasePlayCounterForNovel(VisualNovelNamesHelper.ValueOf((int)novelToPlay.id));

            PlayThroughCounterAnimationManager.Instance()
                .SetAnimation(true, VisualNovelNamesHelper.ValueOf((int)novelToPlay.id));

            int userRole = FeedbackRoleManager.Instance.GetFeedbackRole();

            PlayerDataManager.Instance().SetNovelHistory(playThroughHistory);

            // Check if the current novel is the introductory dialogue
            if (novelToPlay.title == "Einstiegsdialog")
            {
                // Load the FoundersBubbleScene to navigate there after the introductory dialogue
                SceneLoader.LoadFoundersBubbleScene();
                return; // Exit the method to prevent further scenes from being loaded
            }

            // Check if the user has a specific role and if the app is in online mode
            if ((userRole == 1 || userRole == 3 || userRole == 4 || userRole == 5) &&
                ApplicationModeManager.Instance().IsOnlineModeActive())
            {
                // Load the ReviewNovelScene where the user can rate or review what they've read
                SceneLoader.LoadReviewNovelScene();
            }
            else
            {
                // If the conditions are not met, load the FeedbackScene
                SceneLoader.LoadFeedbackScene();
            }
        }

        private IEnumerator StartNextEventInOneSeconds(float second)
        {
            yield return new WaitForSeconds(second);

            if (_novelCharacter != -1 && CharacterExpressions.ContainsKey(_novelCharacter))
            {
                if (CharacterExpressions[_novelCharacter] > 13)
                {
                    CharacterExpressions[_novelCharacter] -= 13;
                    _novelImagesController.SetFaceExpression(_novelCharacter, CharacterExpressions[_novelCharacter]);
                }
            }

            StartCoroutine(PlayNextEvent());
        }

        public void ShowAnswer(string message, bool show)
        {
            if (!show)
            {
                return;
            }

            AddEntryToPlayThroughHistory(CharacterRole.PLAYER, message);
            conversationContent.ShowPlayerAnswer(message);
            ScrollToBottom();
        }

        public void SetNextEvent(string id)
        {
            nextEventToPlay = _novelEvents[id];
        }

        private void SetNextEvent(VisualNovelEvent novelEvent)
        {
            string nextEventID = novelEvent.nextId;
            nextEventToPlay = _novelEvents[nextEventID];
        }

        public void ScrollToBottom()
        {
            StartCoroutine(chatScroll.ScrollToBottom());
            FontSizeManager.Instance().UpdateAllTextComponents();
        }

        public void StartTalking()
        {
            _novelImagesController.StartCharacterTalking();
        }

        public void StopTalking()
        {
            _novelImagesController.StopCharacterTalking();
        }

        public void SetWaitingForConfirmation(bool value)
        {
            this.isWaitingForConfirmation = value;
        }

        public void SetTyping(bool value)
        {
            isTyping = value;

            if (!isTyping && isWaitingForConfirmation)
            {
                SetWaitingForConfirmation(false);

                float delay = WaitingTime;
                if (_typingWasSkipped)
                {
                    delay = 0f; // Wartezeit überspringen
                    _typingWasSkipped = false; // Flag zurücksetzen
                }

                StartCoroutine(StartNextEventInOneSeconds(delay));
            }
        }

        public void AnimationFinished()
        {
            SetWaitingForConfirmation(true);

            if (currentAnimation.IsNullOrDestroyed())
            {
                return;
            }

            Destroy(currentAnimation);
        }

        private void AddEntryToPlayThroughHistory(CharacterRole characterRole, string text)
        {
            playThroughHistory.Add(CharacterTypeHelper.GetNameOfCharacter(characterRole) + ": " + text);
        }

        public void AddEntryToPlayThroughHistory(string entry)
        {
            playThroughHistory.Add(entry);
        }

        public static string ReplacePlaceholders(string text, Dictionary<string, string> replacements)
        {
            return Regex.Replace(text, @"\>(.*?)\<", match =>
            {
                string key = match.Groups[1].Value;
                return replacements.TryGetValue(key, out string replacement) ? replacement : match.Value;
            });
        }

        public void AddPathToNovel(int pathValue)
        {
            novelToPlay.AddToPath(pathValue);
        }

        public void RestoreChoice()
        {
            // Check if there is a previous choice to restore
            if (string.IsNullOrEmpty(_optionsId[0])) return;

            // The ID of the event we want to restore to
            string eventIdToRestore = _optionsId[0];

            // Find the index of the event in the eventHistory list
            int indexToRestore = eventHistory.FindIndex(e => e.id == eventIdToRestore);

            if (_optionsCount != 0)
            {
                indexToRestore -= _optionsCount;
            }

            if (indexToRestore == -1) return; // If the event is found in the history

            // Remove the event and all events after it
            eventHistory.RemoveRange(indexToRestore, eventHistory.Count - indexToRestore);

            // Now update playThroughHistory
            // Search from back to front for the second occurrence of ":"
            int colonCount = 0;
            int indexToRemoveFrom = -1;

            // Traverse the list backwards
            for (int i = playThroughHistory.Count - 1; i >= 0; i--)
            {
                if (playThroughHistory[i].Trim() != ":") continue;

                colonCount++;

                if (colonCount != 2) continue;

                indexToRemoveFrom = i;
                break;
            }

            if (indexToRemoveFrom != -1)
            {
                // Remove all entries from the found index onwards
                playThroughHistory.RemoveRange(indexToRemoveFrom, playThroughHistory.Count - indexToRemoveFrom);

                _conversationContentGuiController.ClearUIAfter(indexToRemoveFrom, _optionsCount);
            }

            _optionsCount = 0;

            // Set the previous event as the next event to play, if present
            if (indexToRestore <= 0) return;

            SetNextEvent(eventIdToRestore);
            StartCoroutine(PlayNextEvent());
        }

        private void CreateSpeakingCoroutine(string text)
        {
            _speakingCoroutine = TextToSpeechManager.Instance.Speak(text);
            StartCoroutine(_speakingCoroutine);
        }

        private void SkipSpeaking()
        {
            TextToSpeechManager.Instance.CancelSpeak();
        }

        /// <summary>
        /// Methode zum Anzeigen der HintForSavegameMessageBox
        /// </summary> 
        private void ShowHintForSavegameMessageBox()
        {
            if (hintForSavegameMessageBox == null) return;

            // Überprüfen, ob die HintForSavegameMessageBox bereits geladen ist und schließe sie gegebenenfalls
            if (!hintForSavegameMessageBoxObject.IsNullOrDestroyed())
            {
                hintForSavegameMessageBoxObject.GetComponent<HintForSavegameMessageBox>().CloseMessageBox();
            }

            // Instanziiere und aktiviere die HintForSavegameMessageBox, falls das Canvas nicht null ist
            if (canvas.IsNullOrDestroyed()) return;

            hintForSavegameMessageBoxObject = Instantiate(hintForSavegameMessageBox, canvas.transform);
            hintForSavegameMessageBoxObject.GetComponent<HintForSavegameMessageBox>().Activate();
        }

        /// <summary>
        /// Startet das Spiel vom gespeicherten Punkt, wenn "Fortsetzen" gewählt wird
        /// </summary>
        public void ResumeFromSavedState()
        {
            string novelId = NovelToPlay.id.ToString();
            NovelSaveData savedData = SaveLoadManager.Load(novelId);

            if (savedData == null)
            {
                Debug.LogWarning("No saved data found for the novel.");
                return;
            }

            // Suche den gespeicherten Event in der Liste
            nextEventToPlay = novelToPlay.novelEvents.FirstOrDefault(e => e.id == savedData.currentEventId)
                              ?? novelToPlay.novelEvents[0];

            playThroughHistory = new List<string>(savedData.playThroughHistory);

            _optionsId[0] = savedData.optionsId[1];
            _optionsCount = savedData.optionCount;
            eventHistory = savedData.eventHistory;

            // Aufruf von ReconstructGuiContent und Prüfung des Rückgabewertes
            conversationContent.ReconstructGuiContent(savedData);

            long searchId = novelToPlay.id;

            switch (searchId)
            {
                case 1:
                    break;
                case 2:
                    ElternNovelImageController elternNovelImageController =
                        FindObjectsOfType<ElternNovelImageController>().FirstOrDefault();
                    
                    if (elternNovelImageController != null && savedData.CharacterPrefabData != null)
                    {
                        if (savedData.CharacterPrefabData.TryGetValue(searchId, out CharacterData characterData))
                        {
                            // Setze die Attribute basierend auf den gespeicherten Werten
                            elternNovelImageController.novelKite2CharacterController.SetSkinSprite(characterData.skinIndex);
                            elternNovelImageController.novelKite2CharacterController.SetHandSprite(characterData.handIndex);
                            elternNovelImageController.novelKite2CharacterController.SetClotheSprite(characterData.clotheIndex);
                            elternNovelImageController.novelKite2CharacterController.SetHairSprite(characterData.hairIndex);
                
                            elternNovelImageController.novelKite2CharacterController2.SetSkinSprite(characterData.skinIndex2);
                            elternNovelImageController.novelKite2CharacterController2.SetGlassesSprite(characterData.glassIndex2);
                            elternNovelImageController.novelKite2CharacterController2.SetClotheSprite(characterData.clotheIndex2);
                            elternNovelImageController.novelKite2CharacterController2.SetHairSprite(characterData.hairIndex2);
                        }
                    }

                    break;
                case 3:
                    PresseNovelImageController presseNovelImageController =
                        FindObjectsOfType<PresseNovelImageController>().FirstOrDefault();
                    
                    if (presseNovelImageController != null && savedData.CharacterPrefabData != null)
                    {
                        if (savedData.CharacterPrefabData.TryGetValue(searchId, out CharacterData characterData))
                        {
                            // Setze die Attribute basierend auf den gespeicherten Werten
                            presseNovelImageController.kite2CharacterController.SetSkinSprite(characterData.skinIndex);
                            presseNovelImageController.kite2CharacterController.SetHandSprite(characterData.handIndex);
                            presseNovelImageController.kite2CharacterController.SetClotheSprite(characterData.clotheIndex);
                            presseNovelImageController.kite2CharacterController.SetHairSprite(characterData.hairIndex);
                        }
                    }

                    break;
                case 4:
                    NotarinNovelImageController notarinNovelImageController =
                        FindObjectsOfType<NotarinNovelImageController>().FirstOrDefault();

                    if (notarinNovelImageController != null && savedData.CharacterPrefabData != null)
                    {
                        if (savedData.CharacterPrefabData.TryGetValue(searchId, out CharacterData characterData))
                        {
                            // Setze die Attribute basierend auf den gespeicherten Werten
                            notarinNovelImageController.kite2CharacterController.SetSkinSprite(characterData.skinIndex);
                            notarinNovelImageController.kite2CharacterController.SetHandSprite(characterData.handIndex);
                            notarinNovelImageController.kite2CharacterController.SetClotheSprite(characterData.clotheIndex);
                            notarinNovelImageController.kite2CharacterController.SetHairSprite(characterData.hairIndex);
                        }
                    }

                    break;
                case 5:
                    break;
                case 6:
                    BueroNovelImageController bueroNovelImageController =
                        FindObjectsOfType<BueroNovelImageController>().FirstOrDefault();

                    if (bueroNovelImageController != null && savedData.CharacterPrefabData != null)
                    {
                        if (savedData.CharacterPrefabData.TryGetValue(searchId, out CharacterData characterData))
                        {
                            // Setze die Attribute basierend auf den gespeicherten Werten
                            bueroNovelImageController.kite2CharacterController.SetSkinSprite(characterData.skinIndex);
                            bueroNovelImageController.kite2CharacterController.SetHandSprite(characterData.handIndex);
                            bueroNovelImageController.kite2CharacterController.SetClotheSprite(characterData.clotheIndex);
                            bueroNovelImageController.kite2CharacterController.SetHairSprite(characterData.hairIndex);
                        }
                    }

                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    BekannterNovelImageController bekannterNovelImageController =
                        FindObjectsOfType<BekannterNovelImageController>().FirstOrDefault();

                    if (bekannterNovelImageController != null && savedData.CharacterPrefabData != null)
                    {
                        if (savedData.CharacterPrefabData.TryGetValue(searchId, out CharacterData characterData))
                        {
                            // Setze die Attribute basierend auf den gespeicherten Werten
                            bekannterNovelImageController.kite2CharacterController.SetSkinSprite(characterData.skinIndex);
                            bekannterNovelImageController.kite2CharacterController.SetHandSprite(characterData.handIndex);
                            bekannterNovelImageController.kite2CharacterController.SetClotheSprite(characterData.clotheIndex);
                            bekannterNovelImageController.kite2CharacterController.SetHairSprite(characterData.hairIndex);
                        }
                    }

                    break;
                case 10:
                    BankNovelImageController bankNovelImageController =
                        FindObjectsOfType<BankNovelImageController>().FirstOrDefault();

                    if (bankNovelImageController != null && savedData.CharacterPrefabData != null)
                    {
                        if (savedData.CharacterPrefabData.TryGetValue(searchId, out CharacterData characterData))
                        {
                            // Setze die Attribute basierend auf den gespeicherten Werten
                            bankNovelImageController.novelKite2CharacterController.SetSkinSprite(characterData.skinIndex);
                            bankNovelImageController.novelKite2CharacterController.SetHandSprite(characterData.handIndex);
                            bankNovelImageController.novelKite2CharacterController.SetClotheSprite(characterData.clotheIndex);
                            bankNovelImageController.novelKite2CharacterController.SetHairSprite(characterData.hairIndex);
                        }
                    }

                    break;
                case 11:
                    VerhandlungNovelImageController verhandlungNovelImageController =
                        FindObjectsOfType<VerhandlungNovelImageController>().FirstOrDefault();

                    if (verhandlungNovelImageController != null && savedData.CharacterPrefabData != null)
                    {
                        if (savedData.CharacterPrefabData.TryGetValue(searchId, out CharacterData characterData))
                        {
                            // Setze die Attribute basierend auf den gespeicherten Werten
                            verhandlungNovelImageController.kite2CharacterController.SetSkinSprite(characterData.skinIndex);
                            verhandlungNovelImageController.kite2CharacterController.SetHandSprite(characterData.handIndex);
                            verhandlungNovelImageController.kite2CharacterController.SetClotheSprite(characterData.clotheIndex);
                            verhandlungNovelImageController.kite2CharacterController.SetHairSprite(characterData.hairIndex);
                        }
                    }

                    break;
                case 12:
                    break;
                case 13:
                    IntroNovelImageController introNovelImageController =
                        FindObjectsOfType<IntroNovelImageController>().FirstOrDefault();

                    if (introNovelImageController != null && savedData.CharacterPrefabData != null)
                    {
                        if (savedData.CharacterPrefabData.TryGetValue(searchId, out CharacterData characterData))
                        {
                            // Setze die Attribute basierend auf den gespeicherten Werten
                            introNovelImageController.kite2CharacterController.SetSkinSprite(characterData.skinIndex);
                            introNovelImageController.kite2CharacterController.SetHandSprite(characterData.handIndex);
                            introNovelImageController.kite2CharacterController.SetClotheSprite(characterData.clotheIndex);
                            introNovelImageController.kite2CharacterController.SetHairSprite(characterData.hairIndex);
                        }
                    }

                    break;
            }

            RestoreCharacterExpressions(savedData);

            ActivateMessageBoxes();

            StartCoroutine(PlayNextEvent());
        }

        private void RestoreCharacterExpressions(NovelSaveData savedData)
        {
            foreach (var kvp in savedData.CharacterExpressions)
            {
                _novelImagesController.SetFaceExpression(kvp.Key, kvp.Value);
            }
        }

        /// <summary>
        /// Methode zum Neustarten (bei Auswahl "Neustarten" im Dialog)
        /// </summary>
        public void RestartNovel()
        {
            // Lösche den zugehörigen Speicherstand
            SaveLoadManager.DeleteNovelSaveData(novelToPlay.id.ToString());

            nextEventToPlay = novelToPlay.novelEvents[0];

            StartCoroutine(StartNextEventInOneSeconds(2));
        }

        public VisualNovelEvent GetCurrentEvent()
        {
            return nextEventToPlay;
        }

        private void ActivateMessageBoxes()
        {
            foreach (var messageBox in conversationContent.GuiContent)
            {
                if (messageBox != null && !messageBox.activeSelf)
                {
                    messageBox.SetActive(true);
                }
            }
        }
    }
}