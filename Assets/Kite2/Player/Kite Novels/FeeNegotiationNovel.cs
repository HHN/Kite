using System.Collections.Generic;

public class FeeNegotiationNovel : VisualNovel
{
    public FeeNegotiationNovel()
    {
        title = "Honrarverhandlung mit Kundin";
        description = "Du bist in Kontakt mit einem Kunden gekommen. Nachdem geklärt wurde, dass " +
            "deine angebotenen Leistungen zu den Anforderungen deines Gegenübers passen, geht es " +
            "nun um die Honorarverhandlung.";
        image = 6;
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
                name = "Frau Winkler",
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
                nextId = 7,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Winkler",
                text = "Ist das nicht etwas viel verlangt für die Arbeit? " +
                "Geht das nicht auch günstiger?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 7,
                nextId = 8,
                onChoice = 11,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich kann den Preis um 5 % senken, aber mehr geht nicht."
            },

            new VisualNovelEvent()
            {
                id = 8,
                nextId = 9,
                onChoice = 12,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Die Höhe meiner Preise orientieren sich am Aufwand und sind fair berechnet."
            },

            new VisualNovelEvent()
            {
                id = 9,
                nextId = 10,
                onChoice = 13,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Wenn Ihnen der Preis zu hoch ist, " +
                "könnten wir Komponenten aus dem Angebot streichen."
            },

            new VisualNovelEvent()
            {
                id = 10,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT),
                waitForUserConfirmation = true
            },

            new VisualNovelEvent()
            {
                id = 11,
                nextId = 14,
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
                id = 12,
                nextId = 14,
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
                id = 13,
                nextId = 14,
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
                id = 14,
                nextId = 15,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.CHARAKTER_EXIT_EVENT),
                waitForUserConfirmation = false,
                name = "Frau Winkler",
                animationType = AnimationTypeHelper.ToInt(AnimationType.FLY_IN_FROM_ABOVE),
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 15,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.END_NOVEL_EVENT),
                waitForUserConfirmation = false
            }
        };
    }
}