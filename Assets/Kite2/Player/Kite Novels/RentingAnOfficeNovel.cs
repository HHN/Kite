using System.Collections.Generic;

public class RentingAnOfficeNovel : VisualNovel
{
    public RentingAnOfficeNovel()
    {
        title = "Anmietung eines Büros";
        description = "Du hast heute einen Termin für die Besichtigung von Büroräumen.";
        image = 4;
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
                name = "Herr Müller",
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
                text = "Du hast heute einen Termin für die Besichtigung von Büroräumen."
            },

            new VisualNovelEvent()
            {
                id = 4,
                nextId = 5,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Guten Tag. Dies sind also die zur Verfügung stehenden Räumlichkeiten. " +
                "Welche Art von Unternehmen möchten Sie den gründen?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 5,
                nextId = 6,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich möchte ein Technologieunternehmen gründen, das sich auf die Entwicklung " +
                "nachhaltiger und personalisierter Lösungen in den Bereichen Energie, Mobilität und " +
                "Digitalisierung konzentriert.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 6,
                nextId = 7,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Oh, wirklich? Wie in der Anzeige geschrieben liegt der Mietpreis kalt bei " +
                "8,11 € pro Quadratmeter. Sind Sie sicher, diesen Betrag monatlich über längere " +
                "Zeit bezahlen zu können?",
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
                text = "Es ist in Ordnung, jetzt nervös zu sein. Räumlichkeiten für sein Unternehmen anzumieten ist ein weiterer wichtiger Schritt in die Verwirklichung seiner Pläne."
            },

            new VisualNovelEvent()
            {
                id = 9,
                nextId = 10,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
                waitForUserConfirmation = false,
                opinionChoiceNumber = 2,
                text = "Jetzt ängstlich zu sein ist in Ordnung. Du stehst kurz davor, einen " +
                "weiteren Schritt in die Selbstständigkeit zu beschreiten und von deiner Antwort " +
                "kann abhängen, ob du die Bürofläche bekommen wirst."
            },

            new VisualNovelEvent()
            {
                id = 10,
                nextId = 11,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
                waitForUserConfirmation = false,
                opinionChoiceNumber = 3,
                text = "Du stehst kurz davor, den nächsten Schritt zur Verwirklichung deiner Pläne zu " +
                "tätigen und zu wissen, dass man diese Frage optimistisch beantworten kann ist ermutigend."
            },

            new VisualNovelEvent()
            {
                id = 11,
                nextId = 12,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
                waitForUserConfirmation = false,
                opinionChoiceNumber = 4,
                text = "Es kann verärgern, dass einem unterstellt wird, dass das eigene Unternehmen " +
                "nicht lange genug überleben wird, um die Miete konstant zahlen zu können."
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
                onChoice = 17,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Natürlich! Meine Marktrecherchen ergeben, dass es eine hohe Nachfrage " +
                "gibt und ich habe für die nächsten drei Jahre einen Finanzplan erstellt, der " +
                "genug Einkommen prognostiziert."
            },

            new VisualNovelEvent()
            {
                id = 14,
                nextId = 15,
                onChoice = 19,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich denke schon. Zumindest haben das meine Recherchen ergeben."
            },

            new VisualNovelEvent()
            {
                id = 15,
                nextId = 16,
                onChoice = 18,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich sollte genügend Einkommen haben, " +
                "außerdem habe ich für die nächsten Monate noch eine Gründerförderung."
            },

            new VisualNovelEvent()
            {
                id = 16,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT),
                waitForUserConfirmation = true
            },

            new VisualNovelEvent()
            {
                id = 17,
                nextId = 20,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Wenn Sie das sagen. Wenn Sie mir eine Mieterselbstauskunft zuschicken, " +
                "setze ich Sie auf die Liste der positiven Bewerber*innen. In ein paar Tagen werde " +
                "ich Ihnen Bescheid geben, ob Sie die Räumlichkeiten haben können.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 18,
                nextId = 20,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Das hört sich gut an. Ich bräuchte von Ihnen noch eine Mieterselbstauskunft.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 19,
                nextId = 20,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ich bin mir nicht sichr, ob das ausreicht. " +
                "Es gibt noch andere Bewerber*innen, denen ich die Räumlichkeiten eher vermieten möchte",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 20,
                nextId = 21,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.CHARAKTER_EXIT_EVENT),
                waitForUserConfirmation = false,
                name = "Herr Müller",
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