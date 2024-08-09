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
    [SerializeField] private GameObject viewPort;
    [SerializeField] private GameObject[] novelVisuals;
    [SerializeField] private Button closeButton;
    [SerializeField] private LeaveNovelAndGoBackMessageBox leaveGameAndGoBackMessageBoxObject;
    [SerializeField] private GameObject leaveGameAndGoBackMessageBox;
    [SerializeField] private VisualNovel novelToPlay;
    [SerializeField] private TextMeshProUGUI novelName;
    [SerializeField] private ConversationContentGuiController conversationContent;
    [SerializeField] private GameObject novelImageContainer;
    [SerializeField] private GameObject novelBackgroundPrefab;
    [SerializeField] private GameObject characterPrefabMayer;
    [SerializeField] private GameObject characterPrefabReporterin;
    [SerializeField] private GameObject characterPrefabVermieter;
    [SerializeField] private bool isWaitingForConfirmation = false;
    [SerializeField] private Dictionary<string, VisualNovelEvent> novelEvents = new Dictionary<string, VisualNovelEvent>();
    [SerializeField] private VisualNovelEvent nextEventToPlay;
    [SerializeField] private GameObject conversationViewport;
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
    [SerializeField] private ChatScrollView chatScroll;
    [SerializeField] private ImageScrollView imageScroll;
    [SerializeField] public TypewriterCore currentTypeWriter;
    [SerializeField] public SelectOptionContinueConversation selectOptionContinueConversation;
    [SerializeField] public Button confirmArea;
    [SerializeField] public Button confirmArea2;
    [SerializeField] private CharacterController currentTalkingCharacterController;
    [SerializeField] private GameObject tapToContinueAnimation;
    [SerializeField] private bool isTyping;
    [SerializeField] private List<string> playThroughHistory = new List<string>();
    [SerializeField] private GameObject gptServercallPrefab;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] clips;

    [SerializeField] private GameObject[] novelAnimations;
    [SerializeField] private GameObject viewPortOfImages;
    [SerializeField] private GameObject currentAnimation;

    [SerializeField] private GameObject backgroundBlur;
    [SerializeField] private GameObject imageAreaBlur;
    [SerializeField] private GameObject screenContentBlur;

    [SerializeField] private GameObject backgroundColor;
    [SerializeField] private GameObject imageAreaColor;
    [SerializeField] private GameObject screenContentColor;

    [SerializeField] private GameObject freeTextInputPrefab;

    [SerializeField] private Coroutine timerCoroutine;
    [SerializeField] private float timerForHint = 12.0f; // Time after which the hint to tap on the screen is shown
    [SerializeField] private float timerForHintInitial = 3.0f;

    // Analytics
    [SerializeField] private bool firstUserConfirmation = true;

    private bool tapedAlready = false;

    [SerializeField] private TTSEngine engine;

    private NovelImageController novelImagesController = null;

    void Start()
    {
        tapToContinueAnimation.SetActive(false);
        tapToContinueAnimation.GetComponent<Animator>().enabled = false;
        AnalyticsServiceHandler.Instance().StartStopwatch();
        TextToSpeechService.Instance().SetAudioSource(audioSource);
        BackStackManager.Instance().Push(SceneNames.PLAY_NOVEL_SCENE);
        novelToPlay = PlayManager.Instance().GetVisualNovelToPlay();
        NovelBiasManager.Clear();
        OfflineFeedbackManager.Instance().Clear();
        Initialize();
    }

    public void OnCloseButton()
    {
        if (!DestroyValidator.IsNullOrDestroyed(leaveGameAndGoBackMessageBoxObject))
        {
            leaveGameAndGoBackMessageBoxObject.CloseMessageBox();
        }
        if (DestroyValidator.IsNullOrDestroyed(canvas))
        {
            return;
        }
        leaveGameAndGoBackMessageBoxObject = null;
        leaveGameAndGoBackMessageBoxObject = Instantiate(leaveGameAndGoBackMessageBox, 
            canvas.transform).GetComponent<LeaveNovelAndGoBackMessageBox>();
        leaveGameAndGoBackMessageBoxObject.Activate();
    }

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

        foreach (VisualNovelEvent novelEvent in novelToPlay.novelEvents)
        {
            novelEvents.Add(novelEvent.id, novelEvent);
        }
        nextEventToPlay = novelToPlay.novelEvents[0];
        StartCoroutine(StartNextEventInOneSeconds(1));
    }

    public void OnConfirm()
    {
        Vector2 mousePosition = Input.mousePosition;

        if(novelImagesController.HandleTouchEvent(mousePosition.x, mousePosition.y, audioSource))
        {
            return;
        }
        if (!isWaitingForConfirmation)
        {
            return;
        }
        if(firstUserConfirmation)
        {
            firstUserConfirmation = false;
            tapedAlready = true;
            AnalyticsServiceHandler.Instance().SendPlayNovelFirstConfirmation(); 
        }
        SetWaitingForConfirmation(false);
        PlayNextEvent();
    }

    private void SetVisualElements()
    {
        Debug.Log(novelToPlay.title);
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        
        RectTransform conversationViewportTransform = conversationViewport.GetComponent<RectTransform>();

        conversationViewportTransform.sizeDelta = new Vector2(0, -canvasRect.rect.height * 0.5f);
        RectTransform viewPortTransform = viewPort.GetComponent<RectTransform>();
        switch(novelToPlay.title)
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
                    HandleEndNovelEvent(nextEventToPlay);
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

        if (novelEvent.audioClipToPlay != 0) {
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

        if (novelEvent.waitForUserConfirmation)
        {
            SetWaitingForConfirmation(true);
            return;
        }
        StartCoroutine(StartNextEventInOneSeconds(1));
    }

    public void HandleCharacterExitEvent(VisualNovelEvent novelEvent)
    {
        SetNextEvent(novelEvent);

        novelImagesController.DestroyCharacter();
        if (novelEvent.waitForUserConfirmation)
        {
            SetWaitingForConfirmation(true);
            return;
        }
        StartCoroutine(StartNextEventInOneSeconds(1));
    }

    public void HandleShowMessageEvent(VisualNovelEvent novelEvent)
    {
        TextToSpeechService.Instance().TextToSpeechReadLive(novelEvent.text, engine);
        // novelEvent.text = ReplacePlaceholders(novelEvent.text, novelToPlay.GetGlobalVariables());

        SetNextEvent(novelEvent);

        novelImagesController.SetFaceExpression(novelEvent.expressionType);

        if(novelEvent.show){
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
        TextToSpeechService.Instance().TextToSpeechReadLive(TextToSpeechService.Instance().returnChoicesForTextToSpeech(), engine);
        AddEntryToPlayThroughHistory(CharacterTypeHelper.ValueOf(novelEvent.character), novelEvent.text);
        conversationContent.AddContent(novelEvent, this);
    }

    public void HandleEndNovelEvent(VisualNovelEvent novelEvent)
    {
        AnalyticsServiceHandler.Instance().SendNovelPlayTime();
        PlayRecordManager.Instance().IncrasePlayCounterForNovel(VisualNovelNamesHelper.ValueOf((int) novelToPlay.id));

        int userRole = FeedbackRoleManager.Instance.GetFeedbackRole();
        PlayerDataManager.Instance().SetNovelHistory(playThroughHistory);
        if ((userRole == 1 || userRole == 3 || userRole == 4 || userRole == 5) && ApplicationModeManager.Instance().IsOnlineModeActive())
        {
            SceneLoader.LoadReviewNovelScene();
        }
        else
        {
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
        SetTypeToContinueAnimationActive(value);
    }

    public void SetTypeToContinueAnimationActive(bool value)
    {
        tapToContinueAnimation.SetActive(value);
        tapToContinueAnimation.GetComponent<Animator>().enabled = value;
    }

    public void SetTyping(bool value)
    {
        isTyping = value;

        if (!isTyping && isWaitingForConfirmation)
        {
            SetTypeToContinueAnimationActive(true);
            return;
        }
        tapToContinueAnimation.SetActive(false);
        tapToContinueAnimation.GetComponent<Animator>().enabled = false;
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

    private void AddEntryToPlayThroughHistory(Character character, string text){
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
}

    
