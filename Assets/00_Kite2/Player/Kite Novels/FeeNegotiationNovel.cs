using System.Collections.Generic;

public class FeeNegotiationNovel : VisualNovel
{
    public FeeNegotiationNovel()
    {
        id = -11;
        title = "Honorarverhandlung mit Kundin";
        description = "Du bist in Kontakt mit einem Kunden gekommen. Nachdem geklärt wurde, dass deine angebotenen Leistungen zu den Anforderungen deines Gegenübers passen, geht es nun um die Honorarverhandlung.";
        context = "Es ist das Gespräch einer Gründerin, Lea, mit einer Kundin. Es geht um die Verhandlung eines Preises. Die Gründerin ist bereit mit ihrem Unternehmen die gewünschte Leistung zu erbringen und hofft auf die Einigung auf eine angemessene Entlohnung. ";
        image = 9;
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
                text = "Du bist in Kontakt mit einem Kunden gekommen. Nachdem geklärt wurde, dass " +
                "deine angebotenen Leistungen zu den Anforderungen deines Gegenübers passen, geht " +
                "es nun um die Honorarverhandlung."
            },

            new VisualNovelEvent()
            {
                id = 4,
                nextId = 5,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Winkler",
                text = "Es freut uns, dass Sie uns unterstützen können. " +
                "Nun stellt sich uns natürlich die Frage, wie viel Sie verlangen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 5,
                nextId = 6,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Über die Dauer der Zeit und der mit dem daraus entstehenden Aufwand würde " +
                "ich 20.000,00 Euro in Rechnung stellen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 6,
                nextId = 13,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Winkler",
                text = "Ist das nicht etwas viel verlangt für die Arbeit? " +
                "Geht das nicht auch günstiger?",
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
            //     text = "Dieses Gefühl ist vollkommen normal. Du weißt, dass dieses Gespräch darüber " +
            //     "entscheidet, wie viel du für deine Arbeit bekommst oder ob der*die Auftraggeber*in " +
            //     "überhaupt zusagt und das kann einen nervös machen."
            // },

            // new VisualNovelEvent()
            // {
            //     id = 9,
            //     nextId = 10,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
            //     waitForUserConfirmation = false,
            //     opinionChoiceNumber = 2,
            //     text = "In dieser Situation ängstlich zu sein ist verständlich. Du musst jetzt schließlich " +
            //     "deinen verlangten Preis rechtfertigen und weißt nicht, wie das von deinem Gegenüber " +
            //     "aufgenommen wird."
            // },

            // new VisualNovelEvent()
            // {
            //     id = 10,
            //     nextId = 11,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
            //     waitForUserConfirmation = false,
            //     opinionChoiceNumber = 3,
            //     text = "Du weißt was deine Arbeit wert ist und das ist sehr wichtig. Den verlangten Preis " +
            //     "hast du nicht einfach so gewählt und das darf dein Gegenüber ruhig wissen."
            // },

            // new VisualNovelEvent()
            // {
            //     id = 11,
            //     nextId = 12,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
            //     waitForUserConfirmation = false,
            //     opinionChoiceNumber = 4,
            //     text = "Zu hören, dass deine Arbeit deinem Gegenüber nicht so viel wert ist, " +
            //     "kann einen durchaus verärgern."
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
                text = "Ich kann den Preis um 5 % senken, aber mehr geht nicht."
            },

            new VisualNovelEvent()
            {
                id = 14,
                nextId = 15,
                onChoice = 18,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Die Höhe meiner Preise orientieren sich am Aufwand und sind fair berechnet."
            },

            new VisualNovelEvent()
            {
                id = 15,
                nextId = 16,
                onChoice = 19,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Wenn Ihnen der Preis zu hoch ist, " +
                "könnten wir Komponenten aus dem Angebot streichen."
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
                nextId = 19001,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Winkler",
                text = "Wenn das so ist, werde ich den Auftrag gerne an Sie vergeben. " +
                "Schicken Sie mir bitte ein schriftliches Angebot, dann werde ich entsprechend " +
                "einen Vertrag aufsetzen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 18,
                nextId = 19001,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Winkler",
                text = "Hmm, da muss ich erst nochmal Rücksprache halten. Bitte schicken Sie mir " +
                "ihr Angebot schriftlich zu, damit ich das abklären kann. Die Tage werde ich mich " +
                "bei Ihnen melden, zu welcher Entscheidung es kam.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 19,
                nextId = 19001,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Winkler",
                text = "Hmm, da muss ich erst nochmal Rücksprache halten. Bitte schicken Sie mir " +
                "ihr Angebot schriftlich zu, damit ich das abklären kann. Die Tage werde ich mich " +
                "bei Ihnen melden, zu welcher Entscheidung es kam.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 19001,
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
                name = "Frau Winkler",
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