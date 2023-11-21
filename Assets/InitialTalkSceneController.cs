using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Febucci.UI.Core;
using System;
using System.Drawing;

public class InitialTalkSceneController : SceneController
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
    public CharacterController currentTalkingCharacterController;
    public GameObject tapToContinueAnimation;
    private bool isTyping;
    private List<string> playThroughHistory = new List<string>();

    [SerializeField] private TextMeshProUGUI scoreText;

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

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.INITIAL_TALK_SCENE);

        // novelToPlay = PlayManager.Instance().GetVisualNovelToPlay();
        Debug.Log("Initialization");
        Initialize();
    }

    private void Update()
    {
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
    }

    public void Initialize()
    {
        PromptManager.Instance().InitializePrompt();

        // if (novelToPlay == null)
        // {
        //     return;
        // }

        // novelToPlay.feedback = string.Empty;

        // novelName.text = novelToPlay.title;

        // if (novelToPlay.novelEvents.Count <= 0)
        // {
        //     return;
        // }
        Debug.Log("add t0 dictonary");
        foreach (VisualNovelEvent novelEvent in ExampleNovel())
        {
            novelEvents.Add(novelEvent.id, novelEvent);
        }
        PlayManager.Instance().SetVisualNovelToPlay(new VisualNovel());
        nextEventToPlay = novelEvents[0];
        StartCoroutine(StartNextEventInOneSeconds(1));
    }

    public void OnConfirm()
    {
        if (!isWaitingForConfirmation)
        {
            return;
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
                    // Debug.Log(GetPlayThroughHistoryAsString());
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
                    // HandleEndNovelEvent(nextEventToPlay);
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

    public void HandleBackgrundEvent(VisualNovelEvent novelEvent)
    {
        long nextEventID = novelEvent.nextId;
        nextEventToPlay = novelEvents[nextEventID];

        if (currentBackground != null)
        {
            Destroy(currentBackground);
        }
        // currentBackground = Instantiate(backgroundPrefab[novelEvent.backgroundSpriteId], backgroundContainer.transform);
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

    public IEnumerator StartNextEventInOneSeconds(int second)
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
        Debug.Log("ShowAnswer" + show);
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

    // private string GetPlayThroughHistoryAsString(){
    //     var returnString = "";
    //     foreach(string entry in playThroughHistory){
    //         returnString += entry + "\n";
    //     }
    //     return returnString;
    // }






    public List<VisualNovelEvent> ExampleNovel()
    {
        Debug.Log("TEST");
        // id = -8;
        // title = "Gründerzuschuss";
        // description = "Du bist heute bei deinem örtlichen Arbeitsamt, um einen Gründerzuschuss zu beantragen.";
        // image = 7;
        // nameOfMainCharacter = "Lea";
        // feedback = "";
        return new List<VisualNovelEvent>()
        {
            new VisualNovelEvent()
            {
                id = 0,
                nextId = 1,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SET_BACKGROUND_EVENT),
                waitForUserConfirmation = false,
                animationType = AnimationTypeHelper.ToInt(AnimationType.FLY_IN_FROM_ABOVE),
                xPosition = 0,
                yPosition = 0,
                backgroundSpriteId = 0
            },

            new VisualNovelEvent()
            {
                id = 1,
                nextId = 2,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SET_BACKGROUND_EVENT),
                waitForUserConfirmation = false,
                animationType = AnimationTypeHelper.ToInt(AnimationType.FLY_IN_FROM_ABOVE),
                xPosition = 0,
                yPosition = 0,
                backgroundSpriteId = 0
            },

            new VisualNovelEvent()
            {
                id = 2,
                nextId = 3,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.CHARAKTER_JOIN_EVENT),
                waitForUserConfirmation = false,
                name = "Herr Müller",
                animationType = AnimationTypeHelper.ToInt(AnimationType.FLY_IN_FROM_ABOVE),
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
                xPosition = 0,
                yPosition = -6,
                skinSpriteId = 0,
                clotheSpriteId = 0,
                hairSpriteId = 0,
                faceSpriteId = 0
            },

            new VisualNovelEvent()
            {
                id = 3,
                nextId = 4,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = false,
                name = "Intro",
                text = "Du bist heute bei deinem örtlichen Arbeitsamt, um einen Gründerzuschuss zu beantragen."
            },

            new VisualNovelEvent()
            {
                id = 4,
                nextId = 20,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Guten Tag. Es freut mich, dass Sie heute vorbeikommen konnten. " +
                "Haben Sie alle Unterlagen dabei?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            // new VisualNovelEvent()
            // {
            //     id = 13,
            //     nextId = 14,
            //     onChoice = 17,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
            //     waitForUserConfirmation = false,
            //     name = "Lea",
            //     text = "Nein, in diesem Bereich habe ich kein Zeugnis."
            // },

            // new VisualNovelEvent()
            // {
            //     id = 14,
            //     nextId = 15,
            //     onChoice = 19,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
            //     waitForUserConfirmation = false,
            //     name = "Lea",
            //     text = "Ich hatte Finanzwesen in der Schule und mein altes Wissen selbst " +
            //     "aufgefrischt. Allerdings habe ich meine Schulzeugnisse nicht dabei."
            // },

            // new VisualNovelEvent()
            // {
            //     id = 15,
            //     nextId = 16,
            //     onChoice = 18,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
            //     waitForUserConfirmation = false,
            //     name = "Lea",
            //     text = "Finanzwesen und Buchhaltung habe ich mir selbst beigebracht, während ich " +
            //     "den Businessplan geschrieben habe und die Bewertung für den Finanzplan ist sehr " +
            //     "gut ausgefallen. Außerdem habe ich vor, baldmöglichst eine Person mit Expertise " +
            //     "in diesem Bereich einzustellen."
            // },

            // new VisualNovelEvent()
            // {
            //     id = 16,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT),
            //     waitForUserConfirmation = true
            // },

            new VisualNovelEvent()
            {
                id = 20,
                nextId = 21,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.CHARAKTER_EXIT_EVENT),
                waitForUserConfirmation = false,
                name = "Herr Müller",
                animationType = AnimationTypeHelper.ToInt(AnimationType.FLY_IN_FROM_ABOVE),
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 21,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.END_NOVEL_EVENT),
                waitForUserConfirmation = false
            }
        };
    }
}

    
