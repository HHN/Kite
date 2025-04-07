using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.Novel.VisualNovelFormatter
{
    public class NovelTester
    {
        private Dictionary<string, VisualNovelEvent> _novelEvents;
        private VisualNovelEvent _nextEventToTest;
        private VisualNovel _objectUnderTest;
        private HashSet<CharacterRole> _currentCharacters = new HashSet<CharacterRole>();
        private List<VisualNovelEvent> _choices;
        private HashSet<string> _alreadyPlayedEvents = new HashSet<string>();
        private bool _isOriginalTest = true;
        private int _children;
        private NovelTester _parent;
        private bool _isTestFinished;
        private bool _isTestSuccessful;

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
            _objectUnderTest = novelToTest;

            if (_objectUnderTest == null)
            {
                OnTestFailed("Novel under test is null.", "-", "-");
                return;
            }

            _objectUnderTest.ClearGlobalVariables();
            _objectUnderTest.feedback = string.Empty;
            _objectUnderTest.playedPath = string.Empty;
            _novelEvents = new Dictionary<string, VisualNovelEvent>();
            _currentCharacters = new HashSet<CharacterRole>();
            _choices = new List<VisualNovelEvent>();
            _alreadyPlayedEvents = new HashSet<string>();

            if (string.IsNullOrEmpty(_objectUnderTest.title))
            {
                OnTestFailed("Novel title is null or empty.", "-", "-");
                return;
            }

            if (_objectUnderTest.novelEvents?.Count <= 0)
            {
                OnTestFailed("No novel events found.", _objectUnderTest.title, "-");
                return;
            }

            foreach (VisualNovelEvent novelEvent in _objectUnderTest.novelEvents)
            {
                _novelEvents.Add(novelEvent?.id, novelEvent);
            }

            _nextEventToTest = _objectUnderTest.novelEvents[0];
            PlayNextEvent();
        }

        private void PlayNextEvent()
        {
            if (_nextEventToTest == null)
            {
                OnTestFailed("Event to play is null!", _objectUnderTest.title, "-");
                return;
            }

            VisualNovelEvent eventUnderTest = _nextEventToTest;

            if (string.IsNullOrEmpty(eventUnderTest.id))
            {
                OnTestFailed("Event id is null or empty!", _objectUnderTest.title, "-");
                return;
            }

            if (_alreadyPlayedEvents.Contains(eventUnderTest.id))
            {
                //OnTestFailed("Loop Detected!", objectUnderTest.title, eventUnderTest.id);
                TestEndedSuccessfully();
                return;
            }

            _alreadyPlayedEvents.Add(eventUnderTest.id);

            if (string.IsNullOrEmpty(eventUnderTest.nextId) &&
                VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) !=
                VisualNovelEventType.SHOW_CHOICES_EVENT &&
                VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) != VisualNovelEventType.END_NOVEL_EVENT)
            {
                OnTestFailed("Id of next event is null or empty!", _objectUnderTest.title, eventUnderTest.id);
                return;
            }

            if (!_novelEvents.ContainsKey(eventUnderTest.nextId) &&
                VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) !=
                VisualNovelEventType.SHOW_CHOICES_EVENT &&
                VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) != VisualNovelEventType.END_NOVEL_EVENT)
            {
                OnTestFailed("Next event to play not found!", _objectUnderTest.title, eventUnderTest.id);
                return;
            }

            if (VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) == VisualNovelEventType.ADD_CHOICE_EVENT
                && string.IsNullOrEmpty(eventUnderTest.onChoice))
            {
                OnTestFailed("Add Choice event without onChoice value!", _objectUnderTest.title, eventUnderTest.id);
                return;
            }

            if (VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) ==
                VisualNovelEventType.ADD_CHOICE_EVENT &&
                !_novelEvents.ContainsKey(eventUnderTest.onChoice))
            {
                OnTestFailed("Add Choice event with on choice target that could not be found!", _objectUnderTest.title,
                    eventUnderTest.id);
                return;
            }

            VisualNovelEventType type = VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType);

            if (VisualNovelEventTypeHelper.ValueOf(_nextEventToTest.eventType) !=
                VisualNovelEventType.SHOW_CHOICES_EVENT &&
                VisualNovelEventTypeHelper.ValueOf(_nextEventToTest.eventType) !=
                VisualNovelEventType.END_NOVEL_EVENT)
            {
                string nextEventID = eventUnderTest.nextId;
                _nextEventToTest = _novelEvents[nextEventID];
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
                    OnTestFailed("Event without event type!", _objectUnderTest.title, eventUnderTest.id);
                    return;
                }
            }
        }

        private void HandlePlaySoundEvent(VisualNovelEvent novelEvent)
        {
            if (!ValidateEventField(novelEvent.audioClipToPlay, "Sound Event without audio clip!", novelEvent)) return;
            PlayNextEvent();
        }

        private void HandlePlayAnimationEvent(VisualNovelEvent novelEvent)
        {
            if (!ValidateEventField(novelEvent.animationToPlay, "Animation Event without animation!", novelEvent)) return;
            PlayNextEvent();
        }

        private void HandleFreeTextInputEvent(VisualNovelEvent novelEvent)
        {
            if (!ValidateEventField(novelEvent.questionForFreeTextInput, "Freetext input Event without question!", novelEvent)) return;
            if (!ValidateEventField(novelEvent.variablesName, "Freetext input Event without variable!", novelEvent)) return;
            PlayNextEvent();
        }

        private void HandleGptPromptEvent(VisualNovelEvent novelEvent)
        {
            if (!ValidateEventField(novelEvent.gptPrompt, "GPT prompt event without prompt!", novelEvent)) return;
            if (!ValidateEventField(novelEvent.variablesNameForGptPrompt, "GPT prompt event without variable!", novelEvent)) return;
            PlayNextEvent();
        }

        private void HandleSavePersistentEvent(VisualNovelEvent novelEvent)
        {
            if (!ValidateEventField(novelEvent.key, "Save persistent event without key!", novelEvent)) return;
            if (!ValidateEventField(novelEvent.value, "Save persistent event without value!", novelEvent)) return;
            PlayNextEvent();
        }

        private void HandleMarkBiasEvent(VisualNovelEvent novelEvent)
        {
            if (!ValidateEventField(novelEvent.relevantBias, "Discrimination bias event without discrimination bias!", novelEvent)) return;
            PlayNextEvent();
        }

        private void HandleBackgrundEvent(VisualNovelEvent novelEvent)
        {
            if (!ValidateEventField(novelEvent.backgroundSprite, "Location event without Location!", novelEvent)) return;
            PlayNextEvent();
        }

        private void HandleCharacterJoinEvent(VisualNovelEvent novelEvent)
        {
            CharacterRole role = CharacterTypeHelper.ValueOf(novelEvent.character);
            if (role == CharacterRole.None)
            {
                OnTestFailed("CharacterRole joins event without character!", _objectUnderTest.title, novelEvent.id);
                return;
            }

            _currentCharacters.Add(role);
            PlayNextEvent();
        }

        private void HandleCharacterExitEvent(VisualNovelEvent novelEvent)
        {
            CharacterRole role = CharacterTypeHelper.ValueOf(novelEvent.character);
            
            if (role != CharacterRole.None && role != CharacterRole.Outro && role != CharacterRole.Intro && role != CharacterRole.Info && role != CharacterRole.Player)
            {
                if (!_currentCharacters.Contains(role))
                {
                    OnTestFailed("CharacterRole exit event with character that is not in the scene!", _objectUnderTest.title, novelEvent.id);
                    return;
                }

                _currentCharacters.Remove(role);
            }
            else
            {
                _currentCharacters = new HashSet<CharacterRole>();
            }

            PlayNextEvent();
        }

        private void HandleShowMessageEvent(VisualNovelEvent novelEvent)
        {
            if (string.IsNullOrEmpty(novelEvent.text))
            {
                OnTestFailed("Show message event without message!", _objectUnderTest.title, novelEvent.id);
                return;
            }

            CharacterRole role = CharacterTypeHelper.ValueOf(novelEvent.character);
            if (!_currentCharacters.Contains(role) && role != CharacterRole.Intro && role != CharacterRole.Outro && role != CharacterRole.Info && role != CharacterRole.Player)
            {
                OnTestFailed("Show message event with speaking character that is not in the scene!", _objectUnderTest.title, novelEvent.id);
                return;
            }

            if (CharacterExpressionHelper.ValueOf(novelEvent.expressionType) == CharacterExpression.None)
            {
                OnTestFailed("Show message event without character expression!", _objectUnderTest.title, novelEvent.id);
                return;
            }

            PlayNextEvent();
        }

        private void HandleAddChoiceEvent(VisualNovelEvent novelEvent)
        {
            _choices.Add(novelEvent);

            if (string.IsNullOrEmpty(novelEvent.text))
            {
                OnTestFailed("Add choice event without text!", _objectUnderTest.title, novelEvent.id);
                return;
            }

            PlayNextEvent();
        }

        private void HandleShowChoicesEvent(VisualNovelEvent novelEvent)
        {
            if (_choices == null || _choices.Count == 0)
            {
                OnTestFailed("Show choices event without choices!", _objectUnderTest.title, novelEvent.id);
                return;
            }

            foreach (VisualNovelEvent visualNovelEvent in _choices)
            {
                NovelTester novelTester = DeepCopy();
                novelTester.PerformChoice(_novelEvents[visualNovelEvent.onChoice]);
            }
        }

        private void PerformChoice(VisualNovelEvent visualNovelEvent)
        {
            if (visualNovelEvent == null)
            {
                OnTestFailed("On choice event with target that could not be found!", _objectUnderTest.title, visualNovelEvent.id);
                return;
            }

            _nextEventToTest = visualNovelEvent;
            _choices = new List<VisualNovelEvent>();
            PlayNextEvent();
        }

        private NovelTester DeepCopy()
        {
            NovelTester newCopy = new NovelTester();

            newCopy._novelEvents = new Dictionary<string, VisualNovelEvent>();
            foreach (var entry in _novelEvents)
            {
                newCopy._novelEvents.Add(entry.Key, entry.Value.DeepCopy());
            }

            if (_nextEventToTest != null) newCopy._nextEventToTest = _nextEventToTest.DeepCopy();
            if (_objectUnderTest != null) newCopy._objectUnderTest = _objectUnderTest.DeepCopy();

            newCopy._currentCharacters = new HashSet<CharacterRole>();
            foreach (CharacterRole character in _currentCharacters)
            {
                newCopy._currentCharacters.Add(character);
            }

            newCopy._choices = new List<VisualNovelEvent>();

            foreach (VisualNovelEvent choice in _choices)
            {
                newCopy._choices.Add(choice.DeepCopy());
            }

            newCopy._alreadyPlayedEvents = new HashSet<string>();

            foreach (string alreadyPlayedEvent in _alreadyPlayedEvents)
            {
                newCopy._alreadyPlayedEvents.Add(alreadyPlayedEvent);
            }

            newCopy._parent = this;
            newCopy._isOriginalTest = false;
            _children++;
            return newCopy;
        }

        private void SuccessfullyEndOfTestTriggeredByChildren()
        {
            if (_isTestFinished)
            {
                return;
            }

            _children--;

            if (_children == 0)
            {
                TestEndedSuccessfully();
            }
        }

        private void FailedEndOfTestTriggeredByChildren()
        {
            if (_isTestFinished)
            {
                return;
            }

            _isTestFinished = true;
            TestEndedEarly();
        }

        private void TestEndedSuccessfully()
        {
            _isTestFinished = true;
            _isTestSuccessful = true;
            _parent?.SuccessfullyEndOfTestTriggeredByChildren();
        }

        private void TestEndedEarly()
        {
            if (_isOriginalTest)
            {
                Debug.LogError("Finished Test of Novel with Errors. Novel under Test: " + _objectUnderTest.title + ";");
            }
            else
            {
                _parent?.FailedEndOfTestTriggeredByChildren();
            }
        }
        
        private bool ValidateEventField(string value, string errorMessage, VisualNovelEvent novelEvent)
        {
            if (string.IsNullOrEmpty(value) || value == "NONE")
            {
                OnTestFailed(errorMessage, _objectUnderTest.title, novelEvent.id);
                return false;
            }
            return true;
        }

        private void OnTestFailed(string error, string visualNovelUnderTest, string eventUnderTest)
        {
            _isTestFinished = true;
            TestEndedEarly();
            Debug.LogError("Error while testing novel. Novel under test: " + visualNovelUnderTest + "; Event under test: " + eventUnderTest + "; Error: " + error + ";");
        }

        public bool IsTestOver()
        {
            return _isTestFinished;
        }

        public bool IsTestSuccessful()
        {
            return _isTestSuccessful;
        }
    }
}