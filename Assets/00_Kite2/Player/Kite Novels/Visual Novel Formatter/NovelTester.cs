using System.Collections.Generic;
using UnityEngine;

public class NovelTester
{
    private Dictionary<string, VisualNovelEvent> novelEvents;
    private VisualNovelEvent nextEventToTest;
    private VisualNovel objectUnderTest;
    private HashSet<Character> currentCharacters = new HashSet<Character>();
    private List<VisualNovelEvent> choices;
    private HashSet<string> alreadyPlayedEvents = new HashSet<string>();
    private bool isOriginalTest = true;
    private int children = 0;
    private NovelTester parent = null;
    private bool isTestFinished = false;
    private bool isTestSuccessfull = false;

    public static List<NovelTester> TestNovels(List<VisualNovel> novels)
    {
        List<NovelTester> tests = new List<NovelTester>();

        if (novels == null || novels.Count == 0)
        {
            Debug.LogWarning("No Novels to test.");
            return tests;
        }

        foreach (VisualNovel novel in novels) 
        {
            NovelTester tester = new NovelTester();
            tester.TestNovel(novel);
            tests.Add(tester);
            
        }
        return tests;
    }

    private void TestNovel(VisualNovel novelToTest)
    {
        objectUnderTest = novelToTest;

        if (objectUnderTest == null)
        {
            OnTestFailed("Novel under test is null.", "-", "-");
            return;
        }
        objectUnderTest.ClearGlobalVariables();
        objectUnderTest.ClearPlayedEvents();
        objectUnderTest.feedback = string.Empty;
        novelEvents = new Dictionary<string, VisualNovelEvent>();
        currentCharacters = new HashSet<Character>();
        choices = new List<VisualNovelEvent>();
        alreadyPlayedEvents = new HashSet<string>();

        if (string.IsNullOrEmpty(objectUnderTest.title))
        {
            OnTestFailed("Novel title is null or empty.", "-", "-");
            return;
        }
        if (objectUnderTest.novelEvents?.Count <= 0)
        {
            OnTestFailed("No novel events found.", objectUnderTest.title, "-");
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
            OnTestFailed("Event to play is null!", objectUnderTest.title, "-");
            return;
        }
        VisualNovelEvent eventUnderTest = nextEventToTest;

        if (string.IsNullOrEmpty(eventUnderTest.id))
        {
            OnTestFailed("Event id is null or empty!", objectUnderTest.title, "-");
            return;
        }
        if (alreadyPlayedEvents.Contains(eventUnderTest.id))
        {
            //OnTestFailed("Loop Detected!", objectUnderTest.title, eventUnderTest.id);
            TestEndedSuccessfully();
            return;
        }
        alreadyPlayedEvents.Add(eventUnderTest.id);

        if (string.IsNullOrEmpty(eventUnderTest.nextId) && 
           (VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) != VisualNovelEventType.SHOW_CHOICES_EVENT) &&
           (VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) != VisualNovelEventType.END_NOVEL_EVENT))
        {
            OnTestFailed("Id of next event is null or empty!", objectUnderTest.title, eventUnderTest.id);
            return;
        }
        if (!novelEvents.ContainsKey(eventUnderTest.nextId) &&
           (VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) != VisualNovelEventType.SHOW_CHOICES_EVENT) &&
           (VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) != VisualNovelEventType.END_NOVEL_EVENT))
        {
            OnTestFailed("Next event to play not found!", objectUnderTest.title, eventUnderTest.id);
            return;
        }
        if ((VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) == VisualNovelEventType.ADD_CHOICE_EVENT) 
            && (string.IsNullOrEmpty(eventUnderTest.onChoice)))
        {
            OnTestFailed("Add Choice event without onChoice value!", objectUnderTest.title, eventUnderTest.id);
            return;
        }
        if ((VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) == VisualNovelEventType.ADD_CHOICE_EVENT) && 
            !novelEvents.ContainsKey(eventUnderTest.onChoice))
        {
            OnTestFailed("Add Choice event with on choice target that could not be found!", objectUnderTest.title, eventUnderTest.id);
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
                    OnTestFailed("Event without event type!", objectUnderTest.title, eventUnderTest.id);
                    return;
                }
        }
    }

    private void HandlePlaySoundEvent(VisualNovelEvent novelEvent)
    {
        if (KiteSoundHelper.ValueOf(novelEvent.audioClipToPlay) == KiteSound.NONE)
        {
            OnTestFailed("Sound Event without audio clip!", objectUnderTest.title, novelEvent.id);
            return;
        }
        PlayNextEvent();
    }

    private void HandlePlayAnimationEvent(VisualNovelEvent novelEvent)
    {
        if (KiteAnimationHelper.ValueOf(novelEvent.animationToPlay) == KiteAnimation.NONE)
        {
            OnTestFailed("Animation Event without animation!", objectUnderTest.title, novelEvent.id);
            return;
        }
        PlayNextEvent();
    }

    private void HandleFreeTextInputEvent(VisualNovelEvent novelEvent)
    {
        if (string.IsNullOrEmpty(novelEvent.questionForFreeTextInput))
        {
            OnTestFailed("Freetext input Event without question!", objectUnderTest.title, novelEvent.id);
            return;
        }
        if (string.IsNullOrEmpty(novelEvent.variablesName))
        {
            OnTestFailed("Freetext input Event without variable!", objectUnderTest.title, novelEvent.id);
            return;
        }
        PlayNextEvent();
    }

    private void HandleGptPromptEvent(VisualNovelEvent novelEvent)
    {
        if (string.IsNullOrEmpty(novelEvent.gptPrompt))
        {
            OnTestFailed("GPT prompt event without prompt!", objectUnderTest.title, novelEvent.id);
            return;
        }
        if (string.IsNullOrEmpty(novelEvent.variablesNameForGptPromp))
        {
            OnTestFailed("GPT prompt event without variable!", objectUnderTest.title, novelEvent.id);
            return;
        }
        PlayNextEvent();
    }

    private void HandleSavePersistentEvent(VisualNovelEvent novelEvent)
    {
        if (string.IsNullOrEmpty(novelEvent.key))
        {
            OnTestFailed("Save persistent event without key!", objectUnderTest.title, novelEvent.id);
            return;
        }
        if (string.IsNullOrEmpty(novelEvent.value))
        {
            OnTestFailed("Save persistent event without value!", objectUnderTest.title, novelEvent.id);
            return;
        }
        PlayNextEvent();
    }

    private void HandleMarkBiasEvent(VisualNovelEvent novelEvent)
    {
        if (DiscriminationBiasHelper.ValueOf(novelEvent.relevantBias) == DiscriminationBias.NONE)
        {
            OnTestFailed("Discrimation bias event without discrimation bias!", objectUnderTest.title, novelEvent.id);
            return;
        }
        PlayNextEvent();
    }

    private void HandleBackgrundEvent(VisualNovelEvent novelEvent)
    {
        if (LocationHelper.ValueOf(novelEvent.backgroundSpriteId) == Location.NONE)
        {
            OnTestFailed("Location event without Location!", objectUnderTest.title, novelEvent.id);
            return;
        }
        PlayNextEvent();
    }

    private void HandleCharacterJoinEvent(VisualNovelEvent novelEvent)
    {
        if (CharacterTypeHelper.ValueOf(novelEvent.character) == Character.NONE)
        {
            OnTestFailed("Character joins event without character!", objectUnderTest.title, novelEvent.id);
            return;
        }
        currentCharacters.Add(CharacterTypeHelper.ValueOf(novelEvent.character));
        PlayNextEvent();
    }

    private void HandleCharacterExitEvent(VisualNovelEvent novelEvent)
    {
        if ((CharacterTypeHelper.ValueOf(novelEvent.character) != Character.NONE) &&
            (CharacterTypeHelper.ValueOf(novelEvent.character) != Character.OUTRO) &&
            (CharacterTypeHelper.ValueOf(novelEvent.character) != Character.INTRO) &&
            (CharacterTypeHelper.ValueOf(novelEvent.character) != Character.INFO) &&
            (CharacterTypeHelper.ValueOf(novelEvent.character) != Character.PLAYER)) 
        {
            if (!currentCharacters.Contains(CharacterTypeHelper.ValueOf(novelEvent.character)))
            {
                OnTestFailed("Character exit event with character that is not in the scene!", objectUnderTest.title, novelEvent.id);
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
            OnTestFailed("Show message event without message!", objectUnderTest.title, novelEvent.id);
            return;
        }
        if (!currentCharacters.Contains(CharacterTypeHelper.ValueOf(novelEvent.character)) &&
            (CharacterTypeHelper.ValueOf(novelEvent.character) != Character.INTRO) &&
            (CharacterTypeHelper.ValueOf(novelEvent.character) != Character.OUTRO) &&
            (CharacterTypeHelper.ValueOf(novelEvent.character) != Character.INFO) &&
            (CharacterTypeHelper.ValueOf(novelEvent.character) != Character.PLAYER))
        {
            OnTestFailed("Show message event with speaking character that is not in the scene!", objectUnderTest.title, novelEvent.id);
            return;
        }
        if (CharacterExpressionHelper.ValueOf(novelEvent.expressionType) == CharacterExpression.NONE)
        {
            OnTestFailed("Show message event without character expression!", objectUnderTest.title, novelEvent.id);
            return;
        }
        PlayNextEvent();
    }

    private void HandleAddChoiceEvent(VisualNovelEvent novelEvent)
    {
        choices.Add(novelEvent);

        if (string.IsNullOrEmpty(novelEvent.text))
        {
            OnTestFailed("Add choice event without text!", objectUnderTest.title, novelEvent.id);
            return;
        }
        PlayNextEvent();
    }

    private void HandleShowChoicesEvent(VisualNovelEvent novelEvent)
    {
        if (choices == null || choices.Count == 0) {
            OnTestFailed("Show choices event without choices!", objectUnderTest.title, novelEvent.id);
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
            OnTestFailed("On choice event with target that could not be found!", objectUnderTest.title, visualNovelEvent.id);
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

        newCopy.alreadyPlayedEvents = new HashSet<string>();

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
        isTestFinished = true;
        isTestSuccessfull = true;
        parent?.SuccessfullyEndOfTestTriggerdByChildren();
    }

    private void TestEndedEarly()
    {
        if (isOriginalTest)
        {
            Debug.LogError("Finished Test of Novel with Errors. Novel under Test: " + objectUnderTest.title + ";");
        }
        else
        {
            parent?.FailedEndOfTestTriggerdByChildren();
        }
    }

    private void OnTestFailed(string error, string visualNovelUnderTest, string eventUnderTest)
    {
        isTestFinished = true;
        TestEndedEarly();
        Debug.LogError("Error while testing novel. Novel under test: " + visualNovelUnderTest + "; Event under test: " + eventUnderTest + "; Error: " + error + ";");
    }

    public bool IsTestOver()
    {
        return isTestFinished;
    }

    public bool IsTestSuccessfull()
    {
        return isTestSuccessfull;
    }
}