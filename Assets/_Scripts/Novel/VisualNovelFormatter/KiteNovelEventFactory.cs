namespace Assets._Scripts.Novel.VisualNovelFormatter
{
    /// <summary>
    /// Factory class for creating different types of visual novel events used in a visual novel formatter.
    /// </summary>
    public abstract class KiteNovelEventFactory
    {
        /// <summary>
        /// Creates a VisualNovelEvent of type MarkBiasEvent with the specified bias relevance.
        /// </summary>
        /// <param name="id">The unique identifier of the event.</param>
        /// <param name="nextId">The identifier of the next event in the sequence.</param>
        /// <param name="relevantBias">The relevant bias associated with the event.</param>
        /// <returns>A VisualNovelEvent configured as a MarkBiasEvent with the specified properties.</returns>
        public static VisualNovelEvent GetBiasEvent(string id, string nextId, string relevantBias)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.MarkBiasEvent, false);
            novelEvent.relevantBias = relevantBias;
            return novelEvent;
        }

        /// <summary>
        /// Creates a VisualNovelEvent of the type ShowMessageEvent where a specified character speaks a dialog message with a given expression.
        /// </summary>
        /// <param name="id">The unique identifier of the event.</param>
        /// <param name="nextId">The identifier of the next event in the sequence.</param>
        /// <param name="characterRole">The role or identifier of the character speaking.</param>
        /// <param name="dialogMessage">The dialog message to be spoken by the character.</param>
        /// <param name="expression">The expression type for the character during the dialog.</param>
        /// <returns>A VisualNovelEvent configured as a ShowMessageEvent with the specified properties.</returns>
        public static VisualNovelEvent GetCharacterTalksEvent(string id, string nextId, int characterRole, string dialogMessage, int expression)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.ShowMessageEvent, true);
            novelEvent.character = characterRole;
            novelEvent.text = dialogMessage;
            novelEvent.expressionType = expression;
            return novelEvent;
        }

        /// <summary>
        /// Creates a VisualNovelEvent of type CharacterJoinEvent with the specified properties.
        /// </summary>
        /// <param name="id">The unique identifier of the event.</param>
        /// <param name="nextId">The identifier of the next event in the sequence.</param>
        /// <param name="characterRole">The role of the character joining the event.</param>
        /// <param name="expression">The expression type associated with the character.</param>
        /// <returns>A VisualNovelEvent configured as a CharacterJoinEvent with the specified properties.</returns>
        public static VisualNovelEvent GetCharacterJoinsEvent(string id, string nextId, int characterRole, int expression)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.CharacterJoinEvent, false);
            novelEvent.character = characterRole;
            novelEvent.expressionType = expression;
            return novelEvent;
        }

        /// <summary>
        /// Creates a VisualNovelEvent of type CharacterExitEvent with the specified properties.
        /// </summary>
        /// <param name="id">The unique identifier of the event.</param>
        /// <param name="nextId">The identifier of the next event in the sequence.</param>
        /// <returns>A VisualNovelEvent configured as a CharacterExitEvent with the specified properties.</returns>
        public static VisualNovelEvent GetCharacterExitsEvent(string id, string nextId)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.CharacterExitEvent, false);
            return novelEvent;
        }

        /// <summary>
        /// Creates a VisualNovelEvent of the type EndNovelEvent with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the event.</param>
        /// <returns>A VisualNovelEvent configured as an EndNovelEvent with the specified properties.</returns>
        public static VisualNovelEvent GetEndNovelEvent(string id)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, "", VisualNovelEventType.EndNovelEvent, false);
            return novelEvent;
        }

        /// <summary>
        /// Creates a VisualNovelEvent of type AddChoiceEvent with the specified choice details.
        /// </summary>
        /// <param name="id">The unique identifier of the event.</param>
        /// <param name="nextId">The identifier of the next event in the sequence.</param>
        /// <param name="optionText">The text displayed for the choice option.</param>
        /// <param name="onChoice">The identifier of the event triggered when the choice is selected.</param>
        /// <param name="showAfterSelection">Indicates whether the choice will be displayed again after being selected.</param>
        /// <returns>A VisualNovelEvent configured as an AddChoiceEvent with the specified properties.</returns>
        public static VisualNovelEvent GetAddChoiceEvent(string id, string nextId, string optionText, string onChoice, bool showAfterSelection)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.AddChoiceEvent, false);
            novelEvent.text = optionText;
            novelEvent.onChoice = onChoice;
            novelEvent.show = showAfterSelection;
            return novelEvent;
        }

        /// <summary>
        /// Creates a VisualNovelEvent of type ShowChoicesEvent with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the event.</param>
        /// <returns>A VisualNovelEvent configured as a ShowChoicesEvent with the specified properties.</returns>
        public static VisualNovelEvent GetShowChoicesEvent(string id)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, "", VisualNovelEventType.ShowChoicesEvent, true);
            return novelEvent;
        }

        /// <summary>
        /// Creates a VisualNovelEvent of type PlaySoundEvent with the specified audio clip to play.
        /// </summary>
        /// <param name="id">The unique identifier of the event.</param>
        /// <param name="nextId">The identifier of the next event in the sequence.</param>
        /// <param name="audioClipToPlay">The name of the audio clip to be played during the event.</param>
        /// <returns>A VisualNovelEvent configured as a PlaySoundEvent with the specified properties.</returns>
        public static VisualNovelEvent GetPlaySoundEvent(string id, string nextId, string audioClipToPlay)
        {
            VisualNovelEvent novelEvent = CreateEvent(id, nextId, VisualNovelEventType.PlaySoundEvent, false);
            novelEvent.audioClipToPlay = audioClipToPlay;
            return novelEvent;
        }

        /// <summary>
        /// Creates a VisualNovelEvent with the specified properties.
        /// </summary>
        /// <param name="id">The unique identifier of the event.</param>
        /// <param name="nextId">The identifier of the next event in the sequence.</param>
        /// <param name="type">The type of the visual novel event.</param>
        /// <param name="waitForConfirmation">Indicates whether user confirmation is required to proceed to the next event.</param>
        /// <returns>A VisualNovelEvent with the specified properties configured.</returns>
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