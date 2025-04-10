namespace Assets._Scripts.Novel
{
    public enum VisualNovelEventType
    {
        None,
        SetBackgroundEvent,
        CharakterJoinEvent,
        CharacterExitEvent,
        ShowMessageEvent,
        AddChoiceEvent,
        ShowChoicesEvent,
        EndNovelEvent,
        PlaySoundEvent,
        PlayAnimationEvent,
        GptPromptEvent,
        SavePersistentEvent,
        MarkBiasEvent,
        SaveVariableEvent,
        AddFeedbackEvent,
        AddFeedbackUnderConditionEvent,
        CalculateVariableFromBooleanExpressionEvent
    }

    public class VisualNovelEventTypeHelper
    {
        public static int ToInt(VisualNovelEventType eventType)
        {
            switch (eventType)
            {
                case VisualNovelEventType.None:
                {
                    return 0;
                }
                case VisualNovelEventType.SetBackgroundEvent:
                {
                    return 1;
                }
                case VisualNovelEventType.CharakterJoinEvent:
                {
                    return 2;
                }
                case VisualNovelEventType.CharacterExitEvent:
                {
                    return 3;
                }
                case VisualNovelEventType.ShowMessageEvent:
                {
                    return 4;
                }
                case VisualNovelEventType.AddChoiceEvent:
                {
                    return 5;
                }
                case VisualNovelEventType.ShowChoicesEvent:
                {
                    return 6;
                }
                case VisualNovelEventType.EndNovelEvent:
                {
                    return 10;
                }
                case VisualNovelEventType.PlaySoundEvent:
                {
                    return 11;
                }
                case VisualNovelEventType.PlayAnimationEvent:
                {
                    return 12;
                }
                case VisualNovelEventType.GptPromptEvent:
                {
                    return 14;
                }
                case VisualNovelEventType.SavePersistentEvent:
                {
                    return 15;
                }
                case VisualNovelEventType.MarkBiasEvent:
                {
                    return 16;
                }
                case VisualNovelEventType.SaveVariableEvent:
                {
                    return 17;
                }
                case VisualNovelEventType.AddFeedbackEvent:
                {
                    return 18;
                }
                case VisualNovelEventType.AddFeedbackUnderConditionEvent:
                {
                    return 19;
                }
                case VisualNovelEventType.CalculateVariableFromBooleanExpressionEvent:
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
                    return VisualNovelEventType.None;
                }
                case 1:
                {
                    return VisualNovelEventType.SetBackgroundEvent;
                }
                case 2:
                {
                    return VisualNovelEventType.CharakterJoinEvent;
                }
                case 3:
                {
                    return VisualNovelEventType.CharacterExitEvent;
                }
                case 4:
                {
                    return VisualNovelEventType.ShowMessageEvent;
                }
                case 5:
                {
                    return VisualNovelEventType.AddChoiceEvent;
                }
                case 6:
                {
                    return VisualNovelEventType.ShowChoicesEvent;
                }
                case 10:
                {
                    return VisualNovelEventType.EndNovelEvent;
                }
                case 11:
                {
                    return VisualNovelEventType.PlaySoundEvent;
                }
                case 12:
                {
                    return VisualNovelEventType.PlayAnimationEvent;
                }
                case 14:
                {
                    return VisualNovelEventType.GptPromptEvent;
                }
                case 15:
                {
                    return VisualNovelEventType.SavePersistentEvent;
                }
                case 16:
                {
                    return VisualNovelEventType.MarkBiasEvent;
                }
                case 17:
                {
                    return VisualNovelEventType.SaveVariableEvent;
                }
                case 18:
                {
                    return VisualNovelEventType.AddFeedbackEvent;
                }
                case 19:
                {
                    return VisualNovelEventType.AddFeedbackUnderConditionEvent;
                }
                case 20:
                {
                    return VisualNovelEventType.CalculateVariableFromBooleanExpressionEvent;
                }
                default:
                {
                    return VisualNovelEventType.None;
                }
            }
        }
    }
}