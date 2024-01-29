using System.Collections.Generic;
using UnityEngine;

public class CallWithParentsNovel : VisualNovel
{

    public CallWithParentsNovel()
    {
        id = -2;
        title = "Telefonat mit den Eltern";
        description = "Nachdem du dich gründlich informiert hast, hast du dich dazu entschlossen ein Unternehmen zu gründen. Du hast dir auch schon die nächsten Schritte überlegt und rufst nun deine Eltern an, um ihnen von deinem Gründungsvorhaben zu berichten.";
        image = 1;
        nameOfMainCharacter = "Lea";
        feedback = "";
        context = "Es ist das Gespräch einer Gründerin, Lea, mit ihrer Mutter. Sie hat sich gründlich informiert und hat sich entschlossen, ein Unternehmen zu gründen. Sie ruft nun ihre Eltern an, um ihnen von dem Gründungsvorhaben zu berichten. ";
        novelEvents = new List<VisualNovelEvent>()
        {
            //TODO: Change CRITICAL to CONCERNED once available

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
                name = "Mama",
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
                text = "Nachdem du dich gründlich informiert hast, hast du dich dazu entschlossen ein Unternehmen zu gründen. " + 
                "Du hast dir auch schon die nächsten Schritte überlegt und rufst nun deine Eltern an, um ihnen von deinem Gründungsvorhaben zu berichten"
            },

            new VisualNovelEvent()
            {
                id = 4,
                nextId = 4001,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = false,
                name = "Intro",
                text = "Freizeichen"
            },

            new VisualNovelEvent()
            {
                id = 4001,
                nextId = 5,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.PLAY_SOUND_EVENT),
                waitForUserConfirmation = true,
                audioClipToPlay = SoundEnumHelper.ToInt(SoundsEnum.TELEPHONE_CALL)
            },

            new VisualNovelEvent()
            {
                id = 5,
                nextId = 6,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Hallo …. Schön, dass du anrufst. Wie geht es dir?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 6,
                nextId = 7,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Hi. Mir geht es gut. Ich rufe an, weil ich euch etwas wichtiges mitteilen möchte.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 7,
                nextId = 8,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Wirklich? Was denn?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 8,
                nextId = 15,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Intro",
                text = "Deine Eltern klingen interessiert."
            },

            // new VisualNovelEvent()
            // {
            //     id = 9,
            //     nextId = 10,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_OPINION_FEEDBACK_EVENT),
            //     waitForUserConfirmation = false
            // },

            // //TODO: Check if answers are correct

            // new VisualNovelEvent()
            // {
            //     id = 10,
            //     nextId = 11,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
            //     waitForUserConfirmation = false,
            //     opinionChoiceNumber = 1,
            //     text = "Deinen Eltern dein Gründungsvorhaben zu erzählen, kann dich nervös machen. " +
            //     "Ihre Meinung zählt für dich schließlich mehr als die von Fremden."
            // },

            // new VisualNovelEvent()
            // {
            //     id = 11,
            //     nextId = 12,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
            //     waitForUserConfirmation = false,
            //     opinionChoiceNumber = 2,
            //     text = "Du weißt nicht, wie deine Eltern auf deine Idee, zu gründen, reagieren werden. " +
            //     "Daher ist es in Ordnung in dieser Situation ängstlich zu sein."
            // },

            // new VisualNovelEvent()
            // {
            //     id = 12,
            //     nextId = 14,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
            //     waitForUserConfirmation = false,
            //     opinionChoiceNumber = 3,
            //     text = "Es kann Mut machen, seinen eigenen Eltern von der Gründungsidee zu erzählen. " +
            //     "Insbesondere wenn du jetzt schon weißt, dass sie hinter dir stehen."
            // },

            // new VisualNovelEvent()
            // {
            //     id = 14,
            //     nextId = 15,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ASK_FOR_OPINION_EVENT),
            //     waitForUserConfirmation = true,
            // },

            new VisualNovelEvent()
            {
                id = 15,
                nextId = 16,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich habe vor mich selbständig zu machen und ein Unternehmen zu gründen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 16,
                nextId = 17,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Oh, das klingt interessant. Was für ein Unternehmen möchtest du denn gründen?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.QUESTIONING)
            },

            // new VisualNovelEvent()
            // {
            //     id = 17,
            //     nextId = getRandomNumber(18, 22),
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            //     waitForUserConfirmation = true,
            //     name = "Lea",
            //     text = "[Angabe der Branche]"
            // },

            new VisualNovelEvent()
            {
                id = 17,
                nextId = 161,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "[Angabe der Branche]",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 18,
                nextId = 22,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Hm, bist du sicher, dass das eine gute Idee ist?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 19,
                nextId = 77,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Das ist eine mutige Entscheidung und wenn du denkst, dass das der richtige Weg für dich ist, dann unterstütze ich dich darin.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.QUESTIONING)
            },

            new VisualNovelEvent()
            {
                id = 20,
                nextId = 94,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Das ist interessant, aber hast du einen klaren Geschäftsplan? Wie willst du denn die Finanzen organisieren?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.QUESTIONING)
            },

            new VisualNovelEvent()
            {
                id = 21,
                nextId = 113,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Also wenn ich ehrlich bin, finde ich die Idee nicht so gut. Lass es lieber.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.DECISION_NO)
            },

            new VisualNovelEvent()
            {
                id = 22,
                nextId = 23,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Ich meine es nur gut, aber es wäre vielleicht klug, einige Jahre in der Branche zu arbeiten und mehr Erfahrung zu sammeln, bevor du diesen großen Schritt wagst.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 23,
                nextId = 24,
                onChoice = 25,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich verstehe deine Sorgen. Aber ich habe gründlich darüber nachgedacht und fühle mich bereit, diese Herausforderung anzunehmen."
            },

            new VisualNovelEvent()
            {
                id = 24,
                nextId = 200,
                onChoice = 67,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich verstehe eure Bedenken, aber ich habe mich intensiv vorbereitet und werde auch Mentor*innen an meiner Seite haben."
            },

            new VisualNovelEvent()
            {
                id = 25,
                nextId = 26,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Es ist gut zu hören, dass du dich vorbereitet hast, aber wie willst du das alleine schaffen? ",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 26,
                nextId = 27,
                onChoice = 29,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Mit der richtigen Planung geht das schon."
            },

            new VisualNovelEvent()
            {
                id = 27,
                nextId = 28,
                onChoice = 43,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Keine Sorge, ich bin nicht alleine."
            },

            new VisualNovelEvent()
            {
                id = 28,
                nextId = 200,
                onChoice = 48,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ehrlich gesagt, beschäftigt mich diese Frage auch."
            },

            new VisualNovelEvent()
            {
                id = 29,
                nextId = 30,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich werde bestimmt vor Herausforderungen stehen, aber ich habe das Gefühl, " + 
                "dass ich mit der richtigen Planung und Unterstützung die notwendigen Schritte gehen wird."
            },

            new VisualNovelEvent()
            {
                id = 30,
                nextId = 31,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Na ja, hoffentlich klappt das alles. Ich drücke dir auf jeden Fall die Daumen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.QUESTIONING)
            },

            new VisualNovelEvent()
            {
                id = 31,
                nextId = 32,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Danke. Ah, was ich dir auch noch sagen wollte."
            },

            new VisualNovelEvent()
            {
                id = 32,
                nextId = 33,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich habe diese Woche noch einen Termin... "
            },

            new VisualNovelEvent()
            {
                id = 33,
                nextId = 34,
                onChoice = 149,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "... bei der Bank."
            },

            new VisualNovelEvent()
            {
                id = 34,
                nextId = 35,
                onChoice = 149,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "... beim Notar."
            },

            new VisualNovelEvent()
            {
                id = 35,
                nextId = 200,
                onChoice = 149,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "... für die Besichtigung von Büroräumen."
            },

            new VisualNovelEvent()
            {
                id = 43,
                nextId = 44,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich werde nämlich auch von erfahrenen Mentor*innen unterstützt."
            },

            new VisualNovelEvent()
            {
                id = 44,
                nextId = 45,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Diese Gelegenheit ist eine Chance, meine Grenzen zu erweitern und über mich hinauszuwachsen."
            },

            new VisualNovelEvent()
            {
                id = 45,
                nextId = 46,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Es ist schön zu hören, dass du so engagiert und vorbereitet bist.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.PROUD)
            },

            new VisualNovelEvent()
            {
                id = 46,
                nextId = 47,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Ich bin stolz auf dich! Und denk daran, dass ich immer hier bin, um dir zu helfen, wenn du Unterstützung brauchst.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.PROUD)
            },

            new VisualNovelEvent()
            {
                id = 47,
                nextId = 32,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Danke, deine Unterstützung bedeutet mir sehr viel."
            },

            new VisualNovelEvent()
            {
                id = 48,
                nextId = 49,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich bin mir schon bewusst, dass es eine riesige Aufgabe ist, ein Unternehmen alleine aufzubauen... Hm, jetzt bin ich mir doch etwas unsicher."
            },

            new VisualNovelEvent()
            {
                id = 49,
                nextId = 50,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Es ist wichtig, dass du realistisch über die Risiken nachdenkst.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SCARED)
            },

            new VisualNovelEvent()
            {
                id = 50,
                nextId = 51,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Außerdem musst du ja dann auch oft verhandeln. Vielleicht wäre es eine Überlegung wert, einem Partner diese schwierigen Gespräche zu überlassen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 51,
                nextId = 52,
                onChoice = 30,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Das kriege ich auch selbst hin."
            },

            new VisualNovelEvent()
            {
                id = 52,
                nextId = 200,
                onChoice = 53,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Was soll das denn heißen? Als ob ich das nicht alleine hinkriegen würde!"
            },

            new VisualNovelEvent()
            {
                id = 53,
                nextId = 54,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Denk lieber noch einmal darüber nach.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 54,
                nextId = 55,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Es ist naiv zu denken, dass du ohne Erfahrung ein Unternehmen führen kannst.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.REFUSING)
            },

            new VisualNovelEvent()
            {
                id = 55,
                nextId = 56,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Oder vielleicht übernimmst du den kreativen Teil und überlässt die technischen Geschäftsaspekte lieber einem Partner?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 56,
                nextId = 57,
                onChoice = 58,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Du gehst nicht auf diesen Kommentar ein.",
                show = false
            },
            
            new VisualNovelEvent()
            {
                id = 57,
                nextId = 200,
                onChoice = 66,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Du gehst darauf ein.",
                show = false
            },

            new VisualNovelEvent()
            {
                id = 58,
                nextId = 59,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Na gut. Vielleicht solltest du noch eine Nacht darüber schlafen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 59,
                nextId = 60,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Dann erzähle ich lieber nicht, dass ich diese Woche noch einen Termin..."
            },

            new VisualNovelEvent()
            {
                id = 60,
                nextId = 61,
                onChoice = 63,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "... bei der Bank habe."
            },

            new VisualNovelEvent()
            {
                id = 61,
                nextId = 62,
                onChoice = 63,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "... beim Notar habe."
            },

            new VisualNovelEvent()
            {
                id = 62,
                nextId = 200,
                onChoice = 63,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "... für die Besichtigung von Büroräumen habe."
            },

            new VisualNovelEvent()
            {
                id = 63,
                nextId = 64,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Wir können ja nochmal darüber sprechen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.NONE)
            },

            new VisualNovelEvent()
            {
                id = 64,
                nextId = 65,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ja, mal schauen. Ich melde mich. Bis dann."
            },

            new VisualNovelEvent()
            {
                id = 65,
                nextId = 65001,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Bis dann.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.NONE)
            },

            new VisualNovelEvent()
            {
                id = 65001,
                nextId = 199,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.PLAY_SOUND_EVENT),
                waitForUserConfirmation = false,
                audioClipToPlay = SoundEnumHelper.ToInt(SoundsEnum.LEAVE_SCENE)
            },

            new VisualNovelEvent()
            {
                id = 66,
                nextId = 58,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich kann genauso gut die technischen und geschäftlichen Aspekte übernehmen. Lassen wir es einfach."
            },

            new VisualNovelEvent()
            {
                id = 67,
                nextId = 68,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Es ist gut zu hören, dass du dich vorbereitet hast und dass du Unterstützung haben wirst. ",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 68,
                nextId = 69,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Aber denk daran, dass nicht alles nach Plan verläuft. Bist du bereit für Rückschläge?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 69,
                nextId = 70,
                onChoice = 72,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich weiß, dass es nicht immer reibungslos laufen wird. Rückschläge gehören aber dazu, und ich werde daraus lernen."
            },

            new VisualNovelEvent()
            {
                id = 70,
                nextId = 71,
                onChoice = 74,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ja, aber das sehe ich nicht als Hindernis."
            },

            new VisualNovelEvent()
            {
                id = 71,
                nextId = 200,
                onChoice = 53,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ach, da mache ich mir nicht so viele Gedanken. Ich glaube, das wird schon alles gut gehen."
            },

            new VisualNovelEvent()
            {
                id = 72,
                nextId = 73,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Und ich kann mich außerdem an neue Situationen anpassen, um meinen Weg zum Erfolg zu finden."
            },
            
            new VisualNovelEvent()
            {
                id = 73,
                nextId = 45,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich habe schließlich auch schon einen Plan und kann dann endlich das tun, was mir Spaß macht."
            },

            new VisualNovelEvent()
            {
                id = 74,
                nextId = 75,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Natürlich, aber ich mache mir einfach Sorgen und möchte, dass du an alles gedacht hast.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 75,
                nextId = 76,
                onChoice = 30,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Das verstehe ich, aber es wäre schön, wenn du dich auch einfach für mich freuen könntest."
            },

            new VisualNovelEvent()
            {
                id = 76,
                nextId = 200,
                onChoice = 49,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ja, habe ich und ich ziehe das jetzt durch."
            },

            new VisualNovelEvent()
            {
                id = 77,
                nextId = 78,
                onChoice = 79,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Vielen Dank für eure Unterstützung. Es bedeutet mir viel, dass ihr hinter mir steht."
            },

            new VisualNovelEvent()
            {
                id = 78,
                nextId = 200,
                onChoice = 88,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Danke, aber es wäre schön, wenn du mehr Vertrauen in meine Entscheidungen hättest."
            },

            new VisualNovelEvent()
            {
                id = 79,
                nextId = 80,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Ich stehe immer hinter dir.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 80,
                nextId = 81,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Ich möchte nur sicherstellen, dass du alle Aspekte gut durchdacht hast.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 81,
                nextId = 82,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Ein Unternehmen zu gründen kann sehr anspruchsvoll sein.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 82,
                nextId = 83,
                onChoice = 84,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich weiß, aber ich habe mir da schon echt viele Gedanken gemacht und fühle mich vorbereitet."
            },

            new VisualNovelEvent()
            {
                id = 83,
                nextId = 200,
                onChoice = 87,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Das verstehe ich, aber ich habe bereits einen Plan."
            },

            new VisualNovelEvent()
            {
                id = 84,
                nextId = 85,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Natürlich, aber ich mache mir einfach Sorgen und möchte, dass du an alles gedacht hast.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 85,
                nextId = 86,
                onChoice = 30,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Das verstehe ich, aber es wäre schön, wenn du dich auch einfach für mich freuen könntest."
            },

            new VisualNovelEvent()
            {
                id = 86,
                nextId = 200,
                onChoice = 45,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Keine Sorge, das habe ich! Ich bin super vorbereitet und weiß auch, wo ich mir Hilfe holen kann."
            },

            new VisualNovelEvent()
            {
                id = 87,
                nextId = 45,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Außerdem ist diese Gelegenheit eine Chance, meine Grenzen zu erweitern und über mich hinauszuwachsen."
            },

            new VisualNovelEvent()
            {
                id = 88,
                nextId = 89,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Ich hoffe wirklich, dass du alle Herausforderungen meisterst",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 89,
                nextId = 90,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Aber denk daran, dass es nicht immer einfach sein wird.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 90,
                nextId = 91,
                onChoice = 23,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich verstehe deine Sorgen."
            },

            new VisualNovelEvent()
            {
                id = 91,
                nextId = 92,
                onChoice = 93,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich bin ja nicht alleine."
            }, 

            new VisualNovelEvent()
            {
                id = 92,
                nextId = 200,
                onChoice = 84,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich weiß, aber ich habe mir da schon echt viele Gedanken gemacht."
            },

            new VisualNovelEvent()
            {
                id = 93,
                nextId = 45,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich werde von erfahrenen Mentor*innen unterstützt. Diese Gelegenheit ist eine Chance, meine Grenzen zu erweitern und über mich hinauszuwachsen."
            },

            new VisualNovelEvent()
            {
                id = 94,
                nextId = 95,
                onChoice = 96,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich plane, mit meinen Ersparnissen anzufangen und nach weiteren Finanzierungsmöglichkeiten zu suchen."
            },

            new VisualNovelEvent()
            {
                id = 95,
                nextId = 200,
                onChoice = 105,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich werde nach Krediten und anderen Möglichkeiten suchen, um die Finanzierung zu sichern."
            },

            new VisualNovelEvent()
            {
                id = 96,
                nextId = 97,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Das ist vernünftig, aber denk daran, dass das Aufbauen eines Unternehmens oft mehr kostet, als erwartet.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 97,
                nextId = 98,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Hast du denn einen Plan für den Fall, dass du nicht genug Finanzierung erhältst?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 98,
                nextId = 99,
                onChoice = 101,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich weiß, dass unerwartete Kosten auftreten könnten."
            },

            new VisualNovelEvent()
            {
                id = 99,
                nextId = 100,
                onChoice = 103,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Diese Vorstellung macht mir ehrlich gesagt Angst."
            },

            new VisualNovelEvent()
            {
                id = 100,
                nextId = 200,
                onChoice = 74,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ja, darüber habe ich mich auch schon etwas informiert und bin zuversichtlich, dass ich das mit der Finanzierung schon hinkriege."
            },

            new VisualNovelEvent()
            {
                id = 101,
                nextId = 102,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Deshalb habe ich alternative Finanzierungsquellen und auch Backup-Pläne in Betracht gezogen."
            },

            new VisualNovelEvent()
            {
                id = 102,
                nextId = 45,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich werde flexibel und bereit sein, mich anzupassen, um sicherzustellen, dass mein Unternehmen auf Kurs bleibt, selbst wenn ich nicht sofort die gewünschte Finanzierung erhalte."
            },

            new VisualNovelEvent()
            {
                id = 103,
                nextId = 104,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich habe zwar darüber nachgedacht, was passieren könnte, wenn meine Finanzierung nicht ausreicht..."
            },

            new VisualNovelEvent()
            {
                id = 104,
                nextId = 49,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Aber ich weiß nicht so genau, was ich tun würde."
            },

            new VisualNovelEvent()
            {
                id = 105,
                nextId = 106,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Kredite könnten eine Möglichkeit sein, aber vergiss nicht, dass du Zinsen und Rückzahlungspläne berücksichtigen musst.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 106,
                nextId = 107,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Bist du denn sicher, dass du die finanzielle Belastung tragen kannst?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 107,
                nextId = 108,
                onChoice = 110,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich werde mich gründlich über die verschiedenen Kreditoptionen informieren, bevor ich eine Entscheidung treffe."
            },

            new VisualNovelEvent()
            {
                id = 108,
                nextId = 109,
                onChoice = 111,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ja, das habe ich natürlich berücksichtigt und ich denke schon, dass ich das schaffe."
            },

            new VisualNovelEvent()
            {
                id = 109,
                nextId = 200,
                onChoice = 112,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Da mache ich mir nicht so viele Sorgen."
            },

            new VisualNovelEvent()
            {
                id = 110,
                nextId = 45,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Dann kann ich sicherstellen, dass ich das schaffe. Ansonsten kann ich auch nach anderen Finanzierungsmöglichkeiten suchen."
            },

            new VisualNovelEvent()
            {
                id = 111,
                nextId = 84,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich werde es mir aber nochmal genauer anschauen, bevor ich mich entscheide."
            },

            new VisualNovelEvent()
            {
                id = 112,
                nextId = 53,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich möchte auch nicht von vornherein eine Option ausschließen, solange ich die Chancen auf Erfolg sehe. Das wird schon alles."
            },

            new VisualNovelEvent()
            {
                id = 113,
                nextId = 114,
                onChoice = 115,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Was? Warum?"
            },

            new VisualNovelEvent()
            {
                id = 114,
                nextId = 200,
                onChoice = 123,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ihr könnt mir das nicht verbieten."
            },

            new VisualNovelEvent()
            {
                id = 115,
                nextId = 116,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Ich möchte nicht, dass du dich in etwas stürzt, das möglicherweise keine Zukunft hat.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 116,
                nextId = 117,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Das ist so riskant und hört sich eher wie eine Schnapsidee an.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 117,
                nextId = 118,
                onChoice = 119,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Eine Schnapsidee also. Du denkst ich schaffe das nicht."
            },

            new VisualNovelEvent()
            {
                id = 118,
                nextId = 200,
                onChoice = 49,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Kannst du mich nicht einfach unterstützen und dich für mich freuen?"
            },

            new VisualNovelEvent()
            {
                id = 119,
                nextId = 120,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Ich mache mir Sorgen um dich, und du ignorierst komplett, was ich sagen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 120,
                nextId = 121,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Du wirst es bereuen, wenn du in diese Unternehmensgründung stolperst und alles verlierst.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 121,
                nextId = 122,
                onChoice = 53,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich habe mir bereits viele Gedanken darüber gemacht und werde das angehen. Ob es dir nun passt oder nicht."
            },

            new VisualNovelEvent()
            {
                id = 122,
                nextId = 200,
                onChoice = 49,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Kannst du mich nicht einfach unterstützen und dich für mich freuen?"
            },

            new VisualNovelEvent()
            {
                id = 123,
                nextId = 124,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Am Ende ist das immer noch meine Entscheidung!"
            },

            new VisualNovelEvent()
            {
                id = 124,
                nextId = 125,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Glaubst du wirklich, du könntest ein Unternehmen führen?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 125,
                nextId = 126,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Du solltest realistischer sein und dir eine vernünftige Karriere suchen, anstatt diese Fantasie zu verfolgen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 126,
                nextId = 127,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Und was ist denn mit Familie? Du willst doch später Kinder haben, oder nicht?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 127,
                nextId = 128,
                onChoice = 129,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Du denkst also, dass ich unfähig bin, erfolgreich zu sein, weil ich mal Kinder haben will?"
            },
            
            new VisualNovelEvent()
            {
                id = 128,
                nextId = 200,
                onChoice = 139,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Wer sagt denn, dass ich überhaupt Kinder will?"
            },

            new VisualNovelEvent()
            {
                id = 129,
                nextId = 130,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Nein, so war das nicht gemeint, aber wie stellst du es dir denn vor? Kinder haben und ein Unternehmen zu führen!",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 130,
                nextId = 131,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Das alles zu stämmen, ist alles andere als leicht.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 131,
                nextId = 132,
                onChoice = 133,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Das ist doch veraltetes Denken!"
            },

            new VisualNovelEvent()
            {
                id = 132,
                nextId = 200,
                onChoice = 137,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Wieso sprechen wir denn überhaupt über Kinder?"
            },

            new VisualNovelEvent()
            {
                id = 133,
                nextId = 134,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Würdest du das auch zu meinem Bruder sagen?"
            },

            new VisualNovelEvent()
            {
                id = 134,
                nextId = 135,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Das ist doch etwas ganzes anderes. Du hast es schließlich als Frau auch deutlich schwerer.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 135,
                nextId = 136,
                onChoice = 49,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Aber solltest du mich dann nicht umso mehr unterstützen?"
            },

            new VisualNovelEvent()
            {
                id = 136,
                nextId = 200,
                onChoice = 53,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ach lassen wir das. Ich weiß schon, was ich tue."
            },

            new VisualNovelEvent()
            {
                id = 137,
                nextId = 138,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Das hat doch mit meiner Entscheidung gar nichts zu tun."
            },

            new VisualNovelEvent()
            {
                id = 138,
                nextId = 49,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Wenn Kinder mit im Spiel sind, es es nochmal ein anderes Risiko.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 139,
                nextId = 140,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Wieso sollte es gegeben sein, dass ich auf jeden Fall Kinder haben werde? Außerdem spielt das überhaupt keine Rolle."
            },

            new VisualNovelEvent()
            {
                id = 140,
                nextId = 141,
                onChoice = 133,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Das ist doch veraltetes Denken!"
            },

            new VisualNovelEvent()
            {
                id = 141,
                nextId = 200,
                onChoice = 142,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Na wie gut, dass ich dann keine Kinder will."
            },

            new VisualNovelEvent()
            {
                id = 142,
                nextId = 143,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Das denkst du jetzt. Warte noch ein paar Jahre, dann änderst du deine Meinung.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 143,
                nextId = 144,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Aber selbst ohne Kinder solltest du dir das nochmal überlegen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 144,
                nextId = 145,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Ah! Tu dich doch mit deinem Bruder zusammen. Er kennt sich bestimmt besser aus und dann wird das auch erfolgreich.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.HAPPY)
            },

            new VisualNovelEvent()
            {
                id = 145,
                nextId = 146,
                onChoice = 147,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Wieso sollte er es denn besser können als ich?"
            },

            new VisualNovelEvent()
            {
                id = 146,
                nextId = 200,
                onChoice = 53,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ach lassen wir das Thema."
            },

            new VisualNovelEvent()
            {
                id = 147,
                nextId = 148,
                onChoice = 53,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich kriege das auch ohne ihn hin."
            },

            new VisualNovelEvent()
            {
                id = 148,
                nextId = 200,
                onChoice = 53,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Was ist denn das für eine Logik?"
            },

            new VisualNovelEvent()
            {
                id = 149,
                nextId = 150,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Mal schauen, wie das läuft."
            },

            new VisualNovelEvent()
            {
                id = 150,
                nextId = 151,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Da bin ich auch gespannt. Denk dran, die Leute werden sich wohler fühlen, mit dir zu arbeiten, wenn du freundlich und aufgeschlossen auftrittst.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SCARED)
            },

            new VisualNovelEvent()
            {
                id = 151,
                nextId = 152,
                onChoice = 166,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Du gehst nicht weiter darauf ein.",
                show = false
            },

            new VisualNovelEvent()
            {
                id = 152,
                nextId = 200,
                onChoice = 169,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Du gehst auf diesen Kommentar ein.",
                show = false
            },

            // new VisualNovelEvent()
            // {
            //     id = 151,
            //     nextId = 158,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            //     waitForUserConfirmation = true,
            //     name = "Lea",
            //     text = "Klar, ich melde mich dann."
            // },

            // new VisualNovelEvent()
            // {
            //     id = 152,
            //     nextId = 153,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            //     waitForUserConfirmation = true,
            //     name = "Mama",
            //     text = "Und du kannst auch deinen weiblichen Charme nutzen! Dann helfen Leute dir noch viel lieber.",
            //     expressionType = ExpressionTypeHelper.ToInt(ExpressionType.HAPPY)
            // },

            new VisualNovelEvent()
            {
                id = 153,
                nextId = 154,
                onChoice = 151,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Du beschließt, nicht weiter auf diesen Kommentar einzugehen."
            },

            new VisualNovelEvent()
            {
                id = 154,
                nextId = 200,
                onChoice = 155,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Du gehst auf diesen Kommentar ein.",
                show = false
            },

            new VisualNovelEvent()
            {
                id = 155,
                nextId = 156,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Das würdest du jetzt nicht zu einem Mann sagen. Was soll 'weiblicher Charme' überhaupt bedeuten?"
            },

            new VisualNovelEvent()
            {
                id = 156,
                nextId = 157,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Du hast ja recht. Da habe ich nicht richtig darüber nachgedacht.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.DEFEATED)
            },

            new VisualNovelEvent()
            {
                id = 157,
                nextId = 151,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Melde dich einfach nach dem Termin, ja?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.DEFEATED)
            },

            new VisualNovelEvent()
            {
                id = 158,
                nextId = 159,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich muss auch schon wieder weiter."
            },

            new VisualNovelEvent()
            {
                id = 159,
                nextId = 65001,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Nochmal danke und bis dann."
            },

            new VisualNovelEvent()
            {
                id = 161,
                nextId = 162,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Intro",
                text = "Wie reagiert deine Mutter?"
            },

            new VisualNovelEvent()
            {
                id = 162,
                nextId = 163,
                onChoice = 18,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Intro",
                text = "Besorgt"
            },

            new VisualNovelEvent()
            {
                id = 163,
                nextId = 164,
                onChoice = 19,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Intro",
                text = "Besorgt, aber unterstützend"
            },

            new VisualNovelEvent()
            {
                id = 164,
                nextId = 165,
                onChoice = 20,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Intro",
                text = "Praktikabel denkend"
            },

            new VisualNovelEvent()
            {
                id = 165,
                nextId = 200,
                onChoice = 21,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Intro",
                text = "Ablehnend"
            },

            new VisualNovelEvent()
            {
                id = 166,
                nextId = 167,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Hm, ich muss dann auch weiter."
            },

            new VisualNovelEvent()
            {
                id = 167,
                nextId = 168,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich halte euch auf dem Laufenden. Bis dann."
            },

            new VisualNovelEvent()
            {
                id = 168,
                nextId = 65001,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Mama",
                text = "Alles klar, und besuch uns auch mal wieder. Bis dann.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 169,
                nextId = 166,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Das würdest du jetzt nicht zu einem Mann sagen. Es ist okay zu zeigen, dass ich weiß, was ich will und vorbereitet bin."
            },

            new VisualNovelEvent()
            {
                id = 199,
                nextId = 201,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.CHARAKTER_EXIT_EVENT),
                waitForUserConfirmation = false,
                name = "Mama",
                animationType = AnimationTypeHelper.ToInt(AnimationType.FLY_IN_FROM_ABOVE),
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 200,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT),
                waitForUserConfirmation = true
            },

            new VisualNovelEvent()
            {
                id = 201,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.END_NOVEL_EVENT),
                waitForUserConfirmation = false
            }
            
        };
    }

    public int getRandomNumber(int lowerBound, int upperBound) 
    {
        Debug.Log(System.DateTime.Now.Millisecond);
        Random.InitState(System.DateTime.Now.Millisecond);
        return Random.Range(lowerBound, upperBound);
    }
}
