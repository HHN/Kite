namespace Assets._Scripts.Novels.Visual_Novel_Formatter
{
    public abstract class KiteNovelEventFactory
    {
        public static VisualNovelEvent GetBiasEvent(string id, string nextId, DiscriminationBias relevantBias)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.MARK_BIAS_EVENT, false);
            novelEvent.relevantBias = DiscriminationBiasHelper.ToInt(relevantBias);
            return novelEvent;
        }

        public static VisualNovelEvent GetCharacterTalksEvent(string id, string nextId, CharacterRole characterRole,
            string dialogMessage, CharacterExpression expression)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.SHOW_MESSAGE_EVENT, true);
            novelEvent.character = CharacterTypeHelper.ToInt(characterRole);
            novelEvent.text = dialogMessage;
            novelEvent.expressionType = CharacterExpressionHelper.ToInt(expression);
            return novelEvent;
        }

        public static VisualNovelEvent GetSetBackgroundEvent(string id, string nextId, Location location)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.SET_BACKGROUND_EVENT, false);
            novelEvent.backgroundSpriteId = LocationHelper.ToInt(location);
            return novelEvent;
        }

        public static VisualNovelEvent GetCharacterJoinsEvent(string id, string nextId, CharacterRole characterRole,
            CharacterExpression expression)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.CHARAKTER_JOIN_EVENT, false);
            novelEvent.character = CharacterTypeHelper.ToInt(characterRole);
            novelEvent.expressionType = CharacterExpressionHelper.ToInt(expression);
            return novelEvent;
        }

        public static VisualNovelEvent GetCharacterExitsEvent(string id, string nextId)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.CHARAKTER_EXIT_EVENT, false);
            return novelEvent;
        }

        public static VisualNovelEvent GetFreeTextInputEvent(string id, string nextId, string question,
            string variablesName)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.FREE_TEXT_INPUT_EVENT, true);
            novelEvent.questionForFreeTextInput = question;
            novelEvent.variablesName = variablesName;
            return novelEvent;
        }

        public static VisualNovelEvent GetGptEvent(string id, string nextId, string prompt, string variablesName,
            CompletionHandler completionHandlerId)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.GPT_PROMPT_EVENT, false);
            novelEvent.gptPrompt = prompt;
            novelEvent.variablesNameForGptPrompt = variablesName;
            novelEvent.gptCompletionHandlerId = CompletionHandlerHelper.ToInt(completionHandlerId);
            return novelEvent;
        }

        public static VisualNovelEvent GetSavePersistentEvent(string id, string nextId, string key, string value)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.SAVE_PERSISTENT_EVENT, false);
            novelEvent.key = key;
            novelEvent.value = value;
            return novelEvent;
        }

        public static VisualNovelEvent GetSaveVariableEvent(string id, string nextId, string key, string value)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.SAVE_VARIABLE_EVENT, false);
            novelEvent.key = key;
            novelEvent.value = value;
            return novelEvent;
        }

        public static VisualNovelEvent GetCalculateVariableFromBooleanExpressionEvent(string id, string nextId,
            string key, string value)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId,
                VisualNovelEventType.CALCULATE_VARIABLE_FROM_BOOLEAN_EXPRESSION_EVENT, false);
            novelEvent.key = key;
            novelEvent.value = value;
            return novelEvent;
        }

        public static VisualNovelEvent GetAddFeedbackEvent(string id, string nextId, string message)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.ADD_FEEDBACK_EVENT, false);
            novelEvent.value = message;
            return novelEvent;
        }

        public static VisualNovelEvent GetAddFeedbackUnderConditionEvent(string id, string nextId, string key,
            string value)
        {
            VisualNovelEvent novelEvent =
                CreateEvent(id, nextId, VisualNovelEventType.ADD_FEEDBACK_UNDER_CONDITION_EVENT, false);
            novelEvent.key = key;
            novelEvent.value = value;
            return novelEvent;
        }

        public static VisualNovelEvent GetEndNovelEvent(string id)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, "", VisualNovelEventType.END_NOVEL_EVENT, false);
            return novelEvent;
        }

        public static VisualNovelEvent GetAddChoiceEvent(string id, string nextId, string optionText, string onChoice,
            bool showAfterSelection)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.ADD_CHOICE_EVENT, false);
            novelEvent.text = optionText;
            novelEvent.onChoice = onChoice;
            novelEvent.show = showAfterSelection;
            return novelEvent;
        }

        public static VisualNovelEvent GetShowChoicesEvent(string id)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, "", VisualNovelEventType.SHOW_CHOICES_EVENT, true);
            return novelEvent;
        }

        public static VisualNovelEvent GetPlayAnimationEvent(string id, string nextId, KiteAnimation animationToPlay)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.PLAY_ANIMATION_EVENT, false);
            novelEvent.animationToPlay = KiteAnimationHelper.ToInt(animationToPlay);
            return novelEvent;
        }

        public static VisualNovelEvent GetPlaySoundEvent(string id, string nextId, KiteSound audioClipToPlay)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.PLAY_SOUND_EVENT, false);
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