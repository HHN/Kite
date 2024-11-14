using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Febucci.UI.Core;
using System;
using System.Text.RegularExpressions;
using LeastSquares.Overtone;
using System.Data;
using System.Linq;

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
    [SerializeField] private GameObject characterPrefabMayer;
    [SerializeField] private GameObject characterPrefabReporterin;
    [SerializeField] private GameObject characterPrefabVermieter;
    [SerializeField] private GameObject characterPrefabMutter;
    [SerializeField] private GameObject characterPrefabVater;
    [SerializeField] private GameObject characterPrefabIntro;
    [SerializeField] private GameObject backgroundContainer;
    [SerializeField] private GameObject deskContainer;
    [SerializeField] private GameObject decoDeskContainer;
    [SerializeField] private GameObject decoBackgroudContainer;
    [SerializeField] private GameObject[] backgroundPrefab;
    [SerializeField] private GameObject[] deskPrefab;
    [SerializeField] private GameObject[] decoDeskPrefab;
    [SerializeField] private GameObject[] decoBackgroundPrefab;
    [SerializeField] private GameObject currentBackground;
    [SerializeField] private GameObject currentDesk;
    [SerializeField] private GameObject currentDecoDesk;
    [SerializeField] private GameObject currentDecoBackgroud;
    [SerializeField] private GameObject characterContainer;
    [SerializeField] private GameObject[] novelAnimations;
    [SerializeField] private GameObject viewPortOfImages;
    [SerializeField] private GameObject currentAnimation;

    [Header("GPT und MessageBox")] [SerializeField]
    private GameObject gptServercallPrefab;

    [SerializeField] private LeaveNovelAndGoBackMessageBox leaveGameAndGoBackMessageBoxObject;
    [SerializeField] private GameObject leaveGameAndGoBackMessageBox;
    [SerializeField] private HintForSavegameMessageBox hintForSavegameMessageBoxObject;
    [SerializeField] private GameObject hintForSavegameMessageBox;

    [Header("Skript- und Controller-Referenzen")] [SerializeField]
    private VisualNovel novelToPlay;

    [SerializeField] public TypewriterCore currentTypeWriter;
    [SerializeField] public SelectOptionContinueConversation selectOptionContinueConversation;

    [SerializeField] private CharacterController currentTalkingCharacterController;

    //[SerializeField] private GameObject tapToContinueAnimation;
    [SerializeField] private TTSEngine engine;

    [Header("Audio-Komponenten")] [SerializeField]
    private AudioSource audioSource;

    [SerializeField] private AudioClip[] clips;
    [SerializeField] private float timerForHint = 12.0f; // Time after which the hint to tap on the screen is shown
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

    [Header("Timing und Analytics")] private Coroutine _timerCoroutine;
    private bool _typingWasSkipped;

    public bool IsPaused { get; set; }

    public VisualNovel NovelToPlay => novelToPlay;
    public VisualNovelEvent NextEventToPlay => nextEventToPlay;
    public List<string> PlayThroughHistory => playThroughHistory;

    private void Start()
    {
        _conversationContentGuiController = FindAnyObjectByType<ConversationContentGuiController>();

        //tapToContinueAnimation.SetActive(false);
        //tapToContinueAnimation.GetComponent<Animator>().enabled = false;
        AnalyticsServiceHandler.Instance().StartStopwatch();
        TextToSpeechService.Instance().SetAudioSource(audioSource);
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
        headerImage.SetActive(novelToPlay.title != "Einstiegsdialog");

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

            PlayNextEvent();
        }
    }

    public void OnConfirm()
    {
        Vector2 mousePosition = Input.mousePosition;

        if (_novelImagesController.HandleTouchEvent(mousePosition.x, mousePosition.y, audioSource))
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

            return; // Beendet die Methode, um nicht zum nächsten Event zu springen
        }

        if (!isWaitingForConfirmation)
        {
            return;
        }

        SetWaitingForConfirmation(false);
        PlayNextEvent();
    }

    private void SetVisualElements()
    {
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        RectTransform conversationViewportTransform = conversationViewport.GetComponent<RectTransform>();

        conversationViewportTransform.sizeDelta = new Vector2(0, -canvasRect.rect.height * 0.5f);
        RectTransform viewPortTransform = viewPort.GetComponent<RectTransform>();

        switch (novelToPlay.title)
        {
            case "Bank Kontoeröffnung":
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

    public void PlayNextEvent()
    {
        // Stop if paused
        if (IsPaused)
        {
            return;
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
        if (nextEventToPlay.id.StartsWith("OptionsLabel"))
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
                PlayNextEvent();
                break;
            }
        }
    }

    private void HandlePlaySoundEvent(VisualNovelEvent novelEvent)
    {
        SetNextEvent(novelEvent);

        if (novelEvent.audioClipToPlay != 0)
        {
            audioSource.clip = clips[novelEvent.audioClipToPlay];
            audioSource.Play();
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
            || novelEvent.variablesNameForGptPromp == String.Empty
            || novelEvent.variablesNameForGptPromp == "")
        {
            return;
        }

        if (ApplicationModeManager.Instance().IsOfflineModeActive())
        {
            PlayNextEvent();
            return;
        }

        GetCompletionServerCall call = Instantiate(gptServercallPrefab).GetComponent<GetCompletionServerCall>();
        call.sceneController = this;
        GptRequestEventOnSuccessHandler onSuccessHandler = new GptRequestEventOnSuccessHandler
        {
            variablesNameForGptPromp = novelEvent.variablesNameForGptPromp,
            completionHandler = GptCompletionHandlerManager.Instance()
                .GetCompletionHandlerById(novelEvent.gptCompletionHandlerId)
        };
        call.onSuccessHandler = onSuccessHandler;
        call.prompt = ReplacePlaceholders(novelEvent.gptPrompt, novelToPlay.GetGlobalVariables());
        call.SendRequest();
        DontDestroyOnLoad(call.gameObject);

        if (novelEvent.waitForUserConfirmation)
        {
            SetWaitingForConfirmation(true);
            return;
        }

        PlayNextEvent();
    }

    private void HandleSavePersistentEvent(VisualNovelEvent novelEvent)
    {
        SetNextEvent(novelEvent);
        WriteUserInputToFile(novelEvent.key, ReplacePlaceholders(novelEvent.value, novelToPlay.GetGlobalVariables()));
        PlayNextEvent();
    }

    private void HandleSaveVariableEvent(VisualNovelEvent novelEvent)
    {
        SetNextEvent(novelEvent);
        novelToPlay.AddGlobalVariable(novelEvent.key, novelEvent.value);
        PlayNextEvent();
    }

    private void HandleCalculateVariableFromBooleanExpressionEvent(VisualNovelEvent novelEvent)
    {
        SetNextEvent(novelEvent);
        string booleanExpression = ReplacePlaceholders(novelEvent.value, novelToPlay.GetGlobalVariables());
        novelToPlay.AddGlobalVariable(novelEvent.key, EvaluateBooleanExpression(booleanExpression).ToString());
        PlayNextEvent();
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
        PlayNextEvent();
    }

    private void HandleAddFeedbackUnderConditionEvent(VisualNovelEvent novelEvent)
    {
        SetNextEvent(novelEvent);

        if (novelToPlay.IsVariableExistend(novelEvent.key) && (novelToPlay.GetGlobalVariable(novelEvent.key) == "True"
                                                               || novelToPlay.GetGlobalVariable(novelEvent.key) ==
                                                               "true" ||
                                                               novelToPlay.GetGlobalVariable(novelEvent.key) == "TRUE"))
        {
            OfflineFeedbackManager.Instance().AddLineToPrompt(novelEvent.value);
        }

        PlayNextEvent();
    }

    private void HandleMarkBiasEvent(VisualNovelEvent novelEvent)
    {
        SetNextEvent(novelEvent);
        string biasInformation =
            DiscriminationBiasHelper.GetInformationString(DiscriminationBiasHelper.ValueOf(novelEvent.relevantBias));
        PromptManager.Instance().AddFormattedLineToPrompt("Hinweis", biasInformation);
        NovelBiasManager.Instance().MarkBiasAsRelevant(DiscriminationBiasHelper.ValueOf(novelEvent.relevantBias));
        PlayNextEvent();
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

        //if (novelEvent.waitForUserConfirmation)
        //{
        //    SetWaitingForConfirmation(true);
        //    return;
        //}

        StartCoroutine(StartNextEventInOneSeconds(1));
    }

    private void HandleCharacterExitEvent(VisualNovelEvent novelEvent)
    {
        SetNextEvent(novelEvent);

        _novelImagesController.DestroyCharacter();

        //if (novelEvent.waitForUserConfirmation)
        //{
        //    SetWaitingForConfirmation(true);
        //    return;
        //}

        StartCoroutine(StartNextEventInOneSeconds(1));
    }

    private void HandleShowMessageEvent(VisualNovelEvent novelEvent)
    {
        //TextToSpeechManager.Instance.Speak(novelEvent.text);


        //TextToSpeechService.Instance().TextToSpeechReadLive(novelEvent.text, engine); --> TODO: Überall entfernen
        // novelEvent.text = ReplacePlaceholders(novelEvent.text, novelToPlay.GetGlobalVariables());

        SetNextEvent(novelEvent);

        _novelImagesController.SetFaceExpression(novelEvent.character, novelEvent.expressionType);
        _novelCharacter = novelEvent.character;

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
        TextToSpeechService.Instance().addChoiceToChoiceCollectionForTextToSpeech(novelEvent.text);
        PlayNextEvent();
    }

    private void HandleShowChoicesEvent(VisualNovelEvent novelEvent)
    {
        _novelImagesController.SetFaceExpression(_novelCharacter, 5);
        // Enable animations when showing choices
        AnimationFlagSingleton.Instance().SetFlag(true);

        // Convert the available choices to speech for accessibility using Text-to-Speech (TTS)
        TextToSpeechService.Instance()
            .TextToSpeechReadLive(TextToSpeechService.Instance().returnChoicesForTextToSpeech(), engine);

        // Log the event to the playthrough history, including the character and their dialogue
        AddEntryToPlayThroughHistory(CharacterTypeHelper.ValueOf(novelEvent.character), novelEvent.text);

        // Add the conversation content to the UI or dialogue system
        conversationContent.AddContent(novelEvent, this);
    }

    public void HandleEndNovelEvent()
    {
        AnalyticsServiceHandler.Instance().SendNovelPlayTime();

        PlayRecordManager.Instance().IncrasePlayCounterForNovel(VisualNovelNamesHelper.ValueOf((int)novelToPlay.id));

        PlaythrouCounterAnimationManager.Instance()
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
        PlayNextEvent();
    }

    public void ShowAnswer(string message, bool show)
    {
        if (!show)
        {
            return;
        }

        AddEntryToPlayThroughHistory(Character.PLAYER, message);
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

    private void ScrollToBottom()
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
        //SetTypeToContinueAnimationActive(value);
    }

    //public void SetTypeToContinueAnimationActive(bool value)
    //{
    //    tapToContinueAnimation.SetActive(value);
    //    tapToContinueAnimation.GetComponent<Animator>().enabled = value;
    //}

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

        //tapToContinueAnimation.SetActive(false);
        //tapToContinueAnimation.GetComponent<Animator>().enabled = false;
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

    private void AddEntryToPlayThroughHistory(Character character, string text)
    {
        playThroughHistory.Add(CharacterTypeHelper.GetNameOfCharacter(character) + ": " + text);
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
        if (!string.IsNullOrEmpty(_optionsId[0]))
        {
            // The ID of the event we want to restore to
            string eventIdToRestore = _optionsId[0];

            // Find the index of the event in the eventHistory list
            int indexToRestore = eventHistory.FindIndex(e => e.id == eventIdToRestore);

            if (indexToRestore != -1) // If the event is found in the history
            {
                // Remove the event and all events after it
                eventHistory.RemoveRange(indexToRestore, eventHistory.Count - indexToRestore);

                // Now update playThroughHistory
                // Search from back to front for the second occurrence of ":"
                int colonCount = 0;
                int indexToRemoveFrom = -1;

                // Traverse the list backwards
                for (int i = playThroughHistory.Count - 1; i >= 0; i--)
                {
                    if (playThroughHistory[i].Trim() == ":")
                    {
                        colonCount++;
                        if (colonCount == 2)
                        {
                            indexToRemoveFrom = i;
                            break;
                        }
                    }
                }

                if (indexToRemoveFrom != -1)
                {
                    // Remove all entries from the found index onwards
                    playThroughHistory.RemoveRange(indexToRemoveFrom, playThroughHistory.Count - indexToRemoveFrom);

                    _conversationContentGuiController.ClearUIAfter(indexToRemoveFrom);
                }

                // Set the previous event as the next event to play, if present
                if (indexToRestore > 0)
                {
                    SetNextEvent(eventIdToRestore);
                    PlayNextEvent();
                }
            }
        }
    }

    // Methode zum Anzeigen der HintForSavegameMessageBox
    private void ShowHintForSavegameMessageBox()
    {
        if (hintForSavegameMessageBox != null)
        {
            // Überprüfen, ob die HintForSavegameMessageBox bereits geladen ist und schließe sie gegebenenfalls
            if (!hintForSavegameMessageBoxObject.IsNullOrDestroyed())
            {
                hintForSavegameMessageBoxObject.CloseMessageBox();
            }

            // Instanziere und aktiviere die HintForSavegameMessageBox, falls das Canvas nicht null ist
            if (!canvas.IsNullOrDestroyed())
            {
                hintForSavegameMessageBoxObject = Instantiate(hintForSavegameMessageBox, canvas.transform)
                    .GetComponent<HintForSavegameMessageBox>();
                hintForSavegameMessageBoxObject.Activate();
            }
        }
    }

    // Startet das Spiel vom gespeicherten Punkt, wenn "Fortsetzen" gewählt wird
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
        nextEventToPlay = novelToPlay.novelEvents
            .FirstOrDefault(e => e.id == savedData.currentEvent);

        if (nextEventToPlay == null)
        {
            Debug.LogWarning(
                $"Event ID '{savedData.currentEvent}' not found in novelEvents. Starting from the first event.");
            nextEventToPlay = novelToPlay.novelEvents[0]; // Setze das erste Event als Standard
        }

        // Speichere den bisherigen Verlauf
        playThroughHistory = new List<string>(savedData.playThroughHistory);

        _conversationContentGuiController.ReconstructGuiContent(savedData);

        //Lösche den zugehörigen Speicherstand
        //SaveLoadManager.DeleteNovelSaveData(novelToPlay.id.ToString());

        PlayNextEvent();
    }


    // Methode zum Neustarten (bei Auswahl "Neustarten" im Dialog)
    public void RestartNovel()
    {
        // Lösche den zugehörigen Speicherstand
        SaveLoadManager.DeleteNovelSaveData(novelToPlay.id.ToString());

        // foreach (VisualNovelEvent novelEvent in novelToPlay.novelEvents)
        // {
        //     _novelEvents.Add(novelEvent.id, novelEvent);
        // }

        nextEventToPlay = novelToPlay.novelEvents[0];

        PlayNextEvent();
    }

    public VisualNovelEvent GetCurrentEvent()
    {
        return nextEventToPlay;
    }
}