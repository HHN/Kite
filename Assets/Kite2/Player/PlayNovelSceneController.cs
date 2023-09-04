using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Febucci.UI.Core;

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
    public FeelingPanelController feelingPanelController;
    public TypewriterCore currentTypeWriter;
    public SelectOptionContinueConversation selectOptionContinueConversation;
    public Button confirmArea;
    public CharacterController currentTalkingCharacterController;
    public GameObject tapToContinueAnimation;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.PLAY_NOVEL_SCENE);

        novelToPlay = PlayManager.Instance().GetVisualNovelToPlay();
        Initialize();
    }

    public void Initialize()
    {
        PromptManager.Instance().InitializePrompt();

        if (novelToPlay == null)
        {
            return;
        }

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
                    HandleEndNovelEvent(nextEventToPlay);
                    break;
                }
            default:
                {
                    break;
                }
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
        currentBackground = Instantiate(backgroundPrefab[novelEvent.backgroundSpriteId], backgroundContainer.transform);

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
        controller.SetFaceSprite(novelEvent.faceSpriteId);

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
            currentTalkingCharacterController.SetFaceSprite(novelEvent.changeFaceToSpriteId);
        }

        conversationContent.AddContent(novelEvent, this);

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
        SceneLoader.LoadFeedbackScene();
    }

    public IEnumerator StartNextEventInOneSeconds(int second)
    {
        yield return new WaitForSeconds(second);
        PlayNextEvent();
    }

    public void ShowAnswer(string message)
    {
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
}
