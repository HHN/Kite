using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using Assets._Scripts.Managers;
using Assets._Scripts.Novel;
using Assets._Scripts.Novel.CharacterController;
using Assets._Scripts.Player;
using Assets._Scripts.SaveNovelData;
using Assets._Scripts.SceneManagement;
using Assets._Scripts.Server_Communication.Server_Calls;
using Assets._Scripts.UI_Elements.Messages;
using Assets._Scripts.Utilities;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Components.Typewriter._Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.SceneControllers
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
        [SerializeField] private GameObject headerImage;

        [Header("Novel-Visuals und Prefabs")] 
        [SerializeField] private GameObject[] novelVisuals;

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
        private GameObject _hintForSavegameMessageBoxObject;
        [SerializeField] private GameObject hintForSavegameMessageBox;

        [Header("Skript- und Controller-Referenzen")] [SerializeField]
        private VisualNovel novelToPlay;

        [SerializeField] public TypewriterCore currentTypeWriter;
        [SerializeField] public SelectOptionContinueConversation selectOptionContinueConversation;

        [SerializeField] private Kite2CharacterController currentTalkingKite2CharacterController;

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

        private static PlayNovelSceneController _instance;

        public static PlayNovelSceneController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<PlayNovelSceneController>();
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject("PlayNovelSceneController");
                        _instance = obj.AddComponent<PlayNovelSceneController>();
                        DontDestroyOnLoad(obj);
                    }
                }

                return _instance;
            }
        }

        private void Start()
        {
            _conversationContentGuiController = FindAnyObjectByType<ConversationContentGuiController>();

            AnalyticsServiceHandler.Instance().StartStopwatch();
            BackStackManager.Instance().Push(SceneNames.PlayNovelScene);
            novelToPlay = PlayManager.Instance().GetVisualNovelToPlay();
            
            NovelBiasManager.Clear();
            OfflineFeedbackManager.Instance().Clear();
            
            Initialize();
        }

        private void Initialize()
        {
            if (novelToPlay == null) return; 
            
            PromptManager.Instance().InitializePrompt(novelToPlay.id);

            AnalyticsServiceHandler.Instance().SetIdOfCurrentNovel(novelToPlay.id);
            novelToPlay.ClearGlobalVariables();
            novelToPlay.feedback = string.Empty;
            novelToPlay.playedPath = string.Empty;

            novelName.text = novelToPlay.title;

            if (novelToPlay.novelEvents.Count <= 0) return;

            SetVisualElements();
            HandleHeaderImage();
            InitializeCharacterExpressions();
            InitializeNovelEvents();
            CheckForSavegame();
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
                case "Gespräch mit einem Investor":
                {
                    GameObject novelImagesInstance = Instantiate(novelVisuals[5], viewPortTransform);
                    Transform controllerTransform = novelImagesInstance.transform.Find("Controller");
                    _novelImagesController = controllerTransform.GetComponent<InvestorNovelImageController>();
                    break;
                }
                case "Vertrieb":
                {
                    GameObject novelImagesInstance = Instantiate(novelVisuals[5], viewPortTransform);
                    Transform controllerTransform = novelImagesInstance.transform.Find("Controller");
                    _novelImagesController = controllerTransform.GetComponent<InvestorNovelImageController>();
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
                    _novelImagesController = controllerTransform.GetComponent<HonorarNovelImageController>();
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
        
        private void HandleHeaderImage()
        {
            // Hide the header image, as it is not needed in the introductory dialogue
            bool isIntro = novelToPlay.title == "Einstiegsdialog";
            bool isIntroFromMainMenu = GameManager.Instance.IsIntroNovelLoadedFromMainMenu;
            
            headerImage.SetActive(!isIntro || !isIntroFromMainMenu);
        }
        
        private void InitializeCharacterExpressions()
        {
            CharacterExpressions.Clear();
            
            List<int> characters = novelToPlay.novelEvents
                .Select(e => e.character) // Wähle das `character`-Feld aus
                .Where(c => c != 0 && c != 1 && c != 4) // Schließe die Werte 0, 1 und 4 aus
                .Distinct() // Optional: Entfernt Duplikate
                .ToList(); // Konvertiere das Ergebnis in eine Liste

            foreach (var characterId in characters)
            {
                CharacterExpressions[characterId] = -1;
            }
        }

        private void InitializeNovelEvents()
        {
            foreach (VisualNovelEvent novelEvent in novelToPlay.novelEvents)
            {
                _novelEvents.Add(novelEvent.id, novelEvent);
            }
        }

        private void CheckForSavegame()
        {
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
            TextToSpeechManager.Instance.CancelSpeak();

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

                //TextToSpeechManager.Instance.CancelSpeak();

                return; // Beendet die Methode, um nicht zum nächsten Event zu springen
            }

            if (!isWaitingForConfirmation)
            {
                return;
            }

            SetWaitingForConfirmation(false);
            StartCoroutine(PlayNextEvent());
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
                yield return WaitForSpeechToFinish();
            }

            // Stop if paused
            if (IsPaused)
            {
                yield break;
            }

            HandleEventPreparation();

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

        private IEnumerator WaitForSpeechToFinish()
        {
            if (_speakingCoroutine != null)
            {
                while (TextToSpeechManager.Instance.IsSpeaking())
                {
                    yield return null;
                }
            }
        }
        
        private void HandleEventPreparation()
        {
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

            if (novelEvent.audioClipToPlay == KiteSoundHelper.ToInt(KiteSound.LeaveScene))
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
            if (string.IsNullOrWhiteSpace(expression)) return false;

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
            string biasInformation = DiscriminationBiasHelper.GetInformationString(DiscriminationBiasHelper.ValueOf(novelEvent.relevantBias));
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
            if (CharacterExpressions.ContainsKey(_novelCharacter) && _novelCharacter != 0 && _novelCharacter != 1 &&
                _novelCharacter != 4)
            {
                CharacterExpressions[_novelCharacter] = novelEvent.expressionType;
                _novelImagesController.SetFaceExpression(_novelCharacter, CharacterExpressions[_novelCharacter]);
            }

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

            PlayThroughCounterAnimationManager.Instance().SetAnimation(true, VisualNovelNamesHelper.ValueOf((int)novelToPlay.id));

            PlayerDataManager.Instance().SetNovelHistory(playThroughHistory);

            // Check if the current novel is the introductory dialogue
            if (novelToPlay.title == "Einstiegsdialog")
            {
                // Load the FoundersBubbleScene to navigate there after the introductory dialogue
                SceneLoader.LoadFoundersBubbleScene();
                return; // Exit the method to prevent further scenes from being loaded
            }

            SceneLoader.LoadFeedbackScene();
            
        }

        private IEnumerator StartNextEventInOneSeconds(float second)
        {
            if (_novelCharacter != -1 && CharacterExpressions.ContainsKey(_novelCharacter))
            {
                if (CharacterExpressions[_novelCharacter] > 13)
                {
                    CharacterExpressions[_novelCharacter] -= 13;
                    _novelImagesController.SetFaceExpression(_novelCharacter, CharacterExpressions[_novelCharacter]);
                }
            }
            
            yield return new WaitForSeconds(second);

            StartCoroutine(PlayNextEvent());
        }

        public void ShowAnswer(string message, bool show)
        {
            if (!show) return;

            AddEntryToPlayThroughHistory(CharacterRole.Player, message);
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

            if (currentAnimation.IsNullOrDestroyed()) return;

            Destroy(currentAnimation);
        }

        private void AddEntryToPlayThroughHistory(CharacterRole characterRole, string text)
        {
            playThroughHistory.Add(CharacterTypeHelper.GetNameOfCharacter(characterRole) + ": " + text);
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
            if (indexToRestore == -1) return; // If the event is found in the history

            if (_optionsCount != 0)
            {
                indexToRestore -= _optionsCount;
            }
            
            // Entfernen des Events und aller nachfolgenden Events aus der Historie
            if (indexToRestore >= 0)
            {
                eventHistory.RemoveRange(indexToRestore, eventHistory.Count - indexToRestore);
            }

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

        /// <summary>
        /// Methode zum Anzeigen der HintForSavegameMessageBox
        /// </summary> 
        private void ShowHintForSavegameMessageBox()
        {
            if (hintForSavegameMessageBox == null || DestroyValidator.IsNullOrDestroyed(canvas)) return;

            // Überprüfen, ob die HintForSavegameMessageBox bereits geladen ist und schließe sie gegebenenfalls
            if (_hintForSavegameMessageBoxObject != null && !_hintForSavegameMessageBoxObject.IsNullOrDestroyed())
            {
                _hintForSavegameMessageBoxObject.GetComponent<HintForSavegameMessageBox>().CloseMessageBox();
            }

            _hintForSavegameMessageBoxObject = Instantiate(hintForSavegameMessageBox, canvas.transform);
            _hintForSavegameMessageBoxObject.GetComponent<HintForSavegameMessageBox>().Activate();
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

            // Dictionary mit Zuordnung von searchId zu Controller-Typ
            Dictionary<long, Type> novelControllerMap = new Dictionary<long, Type>
            {
                { 2, typeof(ElternNovelImageController) },
                { 3, typeof(PresseNovelImageController) },
                { 4, typeof(NotarinNovelImageController) },
                { 6, typeof(BueroNovelImageController) },
                { 9, typeof(InvestorNovelImageController) },
                { 10, typeof(BankNovelImageController) },
                { 11, typeof(HonorarNovelImageController) },
                { 13, typeof(IntroNovelImageController) }
            };

            // Falls die ID in der Map existiert, versuche den Controller zu finden
            if (novelControllerMap.TryGetValue(searchId, out Type controllerType))
            {
                var controller = FindObjectsOfType(controllerType).FirstOrDefault() as MonoBehaviour;
 
                if (controller != null && savedData.CharacterPrefabData != null)
                {
                    if (savedData.CharacterPrefabData.TryGetValue(searchId, out CharacterData characterData))
                    {
                        // Setzt die Charakterattribute basierend auf dem gefundenen Controller
                        ApplyCharacterData(controller, characterData);
                    }
                }
            }

            RestoreCharacterExpressions(savedData);

            ActivateMessageBoxes();

            StartCoroutine(PlayNextEvent());
        }

        private void ApplyCharacterData(MonoBehaviour controller, CharacterData characterData)
        {
            if (controller is ElternNovelImageController elternController)
            {
                // Setze die Attribute basierend auf den gespeicherten Werten
                elternController.novelKite2CharacterController.SetSkinSprite(characterData.skinIndex);
                elternController.novelKite2CharacterController.SetHandSprite(characterData.handIndex);
                elternController.novelKite2CharacterController.SetClotheSprite(characterData.clotheIndex);
                elternController.novelKite2CharacterController.SetHairSprite(characterData.hairIndex);

                elternController.novelKite2CharacterController2.SetSkinSprite(characterData.skinIndex2);
                elternController.novelKite2CharacterController2.SetGlassesSprite(characterData.glassIndex2);
                elternController.novelKite2CharacterController2.SetClotheSprite(characterData.clotheIndex2);
                elternController.novelKite2CharacterController2.SetHairSprite(characterData.hairIndex2);
            }
            
            if (controller is PresseNovelImageController presseController)
            {
                // Setze die Attribute basierend auf den gespeicherten Werten
                presseController.novelKite2CharacterController.SetSkinSprite(characterData.skinIndex);
                presseController.novelKite2CharacterController.SetHandSprite(characterData.handIndex);
                presseController.novelKite2CharacterController.SetClotheSprite(characterData.clotheIndex);
                presseController.novelKite2CharacterController.SetHairSprite(characterData.hairIndex);
            }

            if (controller is NotarinNovelImageController notarinController)
            {
                // Setze die Attribute basierend auf den gespeicherten Werten
                notarinController.novelKite2CharacterController.SetSkinSprite(characterData.skinIndex);
                notarinController.novelKite2CharacterController.SetClotheSprite(characterData.clotheIndex);
                notarinController.novelKite2CharacterController.SetHairSprite(characterData.hairIndex);
            }

            if (controller is BueroNovelImageController bueroController)
            {
                // Setze die Attribute basierend auf den gespeicherten Werten
                bueroController.novelKite2CharacterController.SetSkinSprite(characterData.skinIndex);
                bueroController.novelKite2CharacterController.SetClotheSprite(characterData.clotheIndex);
                bueroController.novelKite2CharacterController.SetHairSprite(characterData.hairIndex);
            }

            if (controller is InvestorNovelImageController investorController)
            {
                // Setze die Attribute basierend auf den gespeicherten Werten
                investorController.novelKite2CharacterController.SetSkinSprite(characterData.skinIndex);
                // investorController.novelKite2CharacterController.SetHandSprite(characterData.handIndex);
                investorController.novelKite2CharacterController.SetClotheSprite(characterData.clotheIndex);
                investorController.novelKite2CharacterController.SetHairSprite(characterData.hairIndex);
            }

            if (controller is BankNovelImageController bankController)
            {
                // Setze die Attribute basierend auf den gespeicherten Werten
                bankController.novelKite2CharacterController.SetSkinSprite(characterData.skinIndex);
                bankController.novelKite2CharacterController.SetHandSprite(characterData.handIndex);
                bankController.novelKite2CharacterController.SetClotheSprite(characterData.clotheIndex);
                bankController.novelKite2CharacterController.SetHairSprite(characterData.hairIndex);
            }

            if (controller is HonorarNovelImageController honorarController)
            {
                // Setze die Attribute basierend auf den gespeicherten Werten
                honorarController.novelKite2CharacterController.SetSkinSprite(characterData.skinIndex);
                // honorarController.novelKite2CharacterController.SetHandSprite(characterData.handIndex);
                honorarController.novelKite2CharacterController.SetClotheSprite(characterData.clotheIndex);
                honorarController.novelKite2CharacterController.SetHairSprite(characterData.hairIndex);
            }
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