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
                waitForUserConfirmation = true,
                gptPrompt = GetTaskPrompt() + " Dies ist die Liste Visual Novels: " + GetNovelSummary() + " Dies ist die Antwort der Person: [Preverences]",
                variablesNameForGptPromp = "GPTAnswerForPreverences",
                gptCompletionHandlerId = 0
            },

            new VisualNovelEvent()
            {
                id = 8,
                nextId = 9,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Mayer",
                text = "Dies sind die Novels, welche dich hoffentlich am meisten interessieren: [GPTAnswerForPreverences]",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 9,
                nextId = 61,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Mayer",
                text = "Dies sind die Novels, welche dich hoffentlich am meisten interessieren: [GPTAnswerForPreverences]",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
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
                id = 500,
                nextId = 600,
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

    private string GetTaskPrompt()
    {
        return  "Du erhältst eine Liste von Visual Novels, die jeweils mit spezifischen Tags versehen sind. " +
"Deine Aufgabe ist es, anhand einer gegebenen Antwort einer Person zu bewerten, wie relevant jede dieser Visual Novels für die Person ist. " +
"Deine Antwort sollte die Namen von drei relevanten Visual Novels enthalten. Nutze das Format: '[Name der ersten Novel]; [Name der zweiten Novel]; [Name dritten Novel]'";
    }

    private string GetNovelSummary()
    {
        return "1. BankAccountOpeningNovel: Finanzmanagement: Fokus auf Bankkonten und finanzielle Planung. | Entscheidungsfindung: Verschiedene Handlungsoptionen und deren Konsequenzen. | Realistisches Business-Szenario: Praktische Darstellung von Geschäftssituationen. | Kommunikationsfähigkeit: Interaktion und Dialogführung in einem professionellen Umfeld. | Strategische Problem-Lösung: Herausforderungen im Kontext der Geschäftsgründung. | Selbstbehauptung im Business: Verteidigung und Präsentation der Geschäftsidee. " + 
"2. BankAppointmentNovel: Finanzmanagement: Fokus auf Kreditbeantragung und Rückzahlungsplanung. | Entscheidungsfindung: Auswahl von Argumenten zur Kreditwürdigkeit. | Realistisches Business-Szenario: Darstellung eines Kreditgesprächs in der Bank. | Kommunikationsfähigkeit: Effektive Kommunikation und Überzeugungsarbeit im Finanzkontext. | Strategische Problem-Lösung: Entwicklung und Präsentation eines überzeugenden Businessplans. | Selbstbehauptung im Business: Selbstsicheres Auftreten und Verteidigung der eigenen Geschäftspläne. " +
"3. BankTalkNovel: Finanzmanagement: Fokus auf Kreditgespräche und Finanzierungsmöglichkeiten. | Kommunikationsfähigkeit: Interaktion mit Bankpersonal und effektive Darstellung der eigenen Position. | Realistisches Business-Szenario: Authentische Darstellung eines Banktermins. | Entscheidungsfindung: Auswahl von Antworten und Umgang mit unerwarteten Fragen. | Selbstbehauptung im Business: Verteidigung der eigenen Geschäftsidee und Umgang mit potenziellem Sexismus. | Emotionale Intelligenz: Bewältigung von Nervosität und Unsicherheit in professionellen Gesprächen. " +
"4. CallWithNotaryNovel: Finanzierung, Geschäftspartner, Führungsfähigkeit, Unternehmensführung, Geschäftsführung, Voreingenommenheit, Missverständnisse, Selbstrepräsentation " +
"5. CallWithParentsNovel: Eltern, Finanzierung, Ersparnisse, Kredit, Geld, Sorge, Unterstützung, Erfahrung, Risiken, Herausforderung, Familienplanung, Kinderwunsch, Muttersein, Familie und Beruf, Schwangerschaft, Verhandlungen, Selbstbewusstsein, Selbstsicherheit, Führungsfähigkeit, Unternehmensführung, Geschäftsführung, Mentor*innen, Geschäftspartner, Bruder, Geschwister, Aussehen, Weiblicher Charme, Tightrope Bias, Benevolenter Sexismus, Maternal Bias, Work-Life-Balance" +
"6. ConversationWithAcquaintancesNovel: Unternehmensgründung: Fokus auf Leas Entscheidung, ein Unternehmen in der Tech-Branche zu gründen und die Herausforderungen, die damit einhergehen. | Geschlechterrollen: Auseinandersetzung mit stereotypen Annahmen und Vorurteilen bezüglich Frauen in der Technologiebranche. | Selbstbehauptung: Leas Fähigkeit, sich gegen geschlechtsspezifische Vorurteile zu behaupten und ihre Kompetenz zu unterstreichen. | Netzwerken: Interaktion mit einer alten Bekannten und die Bedeutung von sozialen Kontakten für berufliche Entwicklungen. | Emotionale Intelligenz: Umgang mit sensiblen Themen wie Geschlechterstereotypen und die Fähigkeit, angemessen darauf zu reagieren. " +
"7. FeeNegotiationNovel: Verhandlungsgeschick: Fokus auf die Fähigkeit, in Honorarverhandlungen effektiv zu kommunizieren und eigene Interessen zu vertreten. | Preisgestaltung: Auseinandersetzung mit der Herausforderung, faire Preise für Dienstleistungen festzulegen und zu rechtfertigen. | Kundenbeziehungen: Umgang mit Kundenanfragen und -erwartungen in Bezug auf Kosten und Leistungsumfang. | Selbstbewusstsein: Stärkung des Selbstvertrauens in der Diskussion und Verteidigung der eigenen Preispolitik. | Kompromissfindung: Fähigkeit, flexible Lösungen zu finden, die sowohl den Kundenanforderungen als auch den eigenen Geschäftsinteressen gerecht werden. " +
"8. InitialInterviewForGrantApplicationNovel: Familienplanung, Kinderwunsch, Muttersein, Familie und Beruf, Schwangerschaft, Biases gegenüber Frauen mit Kindern, Maternal Bias, Führungsfähigkeit, Unternehmensführung, Geschäftsführung, Partnerbeteiligung, Work-Life-Balance, Risikomanagement " +
"9. PressTalkNovel: Pressebericht, Sensationsjournalismus, Interview, Artikel, Geschlechterdarstellung in den Medien, Wirkung von Unternehmerinnengeschichten, Unternehmerinnen als Vorbilder, Role Model, Selbstpräsentation, Auseinandersetzung mit Klischees, Tokenism " +
"10. RentingAnOfficeNovel: Miete, Vermieter, Bewerbung, Finanzplan, Marktforschung, Selbstauskunft, Netzwerken, Geschlechterklischee, Geschlechterstereotype, Familienplanung, Kinderwunsch, Muttersein, Familie und Beruf, Schwangerschaft, Partnerbeteiligung, Benevolenter Sexismus, Frauenpower " +
"11. StartUpGrantNovel: Gründerzuschuss: Beantragung eines Gründerzuschusses beim Arbeitsamt. | Unterlagen und Qualifikationen: Diskussion über die notwendigen Unterlagen und Qualifikationen für die Beantragung. | Finanzwissen und Buchhaltung: Auseinandersetzung mit der Bedeutung von Finanzwissen und Buchhaltungskompetenzen für Unternehmensgründer. | Selbstbewusstsein und Kompetenznachweis: Strategien, um Selbstbewusstsein zu zeigen und Kompetenzen zu beweisen. | Umgang mit Zweifeln: Reaktion auf Zweifel oder kritische Fragen bezüglich der eigenen Fähigkeiten und Vorbereitung.";
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

}