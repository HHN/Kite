using System.Collections.Generic;
using UnityEngine;

public class NovelTester
{
    private Dictionary<string, VisualNovelEvent> novelEvents;
    private VisualNovelEvent nextEventToTest;
    private VisualNovel objectUnderTest;
    private HashSet<Character> currentCharacters = new HashSet<Character>();
    private List<VisualNovelEvent> choices;
    private List<string> alreadyPlayedEvents = new List<string>();
    private bool isOriginalTest = true;
    private int children = 0;
    private NovelTester parent = null;
    private bool isTestFinished = false;

    public static void TestNovels(List<VisualNovel> novels)
    {
        if (novels == null || novels.Count == 0)
        {
            Debug.LogWarning("No Novels to test.");
            return;
        }
        foreach (VisualNovel novel in novels) 
        { 
            new NovelTester().TestNovel(novel);
        }
    }

    private void TestNovel(VisualNovel novelToTest)
    {
        objectUnderTest = novelToTest;

        if (objectUnderTest == null)
        {
            Debug.LogWarning("Novel under test is null.");
            TestEndedEarly();
            return;
        }
        objectUnderTest.ClearGlobalVariables();
        objectUnderTest.feedback = string.Empty;
        novelEvents = new Dictionary<string, VisualNovelEvent>();
        currentCharacters = new HashSet<Character>();
        choices = new List<VisualNovelEvent>();
        alreadyPlayedEvents = new List<string>();

        if (string.IsNullOrEmpty(objectUnderTest.title))
        {
            Debug.LogWarning("Novel under test (" + objectUnderTest.title + "): Title is null or empty.");
        }
        if (objectUnderTest.novelEvents?.Count <= 0)
        {
            Debug.LogWarning("Novel under test (" + objectUnderTest.title + "): No novel-events found.");
            TestEndedEarly();
            return;
        }
        foreach (VisualNovelEvent novelEvent in objectUnderTest.novelEvents)
        {
            novelEvents.Add(novelEvent?.id, novelEvent);
        }
        nextEventToTest = objectUnderTest.novelEvents[0];
        PlayNextEvent();
    }

    private void PlayNextEvent()
    {
        if (nextEventToTest == null)
        {
            Debug.LogWarning("Novel under test (" + objectUnderTest.title + "): Events are not correctly connected. Event to Play is null!");
            TestEndedEarly();
            return;
        }
        VisualNovelEvent eventUnderTest = nextEventToTest;

        if (string.IsNullOrEmpty(eventUnderTest.id))
        {
            Debug.LogWarning("Novel under test (" + objectUnderTest.title + "): Event to play has an empty id! Event ID: " + eventUnderTest.id);
            TestEndedEarly();
            return;
        }
        if (alreadyPlayedEvents.Contains(eventUnderTest.id))
        {
            Debug.LogWarning("Novel under test (" + objectUnderTest.title + "): Loop Detected! Event ID: " + eventUnderTest.id);
            TestEndedEarly();
            return;
        }
        alreadyPlayedEvents.Add(eventUnderTest.id);

        if (string.IsNullOrEmpty(eventUnderTest.nextId) && 
           (VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) != VisualNovelEventType.SHOW_CHOICES_EVENT) &&
           (VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) != VisualNovelEventType.END_NOVEL_EVENT))
        {
            Debug.LogWarning("Novel under test (" + objectUnderTest.title + "): Next event to play has an empty id! Event ID: " + eventUnderTest.id);
            TestEndedEarly();
            return;
        }
        if (!novelEvents.ContainsKey(eventUnderTest.nextId) &&
           (VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) != VisualNovelEventType.SHOW_CHOICES_EVENT) &&
           (VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) != VisualNovelEventType.END_NOVEL_EVENT))
        {
            Debug.LogWarning("Novel under test (" + objectUnderTest.title + "): Next event to play not found! Event ID: " + eventUnderTest.id);
            TestEndedEarly();
            return;
        }
        if ((VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) == VisualNovelEventType.ADD_CHOICE_EVENT) 
            && (string.IsNullOrEmpty(eventUnderTest.onChoice)))
        {
            Debug.LogWarning("Novel under test (" + objectUnderTest.title + "): On Choice Event has no OnChoice! Event ID: " + eventUnderTest.id);
            TestEndedEarly();
            return;
        }
        if ((VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) == VisualNovelEventType.ADD_CHOICE_EVENT) && 
            !novelEvents.ContainsKey(eventUnderTest.onChoice))
        {
            Debug.LogWarning("Novel under test (" + objectUnderTest.title + "): On Choice target not found! Event ID: " + eventUnderTest.id);
            TestEndedEarly();
            return;
        }
        VisualNovelEventType type = VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType);

        if ((VisualNovelEventTypeHelper.ValueOf(nextEventToTest.eventType) != VisualNovelEventType.SHOW_CHOICES_EVENT) &&
    (VisualNovelEventTypeHelper.ValueOf(nextEventToTest.eventType) != VisualNovelEventType.END_NOVEL_EVENT))
        {
            string nextEventID = eventUnderTest.nextId;
            nextEventToTest = novelEvents[nextEventID];
        }

        switch (type)
        {
            case VisualNovelEventType.SET_BACKGROUND_EVENT:
                {
                    HandleBackgrundEvent(eventUnderTest);
                    break;
                }
            case VisualNovelEventType.CHARAKTER_JOIN_EVENT:
                {
                    HandleCharacterJoinEvent(eventUnderTest);
                    break;
                }
            case VisualNovelEventType.CHARAKTER_EXIT_EVENT:
                {
                    HandleCharacterExitEvent(eventUnderTest);
                    break;
                }
            case VisualNovelEventType.SHOW_MESSAGE_EVENT:
                {
                    HandleShowMessageEvent(eventUnderTest);
                    break;
                }
            case VisualNovelEventType.ADD_CHOICE_EVENT:
                {
                    HandleAddChoiceEvent(eventUnderTest);
                    break;
                }
            case VisualNovelEventType.SHOW_CHOICES_EVENT:
                {
                    HandleShowChoicesEvent(eventUnderTest);
                    break;
                }
            case VisualNovelEventType.END_NOVEL_EVENT:
                {
                    TestEndedSuccessfully(); 
                    break;
                }
            case VisualNovelEventType.PLAY_SOUND_EVENT:
                {
                    HandlePlaySoundEvent(eventUnderTest);
                    break;
                }
            case VisualNovelEventType.PLAY_ANIMATION_EVENT:
                {
                    HandlePlayAnimationEvent(eventUnderTest);
                    break;
                }
            case VisualNovelEventType.FREE_TEXT_INPUT_EVENT:
                {
                    HandleFreeTextInputEvent(eventUnderTest);
                    break;
                }
            case VisualNovelEventType.GPT_PROMPT_EVENT:
                {
                    HandleGptPromptEvent(eventUnderTest);
                    break;
                }
            case VisualNovelEventType.SAVE_PERSISTENT_EVENT:
                {
                    HandleSavePersistentEvent(eventUnderTest);
                    break;
                }
            case VisualNovelEventType.MARK_BIAS_EVENT:
                {
                    HandleMarkBiasEvent(eventUnderTest);
                    break;
                }
            default:
                {
                    Debug.LogWarning("Novel under test (" + objectUnderTest.title + "): Event without event type found!");
                    TestEndedEarly();
                    return;
                }
        }
    }

    private void HandlePlaySoundEvent(VisualNovelEvent novelEvent)
    {
        if (KiteSoundHelper.ValueOf(novelEvent.audioClipToPlay) == KiteSound.NONE)
        {
            Debug.LogWarning("Novel under test (" + objectUnderTest.title + "): Sound event without valid audio clip! Event ID: " + novelEvent.id);
            TestEndedEarly();
            return;
        }
        PlayNextEvent();
    }

    private void HandlePlayAnimationEvent(VisualNovelEvent novelEvent)
    {
        if (KiteAnimationHelper.ValueOf(novelEvent.animationToPlay) == KiteAnimation.NONE)
        {
            Debug.LogWarning("Novel under test (" + objectUnderTest.title + "): Animation event without valid animation! Event ID: " + novelEvent.id);
            TestEndedEarly();
            return;
        }
        PlayNextEvent();
    }

    private void HandleFreeTextInputEvent(VisualNovelEvent novelEvent)
    {
        if (string.IsNullOrEmpty(novelEvent.questionForFreeTextInput))
        {
            Debug.LogWarning("Novel under test (" + objectUnderTest.title + "): Question for free text input is null or empty! Event ID: " + novelEvent.id);
            TestEndedEarly();
            return;
        }
        if (string.IsNullOrEmpty(novelEvent.variablesName))
        {
            Debug.LogWarning("Novel under test (" + objectUnderTest.title + "): Variables name for free text input is null or empty! Event ID: " + novelEvent.id);
            TestEndedEarly();
            return;
        }
        PlayNextEvent();
    }

    private void HandleGptPromptEvent(VisualNovelEvent novelEvent)
    {
        if (string.IsNullOrEmpty(novelEvent.gptPrompt))
        {
            Debug.LogWarning("Novel under test (" + objectUnderTest.title + "): Prompt for LLM is null or empty! Event ID: " + novelEvent.id);
            TestEndedEarly();
            return;
        }
        if (string.IsNullOrEmpty(novelEvent.variablesNameForGptPromp))
        {
            Debug.LogWarning("Novel under test (" + objectUnderTest.title + "): Variables name for GPT Prompt is null or empty! Event ID: " + novelEvent.id);
            TestEndedEarly();
            return;
        }
        PlayNextEvent();
    }

    private void HandleSavePersistentEvent(VisualNovelEvent novelEvent)
    {
        if (string.IsNullOrEmpty(novelEvent.key))
        {
            Debug.LogWarning("Novel under test (" + objectUnderTest.title + "): Save Persistent - Key is null or empty! Event ID: " + novelEvent.id);
            TestEndedEarly();
            return;
        }
        if (string.IsNullOrEmpty(novelEvent.value))
        {
            Debug.LogWarning("Novel under test (" + objectUnderTest.title + "): Save Persistent - Value is null or empty! Event ID: " + novelEvent.id);
            TestEndedEarly();
            return;
        }
        PlayNextEvent();
    }

    private void HandleMarkBiasEvent(VisualNovelEvent novelEvent)
    {
        if (DiscriminationBiasHelper.ValueOf(novelEvent.relevantBias) == DiscriminationBias.NONE)
        {
            Debug.LogWarning("Novel under test (" + objectUnderTest.title + "): Discrimination Bias Event withut Discrimination Bias! Event ID: " + novelEvent.id);
            TestEndedEarly();
            return;
        }
        PlayNextEvent();
    }

    private void HandleBackgrundEvent(VisualNovelEvent novelEvent)
    {
        if (LocationHelper.ValueOf(novelEvent.backgroundSpriteId) == Location.NONE)
        {
            Debug.LogWarning("Novel under test (" + objectUnderTest.title + "): Location Event without location! Event ID: " + novelEvent.id);
            TestEndedEarly();
            return;
        }
        PlayNextEvent();
    }

    private void HandleCharacterJoinEvent(VisualNovelEvent novelEvent)
    {
        if (CharacterTypeHelper.ValueOf(novelEvent.character) == Character.NONE)
        {
            Debug.LogWarning("Novel under test (" + objectUnderTest.title + "): Character Joins Event without Character! Event ID: " + novelEvent.id);
            TestEndedEarly();
            return;
        }
        currentCharacters.Add(CharacterTypeHelper.ValueOf(novelEvent.character));
        PlayNextEvent();
    }

    private void HandleCharacterExitEvent(VisualNovelEvent novelEvent)
    {
        if (CharacterTypeHelper.ValueOf(novelEvent.character) != Character.NONE)
        {
            if (!currentCharacters.Contains(CharacterTypeHelper.ValueOf(novelEvent.character)))
            {
                Debug.LogWarning("Novel under test (" + objectUnderTest.title + "): Character Exit Event with Character that is not in the scene! Event ID: " + novelEvent.id);
                TestEndedEarly();
                return;
            } 
            else
            {
                currentCharacters.Remove(CharacterTypeHelper.ValueOf(novelEvent.character));
            }
        } 
        else
        {
            currentCharacters = new HashSet<Character>();
        }
        PlayNextEvent();
    }

    private void HandleShowMessageEvent(VisualNovelEvent novelEvent)
    {
        if (string.IsNullOrEmpty(novelEvent.text))
        {
            Debug.LogWarning("Novel under test (" + objectUnderTest.title + "): Show Message Event - message is null or empty! Event ID: " + novelEvent.id);
            TestEndedEarly();
            return;
        }
        if (!currentCharacters.Contains(CharacterTypeHelper.ValueOf(novelEvent.character)) &&
            (CharacterTypeHelper.ValueOf(novelEvent.character) != Character.INTRO) &&
            (CharacterTypeHelper.ValueOf(novelEvent.character) != Character.OUTRO) &&
            (CharacterTypeHelper.ValueOf(novelEvent.character) != Character.INFO) &&
            (CharacterTypeHelper.ValueOf(novelEvent.character) != Character.PLAYER))
        {
            Debug.LogWarning("Novel under test (" + objectUnderTest.title + "): Show Message Event - Speaking character is not in the scene! Event ID: " + novelEvent.id);
            TestEndedEarly();
            return;
        }
        if (CharacterExpressionHelper.ValueOf(novelEvent.expressionType) == CharacterExpression.NONE)
        {
            Debug.LogWarning("Novel under test (" + objectUnderTest.title + "): Show Message Event - without valid character-expression! Event ID: " + novelEvent.id);
            TestEndedEarly();
            return;
        }
        PlayNextEvent();
    }

    private void HandleAddChoiceEvent(VisualNovelEvent novelEvent)
    {
        choices.Add(novelEvent);

        if (string.IsNullOrEmpty(novelEvent.text))
        {
            Debug.LogWarning("Novel under test (" + objectUnderTest.title + "): Add Choice - message is null or empty! Event ID: " + novelEvent.id);
            TestEndedEarly();
            return;
        }
        PlayNextEvent();
    }

    private void HandleShowChoicesEvent(VisualNovelEvent novelEvent)
    {
        if (choices == null || choices.Count == 0) {
            Debug.LogWarning("Novel under test (" + objectUnderTest.title + "): Show choices event -  without choices! Event ID: " + novelEvent.id);
            TestEndedEarly();
            return;
        }
        foreach (VisualNovelEvent visualNovelEvent in choices)
        {
            NovelTester novelTester = this.DeepCopy();
            novelTester.PerformChoice(novelEvents[visualNovelEvent.onChoice]);
        }
    }

    private void PerformChoice(VisualNovelEvent visualNovelEvent)
    {
        if (visualNovelEvent == null)
        {
            Debug.LogWarning("Novel under test (" + objectUnderTest.title + "): Show choices event -  Choice not found! Event ID: " + nextEventToTest.id);
            TestEndedEarly();
            return;
        }
        nextEventToTest = visualNovelEvent;
        choices = new List<VisualNovelEvent>();
        PlayNextEvent();
    }

    private NovelTester DeepCopy()
    {
        NovelTester newCopy = new NovelTester();

        newCopy.novelEvents = new Dictionary<string, VisualNovelEvent>();
        foreach (var entry in this.novelEvents)
        {
            newCopy.novelEvents.Add(entry.Key, entry.Value.DeepCopy());
        }

        if (this.nextEventToTest != null)
            newCopy.nextEventToTest = this.nextEventToTest.DeepCopy();

        if (this.objectUnderTest != null)
            newCopy.objectUnderTest = this.objectUnderTest.DeepCopy(); 

        newCopy.currentCharacters = new HashSet<Character>();
        foreach (Character character in this.currentCharacters)
        {
            newCopy.currentCharacters.Add(character);
        }

        newCopy.choices = new List<VisualNovelEvent>();

        foreach (VisualNovelEvent choice in this.choices)
        {
            newCopy.choices.Add(choice.DeepCopy());
        }

        newCopy.alreadyPlayedEvents = new List<string>();

        foreach (string alreadyPlayedEvent in this.alreadyPlayedEvents)
        {
            newCopy.alreadyPlayedEvents.Add(alreadyPlayedEvent);
        }
        newCopy.parent = this;
        newCopy.isOriginalTest = false;
        children++;
        return newCopy;
    }

    private void SuccessfullyEndOfTestTriggerdByChildren()
    {
        if (isTestFinished)
        {
            return;
        }
        children--;

        if (children == 0)
        {
            TestEndedSuccessfully();
            isTestFinished = true;
        }
    }

    private void FailedEndOfTestTriggerdByChildren()
    {
        if (isTestFinished)
        {
            return;
        }
        isTestFinished = true;
        TestEndedEarly();
    }

    private void TestEndedSuccessfully()
    {
        if (isOriginalTest)
        {
            Debug.Log("Successfully ended test for Novel: " + objectUnderTest.title);
        } else
        {
            if (parent == null)
            {
                Debug.LogWarning("Unexprected Error while Testing Novel: Parent is null where it should not be null. ");
            }
            else
            {
                parent.SuccessfullyEndOfTestTriggerdByChildren();
            }
        }
    }

    private void TestEndedEarly()
    {
        if (isOriginalTest)
        {
            Debug.LogWarning("Ended Test early!");
        }
        else
        {
            parent.FailedEndOfTestTriggerdByChildren();
        }
    }
}
