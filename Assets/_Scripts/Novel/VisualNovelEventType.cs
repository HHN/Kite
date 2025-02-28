namespace Assets._Scripts.Novel
{
    public enum VisualNovelEventType
    {
        NONE,
        SET_BACKGROUND_EVENT,
        CHARAKTER_JOIN_EVENT,
        CHARAKTER_EXIT_EVENT,
        SHOW_MESSAGE_EVENT,
        ADD_CHOICE_EVENT,
        SHOW_CHOICES_EVENT,
        END_NOVEL_EVENT,
        PLAY_SOUND_EVENT,
        PLAY_ANIMATION_EVENT,
        FREE_TEXT_INPUT_EVENT,
        GPT_PROMPT_EVENT,
        SAVE_PERSISTENT_EVENT,
        MARK_BIAS_EVENT,
        SAVE_VARIABLE_EVENT,
        ADD_FEEDBACK_EVENT,
        ADD_FEEDBACK_UNDER_CONDITION_EVENT,
        CALCULATE_VARIABLE_FROM_BOOLEAN_EXPRESSION_EVENT
    }

    public class VisualNovelEventTypeHelper
    {
        public static int ToInt(VisualNovelEventType eventType)
        {
            switch (eventType)
            {
                case VisualNovelEventType.NONE:
                {
                    return 0;
                }
                case VisualNovelEventType.SET_BACKGROUND_EVENT:
                {
                    return 1;
                }
                case VisualNovelEventType.CHARAKTER_JOIN_EVENT:
                {
                    return 2;
                }
                case VisualNovelEventType.CHARAKTER_EXIT_EVENT:
                {
                    return 3;
                }
                case VisualNovelEventType.SHOW_MESSAGE_EVENT:
                {
                    return 4;
                }
                case VisualNovelEventType.ADD_CHOICE_EVENT:
                {
                    return 5;
                }
                case VisualNovelEventType.SHOW_CHOICES_EVENT:
                {
                    return 6;
                }
                case VisualNovelEventType.END_NOVEL_EVENT:
                {
                    return 10;
                }
                case VisualNovelEventType.PLAY_SOUND_EVENT:
                {
                    return 11;
                }
                case VisualNovelEventType.PLAY_ANIMATION_EVENT:
                {
                    return 12;
                }
                case VisualNovelEventType.FREE_TEXT_INPUT_EVENT:
                {
                    return 13;
                }
                case VisualNovelEventType.GPT_PROMPT_EVENT:
                {
                    return 14;
                }
                case VisualNovelEventType.SAVE_PERSISTENT_EVENT:
                {
                    return 15;
                }
                case VisualNovelEventType.MARK_BIAS_EVENT:
                {
                    return 16;
                }
                case VisualNovelEventType.SAVE_VARIABLE_EVENT:
                {
                    return 17;
                }
                case VisualNovelEventType.ADD_FEEDBACK_EVENT:
                {
                    return 18;
                }
                case VisualNovelEventType.ADD_FEEDBACK_UNDER_CONDITION_EVENT:
                {
                    return 19;
                }
                case VisualNovelEventType.CALCULATE_VARIABLE_FROM_BOOLEAN_EXPRESSION_EVENT:
                {
                    return 20;
                }

                default:
                {
                    return -1;
                }
            }
        }

        public static VisualNovelEventType ValueOf(int i)
        {
            switch (i)
            {
                case 0:
                {
                    return VisualNovelEventType.NONE;
                }
                case 1:
                {
                    return VisualNovelEventType.SET_BACKGROUND_EVENT;
                }
                case 2:
                {
                    return VisualNovelEventType.CHARAKTER_JOIN_EVENT;
                }
                case 3:
                {
                    return VisualNovelEventType.CHARAKTER_EXIT_EVENT;
                }
                case 4:
                {
                    return VisualNovelEventType.SHOW_MESSAGE_EVENT;
                }
                case 5:
                {
                    return VisualNovelEventType.ADD_CHOICE_EVENT;
                }
                case 6:
                {
                    return VisualNovelEventType.SHOW_CHOICES_EVENT;
                }
                case 10:
                {
                    return VisualNovelEventType.END_NOVEL_EVENT;
                }
                case 11:
                {
                    return VisualNovelEventType.PLAY_SOUND_EVENT;
                }
                case 12:
                {
                    return VisualNovelEventType.PLAY_ANIMATION_EVENT;
                }
                case 13:
                {
                    return VisualNovelEventType.FREE_TEXT_INPUT_EVENT;
                }
                case 14:
                {
                    return VisualNovelEventType.GPT_PROMPT_EVENT;
                }
                case 15:
                {
                    return VisualNovelEventType.SAVE_PERSISTENT_EVENT;
                }
                case 16:
                {
                    return VisualNovelEventType.MARK_BIAS_EVENT;
                }
                case 17:
                {
                    return VisualNovelEventType.SAVE_VARIABLE_EVENT;
                }
                case 18:
                {
                    return VisualNovelEventType.ADD_FEEDBACK_EVENT;
                }
                case 19:
                {
                    return VisualNovelEventType.ADD_FEEDBACK_UNDER_CONDITION_EVENT;
                }
                case 20:
                {
                    return VisualNovelEventType.CALCULATE_VARIABLE_FROM_BOOLEAN_EXPRESSION_EVENT;
                }
                default:
                {
                    return VisualNovelEventType.NONE;
                }
            }
        }
    }
}