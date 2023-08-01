using System.Collections.Generic;

public class CallWithParentsNovel : VisualNovel
{
    public CallWithParentsNovel()
    {
        title = "Telefonat mit den Eltern";
        description = "Du beschließt, deine Eltern anzurufen, und ihnen von deinem Gründungsvorhaben zu berichten.";
        image = 1;
        nameOfMainCharacter = "Lea";
        feedback = "";
        novelEvents = new List<VisualNovelEvent>()
        {
            new VisualNovelEvent()
            {
                id = 1,
                nextId = 2,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SET_BACKGROUND_EVENT),
                waitForUserConfirmation = false,
                animationType = AnimationTypeHelper.ToInt(AnimationType.FLY_IN_FROM_ABOVE),
                xPosition = 0,
                yPosition = 0,
                imageId = 0
            },

            new VisualNovelEvent()
            {
                id = 2,
                nextId = 3,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.CHARAKTER_JOIN_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                animationType = AnimationTypeHelper.ToInt(AnimationType.FLY_IN_FROM_ABOVE),
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
                xPosition = 0,
                yPosition = -6,
                imageId = 0
            },

            new VisualNovelEvent()
            {
                id = 3,
                nextId = 4,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Intro",
                text = "Du beschließt, deine Eltern anzurufen, und ihnen von deinem Gründungsvorhaben zu berichten."
            },

            new VisualNovelEvent()
            {
                id = 4,
                nextId = 5,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Hallo, Schön dass du anrufst. Wie geht es dir?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 5,
                nextId = 6,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Hi. Mir geht es gut. Ich rufe an, weil ich euch etwas wichtiges mitteilen möchte.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 6,
                nextId = 7,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Wirklich? Was denn?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 7,
                nextId = 8,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_OPINION_FEEDBACK_EVENT),
                waitForUserConfirmation = false
            },

            new VisualNovelEvent()
            {
                id = 8,
                nextId = 9,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
                waitForUserConfirmation = false,
                opinionChoiceNumber = 1,
                text = "Deinen Eltern dein Gründungsvorhaben zu erzählen, kann dich nervös machen. " +
                "Ihre Meinung zählt für dich schließlich mehr als die von Fremden."
            },

            new VisualNovelEvent()
            {
                id = 9,
                nextId = 10,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
                waitForUserConfirmation = false,
                opinionChoiceNumber = 2,
                text = "Du weißt nicht, wie deine Eltern auf deine Idee, zu gründen, reagieren werden. " +
                "Daher ist es in Ordnung auch in dieser Situation ängstlich zu sein."
            },

            new VisualNovelEvent()
            {
                id = 10,
                nextId = 11,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
                waitForUserConfirmation = false,
                opinionChoiceNumber = 3,
                text = "Es kann Mut machen, seinen eigenen Eltern von der Gründungsidee zu erzählen. " +
                "Insbesondere wenn du jetzt schon weißt, dass sie hinter dir stehen."
            },

            new VisualNovelEvent()
            {
                id = 11,
                nextId = 12,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
                waitForUserConfirmation = false,
                opinionChoiceNumber = 4,
                text = ""
            },

            new VisualNovelEvent()
            {
                id = 12,
                nextId = 13,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ASK_FOR_OPINION_EVENT),
                waitForUserConfirmation = true,
            },

            new VisualNovelEvent()
            {
                id = 13,
                nextId = 14,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich habe vor mich selbständig zu machen und ein Unternehmen zu gründen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 14,
                nextId = 15,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Oh... Ist sowas nicht furchtbar unsicher?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 15,
                nextId = 16,
                onChoice = 18,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "So etwas ist immer mit einem Risiko verbunden, aber ich habe mir darüber " +
                "bereits Gedanken gemacht."
            },

            new VisualNovelEvent()
            {
                id = 16,
                nextId = 17,
                onChoice = 19,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Eigentlich nicht, wenn man richtig plant."
            },

            new VisualNovelEvent()
            {
                id = 17,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT),
                waitForUserConfirmation = true
            },

            new VisualNovelEvent()
            {
                id = 18,
                nextId = 20,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Ah, wenn das so ist, wird das sicher klappen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 19,
                nextId = 20,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Du hast bisher immer alles geschafft, was du wolltest. " +
                "Also wirst du auch das packen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 20,
                nextId = 21,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.CHARAKTER_EXIT_EVENT),
                waitForUserConfirmation = false,
                name = "Mama",
                animationType = AnimationTypeHelper.ToInt(AnimationType.FLY_IN_FROM_ABOVE),
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 21,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.END_NOVEL_EVENT),
                waitForUserConfirmation = false
            }
        };
    }
}
