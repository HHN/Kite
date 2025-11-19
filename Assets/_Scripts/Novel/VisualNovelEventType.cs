namespace Assets._Scripts.Novel
{
    /// <summary>
    /// Defines the various types of events that can occur within a visual novel.
    /// Each enum member represents a distinct action or state change in the narrative flow.
    /// </summary>
    public enum VisualNovelEventType
    {
        None,
        CharacterJoinEvent,
        CharacterExitEvent,
        ShowMessageEvent,
        AddChoiceEvent,
        ShowChoicesEvent,
        EndNovelEvent,
        PlaySoundEvent,
        MarkBiasEvent
    }

    /// <summary>
    /// Provides helper methods for converting between <see cref="VisualNovelEventType"/> enum values and integer representations.
    /// This is useful for serialization or external data storage where enums might be stored as integers.
    /// </summary>
    public class VisualNovelEventTypeHelper
    {
        /// <summary>
        /// Converts a <see cref="VisualNovelEventType"/> enum value to its corresponding integer representation.
        /// </summary>
        /// <param name="eventType">The <see cref="VisualNovelEventType"/> to convert.</param>
        /// <returns>The integer value corresponding to the event type, or -1 if the event type is not mapped.</returns>
        public static int ToInt(VisualNovelEventType eventType)
        {
            return eventType switch
            {
                VisualNovelEventType.None => 0,
                VisualNovelEventType.CharacterJoinEvent => 2,
                VisualNovelEventType.CharacterExitEvent => 3,
                VisualNovelEventType.ShowMessageEvent => 4,
                VisualNovelEventType.AddChoiceEvent => 5,
                VisualNovelEventType.ShowChoicesEvent => 6,
                VisualNovelEventType.EndNovelEvent => 10,
                VisualNovelEventType.PlaySoundEvent => 11,
                VisualNovelEventType.MarkBiasEvent => 16,
                _ => -1
            };
        }

        /// <summary>
        /// Converts an integer value back to its corresponding <see cref="VisualNovelEventType"/> enum value.
        /// </summary>
        /// <param name="i">The integer value representing an event type.</param>
        /// <returns>The <see cref="VisualNovelEventType"/> corresponding to the integer, or <see cref="VisualNovelEventType.None"/> if the integer is not mapped.</returns>
        public static VisualNovelEventType ValueOf(int i)
        {
            return i switch
            {
                0 => VisualNovelEventType.None,
                2 => VisualNovelEventType.CharacterJoinEvent,
                3 => VisualNovelEventType.CharacterExitEvent,
                4 => VisualNovelEventType.ShowMessageEvent,
                5 => VisualNovelEventType.AddChoiceEvent,
                6 => VisualNovelEventType.ShowChoicesEvent,
                10 => VisualNovelEventType.EndNovelEvent,
                11 => VisualNovelEventType.PlaySoundEvent,
                16 => VisualNovelEventType.MarkBiasEvent,
                _ => VisualNovelEventType.None
            };
        }
    }
}