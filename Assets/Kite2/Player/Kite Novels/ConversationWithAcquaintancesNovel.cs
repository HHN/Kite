using System.Collections.Generic;

public class ConversationWithAcquaintancesNovel : VisualNovel
{
    public ConversationWithAcquaintancesNovel()
    {
        id = -9;
        title = "Gespräch mit Bekannten";
        description = "Du triffst dich mit einem*einer Bekannten, den*die du seit ein paar Jahren " +
            "nicht mehr gesehen hast, in einem kleinen Café. Er*Sie hat dir soeben erzählt, wo " +
            "er*sie zurzeit beruflich steht. Nun bist du an der Reihe.";
        image = 8;
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
                backgroundSpriteId = 0
            },

            new VisualNovelEvent()
            {
                id = 2,
                nextId = 3,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.CHARAKTER_JOIN_EVENT),
                waitForUserConfirmation = true,
                name = "Lisa",
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
                text = "Du triffst dich mit einem*einer Bekannten, den*die du seit ein paar Jahren " +
                "nicht mehr gesehen hast, in einem kleinen Café. Er*Sie hat dir soeben erzählt, wo " +
                "er*sie zurzeit beruflich steht. Nun bist du an der Reihe."
            },

            new VisualNovelEvent()
            {
                id = 4,
                nextId = 5,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lisa",
                text = "Und letztes Jahr wurde ich befördert. Wie läuft es bei dir?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 5,
                nextId = 6,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich bin gerade dabei mich selbstständig zu machen und ein Unternehmen zu gründen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 6,
                nextId = 7,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lisa",
                text = "Wirklich? In welcher Branche?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 7,
                nextId = 8,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ja, genau! Ich habe mich dazu entschieden, meinen eigenen Weg zu gehen und " +
                "in der Tech-Branche Fuß zu fassen. Meine Firma spezialisiert sich auf die " +
                "Entwicklung von KI-gestützter Software zur Verbesserung von Geschäftsprozessen. " +
                "Durch den Einsatz künstlicher Intelligenz wollen wir Unternehmen dabei helfen, " +
                "effizienter zu arbeiten und datengesteuerte Entscheidungen zu treffen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 8,
                nextId = 9,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lisa",
                text = "Ist das als Frau nicht ziemlich schwer?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 9,
                nextId = 10,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_OPINION_FEEDBACK_EVENT),
                waitForUserConfirmation = false
            },

            new VisualNovelEvent()
            {
                id = 10,
                nextId = 11,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
                waitForUserConfirmation = false,
                opinionChoiceNumber = 1,
                text = "Da du nicht vorhersehen kannst, wie die Frage von deinem Gegenüber gemeint ist " +
                "und wie es auf deine Antwort reagieren wird, ist es verständlich nervös zu sein."
            },

            new VisualNovelEvent()
            {
                id = 11,
                nextId = 12,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
                waitForUserConfirmation = false,
                opinionChoiceNumber = 2,
                text = ""
            },

            new VisualNovelEvent()
            {
                id = 12,
                nextId = 13,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
                waitForUserConfirmation = false,
                opinionChoiceNumber = 3,
                text = "Du hast jetzt die Möglichkeit, deinem Gegenüber zu zeigen, dass du die " +
                "Gründung eines Unternehmens stemmen kannst und dies nichts mit deinem Geschlecht " +
                "zu tun hat. Dies kann ermutigen."
            },

            new VisualNovelEvent()
            {
                id = 13,
                nextId = 14,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
                waitForUserConfirmation = false,
                opinionChoiceNumber = 4,
                text = "Es ist verständlich, verärgert zu sein. Deine Fertigkeiten werden auf dein " +
                "Geschlecht beschränkt und dir unterstellt, dass die Gründung für dich schwer sein " +
                "muss nur weil du eine Frau bist."
            },

            new VisualNovelEvent()
            {
                id = 14,
                nextId = 15,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ASK_FOR_OPINION_EVENT),
                waitForUserConfirmation = true,
            },

            new VisualNovelEvent()
            {
                id = 15,
                nextId = 16,
                onChoice = 19,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Nein, überhaupt nicht."
            },

            new VisualNovelEvent()
            {
                id = 16,
                nextId = 17,
                onChoice = 20,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Warum sollte es? Ich als Frau kann den gleichen Arbeitsaufwand aufbringen wie ein Mann."
            },

            new VisualNovelEvent()
            {
                id = 17,
                nextId = 18,
                onChoice = 21,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Schwieriger ist es nicht. Man muss nur leider immer wieder damit rechnen, " +
                "dass man einem nicht so viel zutraut wie einem Mann und man deswegen immer wieder " +
                "aufs neue seine Kompetenzen aufzeigen muss."
            },

            new VisualNovelEvent()
            {
                id = 18,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT),
                waitForUserConfirmation = true
            },

            new VisualNovelEvent()
            {
                id = 19,
                nextId = 22,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lisa",
                text = "Ah, ich verstehe.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 20,
                nextId = 22,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lisa",
                text = "Selbstverständlich, tut mir leid. Ich wollte eigentlich einfach nur wissen, " +
                "ob so eine Gründung nicht im allgemeinen schwierig und zeitaufwendig ist.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 21,
                nextId = 22,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lisa",
                text = "Das klingt aber schon irgendwie als wäre es für Frauen anstrengender, " +
                "wenn sie sich ständig gegen Sexismus wehren müssen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 22,
                nextId = 23,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.CHARAKTER_EXIT_EVENT),
                waitForUserConfirmation = false,
                name = "Lisa",
                animationType = AnimationTypeHelper.ToInt(AnimationType.FLY_IN_FROM_ABOVE),
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 23,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.END_NOVEL_EVENT),
                waitForUserConfirmation = false
            }
        };
    }
}