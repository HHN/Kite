using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Febucci.UI.Core;
using System;
using System.Drawing;
using System.Text.RegularExpressions;

public class PlayNovelSceneController : SceneController
{
    private VisualNovel novelToPlay;
    public Slider progressbar;
    public TextMeshProUGUI novelName;
    public ConversationContentGuiController conversationContent;
    public GameObject novelImageContainer;
    public GameObject novelBackgroundPrefab;
    public GameObject characterPrefab;
    private bool isWaitingForConfirmation = false;
    private Dictionary<long, VisualNovelEvent> novelEvents = new Dictionary<long, VisualNovelEvent>();
    private VisualNovelEvent nextEventToPlay;
    public GameObject backgroundContainer;
    public GameObject[] backgroundPrefab;
    private GameObject currentBackground;
    public GameObject characterContainer;
    private Dictionary<string, GameObject> currentCharacters = new Dictionary<string, GameObject>();
    public ChatScrollView chatScroll;
    [SerializeField] private ImageScrollView imageScroll;
    public FeelingPanelController feelingPanelController;
    public TypewriterCore currentTypeWriter;
    public SelectOptionContinueConversation selectOptionContinueConversation;
    public Button confirmArea;
    public Button confirmArea2;
    public CharacterController currentTalkingCharacterController;
    public GameObject tapToContinueAnimation;
    private bool isTyping;
    private List<string> playThroughHistory = new List<string>();

    [SerializeField] private TextMeshProUGUI scoreText;
    //[SerializeField] private TextMeshProUGUI moneyText;

    [SerializeField] private GameObject addScoreServerCallPrefab;
    [SerializeField] private GameObject addMoneyServerCallPrefab;
    [SerializeField] private GameObject gptServercallPrefab;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] clips;

    [SerializeField] private GameObject[] novelAnimations;
    [SerializeField] private GameObject viewPortOfImages;
    private GameObject currentAnimation;

    [SerializeField] private GameObject backgroundBlur;
    [SerializeField] private GameObject imageAreaBlur;
    [SerializeField] private GameObject screenContentBlur;

    [SerializeField] private GameObject backgroundColor;
    [SerializeField] private GameObject imageAreaColor;
    [SerializeField] private GameObject screenContentColor;

    [SerializeField] private GameObject freeTextInputPrefab;

    // Analytics
    private bool firstUserConfirmation = true;

    void Start()
    {
        AnalyticsServiceHandler.Instance().StartStopwatch();
        BackStackManager.Instance().Push(SceneNames.PLAY_NOVEL_SCENE);

        novelToPlay = PlayManager.Instance().GetVisualNovelToPlay();
        Initialize();
        InitializeMoneyAndScore();
    }

    private void Update()
    {
        /**
        if (Input.GetKeyDown(KeyCode.A))
        {
            SetScreenContentColor(true, new UnityEngine.Color(0, 0, 0, 1));
        } 
        else if (Input.GetKeyDown(KeyCode.S))
        {
            SetScreenContentColor(false, new UnityEngine.Color(0, 0, 0, 1));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            SetImageAreaColor(true, new UnityEngine.Color(0, 0, 0, 1));
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            SetImageAreaColor(false, new UnityEngine.Color(0, 0, 0, 1));
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            SetBackgroundColor(true, new UnityEngine.Color(0, 0, 0, 1));
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            SetBackgroundColor(false, new UnityEngine.Color(0, 0, 0, 1));
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            SetScreenContentBlur(true);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            SetScreenContentBlur(false);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            SetImageAreaBlur(true);
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            SetImageAreaBlur(false);
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            SetBackGroundBlur(true);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            SetBackGroundBlur(false);
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            SetCharacterBrither(true, "Herr Mayer");
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            SetCharacterBrither(false, "Herr Mayer");
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            SetCharacterBlur(true, "Herr Mayer");
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            SetCharacterBlur(false, "Herr Mayer");
        }
        */
    }

    public void InitializeMoneyAndScore()
    {
        long money = MoneyManager.Instance().GetMoney();
        long score = ScoreManager.Instance().GetScore();

       // moneyText.text = money.ToString();
        scoreText.text = score.ToString();
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
                    Debug.Log(GetPlayThroughHistoryAsString());
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
            case VisualNovelEventType.ADD_OPINION_CHOICE_EVENT:
                {
                    HandleAddOpinionChoiceEvent(nextEventToPlay);
                    break;
                }
            case VisualNovelEventType.ASK_FOR_OPINION_EVENT:
                {
                    confirmArea.gameObject.SetActive(false);
                    confirmArea2.gameObject.SetActive(false);
                    HandleAskForOpinionEvent(nextEventToPlay);
                    ScrollToBottom();
                    break;
                }
            case VisualNovelEventType.SHOW_OPINION_FEEDBACK_EVENT:
                {
                    HandleOpinionFeedbackEvent(nextEventToPlay);
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
            default:
                {
                    break;
                }
        }
    }

    private void HandlePlaySoundEvent(VisualNovelEvent novelEvent)
    {
        long nextEventID = novelEvent.nextId;
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
        if (novelEvent.audioClipToPlay == SoundEnumHelper.ToInt(SoundsEnum.LEAVE_SCENE))
        {
            StartCoroutine(StartNextEventInOneSeconds(2.5f));
            return;
        }
        StartCoroutine(StartNextEventInOneSeconds(1));
    }

    private void HandlePlayAnimationEvent(VisualNovelEvent novelEvent)
    {
        long nextEventID = novelEvent.nextId;
        nextEventToPlay = novelEvents[nextEventID];

        if (novelEvent.animationToPlay != 0)
        {
            currentAnimation = Instantiate(novelAnimations[novelEvent.animationToPlay], viewPortOfImages.transform);
        }
    }

    private void HandleFreeTextInputEvent(VisualNovelEvent novelEvent)
    {
        long nextEventID = novelEvent.nextId;
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
        long nextEventID = novelEvent.nextId;
        nextEventToPlay = novelEvents[nextEventID];

        if (novelEvent.gptPrompt == String.Empty 
            || novelEvent.gptPrompt == "" 
            || novelEvent.variablesNameForGptPromp == String.Empty 
            || novelEvent.variablesNameForGptPromp == "")
        {
            return;
        }
        GetCompletionServerCall call = Instantiate(gptServercallPrefab).GetComponent<GetCompletionServerCall>();
        call.sceneController = this;
        GptRequestEventOnSuccessHandler onSuccessHandler = new GptRequestEventOnSuccessHandler();
        onSuccessHandler.variablesNameForGptPromp = novelEvent.variablesNameForGptPromp;
        onSuccessHandler.completionHandler = GptCompletionHandlerManager.Instance().GetCompletionHandlerById(novelEvent.id);
        call.onSuccessHandler = onSuccessHandler;
        call.prompt = ReplacePlaceholders(novelEvent.gptPrompt, novelToPlay.GetGlobalVariables());
        call.SendRequest();
        DontDestroyOnLoad(call.gameObject);

        PlayNextEvent();
    }

    public void HandleBackgrundEvent(VisualNovelEvent novelEvent)
    {
        long nextEventID = novelEvent.nextId;
        nextEventToPlay = novelEvents[nextEventID];

        if (currentBackground != null)
        {
            Destroy(currentBackground);
        }
        currentBackground = Instantiate(backgroundPrefab[novelEvent.backgroundSpriteId], backgroundContainer.transform);
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
        long nextEventID = novelEvent.nextId;
        nextEventToPlay = novelEvents[nextEventID];

        GameObject character = Instantiate(characterPrefab, characterContainer.transform);
        CharacterController controller = character.GetComponent<CharacterController>();
        controller.SetSkinSprite(novelEvent.skinSpriteId);
        controller.SetClotheSprite(novelEvent.clotheSpriteId);
        controller.SetHairSprite(novelEvent.hairSpriteId);
        controller.SetFaceExpression(novelEvent.faceSpriteId);

        currentCharacters.Add(novelEvent.name, character);

        if (novelEvent.waitForUserConfirmation)
        {
            SetWaitingForConfirmation(true);
            return;
        }
        StartCoroutine(StartNextEventInOneSeconds(1));
    }

    public void HandleCharacterExitEvent(VisualNovelEvent novelEvent)
    {
        long nextEventID = novelEvent.nextId;
        nextEventToPlay = novelEvents[nextEventID];

        GameObject character = currentCharacters[novelEvent.name];
        currentCharacters.Remove(novelEvent.name);
        Destroy(character);

        if (novelEvent.waitForUserConfirmation)
        {
            SetWaitingForConfirmation(true);
            return;
        }
        StartCoroutine(StartNextEventInOneSeconds(1));
    }

    public void HandleShowMessageEvent(VisualNovelEvent novelEvent)
    {
        novelEvent.text = ReplacePlaceholders(novelEvent.text, novelToPlay.GetGlobalVariables());

        long nextEventID = novelEvent.nextId;
        nextEventToPlay = novelEvents[nextEventID];

        if (currentCharacters.ContainsKey(novelEvent.name))
        {
            GameObject character = currentCharacters[novelEvent.name];
            currentTalkingCharacterController = character.GetComponent<CharacterController>();
            currentTalkingCharacterController.SetFaceExpression(novelEvent.expressionType);
        }

        if(novelEvent.show){
            conversationContent.AddContent(novelEvent, this);

            AddEntryToPlayThroughHistory(novelEvent.name, novelEvent.text);
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
        long nextEventID = novelEvent.nextId;
        nextEventToPlay = novelEvents[nextEventID];

        conversationContent.AddContent(novelEvent, this);

        if (novelEvent.waitForUserConfirmation)
        {
            SetWaitingForConfirmation(true);
            return;
        }
        AnalyticsServiceHandler.Instance().AddChoiceToList(novelEvent.text);
        PlayNextEvent();
    }

    public void HandleShowChoicesEvent(VisualNovelEvent novelEvent)
    { 
        AddEntryToPlayThroughHistory(novelEvent.name, novelEvent.text);
        conversationContent.AddContent(novelEvent, this);
    }

    public void HandleAddOpinionChoiceEvent(VisualNovelEvent novelEvent)
    {
        long nextEventID = novelEvent.nextId;
        nextEventToPlay = novelEvents[nextEventID];

        switch (novelEvent.opinionChoiceNumber)
        {
            case 1:
                {
                    feelingPanelController.SetNervousFeedback(novelEvent.text);
                    break;
                }
            case 2:
                {
                    feelingPanelController.SetFearfullFeedback(novelEvent.text);
                    break;
                }
            case 3:
                {
                    feelingPanelController.SetEncouragedFeedback(novelEvent.text);
                    break;
                }
            case 4:
                {
                    feelingPanelController.SetAnnoyedFeedback(novelEvent.text);
                    break;
                }
            default:
                {
                    break;
                }
        }
        PlayNextEvent();
    }

    public void HandleAskForOpinionEvent(VisualNovelEvent novelEvent)
    {
        long nextEventID = novelEvent.nextId;
        nextEventToPlay = novelEvents[nextEventID];

        this.feelingPanelController.Initialize();
        conversationContent.AddContent(novelEvent, this);
    }

    public void HandleOpinionFeedbackEvent(VisualNovelEvent novelEvent)
    {
        long nextEventID = novelEvent.nextId;
        nextEventToPlay = novelEvents[nextEventID];

        feelingPanelController.CleanUp();
        PlayNextEvent();
    }

    public void HandleEndNovelEvent(VisualNovelEvent novelEvent)
    {
        AnalyticsServiceHandler.Instance().SendNovelPlayTime();
        AddScore(5);
        AddMoney(5);

        int userRole = FeedbackRoleManager.Instance.GetFeedbackRole();
        if (userRole == 1 || userRole == 3 || userRole == 4 || userRole == 5)
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
        AddEntryToPlayThroughHistory("Lea", message);
        conversationContent.ShowPlayerAnswer(message);
        ScrollToBottom();
    }

    public void SetNextEvent(long id)
    {
        nextEventToPlay = novelEvents[id];
    }

    public void ScrollToBottom()
    {
        StartCoroutine(chatScroll.ScrollToBottom());
    }

    public void SetFeelingsPanelActive(bool active)
    {
        feelingPanelController.gameObject.SetActive(active);
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
        tapToContinueAnimation.SetActive(value);
        tapToContinueAnimation.GetComponent<Animator>().enabled = value;
    }

    public void SetTyping(bool value)
    {
        isTyping = value;

        if (!isTyping && isWaitingForConfirmation)
        {
            tapToContinueAnimation.SetActive(true);
            tapToContinueAnimation.GetComponent<Animator>().enabled = true;
            return;
        }
        tapToContinueAnimation.SetActive(false);
        tapToContinueAnimation.GetComponent<Animator>().enabled = false;
    }

    public void AddScore(long value)
    {
        scoreText.text = (ScoreManager.Instance().GetScore() + value).ToString();

        if (GameManager.Instance().applicationMode != ApplicationModes.LOGGED_IN_USER_MODE || value == 0)
        {
            return;
        }
        AddScoreServerCall call = Instantiate(this.addScoreServerCallPrefab).GetComponent<AddScoreServerCall>();
        call.sceneController = this;
        call.onSuccessHandler = UpdateScoreAndMoneyManager.Instance();
        call.value = value;
        call.SendRequest();
        DontDestroyOnLoad(call.gameObject);
    }

    public void AddMoney(long value)
    {
        //moneyText.text = (MoneyManager.Instance().GetMoney() + value).ToString();

        if (GameManager.Instance().applicationMode != ApplicationModes.LOGGED_IN_USER_MODE || value == 0)
        {
            return;
        }
        AddMoneyServerCall call = Instantiate(this.addMoneyServerCallPrefab).GetComponent<AddMoneyServerCall>();
        call.sceneController = this;
        call.onSuccessHandler = UpdateScoreAndMoneyManager.Instance();
        call.value = value;
        call.SendRequest();
        DontDestroyOnLoad(call.gameObject);
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

    public void SetBackGroundBlur(bool value)
    {
        if (backgroundBlur == null)
        {
            return;
        }
        backgroundBlur.SetActive(value);
    }

    public void SetImageAreaBlur(bool value)
    {
        if (backgroundBlur == null || imageAreaBlur == null)
        {
            return;
        }
        backgroundBlur.SetActive(false);
        imageAreaBlur.SetActive(value);
    }

    public void SetScreenContentBlur(bool value)
    {
        if (backgroundBlur == null || imageAreaBlur == null || screenContentBlur == null)
        {
            return;
        }
        backgroundBlur.SetActive(false);
        imageAreaBlur.SetActive(false);
        screenContentBlur.SetActive(value);
    }

    public void SetBackgroundColor(bool value, UnityEngine.Color color)
    {
        if (backgroundColor == null)
        {
            return;
        }
        backgroundColor.SetActive(value);
        color.a = (2f / 3f);
        backgroundColor.GetComponent<Image>().color = color;
    }

    public void SetImageAreaColor(bool value, UnityEngine.Color color)
    {
        if (backgroundColor == null || imageAreaColor == null)
        {
            return;
        }
        backgroundColor.SetActive(false);
        imageAreaColor.SetActive(value);
        color.a = (2f / 3f);
        imageAreaColor.GetComponent<Image>().color = color;
    }

    public void SetScreenContentColor(bool value, UnityEngine.Color color)
    {
        if (backgroundColor == null || imageAreaColor == null || screenContentColor == null)
        {
            return;
        }
        backgroundColor.SetActive(false);
        imageAreaColor.SetActive(false);
        screenContentColor.SetActive(value);
        color.a = (2f / 3f);
        screenContentColor.GetComponent<Image>().color = color;
    }

    public void SetCharacterBrither(bool value, string characterName)
    {
        if (!currentCharacters.ContainsKey(characterName))
        {
            return;
        }

        ShaderToggle shaderToogle = currentCharacters[characterName].GetComponent<ShaderToggle>();

        if (shaderToogle == null)
        {
            return;
        }

        shaderToogle.SetCharacterBrightness(value);
    }

    public void SetCharacterBlur(bool value, string characterName)
    {
        if (!currentCharacters.ContainsKey(characterName))
        {
            return;
        }

        ShaderToggle shaderToogle = currentCharacters[characterName].GetComponent<ShaderToggle>();

        if (shaderToogle == null)
        {
            return;
        }

        shaderToogle.SetCharacterBlur(value);
    }

    private void AddEntryToPlayThroughHistory(string name, string text){
        playThroughHistory.Add(name + ": " + text);
    }

    private string GetPlayThroughHistoryAsString(){
        var returnString = "";
        foreach(string entry in playThroughHistory){
            returnString += entry + "\n";
        }
        return returnString;
    }

    static string ReplacePlaceholders(string text, Dictionary<string, string> replacements)
    {
        return Regex.Replace(text, @"\[(.*?)\]", match =>
        {
            string key = match.Groups[1].Value;
            return replacements.TryGetValue(key, out string replacement) ? replacement : match.Value;
        });
    }
}

    
