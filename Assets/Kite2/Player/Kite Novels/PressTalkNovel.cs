using System.Collections.Generic;

public class PressTalkNovel : VisualNovel
{
    public PressTalkNovel()
    {
        title = "Pressegespräch";
        description = "Du befindest dich auf einer Veranstaltung, bei der Jungunternehmer*innen ihre " +
            "Geschäftsidee vor einem Publikum präsentieren können, um Rückmeldung zu der Idee zu " +
            "erhalten und zu networken. Nachdem du deine Geschäftsidee vor dem Publikum gepitcht hast, " +
            "stellst du dich an einen Tisch mit anderen Gästen, um mit ihnen zu reden.";
        image = 2;
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
                name = "Frau Mayer",
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
                text = "Du befindest dich auf einer Veranstaltung, bei der Jungunternehmer*innen ihre " +
                "Geschäftsidee vor einem Publikum präsentieren können, um Rückmeldung zu der Idee zu " +
                "erhalten und zu networken. Nachdem du deine Geschäftsidee vor dem Publikum gepitcht " +
                "hast, stellst du dich an einen Tisch mit anderen Gästen, um mit ihnen zu reden."
            },

            new VisualNovelEvent()
            {
                id = 4,
                nextId = 5,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Das war ein sehr interessanter Pitch. Ich bin von der lokalen Presse und es " +
                "würde mich freuen, wenn ich etwas über Ihre Geschäftsidee schreiben dürfte.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 5,
                nextId = 6,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Danke! Das wäre toll! Haben Sie eine spezielle Frage für Ihren Artikel?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 6,
                nextId = 7,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Ja. Wann kamen Sie auf die Idee für Ihr Unternehmen?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 7,
                nextId = 8,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Die Idee für mein Unternehmen kam mir während eines Sabbatjahres im Jahr 2021, " +
                "als ich die enorme Lücke in der Branche für nachhaltige, personalisierte Technologien " +
                "entdeckte.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 8,
                nextId = 9,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Das ist spannend. Eine Frau, die ein Unternehmen gründet, " +
                "das wird sicher viele Leser*innen interessieren.",
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
                text = "Nervosität während eines Interviews mit jemandem von der Presse ist verständlich. " +
                "Es bringt Aufmerksamkeit für das eigene Unternehmen und auch mögliche Kund*innen."
            },

            new VisualNovelEvent()
            {
                id = 11,
                nextId = 12,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
                waitForUserConfirmation = false,
                opinionChoiceNumber = 2,
                text = "Ängstlich zu sein ist in Ordnung. Du weißt nicht, was dein Gegenüber in dem " +
                "Artikel schreiben wird und welche Auswirkungen der Artikel haben wird."
            },

            new VisualNovelEvent()
            {
                id = 12,
                nextId = 13,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
                waitForUserConfirmation = false,
                opinionChoiceNumber = 3,
                text = "Jemand von der Zeitung ist auf dich aufmerksam geworden und das spornt an. " +
                "Der Artikel kann dir mehr Beachtung und Kund*innen bringen."
            },

            new VisualNovelEvent()
            {
                id = 13,
                nextId = 14,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
                waitForUserConfirmation = false,
                opinionChoiceNumber = 4,
                text = "An diesem Punkt des Gesprächs wirkt es, als würde dein Geschlecht für den Artikel " +
                "mehr zählen als deine Geschäftsidee. Dies kann durchaus verärgern."
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
                text = "Das wäre toll. Wann wird der Artikel erscheinen?"
            },

            new VisualNovelEvent()
            {
                id = 16,
                nextId = 17,
                onChoice = 20,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Mir ist es wichtig, dass meine Geschäftsidee in dem Artikel betont wird. " +
                "Ist das machbar?"
            },

            new VisualNovelEvent()
            {
                id = 17,
                nextId = 18,
                onChoice = 21,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Wäre es möglich, dass mein Geschlecht nicht allzu sehr in dem Artikel " +
                "hervorgehoben wird? Ich möchte für mein Unternehmen Aufmerksamkeit bekommen und " +
                "nicht weil ich eine Frau bin."
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
                name = "Frau Mayer",
                text = "Vielen Dank für die Beantwortung der Frage. " +
                "Der Artikel wird am Sonntag erscheinen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 20,
                nextId = 22,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Aber natürlich! Vielen Dank für den Hinweis. " +
                "Der Artikel wird am nächsten Sonntag erscheinen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 21,
                nextId = 22,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Das wird allerdings nicht so gut bei den Lesenden ankommen. Ich denke unter " +
                "diesen Umständen wird sich die Redaktion für eine andere Story entscheiden.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 22,
                nextId = 23,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Ich wünsche Ihnen viel Erfolg für Ihr Unternehmen! Auf Wiedersehen!",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 23,
                nextId = 24,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.CHARAKTER_EXIT_EVENT),
                waitForUserConfirmation = false,
                name = "Frau Mayer",
                animationType = AnimationTypeHelper.ToInt(AnimationType.FLY_IN_FROM_ABOVE),
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 24,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.END_NOVEL_EVENT),
                waitForUserConfirmation = false
            }
        };
    }
}