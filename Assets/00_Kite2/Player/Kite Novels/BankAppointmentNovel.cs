using System.Collections.Generic;

public class BankAppointmentNovel : VisualNovel
{
    public BankAppointmentNovel()
    {
        id = -10;
        title = "Banktermin zur Kreditvergabe";
        description = "Du hast eine Einladung zu einem Bankgespräch erhalten, um mehr " +
            "Informationen über einen Kredit zu erhalten und diesen darauf zu beantragen.";
        image = 4;
        nameOfMainCharacter = "Lea";
        context = "Es ist das Gespräch einer Gründerin, Lea, mit einem Bank-Mitarbeiter. Es geht um einen Kredit. Die Gründerin hat sich gut vorbereitet und hofft Informationen über den Kredit zu erhalten.";
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
                backgroundSpriteId = 0
            },

            new VisualNovelEvent()
            {
                id = 2,
                nextId = 3,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.CHARAKTER_JOIN_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Winkler",
                animationType = AnimationTypeHelper.ToInt(AnimationType.FLY_IN_FROM_ABOVE),
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
                xPosition = 0,
                yPosition = -6,
                skinSpriteId = 0,
                clotheSpriteId = 0,
                hairSpriteId = 0,
                faceSpriteId = 0
            },

            new VisualNovelEvent()
            {
                id = 3,
                nextId = 4,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Intro",
                text = "Du hast eine Einladung zu einem Bankgespräch erhalten, um mehr " +
                "Informationen über einen Kredit zu erhalten und diesen darauf zu beantragen."
            },

            new VisualNovelEvent()
            {
                id = 4,
                nextId = 5,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Winkler",
                text = "Willkommen. In welcher Höhe möchten Sie denn den Kredit beantragen?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 5,
                nextId = 6,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich bräuchte einen Kredit in Höhe von 20.000,00 Euro.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 6,
                nextId = 13,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Winkler",
                text = "Hmm... Das ist ein ziemlich hoher Kredit. Sind Sie sicher, " +
                "den auch wieder zurückzahlen zu können?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            // new VisualNovelEvent()
            // {
            //     id = 7,
            //     nextId = 8,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_OPINION_FEEDBACK_EVENT),
            //     waitForUserConfirmation = false
            // },

            // new VisualNovelEvent()
            // {
            //     id = 8,
            //     nextId = 9,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
            //     waitForUserConfirmation = false,
            //     opinionChoiceNumber = 1,
            //     text = "Du darfst in dieser Situation nervös sein. " +
            //     "Der Kredit ist wichtig und der Ausgang des Gesprächs unklar."
            // },

            // new VisualNovelEvent()
            // {
            //     id = 9,
            //     nextId = 10,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
            //     waitForUserConfirmation = false,
            //     opinionChoiceNumber = 2,
            //     text = "Jetzt ängstlich zu sein ist normal. Du brauchst den Kredit, um mit deinem " +
            //     "Unternehmen durchstarten zu können und musst nun darlegen, wie du ihn zurückzahlen möchtest."
            // },

            // new VisualNovelEvent()
            // {
            //     id = 10,
            //     nextId = 11,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
            //     waitForUserConfirmation = false,
            //     opinionChoiceNumber = 3,
            //     text = ""
            // },

            // new VisualNovelEvent()
            // {
            //     id = 11,
            //     nextId = 12,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
            //     waitForUserConfirmation = false,
            //     opinionChoiceNumber = 4,
            //     text = "Es ist verständlich, verärgert zu sein. Du brauchst den Kredit und dass man " +
            //     "dir unterstellt, dass du ihn nicht zurückzahlen kannst, ist nervig."
            // },

            // new VisualNovelEvent()
            // {
            //     id = 12,
            //     nextId = 13,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ASK_FOR_OPINION_EVENT),
            //     waitForUserConfirmation = true,
            // },

            new VisualNovelEvent()
            {
                id = 13,
                nextId = 14,
                onChoice = 16,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Natürlich kann ich ihn zurückzahlen. Sehen Sie, hier in meinem Businessplan " +
                "habe ich bereits damit kalkuliert und habe genügend Einnahmen, um die Schulden zu tilgen."
            },

            new VisualNovelEvent()
            {
                id = 14,
                nextId = 15,
                onChoice = 16,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich wäre nicht hier und hätte mit Ihnen den Termin ausgemacht, wenn ich mir " +
                "nicht sicher wäre, den Kredit zurückzahlen zu können."
            },

            new VisualNovelEvent()
            {
                id = 15,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT),
                waitForUserConfirmation = true
            },

            new VisualNovelEvent()
            {
                id = 16,
                nextId = 17,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Winkler",
                text = "Könnten Sie mir ihren Busnessplan da lassen? Dann werde ich das überprüfen. " +
                "Ich werde Ihnen dann meine Entscheidung in einer Mail mitteilen und je nachdem " +
                "können wir noch einmal einen Termin ausmachen, um alles formelle zu klären.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 17,
                nextId = 18,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.CHARAKTER_EXIT_EVENT),
                waitForUserConfirmation = false,
                name = "Frau Winkler",
                animationType = AnimationTypeHelper.ToInt(AnimationType.FLY_IN_FROM_ABOVE),
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 18,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.END_NOVEL_EVENT),
                waitForUserConfirmation = false
            }
        };
    }
}