using System.Collections.Generic;

public class PlayManager
{
    private static PlayManager instance;
    private VisualNovel novelToPlay;

    private PlayManager() { }

    public static PlayManager Instance()
    {
        if (instance == null)
        {
            instance = new PlayManager();
        }
        return instance;
    }

    public void SetVisualNovelToPlay(VisualNovel novelToPlay)
    {
        this.novelToPlay = novelToPlay;
    }

    public VisualNovel GetVisualNovelToPlay()
    {
        if (novelToPlay == null)
        {
            return null;
        }
        List<VisualNovelEvent> visualNovelEvents = new List<VisualNovelEvent>();

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 1,
            nextId = 2,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SET_BACKGROUND_EVENT),
            waitForUserConfirmation = false,
            animationType = AnimationTypeHelper.ToInt(AnimationType.FLY_IN_FROM_ABOVE),
            xPosition = 0,
            yPosition = 0,
            imageId = 0
        });

        visualNovelEvents.Add(new VisualNovelEvent()
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
            imageId = 0
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 3,
            nextId = 4,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Intro",
            text = "Du hast eine Einladung zu einem Bankgespräch erhalten, um mehr Informationen über einen Kredit zu erhalten und diesen darauf zu beantragen.",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 4,
            nextId = 5,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Lea",
            text = "Hallo. Schönen guten Tag.",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 5,
            nextId = 6,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Herr Mayer",
            text = "Hallo. Guten Tag, setzen Sie sich. Kann ich Ihnen was zu trinken anbieten?",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 6,
            nextId = 7,
            onChoice = 9,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
            waitForUserConfirmation = false,
            name = "Lea",
            text = "Ja, gerne. Ein Wasser."
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 7,
            nextId = 8,
            onChoice = 13,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
            waitForUserConfirmation = false,
            name = "Lea",
            text = "Nein, Danke."
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 8,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT),
            waitForUserConfirmation = true,
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 9,
            nextId = 10,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Herr Mayer",
            text = "Wasser. Mit Gas, ohne Gas?",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 10,
            nextId = 11,
            onChoice = 13,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
            waitForUserConfirmation = false,
            name = "Lea",
            text = "Ja, gerne mit."
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 11,
            nextId = 12,
            onChoice = 13,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
            waitForUserConfirmation = false,
            name = "Lea",
            text = "Bitte ohne Gas."
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 12,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT),
            waitForUserConfirmation = true,
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 13,
            nextId = 14,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Herr Mayer",
            text = "Verdammt kalt heute, nicht?",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 14,
            nextId = 15,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Lea",
            text = "Ja.",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 15,
            nextId = 16,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Herr Mayer",
            text = "Haben Sie gut hergefunden?",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 16,
            nextId = 17,
            onChoice = 19,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
            waitForUserConfirmation = false,
            name = "Lea",
            text = "Ja, das hat sehr gut geklappt. ich war ja schon mal hier in der Bank. " +
            "Aber da noch nicht als Geschäftskunde."
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 17,
            nextId = 18,
            onChoice = 19,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
            waitForUserConfirmation = false,
            name = "Lea",
            text = "Ich habe leider ein wenig gebraucht, um das Büro zu finden."
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 18,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT),
            waitForUserConfirmation = true,
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 19,
            nextId = 20,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Herr Mayer",
            text = "Ja, was führt Sie zu mir?",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 20,
            nextId = 21,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Lea",
            text = "Ja, also wie vorab schon in der E-Mail besprochen geht es darum, " +
            "dass ich gerne einen kredit aufnehmen möchte. Und ich würde entsprechend " +
            "auch ein Geschäftkonto hier einrichten.",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 21,
            nextId = 22,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Lea",
            text = "Da sagten Sie, dass das ja zusammen gekoppelt ist. " +
            "Was mir auch recht ist, da ich noch kein Geschäftskonto habe.",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 22,
            nextId = 23,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Lea",
            text = "Und Sie hatten ja meine Unterlagen und Businessplan schon gesichtet. " +
            "Entsprechend bin ich jetzt auch hier um Fragen zu beantowrten und auch gespannt " +
            "auf das, was wir hier gemeinsam machen können.",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 23,
            nextId = 24,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Herr Mayer",
            text = "Verzeihung. könnten Sie mir nochmal kurz sagen worum es geht? " +
            "Ich habe so viele Kunden, da komme ich ab und zu durcheinander.",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 24,
            nextId = 25,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Lea",
            text = "Ja, natürlich. Ich habe ein Game Development Studio " +
            "namens Knights Gambit Studios gegründen.",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 25,
            nextId = 26,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Herr Mayer",
            text = "Ah ok.",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 26,
            nextId = 27,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Herr Mayer",
            text = "Eine Frage, wenn es um Investitionen geht. " +
            "Haben Sie einen Mann? Oder sind Sie verheiratet?",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 27,
            nextId = 28,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Lea",
            text = "Ob ich einen Mann habe? Also...",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 28,
            nextId = 29,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Herr Mayer",
            text = "Oder müssen Sie das jetzt alles alleine stemmen?",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 29,
            nextId = 30,
            onChoice = 34,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
            waitForUserConfirmation = false,
            name = "Lea",
            text = "Also ich hab mit dieser Frage jetzt nicht gerechnet um ehrlich zu sein."
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 30,
            nextId = 31,
            onChoice = 34,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
            waitForUserConfirmation = false,
            name = "Lea",
            text = "Das spielt doch keine Rolle."
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 31,
            nextId = 32,
            onChoice = 34,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
            waitForUserConfirmation = false,
            name = "Lea",
            text = "Nein."
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 32,
            nextId = 33,
            onChoice = 34,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
            waitForUserConfirmation = false,
            name = "Lea",
            text = "Ja."
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 33,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT),
            waitForUserConfirmation = true,
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 34,
            nextId = 35,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Herr Mayer",
            text = "Ich möchte wissen, ob da jetzt ein größeres Startkapital auch vorhanden ist?",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 35,
            nextId = 36,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Lea",
            text = "Nein, also ich habe jetzt kein größeres Eigenkapital und deswegen brauche ich den Kredit.",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 36,
            nextId = 37,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Herr Mayer",
            text = "Aha. Und haben Sie Kinder?",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 37,
            nextId = 38,
            onChoice = 41,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
            waitForUserConfirmation = false,
            name = "Lea",
            text = "Ja."
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 38,
            nextId = 39,
            onChoice = 41,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
            waitForUserConfirmation = false,
            name = "Lea",
            text = "Nein."
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 39,
            nextId = 40,
            onChoice = 41,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
            waitForUserConfirmation = false,
            name = "Lea",
            text = "Warum wollen Sie das wissen?"
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 40,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT),
            waitForUserConfirmation = true,
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 41,
            nextId = 42,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Herr Mayer",
            text = "Es ist einfach nur wichtig zu wissen, weil ich schon Gründerinnen hier hatte, und da war dann der Kinderwunsch im Endeffekt größer.",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 42,
            nextId = 43,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Herr Mayer",
            text = "Die haben dann den Kredit bekommen und plötzlich waren Kinder da und dann wurde das schwierig. Aber wie gesagt, das ist jetzt eigentlich gar nicht so wichtig.",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 43,
            nextId = 44,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Lea",
            text = "Ich hätte eine Frage. Und zwar wie geht es ab hier weiter? Also ich habe nen Gründungsberater, der mir natürlich den Ablauf erklärt hat, aber jetzt intern über Sie wie läuft das?",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 44,
            nextId = 45,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Lea",
            text = "Ich bin jetzt davon ausgegangen, dass wir in diesem Gespräch eigentlich abschließend klären, ob Sie das als Bank machen möchten.",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 45,
            nextId = 46,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Lea",
            text = "Wie ich heraushören kann, ist das ja noch offen, weil Sie ja gar nicht wissen, was in meinem Businessplan überhaupt beschrieben wird. Ich dachte, es werden jetzt konkrete Fragen gestellt.",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 46,
            nextId = 47,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Herr Mayer",
            text = "Also, prinzipiell ist es so, dass ich erst nochmal Rückfrage mit jemanden aus der Filialleitung halten muss, denn ich darf hier in so einem Fall gar keine Entscheidung treffen.",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 47,
            nextId = 48,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Herr Mayer",
            text = "Wie gesagt, ich bin eigentlich nur hier um so ein bisschen rauszufinden, hat das Perspektive?",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 48,
            nextId = 49,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Herr Mayer",
            text = "Kann das funktionieren? Kann ich mir das auch vorstellen? Die Frage ist ja auch, was gebe ich an Informationen weiter. Und was ich auf jeden Fall sehe, ist eine sehr selbstbewusste junge Frau.",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 49,
            nextId = 50,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Herr Mayer",
            text = "Das ist ein sehr wichtiger Punkt, finde ich. Da denke ich haben sie sicher auch ganz gute Chancen.",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 50,
            nextId = 51,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Herr Mayer",
            text = "Ich kann jetzt aber in dem Zustand hier noch keine Entscheidungen treffen. Ich würde mich auf jeden Fall bei Ihnen melden. Am besten machen wir noch einen weiteren Termin aus.",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 51,
            nextId = 52,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Herr Mayer",
            text = "Ich denke mal im Laufe der nächsten 4 bis 5 Monate. Und ja, ich drück Ihnen die Daumen.",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 52,
            nextId = 53,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Lea",
            text = "Dankeschön.",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 53,
            nextId = 54,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
            waitForUserConfirmation = true,
            name = "Herr Mayer",
            text = "Alles Gute. Auf bald.",
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 54,
            nextId = 55,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.CHARAKTER_EXIT_EVENT),
            waitForUserConfirmation = false,
            name = "Herr Müller",
            animationType = AnimationTypeHelper.ToInt(AnimationType.FLY_IN_FROM_ABOVE),
            expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
        });

        visualNovelEvents.Add(new VisualNovelEvent()
        {
            id = 55,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.END_NOVEL_EVENT),
            waitForUserConfirmation = false
        });

        novelToPlay.novelEvents = visualNovelEvents;

        string feedback = "In diesem Szenario hat der Bankmitarbeiter die Unternehmerin diskriminiert. " +
            "Er hat unangemessene Fragen zu ihrem Familienstand gestellt, was irrelevant für den Kreditantrag ist. " +
            "Es ist nicht seine Aufgabe, die Familienplanung der Unternehmerin zu beurteilen oder zu berücksichtigen. " +
            "Es ist auch nicht angebracht, nach einem Ehepartner oder einem größeren Startkapital zu fragen, " +
            "da dies nicht relevant für den Kreditantrag ist." + " \n\n" + "Die Unternehmerin hat sich jedoch gut verhalten, " +
            "indem sie ruhig und sachlich geblieben ist und die Fragen des Bankmitarbeiters beantwortet hat. " +
            "Sie hat auch den Ablauf des Gesprächs und ihre Erwartungen klar ausgedrückt und um konkrete Fragen gebeten. \n\n" + 
            "Die Unternehmerin sollte das Verhalten des Bankmitarbeiters ansprechen und gegebenenfalls eine " +
            "Beschwerde einreichen. Es ist wichtig, dass der Bankmitarbeiter sein Verhalten überdenkt und sich " +
            "in Zukunft professioneller verhält, um Diskriminierung zu vermeiden.";

        novelToPlay.feedback = feedback;
        return novelToPlay;
    }
}