using System.Collections.Generic;
using UnityEngine;

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
                text = "Bei deinem Banktermin heute möchtest du ein Firmenkonto eröffnen. Du hast deinen Businessplan und alle weiteren benötigten Dokumente bei dir."
            },

            new VisualNovelEvent()
            {
                id = 4,
                nextId = 5,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Willkommen, Frau ... Haben Sie alle Unterlagen bei sich?",  //TODO: add name
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 5,
                nextId = 500,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ja. Ab Seite 26 finden Sie meine Finanzplanung.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 500,
                nextId = 6,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.PLAY_SOUND_EVENT),
                waitForUserConfirmation = false,
                audioClipToPlay = SoundEnumHelper.ToInt(SoundsEnum.PAPER_SOUND)
            },

            new VisualNovelEvent()
            {
                id = 6,
                nextId = 7,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Vielen Dank. (Überfliegt Businessplan) Nach einer kurzen Überprüfung habe ich das Gefühl, dass Ihre Geschäftsidee in eine Risikobranche fällt, was wir nur ungern unterstützen.", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.QUESTIONING)
            },

            new VisualNovelEvent()
            {
                id = 7,
                nextId = 8,
                onChoice = 9,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Meine Marktrecherche hat ergeben, dass es genug Kund*innen gibt und ich regelmäßig Einnahmen erzielen kann. Das können Sie alles auf Seite 14 nachlesen."
            },

            new VisualNovelEvent()
            {
                id = 8,
                nextId = 400,
                onChoice = getRandomNumber(9, 11),
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Bitte lesen Sie sich den Plan aufmerksamer durch. Dann werden Sie erkennen, dass es sich nicht um eine Risikobranche handelt."
            },

            new VisualNovelEvent()
            {
                id = 9,
                nextId = 501,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.PLAY_SOUND_EVENT),
                waitForUserConfirmation = false,
                audioClipToPlay = SoundEnumHelper.ToInt(SoundsEnum.PAPER_SOUND)
            },

            new VisualNovelEvent()
            {
                id = 501,
                nextId = 12,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "(Liest sich entsprechende Seiten nochmal durch) Jetzt nach erneutem Lesen, nehme ich meine Antwort zurück. Haben Sie das ganz alleine geschrieben? Das liest sich wirklich nicht schlecht.", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.ASTONISHED)
            },

            new VisualNovelEvent()
            {
                id = 10,
                nextId = 74,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "In dem Fall werde ich den Businessplan noch einmal prüfen. Allerdings wird das etwas dauern.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 11,
                nextId = 106,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Nein, uns ist es zu riskant, aber Sie können es ja noch bei einer anderen Bank versuchen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 12,
                nextId = 13,
                onChoice = 15,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ja, ich habe das alleine geschrieben. Falls noch Unklarheiten bestehen, können wir die gerne besprechen."
            },

            new VisualNovelEvent()
            {
                id = 13,
                nextId = 14,
                onChoice = 15,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Da freut sich meine innere Autorin! Ja, ich habe den Plan eigenhändig verfasst."
            },

            new VisualNovelEvent()
            {
                id = 14,
                nextId = 400,
                onChoice = 520,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Was meinen Sie damit?"
            },

            new VisualNovelEvent()
            {
                id = 15,
                nextId = 16,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ich bin beeindruckt. Das ist für eine Frau eine ziemliche Leistung!",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.ASTONISHED)
            },

            new VisualNovelEvent()
            {
                id = 16,
                nextId = 17,
                onChoice = 19,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Danke! Aber wissen Sie, ich habe gehört, Frauen können eben auch beeindruckende Geschäfte ins Leben rufen. Wir brechen eben gern mit Klischees."
            },

            new VisualNovelEvent()
            {
                id = 17,
                nextId = 18,
                onChoice = 39,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Danke, aber Geschlecht sollte keine Rolle spielen, wenn es um unternehmerischen Erfolg geht. Ich bin überzeugt, dass harte Arbeit und Fachkenntnisse die entscheidenden Faktoren sind."
            },

            new VisualNovelEvent()
            {
                id = 18,
                nextId = 400,
                onChoice = 70,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Wieso sollte das für eine Frau eine ziemliche Leistung sein?"
            },

            new VisualNovelEvent()
            {
                id = 19,
                nextId = 20,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Haha, ja natürlich! Ich finde es auch wirklich toll, dass Sie dieses Projekt angehen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 20,
                nextId = 21,
                onChoice = 23,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Danke, lassen Sie uns doch wieder über das Firmenkonto sprechen."
            },

            new VisualNovelEvent()
            {
                id = 21,
                nextId = 22,
                onChoice = 23,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich auch! Deshalb möchte ich nochmal auf das Firmenkonto zu sprechen kommen."
            },

            new VisualNovelEvent()
            {
                id = 22,
                nextId = 400,
                onChoice = 35,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Danke, aber es wäre schön, wenn wir alle mehr auf unsere Wortwahl achten könnten. Schließlich haben Worte Macht, auch wenn Sie es nicht böse meinen."
            },

            new VisualNovelEvent()
            {
                id = 23,
                nextId = 24,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Gerne. Es würde mich wirklich freuen, wenn Sie bei uns ein Konto eröffnen. Vielleicht könnten wir das als Bank auch als Werbung für uns nutzen und damit auch Werbung für Sie und Ihr Unternehmen machen!",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 24,
                nextId = 25,
                onChoice = 27,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Die Idee klingt interessant. Könnten Sie mir mehr darüber erzählen, wie wir diese Werbemöglichkeiten konkret gestalten könnten?"
            },

            new VisualNovelEvent()
            {
                id = 25,
                nextId = 26,
                onChoice = 27,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Wie haben Sie sich das denn vorgestellt? Wie soll die Werbung aussehen?"
            },

            new VisualNovelEvent()
            {
                id = 26,
                nextId = 400,
                onChoice = 34,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich eröffne gerne ein Konto, aber an der Werbung bin ich nicht interessiert. Trotzdem danke für das Angebot."
            },

            new VisualNovelEvent()
            {
                id = 27,
                nextId = 28,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Also, das ist jetzt erstmal nur eine spontane Idee, aber Sie als Unternehmerin auf einem Plakat, das mit einem Werbespruch versehen ist. Z. B. 'Mit Stil zum Erfolg - Frauen, die Bankgeschäfte elegant managen!' oder 'Bankgeschäfte so einfach wie ein Rezept! Frauen managen mühelos Finanzen und Familie.' oder so ähnlich.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.LAUGHING)
            },

            new VisualNovelEvent()
            {
                id = 28,
                nextId = 29,
                onChoice = 508,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte erstmal nur das Firmenkonto eröffnen und melde mich nochmal bei Ihnen wegen der Werbung."
            },

            new VisualNovelEvent()
            {
                id = 29,
                nextId = 400,
                onChoice = 34,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich denke, diese Werbung passt nicht zu mir. Ich möchte nur das Firmenkonto eröffnen."
            },

            new VisualNovelEvent()
            {
                id = 508,
                nextId = 30,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.PLAY_SOUND_EVENT),
                waitForUserConfirmation = false,
                audioClipToPlay = SoundEnumHelper.ToInt(SoundsEnum.MAN_LAUGHING)
            },

            new VisualNovelEvent()
            {
                id = 30,
                nextId = 31,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Das würde mich sehr freuen! Hier sind die Formulare, die Sie ausfüllen müssen. Sie können die dort am Tisch ausfüllen und vorne bei der Kollegin abgeben. Wir müssen danach noch eine Legitimationsprüfung durchführen. Nach erfolgreicher Prüfung und Abschluss der Formalitäten wird das Firmenkonto dann eingerichtet. In einigen Tagen erhalten Sie von uns einen Brief mit allen weiteren Unterlagen und Informationen.", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.LAUGHING)
            },

            new VisualNovelEvent()
            {
                id = 31,
                nextId = 32,
                onChoice = 33,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Okay, vielen Dank und noch einen schönen Tag!"
            },

            new VisualNovelEvent()
            {
                id = 32,
                nextId = 400,
                onChoice = 401,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Alles klar, danke für Ihre Hilfe. Auf Wiedersehen."
            },

            new VisualNovelEvent()
            {
                id = 33,
                nextId = 401,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Gerne und auf Wiedersehen! ",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 34,
                nextId = 31,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Gar kein Problem. Hier sind die Formulare, die Sie ausfüllen müssen. Sie können die dort am Tisch ausfüllen und vorne bei der Kollegin abgeben. Wir müssen danach noch eine Legitimationsprüfung durchführen. Nach erfolgreicher Prüfung und Abschluss der Formalitäten wird das Firmenkonto dann eingerichtet. In einigen Tagen erhalten Sie von uns einen Brief mit allen weiteren Unterlagen und Informationen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 35,
                nextId = 36,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ja, die Äußerung war wirklich nicht gut durchdacht. Jedenfalls freue ich mich darauf, mit Ihnen zusammenzuarbeiten und Ihre unternehmerischen Erfolge zu unterstützen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.DEFEATED),
            },

            new VisualNovelEvent()
            {
                id = 36,
                nextId = 37,
                onChoice = 167,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Das freut mich sehr."
            },

            new VisualNovelEvent()
            {
                id = 37,
                nextId = 400,
                onChoice = 167,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Danke, wie können wir denn weiter vorgehen?"
            },

            new VisualNovelEvent()
            {
                id = 39,
                nextId = 41,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ja, selbstverständlich. So war das ja auch gar nicht gemeint, aber das wissen Sie doch.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.DEFEATED),
            },

            new VisualNovelEvent()
            {
                id = 41,
                nextId = 42,
                onChoice = 23,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Lassen Sie uns doch wieder über das Firmenkonto sprechen."
            },

            new VisualNovelEvent()
            {
                id = 42,
                nextId = 400,
                onChoice = 43,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Wie war es denn gemeint?"
            },

            new VisualNovelEvent()
            {
                id = 43,
                nextId = 44,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Na ja, es war eben unterstützend gemeint. Ich dachte mir, dass es Ihnen bestimmt nicht leicht gefallen ist.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
            },

            new VisualNovelEvent()
            {
                id = 44,
                nextId = 45,
                onChoice = 46,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Und wieso denken Sie das?"
            },

            new VisualNovelEvent()
            {
                id = 45,
                nextId = 400,
                onChoice = 23,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ach so, ich verstehe. Nun ja, ich möchte nochmal auf das Firmenkonto zu sprechen kommen."
            },

            new VisualNovelEvent()
            {
                id = 46,
                nextId = 47,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Also, so einen Businessplan zu schreiben, ist ja schon etwas komplizierter. Ich verstehe gar nicht so ganz, warum Sie so auf ein Kompliment herumreiten.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.QUESTIONING),
            },

            new VisualNovelEvent()
            {
                id = 47,
                nextId = 48,
                onChoice = 49,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Weil es kein Kompliment ist. Ihre Aussage impliziert, dass Frauen normalerweise nicht in der Lage sind, einen guten Businessplan zu erstellen."
            },

            new VisualNovelEvent()
            {
                id = 48,
                nextId = 400,
                onChoice = 23,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ach, ist schon in Ordnung. Reden wir doch nochmal über die Eröffnung des Firmenkontos."
            },

            new VisualNovelEvent()
            {
                id = 49,
                nextId = 50,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Jetzt übertreiben Sie aber. Das habe ich doch gar nicht gesagt!",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.ASTONISHED),
            },

            new VisualNovelEvent()
            {
                id = 50,
                nextId = 51,
                onChoice = getRandomNumber(53,54),
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Aber das steckt in der Aussage mit drin und das ist nicht nur unprofessionell, sondern auch sexistisch und diskriminierend."
            },

            new VisualNovelEvent()
            {
                id = 51,
                nextId = 52,
                onChoice = 57,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Vergessen Sie es einfach. Ich möchte lediglich ein Firmenkonto eröffnen."
            },

            new VisualNovelEvent()
            {
                id = 52,
                nextId = 400,
                onChoice = 57,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ach, schon in Ordnung. Konzentrieren wir uns doch auf das Firmenkonto."
            },

            new VisualNovelEvent()
            {
                id = 53,
                nextId = 55,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ich verstehe, dass Sie das so empfinden, aber ich habe es wirklich nicht sexistisch gemeint. Manchmal kommen Dinge anders rüber als beabsichtigt. ",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.DEFEATED),
            },

            new VisualNovelEvent()
            {
                id = 54,
                nextId = 68,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Das war überhaupt nicht sexistisch! Sie müssen Dinge nicht überinterpretieren. Wenn Sie so sensibel sind, sollten Sie vielleicht überlegen, ob Sie im Geschäftsumfeld richtig aufgehoben sind. Ich habe keine Lust auf solche Anschuldigungen!",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.DECISION_NO),
            },

            new VisualNovelEvent()
            {
                id = 55,
                nextId = 56,
                onChoice = 57,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich verstehe. Können wir nochmal über das Firmenkonto sprechen?"
            },

            new VisualNovelEvent()
            {
                id = 56,
                nextId = 400,
                onChoice = 58,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich denke, ich möchte lieber das Firmenkonto doch bei einer anderen Bank eröffnen."
            },

            new VisualNovelEvent()
            {
                id = 57,
                nextId = 300,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Wie gesagt, habe ich das Gefühl, dass Ihre Geschäftsidee in eine Risikobranche fällt, was wir nun mal ungern unterstützen. Wir melden uns bei Ihnen, nachdem wir Ihren Businessplan nochmal geprüft haben.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.NONE),
            },

            new VisualNovelEvent()
            {
                id = 300,
                nextId = 33,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "In Ordnung. Danke für Ihre Zeit, auf Wiedersehen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.NONE),
            },

            new VisualNovelEvent()
            {
                id = 58,
                nextId = 59,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ach so? Sie haben da natürlich die freie Wahl, aber diese plötzliche Umentscheidung wundert mich doch etwas. Das wirkt auf mich als seien Sie da etwas emotional unterwegs.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.ASTONISHED),
            },

            new VisualNovelEvent()
            {
                id = 59,
                nextId = 60,
                onChoice = 62,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich habe meine Entscheidung nicht leichtfertig getroffen. Es ist wichtig für mich, in einer Bank zu sein, die meine Geschäftsphilosophie und -prinzipien unterstützt. Vielen Dank für Ihre Zeit und Beratung."
            },

            new VisualNovelEvent()
            {
                id = 60,
                nextId = 61,
                onChoice = 62,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Nach unserer Unterhaltung denke ich, dass eine andere Bank besser zu meinen Vorstellungen passt. Ich schätze Ihre Zeit und danke für Ihre bisherige Beratung."
            },

            new VisualNovelEvent()
            {
                id = 61,
                nextId = 400,
                onChoice = 65,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Es war interessant, mit Ihnen zu sprechen, aber ich glaube, wir haben unterschiedliche Ansichten darüber, wie eine professionelle Zusammenarbeit aussehen sollte. Daher denke ich, es ist besser, wenn ich mich anderweitig umsehe. Vielen Dank für Ihre Zeit."
            },

            new VisualNovelEvent()
            {
                id = 62,
                nextId = 63,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Oh, das ist schade, aber ich verstehe. Zeigt ja doch, dass Frauen dazu neigen, vorsichtiger zu sein. Wenn Sie Ihre Meinung ändern und die Vorteile unserer Unterstützung erkennen, können Sie natürlich gerne zurückkommen. Frauen müssen eben manchmal lernen, sich von ihrer Risikoaversion zu befreien, aber wir kümmern uns gerne um unsere Kundinnen. Ich wünsche Ihnen aber viel Erfolg bei Ihren weiteren Bemühungen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
            },

            new VisualNovelEvent()
            {
                id = 63,
                nextId = 64,
                onChoice = 33,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Schönen Tag noch."
            },

            new VisualNovelEvent()
            {
                id = 64,
                nextId = 400,
                onChoice = 33,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ihre Aussage bekräftigt meine Entscheidung nur weiter. Ich suche eine Bank, die meine unternehmerische Professionalität schätzt, unabhängig von Geschlechtsstereotypen. Einen schönen Tag noch."
            },

            new VisualNovelEvent()
            {
                id = 65,
                nextId = 66,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Schade, dass Sie sich so entschieden haben. Vielleicht ist es für Sie als Frau tatsächlich etwas zu anspruchsvoll. Aber wie gesagt, haben Sie die freie Wahl. Wenn Sie sich wieder sicher fühlen und professionelle Unterstützung brauchen, wissen Sie, wo Sie uns finden.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
            },

            new VisualNovelEvent()
            {
                id = 66,
                nextId = 67,
                onChoice = 33,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Es ist für mich nicht zu anspruchsvoll. Ich möchte aber nicht weiter mit Ihrem Sexismus augesetzt sein. Schön Tag noch."
            },

            new VisualNovelEvent()
            {
                id = 67,
                nextId = 400,
                onChoice = 33,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Interessant, dass Sie mein Geschlecht als Grund für meine Entscheidung sehen. Ich entscheide aufgrund meiner Fähigkeiten und Prinzipien. Wenn Sie Ihre Einstellung überdenken sollten, wissen Sie, wo Sie mich finden, falls Sie nach einer moderneren Perspektive suchen. Schön Tag noch."
            },

            new VisualNovelEvent()
            {
                id = 68,
                nextId = 69,
                onChoice = 33,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte nicht, dass dies zu einer persönlichen Angelegenheit wird. Vielleicht können Sie sich nochmal informieren, was Sexismus ist. Schönen Tag noch."
            },

            new VisualNovelEvent()
            {
                id = 69,
                nextId = 400,
                onChoice = 33,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich dulde keine sexistischen Äußerungen oder Diskriminierung. Ich suche mir lieber eine andere Bank. Schönen Tag noch."
            },

            new VisualNovelEvent()
            {
                id = 70,
                nextId = 71,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Oh, das war vielleicht unglücklich formuliert. Ich wollte Sie damit nur ermuntern. Sie haben wirklich gute Arbeit geleistet.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.DEFEATED),
            },

            new VisualNovelEvent()
            {
                id = 71,
                nextId = 72,
                onChoice = 23,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Lassen Sie uns doch wieder über das Firmenkonto sprechen."
            },

            new VisualNovelEvent()
            {
                id = 72,
                nextId = 400,
                onChoice = 35,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Danke, aber es wäre schön, wenn wir alle mehr auf unsere Wortwahl achten könnten. Schließlich haben Worte Macht, auch wenn Sie es nicht böse meinen."
            },

            new VisualNovelEvent()
            {
                id = 520,
                nextId = 73,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.PLAY_SOUND_EVENT),
                waitForUserConfirmation = false,
                audioClipToPlay = SoundEnumHelper.ToInt(SoundsEnum.MAN_LAUGHING)
            },

            new VisualNovelEvent()
            {
                id = 73,
                nextId = 509,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ich meine, es ist doch ziemlich gut. Also für eine Frau ist das ja eine echte Leistung!",   
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.LAUGHING),
            },

            new VisualNovelEvent()
            {
                id = 509,
                nextId = 74,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.PLAY_SOUND_EVENT),
                waitForUserConfirmation = false,
                audioClipToPlay = SoundEnumHelper.ToInt(SoundsEnum.MAN_LAUGHING)
            },

            new VisualNovelEvent()
            {
                id = 74,
                nextId = 16,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ich meine, es ist doch ziemlich gut. Also für eine Frau ist das ja eine echte Leistung!",  
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.LAUGHING),
            },

            new VisualNovelEvent()
            {
                id = 75,
                nextId = 76,
                onChoice = 77,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Natürlich, nehmen Sie sich gerne die Zeit. Ich bin aber überzeugt, dass mein Geschäftsmodell solide ist und keine unnötigen Risiken birgt. Wenn Sie konkrete Fragen haben, können wir die gerne jetzt klären."
            },

            new VisualNovelEvent()
            {
                id = 76,
                nextId = 400,
                onChoice = 105,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Können Sie sich das direkt jetzt nochmal anschauen?"
            },

            new VisualNovelEvent()
            {
                id = 77,
                nextId = 78,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Schon gut. Sie sind aber selbstbewusst und überzeugt.",  
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.ASTONISHED),
            },

            new VisualNovelEvent()
            {
                id = 78,
                nextId = 79,
                onChoice = 510,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Klar, wenn nicht ich, wer dann?"
            },

            new VisualNovelEvent()
            {
                id = 79,
                nextId = 400,
                onChoice = 84,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Wieso sollte ich das nicht sein?"
            },

            new VisualNovelEvent()
            {
                id = 510,
                nextId = 80,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.PLAY_SOUND_EVENT),
                waitForUserConfirmation = false,
                audioClipToPlay = SoundEnumHelper.ToInt(SoundsEnum.MAN_LAUGHING)
            },

            new VisualNovelEvent()
            {
                id = 80,
                nextId = 81,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Haha, Sie gefallen mir. Wissen Sie was? Ich habe sowieso noch etwas Zeit, dann schaue ich jetzt doch nochmal über Ihren Businessplan drüber.",  
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.LAUGHING),
            },

            new VisualNovelEvent()
            {
                id = 81,
                nextId = 82,
                onChoice = 83,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Das freut mich!"
            },

            new VisualNovelEvent()
            {
                id = 82,
                nextId = 400,
                onChoice = 83,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Klasse, vielen Dank."
            },

            new VisualNovelEvent()
            {
                id = 83,
                nextId = 501,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "(Papier raschelt) Jetzt nach erneutem Lesen, nehme ich meine Antwort zurück. Haben Sie das ganz alleine geschrieben? Das liest sich wirklich nicht schlecht.",  
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.ASTONISHED),
            },

            new VisualNovelEvent()
            {
                id = 84,
                nextId = 85,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Nun ja, es wundert mich ein bisschen, dass Sie als Frau solch ein Risiko eingehen mit einer Unternehmensgründung. Hätte ich nicht gedacht.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.ASTONISHED),
            },

            new VisualNovelEvent()
            {
                id = 85,
                nextId = 86,
                onChoice = 88,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Wieso denn?"
            },

            new VisualNovelEvent()
            {
                id = 86,
                nextId = 87,
                onChoice = 103,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich habe alles so geplant, sodass die Risiken minimal sind."
            },

            new VisualNovelEvent()
            {
                id = 87,
                nextId = 400,
                onChoice = 104,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Das war jetzt aber sexistisch."
            },

            new VisualNovelEvent()
            {
                id = 88,
                nextId = 85,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Frauen sind doch normalerweise vorsichtiger, was bspw. Investitionen betrifft.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.ASTONISHED),
            },

            new VisualNovelEvent()
            {
                id = 89,
                nextId = 90,
                onChoice = 23,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ach, lassen Sie uns doch über das Firmenkonto sprechen."
            },

            new VisualNovelEvent()
            {
                id = 90,
                nextId = 91,
                onChoice = 92,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Es gibt doch keine einheitliche Regel, die besagt, dass Frauen per se vorsichtiger bei Investitionen sind."
            },

            new VisualNovelEvent()
            {
                id = 91,
                nextId = 400,
                onChoice = getRandomNumber(96, 98),
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich kann verstehen, dass es bestimmte Vorstellungen darüber gibt, wie Frauen und Männer mit Finanzen umgehen. Aber ich denke, es ist wichtig, die Vielfalt individueller Ansätze zu erkennen."
            },

            new VisualNovelEvent()
            {
                id = 92,
                nextId = 93,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Es gibt unterschiedliche Meinungen dazu, wie Geschlecht die Anlageentscheidungen beeinflussen kann. Unabhängig davon, welcher Standpunkt vertreten wird, ist es unser Ziel, Sie in Ihren finanziellen Zielen zu unterstützen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
            },

            new VisualNovelEvent()
            {
                id = 93,
                nextId = 94,
                onChoice = 95,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich verstehe. Dann möchte ich gerne auf das Firmenkonto zu sprechen kommen."
            },

            new VisualNovelEvent()
            {
                id = 94,
                nextId = 400,
                onChoice = 58,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich denke, ich möchte lieber das Firmenkonto doch bei einer anderen Bank eröffnen."
            },

            new VisualNovelEvent()
            {
                id = 95,
                nextId = 12,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Wie gesagt, habe ich das Gefühl, dass Ihre Geschäftsidee in eine Risikobranche fällt, was wir nun mal ungern unterstützen. Aber ich habe noch etwas Zeit, da kann ich auch nochmal drüberlesen. (Papier raschelt) Jetzt nach erneutem Lesen, nehme ich meine Antwort zurück. Haben Sie das ganz alleine geschrieben? Das liest sich wirklich nicht schlecht.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.ASTONISHED),
            },

            new VisualNovelEvent()
            {
                id = 96,
                nextId = 99,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Entschuldigen Sie bitte, wenn meine Worte unangemessen wirkten. Das war nicht meine Absicht. Lassen Sie uns doch weiter über Ihr Geschäft sprechen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.DEFEATED),
            },

            new VisualNovelEvent()
            {
                id = 97,
                nextId = 101,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Oh, das tut mir leid, wenn das so rüberkam. Sie sind einfach selbstbewusst, und das ist ja auch gut so. Lassen Sie uns doch weiter über Ihr Geschäft sprechen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.DEFEATED),
            },

            new VisualNovelEvent()
            {
                id = 98,
                nextId = 99,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ich denke, Sie nehmen das zu persönlich. Ich versuche nur, realistisch zu sein. Aber wenn Sie sich so sicher sind, dann lassen Sie uns weitermachen. Frauen haben eben manchmal andere Ansichten.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.REFUSING),
            },

            new VisualNovelEvent()
            {
                id = 99,
                nextId = 100,
                onChoice = 95,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Wenn Sie den Businessplan nochmal geprüft haben, möchte ich gerne weiterhin ein Firmenkonto eröffnen."
            },

            new VisualNovelEvent()
            {
                id = 100,
                nextId = 400,
                onChoice = 58,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich denke, ich möchte lieber das Firmenkonto doch bei einer anderen Bank eröffnen."
            },

            new VisualNovelEvent()
            {
                id = 101,
                nextId = 102,
                onChoice = 95,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Danke, wenn Sie den Businessplan nochmal geprüft haben, möchte ich gerne weiterhin ein Firmenkonto eröffnen."
            },

            new VisualNovelEvent()
            {
                id = 102,
                nextId = 400,
                onChoice = 58,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich denke, ich möchte lieber das Firmenkonto doch bei einer anderen Bank eröffnen."
            },

            new VisualNovelEvent()
            {
                id = 103,
                nextId = 89,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Klasse. Frauen sind ja normalerweise auch vorsichtiger und achten besser auf solche Details. Das machen Sie schon echt gut.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
            },

            new VisualNovelEvent()
            {
                id = 104,
                nextId = 89,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Sexismus ist direkt so ein starker Vorwurf. Ich bin doch nicht sexistisch. So war das ja auch gar nicht gemeint, aber das wissen Sie doch.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.PROUD),
            },

            new VisualNovelEvent()
            {
                id = 105,
                nextId = 12,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Nun gut. Etwas Zeit habe ich für diesen Termin sowieso noch eingeplant. (Papier raschelt) Jetzt nach erneutem Lesen, nehme ich meine Antwort zurück. Haben Sie das ganz alleine geschrieben? Das liest sich wirklich nicht schlecht.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.ASTONISHED),
            },

            new VisualNovelEvent()
            {
                id = 106,
                nextId = 107,
                onChoice = 108,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Mein Businessplan und meine Marktanalysen belegen aber, dass mein Unternehmen solide aufgestellt ist und kein unnötiges Risiko birgt. Vielleicht könnten wir gemeinsam genauer auf die Daten schauen, die diese Risikoeinschätzung begründen?"
            },

            new VisualNovelEvent()
            {
                id = 107,
                nextId = 400,
                onChoice = 113,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Das verstehe ich, aber könnten wir vielleicht darüber sprechen, wie ich das Risiko minimieren könnte? Ich habe einige Strategien entwickelt, um potenzielle Risiken zu mindern, die ich gerne mit Ihnen teilen würde."
            },

            new VisualNovelEvent()
            {
                id = 108,
                nextId = 109,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ich verstehe Ihren Standpunkt, aber unsere Richtlinien sind leider recht strikt in Bezug auf bestimmte Branchen. Schließlich können Sie nicht garantieren, ob Sie Ihre Kundschaft halten können.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL),
            },

            new VisualNovelEvent()
            {
                id = 109,
                nextId = 110,
                onChoice = 111,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Garantieren kann ich das nicht, aber wer kann das schon?"
            },

            new VisualNovelEvent()
            {
                id = 110,
                nextId = 400,
                onChoice = 112,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Auf Seite 15 können Sie nachlesen, wie ich nicht nur Kundschaft halten werde, sondern welche konkreten Schritte geplant sind, um immer mehr Kundschaft zu gewinnen."
            },

            new VisualNovelEvent()
            {
                id = 111,
                nextId = 510,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Haha, das stimmt allerdings. Sie gefallen mir. Wissen Sie was? Ich habe sowieso noch etwas Zeit, dann schaue ich jetzt doch nochmal über Ihren Businessplan drüber.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.LAUGHING),
            },

            new VisualNovelEvent()
            {
                id = 112,
                nextId = 81,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Nun gut, wenn Sie so davon überzeugt sind. Ich habe sowieso noch etwas Zeit, dann schaue ich jetzt doch nochmal über Ihren Businessplan drüber.", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL),
            },

            new VisualNovelEvent()
            {
                id = 113,
                nextId = 114,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ich denke, Ihr Unternehmen ist ein bisschen zu... innovativ und vielleicht übernehmen Sie sich damit auch ein bisschen.", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL),
            },

            new VisualNovelEvent()
            {
                id = 114,
                nextId = 115,
                onChoice = 116,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Was heißt denn 'zu innovativ'? Wieso denken Sie das?"
            },

            new VisualNovelEvent()
            {
                id = 115,
                nextId = 400,
                onChoice = 200,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich schätze Ihre Bedenken, aber ich bin zuversichtlich, dass mein Unternehmen erfolgreich wird. Ich habe mir die Zeit genommen, um ein fundiertes Verständnis für den Markt zu entwickeln und habe klare Strategien, um potenzielle Risiken zu bewältigen."
            },

            new VisualNovelEvent()
            {
                id = 116,
                nextId = 117,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Nun, ich denke einfach, dass Frauen oft dazu neigen, ihre Risikotoleranz zu überschätzen. Vielleicht wäre es klug, Ihre Ambitionen etwas zu drosseln und zunächst klein anzufangen.", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL),
            },

            new VisualNovelEvent()
            {
                id = 117,
                nextId = 118,
                onChoice = 119,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Mein Geschlecht ist kein Hindernis für den Erfolg meines Unternehmens. Meine Ambitionen sind auch nicht übermäßig hoch, sondern basieren auf einer gründlichen Analyse und Planung."
            },

            new VisualNovelEvent()
            {
                id = 118,
                nextId = 400,
                onChoice = 181,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich sehe keinen Grund, warum mein Geschlecht eine Rolle bei der Bewertung meines Unternehmens spielen sollte. Betrachten Sie doch bitte meine Leistungen und meinen Geschäftsplan objektiv."
            },

            new VisualNovelEvent()
            {
                id = 119,
                nextId = 120,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Das wollte ich so ja auch gar nicht sagen. Schließlich bin ich kein Sexist! Ich mache mir da nur Sorgen um Sie und wollte Ihnen nur einen gut gemeinten Ratschlag geben.", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.PROUD),
            },

            new VisualNovelEvent()
            {
                id = 120,
                nextId = 121,
                onChoice = 122,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ach so. Ich würde aber gerne verstehen, warum Sie denken, dass mein Unternehmen zu innovativ ist. Haben Sie Bedenken bezüglich der Marktakzeptanz oder der Finanzierung?"
            },

            new VisualNovelEvent()
            {
                id = 121,
                nextId = 400,
                onChoice = 504,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Danke, aber für mich wäre es hilfreicher, wenn Sie mit mir nochmal durch meinen Businessplan gehen würden. Wäre das möglich?"
            },

            new VisualNovelEvent()
            {
                id = 122,
                nextId = 123,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Wir haben interne Richtlinien und Risikobewertungen, die uns bei der Entscheidung helfen, welche Geschäftsbereiche wir unterstützen können. Da berücksichtigen wir Faktoren wie Branchentrends, Marktsättigung und potenzielle regulatorische Risiken.", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL),
            },

            new VisualNovelEvent()
            {
                id = 123,
                nextId = 124,
                onChoice = 125,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Danke für die Erklärung. Inwiefern widersprechen denn Ihre Einschätzungen meinen eigenen Analysen und Daten? Könnten wir genauer darüber sprechen, welche spezifischen Bedenken Ihre internen Richtlinien hervorbringen und wie ich diese adressieren kann?"
            },

            new VisualNovelEvent()
            {
                id = 124,
                nextId = 400,
                onChoice = 125,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Verstehe. Das sind wichtige Faktoren, die bei jeder Geschäftsentscheidung berücksichtigt werden sollten. Könnten Sie mir näher erläutern, wie mein Geschäftsplan in Bezug auf diese Faktoren bewertet wurde?"
            },

            new VisualNovelEvent()
            {
                id = 125,
                nextId = 502,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Natürlich. Wir haben festgestellt, dass Ihr Unternehmen möglicherweise einem höheren Risiko ausgesetzt ist, da es in einem stark umkämpften Markt tätig ist und regulatorischen Schwankungen unterliegen könnte.", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL),
            },

            new VisualNovelEvent()
            {
                id = 502,
                nextId = 126,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.PLAY_SOUND_EVENT),
                waitForUserConfirmation = false,
                audioClipToPlay = SoundEnumHelper.ToInt(SoundsEnum.PAPER_SOUND)
            },

            new VisualNovelEvent()
            {
                id = 126,
                nextId = 127,
                onChoice = 128,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Das Risiko ist sicherlich da, aber ich habe konkrete Maßnahmen ergriffen, um damit umzugehen. (Papier raschelt) Hier auf Seite 16 gehe ich konkret darauf ein." 
            },

            new VisualNovelEvent()
            {
                id = 127,
                nextId = 400,
                onChoice = 175,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Es ist verständlich, dass Sie vorsichtig sind, aber es scheint so, als ob Sie meinen Businessplan nicht gründlich gelesen haben. Auf Seite 16 gehe ich darauf ein."
            },

            new VisualNovelEvent()
            {
                id = 128,
                nextId = 129,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Nun gut, dann kann ich das auch nochmal lesen, aber wie gesagt, Ihre Geschäftsidee scheint in eine Risikobranche zu fallen, was wir nun mal ungern unterstützen. (Papier raschelt) Jetzt nach erneutem Lesen, nehme ich meine Antwort zurück. Haben Sie das ganz alleine geschrieben? Das liest sich wirklich nicht schlecht.", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL),
            },

            new VisualNovelEvent()
            {
                id = 129,
                nextId = 130,
                onChoice = 511,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ja, ich habe das alleine geschrieben. Falls noch Unklarheiten bestehen, können wir die gerne besprechen."
            },

            new VisualNovelEvent()
            {
                id = 130,
                nextId = 400,
                onChoice = 148,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Wieso sollte ich das nicht alleine geschrieben haben?"
            },

            new VisualNovelEvent()
            {
                id = 511,
                nextId = 131,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.PLAY_SOUND_EVENT),
                waitForUserConfirmation = false,
                audioClipToPlay = SoundEnumHelper.ToInt(SoundsEnum.MAN_LAUGHING)
            },

            new VisualNovelEvent()
            {
                id = 131,
                nextId = 132,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ich bin beeindruckt. Das ist für eine Frau eine ziemliche Leistung! Wer hätte das gedacht! Haha. ", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.LAUGHING),
            },

            new VisualNovelEvent()
            {
                id = 132,
                nextId = 133,
                onChoice = 512,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Danke! Aber wissen Sie, ich habe gehört, Frauen können eben auch beeindruckende Geschäfte ins Leben rufen. Wir brechen eben gern mit Klischees."
            },

            new VisualNovelEvent()
            {
                id = 133,
                nextId = 400,
                onChoice = 135,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Und ich hätte gedacht, dass ein Mann einen Businessplan genauer durchlesen würde, bevor er den als 'zu riskant' einstuft, aber so haben wir uns beide geirrt."
            },

            new VisualNovelEvent()
            {
                id = 512,
                nextId = 134,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.PLAY_SOUND_EVENT),
                waitForUserConfirmation = false,
                audioClipToPlay = SoundEnumHelper.ToInt(SoundsEnum.MAN_LAUGHING)
            },

            new VisualNovelEvent()
            {
                id = 134,
                nextId = 20,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Haha, ja natürlich! Ich finde es auch wirklich toll, dass Sie dieses Projekt angehen. ", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.LAUGHING),
            },

            new VisualNovelEvent()
            {
                id = 135,
                nextId = 136,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Sie müssen nicht gleich so schnippisch reagieren. War ja schließlich auch ein Kompliment.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.REFUSING),
            },

            new VisualNovelEvent()
            {
                id = 136,
                nextId = 137,
                onChoice = 513,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Inwiefern war das denn ein Kompliment?"
            },

            new VisualNovelEvent()
            {
                id = 137,
                nextId = 138,
                onChoice = 513,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Auf solche Komplimente verzichte ich lieber."
            },

            new VisualNovelEvent()
            {
                id = 138,
                nextId = 400,
                onChoice = 147,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Können wir wieder über die Eröffnung des Firmenkontos sprechen?"
            },

            new VisualNovelEvent()
            {
                id = 513,
                nextId = 139,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.PLAY_SOUND_EVENT),
                waitForUserConfirmation = false,
                audioClipToPlay = SoundEnumHelper.ToInt(SoundsEnum.MAN_LAUGHING)
            },

            new VisualNovelEvent()
            {
                id = 139,
                nextId = 140,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ach, jetzt stellen Sie sich nicht so an. Sie wissen doch, wie das gemeint war!", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.LAUGHING),
            },

            new VisualNovelEvent()
            {
                id = 140,
                nextId = 141,
                onChoice = 142,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Nein, weiß ich nicht. Wie war das denn gemeint?"
            },

            new VisualNovelEvent()
            {
                id = 141,
                nextId = 400,
                onChoice = 300,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Aha. Ich denke, ich möchte lieber das Firmenkonto doch bei einer anderen Bank eröffnen."
            },

            new VisualNovelEvent()
            {
                id = 142,
                nextId = 143,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Na ja, ich wollte damit nur sagen, dass es beeindruckend ist, dass Sie als Frau in der Geschäftswelt Fuß fassen. Sie sollten das als Anerkennung Ihrer Leistungen sehen.",    
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
            },

            new VisualNovelEvent()
            {
                id = 143,
                nextId = 144,
                onChoice = getRandomNumber(53, 54),
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Verstehe. Allerdings klingen Ihre Worte eher abwertend als lobend. Ich hätte erwartet, dass Sie meine Leistungen unabhängig von meinem Geschlecht bewerten. Das ist nämlich nicht nur unprofessionell, sondern auch sexistisch und diskriminierend."
            },

            new VisualNovelEvent()
            {
                id = 144,
                nextId = 145,
                onChoice = 146,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Aha. Nun ja, was sind denn die nächsten Schritte, um ein Firmenkonto zu eröffnen?"
            },

            new VisualNovelEvent()
            {
                id = 145,
                nextId = 400,
                onChoice = 58,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Aha. Ich denke, ich möchte lieber das Firmenkonto doch bei einer anderen Bank eröffnen."
            },

            new VisualNovelEvent()
            {
                id = 146,
                nextId = 24,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ah ja, die nächsten Schritte... Da fällt mir ein, vielleicht könnten wir das als Bank auch als Werbung für uns nutzen und damit auch Werbung für Sie und Ihr Unternehmen machen!",    
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
            },

            new VisualNovelEvent()
            {
                id = 147,
                nextId = 24,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Natürlich. Oh, da fällt mir ein, vielleicht könnten wir als Bank das auch als Werbung für uns nutzen und damit auch Werbung für Sie und Ihr Unternehmen machen!",    
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
            },

            new VisualNovelEvent()
            {
                id = 148,
                nextId = 149,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Nun, es ist nur so, dass es nicht alltäglich ist, dass Gründerinnen wie Sie solche Businesspläne eigenständig verfassen. Es gibt oft professionelle Berater, die dabei helfen. Aber, äh, ich meine natürlich, das ist eine beeindruckende Leistung, wenn Sie das alleine gemacht haben.",    
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
            },

            new VisualNovelEvent()
            {
                id = 149,
                nextId = 150,
                onChoice = 152,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ja, ich habe meinen Businessplan eigenständig verfasst. Es wäre hilfreich, wenn Sie meine Leistung nicht aufgrund meines Geschlechts anzweifeln würden, sondern sich auf die Qualität meiner Arbeit konzentrieren würden."
            },

            new VisualNovelEvent()
            {
                id = 150,
                nextId = 151,
                onChoice = 171,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Was meinen Sie denn mit 'Gründerinnen wie ich'?"
            },

            new VisualNovelEvent()
            {
                id = 151,
                nextId = 400,
                onChoice = 135,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich weiß, es mag schockierend sein, dass Frauen wie ich dazu in der Lage sind, Businesspläne zu verfassen. Aber keine Sorge, ich werde mein Bestes tun, um Ihre Erwartungen zu übertreffen, selbst wenn sie offensichtlich recht niedrig sind."
            },

            new VisualNovelEvent()
            {
                id = 152,
                nextId = 153,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Verstehen Sie mich bitte nicht falsch. Ich wollte nicht Ihre Leistung aufgrund Ihres Geschlechts anzuzweifeln. Aber es ist nun mal eine Tatsache, dass Frauen in der Geschäftswelt oft vor größeren Herausforderungen stehen als Männer.",    
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.DEFEATED),
            },

            new VisualNovelEvent()
            {
                id = 153,
                nextId = 154,
                onChoice = 146,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ja, Frauen sind oft mit zusätzlichen Hürden konfrontiert, aber das heißt nicht, dass wir weniger fähig sind, erfolgreiche Unternehmen zu gründen und zu führen. Nun ja, ich würde gerne weiter besprechen, wie die nächsten Schritte aussehen, um ein Firmenkonto zu eröffnen."
            },

            new VisualNovelEvent()
            {
                id = 154,
                nextId = 400,
                onChoice = 155,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Vielleicht stehen Frauen vor größeren Herausforderungen als Männer, weil ihre Businesspläne nicht so gründlich gelesen werden?"
            },

            new VisualNovelEvent()
            {
                id = 155,
                nextId = 156,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Was wollen Sie damit sagen?",    
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL),
            },

            new VisualNovelEvent()
            {
                id = 156,
                nextId = 157,
                onChoice = 158,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Nun, Sie haben selbst gesagt, dass sich der Businessplan nicht schlecht liest und vermutlich wären Sie schneller zu diesem Entschluss gekommen, wenn sie den ernsthaft gelesen hätten."
            },

            new VisualNovelEvent()
            {
                id = 157,
                nextId = 400,
                onChoice = getRandomNumber(162, 164),
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Dass wir alle gewisse Biases haben und wir uns dessen teilweise auch gar nicht bewusst sind. Deshalb lesen wir zum Beispiel Businesspläne von Frauen vielleicht weniger gründlich durch, weil wir Frauen unbewusst eine Unternehmensgründung gar nicht zutrauen. Dann kommt es dazu, dass wir uns eben sexistisch verhalten, ohne es zu wollen."
            },

            new VisualNovelEvent()
            {
                id = 158,
                nextId = 159,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Wollen Sie mir jetzt Sexismus unterstellen? Meine Anmerkung bezog sich rein auf meine Erfahrungen in der Branche. Ich mache hier meine Arbeit und es tut mir leid, wenn meine Worte falsch verstanden wurden, aber ich versichere Ihnen, dass Geschlecht keine Rolle in meiner Bewertung spielt.",    
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.PROUD),
            },

            new VisualNovelEvent()
            {
                id = 159,
                nextId = 160,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ihre Aussagen waren sexistisch und der erste Schritt wäre schon getan, wenn wir alle einsehen können, dass wir uns auch unabsichtlich sexistisch äußern können. Aber nun gut, dann sehe ich mich nach einer anderen Bank um.",    
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.PROUD),
            },

            new VisualNovelEvent()
            {
                id = 160,
                nextId = 161,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ja, ich denke, wir kommen hier nicht auf einen Nenner. Ich wünsche Ihnen dennoch viel Glück mit allem.",    
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.NONE),
            },

            new VisualNovelEvent()
            {
                id = 161,
                nextId = 401,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Danke für Ihre Zeit. Einen schönen Tag noch.",    
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.NONE),
            },

            new VisualNovelEvent()
            {
                id = 162,
                nextId = 55,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ich verstehe, dass Sie das so empfinden, aber ich habe es wirklich nicht sexistisch gemeint. Manchmal kommen Dinge anders rüber als beabsichtigt. ",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.DEFEATED),
            },

            new VisualNovelEvent()
            {
                id = 163,
                nextId = 68,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Das war überhaupt nicht sexistisch! Sie müssen Dinge nicht überinterpretieren. Wenn Sie so sensibel sind, sollten Sie vielleicht überlegen, ob Sie im Geschäftsumfeld richtig aufgehoben sind. Ich habe keine Lust auf solche Anschuldigungen!",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.DECISION_NO),
            },

            new VisualNovelEvent()
            {
                id = 164,
                nextId = 165,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Hm, stimmt. So habe ich das noch gar nicht gesehen. Danke für den Denkanstoß.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
            },

            new VisualNovelEvent()
            {
                id = 165,
                nextId = 166,
                onChoice = 167,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Gerne, ich würde nun lieber wieder über die Eröffnung des Firmenkontos sprechen."
            },

            new VisualNovelEvent()
            {
                id = 166,
                nextId = 400,
                onChoice = 167,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Wir lernen jeden Tag etwas Neues. Und nun ist es Zeit für mich zu lernen, wie ich bei Ihnen ein Firmenkonto eröffne."
            },

            new VisualNovelEvent()
            {
                id = 167,
                nextId = 168,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Es würde mich wirklich freuen, wenn Sie bei uns ein Konto eröffnen. Vielleicht könnten wir das als Bank auch als Werbung für uns nutzen und damit auch Werbung für Sie und Ihr Unternehmen machen!",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
            },

            new VisualNovelEvent()
            {
                id = 168,
                nextId = 169,
                onChoice = 27,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Die Idee klingt interessant. Könnten Sie mir mehr darüber erzählen, wie wir diese Werbemöglichkeiten konkret gestalten könnten?"
            },

            new VisualNovelEvent()
            {
                id = 169,
                nextId = 170,
                onChoice = 27,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Wie haben Sie sich das denn vorgestellt? Wie soll die Werbung aussehen?"
            },

            new VisualNovelEvent()
            {
                id = 170,
                nextId = 400,
                onChoice = 34,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich eröffne gerne ein Konto, aber an der Werbung bin ich nicht interessiert. Trotzdem danke für das Angebot."
            },

            new VisualNovelEvent()
            {
                id = 171,
                nextId = 172,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ach, entschuldigen Sie, ich habe mich unklar ausgedrückt. Ich meinte damit, dass es nicht üblich ist, dass Frauen in der Gründungsszene so aktiv sind wie Männer. Es ist einfach beeindruckend zu sehen, wie Sie sich in einem von Männern dominierten Bereich behaupten.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.DEFEATED),
            },

            new VisualNovelEvent()
            {
                id = 172,
                nextId = 173,
                onChoice = 174,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Verstehe. Trotzdem schade, dass Sie Geschlechterklischees bedienen. Immerhin sollen Fähigkeiten und Leistungen unabhängig von meinem Geschlecht bewertet werden. Aber gut, lassen Sie uns den Businessplan noch einmal durchgehen."
            },

            new VisualNovelEvent()
            {
                id = 173,
                nextId = 400,
                onChoice = getRandomNumber(53, 54),
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich denke, Sie haben sich sehr klar ausgedrückt. Schade, dass Sie noch dermaßen veraltete sexistische Vorstellungen über Frauen in der Wirtschaft haben."
            },

            new VisualNovelEvent()
            {
                id = 174,
                nextId = 144,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Sie wissen doch, wie es läuft in der Geschäftswelt. Männer haben einfach ein anderes Gespür für Risiken und Erfolg. Aber keine Sorge, ich bin sicher, Sie machen das bestimmt auch wunderbar.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
            },

            new VisualNovelEvent()
            {
                id = 175,
                nextId = 176,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ihre Beharrlichkeit ist bewundernswert, aber Sie müssen auch Ihre Grenzen kennen. Frauen neigen dazu, sich zu überschätzen und sollten ihre Ambitionen entsprechend anpassen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL),
            },

            new VisualNovelEvent()
            {
                id = 176,
                nextId = 177,
                onChoice = 505,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Diese Annahme ist keine wissenschaftlich fundierte Tatsache. Bleiben wir doch bei Fakten. Wie dem, dass mein Businessplan aussagefkräftig und gut genug recherchiert ist, um ein Firmenkonto zu eröffnen."
            },

            new VisualNovelEvent()
            {
                id = 177,
                nextId = 178,
                onChoice = 507,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Interessant, dass Sie das sagen. Ich dachte, es wäre eher so, dass Männer dazu neigen, sich zu überschätzen und Frauen diejenigen sind, die das Unternehmen am Laufen halten."
            },

            new VisualNovelEvent()
            {
                id = 178,
                nextId = 400,
                onChoice = 158,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Sie sollten Ihre voreingenommenen Ansichten über Frauen in der Geschäftswelt überdenken."
            },

            new VisualNovelEvent()
            {
                id = 505,
                nextId = 179,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.PLAY_SOUND_EVENT),
                waitForUserConfirmation = false,
                audioClipToPlay = SoundEnumHelper.ToInt(SoundsEnum.MAN_LAUGHING)
            },

            new VisualNovelEvent()
            {
                id = 179,
                nextId = 129,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ihre Beharrlichkeit ist bewundernswert, aber Sie müssen auch Ihre Grenzen kennen. Frauen neigen dazu, sich zu überschätzen und sollten ihre Ambitionen entsprechend anpassen.", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.LAUGHING),
            },

            new VisualNovelEvent()
            {
                id = 504,
                nextId = 180,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.PLAY_SOUND_EVENT),
                waitForUserConfirmation = false,
                audioClipToPlay = SoundEnumHelper.ToInt(SoundsEnum.PAPER_SOUND)
            },

            new VisualNovelEvent()
            {
                id = 180,
                nextId = 129,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Nun gut, wenn Sie so sehr darauf bestehen. Aber wie gesagt, Ihre Geschäftsidee scheint in eine Risikobranche zu fallen, was wir nun mal ungern unterstützen. (Papier raschelt) Jetzt nach erneutem Lesen, nehme ich meine Antwort zurück. Haben Sie das ganz alleine geschrieben? Das liest sich wirklich nicht schlecht.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.QUESTIONING),
            },

            new VisualNovelEvent()
            {
                id = 181,
                nextId = 301,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Und ich habe Ihren Businessplan bereits gelesen und schätze den als zu riskant ein. Sie müssen auch lernen mit einer negativen Antwort umzugehen, auch wenn es Ihnen nicht gefällt.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.REFUSING),
            },

            new VisualNovelEvent()
            {
                id = 301,
                nextId = 182,
                onChoice = 122,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Trotzdem möchte ich gerne verstehen, wie Sie zu dieser Einschätzung gekommen sind."
            },

            new VisualNovelEvent()
            {
                id = 182,
                nextId = 400,
                onChoice = 183,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich habe allerdings die Vermutung, dass Sie sich den Plan nicht gründlich genug durchgelesen haben und mir Sachen nicht zutrauen, weil ich kein Mann bin."
            },

            new VisualNovelEvent()
            {
                id = 183,
                nextId = 184,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Es tut mir leid, wenn Sie das so empfunden haben. Unsere Bank legt großen Wert darauf, Frauenunternehmen zu unterstützen und zu fördern. Unsere Entscheidungen basieren ausschließlich auf einer sorgfältigen Analyse der Geschäftspläne und Risiken.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.DEFEATED),
            },

            new VisualNovelEvent()
            {
                id = 184,
                nextId = 185,
                onChoice = 504,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Vielleicht habe ich das falsch interpretiert. Können wir dann bitte noch einmal gemeinsam meinen Businessplan durchgehen, damit ich sicherstellen kann, dass alle Aspekte berücksichtigt wurden?"
            },

            new VisualNovelEvent()
            {
                id = 185,
                nextId = 400,
                onChoice = 175,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Es würde mir helfen, wenn Sie nochmal mit mir den Businessplan durchgehen würden."
            },

            new VisualNovelEvent()
            {
                id = 507,
                nextId = 186,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.PLAY_SOUND_EVENT),
                waitForUserConfirmation = false,
                audioClipToPlay = SoundEnumHelper.ToInt(SoundsEnum.MAN_LAUGHING)
            },

            new VisualNovelEvent()
            {
                id = 186,
                nextId = 187,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ach was! Wenn das so wäre, würde es doch nicht so viele Männer geben, die Unternehmen führen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.LAUGHING),
            },

            new VisualNovelEvent()
            {
                id = 187,
                nextId = 188,
                onChoice = 189,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Sie denken, dass Männer besser darin sind Unternehmen zu führen, weil sie Männer sind?"
            },

            new VisualNovelEvent()
            {
                id = 188,
                nextId = 400,
                onChoice = 197,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Eigentlich sind es strukturelle und soziale Faktoren, die dazu führen, dass Männer in Führungspositionen bevorzugt werden. Historisch gesehen wurden Frauen oft der Zugang zu Bildung und Ressourcen verwehrt und das schränkt natürlich ihre beruflichen Möglichkeiten ein."
            },

            new VisualNovelEvent()
            {
                id = 189,
                nextId = 190,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Nun ja, es ist eine Tatsache, dass Männer häufiger Unternehmen führen. Dann ist die logische Schlussfolgerung doch, dass Männer dementsprechend auch bessere Führungskräfte sind.", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.ASTONISHED),
            },

            new VisualNovelEvent()
            {
                id = 190,
                nextId = 191,
                onChoice = 192,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Hm, Sie sollten mal nachschauen, was der Thomas-Kreislauf ist. Das ist ein sehr interessantes Phänomen, der erklärt nach welchem Muster Unternehmen Vorstandsmitglieder rekrutieren. Aber nun gut, dann suche ich eine andere Bank, um mein Firmenkonto zu eröffnen."
            },

            new VisualNovelEvent()
            {
                id = 191,
                nextId = 400,
                onChoice = 194,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Sagen Ihnen die Phänomene 'Thomas-Kreislauf' und 'Matilda-Effekt' etwas?"
            },

            new VisualNovelEvent()
            {
                id = 192,
                nextId = 193,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ach so? Ich schau dann mal später nach, was dieser Thomas-Kreislauf ist. Ich wünsche Ihnen für die Zukunft noch viel Glück.", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
            },

            new VisualNovelEvent()
            {
                id = 193,
                nextId = 401,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Schönen Tag noch."
            },

            new VisualNovelEvent()
            {
                id = 194,
                nextId = 195,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Na ja, ich habe das irgendwo schon mal gehört...", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.NONE),
            },

            new VisualNovelEvent()
            {
                id = 195,
                nextId = 196,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Schauen Sie sich mal die beiden Phänomene genauer an. Das erklärt nämlich so einiges. Aber nun gut, ich schaue dann auch nach einer anderen Bank, wo ich mein Firmenkonto eröffnen kann. Danke für Ihre Zeit."
            },

            new VisualNovelEvent()
            {
                id = 196,
                nextId = 161,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Ähm ja, also dann wünsche ich Ihnen natürlich viel Glück und noch alles Gute für die Zukunft.", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.NONE),
            },

            new VisualNovelEvent()
            {
                id = 197,
                nextId = 198,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Aber mittlerweile sind wir doch bei der Gleichberechtigung schon angekommen. Ich meine, Sie können schließlich auch ein Unternehmen gründen.", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL),
            },

            new VisualNovelEvent()
            {
                id = 198,
                nextId = 199,
                onChoice = 196,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ja, Frauen können Unternehmen gründen. Allerdings stoßen Frauen immer noch auf Hindernisse und Diskriminierung. Es ist wichtig, diese Probleme anzuerkennen und aktiv daran zu arbeiten, sie zu lösen, anstatt anzunehmen, dass alles bereits perfekt ist. Dazu können Sie sich aber gerne noch eigenständig informieren. Ich suche mir dann mal eine andere Bank."
            },

            new VisualNovelEvent()
            {
                id = 199,
                nextId = 400,
                onChoice = 194,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Es wäre schön, wenn wir tatsächlich schon am Ziel angekommen wären, aber das ist leider nicht der Fall Sagen Ihnen die Phänomene 'Thomas-Kreislauf' und 'Matilda-Effekt' etwas?"
            },

            new VisualNovelEvent()
            {
                id = 200,
                nextId = 302,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Nun, das mag sein, aber ich denke, es ist wichtig, realistisch zu bleiben und mögliche Hindernisse zu berücksichtigen.", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL),
            },

            new VisualNovelEvent()
            {
                id = 302,
                nextId = 201,
                onChoice = 181,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Wie gesagt, ich habe alles berücksichtigt und eine gründliche Analyse angefertigt. Das können Sie gerne nochmal nachlesen."
            },

            new VisualNovelEvent()
            {
                id = 201,
                nextId = 400,
                onChoice = 202,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Was für Hindernisse meinen Sie denn?"
            },

            new VisualNovelEvent()
            {
                id = 202,
                nextId = 203,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Nun, zum Beispiel könnte die schnelle Veränderung von Technologien oder sich ändernde Marktbedingungen Ihr Geschäft beeinflussen. Auch die Konkurrenz in Ihrer Branche könnte zunehmen und den Markt schwieriger machen. Ich bin mir schlichtweg nicht sicher, ob Sie mit ihrem Geschäftsmodell genug Umsatz machen.", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL),
            },

            new VisualNovelEvent()
            {
                id = 203,
                nextId = 204,
                onChoice = 181,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Das habe ich, wie gesagt, auf Seite 14 ausgeführt. Nach meiner Analyse werde ich genügend Umsatz machen."
            },

            new VisualNovelEvent()
            {
                id = 204,
                nextId = 400,
                onChoice = 205,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Das gilt allerdings für jedes Geschäft. Außerdem habe ich genau aus diesen Gründen einen flexiblen Geschäftsplan entwickelt, der es mir ermöglicht, schnell auf Veränderungen zu reagieren."
            },

            new VisualNovelEvent()
            {
                id = 205,
                nextId = 501,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Müller",
                text = "Das ist schön zu hören. Es scheint, als hätten Sie sich gründlich auf die Herausforderungen vorbereitet. Vielleicht können wir noch einige Details Ihres Plans besprechen, um sicherzustellen, dass Sie auf alle Eventualitäten vorbereitet sind. (Papier raschelt) Jetzt nach erneutem Lesen, muss ich sagen, das liest sich wirklich nciht schlecht.. Haben Sie das ganz alleine geschrieben?", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.ASTONISHED),
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
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.PLAY_SOUND_EVENT),
                waitForUserConfirmation = false,
                audioClipToPlay = SoundEnumHelper.ToInt(SoundsEnum.LEAVE_SCENE)
            },

            new VisualNovelEvent()
            {
                id = 402,
                nextId = 403,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.CHARAKTER_EXIT_EVENT),
                waitForUserConfirmation = false,
                name = "Herr Müller",
                animationType = AnimationTypeHelper.ToInt(AnimationType.FLY_IN_FROM_ABOVE),
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 403,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.END_NOVEL_EVENT),
                waitForUserConfirmation = false
            }
        };        
    }

    public int getRandomNumber(int lowerBound, int upperBound) 
    {
        //Debug.Log(System.DateTime.Now.Millisecond);
        Random.InitState(System.DateTime.Now.Millisecond);
        return Random.Range(lowerBound, upperBound);
    }
}