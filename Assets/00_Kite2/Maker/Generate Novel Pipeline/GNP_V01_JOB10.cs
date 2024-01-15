using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using UnityEngine;

public class GNP_V01_JOB10 : PipelineJob
{
    public GNP_V01_JOB10()
    {
        this.jobName = "Job10";
    }

    public override PipelineJobState HandleCompletion(string completion)
    {
        Match match = Regex.Match(completion, @"\[(.*?)\]");
        if (match.Success)
        {
            string result = match.Groups[1].Value;

            // Use result for what ever

            return PipelineJobState.COMPLETED;
        }
        else
        {
            return PipelineJobState.FAILED;
        }
    }

    public override void InitializePrompt()
    {
        StringBuilder stringBuilder = new StringBuilder();

        // Rolle
        stringBuilder.Append("Deine Rolle: Du bist eine fortschrittliche KI, spezialisiert auf die Generierung von Visual Novels, die den Gründungsprozess von Unternehmerinnen thematisieren. Dein Hauptaugenmerk liegt dabei auf der authentischen Darstellung von Herausforderungen und Diskriminierungen, denen Frauen im Gründungsprozess begegnen. Ziel ist es, Bewusstsein und Verständnis für die spezifischen Hürden zu schaffen, die Gründerinnen aufgrund ihres Geschlechts überwinden müssen. Deine Visual Novels sollen nicht nur informieren, sondern auch inspirieren und zum Nachdenken anregen. ");
        stringBuilder.AppendLine();

        // Aufgabe
        stringBuilder.Append("Deine Aufgabe ist es einen Dialogskript zu erstellen. Der Dialog findet zwischen Zwei Charakteren statt. ");
        stringBuilder.Append("Der Hauptcharakter heißt " + pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_MAIN_CHARACTER] + ". ");
        stringBuilder.Append("Der Hauptcharakter ist eine Gründerin. ");
        stringBuilder.Append("Der zweite Hauptcharakter und Gesprächspartner der Gründerin heißt: " + pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_TALKING_PARTNER] + ". ");
        stringBuilder.Append("Der zweite Hauptcharakter hat folgende Rolle: " + pipeline.GetMemory()[GenerateNovelPipeline.ROLE_OF_TALKING_PARTNER] + ". ");
        stringBuilder.Append(pipeline.GetMemory()[GenerateNovelPipeline.LOCATION]);
        stringBuilder.Append("Die Geschichte hat folgenden Titel: " + pipeline.GetMemory()[GenerateNovelPipeline.TITLE] + ". ");
        stringBuilder.Append("Das ist die Kurzbeschreibung der Geschichte: " + pipeline.GetMemory()[GenerateNovelPipeline.DESCRIPTION] + ". ");
        stringBuilder.Append("In der Geschichte geht es um folgendes: " + pipeline.GetMemory()[GenerateNovelPipeline.CONTEXT] + ". ");
        stringBuilder.AppendLine();

        // Output Format
        stringBuilder.Append("Das gewünschte Ergebnis:");
        stringBuilder.AppendLine();
        stringBuilder.Append("Bitte beachte, dass dein generiertes Ergebnis von einer speziellen Software weiterverarbeitet wird. Daher ist es entscheidend, dass du das Ergebnis in eckigen Klammern zurückgibst. Deine Antwort sollte direkt mit einer öffnenden eckigen Klammer '[' beginnen und mit einer schließenden eckigen Klammer ']' enden. Beispiel: [Dein generiertes Ergebnis]. Diese Formatierung ermöglicht eine reibungslose Integration und Verarbeitung deiner Ausgabe durch das nachgelagerte System. ");
        stringBuilder.Append("Ich benötige den Dialog im JSON-Stil. Der Dialog soll 38 Elemente haben. Jedes Element soll dabei folgendermaßen aufgbaut sein: [\r\n{\"key\": \"KEY\", \"value\": \"TEXT\"},\r\n...,\r\n{\"key\": \"KEY\", \"value\": \"TEXT\"}\r\n]. Erlaubte Keys sind dabei entweder der exakte Name der ersten Hauptfigur, oder der exakte Name der zweiten Hauptfigur oder das Wort 'info'. Info-Nachrichten sollten sich dabei an der ersten Hauptcharakter wenden (die Gründerin) und in der Du-Form geschrieben sein. Achte außerdem darauf dass die Elemente nummer 3, 5, 9, 20 und 24 Fragen sein sollen, die die zweite Hauptfigur an die erste Hauptfigur stellt. Dementsprechend sollen die Elemente 4, 6, 10, 21 und 25 Antworten sein, die der Hauptcharakter (Die Gründerin) auf die Fragen gibt. ");
        stringBuilder.AppendLine();

        prompt = stringBuilder.ToString();
    }
}
