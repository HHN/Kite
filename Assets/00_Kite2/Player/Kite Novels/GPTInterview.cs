using System.Collections.Generic;
using UnityEngine;

public class GPTInterview : VisualNovel
{
    public GPTInterview()
    {
        id = -13;
        title = "GPTInterview";
        description = "Beispiel Gespräch für den Start der App.";
        image = 0;
        nameOfMainCharacter = "Lea";
        feedback = "";
        context = "";
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
                name = "Herr Mayer",
                animationType = AnimationTypeHelper.ToInt(AnimationType.FLY_IN_FROM_ABOVE),
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED),
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
                nextId = 31,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Mayer",
                text = "Herzlich willkommen in der Welt von KITE! Ich bin eine KI und unterstütze dich dabei resilienter im Umgang mit diskriminierenden Erfahrungen im Gründungsprozess zu werden.", // Geschlecht des Assistenten wählbar machen?
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 31,
                nextId = 4,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.FREE_TEXT_INPUT_EVENT),
                waitForUserConfirmation = false,
                questionForFreeTextInput = "Wie darf ich dich in Zukunft nennen?",  // Fragen ob dutzen oder sietzen?
                variablesName = "PlayerName"
            },

            new VisualNovelEvent()
            {
                id = 4,
                nextId = 5,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Mayer",
                text = "Schön dich kennenzulernen, [PlayerName]. Es freut mich dich begrüßen zu dürfen!", // Geschlecht des Assistenten wählbar machen?
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 5,
                nextId = 6,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Mayer",
                text = "Bist du eine Gründerin oder überlegst selbst zu gründen?", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 6,
                nextId = 7,
                onChoice = 10,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ja."
            },

            new VisualNovelEvent()
            {
                id = 7,
                nextId = 8,
                onChoice = 9,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Nein."
            },

            new VisualNovelEvent()
            {
                id = 8,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT),
                waitForUserConfirmation = true
            },

            new VisualNovelEvent()
            {
                id = 9,
                nextId = 21,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Mayer",
                text = "Wie schön, dass du dich für die Erfahrungen von Gründerinnen interessierst, [PlayerName]!", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 10,
                nextId = 100,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Mayer",
                text = "Super! Du hast jetzt die Wahl: Wir unterhalten uns noch etwas, dann finden wir konkret raus, wie ich dich am besten unterstützen kann (das ist natürlich optimal für alles weitere) oder du springst sofort in die App und spielst direkt die Novels, die wir für dich erstellt haben.", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 100,
                nextId = 101,
                onChoice = 12,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Lass uns weiter unterhalten."
            },

            new VisualNovelEvent()
            {
                id = 101,
                nextId = 8,
                onChoice = 300,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte direkt mit den Novels beginnen."
            },

            new VisualNovelEvent()
            {
                id = 12,
                nextId = 13,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Mayer",
                text = "Erzähl mal: Hast du dich auch schon für einen Firmennamen entschieden, [PlayerName]?",  //TODO Decide what to do if this gets skipped
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 13,
                nextId = 14,
                onChoice = 15,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ja."
            },

            new VisualNovelEvent()
            {
                id = 14,
                nextId = 8,
                onChoice = 19,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Nein."
            },

            new VisualNovelEvent()
            {
                id = 15,
                nextId = 16,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.FREE_TEXT_INPUT_EVENT),
                waitForUserConfirmation = false,
                questionForFreeTextInput = "Welchen Namen hast du dir denn überlegt?",                      //TODO Decide what to do if this gets skipped
                variablesName = "CompanyName"
            },

            new VisualNovelEvent()
            {
                id = 16,
                nextId = 17,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Mayer",
                text = "Dieser Name gefällt mir! Worum geht's denn bei deinem Unternehmen? Mache am besten mal einen Elevator Pitch.", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 17,
                nextId = 18,
                onChoice = 26,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Das möchte ich später erzählen."
            },

            new VisualNovelEvent()
            {
                id = 18,
                nextId = 8,
                onChoice = 20,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Klar, hör gut zu:"
            },

            new VisualNovelEvent()
            {
                id = 19,
                nextId = 23,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Mayer",
                text = "Kein Problem! Erzähl mir aber doch, worum es bei deinem Unternehmen gibt. Mache am besten mal einen Elevator Pitch.", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 20,
                nextId = 21,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.FREE_TEXT_INPUT_EVENT),
                waitForUserConfirmation = false,
                questionForFreeTextInput = "Worum geht's denn bei deinem Unternehmen? Mache am besten mal einen Elevator Pitch.",  
                variablesName = "ElevatorPitch"
            },

            new VisualNovelEvent()
            {
                id = 21,
                nextId = 28,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Mayer",
                text = "Das hört sich richtig gut an, [PlayerName]. [CompanyName]  wird bestimmt ein voller Erfolg!", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 22,
                nextId = 23,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.FREE_TEXT_INPUT_EVENT),
                waitForUserConfirmation = false,
                questionForFreeTextInput = "Worum geht's denn bei deinem Unternehmen? Mache am besten mal einen Elevator Pitch.",  
                variablesName = "ElevatorPitch"
            },

            new VisualNovelEvent()
            {
                id = 23,
                nextId = 24,
                onChoice = 26,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Das möchte ich später erzählen."
            },

            new VisualNovelEvent()
            {
                id = 24,
                nextId = 8,
                onChoice = 25,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Klar, hör gut zu:"
            },

            new VisualNovelEvent()
            {
                id = 25,
                nextId = 27,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.FREE_TEXT_INPUT_EVENT),
                waitForUserConfirmation = false,
                questionForFreeTextInput = "Worum geht's denn bei deinem Unternehmen? Mache am besten mal einen Elevator Pitch.",  
                variablesName = "ElevatorPitch"
            },

            new VisualNovelEvent()
            {
                id = 26,
                nextId = 28,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Mayer",
                text = "Kein Problem.", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 27,
                nextId = 28,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Mayer",
                text = "Das hört sich richtig gut an, [PlayerName]. Dein Unternehmen wird bestimmt ein voller Erfolg!", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 28,
                nextId = 300,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Mayer",
                text = "Im Profilbereich kannst du die Angaben jederzeit ändern.", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 300,
                nextId = 301,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Mayer",
                text = "Wenn du mir mitteilst, welche Szenarien dich besonders interessieren, suche ich dir passende Novels raus. Möchtest du dir direkt selbst alles anschauen oder ein paar Vorschläge haben?", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 301,
                nextId = 302,
                onChoice = 304,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Schlag mir gerne vor, welche Novels am besten zu meinen Interessen passen."
            },

            new VisualNovelEvent()
            {
                id = 302,
                nextId = 8,
                onChoice = 303,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich werde schon alles alleine rausfinden. Lass mich direkt beginnen!"
            },

            new VisualNovelEvent()
            {
                id = 303,
                nextId = 400,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Mayer",
                text = "Alles klar, dann wünsche ich dir viel Spaß beim Erkunden von KITE!", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 304,
                nextId = 305,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.FREE_TEXT_INPUT_EVENT),
                waitForUserConfirmation = false,
                questionForFreeTextInput = "Erzähle mir was dich aktuell am meisten interessiert und ich suche dir passende Inhalte dazu raus.",  
                variablesName = "Preverences"
            },

            new VisualNovelEvent()
            {
                id = 305,
                nextId = 306,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.GPT_PROMPT_EVENT),
                waitForUserConfirmation = true,
                gptPrompt = GetTaskPrompt() + " Dies ist die Liste von Visual Novels: " + GetNovelSummary() + " Dies ist die Antwort der Person: [Preverences]",
                variablesNameForGptPromp = "GPTAnswerForPreverences",
                gptCompletionHandlerId = 0
            },

            new VisualNovelEvent()
            {
                id = 306,
                nextId = 307,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Mayer",
                text = "Super, ich suche dir die passenden Novels raus. Eine Sache noch:", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 307,
                nextId = 308,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Mayer",
                text = "Wir wollen Dich mit KITE bei deiner Gründung begleiten! Forschungen haben gezeigt, dass Gründerinnen häufig anderen Problemen gegenüberstehen als Gründer. In KITE kannst du einige Stationen im Gründungsprozess durchspielen und unterschiedliche Dialoge und Reaktionen für Dich in Ruhe austesten. Im Anschluss analysieren wir das Gespräch, damit du verschiedene Muster und Biases kennenlernst, denen du begegnet bist.", 
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 308,
                nextId = 309,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Mayer",
                text = "Dies sind die Inhalte, welche dich hoffentlich am meisten interessieren: [GPTAnswerForPreverences].",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 309,
                nextId = 400,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Mayer",
                text = "Ich wünsche dir nun viel Spaß und Lernerfolg mit KITE und falls du fragen hast kannst du mich jederzeit aufsuchen!",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },





            

            new VisualNovelEvent()
            {
                id = 400,
                nextId = 401,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.PLAY_SOUND_EVENT),
                waitForUserConfirmation = false,
                audioClipToPlay = SoundEnumHelper.ToInt(SoundsEnum.LEAVE_SCENE)
            },

            new VisualNovelEvent()
            {
                id = 401,
                nextId = 402,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.CHARAKTER_EXIT_EVENT),
                waitForUserConfirmation = false,
                name = "Herr Mayer",
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

    private string GetTaskPrompt()
    {
        return  "Du erhältst eine Liste von Visual Novels, die jeweils mit spezifischen Tags versehen sind. " +
"Deine Aufgabe ist es, anhand einer gegebenen Antwort einer Person zu bewerten, wie relevant jede dieser Visual Novels für die Person ist. " +
"Deine Antwort sollte die Namen von drei relevanten Visual Novels enthalten. Nutze das Format: '[Name der ersten Novel]; [Name der zweiten Novel]; [Name dritten Novel]'";
    }

    private string GetNovelSummary()
    {
        return  "1. BankAccountOpeningNovel: Finanzmanagement: Fokus auf Bankkonten und finanzielle Planung. | Entscheidungsfindung: Verschiedene Handlungsoptionen und deren Konsequenzen. | Realistisches Business-Szenario: Praktische Darstellung von Geschäftssituationen. | Kommunikationsfähigkeit: Interaktion und Dialogführung in einem professionellen Umfeld. | Strategische Problem-Lösung: Herausforderungen im Kontext der Geschäftsgründung. | Selbstbehauptung im Business: Verteidigung und Präsentation der Geschäftsidee. " + 
                "2. BankAppointmentNovel: Finanzmanagement: Fokus auf Kreditbeantragung und Rückzahlungsplanung. | Entscheidungsfindung: Auswahl von Argumenten zur Kreditwürdigkeit. | Realistisches Business-Szenario: Darstellung eines Kreditgesprächs in der Bank. | Kommunikationsfähigkeit: Effektive Kommunikation und Überzeugungsarbeit im Finanzkontext. | Strategische Problem-Lösung: Entwicklung und Präsentation eines überzeugenden Businessplans. | Selbstbehauptung im Business: Selbstsicheres Auftreten und Verteidigung der eigenen Geschäftspläne. " +
                "3. BankTalkNovel: Finanzmanagement: Fokus auf Kreditgespräche und Finanzierungsmöglichkeiten. | Kommunikationsfähigkeit: Interaktion mit Bankpersonal und effektive Darstellung der eigenen Position. | Realistisches Business-Szenario: Authentische Darstellung eines Banktermins. | Entscheidungsfindung: Auswahl von Antworten und Umgang mit unerwarteten Fragen. | Selbstbehauptung im Business: Verteidigung der eigenen Geschäftsidee und Umgang mit potenziellem Sexismus. | Emotionale Intelligenz: Bewältigung von Nervosität und Unsicherheit in professionellen Gesprächen. " +
                "4. CallWithNotaryNovel: Unternehmensgründung: Fokus auf rechtliche Aspekte und Formalitäten der Firmengründung. | Kommunikationsfähigkeit: Effektive Kommunikation mit einer Notarin und Klärung von Gründungsfragen. | Entscheidungsfindung: Auswahl von Antworten und Umgang mit unterschiedlichen Reaktionen der Notarin. | Selbstbehauptung im Business: Verteidigung der eigenen Position als Geschäftsführerin und Umgang mit potenziellem Geschlechterbias. | Emotionale Intelligenz: Bewältigung von Nervosität und Unsicherheit in professionellen Gesprächen. " +
                "5. CallWithParentsNovel: Unternehmensgründung: Fokus auf die Entscheidung zur Selbstständigkeit und die damit verbundenen Herausforderungen. | Familienbeziehungen: Interaktion mit den Eltern und Umgang mit deren Sorgen und Erwartungen. | Emotionale Intelligenz: Bewältigung von Unsicherheit und Druck durch familiäre Erwartungen. | Selbstbehauptung: Verteidigung der eigenen Karriereentscheidungen gegenüber skeptischen Familienmitgliedern. | Entscheidungsfindung: Umgang mit unterschiedlichen Meinungen und Ratschlägen von Familienmitgliedern. " +
                "6. ConversationWithAcquaintancesNovel: Unternehmensgründung: Fokus auf Leas Entscheidung, ein Unternehmen in der Tech-Branche zu gründen und die Herausforderungen, die damit einhergehen. | Geschlechterrollen: Auseinandersetzung mit stereotypen Annahmen und Vorurteilen bezüglich Frauen in der Technologiebranche. | Selbstbehauptung: Leas Fähigkeit, sich gegen geschlechtsspezifische Vorurteile zu behaupten und ihre Kompetenz zu unterstreichen. | Netzwerken: Interaktion mit einer alten Bekannten und die Bedeutung von sozialen Kontakten für berufliche Entwicklungen. | Emotionale Intelligenz: Umgang mit sensiblen Themen wie Geschlechterstereotypen und die Fähigkeit, angemessen darauf zu reagieren. " +
                "7. FeeNegotiationNovel: Verhandlungsgeschick: Fokus auf die Fähigkeit, in Honorarverhandlungen effektiv zu kommunizieren und eigene Interessen zu vertreten. | Preisgestaltung: Auseinandersetzung mit der Herausforderung, faire Preise für Dienstleistungen festzulegen und zu rechtfertigen. | Kundenbeziehungen: Umgang mit Kundenanfragen und -erwartungen in Bezug auf Kosten und Leistungsumfang. | Selbstbewusstsein: Stärkung des Selbstvertrauens in der Diskussion und Verteidigung der eigenen Preispolitik. | Kompromissfindung: Fähigkeit, flexible Lösungen zu finden, die sowohl den Kundenanforderungen als auch den eigenen Geschäftsinteressen gerecht werden. " +
                "8. InitialInterviewForGrantApplicationNovel: Unternehmensgründung: Fokus auf die Vorbereitung und Durchführung eines Erstgesprächs zur Beantragung von Fördermitteln. | Geschäftsideen-Präsentation: Darstellung und Verteidigung der eigenen Geschäftsidee vor potenziellen Förderern. | Umgang mit Diskriminierung: Auseinandersetzung mit geschlechtsspezifischen Vorurteilen und unangemessenen Fragen im professionellen Kontext. | Fördermittelakquise: Erkundung verschiedener Fördermöglichkeiten und Anforderungen für Start-ups. | Emotionale Intelligenz: Bewältigung von Stress und Druck in kritischen Geschäftsgesprächen. " +
                "9. PressTalkNovel: Unternehmer*innen-Präsentation: Darstellung der eigenen Geschäftsidee vor einem Publikum und Networking mit anderen Gästen. | Presseinterview: Interaktion mit einem Pressevertreter und Beantwortung von Fragen zur eigenen Geschäftsidee. | Geschlechterrollen im Business: Auseinandersetzung mit der Betonung des Geschlechts in der Berichterstattung und dem Wunsch, für die Geschäftsidee anerkannt zu werden. | Öffentliche Wahrnehmung: Umgang mit der Darstellung in den Medien und Einfluss auf die öffentliche Wahrnehmung des Unternehmens. | Kommunikationsstrategien: Strategien für effektive Kommunikation und Positionierung in einem Presseinterview. " +
                "10. RentingAnOfficeNovel: Büroanmietung: Besichtigung und Anmietung von Büroräumen für ein neues Unternehmen. | Geschäftsvorbereitung: Diskussion über die Vorbereitung und Planung für die Unternehmensgründung. | Finanzielle Planung: Erörterung der finanziellen Tragfähigkeit und langfristigen Mietzahlungsfähigkeit. | Geschlechterstereotype im Geschäftsleben: Auseinandersetzung mit geschlechtsbezogenen Kommentaren und Stereotypen während des Mietprozesses. | Professionelle Kommunikation: Strategien für effektive und professionelle Kommunikation in geschäftlichen Verhandlungen. " +
                "11. StartUpGrantNovel: Gründerzuschuss: Beantragung eines Gründerzuschusses bei der Agentur für Arbeit. | Unterlagen und Qualifikationen: Diskussion über die notwendigen Unterlagen und Qualifikationen für die Beantragung. | Finanzwissen und Buchhaltung: Auseinandersetzung mit der Bedeutung von Finanzwissen und Buchhaltungskompetenzen für Unternehmensgründer. | Selbstbewusstsein und Kompetenznachweis: Strategien, um Selbstbewusstsein zu zeigen und Kompetenzen zu beweisen. | Umgang mit Zweifeln: Reaktion auf Zweifel oder kritische Fragen bezüglich der eigenen Fähigkeiten und Vorbereitung.";
    }

    private string GetRecommendedNovelsFromGPTOutput(string gptOutput)
    {
        //TODO: To improve the code quality, get the title of each novel instead of hardcoded title
        var returnString = "";
        if (gptOutput.Contains("BankAccountOpeningNovel"))
        {
            returnString += "Bank Kontoeröffnung, ";
        }
        if (gptOutput.Contains("BankAppointmentNovel"))
        {
            returnString += "Banktermin zur Kreditvergabe, ";
        }
        if (gptOutput.Contains("BankTalkNovel"))
        {
            returnString += "Bankgespräch, ";
        }
        if (gptOutput.Contains("CallWithNotaryNovel"))
        {
            returnString += "Telefonat mit dem Notar, ";
        }
        if (gptOutput.Contains("CallWithParentsNovel"))
        {
            returnString += "Erstgespräch Förderantrag, ";
        }
        if (gptOutput.Contains("ConversationWithAcquaintancesNovel"))
        {
            returnString += "Gespräch mit Bekannten, ";
        }
        if (gptOutput.Contains("FeeNegotiationNovel"))
        {
            returnString += "Honorarverhandlung mit Kundin, ";
        }
        if (gptOutput.Contains("InitialInterviewForGrantApplicationNovel"))
        {
            returnString += "Erstgespräch Förderantrag, ";
        }
        if (gptOutput.Contains("PressTalkNovel"))
        {
            returnString += "Pressegespräch, ";
        }
        if (gptOutput.Contains("RentingAnOfficeNovel"))
        {
            returnString += "Anmietung eines Büros, ";
        }
        if (gptOutput.Contains("StartUpGrantNovel"))
        {
            returnString += "Gründerzuschuss, ";
        }
        return ReplaceLastOccurrence(returnString, ",", "und");
        
    }

    private string ReplaceLastOccurrence(string source, string find, string replace)
    {
        int place = source.LastIndexOf(find);

        if (place == -1)
        return source;

        string result = source.Remove(place, find.Length).Insert(place, replace);
        return result;
    }

    private int WriteUserInputToFile(int id, string inputType, string input)
    {
        PlayerDataManager.Instance().SavePlayerData(inputType, input);
        Debug.Log(inputType + ": " + input);
        Debug.Log(PlayerDataManager.Instance().ReadPlayerData(inputType));
        return id;
    }

    private void TestUserInput(){
        Debug.Log(PlayerDataManager.Instance().ReadPlayerData("test"));
    }
    

}