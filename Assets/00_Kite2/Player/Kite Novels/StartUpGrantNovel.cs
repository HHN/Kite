using System.Collections.Generic;

public class StartUpGrantNovel : VisualNovel
{
    public StartUpGrantNovel()
    {
        id = -8;
        title = "Gründerzuschuss";
        description = "Du bist heute bei deiner örtlichen Agentur für Arbeit, um einen Gründerzuschuss zu beantragen.";
        image = 7;
        nameOfMainCharacter = "Lea";
        feedback = "";
        context = "Es ist das Gespräch einer Gründerin, Lea, mit einem Mitarbeiter der Agentur für Arbeit. Es geht um die Beantragung eines Gründerzuschusses. Die Gründerin hat sich gut vorbereitet und hofft den Gründerzuschuss zu bekommen. ";
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
                name = "Herr Müller",
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
                text = "Du bist heute bei deiner örtlichen Agentur für Arbeit, um einen Gründerzuschuss zu beantragen."
            },

            new VisualNovelEvent()
            {
                id = 4,
                nextId = 5,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Guten Tag. Es freut mich, dass Sie heute vorbeikommen konnten. " +
                "Haben Sie alle Unterlagen dabei?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 5,
                nextId = 6,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ja. Ich habe hier meine Zeugnisse von IHK-Kursen in den Bereichen " +
                "Existenzgründung und Social Media-Marketing und eine Bewertung meines Businessplans.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 6,
                nextId = 13,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Nur diese Zwei? Was ist mit den Bereichen Finanzwesen und Buchhaltung? " +
                "Können Sie überhaupt mit Zahlen umgehen?",
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
            //     text = "Es geht um einen Gründerzuschuss, der dir beim Aufbau deines Unternehmens " +
            //     "helfen kann. Jetzt nervös zu sein ist vollkommen verständlich."
            // },

            // new VisualNovelEvent()
            // {
            //     id = 9,
            //     nextId = 10,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
            //     waitForUserConfirmation = false,
            //     opinionChoiceNumber = 2,
            //     text = "Ängstlich zu sein ist in Ordnung. Dein Gegenüber fordert von dir Unterlagen, die " +
            //     "du möglicherweise nicht hast und du weißt nicht, wie er darauf reagieren wird."
            // },

            // new VisualNovelEvent()
            // {
            //     id = 10,
            //     nextId = 11,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
            //     waitForUserConfirmation = false,
            //     opinionChoiceNumber = 3,
            //     text = "Du weißt, dass du die Kompetenzen besitzt und wie du sie beweisen kannst. " +
            //     "Das ist sehr ermutigend."
            // },

            // new VisualNovelEvent()
            // {
            //     id = 11,
            //     nextId = 12,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
            //     waitForUserConfirmation = false,
            //     opinionChoiceNumber = 4,
            //     text = "Dein Gegenüber scheint dir vorzuwerfen, dass du nicht mit Zahlen umgehen kannst. " +
            //     "Das kann verärgern."
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
                onChoice = 17,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Nein, in diesem Bereich habe ich kein Zeugnis."
            },

            new VisualNovelEvent()
            {
                id = 14,
                nextId = 15,
                onChoice = 19,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich hatte Finanzwesen in der Schule und mein altes Wissen selbst " +
                "aufgefrischt. Allerdings habe ich meine Schulzeugnisse nicht dabei."
            },

            new VisualNovelEvent()
            {
                id = 15,
                nextId = 16,
                onChoice = 18,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Finanzwesen und Buchhaltung habe ich mir selbst beigebracht, während ich " +
                "den Businessplan geschrieben habe und die Bewertung für den Finanzplan ist sehr " +
                "gut ausgefallen. Außerdem habe ich vor, baldmöglichst eine Person mit Expertise " +
                "in diesem Bereich einzustellen."
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
                nextId = 20001,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Wir können gerne den Antrag stellen, aber ich kann nicht sagen, " +
                "ob er auch genehmigt wird.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 18,
                nextId = 20001,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Selbst erlerntes Wissen ist nicht aussagekräftig, " +
                "genau wie die Bewertung des Businessplans.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 19,
                nextId = 20001,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Wenn Sie das Zeugnis nachreichen, wird die Beantragung problemlos sein.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 20001,
                nextId = 20,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.PLAY_SOUND_EVENT),
                waitForUserConfirmation = false,
                audioClipToPlay = SoundEnumHelper.ToInt(SoundsEnum.LEAVE_SCENE)
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