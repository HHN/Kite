using System.Collections.Generic;

public class GPTInterview : VisualNovel
{
    public GPTInterview()
    {
        id = -1;
        title = "GPTInterview";
        description = "Beispiel Gespräch für den Start der App.";
        image = 0;
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
                nextId = 4,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.FREE_TEXT_INPUT_EVENT),
                waitForUserConfirmation = false,
                questionForFreeTextInput = "Schönen guten Tag, wie darf ich dich nennen?",  // Fragen ob dutzen oder sietzen?
                variablesName = "PlayerName"
            },

            new VisualNovelEvent()
            {
                id = 4,
                nextId = 5,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Mayer",
                text = "Hallo [PlayerName], es freut mich dich zu sehen. Ich bin dein persönlicher Assistent R2D2.", // Geschlecht des Assistenten wählbar machen?
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 5,
                nextId = 6,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Mayer",
                text = "Um dir den Einstieg in die KITE-App zu erleichtern führe ich dich gerne einmal herum. " + 
                "Möchtest du mir vorher kurz mitteilen welche Themen dich besonders interessieren? Dann suche ich dir die entsprechenden Inhalte schoneinmal raus.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 6,
                nextId = 7,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.FREE_TEXT_INPUT_EVENT),
                waitForUserConfirmation = false,
                questionForFreeTextInput = "Erzähle mir was dich aktuell am meisten interessiert und ich suche dir passende Inhalte dazu raus.",  // Fragen ob dutzen oder sietzen?
                variablesName = "Preverences"
            },

            new VisualNovelEvent()
            {
                id = 7,
                nextId = 8,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.GPT_PROMPT_EVENT),
                gptPrompt = "" +
                
                "Nehm folgenden Satz und setze die Variable so ein, dass es sinn macht:" +
                "Satz: Hallo. Ich bin gespannt über ihr {Unternehmen} zu hören. Guten Tag, setzen Sie sich. Kann ich Ihnen was zu trinken anbieten?" +
                "Variable: {Unternehmen} --> [Geschäftsvorhaben]",
                variablesNameForGptPromp = "text01",
                gptCompletionHandlerId = 0
            },











            new VisualNovelEvent()
            {
                id = 4000,
                nextId = 4,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.GPT_PROMPT_EVENT),
                gptPrompt = "Nehm folgenden Satz und setze die Variable so ein, dass es sinn macht:" +
                "Satz: Hallo. Ich bin gespannt über ihr {Unternehmen} zu hören. Guten Tag, setzen Sie sich. Kann ich Ihnen was zu trinken anbieten?" +
                "Variable: {Unternehmen} --> [Geschäftsvorhaben]",
                variablesNameForGptPromp = "text01",
                gptCompletionHandlerId = 0
            },

            new VisualNovelEvent()
            {
                id = 4,
                nextId = 5,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Hallo. Schönen guten Tag.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
                id = 5,
                nextId = 6,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Mayer",
                text = "[text01]",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.ASTONISHED)
            },

            

            new VisualNovelEvent()
            {
                id = 61,
                nextId = 62,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.CHARAKTER_EXIT_EVENT),
                waitForUserConfirmation = false,
                name = "Herr Mayer",
                animationType = AnimationTypeHelper.ToInt(AnimationType.FLY_IN_FROM_ABOVE),
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 62,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.END_NOVEL_EVENT),
                waitForUserConfirmation = false
            }
        };
    }

    private string getNovelSummary()
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
                "11. StartUpGrantNovel: Gründerzuschuss: Beantragung eines Gründerzuschusses beim Arbeitsamt. | Unterlagen und Qualifikationen: Diskussion über die notwendigen Unterlagen und Qualifikationen für die Beantragung. | Finanzwissen und Buchhaltung: Auseinandersetzung mit der Bedeutung von Finanzwissen und Buchhaltungskompetenzen für Unternehmensgründer. | Selbstbewusstsein und Kompetenznachweis: Strategien, um Selbstbewusstsein zu zeigen und Kompetenzen zu beweisen. | Umgang mit Zweifeln: Reaktion auf Zweifel oder kritische Fragen bezüglich der eigenen Fähigkeiten und Vorbereitung.";
    }
}