using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.Novel.VisualNovelFormatter
{
    /// <summary>
    /// The NovelTester class is responsible for testing instances of the VisualNovel class
    /// by simulating their execution and verifying various aspects of their behavior.
    /// </summary>
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

        /// <summary>
        /// Tests a list of VisualNovel instances by simulating their execution and verifying their behavior.
        /// </summary>
        /// <param name="novels">A list of VisualNovel instances to be tested.</param>
        /// <returns>A list of NovelTester instances containing the results of the executed tests.</returns>
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

        /// <summary>
        /// Tests a single instance of the VisualNovel class by simulating its execution
        /// and verifying its behavior, ensuring that all required properties and events are properly initialized and functional.
        /// </summary>
        /// <param name="novelToTest">The VisualNovel instance to be tested.</param>
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

        /// <summary>
        /// Executes the next event in the visual novel sequence, determining its type and processing logic accordingly.
        /// </summary>
        /// <remarks>
        /// The method validates the current event's integrity, checks for duplicates, and ensures all required data is available.
        /// It handles specific event types such as character interactions, choices, animations, sound effects, and end-of-novel events.
        /// Invalid states are identified, and the flow of events is managed to proceed correctly.
        /// </remarks>
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
                TestEndedSuccessfully();
                return;
            }

            _alreadyPlayedEvents.Add(eventUnderTest.id);

            if (string.IsNullOrEmpty(eventUnderTest.nextId) &&
                VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) != VisualNovelEventType.ShowChoicesEvent &&
                VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) != VisualNovelEventType.EndNovelEvent)
            {
                OnTestFailed("Id of next event is null or empty!", _objectUnderTest.title, eventUnderTest.id);
                return;
            }

            if (!_novelEvents.ContainsKey(eventUnderTest.nextId) &&
                VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) != VisualNovelEventType.ShowChoicesEvent &&
                VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) != VisualNovelEventType.EndNovelEvent)
            {
                OnTestFailed("Next event to play not found!", _objectUnderTest.title, eventUnderTest.id);
                return;
            }

            if ((VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) == VisualNovelEventType.AddChoiceEvent)
                && (string.IsNullOrEmpty(eventUnderTest.onChoice)))
            {
                OnTestFailed("Add Choice event without onChoice value!", _objectUnderTest.title, eventUnderTest.id);
                return;
            }

            if (VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) == VisualNovelEventType.AddChoiceEvent &&
                !_novelEvents.ContainsKey(eventUnderTest.onChoice))
            {
                OnTestFailed("Add Choice event with on choice target that could not be found!", _objectUnderTest.title,
                    eventUnderTest.id);
                return;
            }

            VisualNovelEventType type = VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType);

            if (VisualNovelEventTypeHelper.ValueOf(_nextEventToTest.eventType) != VisualNovelEventType.ShowChoicesEvent &&
                VisualNovelEventTypeHelper.ValueOf(_nextEventToTest.eventType) != VisualNovelEventType.EndNovelEvent)
            {
                string nextEventID = eventUnderTest.nextId;
                _nextEventToTest = _novelEvents[nextEventID];
            }

            switch (type)
            {
                case VisualNovelEventType.CharacterJoinEvent:
                {
                    HandleCharacterJoinEvent(eventUnderTest);
                    break;
                }
                case VisualNovelEventType.CharacterExitEvent:
                {
                    HandleCharacterExitEvent(eventUnderTest);
                    break;
                }
                case VisualNovelEventType.ShowMessageEvent:
                {
                    HandleShowMessageEvent(eventUnderTest);
                    break;
                }
                case VisualNovelEventType.AddChoiceEvent:
                {
                    HandleAddChoiceEvent(eventUnderTest);
                    break;
                }
                case VisualNovelEventType.ShowChoicesEvent:
                {
                    HandleShowChoicesEvent(eventUnderTest);
                    break;
                }
                case VisualNovelEventType.EndNovelEvent:
                {
                    TestEndedSuccessfully();
                    break;
                }
                case VisualNovelEventType.PlaySoundEvent:
                {
                    HandlePlaySoundEvent(eventUnderTest);
                    break;
                }
                case VisualNovelEventType.PlayAnimationEvent:
                {
                    HandlePlayAnimationEvent(eventUnderTest);
                    break;
                }
                case VisualNovelEventType.GptPromptEvent:
                {
                    HandleGptPromptEvent(eventUnderTest);
                    break;
                }
                case VisualNovelEventType.SavePersistentEvent:
                {
                    HandleSavePersistentEvent(eventUnderTest);
                    break;
                }
                case VisualNovelEventType.MarkBiasEvent:
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

        /// <summary>
        /// Handles the event in a VisualNovel where a sound is played by validating the associated audio clip
        /// and proceeding to the next event in the sequence if valid.
        /// </summary>
        /// <param name="novelEvent">The VisualNovelEvent instance that contains the details of the sound event to be handled.</param>
        private void HandlePlaySoundEvent(VisualNovelEvent novelEvent)
        {
            if (!ValidateEventField(novelEvent.audioClipToPlay, "Sound Event without audio clip!", novelEvent)) return;
            PlayNextEvent();
        }

        /// <summary>
        /// Handles the event to play a specified animation during the execution of a visual novel.
        /// </summary>
        /// <param name="novelEvent">The VisualNovelEvent instance containing data about the animation to play.</param>
        private void HandlePlayAnimationEvent(VisualNovelEvent novelEvent)
        {
            if (!ValidateEventField(novelEvent.animationToPlay, "Animation Event without animation!", novelEvent)) return;
            PlayNextEvent();
        }

        /// <summary>
        /// Handles a GPT prompt event by validating its critical fields and triggering the next event if valid.
        /// </summary>
        /// <param name="novelEvent">The VisualNovelEvent instance representing the GPT prompt to process.</param>
        private void HandleGptPromptEvent(VisualNovelEvent novelEvent)
        {
            if (!ValidateEventField(novelEvent.gptPrompt, "GPT prompt event without prompt!", novelEvent)) return;
            if (!ValidateEventField(novelEvent.variablesNameForGptPrompt, "GPT prompt event without variable!", novelEvent)) return;
            PlayNextEvent();
        }

        /// <summary>
        /// Handles a specific type of event in a visual novel by saving persistent data
        /// associated with the event, such as key-value pairs, and continuing the sequence of events.
        /// </summary>
        /// <param name="novelEvent">The VisualNovelEvent instance containing data to be saved persistently.</param>
        private void HandleSavePersistentEvent(VisualNovelEvent novelEvent)
        {
            if (!ValidateEventField(novelEvent.key, "Save persistent event without key!", novelEvent)) return;
            if (!ValidateEventField(novelEvent.value, "Save persistent event without value!", novelEvent)) return;
            PlayNextEvent();
        }

        /// <summary>
        /// Handles events containing bias-related information within a Visual Novel context.
        /// Validates the event's relevant bias before continuing the sequence of events.
        /// </summary>
        /// <param name="novelEvent">An instance of VisualNovelEvent representing the event to process, containing bias details relevant to the scenario.</param>
        private void HandleMarkBiasEvent(VisualNovelEvent novelEvent)
        {
            if (!ValidateEventField(novelEvent.relevantBias, "Discrimination bias event without discrimination bias!", novelEvent)) return;
            PlayNextEvent();
        }

        /// <summary>
        /// Handles the event where a character joins the scene by evaluating and executing the necessary logic.
        /// </summary>
        /// <param name="novelEvent">The VisualNovelEvent instance representing the character join event.</param>
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

        /// <summary>
        /// Handles the exit event for a character in a visual novel by removing the character's role
        /// from the current scene or resetting the scene if applicable.
        /// </summary>
        /// <param name="novelEvent">The VisualNovelEvent instance containing data about the character exit event.</param>
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

        /// <summary>
        /// Handles a "show message" event by validating the event's message, associated character, and character expression.
        /// Verifies that all required data is present and consistent with the current state of the visual novel before proceeding with the next event.
        /// </summary>
        /// <param name="novelEvent">The VisualNovelEvent instance representing the event to be processed.</param>
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

        /// <summary>
        /// Handles the addition of a choice event during the execution of a visual novel test,
        /// ensuring the event is valid and progresses the test to the next event if appropriate.
        /// </summary>
        /// <param name="novelEvent">The VisualNovelEvent instance representing the choice event being handled.</param>
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

        /// <summary>
        /// Handles a "show choices" event within a visual novel test, ensuring choices are properly processed
        /// and simulating user interaction by performing each available choice.
        /// </summary>
        /// <param name="novelEvent">The VisualNovelEvent representing the "show choices" event to be processed.</param>
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

        /// <summary>
        /// Executes a choice in the visual novel event and advances the test by updating the test state and proceeding with the next event.
        /// </summary>
        /// <param name="visualNovelEvent">The VisualNovelEvent representing the choice to be executed in the test.</param>
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

        /// <summary>
        /// Creates and returns a deep copy of the current instance of the NovelTester class, including all its associated data and objects.
        /// </summary>
        /// <returns>A new instance of NovelTester that is a deep copy of the current instance.</returns>
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

        /// <summary>
        /// Marks the successful end of a test when triggered by child nodes in the testing hierarchy.
        /// If all child nodes have completed their testing, it finalizes the test for the current node
        /// and recursively notifies the parent node, if present.
        /// </summary>
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

        /// <summary>
        /// Marks the current test as prematurely finished due to issues in its child's execution hierarchy and propagates the failure condition.
        /// </summary>
        private void FailedEndOfTestTriggeredByChildren()
        {
            if (_isTestFinished)
            {
                return;
            }

            _isTestFinished = true;
            TestEndedEarly();
        }

        /// <summary>
        /// Marks the test as finished and successful, setting the relevant internal flags
        /// and triggering the successful end-of-test logic in the parent tester, if applicable.
        /// </summary>
        private void TestEndedSuccessfully()
        {
            _isTestFinished = true;
            _isTestSuccessful = true;
            _parent?.SuccessfullyEndOfTestTriggeredByChildren();
        }

        /// <summary>
        /// Handles the event when a test needs to be marked as concluded prematurely due to detected errors
        /// either in the primary test instance or within the hierarchy of its child tests.
        /// </summary>
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

        /// <summary>
        /// Validates the specified event field to ensure it is not null, empty, or equal to "NONE".
        /// If validation fails, an error message is logged and the test fails for the given VisualNovelEvent.
        /// </summary>
        /// <param name="value">The value of the event field to validate.</param>
        /// <param name="errorMessage">The error message to log if validation fails.</param>
        /// <param name="novelEvent">The VisualNovelEvent instance associated with the field being validated.</param>
        /// <returns>True if the field is valid; otherwise, false.</returns>
        private bool ValidateEventField(string value, string errorMessage, VisualNovelEvent novelEvent)
        {
            if (string.IsNullOrEmpty(value) || value == "NONE")
            {
                OnTestFailed(errorMessage, _objectUnderTest.title, novelEvent.id);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Handles the logic to execute when a test fails for a visual novel, logging the error and marking the test as finished.
        /// </summary>
        /// <param name="error">The error message describing the reason for the test failure.</param>
        /// <param name="visualNovelUnderTest">The title of the visual novel being tested.</param>
        /// <param name="eventUnderTest">The identifier of the event being tested when the error occurred.</param>
        private void OnTestFailed(string error, string visualNovelUnderTest, string eventUnderTest)
        {
            _isTestFinished = true;
            TestEndedEarly();
            Debug.LogError("Error while testing novel. Novel under test: " + visualNovelUnderTest + "; Event under test: " + eventUnderTest + "; Error: " + error + ";");
        }

        /// <summary>
        /// Determines whether the test for the VisualNovel instance has completed.
        /// </summary>
        /// <returns>True if the test has finished; otherwise, false.</returns>
        public bool IsTestOver()
        {
            return _isTestFinished;
        }

        /// <summary>
        /// Determines whether the test for the VisualNovel instance was successful.
        /// </summary>
        /// <returns>True if the test was successful; otherwise, false.</returns>
        public bool IsTestSuccessful()
        {
            return _isTestSuccessful;
        }
    }
}