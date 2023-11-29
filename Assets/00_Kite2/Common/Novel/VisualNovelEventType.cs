public enum VisualNovelEventType
{
    NONE,
    SET_BACKGROUND_EVENT,
    CHARAKTER_JOIN_EVENT,
    CHARAKTER_EXIT_EVENT,
    SHOW_MESSAGE_EVENT,
    ADD_CHOICE_EVENT,
    SHOW_CHOICES_EVENT,
    ADD_OPINION_CHOICE_EVENT,
    ASK_FOR_OPINION_EVENT,
    SHOW_OPINION_FEEDBACK_EVENT, // This event cleans up the feelingsPanel.
    END_NOVEL_EVENT,
    PLAY_SOUND_EVENT,
    PLAY_ANIMATION_EVENT,
    FREE_TEXT_INPUT_EVENT,
    GPT_PROMPT_EVENT
}

public class VisualNovelEventTypeHelper
{
    public static int ToInt(VisualNovelEventType eventType)
    {
        switch (eventType)
        {
            case VisualNovelEventType.NONE: { return 0; }
            case VisualNovelEventType.SET_BACKGROUND_EVENT: { return 1; }
            case VisualNovelEventType.CHARAKTER_JOIN_EVENT: { return 2; }
            case VisualNovelEventType.CHARAKTER_EXIT_EVENT: { return 3; }
            case VisualNovelEventType.SHOW_MESSAGE_EVENT: { return 4; }
            case VisualNovelEventType.ADD_CHOICE_EVENT: { return 5; }
            case VisualNovelEventType.SHOW_CHOICES_EVENT: { return 6; }
            case VisualNovelEventType.ADD_OPINION_CHOICE_EVENT: { return 7; }
            case VisualNovelEventType.ASK_FOR_OPINION_EVENT: { return 8; }
            case VisualNovelEventType.SHOW_OPINION_FEEDBACK_EVENT: { return 9; }
            case VisualNovelEventType.END_NOVEL_EVENT: { return 10; }
            case VisualNovelEventType.PLAY_SOUND_EVENT: { return 11; }
            case VisualNovelEventType.PLAY_ANIMATION_EVENT: { return 12; }
            case VisualNovelEventType.FREE_TEXT_INPUT_EVENT: { return 13; }
            case VisualNovelEventType.GPT_PROMPT_EVENT: { return 14; }

            default: { return -1; }
        }
    }

    public static VisualNovelEventType ValueOf(int i)
    {
        switch (i)
        {
            case 0: { return VisualNovelEventType.NONE; }
            case 1: { return VisualNovelEventType.SET_BACKGROUND_EVENT; }
            case 2: { return VisualNovelEventType.CHARAKTER_JOIN_EVENT; }
            case 3: { return VisualNovelEventType.CHARAKTER_EXIT_EVENT; }
            case 4: { return VisualNovelEventType.SHOW_MESSAGE_EVENT; }
            case 5: { return VisualNovelEventType.ADD_CHOICE_EVENT; }
            case 6: { return VisualNovelEventType.SHOW_CHOICES_EVENT; }
            case 7: { return VisualNovelEventType.ADD_OPINION_CHOICE_EVENT; }
            case 8: { return VisualNovelEventType.ASK_FOR_OPINION_EVENT; }
            case 9: { return VisualNovelEventType.SHOW_OPINION_FEEDBACK_EVENT; }
            case 10: { return VisualNovelEventType.END_NOVEL_EVENT; }
            case 11: { return VisualNovelEventType.PLAY_SOUND_EVENT; }
            case 12: { return VisualNovelEventType.PLAY_ANIMATION_EVENT; }
            case 13: { return VisualNovelEventType.FREE_TEXT_INPUT_EVENT; }
            case 14: { return VisualNovelEventType.GPT_PROMPT_EVENT; }
            default: { return VisualNovelEventType.NONE; }
        }
    }
}
