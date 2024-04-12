using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Febucci.UI.Core;
using System;
using System.Text.RegularExpressions;

public class PlayNovelSceneController : SceneController
{
    [SerializeField] private VisualNovel novelToPlay;
    [SerializeField] private TextMeshProUGUI novelName;
    [SerializeField] private ConversationContentGuiController conversationContent;
    [SerializeField] private GameObject novelImageContainer;
    [SerializeField] private GameObject novelBackgroundPrefab;
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private bool isWaitingForConfirmation = false;
    [SerializeField] private Dictionary<string, VisualNovelEvent> novelEvents = new Dictionary<string, VisualNovelEvent>();
    [SerializeField] private VisualNovelEvent nextEventToPlay;
    [SerializeField] private GameObject backgroundContainer;
    [SerializeField] private GameObject[] backgroundPrefab;
    [SerializeField] private GameObject currentBackground;
    [SerializeField] private GameObject characterContainer;
    [SerializeField] private Dictionary<Character, GameObject> currentCharacters = new Dictionary<Character, GameObject>();
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
    [SerializeField] private float timerForHint = 5.0f;

    // Analytics
    [SerializeField] private bool firstUserConfirmation = true;

    void Start()
    {
        tapToContinueAnimation.SetActive(false);
        tapToContinueAnimation.GetComponent<Animator>().enabled = false;
        AnalyticsServiceHandler.Instance().StartStopwatch();
        TextToSpeechService.Instance().SetAudioSource(audioSource);
        BackStackManager.Instance().Push(SceneNames.PLAY_NOVEL_SCENE);
        novelToPlay = PlayManager.Instance().GetVisualNovelToPlay();
        NovelBiasManager.Clear();
        Initialize();
        TextToSpeechService.Instance().SetNovelTitle(novelToPlay.title);
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

        novelName.text = novelToPlay.title;

        if (novelToPlay.novelEvents.Count <= 0)
        {
            return;
        }

        foreach (VisualNovelEvent novelEvent in novelToPlay.novelEvents)
        {
            novelEvents.Add(novelEvent.id, novelEvent);
        }
        nextEventToPlay = novelToPlay.novelEvents[0];
        StartCoroutine(StartNextEventInOneSeconds(1));
    }

    public void OnConfirm()
    {
        if (!isWaitingForConfirmation)
        {
            return;
        }
        if(firstUserConfirmation)
        {
            firstUserConfirmation = false;
            AnalyticsServiceHandler.Instance().SendPlayNovelFirstConfirmation(); 
        }
        SetWaitingForConfirmation(false);
        PlayNextEvent();
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
                    HandleBackgrundEvent(nextEventToPlay);
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
            case VisualNovelEventType.MARK_BIAS_EVENT:
                {
                    HandleMarkBiasEvent(nextEventToPlay);
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
        string nextEventID = novelEvent.nextId;
        nextEventToPlay = novelEvents[nextEventID];

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
        string nextEventID = novelEvent.nextId;
        nextEventToPlay = novelEvents[nextEventID];

        if (novelEvent.animationToPlay != 0)
        {
            currentAnimation = Instantiate(novelAnimations[novelEvent.animationToPlay], viewPortOfImages.transform);
        }
    }

    private void HandleFreeTextInputEvent(VisualNovelEvent novelEvent)
    {
        string nextEventID = novelEvent.nextId;
        nextEventToPlay = novelEvents[nextEventID];

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
        string nextEventID = novelEvent.nextId;
        nextEventToPlay = novelEvents[nextEventID];

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
        string nextEventID = novelEvent.nextId;
        nextEventToPlay = novelEvents[nextEventID];
        WriteUserInputToFile(novelEvent.key, ReplacePlaceholders(novelEvent.value, novelToPlay.GetGlobalVariables()));
        PlayNextEvent();
    }

    private void HandleMarkBiasEvent(VisualNovelEvent novelEvent)
    {
        string nextEventID = novelEvent.nextId;
        nextEventToPlay = novelEvents[nextEventID];
        NovelBiasManager.Instance().MarkBiasAsRelevant(DiscriminationBiasHelper.ValueOf(novelEvent.relevantBias));
        PlayNextEvent();
    }

    private void WriteUserInputToFile(string key, string content)
    {
        PlayerDataManager.Instance().SavePlayerData(key, content);
    }

    public void HandleBackgrundEvent(VisualNovelEvent novelEvent)
    {
        string nextEventID = novelEvent.nextId;
        nextEventToPlay = novelEvents[nextEventID];

        if (currentBackground != null)
        {
            Destroy(currentBackground);
        }
        currentBackground = Instantiate(backgroundPrefab[(novelEvent.backgroundSpriteId-1)], backgroundContainer.transform);
        StartCoroutine(imageScroll.ScrollToPoint(0.5f, 1f));

        if (novelEvent.waitForUserConfirmation)
        {
            SetWaitingForConfirmation(true);
            return;
        }
        StartCoroutine(StartNextEventInOneSeconds(1));
    }

    public void HandleCharacterJoinEvent(VisualNovelEvent novelEvent)
    {
        string nextEventID = novelEvent.nextId;
        nextEventToPlay = novelEvents[nextEventID];

        GameObject character = Instantiate(characterPrefab, characterContainer.transform);
        CharacterController controller = character.GetComponent<CharacterController>();

        if (CharacterTypeHelper.ValueOf(novelEvent.character) == Character.MAYER)
        {
            controller.SetSkinSprite(0);
            controller.SetClotheSprite(0);
            controller.SetHairSprite(0);
            controller.SetFaceExpression(0);
        }

        currentCharacters.Add(CharacterTypeHelper.ValueOf(novelEvent.character), character);

        if (novelEvent.waitForUserConfirmation)
        {
            SetWaitingForConfirmation(true);
            return;
        }
        StartCoroutine(StartNextEventInOneSeconds(1));
    }

    public void HandleCharacterExitEvent(VisualNovelEvent novelEvent)
    {
        string nextEventID = novelEvent.nextId;
        nextEventToPlay = novelEvents[nextEventID];

        // For the purpose of robust design against spelling mistakes
        foreach (var character in currentCharacters.Values)
        {
            Destroy(character);
        }
        currentCharacters.Clear();

        if (novelEvent.waitForUserConfirmation)
        {
            SetWaitingForConfirmation(true);
            return;
        }
        StartCoroutine(StartNextEventInOneSeconds(1));
    }

    public void HandleShowMessageEvent(VisualNovelEvent novelEvent)
    {
        //Debug.Log(novelEvent.text);   // Needed to copy the text from the novel to AWS Polly
        TextToSpeechService.Instance().TextToSpeech(novelToPlay.title+novelEvent.id);
        novelEvent.text = ReplacePlaceholders(novelEvent.text, novelToPlay.GetGlobalVariables());

        string nextEventID = novelEvent.nextId;
        nextEventToPlay = novelEvents[nextEventID];

        if (currentCharacters.ContainsKey(CharacterTypeHelper.ValueOf(novelEvent.character)))
        {
            GameObject character = currentCharacters[CharacterTypeHelper.ValueOf(novelEvent.character)];
            currentTalkingCharacterController = character.GetComponent<CharacterController>();
            currentTalkingCharacterController.SetFaceExpression(novelEvent.expressionType);
        }

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
        string nextEventID = novelEvent.nextId;
        nextEventToPlay = novelEvents[nextEventID];

        conversationContent.AddContent(novelEvent, this);

        if (novelEvent.waitForUserConfirmation)
        {
            SetWaitingForConfirmation(true);
            return;
        }
        AnalyticsServiceHandler.Instance().AddChoiceToList(novelEvent.text);
        TextToSpeechService.Instance().addChoiceToChoiceCollectionForTextToSpeech(novelEvent.onChoice);
        PlayNextEvent();
    }

    public void HandleShowChoicesEvent(VisualNovelEvent novelEvent)
    {
        // TODO: Implement text to speech 
        TextToSpeechService.Instance().TextToSpeech(novelToPlay.title + TextToSpeechService.Instance().returnChoicesForTextToSpeech());
        AddEntryToPlayThroughHistory(CharacterTypeHelper.ValueOf(novelEvent.character), novelEvent.text);
        conversationContent.AddContent(novelEvent, this);
    }

    public void HandleEndNovelEvent(VisualNovelEvent novelEvent)
    {
        AnalyticsServiceHandler.Instance().SendNovelPlayTime();

        int userRole = FeedbackRoleManager.Instance.GetFeedbackRole();
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

    public void ScrollToBottom()
    {
        StartCoroutine(chatScroll.ScrollToBottom());
    }

    public void StartTalking()
    {
        if (currentTalkingCharacterController == null)
        {
            return;
        }
        currentTalkingCharacterController.StartTalking();
    }

    public void StopTalking()
    {
        if (currentTalkingCharacterController == null)
        {
            return;
        }
        currentTalkingCharacterController.StopTalking();
        currentTalkingCharacterController = null;
    }

    public void SetWaitingForConfirmation(bool value)
    {
        this.isWaitingForConfirmation = value;
        SetTypeToContinueAnimationActive(value);
    }

    public void SetTypeToContinueAnimationActive(bool value)
    {
        if (value)
        {
            timerCoroutine = StartCoroutine(SetAnimationToTrue()); // Startet die Coroutine neu.
        } else {
            StopCoroutine(timerCoroutine);
            tapToContinueAnimation.SetActive(value);
            tapToContinueAnimation.GetComponent<Animator>().enabled = value;
        }
    }

    IEnumerator SetAnimationToTrue()
    {
        yield return new WaitForSeconds(timerForHint); // Wartet für die angegebene Zeit.
        tapToContinueAnimation.SetActive(true);
        tapToContinueAnimation.GetComponent<Animator>().enabled = true;
    }

    public void SetTyping(bool value)
    {
        isTyping = value;

        if (!isTyping && isWaitingForConfirmation)
        {
            SetAnimationToTrue();
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

    static string ReplacePlaceholders(string text, Dictionary<string, string> replacements)
    {
        return Regex.Replace(text, @"\>(.*?)\<", match =>
        {
            string key = match.Groups[1].Value;
            return replacements.TryGetValue(key, out string replacement) ? replacement : match.Value;
        });
    }
}

    
