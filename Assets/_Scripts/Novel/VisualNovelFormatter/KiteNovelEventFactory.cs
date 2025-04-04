using Assets._Scripts.Novel;

namespace Assets._Scripts.Player.KiteNovels.VisualNovelFormatter
{
    public abstract class KiteNovelEventFactory
    {
        public static VisualNovelEvent GetBiasEvent(string id, string nextId, DiscriminationBias relevantBias)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.MarkBiasEvent, false);
            novelEvent.relevantBias = DiscriminationBiasHelper.ToInt(relevantBias);
            return novelEvent;
        }

        public static VisualNovelEvent GetCharacterTalksEvent(string id, string nextId, CharacterRole characterRole,
            string dialogMessage, CharacterExpression expression)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.ShowMessageEvent, true);
            novelEvent.character = CharacterTypeHelper.ToInt(characterRole);
            novelEvent.text = dialogMessage;
            novelEvent.expressionType = CharacterExpressionHelper.ToInt(expression);
            return novelEvent;
        }

        public static VisualNovelEvent GetSetBackgroundEvent(string id, string nextId)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.SetBackgroundEvent, false);
            return novelEvent;
        }

        public static VisualNovelEvent GetCharacterJoinsEvent(string id, string nextId, CharacterRole characterRole,
            CharacterExpression expression)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.CharakterJoinEvent, false);
            novelEvent.character = CharacterTypeHelper.ToInt(characterRole);
            novelEvent.expressionType = CharacterExpressionHelper.ToInt(expression);
            return novelEvent;
        }

        public static VisualNovelEvent GetCharacterExitsEvent(string id, string nextId)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.CharacterExitEvent, false);
            return novelEvent;
        }

        public static VisualNovelEvent GetGptEvent(string id, string nextId, string prompt, string variablesName,
            CompletionHandler completionHandlerId)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.GptPromptEvent, false);
            novelEvent.gptPrompt = prompt;
            novelEvent.variablesNameForGptPrompt = variablesName;
            novelEvent.gptCompletionHandlerId = CompletionHandlerHelper.ToInt(completionHandlerId);
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

        public static VisualNovelEvent GetCalculateVariableFromBooleanExpressionEvent(string id, string nextId,
            string key, string value)
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

        public static VisualNovelEvent GetAddFeedbackUnderConditionEvent(string id, string nextId, string key,
            string value)
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

        public static VisualNovelEvent GetAddChoiceEvent(string id, string nextId, string optionText, string onChoice,
            bool showAfterSelection)
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

        public static VisualNovelEvent GetPlayAnimationEvent(string id, string nextId, KiteAnimation animationToPlay)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.PlayAnimationEvent, false);
            novelEvent.animationToPlay = KiteAnimationHelper.ToInt(animationToPlay);
            return novelEvent;
        }

        public static VisualNovelEvent GetPlaySoundEvent(string id, string nextId, KiteSound audioClipToPlay)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.PlaySoundEvent, false);
            novelEvent.audioClipToPlay = KiteSoundHelper.ToInt(audioClipToPlay);
            return novelEvent;
        }


        private static VisualNovelEvent CreateEvent(string id, string nextId, VisualNovelEventType type,
            bool waitForConfirmation)
        {
            VisualNovelEvent novelEvent = new VisualNovelEvent();
            novelEvent.id = id;
            novelEvent.nextId = nextId;
            novelEvent.eventType = VisualNovelEventTypeHelper.ToInt(type);
            novelEvent.waitForUserConfirmation = waitForConfirmation;
            return novelEvent;
        }
    }
}