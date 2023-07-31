using System.Collections.Generic;

public class InitialInterviewForGrantApplicationNovel : VisualNovel
{
    public InitialInterviewForGrantApplicationNovel()
    {
        title = "Erstgespräch Förderantrag";
        description = "Du wurdest zu einem Termin beim Arbeitsamt eingeladen, wo du dich mit einem Berater über deine Geschäftsidee unterhalten kannst und hoffentlich Informationen zu passenden Förderungen erhalten wirst.";
        image = 5;
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
                text = "Du wurdest zu einem Termin beim Arbeitsamt eingeladen, wo du dich mit einem Berater über deine Geschäftsidee unterhalten kannst und hoffentlich Informationen zu passenden Förderungen erhalten wirst."
            },

            new VisualNovelEvent()
            {
                id = 4,
                nextId = 5,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Guten Tag. Haben Sie gut hergefunden?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 5,
                nextId = 6,
                onChoice = 9,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ja, ich habe gut hergefunden. Es war nur sehr viel Verkehr."
            },

            new VisualNovelEvent()
            {
                id = 6,
                nextId = 7,
                onChoice = 10,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ja, ich habe gut hergefunden. Es war nur schwer einen Parkplatz zu finden."
            },

            new VisualNovelEvent()
            {
                id = 7,
                nextId = 8,
                onChoice = 11,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich habe das Gebäude zuerst nicht gesehen, aber dann doch noch die richtige Straße gefunden."
            },

            new VisualNovelEvent()
            {
                id = 8,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT),
                waitForUserConfirmation = true
            },

            new VisualNovelEvent()
            {
                id = 9,
                nextId = 12,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Mayer",
                text = "Das freut mich zu hören. Zu dieser Uhrzeit sind immer viele Leute unterwegs.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 10,
                nextId = 12,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Mayer",
                text = "Die Parksituation hier ist tatsächlich sehr schwierig. Die Angestellten beschweren sich auch ständig.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 11,
                nextId = 12,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Mayer",
                text = "Das kann jedem mal passieren. Die Straßenführung hier ist nicht die beste.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 12,
                nextId = 13,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Mayer",
                text = "Nehmen Sie doch bitte Platz!",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 13,
                nextId = 14,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Mayer",
                text = "Sie haben mir ja bereits Ihren vorläufigen Businessplan zugeschickt und er " +
                "liest sich auch sehr gut. Könnten Sie mir dennoch in 2-3 Sätzen Ihre Gründungsidee pitchen?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 14,
                nextId = 15,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Gerne.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 15,
                nextId = 16,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich entwickele eine KI-basierte, personalisierte Lernplattform, die die Art und Weise, " +
                "wie wir Wissen erwerben, revolutioniert, indem sie den Lernprozess auf die individuellen " +
                "Bedürfnisse und Lernstile eines jeden Nutzenden abstimmt.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 16,
                nextId = 17,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Das hört sich nach einer sehr innovativen Idee an. Und Sie haben in ihren " +
                "Papieren gut herausgearbeitet, wie Sie damit einen Profit erzielen können.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 17,
                nextId = 18,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ich halte eine Förderung für gut möglich, allerdings ist es natürlich den " +
                "Förderern wichtig, dass das Unternehmen über lange Zeit besteht und Geld einbringt.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 18,
                nextId = 19,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Da es in der Vergangenheit schon vorkam, dass ein Unternehmen wegen Kindern " +
                "aufgegeben werden musste, würde es mich interessieren, wie es bei Ihnen mit dem Kinderwunsch " +
                "bzw. mit dem Familienstand steht.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 19,
                nextId = 20,
                onChoice = 25,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich habe bereits Kinder, aber es ist für mich kein Problem ein Unternehmen " +
                "zu führen. Ich kann mich auf die Unterstützung meiner Familie verlassen!"
            },

            new VisualNovelEvent()
            {
                id = 20,
                nextId = 21,
                onChoice = 26,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich habe noch keine Kinder und dies ist für mich die " +
                "nächste Zeit auch nicht geplant."
            },

            new VisualNovelEvent()
            {
                id = 21,
                nextId = 22,
                onChoice = 27,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich habe zwar einen Kinderwunsch, dies aber bereits bei der " +
                "Unternehmensplanung mit bedacht. Es sollte mich also nicht behindern."
            },

            new VisualNovelEvent()
            {
                id = 22,
                nextId = 23,
                onChoice = 28,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich finde diese Frage ungerechtfertigt und unpassend. " +
                "Hätten Sie sie auch einem Mann gestellt?"
            },

            new VisualNovelEvent()
            {
                id = 23,
                nextId = 24,
                onChoice = 29,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich sehe keinen Grund, diese Frage beantworten zu müssen."
            },

            new VisualNovelEvent()
            {
                id = 24,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT),
                waitForUserConfirmation = true
            },

            new VisualNovelEvent()
            {
                id = 25,
                nextId = 30,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ich verstehe.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 26,
                nextId = 30,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Das ist gut zu hören.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 27,
                nextId = 30,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Sie sind sich sicher? Ein Kind kann viel Zeit beanspruchen. " +
                "Aber wenn Sie dies schon bedacht haben, wird es wohl passen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 28,
                nextId = 30,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Es tut mir leid, so habe ich die Frage nicht gemeint. " +
                "Natürlich müssen Sie die Fragen nicht beantworten.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 29,
                nextId = 30,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Tatsächlich ja. Wir hatten auch schon den FAll, dass ein Mann während" +
                "des Gründungsprozesses aussteigen musste, weil er Vater wurde und die Einnahmen aus dem" +
                "Unternehmen nicht für die Familie gereicht hätten.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 30,
                nextId = 31,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ich habe bereits ein paar Förderungen herausgesucht, " +
                "die zu Ihrer Gründungsidee passen würden.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 31,
                nextId = 32,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.CHARAKTER_EXIT_EVENT),
                waitForUserConfirmation = false,
                name = "Herr Müller",
                animationType = AnimationTypeHelper.ToInt(AnimationType.FLY_IN_FROM_ABOVE),
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 32,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.END_NOVEL_EVENT),
                waitForUserConfirmation = false
            }
        };
    }
}