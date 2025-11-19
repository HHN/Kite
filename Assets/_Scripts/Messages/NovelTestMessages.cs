namespace Assets._Scripts.Messages
{
    /// <summary>
    /// This static class centralizes all constant string messages used by the <c>NovelTester</c> class
    /// for logging errors and marking failure conditions during the simulated execution of a <c>VisualNovel</c>.
    /// Centralizing these messages enhances maintainability, consistency, and future localization efforts.
    /// </summary>
    public class NovelTestMessages
    {
        // General
        public const string ERR_NOVEL_IS_NULL = "Novel under test is null.";
        public const string ERR_NO_EVENTS_FOUND = "No novel events found.";
        public const string ERR_TITLE_NULL_OR_EMPTY = "Novel title is null or empty.";
    
        // Event Validation
        public const string ERR_EVENT_IS_NULL = "Event to play is null!";
        public const string ERR_EVENT_ID_EMPTY = "Event id is null or empty!";
        public const string ERR_NEXT_ID_EMPTY = "Id of next event is null or empty!";
        public const string ERR_NEXT_EVENT_NOT_FOUND = "Next event to play not found!";
        public const string ERR_EVENT_WITHOUT_TYPE = "Event without event type!";
    
        // Choice Validation
        public const string ERR_CHOICES_EVENT_WITHOUT_CHOICES = "Show choices event without choices!";
        public const string ERR_ADD_CHOICE_WITHOUT_TEXT = "Add choice event without text!";
        public const string ERR_ADD_CHOICE_WITHOUT_ONCHOICE = "Add Choice event without onChoice value!";
        public const string ERR_ADD_CHOICE_TARGET_NOT_FOUND = "Add Choice event with on choice target that could not be found!";
        public const string ERR_ONCHOICE_TARGET_NOT_FOUND = "On choice event with target that could not be found!";
        
        // Character & Message Events
        public const string ERR_JOIN_EVENT_WITHOUT_CHARACTER = "CharacterRole joins event without character!";
        public const string ERR_EXIT_EVENT_CHARACTER_NOT_IN_SCENE = "CharacterRole exit event with character that is not in the scene!";
        public const string ERR_SHOW_MESSAGE_WITHOUT_TEXT = "Show message event without message!";
        public const string ERR_SHOW_MESSAGE_CHARACTER_NOT_IN_SCENE = "Show message event with speaking character that is not in the scene!";

        // Media & Bias Events (for ValidateEventField)
        public const string ERR_SOUND_EVENT_WITHOUT_CLIP = "Sound Event without audio clip!";
        public const string ERR_BIAS_EVENT_WITHOUT_BIAS = "Discrimination bias event without discrimination bias!"; 
    }
}
