using System.Collections.Generic;

public class BankAccountOpeningNovel : VisualNovel
{
    public BankAccountOpeningNovel()
    {
        id = -5;
        title = "Bank Kontoeröffnung";
        description = "Du hast eine Einladung zu einem Bankgespräch erhalten, um mehr " +
            "Informationen über die Eröffnung eines Bankkontos zu erhalten und dieses darauf " +
            "zu beantragen.";
        image = 0;
        nameOfMainCharacter = "Lea";
        feedback = "";
        context = "Es ist das Gespräch einer Gründerin, Lea, mit einem Bank-Mitarbeiter. Es geht um die Eröffnung eines Bank-Kontos. Die Gründerin hat sich gut vorbereitet und hofft sich mit der Bank vertragseinig zu werden.";
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
                text = "Du hast eine Einladung zu einem Bankgespräch erhalten, " +
                "um mehr Informationen über die Eröffnung eines Bankkontos zu erhalten " +
                "und dieses darauf zu beantragen."
            },

            new VisualNovelEvent()
            {
                id = 4,
                nextId = 5,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Willkommen. Haben Sie alle Unterlagen bei sich?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 5,
                nextId = 6,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ja. Ab Seite 26 finden Sie meine Finanzplanung.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 6,
                nextId = 7,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Vielen Dank.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 7,
                nextId = 14,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Nach einer kurzen Überprüfung habe ich das Gefühl, dass Ihre " +
                "Geschäftsidee in eine Risikobranche fällt, was wir nur ungern unterstützen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            // new VisualNovelEvent()
            // {
            //     id = 8,
            //     nextId = 9,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_OPINION_FEEDBACK_EVENT),
            //     waitForUserConfirmation = false
            // },

            // new VisualNovelEvent()
            // {
            //     id = 9,
            //     nextId = 10,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
            //     waitForUserConfirmation = false,
            //     opinionChoiceNumber = 1,
            //     text = "Es ist in Ordnung, jetzt nervös zu sein. " +
            //     "Das Geschäftskonto ist ein wesentlicher Teil für die Geschäftsgründung."
            // },

            // new VisualNovelEvent()
            // {
            //     id = 10,
            //     nextId = 11,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
            //     waitForUserConfirmation = false,
            //     opinionChoiceNumber = 2,
            //     text = "Jetzt ängstlich zu sein ist in Ordnung. Es scheint, als müsstest du jetzt deine " +
            //     "Geschäftsidee verteidigen, um ein Firmenkonto eröffnen zu können."
            // },

            // new VisualNovelEvent()
            // {
            //     id = 11,
            //     nextId = 12,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
            //     waitForUserConfirmation = false,
            //     opinionChoiceNumber = 3,
            //     text = ""
            // },

            // new VisualNovelEvent()
            // {
            //     id = 12,
            //     nextId = 13,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
            //     waitForUserConfirmation = false,
            //     opinionChoiceNumber = 4,
            //     text = "Jetzt verärgert zu sein ist verständlich. Ein Geschäftskonto ist wichtig für die " +
            //     "Firmengründung und eine Ablehnung wäre ein herber Rückschlag."
            // },

            // new VisualNovelEvent()
            // {
            //     id = 13,
            //     nextId = 14,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ASK_FOR_OPINION_EVENT),
            //     waitForUserConfirmation = true,
            // },

            new VisualNovelEvent()
            {
                id = 14,
                nextId = 15,
                onChoice = 17,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Meine Marktrecherche hat ergeben, dass es genug Kunden gibt und ich " +
                "regelmäßige Einnahmen erzielen kann. Das können Sie alles auf Seite 14 nachlesen."
            },

            new VisualNovelEvent()
            {
                id = 15,
                nextId = 16,
                onChoice = 17,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Bitte lesen Sie sich den Plan aufmerksamer durch. Dann werden Sie " +
                "erkennen, dass es sich nicht um eine Risikobranche handelt."
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
                nextId = 18,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "In dem Fall werde ich den Businessplan noch einmal prüfen. " +
                "Allerdings wird das etwas dauern.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 18,
                nextId = 19,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.CHARAKTER_EXIT_EVENT),
                waitForUserConfirmation = false,
                name = "Herr Müller",
                animationType = AnimationTypeHelper.ToInt(AnimationType.FLY_IN_FROM_ABOVE),
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 19,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.END_NOVEL_EVENT),
                waitForUserConfirmation = false
            }
        };
    }
}