using Assets._Scripts.Novel;

namespace Assets._Scripts.Player.KiteNovels.VisualNovelFormatter
{
    public abstract class KiteNovelEventFactory
    {
        public static VisualNovelEvent GetBiasEvent(string id, string nextId, string relevantBias)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.MARK_BIAS_EVENT, false);
            novelEvent.relevantBias = relevantBias;
            return novelEvent;
        }

        public static VisualNovelEvent GetCharacterTalksEvent(string id, string nextId, string characterRole, string dialogMessage, string expression)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.SHOW_MESSAGE_EVENT, true);
            novelEvent.character = characterRole;
            novelEvent.text = dialogMessage;
            novelEvent.expressionType = expression;
            return novelEvent;
        }

        public static VisualNovelEvent GetSetBackgroundEvent(string id, string nextId, string location)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.SET_BACKGROUND_EVENT, false);
            novelEvent.backgroundSprite = location;
            return novelEvent;
        }

        public static VisualNovelEvent GetCharacterJoinsEvent(string id, string nextId, string characterRole, string expression)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.CHARAKTER_JOIN_EVENT, false);
            novelEvent.character = characterRole;
            novelEvent.expressionType = expression;
            return novelEvent;
        }

        public static VisualNovelEvent GetCharacterExitsEvent(string id, string nextId)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.CHARAKTER_EXIT_EVENT, false);
            return novelEvent;
        }

        public static VisualNovelEvent GetFreeTextInputEvent(string id, string nextId, string question, string variablesName)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.FREE_TEXT_INPUT_EVENT, true);
            novelEvent.questionForFreeTextInput = question;
            novelEvent.variablesName = variablesName;
            return novelEvent;
        }

        public static VisualNovelEvent GetGptEvent(string id, string nextId, string prompt, string variablesName, string completionHandlerId)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.GptPromptEvent, false);
            novelEvent.gptPrompt = prompt;
            novelEvent.variablesNameForGptPrompt = variablesName;
            novelEvent.gptCompletionHandler = completionHandlerId;
            return novelEvent;
        }

        public static VisualNovelEvent GetSavePersistentEvent(string id, string nextId, string key, string value)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.SavePersistentEvent, false);
            novelEvent.key = key;
            novelEvent.value = value;
            return novelEvent;
        }

        public static VisualNovelEvent GetSaveVariableEvent(string id, string nextId, string key, string value)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.SaveVariableEvent, false);
            novelEvent.key = key;
            novelEvent.value = value;
            return novelEvent;
        }

        public static VisualNovelEvent GetCalculateVariableFromBooleanExpressionEvent(string id, string nextId, string key, string value)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId,
                VisualNovelEventType.CalculateVariableFromBooleanExpressionEvent, false);
            novelEvent.key = key;
            novelEvent.value = value;
            return novelEvent;
        }

        public static VisualNovelEvent GetAddFeedbackEvent(string id, string nextId, string message)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.AddFeedbackEvent, false);
            novelEvent.value = message;
            return novelEvent;
        }

        public static VisualNovelEvent GetAddFeedbackUnderConditionEvent(string id, string nextId, string key, string value)
        {
            VisualNovelEvent novelEvent =
                CreateEvent(id, nextId, VisualNovelEventType.AddFeedbackUnderConditionEvent, false);
            novelEvent.key = key;
            novelEvent.value = value;
            return novelEvent;
        }

        public static VisualNovelEvent GetEndNovelEvent(string id)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, "", VisualNovelEventType.EndNovelEvent, false);
            return novelEvent;
        }

        public static VisualNovelEvent GetAddChoiceEvent(string id, string nextId, string optionText, string onChoice, bool showAfterSelection)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.AddChoiceEvent, false);
            novelEvent.text = optionText;
            novelEvent.onChoice = onChoice;
            novelEvent.show = showAfterSelection;
            return novelEvent;
        }

        public static VisualNovelEvent GetShowChoicesEvent(string id)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, "", VisualNovelEventType.ShowChoicesEvent, true);
            return novelEvent;
        }

        // TODO: Check if this should be removed. Isn't used by the time this is written.
        public static VisualNovelEvent GetPlayAnimationEvent(string id, string nextId, string animationToPlay)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.PLAY_ANIMATION_EVENT, false);
            novelEvent.animationToPlay = animationToPlay;
            return novelEvent;
        }

        public static VisualNovelEvent GetPlaySoundEvent(string id, string nextId, string audioClipToPlay)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.PLAY_SOUND_EVENT, false);
            novelEvent.audioClipToPlay = audioClipToPlay;
            return novelEvent;
        }


        private static VisualNovelEvent CreateEvent(string id, string nextId, VisualNovelEventType type, bool waitForConfirmation)
        {
            VisualNovelEvent novelEvent = new VisualNovelEvent
            {
                id = id,
                nextId = nextId,
                eventType = VisualNovelEventTypeHelper.ToInt(type),
                waitForUserConfirmation = waitForConfirmation
            };
            return novelEvent;
        }
    }
}