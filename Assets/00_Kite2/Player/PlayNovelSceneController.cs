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

public class PlayNovelSceneController : SceneController
{
    [SerializeField] private VisualNovelEventType type;
    public bool isPaused = false;

    [Header("UI-Komponenten")]
    [SerializeField] private GameObject viewPort;
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

    [Header("GameObject-Referenzen und Prefabs")]
    [SerializeField] private GameObject[] novelVisuals;
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

    [SerializeField] private GameObject gptServercallPrefab;

    [SerializeField] private LeaveNovelAndGoBackMessageBox leaveGameAndGoBackMessageBoxObject;
    [SerializeField] private GameObject leaveGameAndGoBackMessageBox;

    [Header("Skript- und Controller-Referenzen")]
    [SerializeField] private VisualNovel novelToPlay;

    [SerializeField] public TypewriterCore currentTypeWriter;

    [SerializeField] public SelectOptionContinueConversation selectOptionContinueConversation;
    [SerializeField] private CharacterController currentTalkingCharacterController;
    //[SerializeField] private GameObject tapToContinueAnimation;
    [SerializeField] private TTSEngine engine;
    private NovelImageController novelImagesController = null;

    [Header("Audio-Komponenten")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] clips;

    [Header("Timing und Analytics")]
    [SerializeField] private Coroutine timerCoroutine;
    [SerializeField] private float timerForHint = 12.0f; // Time after which the hint to tap on the screen is shown
    [SerializeField] private float timerForHintInitial = 3.0f;
    // Analytics
    [SerializeField] private bool firstUserConfirmation = true;

    [Header("Spielstatus und Logik")]
    [SerializeField] private bool isWaitingForConfirmation = false;
    [SerializeField] private Dictionary<string, VisualNovelEvent> novelEvents = new Dictionary<string, VisualNovelEvent>();
    [SerializeField] private VisualNovelEvent nextEventToPlay;

    [SerializeField] private bool isTyping;
    public List<string> playThroughHistory = new List<string>();

    private bool tapedAlready = false;

    private float waitingTime = 0.5f;
    private bool typingWasSkipped = false;

    [SerializeField] private List<VisualNovelEvent> eventHistory = new List<VisualNovelEvent>();
    private string[] optionsId = new string[2];

    private ConversationContentGuiController conversationContentGuiController;

    private int novelCharacter = -1;

    void Start()
    {
        conversationContentGuiController = FindAnyObjectByType<ConversationContentGuiController>();

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

    //public void OnCloseButton()
    //{
    //    Debug.Log("OnCloseButton: Method called"); // Initial debug statement

    //    isPaused = true; // Pause the novel progression
    //    Debug.Log("OnCloseButton: Set isPaused to true");

    //    if (!DestroyValidator.IsNullOrDestroyed(leaveGameAndGoBackMessageBoxObject))
    //    {
    //        Debug.Log("OnCloseButton: leaveGameAndGoBackMessageBoxObject is valid, closing message box");
    //        leaveGameAndGoBackMessageBoxObject.CloseMessageBox();
    //    }

    //    if (DestroyValidator.IsNullOrDestroyed(canvas))
    //    {
    //        Debug.Log("OnCloseButton: Canvas is null or destroyed, exiting method");
    //        return;
    //    }

    //    Debug.Log("OnCloseButton: Instantiating leaveGameAndGoBackMessageBox");
    //    leaveGameAndGoBackMessageBoxObject = null;
    //    leaveGameAndGoBackMessageBoxObject = Instantiate(leaveGameAndGoBackMessageBox, canvas.transform).GetComponent<LeaveNovelAndGoBackMessageBox>();
    //    leaveGameAndGoBackMessageBoxObject.Activate();
    //    Debug.Log("OnCloseButton: leaveGameAndGoBackMessageBox activated");
    //}


    public void Initialize()
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
        if (novelToPlay.title == "Einstiegsdialog")
        {
            // Hide the header image, as it is not needed in the introductory dialogue
            headerImage.SetActive(false);
        }
        else
        {
            // Show the header image for other novels
            headerImage.SetActive(true);
        }

        foreach (VisualNovelEvent novelEvent in novelToPlay.novelEvents)
        {
            novelEvents.Add(novelEvent.id, novelEvent);
        }
        nextEventToPlay = novelToPlay.novelEvents[0];
        //StartCoroutine(StartNextEventInOneSeconds(0));
        PlayNextEvent();
    }

    public void OnConfirm()
    {
        Vector2 mousePosition = Input.mousePosition;

        if (novelImagesController.HandleTouchEvent(mousePosition.x, mousePosition.y, audioSource))
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

            typingWasSkipped = true; // Flag setzen
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
                    novelImagesController = controllerTransform.GetComponent<BankNovelImageController>();
                    break;
                }
            case "Anmietung eines Büros":
                {
                    GameObject novelImagesInstance = Instantiate(novelVisuals[1], viewPortTransform);
                    Transform controllerTransform = novelImagesInstance.transform.Find("Controller");
                    novelImagesController = controllerTransform.GetComponent<BueroNovelImageController>();
                    break;
                }
            case "Pressegespräch":
                {
                    GameObject novelImagesInstance = Instantiate(novelVisuals[2], viewPortTransform);
                    Transform controllerTransform = novelImagesInstance.transform.Find("Controller");
                    novelImagesController = controllerTransform.GetComponent<PresseNovelImageController>();
                    break;
                }
            case "Telefonat mit den Eltern":
                {
                    GameObject novelImagesInstance = Instantiate(novelVisuals[3], viewPortTransform);
                    Transform controllerTransform = novelImagesInstance.transform.Find("Controller");
                    novelImagesController = controllerTransform.GetComponent<ElternNovelImageController>();
                    break;
                }
            case "Telefonat mit der Notarin":
                {
                    GameObject novelImagesInstance = Instantiate(novelVisuals[4], viewPortTransform);
                    Transform controllerTransform = novelImagesInstance.transform.Find("Controller");
                    novelImagesController = controllerTransform.GetComponent<NotarinNovelImageController>();
                    break;
                }
            case "Gespräch mit einem Bekannten":
                {
                    GameObject novelImagesInstance = Instantiate(novelVisuals[5], viewPortTransform);
                    Transform controllerTransform = novelImagesInstance.transform.Find("Controller");
                    novelImagesController = controllerTransform.GetComponent<BekannterNovelImageController>();
                    break;
                }
            case "Einstiegsdialog":
                {
                    GameObject novelImagesInstance = Instantiate(novelVisuals[6], viewPortTransform);
                    Transform controllerTransform = novelImagesInstance.transform.Find("Controller");
                    novelImagesController = controllerTransform.GetComponent<IntroNovelImageController>();
                    break;
                }
            default:
                {
                    GameObject novelImagesInstance = Instantiate(novelVisuals[0], viewPortTransform);
                    Transform controllerTransform = novelImagesInstance.transform.Find("Controller");
                    novelImagesController = controllerTransform.GetComponent<BankNovelImageController>();
                    break;
                }
        }
        novelImagesController.SetCanvasRect(canvasRect);
    }

    public void PlayNextEvent()
    {
        // Stop if paused
        if (isPaused)
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
            if (int.TryParse(numericPart, out int result))
            {
                // Wenn der Rest eine Zahl ist, speichere das Event
                optionsId[0] = optionsId[1];  // Verschiebe das letzte Event
                optionsId[1] = nextEventToPlay.id; // Speichere das aktuelle Event
            }
        }

        // Save the current event in the eventHistory list
        eventHistory.Add(nextEventToPlay);

        type = VisualNovelEventTypeHelper.ValueOf(nextEventToPlay.eventType);

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
                    nextEventToPlay = novelEvents[nextEventID];
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
        GptRequestEventOnSuccessHandler onSuccessHandler = new GptRequestEventOnSuccessHandler();
        onSuccessHandler.variablesNameForGptPromp = novelEvent.variablesNameForGptPromp;
        onSuccessHandler.completionHandler = GptCompletionHandlerManager.Instance().GetCompletionHandlerById(novelEvent.gptCompletionHandlerId);
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

        expression = expression.Replace("true", "True").Replace("TRUE", "True").Replace("false", "False").Replace("FALSE", "False");
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
            || novelToPlay.GetGlobalVariable(novelEvent.key) == "true" || novelToPlay.GetGlobalVariable(novelEvent.key) == "TRUE"))
        {
            OfflineFeedbackManager.Instance().AddLineToPrompt(novelEvent.value);
        }
        PlayNextEvent();
    }

    private void HandleMarkBiasEvent(VisualNovelEvent novelEvent)
    {
        SetNextEvent(novelEvent);
        string biasInformation = DiscriminationBiasHelper.GetInformationString(DiscriminationBiasHelper.ValueOf(novelEvent.relevantBias));
        PromptManager.Instance().AddFormattedLineToPrompt("Hinweis", biasInformation);
        NovelBiasManager.Instance().MarkBiasAsRelevant(DiscriminationBiasHelper.ValueOf(novelEvent.relevantBias));
        PlayNextEvent();
    }

    private void WriteUserInputToFile(string key, string content)
    {
        PlayerDataManager.Instance().SavePlayerData(key, content);
    }

    public void HandleBackgroundEvent(VisualNovelEvent novelEvent)
    {
        SetNextEvent(novelEvent);

        novelImagesController.SetBackground();
        StartCoroutine(imageScroll.ScrollToPoint(0.5f, 1f));
        StartCoroutine(StartNextEventInOneSeconds(1));
    }

    public void HandleCharacterJoinEvent(VisualNovelEvent novelEvent)
    {
        SetNextEvent(novelEvent);
        novelImagesController.SetCharacter();

        //if (novelEvent.waitForUserConfirmation)
        //{
        //    SetWaitingForConfirmation(true);
        //    return;
        //}

        StartCoroutine(StartNextEventInOneSeconds(1));
    }

    public void HandleCharacterExitEvent(VisualNovelEvent novelEvent)
    {
        SetNextEvent(novelEvent);

        novelImagesController.DestroyCharacter();

        //if (novelEvent.waitForUserConfirmation)
        //{
        //    SetWaitingForConfirmation(true);
        //    return;
        //}

        StartCoroutine(StartNextEventInOneSeconds(1));
    }

    public void HandleShowMessageEvent(VisualNovelEvent novelEvent)
    {
        //TextToSpeechManager.Instance.Speak(novelEvent.text);


        //TextToSpeechService.Instance().TextToSpeechReadLive(novelEvent.text, engine); --> TODO: Überall entfernen
        // novelEvent.text = ReplacePlaceholders(novelEvent.text, novelToPlay.GetGlobalVariables());

        SetNextEvent(novelEvent);

        novelImagesController.SetFaceExpression(novelEvent.character, novelEvent.expressionType);
        novelCharacter = novelEvent.character;

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

    public void HandleAddChoiceEvent(VisualNovelEvent novelEvent)
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

    public void HandleShowChoicesEvent(VisualNovelEvent novelEvent)
    {
        novelImagesController.SetFaceExpression(novelCharacter, 5);
        // Enable animations when showing choices
        AnimationFlagSingleton.Instance().SetFlag(true);

        // Convert the available choices to speech for accessibility using Text-to-Speech (TTS)
        TextToSpeechService.Instance().TextToSpeechReadLive(TextToSpeechService.Instance().returnChoicesForTextToSpeech(), engine);

        // Log the event to the playthrough history, including the character and their dialogue
        AddEntryToPlayThroughHistory(CharacterTypeHelper.ValueOf(novelEvent.character), novelEvent.text);

        // Add the conversation content to the UI or dialogue system
        conversationContent.AddContent(novelEvent, this);
    }

    public void HandleEndNovelEvent()
    {
        AnalyticsServiceHandler.Instance().SendNovelPlayTime();

        PlayRecordManager.Instance().IncrasePlayCounterForNovel(VisualNovelNamesHelper.ValueOf((int)novelToPlay.id));

        PlaythrouCounterAnimationManager.Instance().SetAnimation(true, VisualNovelNamesHelper.ValueOf((int)novelToPlay.id));

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
        if ((userRole == 1 || userRole == 3 || userRole == 4 || userRole == 5) && ApplicationModeManager.Instance().IsOnlineModeActive())
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

    public IEnumerator StartNextEventInOneSeconds(float second)
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
        nextEventToPlay = novelEvents[id];
    }

    private void SetNextEvent(VisualNovelEvent novelEvent)
    {
        string nextEventID = novelEvent.nextId;
        nextEventToPlay = novelEvents[nextEventID];
    }

    public void ScrollToBottom()
    {
        StartCoroutine(chatScroll.ScrollToBottom());
        FontSizeManager.Instance().UpdateAllTextComponents();
    }

    public void StartTalking()
    {
        novelImagesController.StartCharacterTalking();
    }

    public void StopTalking()
    {
        novelImagesController.StopCharacterTalking();
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

            float delay = waitingTime;
            if (typingWasSkipped)
            {
                delay = 0f; // Wartezeit überspringen
                typingWasSkipped = false; // Flag zurücksetzen
            }

            StartCoroutine(StartNextEventInOneSeconds(delay));
            return;
        }

        //tapToContinueAnimation.SetActive(false);
        //tapToContinueAnimation.GetComponent<Animator>().enabled = false;
    }

    public void AnimationFinished()
    {
        SetWaitingForConfirmation(true);

        if (DestroyValidator.IsNullOrDestroyed(currentAnimation))
        {
            return;
        }
        Destroy(currentAnimation);
    }

    private void AddEntryToPlayThroughHistory(Character character, string text)
    {
        playThroughHistory.Add(CharacterTypeHelper.GetNameOfCharacter(character) + ": " + text);
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
        if (!string.IsNullOrEmpty(optionsId[0]))
        {
            // The ID of the event we want to restore to
            string eventIdToRestore = optionsId[0];

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

                    conversationContentGuiController.ClearUIAfter(indexToRemoveFrom);
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
}
