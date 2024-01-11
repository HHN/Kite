using System.Collections.Generic;
using UnityEngine;

public class RentingAnOfficeNovel : VisualNovel
{
    public RentingAnOfficeNovel()
    {
        id = -6;
        title = "Anmietung eines Büros";
        description = "Du hast heute einen Termin für die Besichtigung von Büroräumen.";
        image = 5;
        context = "Es ist das Gespräch einer Gründerin, Lea, mit einem Vermieter. Es geht um die Anmietung von Büro-Räumen. Die Gründerin hat sich gut vorbereitet und hofft, dass sie die Büroräume für ihr Unternehmen bekommt. ";
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
                nextId = 4,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Intro",
                text = "Du hast heute einen Termin für die Besichtigung von Büroräumen. Dort angekommen, wirst du von dem Vermieter angesprochen."
            },

            new VisualNovelEvent()
            {
                id = 4,
                nextId = 5,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Guten Tag, Frau [Name]. Dies sind also die zur Verfügung stehenden Räumlichkeiten. Welche Art von Unternehmen möchten Sie denn gründen?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 5,
                nextId = 6,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich möchte ein Technologieunternehmen gründen, das sich auf die Entwicklung " +
                "nachhaltiger und personalisierter Lösungen in den Bereichen Energie, Mobilität und " +
                "Digitalisierung konzentriert." //TODO: Yvonne: "Es geht um ... mit ... als Zielgruppe."
            },

            new VisualNovelEvent()
            {
                id = 6,
                nextId = 13,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Oh, wie interessant! Wie in der Anzeige geschrieben, liegt der Mietpreis kalt " +
                "bei 8,11€ pro Quadratmeter. Sind Sie sicher, diesen Betrag monatlich über längere Zeit bezahlen zu können?",
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
            //     text = "Es ist in Ordnung, jetzt nervös zu sein. Räumlichlkeiten für das eigene Unternehmen " +
            //     "anzumieten, ist ein weiterer wichtiger Schritt in die Verwirklichung der eigenen Pläne."
            // },

            // new VisualNovelEvent()
            // {
            //     id = 9,
            //     nextId = 10,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
            //     waitForUserConfirmation = false,
            //     opinionChoiceNumber = 2,
            //     text = "Jetzt ängstlich zu sein ist in Ordnung. Du stehst kurz davor, einen " +
            //     "weiteren Schritt in die Selbstständigkeit zu beschreiten und von deiner Antwort " +
            //     "kann abhängen, ob du die Bürofläche bekommen wirst."
            // },

            // new VisualNovelEvent()
            // {
            //     id = 10,
            //     nextId = 11,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
            //     waitForUserConfirmation = false,
            //     opinionChoiceNumber = 3,
            //     text = "Du stehst kurz davor, den nächsten Schritt zur Verwirklichung deiner Pläne zu " +
            //     "tätigen und zu wissen, dass man diese Frage optimistisch beantworten kann ist ermutigend."
            // },

            // new VisualNovelEvent()
            // {
            //     id = 11,
            //     nextId = 12,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
            //     waitForUserConfirmation = false,
            //     opinionChoiceNumber = 4,
            //     text = "Es kann verärgern, dass einem unterstellt wird, dass das eigene Unternehmen " +
            //     "nicht lange genug überleben wird, um die Miete konstant zahlen zu können."
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
                onChoice = 16,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Natürlich!",
                show = false
            },

            new VisualNovelEvent()
            {
                id = 14,
                nextId = 15,
                onChoice = 19,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Laut meinen Recherchen: Ja."
            },

            new VisualNovelEvent()
            {
                id = 15,
                nextId = 400,
                onChoice = 18,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Die nächsten Monate habe ich noch eine Gründungsförderung."
            },

            new VisualNovelEvent()
            {
                id = 16,
                nextId = Random.Range(17, 20),
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Natürlich! Meine Marktrecherche ergeben, dass es eine hohe " +
                "Nachfrage gibt und ich habe für die nächsten drei Jahre einen Finanzplan erstellt, der genug Einkommen prognostiziert."
            },

            new VisualNovelEvent()
            {
                id = 17,
                nextId = 20,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Es ist erstaunlich, wie gut Sie sich auf diese neue Herausforderung vorbereitet haben. " +
                "Frauen neigen dazu, mehr Wert auf die Details zu legen, nicht wahr?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 18,
                nextId = 87,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Gut, dass Sie gut vorbereitet sind. Mieten Sie zum ersten Mal Büroflächen? Haben Sie alle Details und Kosten berücksichtigt?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.NONE)
            },

            new VisualNovelEvent()
            {
                id = 19,
                nextId = 116,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ich schätze Ihre Zuversicht. Dennoch empfehle ich, die langfristige Finanzplanung " +
                "sorgfältig zu prüfen, da Mietverträge bindend sind. Es wäre ratsam, sicherzustellen, dass " +
                "Sie die finanzielle Belastung über einen längeren Zeitraum tragen können.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.QUESTIONING)
            },

            new VisualNovelEvent()
            {
                id = 20,
                nextId = 21,
                onChoice = 23,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich erkläre, dass eher eine gute Vorbereitung entscheidend ist.",
                show = false
            },

            new VisualNovelEvent()
            {
                id = 21,
                nextId = 22,
                onChoice = 67,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich bedanke mich für das Kompliment.",
                show = false
            },

            new VisualNovelEvent()
            {
                id = 22,
                nextId = 400,
                onChoice = 76,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich lenke das Gespräch auf die Büroflächen.",
                show = false
            },

            new VisualNovelEvent()
            {
                id = 23,
                nextId = 24,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich denke, eine gründliche Vorbereitung ist entscheidend, unabhängig vom Geschlecht. " +
                "Alle, die ein Unternehmen gründen, sollten sich um Details kümmern, um erfolgreich zu sein. " +
                "Vielen Dank für das Vertrauen in meine Fähigkeiten."
            },

            new VisualNovelEvent()
            {
                id = 24,
                nextId = 25,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Stimmt, da haben Sie selbstverständlich recht. Aber Frauen haben manchmal zusätzliche " +
                "Herausforderungen in der Geschäftswelt. Ein Freund von mir hat auch gegründet! Da habe ich einiges mitbekommen. " +
                "Sie müssen nämlich auch ein Notariat aufsuchen und ihr Unternehmen ins Handelsregister eintragen lassen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 25,
                nextId = 26,
                onChoice = 29,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Danke für den Tipp."
            },

            new VisualNovelEvent()
            {
                id = 26,
                nextId = 27,
                onChoice = 64,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Vielleicht könnten Sie uns bekannt machen?",
                show = false
            },

            new VisualNovelEvent()
            {
                id = 27,
                nextId = 28,
                onChoice = 65,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich bin bereits mit anderen Unternehmer*innen in Kontakt, aber danke für den Tipp.",
                show = false
            },

            new VisualNovelEvent()
            {
                id = 28,
                nextId = 400,
                onChoice = 66,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Das ist mir bewusst.",
                show = false
            },

            new VisualNovelEvent()
            {
                id = 29,
                nextId = 30,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Gerne, gerne. Ich helfe, wo ich kann. Mein Freund betont außerdem immer wieder, " +
                "wie wichtig Marktforschung ist. Haben Sie das richtig gemacht? Es könnte wirklich den Unterschied machen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 30,
                nextId = 31,
                onChoice = 34,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich antworte selbstbewusst.",
                show = false
            },

            new VisualNovelEvent()
            {
                id = 31,
                nextId = 32,
                onChoice = 54,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich antworte dankbar und bin offen für Feedback.",
                show = false
            },

            new VisualNovelEvent()
            {
                id = 32,
                nextId = 33,
                onChoice = 61,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich antworte sachlich.",
                show = false
            },

            new VisualNovelEvent()
            {
                id = 33,
                nextId = 400,
                onChoice = 63,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich antworte frustriert.",
                show = false
            },

            new VisualNovelEvent()
            {
                id = 34,
                nextId = 35,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ja, wie gesagt war Marktforschung ein entscheidender Teil meiner Vorbereitung. " +
                "Ich habe umfangreiche Analysen durchgeführt, um sicherzustellen, dass mein Unternehmen den " +
                "Bedürfnissen des Marktes entspricht. Ich bin zuversichtlich, dass ich auf einem soliden Fundament stehe."
            },

            new VisualNovelEvent()
            {
                id = 35,
                nextId = 36,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ach so, ja, das hatten Sie erwähnt. Gut, dass Sie sich Sachen besser merken können und " +
                "das so direkt sagen. Bestimmt haben Sie in der Beziehung die Hosen an, oder? Hahaha.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 36,
                nextId = 37,
                onChoice = 39,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich lenke das Gespräch auf den Mietprozess.",
                show = false
            },

            new VisualNovelEvent()
            {
                id = 37,
                nextId = 38,
                onChoice = 47,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich sage, dass das weniger wichtig ist bei einer Unternehmensgründung.",
                show = false
            },

            new VisualNovelEvent()
            {
                id = 38,
                nextId = 400,
                onChoice = 49,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich sage klar, dass ich mich auf die Mietdetails konzentrieren möchte und spreche das Geschlechterklischee an.",
                show = false
            },

            new VisualNovelEvent()
            {
                id = 39,
                nextId = 40,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Interessanter Vergleich! Aber zurück zur Sache - ich bin sehr daran interessiert, " +
                "wie der Mietprozess weitergeht. Haben Sie noch weitere Fragen zu meinen Unternehmensplänen?"
            },

            new VisualNovelEvent()
            {
                id = 40,
                nextId = 41,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Schön mal eine selbstsichere Frau zu sehen! Ich habe erstmal keine weitere Fragen " +
                "zu Ihren Unternehmenslpänen. Ich bräuchte von Ihnen noch eine Selbstauskunft. Es gibt noch " +
                "andere, die sich beworben haben, aber es würde mich freuen, wenn Sie die Räumlichkeiten nehmen möchten.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 41,
                nextId = 42,
                onChoice = 43,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte auf den benevolenten Sexismus aufmerksam machen.",
                show = false
            },

            new VisualNovelEvent()
            {
                id = 42,
                nextId = 400,
                onChoice = 45,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich bedanke und verabschiede mich.",
                show = false
            },

            new VisualNovelEvent()
            {
                id = 43,
                nextId = 44,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich schätze Ihre Anerkennung meiner Selbstsicherheit, aber ich möchte klarstellen, " +
                "dass ich in geschäftlichen Angelegenheiten auf eine professionelle und geschlechtsneutrale " +
                "Betrachtung Wert lege. Der Kommentar 'Schön mal eine selbstsichere Frau zu sehen!' impliziert nämlich, dass Frauen sonst unsicher sind."
            },

            new VisualNovelEvent()
            {
                id = 44,
                nextId = 45,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Wenn Sie das sagen. Also, wenn Sie mir eine Selbstauskunft zuschicken, setze ich " +
                "Sie auf die Liste der positiven Bewerber*innen. In ein paar Tagen werde ich Ihnen Bescheid geben, ob Sie die Räumlichkeiten haben können.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.NONE)
            },

            new VisualNovelEvent()
            {
                id = 45,
                nextId = 46,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Die schicke ich Ihnen so schnell wie möglich zu. Vielen Dank und noch einen schönen Tag."
            },

            new VisualNovelEvent()
            {
                id = 46,
                nextId = 401,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Alles klar, Ihnen auch noch einen schönen Tag. Auf Wiedersehen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 47,
                nextId = 48,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Nun, in der Welt der Unternehmensgründung ist es weniger wichtig, wer die Hosen anhat. " +
                "Es geht mehr darum, die richtigen Entscheidungen zu treffen."
            },

            new VisualNovelEvent()
            {
                id = 48,
                nextId = 41,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Erfrischend mal eine selbstbewusste Frau zu sehen! Ich bräuchte von Ihnen noch eine Selbstauskunft. "+
                "Es gibt noch andere, die sich beworben haben, aber es würde mich freuen, wenn Sie die Räumlichkeiten nehmen möchten.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 49,
                nextId = 50,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich bevorzuge es, geschäftliche Gespräche auf professionellem Niveau zu führen. " +
                "Geschlechterklischees haben wenig Platz in meiner unternehmerischen Realität. Können wir uns auf die Mietdetails konzentrieren?"
            },

            new VisualNovelEvent()
            {
                id = 50,
                nextId = 51,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ach, so war das doch nicht gemeint, aber wenn Sie mir eine Selbstauskunft zuschicken, " +
                "setze ich Sie auf die Liste der positiven Bewerber*innen. In ein paar Tagen werde ich Ihnen Bescheid geben, ob Sie die Räumlichkeiten haben können.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 51,
                nextId = 52,
                onChoice = 45,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich bedanke mich und verabschiede mich.",
                show = false
            },

            new VisualNovelEvent()
            {
                id = 52,
                nextId = 400,
                onChoice = 53,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich erwidere, dass auch Witze oder Aussagen ohne sexistische Intention, sexistisch sein können.",
                show = false
            },

            new VisualNovelEvent()
            {
                id = 53,
                nextId = 44,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich danke Ihnen für die Klarstellung. Allerdings können selbst unbeabsichtigte Äußerungen " +
                "geschlechtsbezogene Konnotationen haben. Ich schätze eine bewusste und geschlechtsneutrale Kommunikation in geschäftlichen Angelegenheiten."
            },

            new VisualNovelEvent()
            {
                id = 54,
                nextId = 55,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich schätze Ihre Betonung der Marktforschung. Ich habe mein Bestes getan, um sicherzustellen, dass ich den Markt gut verstehe. " +
                "Falls Sie spezifische Aspekte haben, bei denen Sie denken, dass ich sie genauer betrachten sollte, bin ich offen für Feedback."
            },

            new VisualNovelEvent()
            {
                id = 55,
                nextId = 56,
                onChoice = 58,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich antworte selbstbewusst.",
                show = false
            },

            new VisualNovelEvent()
            {
                id = 56,
                nextId = 57,
                onChoice = 59,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich gehe auf den Begriff 'Frauenpower' ein und lenke das Gespräch auf den Mietprozess.",
                show = false
            },

            new VisualNovelEvent()
            {
                id = 57,
                nextId = 400,
                onChoice = 60,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich antworte sachliche und lenke das Gespräch auf den Mietprozess.",
                show = false
            },

            new VisualNovelEvent()
            {
                id = 58,
                nextId = 48,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich bin überzeugt, dass meine Qualifikationen und Fähigkeiten mich erfolgreich machen werden. " +
                "Stress gehört zum Unternehmertum dazu, aber ich bin bereit, die Herausforderungen anzunehmen."
            },

            new VisualNovelEvent()
            {
                id = 59,
                nextId = 50,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich verstehe, dass Frauenpower oft positiv gemeint ist, aber ich bevorzuge, nicht auf Geschlechtsstereotypen " +
                "reduziert zu werden. Meine Qualifikationen und mein Engagement für mein Unternehmen sind entscheidend, unabhängig vom Geschlecht. " +
                "Können wir uns auf die Mietdetails konzentrieren?"
            },

            new VisualNovelEvent()
            {
                id = 60,
                nextId = 40,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Stressmanagement ist wichtig, und ich habe Strategien, um damit umzugehen. " +
                "Aber zurück zur Sache - ich bin sehr daran interessiert, wie der Mietprozess weitergeht. " +
                "Haben Sie noch weitere Fragen zu meinen Unternehmensplänen?"
            },

            new VisualNovelEvent()
            {
                id = 61,
                nextId = 62,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ja, Marktforschung ist integral für den Geschäftserfolg. Ich habe eine umfassende " +
                "Analyse durchgeführt und bin zuversichtlich, dass meine Strategien auf soliden Daten basieren."
            },

            new VisualNovelEvent()
            {
                id = 62,
                nextId = 55,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Wie schön! Ich bin mir sicher, dass Sie das mit Ihrer Frauenpower toll machen werden. " +
                "Ich hoffe nur, dass Sie sich mit so einem Unternehmen nicht doch zu viel Stress zumuten.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 63,
                nextId = 35,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ja, Marktforschung war eine grundlegende Komponente meines Vorbereitungsprozesses. " +
                "Das hatte ich auch schon erwähnt. Ich weiß, welche Schritte für eine Unternehmensgründung wichtig sind."
            },

            new VisualNovelEvent()
            {
                id = 64,
                nextId = 30,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Netzwerken, das ist der Schlüssel zum Erfolg! Gut, dass Sie daran gedacht haben. " +
                "Ich schicke Ihnen später seine Kontaktdaten. Sagen Sie ihm gerne, dass ich Sie geschickt habe. " +
                "Er betont außerdem immer wieder, wie wichtig Marktforschung ist. Haben Sie das richtig gemacht? Es könnte wirklich den Unterschied machen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 65,
                nextId = 30,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Gerne! Netzwerken, das ist der Schlüssel zum Erfolg! Gut, dass Sie schon Leute kennen. " +
                "Mein Freund betont außerdem immer wieder, wie wichtig Marktforschung ist. Haben Sie das richtig gemacht? " +
                "Es könnte wirklich den Unterschied machen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 66,
                nextId = 30,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Oh, ach so. Dann ist ja gut. Mein Freund betont auch immer wieder, wie wichtig Marktforschung ist. " +
                "Haben Sie das richtig gemacht? Es könnte wirklich den Unterschied machen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 67,
                nextId = 68,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Vielen Dank für das Kompliment. Ich schätze die positive Einschätzung meiner Vorbereitung. " +
                "Ich bin fest entschlossen, die Büroflächen effektiv zu nutzen und mein Unternehmen erfolgreich zu führen."
            },

            new VisualNovelEvent()
            {
                id = 68,
                nextId = 69,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Das freut mich zu hören. Es ist wirklich großartig zu sehen, dass Sie sich das zutrauen! " +
                "Wenn Sie irgendwelche Probleme haben, lassen Sie es mich wissen. Frauen können zusätzliche Unterstützung gut gebrauchen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 69,
                nextId = 70,
                onChoice = 73,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Danke für die Unterstützung. Wenn ich spezifische Anliegen habe, werde ich sicherlich darauf zukommen."
            },

            new VisualNovelEvent()
            {
                id = 71,
                nextId = 72,
                onChoice = 65,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich bin bereits mit anderen Unternehmer*innen in Kontakt, aber danke für den Tipp."
            },

            new VisualNovelEvent()
            {
                id = 72,
                nextId = 400,
                onChoice = 74,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte bzgl. den letzten Satz sensibilisieren."
            },

            new VisualNovelEvent()
            {
                id = 73,
                nextId = 30,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Gerne, gerne. Ich helfe, wo ich kann. Ein Freund von mir hat auch gegründet! " +
                "Da habe ich einiges mitbekommen. Er betont immer wieder, wie wichtig Marktforschung ist. " +
                "Haben Sie das richtig gemacht? Es könnte wirklich den Unterschied machen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 74,
                nextId = 75,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich verstehe, dass Ihre Worte nett gemeint sind, aber sie wirken so, als würden " +
                "Frauen immer wieder Hilfe brauchen. Ich schätze Ihr Angebot, aber ich bin fest davon überzeugt, " +
                "dass meine Fähigkeiten und Fachkenntnisse ausreichen, um mein Unternehmen erfolgreich zu führen."
            },

            new VisualNovelEvent()
            {
                id = 75,
                nextId = 30,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Oh, ach so. Dann ist ja gut. Ein Freund von mir hat auch gegründet! Da habe ich einiges mitbekommen. " +
                "Er betont immer wieder, wie wichtig Marktforschung ist. Haben Sie das richtig gemacht? Es könnte wirklich den Unterschied machen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 76,
                nextId = 77,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Hm, wie sieht es denn eigentlich mit den nächsten Schritten aus, um den Mietprozess voranzutreiben? " +
                "Benötigen Sie noch weitere Informationen?"
            },

            new VisualNovelEvent()
            {
                id = 77,
                nextId = 78,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Es gibt einige Standarddokumente, die wir benötigen, um den Mietprozess in Gang zu setzen. " +
                "Aber wissen Sie, die Geschäftswelt kann manchmal hart sein, besonders für Frauen. Sie sind bestimmt ziemlich " +
                "verunsichert, was die Gründung betrifft.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 78,
                nextId = 79,
                onChoice = 75,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich bin gut vorbereitet, deshalb fühle ich mich da sehr sicher."
            },

            new VisualNovelEvent()
            {
                id = 79,
                nextId = 80,
                onChoice = 81,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich komme damit gut zurecht. Welche Standarddokumente benötigen Sie denn?"
            },

            new VisualNovelEvent()
            {
                id = 80,
                nextId = 400,
                onChoice = 65,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ein bisschen Sorge habe ich tatsächlich, aber da spreche ich mit anderen, die bereits gegründet haben darüber."
            },

            new VisualNovelEvent()
            {
                id = 81,
                nextId = 82,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ich will Ihnen ja nur helfen, aber wenn Sie denken, dass Sie diese nicht benötigen, soll es mir recht sein.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 82,
                nextId = 83,
                onChoice = 85,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Das verstehe ich und bin auch sehr dankbar und hoffe deshalb, dass der Mietprozess reibungslos ablaufen wird."
            },

            new VisualNovelEvent()
            {
                id = 83,
                nextId = 84,
                onChoice = 85,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich schätze Ihre Hilfe, aber ich möchte mich gerne auf den Mietprozess konzentrieren."
            },

            new VisualNovelEvent()
            {
                id = 84,
                nextId = 400,
                onChoice = 86,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ja, aber ich brauche von Ihnen keine Hilfe. Können wir uns bitte auf den Mietprozess konzenrtieren?"
            },

            new VisualNovelEvent()
            {
                id = 85,
                nextId = 30,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ahh, ich versehe. Ich helfe, wo ich kann. Übrigens! Ein Freund von mir hat auch gegründet! " +
                "Da habe ich einiges mitbekommen. Er betont immer wieder, wie wichtig Marktforschung ist. " +
                "Haben Sie das richtig gemacht? Es könnte wirklich den Unterschied machen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 86,
                nextId = 30,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Aber ein Freund von mir hat auch gegründet! Da habe ich einiges mitbekommen. " +
                "Er betont immer wieder, wie wichtig Marktforschung ist. Haben Sie das richtig gemacht? Es könnte wirklich den Unterschied machen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 87,
                nextId = 88,
                onChoice = 90,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ja, ich miete zum ersten Mal und habe aber alles berücksichtigt."
            },

            new VisualNovelEvent()
            {
                id = 88,
                nextId = 89,
                onChoice = 90,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich habe schonmal Büroflächen gemietet und weiß, worauf ich achten muss."
            },

            new VisualNovelEvent()
            {
                id = 89,
                nextId = 400,
                onChoice = 102,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich habe noch eine Frage zu den Büroflächen."
            },

            new VisualNovelEvent()
            {
                id = 90,
                nextId = 91,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Das freut mich zu hören, aber wir möchten sicherstellen, dass Mieter*innen langfristig gut zurechtkommen. Gibt es besondere Umstände oder Faktoren, die Ihre finanzielle Situation beeinflussen könnten?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 91,
                nextId = 92,
                onChoice = 93,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich stelle meine Vorbereitung klar.",
                show = false
            },

            new VisualNovelEvent()
            {
                id = 92,
                nextId = 400,
                onChoice = 101,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich frage nach, was gemeint ist.",
                show = false
            },

            new VisualNovelEvent()
            {
                id = 93,
                nextId = 94,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich habe meine finanzielle Situation sorgfältig analysiert und bin gut vorbereitet, " +
                "die Miete langfristig zu tragen. Sollten Sie spezifische Unterlagen benötigen, um dies zu bestätigen, bin ich bereit, diese bereitzustellen."
            },

            new VisualNovelEvent()
            {
                id = 94,
                nextId = 95,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "In manchen Fällen gibt es besondere Umstände, die die finanzielle Stabilität beeinflussen können. " +
                "Ich will hier wirklich nur unterstützen und auf Nummer Sicher gehen. Haben Sie vielleicht besondere Planungen " +
                "oder Änderungen in Ihrer Lebenssituation, die sich auf Ihre finanzielle Situation auswirken könnten?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 95,
                nextId = 96,
                onChoice = 77,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich schicke Ihnen gerne alle notwendigen Dokumente."
            },

            new VisualNovelEvent()
            {
                id = 96,
                nextId = 400,
                onChoice = 97,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Sprechen Sie von einer möglichen Schwangerschaft?"
            },

            new VisualNovelEvent()
            {
                id = 97,
                nextId = 98,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Entschuldigen Sie bitte jegliche Missverständnisse. So war das nicht gemeint.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 98,
                nextId = 99,
                onChoice = 100,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich schätze Ihre Entschuldigung. Wenn es keine weiteren persönlichen Fragen gibt, können wir uns darauf konzentrieren, wie wir den Mietprozess vorantreiben."
            },

            new VisualNovelEvent()
            {
                id = 99,
                nextId = 400,
                onChoice = 81,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich verstehe, aber so eine persönliche Frage hat hier nichts zu suchen."
            },

            new VisualNovelEvent()
            {
                id = 100,
                nextId = 55,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Danke für Ihr Verständnis. Ich bin mir sicher, dass Sie Ihr Unternemen mit Ihrer " +
                "Frauenpower toll leiten werden. Ich hoffe nur, dass Sie sich mit so einem Unternehmen nicht doch zu viel Stress zumuten.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 101,
                nextId = 94,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Könnten Sie bitte genauer darauf eingehen, welche besonderen Umstände oder Faktoren " +
                "Sie im Kopf haben? Ich möchte sicherstellen, dass wir alle relevanten Aspekte klären können."
            },

            new VisualNovelEvent()
            {
                id = 102,
                nextId = 103,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Was für eine Frage haben Sie denn? Sie haben sich ja wirklich Gedanken zu all dem gemacht! Das hätte ich gar nicht erwartet.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 103,
                nextId = 104,
                onChoice = 105,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Besteht die Möglichkeit, die Büroflächen nach den Bedürfnissen des Unternehmens anzupassen oder zu erweitern?"
            },

            new VisualNovelEvent()
            {
                id = 104,
                nextId = 400,
                onChoice = 115,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Wieso hätten Sie das nicht erwartet?"
            },

            new VisualNovelEvent()
            {
                id = 105,
                nextId = 106,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Was für eine Frage haben Sie denn? Sie haben sich ja wirklich Gedanken zu all dem gemacht! Das hätte ich gar nicht erwartet.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 106,
                nextId = 107,
                onChoice = 109,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Schön zu hören, dass das kein Problem darstellt."
            },

            new VisualNovelEvent()
            {
                id = 107,
                nextId = 108,
                onChoice = 110,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Wieso sollte ich meinen Mann mitbringen?"
            },

            new VisualNovelEvent()
            {
                id = 108,
                nextId = 400,
                onChoice = 114,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Vielen Dank für das Angebot. Für geschäftliche Entscheidungen bin ich eigenverantwortlich."
            },

            new VisualNovelEvent()
            {
                id = 109,
                nextId = 30,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ja, als Frau kann es schon manchmal schwieriger sein bestimmte Gespräche zu führen, " +
                "aber es ist wirklich toll, dass Sie sich das zutrauen! Ein Freund von mir hat auch gegründet! " +
                "Da habe ich einiges mitbekommen. Er betont immer wieder, wie wichtig Marktforschung ist. " +
                "Haben Sie das richtig gemacht? Es könnte wirklich den Unterschied machen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 110,
                nextId = 111,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Nun ja, vielleicht kennt er sich da ja etwas besser aus und vier Augen sehen ja auch mehr als zwei.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 111,
                nextId = 112,
                onChoice = 113,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich sage, dass das nicht OK ist. Immerhin bin ich die Gründerin.",
                show = false
            },

            new VisualNovelEvent()
            {
                id = 112,
                nextId = 400,
                onChoice = 77,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Das kann ich nachvollziehen, aber lassen Sie uns doch auf den Mietprozess an sich zurückkommen. Benötigen Sie noch weitere Informationen?"
            },

            new VisualNovelEvent()
            {
                id = 113,
                nextId = 81,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ehrlich gesagt, ist es nicht in Ordnung, dass Sie annehmen, dass ich einen Ehemann " +
                "habe und dass dieser sich besser auskennen könnte. Schließlich bin ich diejenige, die ein Unternehmen gründet."
            },

            new VisualNovelEvent()
            {
                id = 114,
                nextId = 25,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Stimmt, gut, dass Sie so handeln! Aber Frauen haben manchmal zusätzliche Herausforderungen " +
                "in der Geschäftswelt. Ein Freund von mir hat auch gegründet! Da habe ich einiges mitbekommen. Sie müssen " +
                "nämlich auch ein Notariat aufsuchen und ihr Unternehmen ins Handelsregister eintragen lassen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 115,
                nextId = 20,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Oh, das war vielleicht etwas ungeschickt ausgedrückt. Normalerweise treffe ich auf " +
                "viele Interessenten, die nicht so gründlich vorbereitet sind. Ihre Planung hat mich positiv überrascht. " +
                "Frauen neigen dazu, mehr Wert auf die Details zu legen, nicht wahr?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 116,
                nextId = 117,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Das habe ich bereits und es wird keine finanzielle Probleme geben."
            },

            new VisualNovelEvent()
            {
                id = 117,
                nextId = 118,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ich mache mir nur große Sorgen, um Sie. Finanzen... Also, da sollten Sie sich doch nicht Ihr hübsches Köpfchen darüber zerbrechen müssen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 118,
                nextId = 119,
                onChoice = 121,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte sensibilisieren.",
                show = false
            },

            new VisualNovelEvent()
            {
                id = 119,
                nextId = 120,
                onChoice = 76,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich lenke das Gespräch auf die Büroflächen.",
                show = false
            },

            new VisualNovelEvent()
            {
                id = 120,
                nextId = 400,
                onChoice = 122,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich sage klar, dass das sexistisch ist.",
                show = false
            },

            new VisualNovelEvent()
            {
                id = 121,
                nextId = 75,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich verstehe, dass Ihre Worte nett gemeint sind, aber sie wirken so, als würden Frauen " +
                "immer wieder Hilfe brauchen. Ich schätze Ihr Angebot, aber ich bin fest davon überzeugt, dass " +
                "meine Fähigkeiten und Fachkenntnisse ausreichen, um mein Unternehmen erfolgreich zu führen."
            },

            new VisualNovelEvent()
            {
                id = 122,
                nextId = 81,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich verstehe, dass Sie vielleicht besorgt sind, aber solche Kommentare über mein 'hübsches Köpfchen' " +
                "sind sexistisch. Ich bevorzuge es, wenn unsere Gespräche sich auf geschäftliche Aspekte konzentrieren."
            },            

            new VisualNovelEvent()
            {
                id = 400,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT),
                waitForUserConfirmation = true
            },

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
            }
        };
    }
}