using System.Collections.Generic;
using UnityEngine;

public class GenerateNovelPipeline : GPT_Pipeline
{
    public static string QUESTION_01 = "Question01";
    public static string QUESTION_02 = "Question02";
    public static string QUESTION_03 = "Question03";

    public static string TEMPLATE_ANSWER_01 = "TemplateAnswer01";
    public static string TEMPLATE_ANSWER_02 = "TemplateAnswer02";
    public static string TEMPLATE_ANSWER_03 = "TemplateAnswer03";

    public static string ANSWER_01 = "Answer01";
    public static string ANSWER_02 = "Answer02";
    public static string ANSWER_03 = "Answer03";

    public static string MODIFIED_ANSWER_01 = "ModifiedAnswer01";
    public static string MODIFIED_ANSWER_02 = "ModifiedAnswer02";
    public static string MODIFIED_ANSWER_03 = "ModifiedAnswer03";

    public static string TEMPLATE_LINEAR_NOVEL = "templateLinearNovel";
    public static string TEMPLATE_DESCRIPTION = "templateDescription";

    public static string GENERATED_LINEAR_NOVEL = "generatedLinearNovel";
    public static string GENERATED_TITLE = "generatedTitle";
    public static string GENERATED_DESCRIPTION = "generatedDescription";

    public static string SHORT_SUMMARY = "shortSummary";

    public static string ALTERNATIVE_ANSWER_01_01 = "aa0101";
    public static string ALTERNATIVE_ANSWER_01_02 = "aa0102";
    public static string ALTERNATIVE_ANSWER_01_03 = "aa0103";
    public static string ALTERNATIVE_ANSWER_02_01 = "aa0201";
    public static string ALTERNATIVE_ANSWER_02_02 = "aa0202";
    public static string ALTERNATIVE_ANSWER_02_03 = "aa0203";
    public static string ALTERNATIVE_ANSWER_03_01 = "aa0301";
    public static string ALTERNATIVE_ANSWER_03_02 = "aa0302";
    public static string ALTERNATIVE_ANSWER_03_03 = "aa0303";
    public static string ALTERNATIVE_ANSWER_04_01 = "aa0401";
    public static string ALTERNATIVE_ANSWER_04_02 = "aa0402";
    public static string ALTERNATIVE_ANSWER_04_03 = "aa0403";
    public static string ALTERNATIVE_ANSWER_05_01 = "aa0501";
    public static string ALTERNATIVE_ANSWER_05_02 = "aa0502";
    public static string ALTERNATIVE_ANSWER_05_03 = "aa0503";

    public static string LIST_OF_LOCATIONS = "listOfLocations";
    public static string LIST_OF_CHARACTERS = "listOfCharacters";
    public static string LIST_OF_DISPLAY_IMAGES = "listOfDisplayImages";

    public static string CHOSEN_LOCATION = "chosenLocation";
    public static string CHOSEN_CHARACTER = "chosenCharacter";
    public static string CHOSEN_DISPLAY_IMAGE = "chosenDisplayImage";

    public Dictionary<int, string> idToKey = new Dictionary<int, string>();
    public Dictionary<int, string> idToValue = new Dictionary<int, string>();

    public PipelineJob job01;
    public PipelineJob job02;
    public PipelineJob job03;
    public PipelineJob job04;
    public PipelineJob job05;
    public PipelineJob job06;
    public PipelineJob job07;
    public PipelineJob job08;
    public PipelineJob job09;
    public PipelineJob job10;
    public PipelineJob job11;
    public PipelineJob job12;
    public PipelineJob job13;
    public PipelineJob job14;
    public PipelineJob job15;

    public override void InitializePipelineJobs()
    {
        memory.Add(QUESTION_01, "Was sind die Hauptereignisse oder Schlüsselmomente dieser Geschichte?");
        memory.Add(QUESTION_02, "Wer sind die Hauptfiguren und welche Rollen spielen sie in der Geschichte?");
        memory.Add(QUESTION_03, "Welche wichtigen Entscheidungen und Wendepunkte gibt es in der Geschichte?");

        memory.Add(TEMPLATE_ANSWER_01, "[Bei meinem Banktermin zur Kreditanfrage für mein Game Development Studio wurde ich von Herrn Mayer, dem Bankberater, mit unerwarteten, persönlichen Fragen konfrontiert. Er fragte nach meinem Beziehungsstatus und ob ich Kinder habe oder plane, welche zu bekommen, was mich überraschte, da diese Fragen irrelevant für den Kredit waren. Trotz meines detaillierten Businessplans konnte Herr Mayer keine Entscheidung treffen und schlug einen weiteren Termin in einigen Monaten vor, was den Prozess unnötig verzögerte.]");
        memory.Add(TEMPLATE_ANSWER_02, "[In dieser Geschichte sind Lea, eine Unternehmerin, die einen Kredit für ihr Game Development Studio beantragen möchte, und Herr Mayer, der Bankberater, die Hauptfiguren. Lea ist gut vorbereitet und erwartet professionelle Beratung, während Herr Mayer durch seine ungewöhnlichen, persönlichen Fragen, die nichts mit dem Kredit zu tun haben, auffällt und keine sofortige Entscheidung über den Kreditantrag treffen kann.]");
        memory.Add(TEMPLATE_ANSWER_03, "[In der Geschichte gibt es zwei wichtige Wendepunkte: Erstens, die unerwarteten persönlichen Fragen von Herrn Mayer, die den professionellen Rahmen des Kreditgesprächs verlassen und Lea in eine unangenehme Situation bringen. Zweitens, die Entscheidung von Herrn Mayer, keine sofortige Aussage über den Kreditantrag zu treffen, sondern einen weiteren Termin in einigen Monaten vorzuschlagen, was den Fortschritt von Leas Geschäftsplänen potenziell verzögert.]");
        
        memory.Add(ANSWER_01, "Ich will mich selbstständig machen als Spiele-Entwicklerin und habe meiner Mutter davon erzählt. Sie war nicht sehr begeistert. Mein Entwicklungs-Studio heißt Knights Gambit Development Studio.");
        memory.Add(ANSWER_02, "Ich und meine Mutter. Ich bin eine Frau und möchte mich selbstständig machen. Dies habe ich meiner Mutter erzählt. Sie war nicht sehr begeistert davon und hatm ir empfohlen dass ich mich mit meinem Bruder zusammen tue.");
        memory.Add(ANSWER_03, "Meine Mutter war zunächst skeptisch und hat mir empfohlen, dass ich mich mit meinem Bruder zusammen tun sollte. Ihm traut sie die selbstständigkeit wohl er zu, weil er ein Mann ist. Ich habe ihr gesagt, dass ich das auch alleine packen kann und packen will. Am Ende hat sie doch gesagt, dass sie mich dabei unterstützt.");
        
        memory.Add(LIST_OF_LOCATIONS, "1. Büro");
        memory.Add(LIST_OF_CHARACTERS, "1. Herr Mayer (Ein seriöser Herr im Anzug.");
        memory.Add(LIST_OF_DISPLAY_IMAGES, "1. Ein Bild auf dem man ein Smartphone sieht, auf dem Gerade ein Telefonat mit der Mama läuft. 2. Ein Bild auf dem man eine Hand mit Mikrofon sehen kann. 3. Ein Bild mit einem Siegel und einem Stift. 4. Ein Bild mit Büchern. 5. Ein Bild mit einem Schlüssel. 6. Ein Bild von einer Bank (Finanz-Institut). 7. Ein Bild von der Tür des Arbeitsamtes. 8. Ein Bild von zwei Kaffe-Tassen.");

        memory.Add(TEMPLATE_DESCRIPTION, "Du hast eine Einladung zu einem Bankgespräch erhalten, um mehr Informationen über einen Kredit zu erhalten und diesen darauf zu beantragen.");
        
        memory.Add(TEMPLATE_LINEAR_NOVEL, "[\r\n    {\"ID\": 1, \"key\": \"Ort\", \"value\": \"Büro\"},\r\n    {\"ID\": 2, \"key\": \"Eintritt\", \"value\": \"Mayer\"},\r\n    {\"ID\": 3, \"key\": \"Info\", \"value\": \"Du hast eine Einladung zu einem Bankgespräch erhalten, um mehr Informationen über einen Kredit zu erhalten und diesen darauf zu beantragen.\"},\r\n    {\"ID\": 4, \"key\": \"Lea\", \"value\": \"Hallo. Schönen guten Tag.\"},\r\n    {\"ID\": 5, \"key\": \"Mayer\", \"value\": \"Hallo. Guten Tag, setzen Sie sich. Kann ich Ihnen was zu trinken anbieten?\"},\r\n    {\"ID\": 6, \"key\": \"Lea\", \"value\": \"Ja, gerne. Ein Wasser.\"},\r\n    {\"ID\": 7, \"key\": \"Mayer\", \"value\": \"Wasser. Mit Gas, ohne Gas?\"},\r\n    {\"ID\": 8, \"key\": \"Lea\", \"value\": \"Ja, gerne mit.\"},\r\n    {\"ID\": 9, \"key\": \"Mayer\", \"value\": \"Verdammt kalt heute, nicht?\"},\r\n    {\"ID\": 10, \"key\": \"Lea\", \"value\": \"Ja.\"},\r\n    {\"ID\": 11, \"key\": \"Mayer\", \"value\": \"Haben Sie gut hergefunden?\"},\r\n    {\"ID\": 12, \"key\": \"Lea\", \"value\": \"Ja, das hat sehr gut geklappt. ich war ja schon mal hier in der Bank. Aber da noch nicht als Geschäftskunde.\"},\r\n    {\"ID\": 13, \"key\": \"Mayer\", \"value\": \"Ja, was führt Sie zu mir?\"},\r\n    {\"ID\": 14, \"key\": \"Lea\", \"value\": \"Ja, also wie vorab schon in der E-Mail besprochen geht es darum, dass ich gerne einen Kredit aufnehmen möchte. Und ich würde entsprechend auch ein Geschäftskonto hier einrichten.\"},\r\n    {\"ID\": 15, \"key\": \"Lea\", \"value\": \"Da sagten Sie, dass das ja zusammen gekoppelt ist. Was mir auch recht ist, da ich noch kein Geschäftskonto habe.\"},\r\n    {\"ID\": 16, \"key\": \"Lea\", \"value\": \"Und Sie hatten ja meine Unterlagen und Businessplan schon gesichtet. Entsprechend bin ich jetzt auch hier um Fragen zu beantworten und auch gespannt auf das, was wir hier gemeinsam machen können.\"},\r\n    {\"ID\": 17, \"key\": \"Mayer\", \"value\": \"Verzeihung. könnten Sie mir nochmal kurz sagen worum es geht? Ich habe so viele Kunden, da komme ich ab und zu durcheinander.\"},\r\n    {\"ID\": 18, \"key\": \"Lea\", \"value\": \"Ja, natürlich. Ich habe ein Game Development Studio namens Knights Gambit Studios gegründet.\"},\r\n    {\"ID\": 19, \"key\": \"Mayer\", \"value\": \"Ah ok.\"},\r\n    {\"ID\": 20, \"key\": \"Mayer\", \"value\": \"Eine Frage, wenn es um Investitionen geht. Haben Sie einen Mann? Oder sind Sie verheiratet?\"},\r\n    {\"ID\": 21, \"key\": \"Lea\", \"value\": \"Ob ich einen Mann habe? Also...\"},\r\n    {\"ID\": 22, \"key\": \"Mayer\", \"value\": \"Oder müssen Sie das jetzt alles alleine stemmen?\"},\r\n    {\"ID\": 23, \"key\": \"Lea\", \"value\": \"Also ich habe mit dieser Frage jetzt nicht gerechnet um ehrlich zu sein.\"},\r\n    {\"ID\": 24, \"key\": \"Mayer\", \"value\": \"Ich möchte wissen, ob da jetzt ein größeres Startkapital auch vorhanden ist?\"},\r\n    {\"ID\": 25, \"key\": \"Lea\", \"value\": \"Nein, also ich habe jetzt kein größeres Eigenkapital und deswegen brauche ich den Kredit.\"},\r\n    {\"ID\": 26, \"key\": \"Mayer\", \"value\": \"Aha. Und haben Sie Kinder?\"},\r\n    {\"ID\": 27, \"key\": \"Lea\", \"value\": \"Warum wollen Sie das wissen?\"},\r\n    {\"ID\": 28, \"key\": \"Mayer\", \"value\": \"Es ist einfach nur wichtig zu wissen, weil ich schon Gründerinnen hier hatte, und da war dann der Kinderwunsch im Endeffekt größer.\"},\r\n    {\"ID\": 29, \"key\": \"Mayer\", \"value\": \"Die haben dann den Kredit bekommen und plötzlich waren Kinder da und dann wurde das schwierig. Aber wie gesagt, das ist jetzt eigentlich gar nicht so wichtig.\"},\r\n    {\"ID\": 30, \"key\": \"Lea\", \"value\": \"Ich hätte eine Frage. Und zwar wie geht es ab hier weiter? Also ich habe nen Gründungsberater, der mir natürlich den Ablauf erklärt hat, aber jetzt intern über Sie wie läuft das?\"},\r\n    {\"ID\": 31, \"key\": \"Lea\", \"value\": \"Ich bin jetzt davon ausgegangen, dass wir in diesem Gespräch eigentlich abschließend klären, ob Sie das als Bank machen möchten.\"},\r\n    {\"ID\": 32, \"key\": \"Lea\", \"value\": \"Wie ich heraushören kann, ist das ja noch offen, weil Sie ja gar nicht wissen, was in meinem Businessplan überhaupt beschrieben wird. Ich dachte, es werden jetzt konkrete Fragen gestellt.\"},\r\n    {\"ID\": 33, \"key\": \"Mayer\", \"value\": \"Also, prinzipiell ist es so, dass ich erst nochmal Rückfrage mit jemanden aus der Filialleitung halten muss, denn ich darf hier in so einem Fall gar keine Entscheidung treffen.\"},\r\n    {\"ID\": 34, \"key\": \"Mayer\", \"value\": \"Wie gesagt, ich bin eigentlich nur hier um so ein bisschen rauszufinden, hat das Perspektive?\"},\r\n    {\"ID\": 35, \"key\": \"Mayer\", \"value\": \"Kann das funktionieren? Kann ich mir das auch vorstellen? Die Frage ist ja auch, was gebe ich an Informationen weiter. Und was ich auf jeden Fall sehe, ist eine sehr selbstbewusste junge Frau.\"},\r\n    {\"ID\": 36, \"key\": \"Mayer\", \"value\": \"Das ist ein sehr wichtiger Punkt, finde ich. Da denke ich haben sie sicher auch ganz gute Chancen.\"},\r\n    {\"ID\": 37, \"key\": \"Mayer\", \"value\": \"Ich kann jetzt aber in dem Zustand hier noch keine Entscheidungen treffen. Ich würde mich auf jeden Fall bei Ihnen melden. Am besten machen wir noch einen weiteren Termin aus.\"},\r\n    {\"ID\": 38, \"key\": \"Mayer\", \"value\": \"Ich denke mal im Laufe der nächsten 4 bis 5 Monate. Und ja, ich drück Ihnen die Daumen.\"},\r\n    {\"ID\": 39, \"key\": \"Lea\", \"value\": \"Dankeschön.\"},\r\n    {\"ID\": 40, \"key\": \"Mayer\", \"value\": \"Alles Gute. Auf bald.\"},\r\n    {\"ID\": 41, \"key\": \"Austritt\", \"value\": \"Mayer\"}\r\n]");
    }

    public void GenerateNovel()
    {
        jobs.Enqueue(job01);
        jobs.Enqueue(job02);
        jobs.Enqueue(job03);
        jobs.Enqueue(job04);
        jobs.Enqueue(job05);
        jobs.Enqueue(job06);
        jobs.Enqueue(job07);
        jobs.Enqueue(job08);
        jobs.Enqueue(job09);
        jobs.Enqueue(job10);
        jobs.Enqueue(job11);
        jobs.Enqueue(job12);
        jobs.Enqueue(job13);
        jobs.Enqueue(job14);
        jobs.Enqueue(job15);

        Debug.Log("Generierung der Novel wird begonnen. Jobs in Warteschlange: " + jobs.Count);
        StartPipeline();
    }

    public override void OnFailure()
    {
        Debug.Log("Generierung der Novel Fehlgeschlagen.");
    }

    public override void OnProgress()
    {
        Debug.Log("Fortschritt wurde Erzielt. Restliche Jobs in Warteschlange: " + jobs.Count);
    }

    public override void OnSuccess()
    {
        Debug.Log("Novel wurde erfolgreich generiert!");

        VisualNovel novel = NovelFactory.GenerateNovel(this.memory, this.idToKey, this.idToValue);

        GeneratedNovelManager.Instance().AddUserNovel(novel);
    }
}
