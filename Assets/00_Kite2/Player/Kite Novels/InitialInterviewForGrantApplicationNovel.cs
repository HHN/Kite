using System.Collections.Generic;
using UnityEngine;

public class InitialInterviewForGrantApplicationNovel : VisualNovel
{
    public InitialInterviewForGrantApplicationNovel()
    {
        id = -7;
        title = "Erstgespräch Förderantrag";
        description = "Du wurdest zu einem Termin beim Arbeitsamt eingeladen, wo du dich mit einem Berater über deine Geschäftsidee unterhalten kannst und hoffentlich Informationen zu passenden Förderungen erhalten wirst.";
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
                nextId = 300,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Intro",
                text = "Heute ist es so weit! In diesem Gebäude wirst du mit deiner Ansprechperson eine Unterhaltung über deine Geschäftsidee führen."
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
                onChoice = 179,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ja, ich habe gut hergefunden. Es war nur sehr viel Verkehr."
            },

            new VisualNovelEvent()
            {
                id = 6,
                nextId = 7,
                onChoice = 180,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ja, ich habe gut hergefunden. Es war nur schwer einen Parkplatz zu finden."
            },

            new VisualNovelEvent()
            {
                id = 7,
                nextId = 8,
                onChoice = 181,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich habe das Gebäude zuerst nicht gesehen, aber dann doch noch die richtige Straße gefunden."
            },

            new VisualNovelEvent()
            {
                id = 8,
                nextId = 400,
                onChoice = 182,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ja, ich habe gut hergefunden."
            },

            new VisualNovelEvent()
            {
                id = 9,
                nextId = 12,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Mayer",
                text = "Ja, zu dieser Uhrzeit sind immer viele Leute untnerwegs. Schön, dass Sie gut hergefunden haben.",
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
                text = "Das kann schonmal passieren. Die Straßenführung ist hier nicht die Beste.",
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
                "liest sich auch sehr gut. Könnten Sie mir dennoch Ihre Gründungsidee pitchen?",
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

            // TODO: Change this later or remove
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
                nextId = 301,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ich halte eine Förderung für gut möglich.",
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
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_OPINION_FEEDBACK_EVENT),
                waitForUserConfirmation = false
            },

            new VisualNovelEvent()
            {
                id = 20,
                nextId = 21,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
                waitForUserConfirmation = false,
                opinionChoiceNumber = 1,
                text = "In dieser Situation nervös zu sein, ist verständlich. Du musst schließlich deine " +
                "eigene Geschäftsidee erklären und weißt nicht, ob sie positiv aufgenommen wird."
            },

            new VisualNovelEvent()
            {
                id = 21,
                nextId = 22,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
                waitForUserConfirmation = false,
                opinionChoiceNumber = 2,
                text = "Dieses Gefühl ist vollkommen normal. Die Gründung eines Unternehmens ist ein großer " + 
                "Schritt und die Geschäftsidee einer fremden Person vorzustellen, ohne zu wissen, wie er*sie reagiert, ist nervenaufreibend."
            },

            new VisualNovelEvent()
            {
                id = 22,
                nextId = 23,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
                waitForUserConfirmation = false,
                opinionChoiceNumber = 3,
                text = "Deine Idee scheint überzeugt zu haben. Also ist ein ermutigtes Gefühl durchaus angemessen! " +
                "Du hast schließlich die Fakten deines Businessplans im Kopf und weißt, was du damit erreichen willst."
            },

            new VisualNovelEvent()
            {
                id = 23,
                nextId = 24,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
                waitForUserConfirmation = false,
                opinionChoiceNumber = 4,
                text = "Auch heutzutage werden Menschen manchmal bei Gesprächen zu einer Förderung immer noch mit Sexismus konfrontiert. " + 
                "Fragen wie diese hier beruhen teilweise auch unbewust auf der Annahme, dass Frauen weniger leisten können, wenn sie eine " + 
                "Familie haben oder mit ihren Geschäftsideen weniger Erfolg haben als Männer."
            },

            new VisualNovelEvent()
            {
                id = 24,
                nextId = 25,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ASK_FOR_OPINION_EVENT),
                waitForUserConfirmation = true,
            },

            new VisualNovelEvent()
            {
                id = 25,
                nextId = 26,
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
                id = 26,
                nextId = 27,
                onChoice = 183,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich habe bereits Kinder."
            },

            new VisualNovelEvent()
            {
                id = 27,
                nextId = 28,
                onChoice = 184,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich habe nicht vor Kinder zu haben."
            },

            new VisualNovelEvent()
            {
                id = 28,
                nextId = 29,
                onChoice = 185,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich habe einen Kinderwunsch."
            },

            new VisualNovelEvent()
            {
                id = 29,
                nextId = 30,
                onChoice = 186,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich finde diese Frage ungerechtfertigt."
            },

            new VisualNovelEvent()
            {
                id = 30,
                nextId = 400,
                onChoice = 187,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich sehe keinen Grund, diese Frage beantworten zu müssen."
            },
       
            new VisualNovelEvent()
            {
                id = 31,
                nextId = 32,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Aber es ist für mich kein Problem ein Unternehmen zu führen."
            },

            new VisualNovelEvent()
            {
                id = 32,
                nextId = Random.Range(33, 36),
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich kann mich auf die Unterstützung meiner Familie verlassen!"
            },

            new VisualNovelEvent()
            {
                id = 33,
                nextId = 36,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Das freut mich zu hören, dass Sie auf familiäre Unterstützung zählen können.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 34,
                nextId = 78,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Vielen Dank für Ihre Offenheit.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 35,
                nextId = 79,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Sind Sie sich sicher? Nicht, dass Sie sich doch überfordert fühlen mit der vielen Verantwortung.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 36,
                nextId = 37,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ja, ich bin auch sehr froh darüber, dass ich auf meine Familie zählen kann."
            },

            new VisualNovelEvent()
            {
                id = 37,
                nextId = 38,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Das ist schön zu hören. Bei der Gründung und Führung eines Unternehmens ist es " +
                "jedoch wichtig, alle Aspekte zu berücksichtigen, einschließlich möglicher Risiken.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 38,
                nextId = 39,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Da Sie bereits familiäre Verpflichtungen haben, wie haben Sie geplant, mit möglichen " +
                "unerwarteten Herausforderungen umzugehen und die Balance zwischen Familie und Unternehmen aufrechtzuerhalten?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 39,
                nextId = 40,
                onChoice = 188,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich habe da bereits einen Plan, wie ich damit umgehen werde."
            },

            new VisualNovelEvent()
            {
                id = 40,
                nextId = 41,
                onChoice = 189,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ehrlich gesagt, ist es frustrierend, dass meine familiäre Situation eine potenzielle Schwäche darstellt."
            },

            new VisualNovelEvent()
            {
                id = 41,
                nextId = 400,
                onChoice = 190,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Lassen Sie uns bitte auf die Stärken und Potenziale meiner Idee konzentrieren."
            },

            new VisualNovelEvent()
            {
                id = 42,
                nextId = Random.Range(43, 45),
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich habe intensiv recherchiert und meinen Geschäftsplan darauf ausgerichtet, mögliche Risiken zu minimieren."
            },

            new VisualNovelEvent()
            {
                id = 43,
                nextId = 45,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Das freut mich. Haben Sie denn noch Fragen oder Themen, die Sie besprechen möchten?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 44,
                nextId = 59,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ich bin mir nicht sicher, ob das ausreichend ist.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 45,
                nextId = 46,
                onChoice = 47,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte nach Informationen zu Förderungen fragen."
            },

            new VisualNovelEvent()
            {
                id = 46,
                nextId = 400,
                onChoice = 54,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte das Gespräch beenden."
            },

            new VisualNovelEvent()
            {
                id = 47,
                nextId = 48,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Da gibt es verschiedene Möglichkeiten, die wir besprechen können. " +
                "Abhängig von Ihrer Branche, Ihrem Geschäftsmodell und anderen Faktoren könnten staatliche Förderprogramme, Zuschüsse oder Darlehen relevant sein.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 48,
                nextId = 49,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ich habe bereits einige Förderungen für Sie herausgesucht. Schauen Sie diese bitte durch.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 49,
                nextId = 50,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Wir können dann in einem weiteren Gespräch die verschiedenen Optionen im Detail durchgehen, " +
                "um sicherzustellen, dass Sie die bestmögliche Unterstützung für Ihr Unternehmen erhalten.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 50,
                nextId = 51,
                onChoice = 52,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Das hört sich nach einer guten Idee an."
            },

            new VisualNovelEvent()
            {
                id = 51,
                nextId = 400,
                onChoice = 52,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Das können wir gerne so machen."
            },

            new VisualNovelEvent()
            {
                id = 52,
                nextId = 53,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Gut, melden Sie sich dann gerne, um einen weiteren Termin auszumachen. Ich wünsche Ihnen noch einen schönen Tag.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 53,
                nextId = 401,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Vielen Dank, das wünsche ich Ihnen auch. Auf Wiedersehen."
            },

            new VisualNovelEvent()
            {
                id = 54,
                nextId = 55,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Danke für Ihre Unterstützung. Im Moment habe ich jedoch keine weiteren Fragen."
            },

            new VisualNovelEvent()
            {
                id = 55,
                nextId = 56,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "In Ordnung. Ich wünsche Ihnen viel Erfolg bei Ihrer Unternehmensgründung. " +
                "Wenn Sie sich zu einem späteren Zeitpunkt erneut mit uns in Verbindung setzen möchten, stehen wir Ihnen gerne zur Verfügung.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 56,
                nextId = 57,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Das ist sehr nett von Ihnen. Ich werde darüber nachdenken und mich melden, wenn nötig."
            },

            new VisualNovelEvent()
            {
                id = 57,
                nextId = 58,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Das freut mich zu hören. Ich wünsche Ihnen einen schönen Tag und alles Gute für Ihre Pläne. Auf Wiedersehen!",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 58,
                nextId = 401,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Vielen Dank und auf Wiedersehen."
            },

            new VisualNovelEvent()
            {
                id = 59,
                nextId = 60,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Vielleicht möchte Ihr Partner bei dem nächsten Gespräch dabei sein?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 60,
                nextId = 61,
                onChoice = 63,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich treffe die Entscheidungen alleine."
            },

            new VisualNovelEvent()
            {
                id = 61,
                nextId = 62,
                onChoice = 63,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich habe eine Partnerin und treffe die Entscheidungen alleine."
            },

            new VisualNovelEvent()
            {
                id = 62,
                nextId = 400,
                onChoice = 67,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich überlege es mir."
            },

            new VisualNovelEvent()
            {
                id = 63,
                nextId = 64,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Oh ja, natürlich. Danke für die Klarstellung. Das war auch nur als Vorschlag gemeint. Haben Sie denn noch Fragen oder Themen, die Sie besprechen möchten?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.DEFEATED)
            },

            new VisualNovelEvent()
            {
                id = 64,
                nextId = 65,
                onChoice = 66,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte nach Informationen zu Förderungen fragen."
            },

            new VisualNovelEvent()
            {
                id = 65,
                nextId = 400,
                onChoice = 54,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte das Gespräch beenden."
            },

            new VisualNovelEvent()
            {
                id = 66,
                nextId = 47,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Haben Sie Informationen zu möglichen Förderungen, die zu mir passen könnten?"
            },

            new VisualNovelEvent()
            {
                id = 67,
                nextId = 47,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Aber haben Sie denn vielleicht bereits Informationen zu möglichen Förderungen, die zu mir passen könnten?"
            },

            new VisualNovelEvent()
            {
                id = 68,
                nextId = Random.Range(69, 71),
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Männer werden selten danach gefragt, wie sie Familie und Arbeit unter einen Hut bringen."
            },

            new VisualNovelEvent()
            {
                id = 69,
                nextId = 64,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ja, Sie haben Recht. Haben Sie denn noch Fragen oder Themen, die Sie besprechen möchten?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.DEFEATED)
            },

            new VisualNovelEvent()
            {
                id = 70,
                nextId = 71,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Nun ja, das ist leichter gesagt als getan. Frauen haben nun mal andere Prioritäten. " +
                "Ich meine, wie können Sie sicherstellen, dass Ihre Arbeit nicht darunter leidet?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 71,
                nextId = 72,
                onChoice = 73,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte darauf eingehen."
            },

            new VisualNovelEvent()
            {
                id = 72,
                nextId = 400,
                onChoice = 132,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte das Gespräch beenden."
            },

            new VisualNovelEvent()
            {
                id = 73,
                nextId = 74,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich habe sorgfältig darüber nachgedacht und Pläne entwickelt, um eine ausgewogene " +
                "Balance zwischen meinem Unternehmen und meiner Familie zu halten."
            },

            new VisualNovelEvent()
            {
                id = 74,
                nextId = 75,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich erwarte nicht, dass es einfach wird, aber ich bin zuversichtlich, dass ich Herausforderungen bewältigen kann."
            },

            new VisualNovelEvent()
            {
                id = 75,
                nextId = 76,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Nun, ich hoffe, dass Ihre Zuversicht gerechtfertigt ist. Es ist nur so, dass es in der " +
                "Geschäftswelt sehr anspruchsvoll sein kann, vor allem für Frauen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 76,
                nextId = 64,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Aber gut, wenn Sie denken, dass Sie es schaffen können. Haben Sie denn noch Fragen oder Themen, die Sie besprechen möchten?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 77,
                nextId = Random.Range(69, 71),
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Meine familiäre Situation beeinflusst nicht meine beruflichen Fähigkeiten. " +
                "Wenn es weitere Aspekte meiner Geschäftspläne gibt, über die Sie sprechen möchten, " +
                "stehe ich zur Verfügung. Aber lassen Sie uns bitte auf die Stärken und Potenziale meiner Idee konzentrieren."
            },

            new VisualNovelEvent()
            {
                id = 78,
                nextId = 59,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Es ist wichtig, dass Gründer*innen gut vorbereitet sind, um den langfristigen Erfolg ihres Unternehmens sicherzustellen. " +
                "Die Unterstützung Ihrer Familie könnte in der Tat eine wertvolle Ressource sein.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 79,
                nextId = 80,
                onChoice = 68,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ehrlich gesagt, ist es frustrierend, dass meine familiäre Situation eine potenzielle Schwäche darstellt."
            },

            new VisualNovelEvent()
            {
                id = 80,
                nextId = 400,
                onChoice = 81,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich habe bereits Überlegung getroffen und habe Strategien entwickelt, um damit umzugehen."
            },

            new VisualNovelEvent()
            {
                id = 81,
                nextId = Random.Range(43, 45),
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich habe jedoch meine Entscheidung nach reiflicher Überlegung getroffen und habe Strategien, um eine mögliche Überforderung zu vermeiden."
            },

            new VisualNovelEvent()
            {
                id = 82,
                nextId = 85,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Verstehe, vielen Dank für Ihre Offenheit. Es ist gut zu wissen, dass Sie bezüglich Ihrer Kinderwünsche klar sind. " +
                "Das gibt Ihnen sicherlich eine klare Ausrichtung, um sich auf Ihr Unternehmen zu konzentrieren.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 83,
                nextId = 85,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ich verstehe, vielen Dank für Ihre Offenheit.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 84,
                nextId = 100,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Das sagen Sie jetzt, aber die meisten Frauen kriegen ja dann ab einem gewissen " +
                "Alter doch Torschlusspanik. Schließlich tickt auch Ihre biologische Uhr.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 85,
                nextId = 86,
                onChoice = 87,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte mich nun meiner Geschäftsidee widmen."
            },

            new VisualNovelEvent()
            {
                id = 86,
                nextId = 400,
                onChoice = 88,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Dennoch finde ich solch eine Frage unangebracht."
            },

            new VisualNovelEvent()
            {
                id = 87,
                nextId = 60,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Selbstverständlich. Aber vielleicht möchte Ihr Partner bei dem nächsten Gespräch dabei sein?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 88,
                nextId = 89,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich verstehe, dass Vorbereitung wichtig ist, aber es ist auch entscheidend, " +
                "bewusst zu sein, wie solche Fragen auf unterschiedliche Geschlechter wirken können."
            },

            new VisualNovelEvent()
            {
                id = 89,
                nextId = 90,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich bin zuversichtlich in meine Fähigkeiten und möchte sicherstellen, " +
                "dass meine Geschäftsidee objektiv bewertet wird, unabhängig von Geschlechterstereotypen."
            },

            new VisualNovelEvent()
            {
                id = 90,
                nextId = Random.Range(91, 93),
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Können wir uns auf die geschäftlichen Aspekte meiner Idee konzentrieren?"
            },

            new VisualNovelEvent()
            {
                id = 92,
                nextId = 94,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Das sind aber berechtigte Sorgen und Fragen!",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 91,
                nextId = 93,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ich verstehe, was Sie meinen, und es tut mir leid, dass meine Frage unangemessen war.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 93,
                nextId = 64,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Sie haben recht, wir sollten sicherstellen, dass unsere Gespräche frei von jeglichem Geschlechterbias sind. " +
                "Haben Sie denn noch Fragen oder Themen, die Sie besprechen möchten?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 94,
                nextId = 95,
                onChoice = 96,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte lieber mit einer anderen Ansprechperson zusammenarbeiten."
            },

            new VisualNovelEvent()
            {
                id = 95,
                nextId = 400,
                onChoice = 99,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich frage lieber woanders nach Informationen zu Förderungen."
            },

            new VisualNovelEvent()
            {
                id = 96,
                nextId = 97,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Wenn meine Fähigkeiten infrage gestelllt werden, weil Sie Negatives mit Frauen " +
                "als Gründerinnen verbinden, fühle ich mich nicht angemessen unterstützt und respektiert. " +
                "Deshalb möchte ich das Gespräch beenden und in Zukunft mit einer anderen Ansprechperson zusammenarbeiten."
            },

            new VisualNovelEvent()
            {
                id = 97,
                nextId = 98,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ich verstehe. Wenn Sie Ihre Meinung ändern oder Fragen haben, stehe ich zur Verfügung. " +
                "Ich wünsche Ihnen noch einen schönen Tag. Auf Wiedersehen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.NONE)
            },

            new VisualNovelEvent()
            {
                id = 98,
                nextId = 401,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Auf Wiedersehen."
            },

            new VisualNovelEvent()
            {
                id = 99,
                nextId = 97,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich denke, es ist am besten, wenn ich meine Zeit und Energie in Gespräche investiere, " +
                "die sich auf die positiven Aspekte meiner Idee konzentrieren. Deswegen werde ich mich an anderer " +
                "Stelle nach möglichen Förderungen erkunden."
            },

            new VisualNovelEvent()
            {
                id = 100,
                nextId = 101,
                onChoice = 87,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte mich nun meiner Geschäftsidee widmen."
            },

            new VisualNovelEvent()
            {
                id = 101,
                nextId = 400,
                onChoice = 88,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "So ein Kommentar ist sexistisch."
            },

            new VisualNovelEvent()
            {
                id = 102,
                nextId = Random.Range(103, 106),
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Allerdings habe ich das auch bereits bei der Unternehmensplanung mitbedacht. Es sollte also kein Problem sein."
            },

            new VisualNovelEvent()
            {
                id = 103,
                nextId = 106,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Es freut mich zu hören, dass Sie bereits im Voraus überlegt haben, wie Sie die Herausforderungen des Unternehmertums mit Ihrem Kinderwunsch in Einklang bringen können.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 104,
                nextId = 178,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ich verstehe. Vielen Dank für Ihre Offenheit.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 105,
                nextId = 98,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Kinderwunsch und Unternehmensgründung sind oft schwer zu vereinbaren. " +
                "Ein Unternehmen zu führen erfordert viel Zeit und Engagement, und Kinder bringen zusätzliche Verantwortung mit sich.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.QUESTIONING)
            },

            new VisualNovelEvent()
            {
                id = 106,
                nextId = 107,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Eine durchdachte Planung ist ein wichtiger Schritt, um sicherzustellen, dass Sie sowohl persönliche als auch geschäftliche Ziele erreichen können.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 107,
                nextId = 108,
                onChoice = 42,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich habe da bereits einen Plan, wie ich damit umgehen werde."
            },

            new VisualNovelEvent()
            {
                id = 108,
                nextId = 109,
                onChoice = 110,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ehrlich gesagt, ist es frustrierend, dass mein Kinderwunsch eine potenzielle Schwäche darstellt."
            },

            new VisualNovelEvent()
            {
                id = 109,
                nextId = 400,
                onChoice = 110,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Lassen Sie uns bitte auf die Stärken und Potenziale meiner Idee konzentrieren."
            },

            new VisualNovelEvent()
            {
                id = 110,
                nextId = 111,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Sie müssen verstehen, dass unsere Bedenken darauf abzielen, sicherzustellen, " +
                "dass Sie in der Lage sind, sowohl Ihr Unternehmen erfolgreich zu führen als auch möglichen familiären Verpflichtungen gerecht zu werden.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 111,
                nextId = 112,
                onChoice = 114,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Hätten Sie diese Bedenken auch gegenüber einem Mann geäußert?"
            },

            new VisualNovelEvent()
            {
                id = 112,
                nextId = 113,
                onChoice = 42,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich bin vorbereitet."
            },

            new VisualNovelEvent()
            {
                id = 113,
                nextId = 400,
                onChoice = 122,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte mich auf die Stärken meiner Geschäftsidee konzentrieren."
            },

            new VisualNovelEvent()
            {
                id = 114,
                nextId = 115,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Selbstverständlich. Wir möchten nur, dass Sie auf mögliche Herausforderungen vorbereitet sind.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 116,
                nextId = 117,
                onChoice = 88,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte klarstellen, dass solche Fragen sexistisch sind."
            },

            new VisualNovelEvent()
            {
                id = 117,
                nextId = 118,
                onChoice = 119,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich gehe nicht darauf ein und frage nur noch nach möglichen Förderungen."
            },

            new VisualNovelEvent()
            {
                id = 118,
                nextId = 400,
                onChoice = 121,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte das Gespräch beenden."
            },

            new VisualNovelEvent()
            {
                id = 119,
                nextId = 47,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Haben Sie Informationen zu möglichen Förderungen, die zu mir passen könnten?"
            },

            // No 120

            new VisualNovelEvent()
            {
                id = 121,
                nextId = 55,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich möchte mich für Ihre Zeit bedanken. Ich werde Ihre Bedenken bei der weiteren Entwicklung meiner Geschäftspläne berücksichtigen."
            },

            new VisualNovelEvent()
            {
                id = 122,
                nextId = 123,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Meine Kinderwunsch beeinflusst nicht meine beruflichen Fähigkeiten. " +
                "Wenn es weitere Aspekte meiner Geschäftspläne gibt, über die Sie sprechen möchten, stehe ich zur Verfügung."
            },

            new VisualNovelEvent()
            {
                id = 123,
                nextId = Random.Range(124, 126),
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Aber lassen Sie uns bitte auf die Stärken und Potenziale meiner Idee konzentrieren."
            },

            new VisualNovelEvent()
            {
                id = 124,
                nextId = 66,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ja, Sie haben Recht. Haben Sie denn noch Fragen oder Themen, die Sie besprechen möchten?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 125,
                nextId = 126,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Es ist schön, so optimistisch zu sein, aber die praktische Umsetzung könnte sich " +
                "als ziemlich herausfordernd erweisen. Es ist wichtig, realistisch zu sein, wenn man eine Unternehmensgründung " +
                "in Erwägung zieht, besonders in Ihrer Situation.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.PROUD)
            },

            new VisualNovelEvent()
            {
                id = 126,
                nextId = 127,
                onChoice = 128,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte darauf eingehen."
            },

            new VisualNovelEvent()
            {
                id = 127,
                nextId = 400,
                onChoice = 92,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte das Gespräch beenden."
            },

            new VisualNovelEvent()
            {
                id = 128,
                nextId = 129,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich bin mir bewusst, dass die Balance zwischen Arbeit und Familie eine Herausforderung " +
                "sein kann, aber ich glaube, dass dies für alle Unternehmer*innen gilt, unabhängig vom Geschlecht."
            },

            new VisualNovelEvent()
            {
                id = 129,
                nextId = 130,
                onChoice = 66,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte nach Informationen zu Förderungen fragen."
            },

            new VisualNovelEvent()
            {
                id = 130,
                nextId = 131,
                onChoice = 121,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte das Gespräch beenden."
            },

            new VisualNovelEvent()
            {
                id = 131,
                nextId = 400,
                onChoice = 132,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte das Gespräch mit einer Konfrontation beenden."
            },

            new VisualNovelEvent()
            {
                id = 132,
                nextId = 92,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Da Sie anscheinend große Zweifel haben, dass ich als Frau ein Unternehmen führen " +
                "und gleichzeitig Familie haben kann, möchte ich das Gespräch an dieser Stelle lieber beenden."
            },

            new VisualNovelEvent()
            {
                id = 133,
                nextId = Random.Range(134, 137),
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Hätten Sie diese Frage auch einem Mann gestellt?"
            },

            new VisualNovelEvent()
            {
                id = 134,
                nextId = 137,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Was ist das für eine Unterstellung?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 135,
                nextId = 121,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Es tut mir leid. So habe ich die Frage nicht gemeint. Natürlich müssen Sie die Frage nicht beantworten.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.DEFEATED)
            },

            new VisualNovelEvent()
            {
                id = 136,
                nextId = 149,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Sie müssen verstehen, dass ich mir nur Gedanken mache.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.NONE)
            },

            new VisualNovelEvent()
            {
                id = 137,
                nextId = 138,
                onChoice = 139,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte deeskalierend darauf eingehen und den Sexismus erklären."
            },

            new VisualNovelEvent()
            {
                id = 138,
                nextId = 400,
                onChoice = 148,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte klartsellen, dass solche Fragen reflektiert werden sollen."
            },

            new VisualNovelEvent()
            {
                id = 139,
                nextId = Random.Range(140, 142),
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Es ist möglich, dass solche Fragen unabhängig vom Geschlecht gestellt werden. " +
                "Wir dürfen aber nicht vergessen, dass Geschlechterstereotype und -erwartungen immer noch existieren. " +
                "Fragen nach Familienplanung und Kinderwunsch neigen oft dazu, Frauen häufiger und auf eine andere Weise zu betreffen als Männer. "
            },

            new VisualNovelEvent()
            {
                id = 140,
                nextId = 64,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Entschuldigen Sie bitte, das war unangebracht. Sie haben völlig recht, Geschlecht sollte in dieser Frage keine Rolle spielen. " +
                "Haben Sie denn noch Fragen oder Themen, die Sie besprechen möchten?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.DEFEATED)
            },

            new VisualNovelEvent()
            {
                id = 141,
                nextId = 142,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Nun, Frauen sollten sich bewusst sein, dass sie oft nicht in der Lage sind, " +
                "Familie und Karriere erfolgreich zu managen. Das könnte Ihr Unternehmen gefährden.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 142,
                nextId = 143,
                onChoice = 144,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte klartsellen, dass solche Fragen reflektiert werden sollen."
            },

            new VisualNovelEvent()
            {
                id = 143,
                nextId = 400,
                onChoice = 146,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte klar sagen, dass das sexistisch ist."
            },

            new VisualNovelEvent()
            {
                id = 144,
                nextId = 145,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich schätze Ihre Sorge. Ich werde sicherstellen, dass ich alle Aspekte sorgfältig " +
                "berücksichtige, um erfolgreich zu sein. Aber ich denke, wir sollten uns auf das eigentliche Thema konzentrieren."
            },

            new VisualNovelEvent()
            {
                id = 145,
                nextId = 64,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Sicherlich. Es ist wichtig, gut vorbereitet zu sein. Haben Sie denn noch Fragen, die Sie besprechen möchten?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 146,
                nextId = 147,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ihre Bemerkungen sind beleidigend und sexistisch. Mein Familienstand sollte meine " +
                "Fähigkeiten als Unternehmerin nicht beeinflussen. Ich erwarte Respekt."
            },

            new VisualNovelEvent()
            {
                id = 147,
                nextId = 94,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Sehen Sie, Frauen werden oft überempfindlich, wenn es um solche Dinge geht. Das zeigt nur, dass meine Bedenken gerechtfertigt sind.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.PROUD)
            },

            new VisualNovelEvent()
            {
                id = 148,
                nextId = Random.Range(140, 142),
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Es ist eine Frage, die darauf abzielt sicherzustellen, dass wir in diesem Gespräch fair " +
                "und gleichberechtigt behandelt werden. Wenn Sie nicht bereit sind, diese Fragen zu reflektieren " +
                "und zu überdenken, dann frage ich mich, ob wir hier eine produktive Geschäftsbeziehung haben können."
            },

            new VisualNovelEvent()
            {
                id = 149,
                nextId = 150,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Wir hatten auch schon den Fall, dass ein Mann während des Gründungsprozesses aussteigen " +
                "musste, weil er Vater wurde und die Einnahmen aus dem Unternehmen nicht für die Familie gereicht hätte."
            },

            new VisualNovelEvent()
            {
                id = 150,
                nextId = 151,
                onChoice = 153,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Meine persönliche Situation sollte keinen Einfluss auf meine Fähigkeiten haben."
            },

            new VisualNovelEvent()
            {
                id = 151,
                nextId = 152,
                onChoice = 174,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Konzentrieren wir uns auf die Geschäftsidee an sich."
            },

            new VisualNovelEvent()
            {
                id = 152,
                nextId = 400,
                onChoice = 177,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Die Frage ist dennoch unangemessen."
            },

            new VisualNovelEvent()
            {
                id = 153,
                nextId = 154,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich verstehe Ihre Bedenken, aber ich bin entschlossen, mein Unternehmen " +
                "erfolgreich zu gründen und zu führen. Meine persönliche Situation sollte keinen Einfluss auf meine Fähigkeiten haben."
            },

            new VisualNovelEvent()
            {
                id = 154,
                nextId = 155,
                onChoice = 156,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte mich auf meine Geschäftsidee und mögliche Förderungen konzentrieren."
            },

            new VisualNovelEvent()
            {
                id = 155,
                nextId = 400,
                onChoice = 132,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte das Gespräch beenden."
            },

            new VisualNovelEvent()
            {
                id = 156,
                nextId = Random.Range(157, 159),
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Lassen Sie uns nun über die finanzielle Unterstützung sprechen, die ich für meine Gründungsidee benötige."
            },

            new VisualNovelEvent()
            {
                id = 157,
                nextId = 47,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Sie haben Recht. Entschuldigen Sie bitte. Konzentrieren wir uns auf das Wesentliche: Ihre Förderung.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.DEFEATED)
            },

            new VisualNovelEvent()
            {
                id = 158,
                nextId = 159,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Verstehe. Sie scheinen ja sehr sicher in Ihrer Entscheidung zu sein. " +
                "Aber bedenken Sie, dass der Weg eines Unternehmers hart und unvorhersehbar sein kann.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.NONE)
            },

            new VisualNovelEvent()
            {
                id = 159,
                nextId = 160,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Wenn Sie nicht einmal bereit sind, solche einfachen Fragen zu beantworten, frage ich mich, ob Sie den Herausforderungen gewachsen sind.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 160,
                nextId = 161,
                onChoice = 162,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte einlenken und beschwichtigen."
            },

            new VisualNovelEvent()
            {
                id = 161,
                nextId = 400,
                onChoice = 164,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte klar sagen, dass diese Haltung inakzeptabel ist."
            },

            new VisualNovelEvent()
            {
                id = 162,
                nextId = 163,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Danke, dass Sie das so offen ansprechen. Ich bin mir bewusst, wie anspruchsvoll " +
                "das Unternehmertum sein kann. Ich werde sicherstellen, dass ich alle Aspekte sorgfältig berücksichtige, um erfolgreich zu sein."
            },

            new VisualNovelEvent()
            {
                id = 163,
                nextId = 45,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Das ist eine vernünftige Einstellung. Es ist wichtig, gut vorbereitet zu sein. Haben Sie denn noch Fragen oder Themen, die Sie besprechen möchten?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 164,
                nextId = 165,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ihre Haltung ist inakzeptabel. Meine Entscheidung, diese Frage nicht zu beantworten, sollte " +
                "keinen Einfluss auf meine Fähigkeiten als Unternehmerin haben. Ich erwarte, respektvoll behandelt zu werden."
            },

            new VisualNovelEvent()
            {
                id = 165,
                nextId = 166,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Nun, es sieht so aus, als ob Sie nicht offen für konstruktive Kritik sind. " +
                "Vielleicht sollten Sie noch einmal darüber nachdenken, ob Sie wirklich bereit für den " +
                "Unternehmertum sind, wenn Sie nicht einmal bereit sind, einfache Fragen zu beantworten.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.PROUD)
            },

            new VisualNovelEvent()
            {
                id = 166,
                nextId = 401,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ihre fortgesetzte Abwertung und Unprofessionalität bestätigen nur meine Entscheidung, " +
                "diesen Aspekt meines Privatlebens nicht zu diskutieren. Ich werde mich nach einer anderen Bank umsehen, " +
                "die meine geschäftlichen Pläne mit Respekt behandelt."
            },

            new VisualNovelEvent()
            {
                id = 167,
                nextId = 170,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Nun ja, es wäre für uns aber sehr hilfreich, wenn Sie uns eine Antwort geben würden. " +
                "Schließlich müssen wir abwägen, ob sich eine Förderung auch für uns rentiert.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 168,
                nextId = 166,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Es tut mir leid. So habe ich die Frage nicht gemeint. Natürlich müssen Sie die Frage nicht beantworten.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.DEFEATED)
            },

            new VisualNovelEvent()
            {
                id = 169,
                nextId = 166,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Sie müssen verstehen, dass ich mir nur Gedanken mache.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.NONE)
            },

            new VisualNovelEvent()
            {
                id = 170,
                nextId = 171,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Wie bereits erwähnt, ist es den Förder*innen wichtig, dass das Unternehmen über lange Zeit besteht und Geld einbringt.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 171,
                nextId = 172,
                onChoice = 153,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Meine persönliche Situation sollte keinen Einfluss auf meine Fähigkeiten haben."
            },

            new VisualNovelEvent()
            {
                id = 172,
                nextId = 173,
                onChoice = 174,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Konzentrieren wir uns auf die Geschäftsidee an sich."
            },

            new VisualNovelEvent()
            {
                id = 173,
                nextId = 400,
                onChoice = 177,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Die Frage ist dennoch unangemessen."
            },

            new VisualNovelEvent()
            {
                id = 174,
                nextId = Random.Range(175, 177),
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich schätze Ihre Informationen und werde sicherstellen, dass ich alle Aspekte sorgfältig durchdenke. " +
                "Lassen Sie uns nun über die finanzielle Unterstützung sprechen, die ich für meine Gründungsidee benötige."
            },

            new VisualNovelEvent()
            {
                id = 175,
                nextId = 48,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Da gibt es verschiedene Möglichkeiten, die wir besprechen können. " + 
                "Abhängig von Ihrer Branche, Ihrem Geschäftsmodell und anderen Faktoren könnten staatliche Förderprogramme, Zuschüsse oder Darlehen relevant sein.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 176,
                nextId = 159,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Verstehe. Sie scheinen ja sehr sicher in Ihrer Entscheidung zu sein. Aber bedenken Sie, dass der Weg eines Unternehmers hart und unvorhersehbar sein kann.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.NONE)
            },

            new VisualNovelEvent()
            {
                id = 177,
                nextId = 154,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich finde es unangemessen, nach meinem Familienstand gefragt zu werden. " +
                "Meine Fähigkeiten als Unternehmerin sollten nicht davon abhängen."
            },

            new VisualNovelEvent()
            {
                id = 178,
                nextId = 39,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ihre Überlegungen in Bezug auf Ihren Kinderwunsch sind relevant, um sicherzustellen, " + 
                "dass Ihr Unternehmen stabil läuft, während Sie auch persönliche Verpflichtungen haben.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.NONE)
            },

            new VisualNovelEvent()
            {
                id = 179,
                nextId = 9,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ja, ich habe gut hergefunden. Es war nur sehr viel Verkehr."
            },

            new VisualNovelEvent()
            {
                id = 180,
                nextId = 10,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ja, ich habe gut hergefunden. Es war nur schwer einen Parkplatz zu finden."
            },

            new VisualNovelEvent()
            {
                id = 181,
                nextId = 11,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich habe das Gebäude zuerst nicht gesehen, aber dann doch noch die richtige Straße gefunden."
            },

            new VisualNovelEvent()
            {
                id = 182,
                nextId = 12,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ja, ich habe gut hergefunden."
            },

            new VisualNovelEvent()
            {
                id = 183,
                nextId = 31,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich habe bereits Kinder."
            },

            new VisualNovelEvent()
            {
                id = 184,
                nextId = Random.Range(82, 85),
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich habe nicht vor Kinder zu haben."
            },

            new VisualNovelEvent()
            {
                id = 185,
                nextId = 102,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich habe einen Kinderwunsch."
            },

            new VisualNovelEvent()
            {
                id = 186,
                nextId = 133,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich finde diese Frage ungerechtfertigt."
            },

            new VisualNovelEvent()
            {
                id = 187,
                nextId = Random.Range(167, 170),
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich sehe keinen Grund, diese Frage beantworten zu müssen."
            },

            new VisualNovelEvent()
            {
                id = 188,
                nextId = 42,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich habe da bereits einen Plan, wie ich damit umgehen werde."
            },

            new VisualNovelEvent()
            {
                id = 189,
                nextId = 68,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ehrlich gesagt, ist es frustrierend, dass meine familiäre Situation eine potenzielle Schwäche darstellt."
            },

            new VisualNovelEvent()
            {
                id = 190,
                nextId = 77,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Lassen Sie uns bitte auf die Stärken und Potenziale meiner Idee konzentrieren."
            },

            // new VisualNovelEvent()
            // {
            //     id = 191,
            //     nextId = ,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            //     waitForUserConfirmation = true,
            //     name = "Lea",
            //     text = ""
            // },
            
            // new VisualNovelEvent()
            // {
            //     id = 192,
            //     nextId = ,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            //     waitForUserConfirmation = true,
            //     name = "Lea",
            //     text = ""
            // },
            
            // new VisualNovelEvent()
            // {
            //     id = 193,
            //     nextId = ,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            //     waitForUserConfirmation = true,
            //     name = "Lea",
            //     text = ""
            // },
            
            // new VisualNovelEvent()
            // {
            //     id = 194,
            //     nextId = ,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            //     waitForUserConfirmation = true,
            //     name = "Lea",
            //     text = ""
            // },
            
            // new VisualNovelEvent()
            // {
            //     id = 195,
            //     nextId = ,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            //     waitForUserConfirmation = true,
            //     name = "Lea",
            //     text = ""
            // },
            
            // new VisualNovelEvent()
            // {
            //     id = 196,
            //     nextId = ,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            //     waitForUserConfirmation = true,
            //     name = "Lea",
            //     text = ""
            // },
            
            // new VisualNovelEvent()
            // {
            //     id = 197,
            //     nextId = ,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            //     waitForUserConfirmation = true,
            //     name = "Lea",
            //     text = ""
            // },
            
            // new VisualNovelEvent()
            // {
            //     id = 198,
            //     nextId = ,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            //     waitForUserConfirmation = true,
            //     name = "Lea",
            //     text = ""
            // },
            
            // new VisualNovelEvent()
            // {
            //     id = 199,
            //     nextId = ,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            //     waitForUserConfirmation = true,
            //     name = "Lea",
            //     text = ""
            // },
            
            // new VisualNovelEvent()
            // {
            //     id = 200,
            //     nextId = ,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            //     waitForUserConfirmation = true,
            //     name = "Lea",
            //     text = ""
            // },
            
            // new VisualNovelEvent()
            // {
            //     id = 201,
            //     nextId = ,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            //     waitForUserConfirmation = true,
            //     name = "Lea",
            //     text = ""
            // },
            
            // new VisualNovelEvent()
            // {
            //     id = 202,
            //     nextId = ,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            //     waitForUserConfirmation = true,
            //     name = "Lea",
            //     text = ""
            // },
            
            // new VisualNovelEvent()
            // {
            //     id = 203,
            //     nextId = ,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            //     waitForUserConfirmation = true,
            //     name = "Lea",
            //     text = ""
            // },
            
            // new VisualNovelEvent()
            // {
            //     id = 204,
            //     nextId = ,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            //     waitForUserConfirmation = true,
            //     name = "Lea",
            //     text = ""
            // },
            
            // new VisualNovelEvent()
            // {
            //     id = 205,
            //     nextId = ,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            //     waitForUserConfirmation = true,
            //     name = "Lea",
            //     text = ""
            // },
            
            // new VisualNovelEvent()
            // {
            //     id = 206,
            //     nextId = ,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            //     waitForUserConfirmation = true,
            //     name = "Lea",
            //     text = ""
            // },
            
            // new VisualNovelEvent()
            // {
            //     id = 207,
            //     nextId = ,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            //     waitForUserConfirmation = true,
            //     name = "Lea",
            //     text = ""
            // },
            
            // new VisualNovelEvent()
            // {
            //     id = 208,
            //     nextId = ,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            //     waitForUserConfirmation = true,
            //     name = "Lea",
            //     text = ""
            // },
            
            // new VisualNovelEvent()
            // {
            //     id = 209,
            //     nextId = ,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            //     waitForUserConfirmation = true,
            //     name = "Lea",
            //     text = ""
            // },
            
            // new VisualNovelEvent()
            // {
            //     id = 210,
            //     nextId = ,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            //     waitForUserConfirmation = true,
            //     name = "Lea",
            //     text = ""
            // },



            









            new VisualNovelEvent()
            {
                id = 401,
                nextId = 402,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.CHARAKTER_EXIT_EVENT),
                waitForUserConfirmation = false,
                name = "Herr Müller",
                animationType = AnimationTypeHelper.ToInt(AnimationType.FLY_IN_FROM_ABOVE),
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 402,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.END_NOVEL_EVENT),
                waitForUserConfirmation = false
            },
            
            new VisualNovelEvent()
            {
                id = 300,
                nextId = 4,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Intro",
                text = "Hoffentlich erhältst du Informationen zu passenden Förderungen und kannst mit deinem Ideenpapier überzeugen."
            },

            new VisualNovelEvent()
            {
                id = 301,
                nextId = 18,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Allerdings ist es natürlich den Förder*innen wichtig, dass das Unternehmen über lange Zeit besteht und Geld einbringt.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 302,
                nextId = 303,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Intro",
                text = "Tatsächlich ist es aber so, dass von Frauen geführte Start-Ups und Unternehmen sogar besser performen als die von Männern."
            },

            new VisualNovelEvent()
            {
                id = 303,
                nextId = 25,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Intro",
                text = "('Die durchschnittlichen Prozentsatzwerte von EBITDA/Umsatz zeigen, dass Unternehmen unter " +
                "weiblicher Führung eine höhere Rentabilität aufweisen als solche unter männlicher Führung (3,33 % vs. 0,68 %), " + 
                "entgegen den Aussagen in früheren Fachpublikationen.' Adm. Sci. 2018, 8, 70 Paola Demartini)"
            },
            
            new VisualNovelEvent()
            {
                id = 400,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT),
                waitForUserConfirmation = true
            }

        };
    }
}