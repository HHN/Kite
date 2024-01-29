using System.Collections.Generic;
using UnityEngine;

//Check for cohesive waiting for user input and if lea dialoug is implemented correctly.
//Check if animationType = AnimationTypeHelper.ToInt(AnimationType.FLY_IN_FROM_ABOVE) is needed more frequently

public class CallWithNotaryNovel : VisualNovel
{
    public CallWithNotaryNovel()
    {
        id = -4;
        title = "Telefonat mit dem Notar";
        description = "Du triffst dich mit einer Notarin, um einen Termin für deine Gründung auszumachen.";
        image = 3;
        nameOfMainCharacter = "Lea";
        context = "Es ist das Gespräch einer Gründerin, Lea, mit einer Notarin. Es geht um die Eintragung des Unternehmens der Gründerin ins Handelsregister. Die Gründerin hat sich gut vorbereitet und hofft einen Termin für ein Eintragung zu erhalten.";
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
                name = "Frau Mayer",
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
                text = "Du triffst dich mit einer Notarin, um einen Termin für deine Gründung auszumachen."
            },

            new VisualNovelEvent()
            {
                id = 4,
                nextId = 5,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Guten Tag, mein Name ist Lea Winkler. Ich rufe an, weil ich mein " +
                "Unternehmen gerne ins Handelsregister eintragen lassen möchte.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 5,
                nextId = 12,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Das freut mich. Als erstes muss ein Fragebogen ausgefüllt werden, in dem " +
                "vorab Fragen zum Unternehmen geklärt werden, wie der Name, die Rechtsform und der " +
                "Unternehmensgegenstand. Wichtig ist auch, dass der Geschäftsführer anwesend ist, " +
                "weil seine Unterschrift essentiell ist.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            // new VisualNovelEvent()
            // {
            //     id = 6,
            //     nextId = 7,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_OPINION_FEEDBACK_EVENT),
            //     waitForUserConfirmation = false
            // },

            // new VisualNovelEvent()
            // {
            //     id = 7,
            //     nextId = 8,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
            //     waitForUserConfirmation = false,
            //     opinionChoiceNumber = 1,
            //     text = "Bei dem Termin beim Notar/bei der Notarin wird dein Unternehmen beurkundet und " +
            //     "ins Handelsregister eingetragen. Bei so einem großen Schritt darfst du nervös sein."
            // },

            // new VisualNovelEvent()
            // {
            //     id = 8,
            //     nextId = 9,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
            //     waitForUserConfirmation = false,
            //     opinionChoiceNumber = 2,
            //     text = "Es ist in Ordnung in diesem Moment ängstlich zu sein, schließlich handelt es sich " +
            //     "um einen wichtigen Schritt in der Unternehmensgründung."
            // },

            // new VisualNovelEvent()
            // {
            //     id = 9,
            //     nextId = 10,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
            //     waitForUserConfirmation = false,
            //     opinionChoiceNumber = 3,
            //     text = "Du bist dabei den nächsten Schritt in der Unternehmensgründung zu beschreiten " +
            //     "und das kann sehr ermutigend wirken."
            // },

            // new VisualNovelEvent()
            // {
            //     id = 10,
            //     nextId = 11,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
            //     waitForUserConfirmation = false,
            //     opinionChoiceNumber = 4,
            //     text = "Es klingt, als würde die Person am anderen Ende der Leitung dich nicht als " +
            //     "Geschäftsführerin erkennen und es ist nachvollziehbar, deswegen verärgert zu sein."
            // },

            // new VisualNovelEvent()
            // {
            //     id = 11,
            //     nextId = 12,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ASK_FOR_OPINION_EVENT),
            //     waitForUserConfirmation = true,
            // },

            new VisualNovelEvent()
            {
                id = 12,
                nextId = 13,
                onChoice = 16,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Vielen Dank. Können Sie mir den Fragebogen per Mail zusenden?"
            },

            new VisualNovelEvent()
            {
                id = 13,
                nextId = 14,
                onChoice = getRandomReactionFromNotary(0),
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Das ist kein Problem, da ich die Geschäftsführerin bin."
            },

            new VisualNovelEvent()
            {
                id = 14,
                nextId = 15,
                onChoice = getRandomReactionFromNotary(1),
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Die Geschäftsführerin bin ich und ich " +
                "möchte auch als solche angesprochen werden!"
            },

            new VisualNovelEvent()
            {
                id = 15,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT),
                waitForUserConfirmation = true
            },

            new VisualNovelEvent()
            {
                id = 16,
                nextId = 17,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Den Fragebogen sende ich Ihnen gerne zu. " +
                "Nächsten Monat hätte ich noch einen Termin frei. Würde Ihnen das passen?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 17,
                nextId = 18,
                onChoice = 20,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ja"
            },

            new VisualNovelEvent()
            {
                id = 18,
                nextId = 19,
                onChoice = 21,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Nein"
            },

            new VisualNovelEvent()
            {
                id = 19,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT),
                waitForUserConfirmation = true
            },

            new VisualNovelEvent()
            {
                id = 20,
                nextId = 63,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ja, das passt wunderbar. Vielen Dank und bis dann!"
            },

            new VisualNovelEvent()
            {
                id = 21,
                nextId = 61,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Würde auch Datum + Uhrzeit gehen?"      //TODO: Change date and time
            },

            new VisualNovelEvent()
            {
                id = 22,
                nextId = 23,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Oh, tut mir leid. Da war ich wohl etwas voreilig. Ich kann Ihnen die benötigten Formulare zusenden und nächsten Monat hätte ich einen Termin frei.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 23,
                nextId = 24,
                onChoice = 26,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Das ist schon in Ordnung. Das kann mal passieren."
            },

            new VisualNovelEvent()
            {
                id = 24,
                nextId = 25,
                onChoice = 27,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ja, wir sollten nicht vergessen, dass die Position der Geschäftsführung vom Geschlecht unabhängig ist."
            },

            new VisualNovelEvent()
            {
                id = 25,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT),
                waitForUserConfirmation = true
            },

            new VisualNovelEvent()
            {
                id = 26,
                nextId = 17,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Danke. Ich kann Ihnen die benötigten Formulare zusenden und nächsten Donnerstag 15 Uhr hätte ich noch einen Termin frei. Würde Ihnen das passen?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 27,
                nextId = 17,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Selbstverständlich. Ich tendiere nur dazu an einen Mann zu denken, wenn ich an die Geschäftsführung denke. " + 
                "Aber ich werde mir das nun mehr ins Gedächtnis rufen. " + 
                "Ich kann Ihnen die benötigten Formulare zusenden und nächsten Donnerstag 15 Uhr hätte ich noch einen Termin frei. Würde Ihnen das passen?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 28,
                nextId = 29,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Ah, okay. Das Formular sende ich Ihnen zu. Nächsten Donnerstag 15 Uhr hätte ich noch einen Termin frei. " + 
                "Würde Ihnen das passen?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 29,
                nextId = 30,
                onChoice = 20,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ja"
            },

            new VisualNovelEvent()
            {
                id = 30,
                nextId = 31,
                onChoice = 21,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Nein"
            },

            new VisualNovelEvent()
            {
                id = 31,
                nextId = 32,
                onChoice = 33,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte nach einer Bestätigung fragen, dass verstanden wurde, dass ich die Geschäftsführung übernehme."
            },

            new VisualNovelEvent()
            {
                id = 32,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT),
                waitForUserConfirmation = true
            },

            new VisualNovelEvent()
            {
                id = 33,
                nextId = Random.Range(34, 37),
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Könnten Sie bitte bestätigen, dass Sie meine Position als Geschäftsführerin notiert haben? Es ist wichtig für den Eintrag ins Handelsregister, dass dies klar ist."
            },

            new VisualNovelEvent()
            {
                id = 34,
                nextId = 37,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = false,
                name = "Frau Mayer",
                text = "Oh, tut mir leid, dass ich das nicht verständlich gemacht habe, aber ich habe das notiert. Keine Sorge.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.ASTONISHED)
            },

            new VisualNovelEvent()
            {
                id = 37,
                nextId = 38,
                onChoice = 40,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Okay, danke für die Klarstellung."
            },

            new VisualNovelEvent()
            {
                id = 38,
                nextId = 39,
                onChoice = 27,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Danke, mir ist nur wichtig zu betonen, dass die Geschäftsführung unabhängig vom Geschlecht wahrgenommen werden kann."
            },

            new VisualNovelEvent()
            {
                id = 39,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT),
                waitForUserConfirmation = true
            },

            new VisualNovelEvent()
            {
                id = 40,
                nextId = 17,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = false,
                name = "Frau Mayer",
                text = "Gerne. Ich kann Ihnen die benötigten Formulare zusenden und nächsten Donnerstag 15 Uhr hätte ich noch einen Termin frei. Würde Ihnen das passen?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 35,
                nextId = 37,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = false,
                name = "Frau Mayer",
                text = "Ja, das ist bereits notiert.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 36,
                nextId = 41,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = false,
                name = "Frau Mayer",
                text = "Ach, Sie sind die Gründerin? Das ist ja außergewöhnlich. Wer hat Ihnen denn bei der Gründung geholfen?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.ASTONISHED)
            },

            new VisualNovelEvent()
            {
                id = 41,
                nextId = 42,
                onChoice = 45,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich hatte keine Hilfe."
            },

            new VisualNovelEvent()
            {
                id = 42,
                nextId = 43,
                onChoice = 45,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich habe mir Ratschläge von anderen eingeholt, aber gründen werde ich alleine."
            },

            new VisualNovelEvent()
            {
                id = 43,
                nextId = 44,
                onChoice = 46,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Was meinen Sie mit dieser Frage?"
            },

            new VisualNovelEvent()
            {
                id = 44,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT),
                waitForUserConfirmation = true
            },

            new VisualNovelEvent()
            {
                id = 45,
                nextId = 17,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = false,
                name = "Frau Mayer",
                text = "Oh, so ist das. Nun gut. Kommen wir zurück zu dem Fragebogen. " + 
                "Ich kann Ihnen die benötigten Formulare zusenden und nächsten Donnerstag 15 Uhr hätte ich noch einen Termin frei. Würde Ihnen das passen?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 46,
                nextId = 47,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = false,
                name = "Frau Mayer",
                text = "Nun ja, als Frau ist es deutlich schwieriger zu gründen. Alleine die Kapitalbeschaffung ist eine große Hürde! Ich würde Ihnen raten, sich einen Partner zu suchen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 47,
                nextId = 48,
                onChoice = 50,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Danke für den Rat, aber ich möchte es lieber alleine versuchen."
            },

            new VisualNovelEvent()
            {
                id = 48,
                nextId = 49,
                onChoice = 50,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Danke für den Rat. Ich werde das in Erwägung ziehen."
            },

            new VisualNovelEvent()
            {
                id = 49,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT),
                waitForUserConfirmation = true
            },

            new VisualNovelEvent()
            {
                id = 50,
                nextId = 17,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = false,
                name = "Frau Mayer",
                text = "Kommen wir zurück zu dem Fragebogen. Ich kann Ihnen die benötigten Formulare zusenden und nächsten Donnerstag 15 Uhr hätte ich noch einen Termin frei. Würde Ihnen das passen?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 51,
                nextId = 52,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = false,
                name = "Frau Mayer",
                text = "Wie auch immer. Wollen Sie den Fragebogen zugesendet bekommen?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 52,
                nextId = 53,
                onChoice = 56,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ja."
            },

            new VisualNovelEvent()
            {
                id = 53,
                nextId = 54,
                onChoice = 57,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte Kritik äußern."
            },

            new VisualNovelEvent()
            {
                id = 54,
                nextId = 55,
                onChoice = 60,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich will das Gespräch abbrechen."
            },

            new VisualNovelEvent()
            {
                id = 55,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT),
                waitForUserConfirmation = true
            },

            new VisualNovelEvent()
            {
                id = 56,
                nextId = 17,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = false,
                name = "Frau Mayer",
                text = "Gut. Nächsten Donnerstag 15 Uhr hätte ich noch einen Termin frei. Würde Ihnen das passen?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 57,
                nextId = Random.Range(58,60),
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Es ist mir wichtig, dass meine Position als Geschäftsführerin richtig anerkannt wird. Ich erwarte, in diesem Prozess angemessen behandelt zu werden."
            },

            new VisualNovelEvent()
            {
                id = 58,
                nextId = 17,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = false,
                name = "Frau Mayer",
                text = "Oh tut mir leid. So war das nicht gemeint. Ich werde mich künftig um eine angemessenere Behandlung bemühen. " + 
                "Nächsten Donnerstag 15 Uhr hätte ich noch einen Termin frei. Würde Ihnen das passen?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.DEFEATED)
            },

            new VisualNovelEvent()
            {
                id = 59,
                nextId = 60,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Sie nehmen das zu persönlich. Geschlecht spielt keine Rolle. Das war nicht meine Absicht, Sie abzuwerten. " + 
                "Wenn Sie denken, ich habe Ihre Position nicht respektiert, dann ist das Ihr Problem.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 60,
                nextId = 63001,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich denke, ich werde mich nach einer Notarin umsehen, die meine Anliegen und meine Rolle als Geschäftsführerin respektiert. Auf Wiedersehen."
            },

             new VisualNovelEvent()
            {
                id = 61,
                nextId = 62,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Ja, das würde auch passen. Dann wüsche ich Ihnen noch einen schönen Tag. Bis dann!"
            },

            new VisualNovelEvent()
            {
                id = 62,
                nextId = 63001,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Danke, wünsche ich Ihnen auch."
            },

            new VisualNovelEvent()
            {
                id = 63,
                nextId = 63001,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Schönen Tag noch!",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 63001,
                nextId = 64,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.PLAY_SOUND_EVENT),
                waitForUserConfirmation = false,
                audioClipToPlay = SoundEnumHelper.ToInt(SoundsEnum.LEAVE_SCENE)
            },

            new VisualNovelEvent()
            {
                id = 64,
                nextId = 65,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.CHARAKTER_EXIT_EVENT),
                waitForUserConfirmation = false,
                name = "Frau Mayer",
                animationType = AnimationTypeHelper.ToInt(AnimationType.FLY_IN_FROM_ABOVE),
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 65,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.END_NOVEL_EVENT),
                waitForUserConfirmation = false
            }
        };
    }

    private int getRandomReactionFromNotary(int optionOneOrTwo){
        int value = 0;
        if(optionOneOrTwo == 0){
            value = Random.Range(0, 10);
            if(value < 5){
                return 22;
            } else if(value > 8){
                return 28;
            } else {
                return 51;
            }
        } else {
            value = Random.Range(0, 10);
            if(value < 2){
                return 22;
            } else if(value > 6){
                return 28;
            } else {
                return 51;
            }
        }
    }
}